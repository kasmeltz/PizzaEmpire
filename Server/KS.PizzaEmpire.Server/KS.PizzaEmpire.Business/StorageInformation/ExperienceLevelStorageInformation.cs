namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Common;
    using KS.PizzaEmpire.Business.Cache;
    using KS.PizzaEmpire.Business.TableStorage;
    using KS.PizzaEmpire.Common.BusinessObjects;
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
            ExperienceLevelTableStorage other = entity as ExperienceLevelTableStorage;

            ExperienceLevel clone = new ExperienceLevel();
            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity)
        {
            ExperienceLevel other = entity as ExperienceLevel;

            ExperienceLevelTableStorage clone = new ExperienceLevelTableStorage();
            clone.PartitionKey = PartitionKey;
            clone.RowKey = RowKey;

            clone.Level = other.Level;
            clone.ExperienceRequired = other.ExperienceRequired;

            return clone;
        }
    }
}
