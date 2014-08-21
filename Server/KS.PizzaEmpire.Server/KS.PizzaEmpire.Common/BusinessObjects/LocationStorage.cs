namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents one storage at one particular business location
    /// </summary>
    public class LocationStorage
    {
         /// <summary>
        /// Creates a new instance of the LocationStorage class
        /// </summary>
        public LocationStorage() { }

        /// <summary>
        /// The items in this storage
        /// </summary>
        public Dictionary<BuildableItemEnum, ItemQuantity> Items { get; set; }

        /// <summary>
        /// Adds an item to the storage
        /// </summary>
        /// <param name="iq"></param>
        public void AddItem(ItemQuantity iq)
        {
            if (!Items.ContainsKey(iq.ItemCode))
            {
                Items[iq.ItemCode] = iq;
            }
            else
            {
                ItemQuantity existing = Items[iq.ItemCode];
                existing.UnStoredQuantity += existing.UnStoredQuantity;
                existing.StoredQuantity += existing.StoredQuantity;
                Items[iq.ItemCode] = existing;
            }                        
        }

        /// <summary>
        /// Gets an item from the storage
        /// </summary>
        /// <param name="iq"></param>
        public ItemQuantity GetItem(BuildableItemEnum itemCode)
        {
            if (!Items.ContainsKey(itemCode))
            {
                return null;
            }
            else
            {
                return Items[itemCode];
            }
        }

        /// <summary>
        /// Sets the level of an item
        /// </summary>
        /// <param name="iq"></param>
        public void SetLevel(ItemQuantity iq)
        {
            if (!Items.ContainsKey(iq.ItemCode))
            {
                Items[iq.ItemCode] = iq;
            }
            else
            {
                ItemQuantity existing = Items[iq.ItemCode];
                existing.Level = existing.Level;
                Items[iq.ItemCode] = existing;
            }
        }

        /// <summary>
        /// Removes an item from the storage
        /// </summary>
        /// <param name="iq"></param>
        public void RemoveItem(ItemQuantity iq)
        {
            if (Items.ContainsKey(iq.ItemCode))
            {
                ItemQuantity existing = Items[iq.ItemCode];
                existing.UnStoredQuantity -= existing.UnStoredQuantity;
                existing.StoredQuantity -= existing.StoredQuantity;
                if (existing.UnStoredQuantity <= 0 && existing.StoredQuantity <= 0)
                {
                    Items.Remove(iq.ItemCode); 
                } else
                {
                    Items[iq.ItemCode] = existing;
                }
            }
        }

        /// <summary>
        /// Transfers in item to storage
        /// </summary>
        /// <param name="iq"></param>
        public void TransferToStorage(ItemQuantity iq)
        {
            if (Items.ContainsKey(iq.ItemCode))
            {
                ItemQuantity existing = Items[iq.ItemCode];
                existing.UnStoredQuantity -= existing.UnStoredQuantity;
                existing.StoredQuantity += existing.StoredQuantity;
                Items[iq.ItemCode] = existing;
            }
        }      
    }
}
