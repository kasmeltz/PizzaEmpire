namespace KS.PizzaEmpire.Unity
{
    using Common.BusinessObjects;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Represents an item that manages the state for some list of GUI elements
    /// </summary>
    public class GUIStateManager
    {
        protected Dictionary<GUIElementEnum, GUIItem> guis { get; set; }

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
                Debug.Log(DateTime.Now + ": Updating state for: " + gui);
                gui.State.Update(player);
            }
        }

        /// <summary>
        /// Updates the manager every frame
        /// </summary>
        public void Update(float dt)
        {
            HandleTouches();
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
                    fingerCount++;
                
            }

            if (fingerCount > 0)
            { 
               Debug.Log(DateTime.Now + ": User has " + fingerCount + " finger(s) touching the screen");
            }        
        }

        /// <summary>
        /// Adds a GUIItem to the manager
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(GUIItem item)
        {
            guis[item.Element] = item;
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
            foreach (GUIItem gui in guis.Values)
            {
                if (gui.State.Available && gui.State.Visible)
                {
                    gui.Draw();
                }
            }
        }
    }
}
