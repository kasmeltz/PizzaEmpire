﻿namespace KS.PizzaEmpire.Unity
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
		
		private GUIStateManager() : base(0, 0, Screen.width, Screen.height)
		{
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
		
		private GameObject currentlyTapped;
		
        /// The currently grabbed item    
		public GUIItem GrabbedItem { get; protected set; }
			               
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
				GetItemAt(GrabbedItem.Rectangle.x, GrabbedItem.Rectangle.y, 
                 	GrabbedItem.Rectangle.width, GrabbedItem.Rectangle.height, 
				          GrabbedItem, AcceptsDraggable);
								
			if (dropper != null)
			{
				dropper.OnDrop(dropper, GrabbedItem);
			}
			
			if (GrabbedItem.DuplicateOnDrag)
			{
				GrabbedItem.Destroy();
				RemoveChild(GUIElementEnum.CurrentDraggable);
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
			GrabbedItem = GetItemAt(x - 2, y - 2, 4, 4, null, IsDraggable);
			if (GrabbedItem != null)
			{
				Vector2 dragHandle = GrabbedItem.DragHandle;
				dragHandle.x = x - (GrabbedItem.Rectangle.x + GrabbedItem.Offset.x);
				dragHandle.y = y - (GrabbedItem.Rectangle.y + GrabbedItem.Offset.y);
				GrabbedItem.DragHandle = dragHandle;
															
				if (GrabbedItem.DuplicateOnDrag)
				{
					GUIItem newItem = GrabbedItem.Clone();					
										
					Rect rectangle = newItem.Rectangle;
					rectangle.x = newItem.Offset.x + rectangle.x;
					rectangle.y = newItem.Offset.y + rectangle.y;
					newItem.Rectangle = rectangle;
					
					Vector2 off = newItem.Offset;
					off.x = 0;
					off.y = 0;
					newItem.Offset = off;
					
					Children[GUIElementEnum.CurrentDraggable] = newItem;												
									
					GrabbedItem = newItem;
				}
			}				
		}
		
		/// <summary>
		/// Finds all items that the mouse is currently touching
		/// </summary>
		public void ScreenMouseRay(float x, float y)
		{
			Vector3 screenPos = new Vector3(x, y, Camera.main.nearClipPlane);			
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);		
			int collisionCount = Physics2D.OverlapPointNonAlloc(worldPos, collisions);
						
			if (collisionCount > 0)
			{
				for (int i = 0;i < collisionCount;i++)
				{
					GameObject newlyTapped = collisions[i].gameObject;
					newlyTapped.SendMessage(GUIMessages.Tapped, SendMessageOptions.DontRequireReceiver);			
				}
			}
			else
			{
				if (currentlyTapped != null)
				{
					currentlyTapped.SendMessage(GUIMessages.UnTapped, SendMessageOptions.DontRequireReceiver);
				}
			}
		}		
		
		/// <summary>
		/// Called when a tap has been handled
		/// </summary>
		public void TapHandled(GameObject tapHandler, GUIElementEnum element)
		{
			if (currentlyTapped != null && currentlyTapped != tapHandler)
			{
				currentlyTapped.SendMessage(GUIMessages.UnTapped, SendMessageOptions.DontRequireReceiver);
			}
			
			currentlyTapped = tapHandler;
			
			if (!TutorialManager.Instance.IsFinished)
			{
				GUIEvent gEvent = new GUIEvent(element, GUIEventEnum.Tap);
				TutorialManager.Instance.TryAdvance(gEvent);
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
				if (GrabbedItem == null)
				{
					ScreenMouseRay(mx, Screen.height - my);			
				}				
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
        
		#region GUIItem
		
		public override void Render () {}
		
		public override GUIItem Clone () { return null; }
		
		public override void Destroy() {}		
		
		#endregion  
    }
}
