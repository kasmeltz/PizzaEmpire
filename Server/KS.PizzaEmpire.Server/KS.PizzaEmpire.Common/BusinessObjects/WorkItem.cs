namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System;

    /// <summary>
    /// Represents ongoing work that will produce some finished item(s) after some length of time
    /// as used by the game logic.
    /// </summary>
    public class WorkItem : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the WorkItem class.
        /// </summary>
        public WorkItem() { }

        /// <summary>
        /// The item code that represents the item we are working on
        /// </summary>
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// The time when this work will be complete in UTC format
        /// </summary>
        public DateTime FinishTime { get; set; }
    }
}
