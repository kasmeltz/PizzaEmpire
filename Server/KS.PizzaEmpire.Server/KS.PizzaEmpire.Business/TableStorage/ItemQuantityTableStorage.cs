using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business.TableStorage
{
    /// <summary>
    /// Represents the data for an item quantity+- as stored in table storage.
    /// </summary>
    public class ItemQuantityTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the ItemQuantityTableStorage class.
        /// </summary>
        public ItemQuantityTableStorage() { }

        public int ItemCode { get; set; }
        public int Quantity { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return ItemQuantity.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new ItemQuantityTableStorage instance from a ItemQuantity instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemQuantityTableStorage From(ItemQuantity item)
        {
            ItemQuantityTableStorage clone = new ItemQuantityTableStorage();

            clone.ItemCode = item.ItemCode;
            clone.Quantity = item.Quantity;

            return clone;
        }

        #endregion
    }
}
