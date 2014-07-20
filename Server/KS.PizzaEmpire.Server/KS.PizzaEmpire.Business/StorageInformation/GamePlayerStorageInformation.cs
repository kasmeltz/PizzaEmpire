
namespace KS.PizzaEmpire.Business.StorageInformation
{
    /// <summary>
    /// Represents in item that contains information about storing an 
    /// GamePlayer entity in various types of storage.
    /// </summary>
    public class GamePlayerStorageInformation : BaseStorageInformation
    {       
        /// <summary>
        /// Creates a new instance of the GamePlayerStorageInformation class with the
        /// provided Unique Key
        /// </summary>
        /// <param name="uniqueKey"></param>
        public GamePlayerStorageInformation(string uniqueKey) 
            : base(uniqueKey)
        {
            TableName = "GamePlayer";
            PartitionKey = uniqueKey.Substring(0,2);
            RowKey = uniqueKey;
            CacheKey = "GP_" + uniqueKey;
        }
    }
}
