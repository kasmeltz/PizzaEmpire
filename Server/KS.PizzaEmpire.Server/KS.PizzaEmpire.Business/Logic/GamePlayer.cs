using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.TableStorage;

namespace KS.PizzaEmpire.Business.Logic
{
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
        /// A unique identifier for this Game Player across the application
        /// </summary>
        public string UniqueKey { get; set; }

        #region ILogicEntity

        /// <summary>
        /// Returns a new instance of the appropriate ICacheEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ICacheEntity ToCacheEntity()
        {
            return GamePlayerCacheable.From(this);
        }

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
            return clone;
        }

        #endregion
    }
}
