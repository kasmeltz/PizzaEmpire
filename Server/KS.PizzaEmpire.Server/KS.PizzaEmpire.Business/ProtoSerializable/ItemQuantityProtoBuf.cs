namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [ProtoContract]
    public class ItemQuantityProtoBuf
    {
         /// <summary>
        /// Creates a new instance of the ItemQuantityProtoBuf class.
        /// </summary>
        public ItemQuantityProtoBuf() { }

        /// <summary>
        /// The type of item this quanity is for
        /// </summary>
        [ProtoMember(1)]
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// The quantity of items
        /// </summary>
        [ProtoMember(2)]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<ItemQuantityProtoBuf> FromBusiness(List<ItemQuantity> items)
        {
            throw new NotImplementedException();


            /*
            if (items == null)
            { 
                return null;
            }   

            List<ItemQuantityProtoBuf> wis = new List<ItemQuantityProtoBuf>();
            foreach (ItemQuantity wi in items)
            {
                ItemQuantityProtoBuf clone = new ItemQuantityProtoBuf();
                clone.ItemCode = wi.ItemCode;
                clone.Quantity = wi.Quantity;
                wis.Add(clone);
            }
            return wis;
             */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<ItemQuantity> ToBusiness(List<ItemQuantityProtoBuf> items)
        {
            throw new NotImplementedException();

            /*
            if (items == null)
            {
                return null;
            } 

            List<ItemQuantity> wis = new List<ItemQuantity>();
            foreach (ItemQuantityProtoBuf wi in items)
            {
                ItemQuantity clone = new ItemQuantity();
                clone.ItemCode = wi.ItemCode;
                clone.Quantity = wi.Quantity;
                wis.Add(clone);
            }
            return wis;
             * */
        }
    }
}
