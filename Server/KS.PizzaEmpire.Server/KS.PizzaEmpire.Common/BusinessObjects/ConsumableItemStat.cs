namespace KS.PizzaEmpire.Common.BusinessObjects
{
    /// <summary>
    /// Represents the per level stats associated with a ConsumableItem
    /// </summary>
    public class ConsumableItemStat
    {
        /// <summary>
        /// Creates a new instance of the ConsumableItemStat class.
        /// </summary>
        public ConsumableItemStat() { }

        /// <summary>
        /// The number of items produced 
        /// </summary>
        public int ProductionQuantity { get; set; }
    }
}
