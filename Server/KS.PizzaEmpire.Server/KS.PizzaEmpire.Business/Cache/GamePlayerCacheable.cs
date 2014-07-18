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
    public class GamePlayerCacheable : ICacheEntity, IToLogicEntity, IToTableStorageEntity
    {       
        /// <summary>
        /// Creates a new instance of the GamePlayerCacheable class.
        /// </summary>
        public GamePlayerCacheable() { }

        /// <summary>
        /// A unique identifier for this Game Player across the application
        /// </summary>
        [ProtoMember(1)]
        public string UniqueKey { get; set; }
        
        #region ICacheEntity

        /// <summary>
        /// Returns a new instance of the appropriate ITableStorageEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ITableStorageEntity ToTableStorageEntity()
        {
            return GamePlayerTableStorage.From(this);
        }

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
        /// Generates a new GamePlayerCacheable instance from a GamePlayerTableStorage instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerCacheable From(GamePlayerTableStorage item)
        {
            GamePlayerCacheable clone = new GamePlayerCacheable();
            clone.UniqueKey = item.UniqueKey;

            return clone;
        }

        /// <summary>
        /// Generates a new GamePlayerCacheable instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerCacheable From(GamePlayer item)
        {
            GamePlayerCacheable clone = new GamePlayerCacheable();
            clone.UniqueKey = item.UniqueKey;

            return clone;
        }

        #endregion
    }
}
