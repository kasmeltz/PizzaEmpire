namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Common;

    /// <summary>
    /// Represents in item that contains information about storing an 
    /// Recipe entity in various types of storage.
    /// </summary>
    public class RecipeStorageInformation : BaseStorageInformation
    {       
        /// <summary>
        /// Creates a new instance of the RecipeStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public RecipeStorageInformation(string uniqueKey) 
            : base(uniqueKey)
        {
            TableName = "Recipe";
            PartitionKey = "Version" + Constants.APPLICATION_VERSION;
            RowKey = uniqueKey;
            CacheKey = "RE_" + uniqueKey;
        }
    }
}
