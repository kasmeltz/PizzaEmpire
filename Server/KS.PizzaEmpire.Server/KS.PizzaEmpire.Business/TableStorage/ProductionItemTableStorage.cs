namespace KS.PizzaEmpire.Business.TableStorage
{
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Represents an item that is used to produce another item in the game 
    /// as stored in table storage.
    /// </summary>
    public class ProductionItemTableStorage : BuildableItemTableStorage
    {
        /// <summary>
        /// Creates a new instance of the ProductionItemTableStorage class.
        /// </summary>
        public ProductionItemTableStorage() { }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public byte[] ProductionStats { get; set; }
    }
}
