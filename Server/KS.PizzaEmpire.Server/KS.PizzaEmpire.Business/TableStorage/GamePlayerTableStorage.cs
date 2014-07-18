using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using Microsoft.WindowsAzure.Storage.Table;

namespace KS.PizzaEmpire.Business.TableStorage
{
    /// <summary>
    /// Represents the state for a player of the game as stored in table storage.
    /// </summary>
    public class GamePlayerTableStorage : TableEntity, ITableStorageEntity, IToCacheEntity, IToLogicEntity
    {
        /// <summary>
        /// Creates a new instance of the GamePlayerTableStorage class.
        /// </summary>
        public GamePlayerTableStorage()
        { }

        /// <summary>
        /// Returns a parition key for table storage from the provided key
        /// </summary>
        /// <returns></returns>
        public static string AutoPartitionKey(string key)
        {
            return key.Substring(0, 2);
        }
        
        /// <summary>
        /// A unique identifier for this Game Player across the application
        /// </summary>
        private string _uniqueKey;
        public string UniqueKey
        {
            get
            {
                return _uniqueKey;
            }
            set
            {
                _uniqueKey = value;
                PartitionKey = AutoPartitionKey(_uniqueKey);
                RowKey = _uniqueKey;
            }
        }

        #region ITableStorage

        /// <summary>
        /// Returns a new instance of the appropriate ICacheEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ICacheEntity ToCacheEntity()
        {
            return GamePlayerCacheable.From(this);
        }

        /// <summary>
        /// Returns a new instance of the appropriate ILogicEntity with cloned data.
        /// </summary>
        /// <returns></returns>
        public ILogicEntity ToLogicEntity()
        {
            return GamePlayer.From(this);
        }

        #endregion ITableStorage

        #region Cloners

        /// <summary>
        /// Generates a new GamePlayerTableStorage instance from a GamePlayerCacheable instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerTableStorage From(GamePlayerCacheable item)
        {
            GamePlayerTableStorage clone = new GamePlayerTableStorage();
            clone.UniqueKey = item.UniqueKey;

            return clone;
        }

        /// <summary>
        /// Generates a new GamePlayerTableStorage instance from a GamePlayer instance.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static GamePlayerTableStorage From(GamePlayer item)
        {
            GamePlayerTableStorage clone = new GamePlayerTableStorage();
            clone.UniqueKey = item.UniqueKey;

            return clone;
        }

        #endregion
    }
}
