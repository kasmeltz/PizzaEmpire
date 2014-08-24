namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using KS.PizzaEmpire.Common.BusinessObjects;
    using ProtoBuf;
    using System.Collections.Generic;

    [ProtoContract]
    public class BuildableItemStatProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStatProtoBuf class.
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<BuildableItemStatProtoSerializable> FromBusiness(List<BuildableItemStat> items)
        {
            if (items == null)
            { 
                return null;
            }   

            List<BuildableItemStatProtoSerializable> bisps = new List<BuildableItemStatProtoSerializable>();
            foreach (BuildableItemStat bis in items)
            {
                BuildableItemStatProtoSerializable clone = new BuildableItemStatProtoSerializable();
                clone.RequiredLevel = bis.RequiredLevel;
                clone.CoinCost = bis.CoinCost;
                clone.CouponCost = bis.CouponCost;
                clone.Experience = bis.Experience;
                clone.BuildSeconds = bis.BuildSeconds;
                clone.CouponCost = bis.CouponCost;
                clone.SpeedUpCoupons = bis.SpeedUpCoupons;
                clone.SpeedUpSeconds = bis.SpeedUpSeconds;
                clone.RequiredItems = ItemQuantityProtoSerializable.FromBusiness(bis.RequiredItems);
                bisps.Add(clone);
            }
            
            return bisps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<BuildableItemStat> ToBusiness(List<BuildableItemStatProtoSerializable> items)
        {
            if (items == null)
            {
                return null;
            }

            List<BuildableItemStat> bis = new List<BuildableItemStat>();
            foreach (BuildableItemStatProtoSerializable bisps in items)
            {
                BuildableItemStat clone = new BuildableItemStat();
                clone.RequiredLevel = bisps.RequiredLevel;
                clone.CoinCost = bisps.CoinCost;
                clone.CouponCost = bisps.CouponCost;
                clone.Experience = bisps.Experience;
                clone.BuildSeconds = bisps.BuildSeconds;
                clone.CouponCost = bisps.CouponCost;
                clone.SpeedUpCoupons = bisps.SpeedUpCoupons;
                clone.SpeedUpSeconds = bisps.SpeedUpSeconds;
                clone.RequiredItems = ItemQuantityProtoSerializable.ToBusiness(bisps.RequiredItems);
                bis.Add(clone);
            }

            return bis;
        }
    }
}