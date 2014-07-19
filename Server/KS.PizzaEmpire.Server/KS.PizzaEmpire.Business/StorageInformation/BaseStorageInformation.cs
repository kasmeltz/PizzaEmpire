
namespace KS.PizzaEmpire.Business.StorageInformation
{
    /// <summary>
    /// The base implementation of an item that contains information
    /// about various storage settings
    /// </summary>
    public class BaseStorageInformation : IStorageInformation
    {
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
        public bool FromCache { get; set; }

        /// <summary>
        /// Whether this item was found in table storage
        /// </summary>
        public bool FromTableStorage { get; set; }
    }
}
