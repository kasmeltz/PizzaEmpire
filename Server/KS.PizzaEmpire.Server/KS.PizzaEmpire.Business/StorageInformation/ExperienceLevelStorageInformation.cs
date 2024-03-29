﻿namespace KS.PizzaEmpire.Business.StorageInformation
{
    using AutoMapper;
    using Business.Cache;
    using Business.TableStorage;
    using Common;
    using Common.BusinessObjects;
    using System;

    /// <summary>
    /// Represents in item that contains information about storing an 
    /// ExperienceLevel entity in various types of storage.
    /// </summary>
    public class ExperienceLevelStorageInformation : BaseStorageInformation
    {       
        /// <summary>
        /// Creates a new instance of the ExperienceLevelStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public ExperienceLevelStorageInformation(string uniqueKey) 
            : base(uniqueKey)
        {
            TableName = "ExperienceLevel";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "EL_" + uniqueKey;
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
                .Map<ExperienceLevelTableStorage, ExperienceLevel>(
                    (ExperienceLevelTableStorage)entity);
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            ExperienceLevelTableStorage ts = Mapper
               .Map<ExperienceLevel, ExperienceLevelTableStorage>(
                   (ExperienceLevel)entity);

            ts.PartitionKey = PartitionKey;
            ts.RowKey = RowKey;

            return ts;
        }
    }
}
