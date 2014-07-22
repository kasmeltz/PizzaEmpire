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

        /// <summary>
        /// The number of coins owned by the player
        /// </summary>
        public int Coins { get; set; }

        /// <summary>
        /// The number of coupons owned by the player
        /// </summary>
        public int Coupons { get; set; }

        /// <summary>
        /// The current experience of the player
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// The players current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The players inventory of items
        /// </summary>
        public Dictionary<BuildableItemEnum, int> BuildableItems { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        public List<WorkItem> WorkItems { get; set; }

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
        public static GamePlayer From(GamePlayerTableStorage other)
        {
            GamePlayer clone = new GamePlayer();
            clone.Coins = other.Coins;
            clone.Coupons = other.Coupons;
            clone.Experience = other.Experience;
            clone.Level = other.Level;

            using (MemoryStream memoryStream = new MemoryStream(other.BuildableItemsSerialized))
            {
                clone.BuildableItems = Serializer.Deserialize<Dictionary<BuildableItemEnum, int>>(memoryStream);
            }

            using (MemoryStream memoryStream = new MemoryStream(other.WorkItemsSerialized))
            {
                clone.WorkItems = Serializer.Deserialize<List<WorkItem>>(memoryStream);
            }

            if (clone.WorkItems == null)
            {
                clone.WorkItems = new List<WorkItem>();
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
            clone.WorkItems = item.WorkItems;

            if (clone.WorkItems == null)
            {
                clone.WorkItems = new List<WorkItem>();
            }

            return clone;
        }

        #endregion
    }
}
