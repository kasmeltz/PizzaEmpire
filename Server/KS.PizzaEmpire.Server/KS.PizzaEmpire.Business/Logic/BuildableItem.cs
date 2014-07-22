namespace KS.PizzaEmpire.Business.Logic
{
    using Conversion;
    using StorageInformation;
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

        public int ItemCode { get; set; }
        public string Name { get; set; }
        public int CoinCost { get; set; }
        public int Quantity { get; set; }
        public int Experience { get; set; }
        public int BuildSeconds { get; set; }
        public int CouponCost { get; set; }
        public int SpeedUpCoupons { get; set; }

        public Recipe Recipe { get; set; }

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
        public static BuildableItem From(BuildableItemTableStorage item)
        {
            BuildableItem clone = new BuildableItem();

            clone.ItemCode = item.ItemCode;
            clone.Name = item.Name;
            clone.CoinCost = item.CoinCost;
            clone.Quantity = item.Quantity;
            clone.Experience = item.Experience;
            clone.BuildSeconds = item.BuildSeconds;
            clone.CouponCost = item.CouponCost;
            clone.SpeedUpCoupons = item.SpeedUpCoupons;

            return clone;
        }
    }
}
