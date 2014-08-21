namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Common;
    using KS.PizzaEmpire.Business.Cache;
    using KS.PizzaEmpire.Business.ProtoSerializable;
    using KS.PizzaEmpire.Business.TableStorage;
    using KS.PizzaEmpire.Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents in item that contains information about storing a
    /// BuildableItem entity in various types of storage.
    /// </summary>
    public class BuildableItemStorageInformation : BaseStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public BuildableItemStorageInformation(string uniqueKey)
            : base(uniqueKey)
        {
            TableName = "BuildableItem";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "BI_" + uniqueKey;
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ICacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ICacheEntity ToCache(IBusinessObjectEntity item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromCache(ICacheEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity)
        {
            throw new NotImplementedException();

            /*
            BuildableItemTableStorage other = entity as BuildableItemTableStorage;

            BuildableItem clone = new BuildableItem();
     
            clone.ItemCode = (BuildableItemEnum)other.ItemCode;
            clone.RequiredLevel = other.RequiredLevel;
            clone.CoinCost = other.CoinCost;
            clone.ProductionItem = (BuildableItemEnum)other.ProductionItem;
            clone.ProductionCapacity = other.ProductionCapacity;
            clone.BaseProduction = other.BaseProduction;
            clone.StorageCapacity = other.StorageCapacity;
            clone.StorageItem = (BuildableItemEnum)other.StorageItem;
            clone.IsStorage = other.IsStorage;
            clone.IsConsumable = other.IsConsumable;
            clone.IsImmediate = other.IsImmediate;
            clone.IsWorkSubtracted = other.IsWorkSubtracted;
            clone.Experience = other.Experience;
            clone.BuildSeconds = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;

            List<ItemQuantityProtoBuf> wis = null;
            using (MemoryStream memoryStream = new MemoryStream(other.RequiredItemsSerialized))
            {
                wis = Serializer.Deserialize<List<ItemQuantityProtoBuf>>(memoryStream);
            }

            clone.RequiredItems = ItemQuantityProtoBuf.ToBusiness(wis);

            return clone;
             * */
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            throw new NotImplementedException();

            /*
            BuildableItem other = entity as BuildableItem;

            BuildableItemTableStorage clone = new BuildableItemTableStorage();

            clone.PartitionKey = PartitionKey;
            clone.RowKey = RowKey;

            clone.ItemCode = (int)other.ItemCode;
            clone.RequiredLevel = other.RequiredLevel;
            clone.CoinCost = other.CoinCost;
            clone.ProductionItem = (int)other.ProductionItem;
            clone.ProductionCapacity = other.ProductionCapacity;
            clone.BaseProduction = other.BaseProduction;
            clone.StorageCapacity = other.StorageCapacity;
            clone.StorageItem = (int)other.StorageItem;
            clone.IsStorage = other.IsStorage;
            clone.IsConsumable = other.IsConsumable;
            clone.IsImmediate = other.IsImmediate;
            clone.IsWorkSubtracted = other.IsWorkSubtracted;
            clone.Experience = other.Experience;
            clone.BuildSeconds = other.BuildSeconds;
            clone.CouponCost = other.CouponCost;
            clone.SpeedUpCoupons = other.SpeedUpCoupons;
            clone.SpeedUpSeconds = other.SpeedUpSeconds;

            List<ItemQuantityProtoBuf> wis = ItemQuantityProtoBuf.FromBusiness(other.RequiredItems);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, wis);
                clone.RequiredItemsSerialized = memoryStream.ToArray();
            }

            return clone;
             * */
        }
    }
}
