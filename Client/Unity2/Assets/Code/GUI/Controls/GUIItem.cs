namespace KS.PizzaEmpire.Unity
{
    using System;
	using System.Collections.Generic;
	using Common;
	using Common.BusinessObjects;
    using Common.GameLogic;
    using Common.GameLogic.GamePlayerState;
    using Common.ObjectPool;
    using UnityEngine;

    /// <summary>
    /// Represents an item in the GUI
    /// </summary>
    public abstract class GUIItem: IResetable
    {
    	public static Color DisabledColor = 
    		new Color(0.65f, 0.65f, 0.65f, 0.65f);
    	
    	/// <summary>
    	/// Creates a new instance of the GUIItem class
		/// </summary>
		public GUIItem()
    	{
			toRemove = new List<GUIElementEnum>();
			Children = new Dictionary<GUIElementEnum, GUIItem>();
			Offset = new Vector2();
			Draggable = DraggableEnum.NONE;
			Droppable = DraggableEnum.NONE;
			DragHandle = Vector2.zero;
			Available = true;
			Enabled = true;
			Visible = true;
			Content = new GUIContent();
		}    			          
        
        /// <summary>
        /// Sets the rectangle for this item
		/// <summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
		public virtual void SetRectangle(float x, float y, float w, float h, bool isWorld, ScaleMode scale)
		{
			Scale = scale;
			WorldCoords = Vector2.zero;
			IsWorld = isWorld;		

			if (w <= 1 && h <= 1 )
			{
				w = Screen.width * w;
				h = Screen.height * h;
			}		

			if (isWorld)
			{											
				WorldCoords = new Vector2(x, y);
			} 
			else
			{		
				if (x <= 1 && y <= 1)
				{
					x = Screen.width * x;
					y = Screen.height * y;
				}				
			}
			
			if (Scale == ScaleMode.ScaleToFit)
			{
				if (Content.image != null)
				{	
					float r = 
						Math.Min(w / Content.image.width, 
						         h / Content.image.height);
					
					w = Content.image.width * r;
					h = Content.image.height * r;
				}
			}
			
			Rectangle = new Rect(x, y, w, h);
		}
					
        /// <summary>
        /// The children items of this item
        /// </summary>
        protected Dictionary<GUIElementEnum, GUIItem> Children { get; set; }    
        
        /// <summary>
        /// The rendered content of this item
        /// </summary>
        /// <value>The content.</value>
        public GUIContent Content { get; set; }
        
        // set to true when drawing
		public bool IsDrawing { get; set; }
		
		/// <summary>
		/// A list of items to remove at the end of the draw cycle
		/// </summary>
        protected List<GUIElementEnum> toRemove { get; set; }
        
        /// <summary>
        /// Whether the children have been modified
        /// </summary>
		public bool ChildrenModified { get; set; }   

		/// <summary>
		/// The parent of this item
		/// </summary>
		/// <value>The parent of this item</value>
		public GUIItem Parent { get; protected set; }
       
		/// <summary>
		/// The buildable item this GUI item represents
		/// </summary>
		/// <value>The buildable item this GUI item represents</value>
		public BuildableItemEnum BuildableItem { get; set; }

       	/// <summary>
       	/// Gets or sets the drag handle.
       	/// </summary>
       	/// <value>The drag handle.</value>
       	public Vector2 DragHandle { get; set; }
       
        /// <summary>
        /// The items cumulative offset from screen position 0, 0
        /// </summary>
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Draggable category for this item
        /// </summary>
        public DraggableEnum Draggable { get; set; }
        
		/// <summary>
		/// Draggable category for this item
		/// </summary>
		public DraggableEnum Droppable { get; set; }
		
        /// <summary>
        /// Whether this item should be duplicated on drag
        /// </summary>
        public bool DuplicateOnDrag { get; set; }                

        /// <summary>
        /// The element identifier of this item
        /// </summary>
        public GUIElementEnum Element { get; set; }

        /// <summary>
        /// The state that the game player must be in 
        /// for this item to be available
        /// </summary>
        public GamePlayerStateCheck AvailableCheck { get; set; }

		/// <summary>
		/// The state that the game player must be in
		/// for this item to be enabled
		/// </summary>
		public GamePlayerStateCheck EnabledCheck { get; set; }
		
        /// <summary>
        /// The Render action for this item
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Called when an appropriate item is dropped on this item
        /// </summary>
        public Action<GUIItem, GUIItem> OnDrop { get; set;  }

        /// <summary>
        /// The position of this item, with respect to its parent
        /// </summary>
        public Rect Rectangle { get; set; }
        
        //// <summary>
        /// The scaling mode that will be used for this iteam
        /// </summary>
		public ScaleMode Scale { get; protected set; }
        
        /// <summary>
        /// The coordinates of the item in world space
        /// </summary>
        public Vector2 WorldCoords { get; set; }
        
        /// <summary>
        /// Whether the item is in world coords.
        /// </summary>
        public bool IsWorld { get; set; }        

        /// <summary>
        /// The style for this item
        /// </summary>
        public GUIStyle Style { get; set; }

        /// <summary>
        /// Whether this item is animated
        /// </summary>
        /// <value><c>true</c> if animated; otherwise, <c>false</c>.</value>
        public bool Animated { get ; set; }
        
        /// <summary>
        /// Whether this item is currently available
        /// </summary>
        /// <value>True if the item is available, false otherwise</value>
        public bool Available { get; set; }

		/// <summary>
		/// Whether this item is currently visible
		/// </summary>
		/// <value>True if the item is visible, false otherwise</value>
		public bool Visible { get; set; }
		
		/// <summary>
		/// Whether the item is currently enabled
		/// </summary>
		/// <value>True if the item is enabled, false otherwise</value>
		public bool Enabled { get; set; }

		/// <summary>
		/// An action to be performed when the state of the item is updated.
		/// </summayr>
		public Action<GUIItem> OnStateUpdate { get; set; }
		
		/// <summary>
		/// Whether this item will be displayed
		/// </summary>
		/// <value><c>true</c> if displayed; otherwise, <c>false</c>.</value>
		public bool Displayed 
		{ 
		 	get 
		 	{
		 	 	return Available && Visible;
		 	}
 	 	}

		/// <summary>
		/// Whether this o		/// </summary>
		/// <value><c>true</c> if traversed; otherwise, <c>false</c>.</value>
		public bool Traversed
		{
			get
			{
				return Available && Visible && Enabled;
			}
		}
		
		
        /// <summary>
        /// Adds a child to an item
        /// </summary>
        public ErrorCode AddChild(GUIItem item)
        {
            if (Children.ContainsKey(item.Element))
            {
                return ErrorCode.ITEM_ALREADY_EXISTS;
            }
            
            item.Parent = this;

            Vector2 offset = item.Offset;
            offset.x = Offset.x + Rectangle.x;
            offset.y = Offset.y + Rectangle.y;
            item.Offset = offset;

            Children[item.Element] = item;

			ChildrenModified = true;
			
            return ErrorCode.ERROR_OK;
        }

        /// <summary>
        /// Removes a child from the item
        /// </summary>
        public void RemoveChild(GUIElementEnum element)
        {
        	if (IsDrawing)
        	{
        		toRemove.Add(element);
        	}
        	else
        	{
	            if (Children.ContainsKey(element))
	            {
	                Children.Remove(element);
					ChildrenModified = true;    
	            }
            }			    
        }
        
        /// <summary>
        /// Removes any children in the toremove list
        /// </summary>
        protected void RemoveAllChildren()
        {
        	for(int i = 0;i < toRemove.Count;i++)
        	{
        		RemoveChild(toRemove[i]);
        	}
        	toRemove.Clear();
        }

        /// <summary>
        /// Returns the child with the specified element enum
        /// </summary>
        /// <param name="element"></param>
        public GUIItem GetChild(GUIElementEnum element)
        {
            if (Children.ContainsKey(element))
            {
                return Children[element];
            }

            return null;
        }

        /// <summary>
        /// Returns the child with the specified element enum looking recursively
        /// through child items
        /// </summary>
        /// <param name="element"></param>
        public GUIItem GetChildNested(GUIElementEnum element)
        {
            if (Children.ContainsKey(element))
            {
                return Children[element];
            }
            
            foreach (GUIItem child in Children.Values)
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
        /// Returns the child at the specified position looking recursively
        /// through child items
        /// </summary>
        /// <param name="element"></param>
        public GUIItem GetItemAt(float x, float y, float w, float h,
        	GUIItem other, Func<GUIItem, GUIItem, bool> condition)
        {
            foreach (GUIItem item in Children.Values)
            {
				if (!item.Traversed)
                {
                    continue;
                }

                if (x + w > item.Rectangle.x && x < item.Rectangle.x + item.Rectangle.width &&
                       y  + h > item.Rectangle.y && y < item.Rectangle.y + item.Rectangle.height)
                {
                    if (condition(item, other))
                    {
                        return item;
                    }

                    GUIItem found = item
						.GetItemAt(x - item.Rectangle.x, y - item.Rectangle.y, w, h, 
                        	other, condition);

                    if (found != null)
                    {
                        return found;
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// Animates the item every frame. 
        /// Only called if Animate = true
		/// </summary>
        public virtual void Animate(float dt) { }
        
        /// <summary>
        /// Transforms the items world coordinates to screen coordinates
        /// </summary>
        public Rect WorldToScreen()
        {
			Vector2 screenPos = Camera.main.WorldToScreenPoint(WorldCoords);
			Rect rectangle = Rectangle;
			rectangle.x = screenPos.x;
			rectangle.y = Screen.height - screenPos.y;
			return rectangle;
        }

        /// <summary>
        ///  Draws the item
        /// </summary>
        public void Draw(float dt)
        {
        	if (!Displayed)
        	{
        		return;
        	}

			IsDrawing = true;
			
			if (IsWorld)
			{
				Rectangle = WorldToScreen();
			}
			
			if (Animated)
			{
				Animate(dt);
			}
						
			if (!Enabled)
			{
				GUI.color = DisabledColor;
			}			
			Render();
			if (!Enabled)
			{
				GUI.color = Color.white;
			}
				
			if (Children.Count > 0)
            {                
				GUI.BeginGroup(Rectangle);
                foreach (GUIItem item in Children.Values)
                {
	                item.Draw(dt);
                }
                GUI.EndGroup();
            }
            
			IsDrawing = false;
			
			if (toRemove.Count > 0)
			{
				RemoveAllChildren();
			}
        }
        
		/// <summary>
		/// Updates the Avaiable and Enabled statii based on the 
		/// current state of the provided GamePlayer
		/// </summary>
		public void UpdateState(GamePlayer player)
		{
			Available = true;
			Enabled = true;
			
			if (AvailableCheck != null)
			{
				if (!AvailableCheck.IsValid(player))
				{
					Available = false;
				}
			}
			
			if (EnabledCheck != null)
			{
				if (!EnabledCheck.IsValid(player))
				{
					Enabled = false;
				}
			}

			if (OnStateUpdate != null) 
			{
				OnStateUpdate (this);
			}
			
			foreach(GUIItem item in Children.Values)
			{
				item.UpdateState(player);
			}
		}
		
		/// <summary>
		/// Clone this instance.
		/// </summary>
		public abstract GUIItem Clone();
		
		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public abstract void Destroy();
		
		/// <summary>
		/// Copies the state from another instance
		/// </summary>
		/// <param name="other">The GUIItem to copy from</param>
		public virtual void CopyFrom(GUIItem other)
        {
        	Children.Clear();
        	foreach(GUIItem item in other.Children.Values)
        	{
				Children.Add(item.Element, item);
        	}		
			Animated = other.Animated;
			IsWorld = other.IsWorld;
			Vector2 world = WorldCoords;
			world.x = other.WorldCoords.x;
			world.y = other.WorldCoords.y;
			WorldCoords = world;						
        	Parent = other.Parent;	
			BuildableItem = other.BuildableItem;
			Vector2 off = Offset;
			off.x = other.Offset.x;
			off.y = other.Offset.y;
			Offset = off;
			Draggable = other.Draggable;
			Droppable = other.Droppable;
			Vector2 drag = DragHandle;
			drag.x = other.DragHandle.x;
			drag.y = other.DragHandle.y;
			DragHandle = drag;
			DuplicateOnDrag = other.DuplicateOnDrag;
            OnDrop = other.OnDrop;
			Element = other.Element;
			Rect rectangle = Rectangle;
			rectangle.x = other.Rectangle.x;
			rectangle.y = other.Rectangle.y;
			rectangle.width = other.Rectangle.width;
			rectangle.height = other.Rectangle.height;
			Rectangle = rectangle;
			Style = other.Style;
			Content.image = other.Content.image;
			Content.text = other.Content.text;			
			Available = other.Available;
			Visible = other.Visible;
			Enabled = other.Enabled;
			if (AvailableCheck != null)
			{
				AvailableCheck.CopyFrom(other.AvailableCheck);
			}
			if (EnabledCheck != null) 
			{				
				EnabledCheck.CopyFrom(other.EnabledCheck);
			}
			Scale = other.Scale;
        }
        
		#region IResetable
		
		public virtual void Reset()
		{
			toRemove.Clear();
			Children.Clear();
			Animated = false;
			IsWorld = false;
			Vector2 world = WorldCoords;
			world.x = 0;
			world.y = 0;
			WorldCoords = world;			
			Parent = null;
			BuildableItem = BuildableItemEnum.None;
			Vector2 off = Offset;
			off.x = 0;
			off.y = 0;
			Offset = off;
			Draggable = DraggableEnum.NONE;
			Droppable = DraggableEnum.NONE;
			Vector2 drag = DragHandle;
			drag.x = 0;
			drag.y = 0;
			DragHandle = drag;
			DuplicateOnDrag = false;
            OnDrop = null;
			Element = GUIElementEnum.None;
			Rect rectangle = Rectangle;
			rectangle.x = 0;
			rectangle.y = 0;
			rectangle.width = 0;			
			rectangle.height = 0;
			Rectangle = rectangle;
			Style = null;
			Content.text = string.Empty;
			Content.image = null;
			Available = false;
			Enabled = false;
			Visible = false;
			if (AvailableCheck != null)
			{
				AvailableCheck.Reset();
			}
			if (EnabledCheck != null)
			{
				EnabledCheck.Reset();
			}
			Scale = ScaleMode.ScaleToFit;
		}
		
		#endregion
	}
}
