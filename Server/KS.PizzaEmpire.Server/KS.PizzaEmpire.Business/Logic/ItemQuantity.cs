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
    public class ItemQuantity : ILogicEntity, IToTableStorageEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItem class.
        /// </summary>
        public ItemQuantity() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }
        
        [ProtoMember(1)]
        public int ItemCode { get; set; }
        [ProtoMember(2)]
        public int Quantity { get; set; }

        #region IToTableStorageEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return ItemQuantityTableStorage.From(this);
        }

        #endregion

        /// <summary>
        /// Generates a new ItemQuantity instance from a ItemQuantityTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemQuantity From(ItemQuantityTableStorage item)
        {
            ItemQuantity clone = new ItemQuantity();

            clone.ItemCode = item.ItemCode;
            clone.Quantity = item.Quantity;

            return clone;
        }

    }
}
