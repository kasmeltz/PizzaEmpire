namespace KS.PizzaEmpire.Common.APITransfer
{
    using BusinessObjects;
    using System.Collections.Generic;

    public class BuildableItemAPI : IAPIEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemAPI class.
        /// </summary>
        public BuildableItemAPI() { }

        /// <summary>
        /// Identifies the type of item
        /// </summary>
        public BuildableItemEnum ItemCode { get; set; }
            
        /// <summary>
        /// The work item
        /// </summary>
        public WorkItem WorkItem { get; set; }

        /// <summary>
        /// The production item
        /// </summary>
        public ProductionItem ProductionItem { get; set; }

        /// <summary>
        /// The storage item
        /// </summary>
        public StorageItem StorageItem { get; set; }

        /// <summary>
        /// The consumable item
        /// </summary>
        public ConsumableItem ConsumableItem { get; set; }
    }
}
