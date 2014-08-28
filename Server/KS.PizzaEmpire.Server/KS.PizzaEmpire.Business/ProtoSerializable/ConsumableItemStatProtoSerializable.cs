namespace KS.PizzaEmpire.Business.ProtoSerializable
{
    using ProtoBuf;

    [ProtoContract]
    public class ConsumableItemStatProtoSerializable
    {
        /// <summary>
        /// Creates a new instance of the ConsumableItemStatProtoSerializable class.
        /// </summary>
        public ConsumableItemStatProtoSerializable() { }

        /// <summary>
        /// The number of items produced 
        /// </summary>
        [ProtoMember(1)]
        public int ProductionQuantity { get; set; }
    }
}