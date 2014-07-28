﻿namespace KS.PizzaEmpire.Business.TableStorage
{
    using KS.PizzaEmpire.Common.BusinessObjects;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Represents the data for a buildable item as stored in table storage.
    /// </summary>
    public class BuildableItemTableStorage : TableEntity, ITableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemTableStorage class.
        /// </summary>
        public BuildableItemTableStorage() { }

        /// <summary>
        /// Identifies the type of item
        /// </summary>
        public int ItemCode { get; set; }

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
        public int ProductionItem { get; set; }

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
        public int StorageItem { get; set; }

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
        public byte[] RequiredItemsSerialized { get; set; }
    }
}
