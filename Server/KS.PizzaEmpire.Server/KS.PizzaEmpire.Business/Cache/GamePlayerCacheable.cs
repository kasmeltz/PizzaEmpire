using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.TableStorage;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace KS.PizzaEmpire.Business.Cache
{
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

        [ProtoMember(1)]
        public int Coins { get; set; }
        [ProtoMember(2)]
        public int Coupons { get; set; }
        [ProtoMember(3)]
        public int Experience { get; set; }
        [ProtoMember(4)]
        public int Level { get; set; }
        [ProtoMember(5)]
        public Dictionary<int, bool> BuildableItems { get; set; }
        [ProtoMember(6)]
        public Dictionary<int, int> Equipment { get; set; }       

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
            clone.Equipment = item.Equipment;

            return clone;
        }

        #endregion
    }
}
