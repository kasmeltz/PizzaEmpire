namespace KS.PizzaEmpire.Unity
{
    using KS.PizzaEmpire.Common;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents an item in the GUI
    /// </summary>
    public class GUIItem
    {
        /// <summary>
        /// Creates a new instead of the GUIItem class
        /// </summary>
        public GUIItem(float x, float y, float w, float h)
        {
            Rectangle = new Rect(x, y, w, h);
            Children = new Dictionary<GUIElementEnum, GUIItem>();
            State = new GUIState();
            Offset = new Vector2();
        }

        /// <summary>
        /// The children items of this item
        /// </summary>
        protected Dictionary<GUIElementEnum, GUIItem> Children { get; set; }
       
        /// <summary>
        /// The items cumulative offset from screen position 0, 0
        /// </summary>
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Can this item be dragged?
        /// </summary>
        public bool Draggable { get; set; }

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
        public GUIItem GetChildNested(float x, float y)
        {
            foreach (GUIItem item in Children.Values)
            {
                if (!item.State.Visible || !item.State.Available)
                {
                    continue;
                }

                if (x > item.Rectangle.x && x < item.Rectangle.x + item.Rectangle.width &&
                       y > item.Rectangle.y && y < item.Rectangle.y + item.Rectangle.height)
                {
                    if (item.Draggable)
                    {
                        return item;
                    }

                    GUIItem found = item.GetChildNested(x - item.Rectangle.x, y - item.Rectangle.y);
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
    }
}
