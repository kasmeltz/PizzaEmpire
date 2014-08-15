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
    public class GUIStateManager : GUIItem
    {
		private static volatile GUIStateManager instance;
		private static object syncRoot = new object();
		
		private GUIStateManager()
		{
			SetRectangle(0,0,Screen.width, Screen.height, false, ScaleMode.StretchToFill);
		}
		
		/// <summary>
		/// Provides the Singleton instance of the GUIStateManager
		/// </summary>
		public static GUIStateManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new GUIStateManager();
						}
					}
				}
				return instance;
			}
		}		
		
		/// <summary>
		/// Can handle up to 20 collisions with the mouse and screen objects per check
		/// </summary>
		private Collider2D[] collisions = new Collider2D[20];

		/// <summary>
		/// The currently tapped game object
		/// </summary>
		private GameObject currentlyTappedGameObject;

		/// <summary>
		/// The currently dragged game object
		/// </summary>
		private GameObject currentlyDraggedGameObject;
		
        /// The currently grabbed GUI item    
		private GUIItem currentlyGrabbedItem { get; set; }
			               
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
		public static bool AnyItem(GUIItem item, GUIItem other)
		{
			return true;
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
		public bool ReleaseItem(GUIItem grabbedItem)
		{
			if (grabbedItem == null)
			{
				return false;
			}
			
			GUIItem dropper = 
				GetItemAt(grabbedItem.Rectangle.x, grabbedItem.Rectangle.y, 
				          grabbedItem.Rectangle.width, grabbedItem.Rectangle.height, 
				          grabbedItem, AcceptsDraggable);
								
			if (dropper != null)
			{
				dropper.OnDrop(dropper, grabbedItem);
			}
			
			if (currentlyGrabbedItem.DuplicateOnDrag)
			{
				grabbedItem.Destroy();
				RemoveChild(GUIElementEnum.CurrentDraggable);
			}
			grabbedItem = null;

			return true;
		}
		
		/// <summary>
		/// Attempts to make the item at the provided coordinate 
		/// the curerntly dragged item
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public GUIItem GrabItem(float x, float y)
		{		
			GUIItem grabbedItem = GetItemAt(x - 2, y - 2, 4, 4, null, IsDraggable);
			if (grabbedItem != null)
			{
				Vector2 dragHandle = grabbedItem.DragHandle;
				dragHandle.x = x - (grabbedItem.Rectangle.x + grabbedItem.Offset.x);
				dragHandle.y = y - (grabbedItem.Rectangle.y + grabbedItem.Offset.y);
				grabbedItem.DragHandle = dragHandle;
															
				if (grabbedItem.DuplicateOnDrag)
				{
					GUIItem newItem = grabbedItem.Clone();					
										
					Rect rectangle = newItem.Rectangle;
					rectangle.x = newItem.Offset.x + rectangle.x;
					rectangle.y = newItem.Offset.y + rectangle.y;
					newItem.Rectangle = rectangle;
					
					Vector2 off = newItem.Offset;
					off.x = 0;
					off.y = 0;
					newItem.Offset = off;
					
					Children[GUIElementEnum.CurrentDraggable] = newItem;												
									
					grabbedItem = newItem;
				}
			}		

			return grabbedItem;
		}

		/// <summary>
		/// Gets the game world objects at the specified screen position
		/// n.b. for efficiency reasons the results are stored in the collisions
		/// array member.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public int GetGameWorldObjects(float x, float y)
		{
			Vector3 screenPos = new Vector3(x, y, Camera.main.nearClipPlane);			
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);		
			return Physics2D.OverlapPointNonAlloc(worldPos, collisions);
		}
		
		/// <summary>
		/// Handles game world tap at the given coordinates
		/// </summary>
		public void HandleGameWorldTap(float x, float y)
		{
			int collisionCount = GetGameWorldObjects (x, y);

			if (collisionCount > 0)
			{
				for (int i = 0;i < collisionCount;i++)
				{
					GameObject newlyTapped = collisions[i].gameObject;
					Tappable behaviour = newlyTapped.GetComponent<Tappable>();
					if (behaviour != null)
					{
						behaviour.Tap();
						currentlyTappedGameObject = newlyTapped;
						break;
					}
				}
			}
			else 
			{
				if (currentlyTappedGameObject != null)
				{
					Tappable behaviour = currentlyTappedGameObject.GetComponent<Tappable>();
					if (behaviour != null)
					{
						behaviour.UnTap();
						currentlyTappedGameObject = null;
					}
				}
			}
		}		

		/// <summary>
		/// Deals with tap events at the specified screen coordinates
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void HandleTap(float x, float y)
		{
			GUIItem anyItem = GetItemAt(x - 2, y - 2, 4, 4, null, AnyItem);
			
			if (anyItem == null)
			{
				HandleGameWorldTap(x, Screen.height - y);			
			}
		}
				
		/// <summary>
		/// Handles game world drag at the given coordinates
		/// </summary>
		public void HandleGameWorldDragBegin(float x, float y)
		{
			int collisionCount = GetGameWorldObjects (x, y);
			
			if (collisionCount > 0)
			{
				for (int i = 0;i < collisionCount;i++)
				{
					GameObject newlyDragged = collisions[i].gameObject;
					Draggable behaviour = newlyDragged.GetComponent<Draggable>();
					if (behaviour != null)
					{
						behaviour.Drag(new Vector3(x, Screen.height - y, Camera.main.nearClipPlane));
						currentlyDraggedGameObject = newlyDragged;
						break;
					}
				}
			}
		}		

		/// <summary>
		/// Handles the begin of a drag event
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void HandleDragBegin(float x, float y)
		{
			currentlyGrabbedItem = GrabItem(x, y);

			if (currentlyGrabbedItem == null)
			{
				HandleGameWorldDragBegin(x, Screen.height - y);			
			}
		}
			
		/// <summary>
		/// Handles game world drag at the given coordinates
		/// </summary>
		public void HandleGameWorldDrag(float x, float y)
		{
			if (currentlyDraggedGameObject != null)
			{
				Draggable behaviour = currentlyDraggedGameObject.GetComponent<Draggable>();
				if (behaviour != null)
				{
					behaviour.Drag(new Vector3(x, Screen.height - y, Camera.main.nearClipPlane));
				}
			}
		}

		/// <summary>
		/// Handles a drag event at the specified coordinate
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void HandleDrag(float x, float y)
		{
			if (currentlyGrabbedItem != null)
			{
				DragItem(currentlyGrabbedItem, x, y);
			} 
			else 
			{
				HandleGameWorldDrag(x, y);
			}
		}

		/// <summary>
		/// Handles game world drag at the given coordinates
		/// </summary>
		public void HandleGameWorldDragEnd(float x, float y)
		{
			if (currentlyDraggedGameObject != null)
			{
				Droppable behaviour = currentlyDraggedGameObject.GetComponent<Droppable>();
				if (behaviour != null)
				{
					behaviour.Drop(new Vector3(x, Screen.height - y, Camera.main.nearClipPlane));
				}
			}

			currentlyDraggedGameObject = null;
		}

		/// <summary>
		/// Handles a drag event at the specified coordinate
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void HandleDragEnd(float x, float y)
		{
			if (!ReleaseItem(currentlyGrabbedItem)) 
			{
				HandleGameWorldDragEnd(x, y);
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
				HandleDragBegin(mx, my);		
			}												

            if (Input.GetMouseButtonUp(0))
            {
				HandleTap(mx, my);
				HandleDragEnd(mx, my);            	
            }

			if (Input.GetMouseButton(0))
			{
				HandleDrag(mx, my);
			}          
        }

        #endif       
        
		#region GUIItem
		
		public override void Render () {}
		
		public override GUIItem Clone () { return null; }
		
		public override void Destroy() {}		
		
		#endregion  
    }
}
