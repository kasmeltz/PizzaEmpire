namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using Common.BusinessObjects;
    using ProtoBuf;
    using System.Collections.Generic;

    [ProtoContract]
    public class LocationStorageProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the LocationStorageProtoSerializable class.
        /// </summary>
        public LocationStorageProtoSerializable() { }

        /// <summary>
        /// The items in this storage
        /// </summary>
        [ProtoMember(1)]
        public Dictionary<BuildableItemEnum, ItemQuantityProtoSerializable> Items { get; set; }
    }
}