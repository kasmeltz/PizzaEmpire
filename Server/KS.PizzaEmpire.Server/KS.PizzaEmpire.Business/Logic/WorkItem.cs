namespace KS.PizzaEmpire.Business.Logic
{
    using ProtoBuf;
    using System;

    /// <summary>
    /// Represents ongoing work that will produce some finished item(s) after some length of time
    /// as used by the game logic.
    /// </summary>
    [ProtoContract]
    public class WorkItem
    {
        /// <summary>
        /// Creates a new instance of the WorkItem class.
        /// </summary>
        public WorkItem() { }

        [ProtoMember(1)]
        public BuildableItemEnum ItemCode { get; set; }
        [ProtoMember(3)]
        public DateTime FinishTime { get; set; }
    }
}
