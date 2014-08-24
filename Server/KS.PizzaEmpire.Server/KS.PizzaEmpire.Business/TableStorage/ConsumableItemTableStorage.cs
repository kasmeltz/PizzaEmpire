namespace KS.PizzaEmpire.Business.TableStorage
{
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Represents an item that is consumable 
    /// as stored in table storage.
    /// </summary>
    public class ConsumableItemTableStorage : BuildableItemTableStorage
    {
        /// <summary>
        /// Creates a new instance of the ConsumableItemTableStorage class.
        /// </summary>
        public ConsumableItemTableStorage() { }

        /// <summary>
        /// The item that produces this item
        /// </summary>
        public int ProducedWith { get; set; }

        /// <summary>
        /// The item this item should be stored in
        /// </summary>
        public int StoredIn { get; set; }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public byte[] ConsumableStats { get; set; }
    }
}
