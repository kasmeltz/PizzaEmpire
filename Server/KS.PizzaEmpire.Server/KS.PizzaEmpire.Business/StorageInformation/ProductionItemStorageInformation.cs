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
    /// ProductionItem entity in various types of storage.
    /// </summary>
    public class ProductionItemStorageInformation : BaseStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the ProductionItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public ProductionItemStorageInformation(string uniqueKey)
            : base(uniqueKey)
        {
            TableName = "ProductionItem";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "PI_" + uniqueKey;
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
                .Map<ProductionItemTableStorage, ProductionItem>(
                    (ProductionItemTableStorage)entity);
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            BuildableItemTableStorage ts = Mapper
                .Map<ProductionItem, ProductionItemTableStorage>(
                    (ProductionItem)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
