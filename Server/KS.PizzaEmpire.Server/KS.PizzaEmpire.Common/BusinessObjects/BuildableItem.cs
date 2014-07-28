namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;
    using System.IO;

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
        /// The level required to build this item
        /// </summary>
        public int RequiredLevel { get; set; }

        /// <summary>
        /// The cost in coins to build this item
        /// </summary>
        public int CoinCost { get; set; }

        /// <summary>
        /// The item that is required to produce this item
        /// </summary>
        public BuildableItemEnum ProductionItem { get; set; }

        /// <summary>
        /// The production capacity
        /// </summary>
        public int ProductionCapacity { get; set; }

        /// <summary>
        /// The base amount of items that are produced when work is completed
        /// </summary>
        public int BaseProduction { get; set; }

        /// <summary>
        /// The maximum number of items this item can store
        /// </summary>
        public int StorageCapacity { get; set; }

        /// <summary>
        /// The item this item should be stored in
        /// </summary>
        public BuildableItemEnum StorageItem { get; set; }

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
        /// The experience gained when this item is built
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// The number of seconds required to build this item
        /// </summary>
        public int BuildSeconds { get; set; }

        /// <summary>
        /// The number of coupons required to build this item
        /// </summary>
        public int CouponCost { get; set; }

        /// <summary>
        /// The number of coupons required to speed up this item
        /// </summary>
        public int SpeedUpCoupons { get; set; }

        /// <summary>
        /// The number of seconds this item will be sped up by specnding coupons
        /// </summary>
        public int SpeedUpSeconds { get; set; }

        /// <summary>
        /// The required items to build this item
        /// </summary>
        public List<ItemQuantity> RequiredItems { get; set; } 
    }
}

