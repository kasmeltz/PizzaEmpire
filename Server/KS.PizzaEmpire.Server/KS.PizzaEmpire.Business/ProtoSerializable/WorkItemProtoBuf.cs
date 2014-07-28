namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    [ProtoContract]
    public class WorkItemProtoBuf
    {
         /// <summary>
        /// Creates a new instance of the WorkItemProtoBuf class.
        /// </summary>
        public WorkItemProtoBuf() { }

        /// <summary>
        /// The item code that represents the item we are working on
        /// </summary>
        [ProtoMember(1)]
        public BuildableItemEnum ItemCode { get; set; }

        /// <summary>
        /// The time when this work will be complete in UTC format
        /// </summary>
        [ProtoMember(2)]
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<WorkItemProtoBuf> FromBusiness(List<WorkItem> items)
        {
            if (items == null)
            {
                return null;
            }   

            List<WorkItemProtoBuf> wis = new List<WorkItemProtoBuf>();
            foreach (WorkItem wi in items)
            {
                WorkItemProtoBuf clone = new WorkItemProtoBuf();
                clone.ItemCode = wi.ItemCode;
                clone.FinishTime = wi.FinishTime;
                wis.Add(clone);
            }
            return wis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<WorkItem> ToBusiness(List<WorkItemProtoBuf> items)
        {
            if (items == null)
            {
                return null;
            }   

            List<WorkItem> wis = new List<WorkItem>();
            foreach (WorkItemProtoBuf wi in items)
            {
                WorkItem clone = new WorkItem();
                clone.ItemCode = wi.ItemCode;
                clone.FinishTime = wi.FinishTime;
                wis.Add(clone);
            }
            return wis;
        }
    }
}
