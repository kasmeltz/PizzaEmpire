namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that is used to store another item in the game
    /// </summary>
    public class StorageItem : BuildableItem
    {
         /// <summary>
        /// Creates a new instance of the StorageItem class.
        /// </summary>
        public StorageItem() { }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public List<StorageItemStat> StorageStats { get; set; }
    }
}
