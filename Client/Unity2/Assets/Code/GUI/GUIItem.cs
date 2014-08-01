namespace KS.PizzaEmpire.Unity
{
    using Common;
    using System;
    using System.Collections.Generic;
    using Common.ObjectPool;
    using UnityEngine;

    /// <summary>
    /// Represents an item in the GUI
    /// </summary>
    public class GUIItem: IResetable
    {
    	/// <summary>
    	/// Creates a new instance of the GUIItem class
		/// </summary>
    	public GUIItem() : this(0,0,0,0)
    	{
		}    	
		
		/// <summary>
		/// Creates a new instance of the GUIItem class
        /// </summary>
        public GUIItem(float x, float y, float w, float h)
        {
            Rectangle = new Rect(x, y, w, h);
            Children = new Dictionary<GUIElementEnum, GUIItem>();
            State = new GUIState();
            Offset = new Vector2();
            Draggable = DraggableEnum.NONE;
            Droppable = DraggableEnum.NONE;
            DragHandle = Vector2.zero;
        }

        /// <summary>
        /// The children items of this item
        /// </summary>
        protected Dictionary<GUIElementEnum, GUIItem> Children { get; set; }
       
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
        /// The state of this item
        /// </summary>
        public GUIState State { get; set; }

        /// <summary>
        /// The Render action for this item
        /// </summary>
        public Action<GUIItem> Render { get; set; }

        /// <summary>
        /// Called when an appropriate item is dropped on this item
        /// </summary>
        public Action<GUIItem, GUIItem> OnDrop { get; set;  }

        /// <summary>
        /// The position of this item, with respect to its parent
        /// </summary>
        public Rect Rectangle { get; set; }

        /// <summary>
        /// The style for this item
        /// </summary>
        public GUIStyle Style { get; set; }

        /// <summary>
        /// The texture for this item
        /// </summary>
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// The text for this item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Adds a child to an item
        /// </summary>
        public ErrorCode AddChild(GUIItem item)
        {
            if (Children.ContainsKey(item.Element))
            {
                return ErrorCode.ITEM_ALREADY_EXISTS;
            }

            Vector2 offset = item.Offset;
            offset.x = Offset.x + Rectangle.x;
            offset.y = Offset.y + Rectangle.y;
            item.Offset = offset;

            Children[item.Element] = item;

            return ErrorCode.ERROR_OK;
        }

        /// <summary>
        /// Removes a child from the item
        /// </summary>
        public void RemoveChild(GUIElementEnum element)
        {
            if (Children.ContainsKey(element))
            {
                Children.Remove(element);
            }            
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
        public GUIItem GetVisibleObject(float x, float y, float w, float h,
        	GUIItem other, Func<GUIItem, GUIItem, bool> condition)
        {
            foreach (GUIItem item in Children.Values)
            {
                if (!item.State.Visible || !item.State.Available)
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
                        .GetVisibleObject(x - item.Rectangle.x, y - item.Rectangle.y, w, h, 
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
        ///  Draws the item
        /// </summary>
        public void Draw()
        {
            if (Children.Count == 0)
            {
                Render(this);
            }
            else
            {
                GUI.BeginGroup(Rectangle, Style);
                Render(this);
                foreach (GUIItem item in Children.Values)
                {
                    if (item.State.Available && item.State.Visible)
                    {
                        item.Draw();
                    }
                }
                GUI.EndGroup();
            }
        }
        
        /// <summary>
        /// Copies the state from another instance
		/// </summary>
        /// <param name="other">The GUIItem to copy from</param>
        public void CopyFrom(GUIItem other)
        {
        	Children.Clear();
        	foreach(GUIItem item in other.Children.Values)
        	{
				Children.Add(item.Element, item);
        	}			
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
			State.CopyFrom(other.State);
			Render = other.Render;
			Rect rectangle = Rectangle;
			rectangle.x = other.Rectangle.x;
			rectangle.y = other.Rectangle.y;
			rectangle.width = other.Rectangle.width;
			rectangle.height = other.Rectangle.height;
			Rectangle = rectangle;
			Style = other.Style;
			Texture = other.Texture;
			Text = other.Text;
        }
        
		#region IResetable
		
		public void Reset()
		{
			Children.Clear();
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
			State.Reset();
			Render = null;
			Rect rectangle = Rectangle;
			rectangle.x = 0;
			rectangle.y = 0;
			rectangle.width = 0;			
			rectangle.height = 0;
			Rectangle = rectangle;
			Style = null;
			Texture = null;
			Text = null;
		}
		
		#endregion
	}
}
