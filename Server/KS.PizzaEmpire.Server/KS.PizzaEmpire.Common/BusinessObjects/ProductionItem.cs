namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that is used to produce another item in the game
    /// </summary>
    public class ProductionItem : BuildableItem
    {
        /// <summary>
        /// Creates a new instance of the ProductionItem class.
        /// </summary>
        public ProductionItem() { }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public List<ProductionItemStat> ProductionStats { get; set; }
    }
}
