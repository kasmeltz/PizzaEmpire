namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System;
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
            throw new NotImplementedException();

            BuildableItem other = entity as BuildableItem;
            BuildableItemAPI clone = new BuildableItemAPI();

            /*
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
             * */

            return clone;
        }

        /// <summary>
        /// Converts an API dto objec to a business object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IBusinessObjectEntity ToBusinessFormat(IAPIEntity entity)
        {
            throw new NotImplementedException();

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
