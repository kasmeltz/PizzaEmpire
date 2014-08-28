namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using KS.PizzaEmpire.Common.BusinessObjects;
    using ProtoBuf;
    using System.Collections.Generic;

    [ProtoContract]
    public class BuildableItemStatProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStatProtoSerializable class.
        /// </summary>
        public BuildableItemStatProtoSerializable() { }

        /// <summary>
        /// The level required to build this item
        /// </summary>
        [ProtoMember(1)]
        public int RequiredLevel { get; set; }

        /// <summary>
        /// The cost in coins to build this item
        /// </summary>
        [ProtoMember(2)]
        public int CoinCost { get; set; }
               
        /// <summary>
        /// The experience gained when this item is built
        /// </summary>
        [ProtoMember(3)]
        public int Experience { get; set; }

        /// <summary>
        /// The number of seconds required to build this item
        /// </summary>
        [ProtoMember(4)]
        public int BuildSeconds { get; set; }

        /// <summary>
        /// The number of coupons required to build this item
        /// </summary>
        [ProtoMember(5)]
        public int CouponCost { get; set; }

        /// <summary>
        /// The number of coupons required to speed up this item
        /// </summary>
        [ProtoMember(6)]
        public int SpeedUpCoupons { get; set; }

        /// <summary>
        /// The number of seconds this item will be sped up by specnding coupons
        /// </summary>
        [ProtoMember(7)]
        public int SpeedUpSeconds { get; set; }

        /// <summary>
        /// The required items to build this item
        /// </summary>
        [ProtoMember(8)]
        public List<ItemQuantityProtoSerializable> RequiredItems { get; set; }
    }
}