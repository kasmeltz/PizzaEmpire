namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using ProtoBuf;

    [ProtoContract]
    public class StorageItemStatProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the StorageItemStatProtoSerializable class.
        /// </summary>
        public StorageItemStatProtoSerializable() { }

        /// <summary>
        /// The maximum number of items this item can produce at once
        /// </summary>
        [ProtoMember(1)]
        public int Capacity { get; set; }
    }
}