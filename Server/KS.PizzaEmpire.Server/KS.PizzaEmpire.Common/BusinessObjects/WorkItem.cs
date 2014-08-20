namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item that is general work
    /// </summary>
    public class WorkItem : BuildableItem
    {
        /// <summary>
        /// Creates a new instance of the WorkItem class.
        /// </summary>
        public WorkItem() { }

        /// <summary>
        /// The per level stats associated with this item
        /// </summary>
        public List<WorkItemStat> WorkStats { get; set; }
    }
}