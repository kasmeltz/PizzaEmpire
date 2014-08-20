namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents item information for one item for one gameplayer
    /// </summary>
    public class GamePlayerItem
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerItem class
        /// </summary>
        public GamePlayerItem() { }

        /// <summary>
        /// The GamePlayer who owns the BuildableItem
        /// </summary>
        public GamePlayer Player { get; set; }

        /// <summary>
        /// The BuildableItem owned by the GamePlayer
        /// </summary>
        public BuildableItem Item { get; set; }

        /// <summary>
        /// The level of the item
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The stored quantity of the item
        /// </summary>
        public int StoredQuantity { get; set; }

        /// <summary>
        /// The unstored quantity of the item
        /// </summary>
        public int UnstoredQuantity { get; set; }
    }
}
