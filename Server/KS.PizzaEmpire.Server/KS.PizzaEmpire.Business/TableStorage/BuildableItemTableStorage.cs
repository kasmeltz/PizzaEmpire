using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business.TableStorage
{
    /// <summary>
    /// Represents the data for a buildable item as stored in table storage.
    /// </summary>
    public class BuildableItemTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the BuildableItemTableStorage class.
        /// </summary>
        public BuildableItemTableStorage() { }
        
        public int ItemCode { get; set; }
        public string Name { get; set; }
        public int CoinCost { get; set; }
        public int Quantity { get; set; }
        public int Experience { get; set; }
        public int BuildSeconds { get; set; }
        public int CouponCost { get; set; }
        public int SpeedUpCoupons { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return BuildableItem.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new BuildableItemTableStorage instance from a BuildableItem instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BuildableItemTableStorage From(BuildableItem item)
        {
            BuildableItemTableStorage clone = new BuildableItemTableStorage();

            clone.PartitionKey = item.StorageInformation.PartitionKey;
            clone.RowKey = item.StorageInformation.RowKey;

            clone.ItemCode = item.ItemCode;
            clone.Name = item.Name;
            clone.CoinCost = item.CoinCost;
            clone.Quantity = item.Quantity;
            clone.Experience = item.Experience;
            clone.BuildSeconds = item.BuildSeconds;
            clone.CouponCost = item.CouponCost;
            clone.SpeedUpCoupons = item.SpeedUpCoupons;

            return clone;
        }

        #endregion
    }
}
