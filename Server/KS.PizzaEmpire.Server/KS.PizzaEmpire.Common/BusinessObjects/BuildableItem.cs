namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that can be built in the game
    /// </summary>
    public class BuildableItem : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItem class.
        /// </summary>
        public BuildableItem() { }

        /// <summary>
        /// Identifies the type of item
        /// </summary>
        public BuildableItemEnum ItemCode { get; set; }
            
        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public List<BuildableItemStat> Stats { get; set; }
    }
}