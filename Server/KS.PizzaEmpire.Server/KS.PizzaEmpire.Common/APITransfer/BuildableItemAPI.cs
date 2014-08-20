namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    public class BuildableItemAPI : IAPIEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public BuildableItemAPI() { }

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
