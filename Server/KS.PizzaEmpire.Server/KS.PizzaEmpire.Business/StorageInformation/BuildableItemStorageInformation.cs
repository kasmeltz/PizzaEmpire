namespace KS.PizzaEmpire.Business.StorageInformation
{
    using AutoMapper;
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
            return Mapper
                .Map<BuildableItemTableStorage,BuildableItem>(
                    (BuildableItemTableStorage)entity);
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            BuildableItemTableStorage ts = Mapper
                .Map<BuildableItem, BuildableItemTableStorage>(
                    (BuildableItem)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
