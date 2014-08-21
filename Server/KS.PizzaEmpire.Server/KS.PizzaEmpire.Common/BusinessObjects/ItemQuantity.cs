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
        /// The stored quantity of items
        /// </summary>
        public int StoredQuantity { get; set; }

        /// <summary>
        /// The unstored quantity of items
        /// </summary>
        public int UnStoredQuantity { get; set; }

        /// <summary>
        /// The level of the item
        /// </summary>
        public int Level { get; set; }        
    }
}
