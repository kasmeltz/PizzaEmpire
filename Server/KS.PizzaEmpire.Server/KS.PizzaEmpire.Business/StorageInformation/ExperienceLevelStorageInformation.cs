namespace KS.PizzaEmpire.Business.StorageInformation
{
    using Common;

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
    }
}
