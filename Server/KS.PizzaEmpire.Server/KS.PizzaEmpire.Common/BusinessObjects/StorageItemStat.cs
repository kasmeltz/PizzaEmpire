namespace KS.PizzaEmpire.Common.BusinessObjects
{
    /// <summary>
    /// Represents the per level stats associated with a StorageItem
    /// </summary>
    public class StorageItemStat
    {
        /// <summary>
        /// Creates a new instance of the StorageItemStat class.
        /// </summary>
        public StorageItemStat() { }

        /// <summary>
        /// The maximum number of items this item can store at once
        /// </summary>
        public int Capacity { get; set; }       
    }
}
