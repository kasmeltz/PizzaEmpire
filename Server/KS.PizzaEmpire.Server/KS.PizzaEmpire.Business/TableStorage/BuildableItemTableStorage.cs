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

        /// <summary>
        /// The per level production stats associated with this item
        /// </summary>
        public byte[] ProductionStats { get; set; }

        /// <summary>
        /// The per level storage stats associated with this item
        /// </summary>
        public byte[] StorageStats { get; set; }

        /// <summary>
        /// The per level consumable stats associated with this item
        /// </summary>
        public byte[] ConsumableStats { get; set; }

        /// <summary>
        /// The per level work stats associated with this item
        /// </summary>
        public byte[] WorkStats { get; set; }

        /// <summary>
        /// The category of buildable item
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// The item that produces this item
        /// </summary>
        public int ProducedWith { get; set; }

        /// <summary>
        /// The item this item should be stored in
        /// </summary>
        public int StoredIn { get; set; }
    }
}
