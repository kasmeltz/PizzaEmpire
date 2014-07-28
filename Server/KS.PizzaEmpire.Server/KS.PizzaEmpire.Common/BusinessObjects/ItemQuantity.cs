namespace KS.PizzaEmpire.Common.BusinessObjects
{
    /// <summary>
    /// Represents an item quanity as used by the game logic
    /// </summary>
    public class ItemQuantity : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the ItemQuantity class.
        /// </summary>
        public ItemQuantity() { }
        
        /// <summary>
        /// The type of item this quanity is for
        /// </summary>
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// The quantity of items
        /// </summary>
        public int Quantity { get; set; }
    }
}
