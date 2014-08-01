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

        protected GUIItem FocusedItem { get; set; }

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
        /// Returns the focusable object at the current location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GUIItem GetFocusedObject(float x, float y)
        {			
			GUIItem found = null;
			
            foreach (GUIItem item in guis.Values)
            {
                if (!item.State.Visible || !item.State.Available)
                {
                    continue;
                }

                if (x > item.Rectangle.x && x < item.Rectangle.x + item.Rectangle.width &&
                       y > item.Rectangle.y && y < item.Rectangle.y + item.Rectangle.height)
                {
                    if (item.Draggable != DraggableEnum.NONE)
                    {
                        return item;
                    }

					found = item.GetChildNested(x - item.Rectangle.x, y - item.Rectangle.y);
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
			currentRect.x = x - item.Offset.x;
			currentRect.y = y - item.Offset.y;
			item.Rectangle = currentRect;
        }
        
		/// <summary>
		/// Unfocuses the currently focused item
		/// </summary>
		public void UnFocusItem()
		{
			if (FocusedItem == null)
			{
				return;
			}
			
			if (FocusedItem.DuplicateOnDrag)
			{
				GUIItemFactory.Instance.Pool.Store(FocusedItem);
				RemoveItem(GUIElementEnum.CurrentDraggable);
			}
			FocusedItem = null;
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
				FocusedItem = GetFocusedObject(mx, my);
				if (FocusedItem != null)
				{
					if (FocusedItem.DuplicateOnDrag)
					{
						GUIItem newItem = GUIItemFactory.Instance.Pool.New();
						newItem.CopyFrom(FocusedItem);	
						
						/*
						Debug.Log("Before FocusedItem: " + FocusedItem.Rectangle);
						Debug.Log("Before FocusedItem: " + FocusedItem.Offset);
						
						Debug.Log("Before newItem: " + newItem.Rectangle);
						Debug.Log("Before newItem: " + newItem.Offset);
						*/
						
						Rect rectangle = newItem.Rectangle;
						rectangle.x = newItem.Offset.x + rectangle.x;
						rectangle.y = newItem.Offset.y + rectangle.y;
						newItem.Rectangle = rectangle;
						
						Vector2 off = newItem.Offset;
						off.x = 0;
						off.y = 0;
						newItem.Offset = off;
												
						guis[GUIElementEnum.CurrentDraggable] = newItem;												
						
						/*
						Debug.Log("After FocusedItem: " + FocusedItem.Rectangle);
						Debug.Log("After FocusedItem: " + FocusedItem.Offset);						
						
						Debug.Log("After newItem: " + newItem.Rectangle);
						Debug.Log("After newItem: " + newItem.Offset);
						*/
						
						FocusedItem = newItem;
					}
				}
			}

            if (Input.GetMouseButtonUp(0))
            {
            	UnFocusItem();
            }

            if (FocusedItem != null)
            {
                DragItem(FocusedItem, mx, my);
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
