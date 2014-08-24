namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using ProtoBuf;

    [ProtoContract]
    public class ProductionItemStatProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemStatProtoBuf class.
        /// </summary>
        public ProductionItemStatProtoSerializable() { }

        /// <summary>
        /// The maximum number of items this item can produce at once
        /// </summary>
        [ProtoMember(1)]
        public int Capacity { get; set; }
    }
}