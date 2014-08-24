namespace KS.PizzaEmpire.Business.StorageInformation
{
    using AutoMapper;
    using Cache;
    using Common;
    using Common.BusinessObjects;
    using System;
    using TableStorage;

    /// <summary>
    /// Represents in item that contains information about storing a
    /// ProductionItem entity in various types of storage.
    /// </summary>
    public class ConsumableItemStorageInformation : BaseStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the ProductionItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public ConsumableItemStorageInformation(string uniqueKey)
            : base(uniqueKey)
        {
            TableName = "ConsumableItem";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "CI_" + uniqueKey;
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
            return Mapper
                .Map<ConsumableItemTableStorage, ConsumableItem>(
                    (ConsumableItemTableStorage)entity);
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            ConsumableItemTableStorage ts = Mapper
                .Map<ConsumableItem, ConsumableItemTableStorage>(
                    (ConsumableItem)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
