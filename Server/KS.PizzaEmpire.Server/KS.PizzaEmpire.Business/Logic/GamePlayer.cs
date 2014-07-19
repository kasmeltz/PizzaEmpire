using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.StorageInformation;
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
        /// The information fow how this entity should be stored in different types of storage
        /// </summary>
        public IStorageInformation StorageInformation { get; set; }

        public int Coins { get; set; }
        public int Coupons { get; set; }

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

            return clone;
        }

        #endregion
    }
}
