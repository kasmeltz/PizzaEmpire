namespace KS.PizzaEmpire.Common.BusinessObjects
{
    /// <summary>
    /// Represents a consumable item in the game
    /// </summary>
    public class ConsumableItem : BuildableItem
    {
         /// <summary>
        /// Creates a new instance of the ConsumableItem class.
        /// </summary>
        public ConsumableItem() { }

        /// <summary>
        /// The item that produces this item
        /// </summary>
        public BuildableItemEnum ProducedWith { get; set; }

        /// <summary>
        /// The item this item should be stored in
        /// </summary>
        public BuildableItemEnum StoredIn { get; set; }

    }
}