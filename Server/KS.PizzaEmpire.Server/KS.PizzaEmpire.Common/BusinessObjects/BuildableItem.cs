namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that can be built in the game
    /// </summary>
    public class BuildableItem : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItem class.
        /// </summary>
        public BuildableItem() { }

        /// <summary>
        /// Identifies the type of item
        /// </summary>
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// Whether the item is used for storage
        /// </summary>
        public bool IsStorage { get; set; }

        /// <summary>
        /// Whether the item is consumed if required for other items
        /// </summary>
        public bool IsConsumable { get; set; }

        /// <summary>
        /// Whether working on this item happens immediately
        /// </summary>
        public bool IsImmediate { get; set; }

        /// <summary>
        /// Whether doing work subtracts from the quantity of this item
        /// </summary>
        public bool IsWorkSubtracted { get; set; }

        /// <summary>
        /// The item that is required to produce this item
        /// </summary>
        public BuildableItemEnum ProductionItem { get; set; }

        /// <summary>
        /// The item this item should be stored in
        /// </summary>
        public BuildableItemEnum StorageItem { get; set; }

        /// <summary>
        /// The storage area this item is linked to
        /// </summary>
        public InventoryStorageEnum StorageArea { get; set; }        

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public Dictionary<int, BuildableItemStat> Stats { get; set; }

        /// <summary>
        /// Returns the stats for the requested level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public BuildableItemStat GetStat(int level)
        {
            if (!Stats.ContainsKey(level))
            {
                return null;
            }

            return Stats[level];                
        }
    }
}

