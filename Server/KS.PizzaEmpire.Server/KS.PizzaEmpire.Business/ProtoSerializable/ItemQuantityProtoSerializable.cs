namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [ProtoContract]
    public class ItemQuantityProtoSerializable
    {
         /// <summary>
        /// Creates a new instance of the ItemQuantityProtoBuf class.
        /// </summary>
        public ItemQuantityProtoSerializable() { }

        /// <summary>
        /// The type of item this quanity is for
        /// </summary>
        [ProtoMember(1)]
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// The stored quantity of items
        /// </summary>
        [ProtoMember(2)]
        public int StoredQuantity { get; set; }

        /// <summary>
        /// The unstored quantity of items
        /// </summary>
        [ProtoMember(3)]
        public int UnStoredQuantity { get; set; }

        /// <summary>
        /// The level of the item
        /// </summary>
        [ProtoMember(4)]
        public int Level { get; set; }
    }
}
