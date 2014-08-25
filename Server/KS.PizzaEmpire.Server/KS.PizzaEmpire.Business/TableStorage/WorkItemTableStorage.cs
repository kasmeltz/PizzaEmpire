namespace KS.PizzaEmpire.Business.TableStorage
{
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Represents an item that is work
    /// as stored in table storage.
    /// </summary>
    public class WorkItemTableStorage : BuildableItemTableStorage
    {
        /// <summary>
        /// Creates a new instance of the WorkItemTableStorage class.
        /// </summary>
        public WorkItemTableStorage() { }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public byte[] WorkStats { get; set; }
    }
}
