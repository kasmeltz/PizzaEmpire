namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Cache;
    using TableStorage;
    using System.IO;
    using ProtoBuf;
    using KS.PizzaEmpire.Common.BusinessObjects;
    using System.Collections.Generic;
using KS.PizzaEmpire.Business.ProtoSerializable;
    
    /// <summary>
    /// Represents in item that contains information about storing an 
    /// GamePlayer entity in various types of storage.
    /// </summary>
    public class GamePlayerStorageInformation : BaseStorageInformation
    {       
        /// <summary>
        /// Creates a new instance of the GamePlayerStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public GamePlayerStorageInformation(string uniqueKey) 
            : base(uniqueKey)
        {
            TableName = "GamePlayer";
            PartitionKey = uniqueKey.Substring(0,2);
            RowKey = uniqueKey;
            CacheKey = "GP_" + uniqueKey;
        }        

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ICacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ICacheEntity ToCache(IBusinessObjectEntity item)
        {
            GamePlayer gp = item as GamePlayer;

            GamePlayerCacheable clone = new GamePlayerCacheable();
            clone.Coins = gp.Coins;
            clone.Coupons = gp.Coupons;
            clone.Experience = gp.Experience;
            clone.Level = gp.Level;
            clone.BuildableItems = gp.BuildableItems;
            clone.WorkItems = WorkItemProtoBuf.FromBusiness(gp.WorkItems);

            return clone;
        }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromCache(ICacheEntity entity)
        {
            GamePlayerCacheable item = entity as GamePlayerCacheable;

            GamePlayer clone = new GamePlayer();
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;
            clone.BuildableItems = item.BuildableItems;
            clone.WorkItems = WorkItemProtoBuf.ToBusiness(item.WorkItems);

            if (clone.WorkItems == null)
            {
                clone.WorkItems = new List<WorkItem>();
            }

            return clone;
        }

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity)
        {
            GamePlayerTableStorage other = entity as GamePlayerTableStorage;

            GamePlayer clone = new GamePlayer();
            clone.Coins = other.Coins;
            clone.Coupons = other.Coupons;
            clone.Experience = other.Experience;
            clone.Level = other.Level;

            using (MemoryStream memoryStream = new MemoryStream(other.BuildableItemsSerialized))
            {
                clone.BuildableItems = Serializer.Deserialize<Dictionary<BuildableItemEnum, int>>(memoryStream);
            }

            List<WorkItemProtoBuf> wis = null;
            using (MemoryStream memoryStream = new MemoryStream(other.WorkItemsSerialized))
            {
                wis = Serializer.Deserialize<List<WorkItemProtoBuf>>(memoryStream);
            }

            clone.WorkItems = WorkItemProtoBuf.ToBusiness(wis);

            if (clone.WorkItems == null)
            {
                clone.WorkItems = new List<WorkItem>();
            }
            
            return clone;
        }
   
        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            GamePlayer item = entity as GamePlayer;
            GamePlayerTableStorage clone = new GamePlayerTableStorage();
            
            clone.PartitionKey = PartitionKey;
            clone.RowKey = RowKey;

            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.BuildableItems);
                clone.BuildableItemsSerialized = memoryStream.ToArray();
            }

            List<WorkItemProtoBuf> wis = WorkItemProtoBuf.FromBusiness(item.WorkItems);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, wis);
                clone.WorkItemsSerialized = memoryStream.ToArray();
            }

            return clone;
        }
    }
}
