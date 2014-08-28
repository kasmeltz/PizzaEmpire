namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [ProtoContract]
    public class WorkInProgressProtoSerializable
    {
         /// <summary>
        /// Creates a new instance of the WorkInProgressProtoSerializable class.
        /// </summary>
        public WorkInProgressProtoSerializable() { }

        /// <summary>
        /// The details of the work that is in progress
        /// </summary>
        [ProtoMember(1)]
        public ItemQuantity Quantity { get; set; }

        /// <summary>
        /// The location that the work is being done for
        /// </summary>
        [ProtoMember(2)]
        public int Location { get; set; }

        /// <summary>
        /// The time when this work will be complete in UTC format
        /// </summary>
        [ProtoMember(3)] 
        public DateTime FinishTime { get; set; }
    }
}
