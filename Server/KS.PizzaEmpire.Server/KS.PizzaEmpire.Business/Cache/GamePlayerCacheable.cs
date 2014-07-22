namespace KS.PizzaEmpire.Business.Cache
{
    using Conversion;
    using Logic;
    using ProtoBuf;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the state for a player of the game as stored in the cache.
    /// </summary>
    [ProtoContract]
    [Serializable]
    public class GamePlayerCacheable : ICacheEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerCacheable class.
        /// </summary>
        public GamePlayerCacheable() { }

        /// <summary>
        /// The number of coins owned by the player
        /// </summary>
        [ProtoMember(1)]
        public int Coins { get; set; }

        /// <summary>
        /// The number of coupons owned by the player
        /// </summary>
        [ProtoMember(2)]
        public int Coupons { get; set; }

        /// <summary>
        /// The current experience of the player
        /// </summary>
        [ProtoMember(3)]
        public int Experience { get; set; }

        /// <summary>
        /// The players current level
        /// </summary>
        [ProtoMember(4)]
        public int Level { get; set; }

        /// <summary>
        /// The players inventory of items
        /// </summary>
        [ProtoMember(5)]
        public Dictionary<BuildableItemEnum, int> BuildableItems { get; set; }

        /// <summary>
        /// The work in progress for the player
        /// </summary>
        [ProtoMember(6)]
        public List<WorkItem> WorkItems { get; set; }
     
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
        /// Generates a new GamePlayerCacheable instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerCacheable From(GamePlayer item)
        {
            GamePlayerCacheable clone = new GamePlayerCacheable();
            clone.Coins = item.Coins;
            clone.Coupons = item.Coupons;
            clone.Experience = item.Experience;
            clone.Level = item.Level;
            clone.BuildableItems = item.BuildableItems;
            clone.WorkItems = item.WorkItems;

            return clone;
        }

        #endregion
    }
}
