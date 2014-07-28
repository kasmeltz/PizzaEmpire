using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Common.BusinessObjects;
namespace KS.PizzaEmpire.Business.StorageInformation
{
    /// <summary>
    /// The base implementation of an item that contains information
    /// about various storage settings
    /// </summary>
    public abstract class BaseStorageInformation : IStorageInformation
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public BaseStorageInformation(string uniqueKey)
        {
            UniqueKey = uniqueKey;
        }

        /// <summary>
        /// A unique key for this item
        /// </summary>
        public string UniqueKey { get; set; }

        /// <summary>
        /// A table name for this item
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// A partition key for this item
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// A row key for this item
        /// </summary>
        public string RowKey { get; set; }

        /// <summary>
        /// A cache key for this item
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// Whether this item was found in the cache
        /// </summary>
        public bool FoundInCache { get; set; }

        /// <summary>
        /// Whether this item was found in table storage
        /// </summary>
        public bool FoundInTableStorage { get; set; }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract IBusinessObjectEntity FromCache(ICacheEntity entity);

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity);

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ICacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract ICacheEntity ToCache(IBusinessObjectEntity entity);

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity);
    }
}
