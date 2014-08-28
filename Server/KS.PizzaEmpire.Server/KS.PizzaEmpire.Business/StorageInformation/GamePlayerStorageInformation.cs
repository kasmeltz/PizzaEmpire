namespace KS.PizzaEmpire.Business.StorageInformation
{
    using AutoMapper;
    using Cache;
    using Common.BusinessObjects;
    using System;
    using TableStorage;
    
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
        public override ICacheEntity ToCache(IBusinessObjectEntity entity)
        {
            return Mapper
                .Map<GamePlayer, GamePlayerCacheable>(
                    (GamePlayer)entity);
        }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromCache(ICacheEntity entity)
        {
            return Mapper
                .Map<GamePlayerCacheable, GamePlayer>(
                    (GamePlayerCacheable)entity);
        }

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity)
        {
            return Mapper
                .Map<GamePlayerTableStorage, GamePlayer>(
                    (GamePlayerTableStorage)entity);
        }
   
        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            GamePlayerTableStorage ts = Mapper
                .Map<GamePlayer, GamePlayerTableStorage>(
                    (GamePlayer)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
