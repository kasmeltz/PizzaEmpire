using ProtoBuf;
using System;

namespace KS.PizzaEmpire.Business.Logic
{
    /// <summary>
    /// Represents an item that is added to the player after some length of time
    /// as used by the game logic.
    /// </summary>
    [ProtoContract]
    public class DelayedItem
    {
         /// <summary>
        /// Creates a new instance of the DelayedItem class.
        /// </summary>
        public DelayedItem() { }

        [ProtoMember(1)]
        public int ItemCode { get; set; }
        [ProtoMember(2)]
        public int EquipmentCode { get; set; }
        [ProtoMember(3)]
        public DateTime FinishTime { get; set; }
    }
}
