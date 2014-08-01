namespace KS.PizzaEmpire.Unity
{
    using Common.BusinessObjects;
    using KS.PizzaEmpire.Common;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents an item that manages the state for some list of GUI elements
    /// </summary>
    public class GUIStateManager
    {
        protected Dictionary<GUIElementEnum, GUIItem> guis { get; set; }

        protected GUIItem GrabbedItem { get; set; }

        public GUIStateManager()
        {
            guis = new Dictionary<GUIElementEnum, GUIItem>();           
        }
        
        /// <summary>
        /// Updates the state of the gui elements
        /// </summary>
        public void UpdateState(GamePlayer player)
        {
            foreach(GUIItem gui in guis.Values)
            {               
                gui.State.Update(player);
            }
        }

        /// <summary>
        /// Updates the manager every frame
        /// </summary>
        public void Update(float dt)
        {
            HandleTouches();
            
            #if UNITY_STANDALONE_WIN
                HandleMouse();
            #endif
        }

        /// <summary>
        /// Deals with touch events
        /// </summary>
        public void HandleTouches()
        {
            int fingerCount = 0;
            foreach (Touch touch in Input.touches)
            {                
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {
                    fingerCount++;
                }
            }

            if (fingerCount > 0)
            { 
               Debug.Log(DateTime.Now + ": User has " + fingerCount + " finger(s) touching the screen");
            }                        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool IsDraggable(GUIItem item, GUIItem other)
        {
            return (item.Draggable != DraggableEnum.NONE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static bool AcceptsDraggable(GUIItem item, GUIItem other)
        {
            return (item.Droppable == other.Draggable);
        }

        /// <summary>
        /// Returns the object at the current location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GUIItem GetVisibleObject(float x, float y, float w, float h,
        	Func<GUIItem, GUIItem, bool> condition, GUIItem other)
        {			
			GUIItem found = null;
			
            foreach (GUIItem item in guis.Values)
            {
                if (!item.State.Visible || !item.State.Available)
                {
                    continue;
                }

                if (x + w > item.Rectangle.x && x < item.Rectangle.x + item.Rectangle.width &&
                       y + h > item.Rectangle.y && y < item.Rectangle.y + item.Rectangle.height)
                {
					if (condition(item, other))
                    {
                        return item;
                    }

                    found = item
						.GetVisibleObject(x - item.Rectangle.x, y - item.Rectangle.y, 
							w, h, other, condition);

					if (found != null)
					{
						return found;
					}
				}
            }
            
			return found;
        }

        /// <summary>
        /// Drags an item to the absolute screen position specified
        /// </summary>
        public void DragItem(GUIItem item, float x, float y)
        {
			Rect currentRect = item.Rectangle;
			currentRect.x = (x - item.Offset.x) - item.DragHandle.x;
			currentRect.y = (y - item.Offset.y) - item.DragHandle.y;
			item.Rectangle = currentRect;
        }
        
		/// <summary>
		/// Releases the currently grabbed item if there is one
		/// </summary>
		public void ReleaseItem()
		{
			if (GrabbedItem == null)
			{
				return;
			}
			
			GUIItem dropper = 
				GetVisibleObject(GrabbedItem.Rectangle.x, GrabbedItem.Rectangle.y, 
                 	GrabbedItem.Rectangle.width, GrabbedItem.Rectangle.height, 
					AcceptsDraggable, GrabbedItem);
					
			if (dropper == null)
			{
				dropper = 
					GetVisibleObject(GrabbedItem.Rectangle.x, GrabbedItem.Rectangle.y, 
						GrabbedItem.Rectangle.width, GrabbedItem.Rectangle.height, 
						AcceptsDraggable, GrabbedItem);
			}
					
			if (dropper != null)
			{
				dropper.OnDrop(dropper, GrabbedItem);
			}
			
			if (GrabbedItem.DuplicateOnDrag)
			{
				GUIItemFactory.Instance.Pool.Store(GrabbedItem);
				RemoveItem(GUIElementEnum.CurrentDraggable);
			}
			GrabbedItem = null;
		}
		
		/// <summary>
		/// Attempts to make the item at the provided coordinate 
		/// the curerntly dragged item
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void GrabItem(float x, float y)
		{		
			GrabbedItem = GetVisibleObject(x - 2, y - 2, 4, 4, IsDraggable, null);
			if (GrabbedItem != null)
			{
				Vector2 dragHandle = GrabbedItem.DragHandle;
				dragHandle.x = x - (GrabbedItem.Rectangle.x + GrabbedItem.Offset.x);
				dragHandle.y = y - (GrabbedItem.Rectangle.y + GrabbedItem.Offset.y);
				GrabbedItem.DragHandle = dragHandle;
															
				if (GrabbedItem.DuplicateOnDrag)
				{
					GUIItem newItem = GUIItemFactory.Instance.Pool.New();
					newItem.CopyFrom(GrabbedItem);	
										
					Rect rectangle = newItem.Rectangle;
					rectangle.x = newItem.Offset.x + rectangle.x;
					rectangle.y = newItem.Offset.y + rectangle.y;
					newItem.Rectangle = rectangle;
					
					Vector2 off = newItem.Offset;
					off.x = 0;
					off.y = 0;
					newItem.Offset = off;
					
					guis[GUIElementEnum.CurrentDraggable] = newItem;												
									
					GrabbedItem = newItem;
				}
			}				
		}
		
		#if UNITY_STANDALONE_WIN
		
		/// <summary>
        /// Handle the mouse input
        /// </summary>
        public void HandleMouse()
        {
            float mx = Input.mousePosition.x;
            float my = Screen.height- Input.mousePosition.y;

            if (Input.GetMouseButtonDown(0))
            {                
				GrabItem(mx, my);
			}

            if (Input.GetMouseButtonUp(0))
            {
            	ReleaseItem();
            }

			if (GrabbedItem != null)
            {
				DragItem(GrabbedItem, mx, my);
            }            
        }

        #endif

        /// <summary>
        /// Adds a GUIItem to the manager
        /// </summary>
        /// <param name="item"></param>
        public ErrorCode AddItem(GUIItem item)
        {
            if (guis.ContainsKey(item.Element))
            {
                return ErrorCode.ITEM_ALREADY_EXISTS;
            }
            guis[item.Element] = item;

            return ErrorCode.ERROR_OK;
        }

        /// <summary>
        /// Removes a GUIItem from the manager
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(GUIElementEnum element)
        {
            if (guis.ContainsKey(element))
            {
                guis.Remove(element);
            }
        }

        /// <summary>
        /// Returns the item with the specified element enum
        /// </summary>
        /// <param name="element"></param>
        public GUIItem GetItem(GUIElementEnum element)
        {
            if (guis.ContainsKey(element))
            {
                return guis[element];
            }

            return null;
        }

        /// <summary>
        /// Returns the item with the specified element enum looking recursively through items
        /// </summary>
        /// <param name="element"></param>
        public GUIItem GetItemNested(GUIElementEnum element)
        {
            if (guis.ContainsKey(element))
            {
                return guis[element];
            }
            foreach (GUIItem child in guis.Values)
            {
                GUIItem found = child.GetChildNested(element);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }

        /// <summary>
        /// Renders the GUI Manager
        /// </summary>
        public void OnGUI()
        {
            foreach (GUIItem item in guis.Values)
            {
                if (item.State.Available && item.State.Visible)
                {
                    item.Draw();
                }
            }
        }
    }
}
