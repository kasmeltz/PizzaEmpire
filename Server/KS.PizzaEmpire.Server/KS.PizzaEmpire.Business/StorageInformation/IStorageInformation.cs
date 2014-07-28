using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Common.BusinessObjects;
namespace KS.PizzaEmpire.Business.StorageInformation
{
    /// <summary>
    /// Represents in item that contains information about storing an 
    /// entity in various types of storage.
    /// </summary>
    public interface IStorageInformation
    {
        /// <summary>
        /// A unique key for this item
        /// </summary>
        string UniqueKey { get; set; }

        /// <summary>
        /// A table name for this item
        /// </summary>
        string TableName { get; set;  }

        /// <summary>
        /// A partition key for this item
        /// </summary>
        string PartitionKey { get; set; }

        /// <summary>
        /// A row key for this item
        /// </summary>
        string RowKey { get; set; }

        /// <summary>
        /// A cache key for this item
        /// </summary>
        string CacheKey { get; set;  }

        /// <summary>
        /// Whether this item was found in the cache
        /// </summary>
        bool FoundInCache { get; set; }

        /// <summary>
        /// Whether this item was found in table storage
        /// </summary>
        bool FoundInTableStorage { get; set; }

        /// <summary>
        /// Translates an ICacheEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IBusinessObjectEntity FromCache(ICacheEntity entity);

        /// <summary>
        /// Translates an ITableStorageEntity to an IBusinessObjectEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IBusinessObjectEntity FromTableStorage(ITableStorageEntity entity);

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ICacheEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ICacheEntity ToCache(IBusinessObjectEntity entity);

        /// <summary>
        /// Translates an IBusinessObjectEntity to an ITableStorageEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ITableStorageEntity ToTableStorage(IBusinessObjectEntity entity);
    }
}
