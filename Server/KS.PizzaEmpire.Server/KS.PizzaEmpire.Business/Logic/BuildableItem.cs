namespace KS.PizzaEmpire.Business.Logic
{
    using Conversion;
    using ProtoBuf;
    using StorageInformation;
    using System.Collections.Generic;
    using System.IO;
    using TableStorage;

    /// <summary>
    /// Represents an item that can be built in the game
    /// </summary>
    public class BuildableItem : ILogicEntity, IToTableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItem class.
        /// </summary>
        public BuildableItem() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }

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
        /// The capacity (number of free slots) for doing work
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// The base amount of items that are produced when work is completed
        /// </summary>
        public int BaseProduction { get; set; }

        /// <summary>
        /// The production modified when this item is part of the required items
        /// </summary>
        public double ProductionMultiplier { get; set; }

        /// <summary>
        /// The maximum number of this item the player can own
        /// </summary>
        public int MaxQuantity { get; set; }

        /// <summary>
        /// Whether the item is consumed if required for other items
        /// </summary>
        public bool IsConsumable { get; set; }

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

        #region IToTableStorageEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return BuildableItemTableStorage.From(this);
        }

        #endregion

        /// <summary>
        /// Generates a new BuildableItem instance from a BuildableItemTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BuildableItem From(BuildableItemTableStorage other)
        {
            BuildableItem clone = new BuildableItem();

            clone.ItemCode = (BuildableItemEnum)other.ItemCode;
            clone.RequiredLevel = other.RequiredLevel;
            clone.CoinCost = other.CoinCost;
            clone.Capacity = other.Capacity;
            clone.BaseProduction = other.BaseProduction;
            clone.ProductionMultiplier = other.ProductionMultiplier;
            clone.MaxQuantity = other.MaxQuantity;
            clone.IsConsumable = other.IsConsumable;
            clone.Experience = other.Experience;
            clone.BuildSeconds  = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons  = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;

            using (MemoryStream memoryStream = new MemoryStream(other.RequiredItemsSerialized))
            {
                clone.RequiredItems = Serializer.Deserialize<List<ItemQuantity>>(memoryStream);
            }

            if (clone.RequiredItems == null)
            {
                clone.RequiredItems = new List<ItemQuantity>();
            }

            return clone;
        }
    }
}

