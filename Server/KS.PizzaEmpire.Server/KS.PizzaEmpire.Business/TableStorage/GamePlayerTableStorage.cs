namespace KS.PizzaEmpire.Business.TableStorage
{
    using Conversion;
    using Logic;
    using Microsoft.WindowsAzure.Storage.Table;
    using ProtoBuf;
    using System.IO;

    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class GamePlayerTableStorage : TableEntity, ITableStorageEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerTableStorage class.
        /// </summary>
        public GamePlayerTableStorage() { }

        public int Coins { get; set; }
        public int Coupons { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public byte[] BuildableItemsSerialized { get; set; }
        public byte[] EquipmentSerialized { get; set; }
        public byte[] DelayedItemsSerialized { get; set; }

        #region IToLogicEntity
       
        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return GamePlayer.From(this);
        }

        #endregion

        #region Cloners
       
        /// <summary>
        /// Generates a new GamePlayerTableStorage instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerTableStorage From(GamePlayer item)
        {
            GamePlayerTableStorage clone = new GamePlayerTableStorage();
            clone.PartitionKey = item.StorageInformation.PartitionKey;
            clone.RowKey = item.StorageInformation.RowKey;
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.BuildableItems);
                clone.BuildableItemsSerialized = memoryStream.ToArray();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.Equipment);
                clone.EquipmentSerialized = memoryStream.ToArray();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, item.DelayedItems);
                clone.DelayedItemsSerialized = memoryStream.ToArray();
            }

            return clone;
        }

        #endregion
    }
}
