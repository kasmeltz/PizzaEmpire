using KS.PizzaEmpire.Business.Common;

namespace KS.PizzaEmpire.Business.StorageInformation
{
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
    }
}
