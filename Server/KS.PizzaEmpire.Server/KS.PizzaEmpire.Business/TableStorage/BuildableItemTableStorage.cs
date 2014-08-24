namespace KS.PizzaEmpire.Business.TableStorage
{
    using Common.BusinessObjects;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the data for a buildable item as stored in table storage.
    /// </summary>
    public class BuildableItemTableStorage : TableEntity, ITableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemTableStorage class.
        /// </summary>
        public BuildableItemTableStorage() { }

        /// <summary>
        /// Identifies the type of item
        /// </summary>
        public int ItemCode { get; set; }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public byte[] Stats { get; set; }
    }
}
