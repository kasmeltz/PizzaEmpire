using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.TableStorage;
using ProtoBuf;
using System;

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

            return clone;
        }

        #endregion
    }
}
