namespace KS.PizzaEmpire.Common.BusinessObjects
{
    using System;

    /// <summary>
    /// Represents ongoing work that will produce some finished item(s) after some length of time
    /// as used by the game logic.
    /// </summary>
    public class WorkInProgress : IBusinessObjectEntity
    {
        /// <summary>
        /// Creates a new instance of the WorkInProgress class.
        /// </summary>
        public WorkInProgress() { }

        /// <summary>
        /// The details of the work that is in progress
        /// </summary>
        public ItemQuantity Quantity { get; set; }

        /// <summary>
        /// The location that the work is being done for
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// The time when this work will be complete in UTC format
        /// </summary>
        public DateTime FinishTime { get; set; }
    }
}
