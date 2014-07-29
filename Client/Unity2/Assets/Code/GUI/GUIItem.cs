namespace KS.PizzaEmpire.Unity
{
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
        }

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
        /// The children items of this item
        /// </summary>
        public Dictionary<GUIElementEnum, GUIItem> Children { get; set; }

        /// <summary>
        /// The style for this item
        /// </summary>
        public GUIStyle Style { get; set; }

        /// <summary>
        /// Adds a child to an item
        /// </summary>
        public void AddChild(GUIItem item)
        {
            Children[item.Element] = item;
        }

        /// <summary>
        /// Removes a child from the item
        /// </summary>
        public void AddChild(GUIElementEnum element)
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
                    item.Draw();
                }
                GUI.EndGroup();
            }
        }
    }
}
