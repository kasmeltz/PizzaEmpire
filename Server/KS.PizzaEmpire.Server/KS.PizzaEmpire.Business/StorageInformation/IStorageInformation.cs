
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
        bool FromCache { get; set; }

        /// <summary>
        /// Whether this item was found in table storage
        /// </summary>
        bool FromTableStorage { get; set; }
    }
}
