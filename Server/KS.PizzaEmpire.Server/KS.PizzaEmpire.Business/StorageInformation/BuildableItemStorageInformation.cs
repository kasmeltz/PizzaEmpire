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
            BuildableItemTableStorage buildableItemTableStorage = (BuildableItemTableStorage)entity;
            switch ((BuildableItemCategory)buildableItemTableStorage.Category)
            {
                case BuildableItemCategory.Production:
                    return Mapper
                        .Map<BuildableItemTableStorage, ProductionItem>(buildableItemTableStorage);
                case BuildableItemCategory.Consumable:
                    return Mapper
                        .Map<BuildableItemTableStorage, ConsumableItem>(buildableItemTableStorage);
                case BuildableItemCategory.Work:
                    return Mapper
                        .Map<BuildableItemTableStorage, WorkItem>(buildableItemTableStorage);
                case BuildableItemCategory.Storage:
                    return Mapper
                        .Map<BuildableItemTableStorage, StorageItem>(buildableItemTableStorage);
            }

            return null;
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            BuildableItemTableStorage ts = (BuildableItemTableStorage)Mapper
                .Map(entity, entity.GetType(), typeof(BuildableItemTableStorage));
            
            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
