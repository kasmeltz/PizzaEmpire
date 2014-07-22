namespace KS.PizzaEmpire.Business.TableStorage
{
    using Conversion;
    using Logic;
    using Microsoft.WindowsAzure.Storage.Table;
    using ProtoBuf;
    using System.IO;

    /// <summary>
    /// Represents the data for a buildable item as stored in table storage.
    /// </summary>
    public class BuildableItemTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
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
        public byte[] RequiredItemsSerialized { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return BuildableItem.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new BuildableItemTableStorage instance from a BuildableItem instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BuildableItemTableStorage From(BuildableItem other)
        {
            BuildableItemTableStorage clone = new BuildableItemTableStorage();

            clone.PartitionKey = other.StorageInformation.PartitionKey;
            clone.RowKey = other.StorageInformation.RowKey;

            clone.ItemCode = (int)other.ItemCode;
            clone.RequiredLevel = other.RequiredLevel;
            clone.CoinCost = other.CoinCost;
            clone.Capacity = other.Capacity;
            clone.BaseProduction = other.BaseProduction;
            clone.ProductionMultiplier = other.ProductionMultiplier;
            clone.MaxQuantity = other.MaxQuantity;
            clone.IsConsumable = other.IsConsumable;
            clone.Experience = other.Experience;
            clone.BuildSeconds = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, other.RequiredItems);
                clone.RequiredItemsSerialized = memoryStream.ToArray();
            }

            return clone;
        }

        #endregion
    }
}
