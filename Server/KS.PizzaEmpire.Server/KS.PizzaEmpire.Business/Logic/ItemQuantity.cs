namespace KS.PizzaEmpire.Business.Logic
{
    using Conversion;
    using StorageInformation;
    using TableStorage;
    using ProtoBuf;

    /// <summary>
    /// Represents an item quanity as used by the game logic
    /// </summary>
    [ProtoContract]
    public class ItemQuantity
    {
        /// <summary>
        /// Creates a new instance of the ItemQuantity class.
        /// </summary>
        public ItemQuantity() { }

        [ProtoMember(1)]
        public int ItemCode { get; set; }
        [ProtoMember(2)]
        public int Quantity { get; set; }
    }
}
