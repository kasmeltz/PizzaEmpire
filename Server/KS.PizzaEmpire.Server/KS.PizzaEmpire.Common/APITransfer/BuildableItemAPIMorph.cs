namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    /// <summary>
    /// Class that can morph BuildableItem objects to and from a dto format
    /// </summary>
    public class BuildableItemAPIMorph : IAPIEntityMorph
    {
        /// <summary>
        /// Converts a business object to an API dto object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IAPIEntity ToAPIFormat(IBusinessObjectEntity entity)
        {
            BuildableItem other = entity as BuildableItem;
            BuildableItemAPI clone = new BuildableItemAPI();

            /*

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
            */

            clone.ItemCode = other.ItemCode;
            clone.IsStorage = other.IsStorage;
            clone.IsConsumable = other.IsConsumable;
            clone.IsImmediate = other.IsImmediate;
            clone.IsWorkSubtracted = other.IsWorkSubtracted;

            clone.ProductionItem = other.ProductionItem;
            clone.ProductionCapacity = other.ProductionCapacity;
            clone.BaseProduction = other.BaseProduction;
            clone.StorageCapacity = other.StorageCapacity;
            clone.StorageItem = other.StorageItem;
           
            clone.Experience = other.Experience;
            clone.BuildSeconds = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;
            clone.RequiredItems = other.RequiredItems;

            return clone;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            BuildableItemAPI other = entity as BuildableItemAPI;
            BuildableItem clone = new BuildableItem();

            /*

            clone.ItemCode = other.ItemCode;
            clone.RequiredLevel = other.RequiredLevel;
            clone.CoinCost = other.CoinCost;
            clone.ProductionItem = other.ProductionItem;
            clone.ProductionCapacity = other.ProductionCapacity;
            clone.BaseProduction = other.BaseProduction;
            clone.StorageCapacity = other.StorageCapacity;
            clone.StorageItem = other.StorageItem;
            clone.IsConsumable = other.IsConsumable;
            clone.IsImmediate = other.IsImmediate;
            clone.IsWorkSubtracted = other.IsWorkSubtracted;
            clone.Experience = other.Experience;
            clone.BuildSeconds = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;
            clone.RequiredItems = other.RequiredItems;
             * */

            return clone;
        }
    }
}
