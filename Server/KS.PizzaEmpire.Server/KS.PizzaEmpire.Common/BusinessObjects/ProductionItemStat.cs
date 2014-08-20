namespace KS.PizzaEmpire.Common.BusinessObjects
{
    /// <summary>
    /// Represents the per level stats associated with a ProductionItem
    /// </summary>
    public class ProductionItemStat
    {
        /// <summary>
        /// Creates a new instance of the ProductionItemStat class.
        /// </summary>
        public ProductionItemStat() { }

        /// <summary>
        /// The maximum number of items this item can produce at once
        /// </summary>
        public int Capacity { get; set; }
    }
}
