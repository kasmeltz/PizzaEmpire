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
    /// WorkItem entity in various types of storage.
    /// </summary>
    public class WorkItemStorageInformation : BaseStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the WorkItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public WorkItemStorageInformation(string uniqueKey)
            : base(uniqueKey)
        {
            TableName = "WorkItem";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "WI_" + uniqueKey;
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
                .Map<WorkItemTableStorage, WorkItem>(
                    (WorkItemTableStorage)entity);
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            BuildableItemTableStorage ts = Mapper
                .Map<WorkItem, WorkItemTableStorage>(
                    (WorkItem)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
