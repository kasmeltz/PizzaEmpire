namespace KS.PizzaEmpire.Business.Logic
{
    using Cache;
    using Conversion;
    using ProtoBuf;
    using StorageInformation;
    using System.Collections.Generic;
    using System.IO;
    using TableStorage;

    /// <summary>
    /// Represents the state for a player of the game as used in the game logic.
    /// </summary>
    public class GamePlayer : ILogicEntity, IToTableStorageEntity, IToCacheEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayer class.
        /// </summary>
        public GamePlayer() { }

        /// <summary>
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }

        public int Coins { get; set; }
        public int Coupons { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public Dictionary<int, int> BuildableItems { get; set; }
        public Dictionary<int, int> Equipment { get; set; }
        public List<DelayedItem> DelayedItems { get; set; }

        #region IToCacheEntity

        /// <summary>
        /// Returns a new instance of the appropriate ICacheEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ICacheEntity ToCacheEntity()
        {
            return GamePlayerCacheable.From(this);
        }

        #endregion

        #region IToTableStorageEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return GamePlayerTableStorage.From(this);
        }

        #endregion

        #region Cloners

        /// <summary>
        /// Generates a new GamePlayer instance from a GamePlayerTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayer From(GamePlayerTableStorage item)
        {
            GamePlayer clone = new GamePlayer();
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;

            using (MemoryStream memoryStream = new MemoryStream(item.BuildableItemsSerialized))
            {
                clone.BuildableItems = Serializer.Deserialize<Dictionary<int, int>>(memoryStream);
            }

            using (MemoryStream memoryStream = new MemoryStream(item.EquipmentSerialized))
            {
                clone.Equipment = Serializer.Deserialize<Dictionary<int, int>>(memoryStream);
            }

            using (MemoryStream memoryStream = new MemoryStream(item.DelayedItemsSerialized))
            {
                clone.DelayedItems = Serializer.Deserialize<List<DelayedItem>>(memoryStream);
            }

            if (clone.DelayedItems == null)
            {
                clone.DelayedItems = new List<DelayedItem>();
            }

            return clone;
        }

        /// <summary>
        /// Generates a new GamePlayer instance from a GamePlayerCacheable instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayer From(GamePlayerCacheable item)
        {
            GamePlayer clone = new GamePlayer();
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;
            clone.BuildableItems = item.BuildableItems;
            clone.Equipment = item.Equipment;
            clone.DelayedItems = item.DelayedItems;

            if (clone.DelayedItems == null)
            {
                clone.DelayedItems = new List<DelayedItem>();
            }

            return clone;
        }

        #endregion
    }
}
