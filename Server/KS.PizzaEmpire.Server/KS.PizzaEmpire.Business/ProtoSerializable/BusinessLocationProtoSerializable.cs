namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System.Collections.Generic;

    [ProtoContract]
    public class BusinessLocationProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the BusinessLocationProtoSerializable class.
        /// </summary>
        public BusinessLocationProtoSerializable() { }

        /// <summary>
        /// The items at this location
        /// </summary>
        [ProtoMember(1)]
        public LocationStorageProtoSerializable Storage { get; set; }
    }
}