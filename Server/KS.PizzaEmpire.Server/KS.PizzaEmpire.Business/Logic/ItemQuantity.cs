using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using ProtoBuf;

namespace KS.PizzaEmpire.Business.Logic
{
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
