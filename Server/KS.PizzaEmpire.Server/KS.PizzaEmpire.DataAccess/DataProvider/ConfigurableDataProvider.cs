using KS.PizzaEmpire.Business;
using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Services.Caching;
using KS.PizzaEmpire.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.DataAccess.DataProvider
{
    /// <summary>
    /// Represents an item that will provide persistant data about
    /// the game world using a combination of data techniques that can
    /// be configured.
    /// </summary>
    public class ConfigurableDataProvider
    {
        private static volatile ConfigurableDataProvider instance;
        private static object syncRoot = new Object();

        private ConfigurableDataProvider() { }

        /// <summary>
        /// Provides the Singleton instance of the RedisCache
        /// </summary>
        public static ConfigurableDataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ConfigurableDataProvider();
                            instance.UseCache = true;
                            instance.UseTableStorage = true;
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Whether or not to use the cache
        /// </summary>
        private bool _useCache;
        public bool UseCache
        {
            get
            {
                return _useCache;
            }
            set
            {
                _useCache = value;
            }
        }


        /// <summary>
        /// Whether or not to use Table Storage
        /// </summary>
        private bool _useTableStorage;
        public bool UseTableStorage
        {
            get
            {
                return _useTableStorage;
            }
            set
            {
                _useTableStorage = value;
            }
        }
       
        /// <summary>
        /// Gets a GamePlayer from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ILogicEntity> GetFromCache<T, K>(T searchItem)
            where T : ILogicEntity, IToCacheEntity
            where K : ICacheEntity, IToLogicEntity
        {
            if (!_useCache)
            {
                return default(T);
            }

            K cachedItem = await RedisCache.Instance.Get<K>(searchItem.CacheKey);
            if (cachedItem == null)
            {
                return default(T);
            }

            return cachedItem.ToLogicEntity();
        }

        /// <summary>
        /// Retrieves the persistent data for a game player from the data store.
        /// </summary>
        /// <param name="key">The unique key of the game player whose data is requested</param>
        /// <returns>A GamePlayer instance representing the persistent data associated with the 
        /// game player</returns>
        public async Task<T> Get<T, K>(T searchItem)
            where T : ILogicEntity, IToCacheEntity
            where K : ICacheEntity, IToLogicEntity
        {
            T item = (T)await GetFromCache<T,K>(searchItem);

            if (item == null)
            {
                 AzureTableStorage storage = new AzureTableStorage();
                 //await storage.SetTable(searchItem.TableName);
                /*
                string cacheKey = "GP" + key;
                GamePlayer player = await RedisCache.Instance.Get<GamePlayer>(cacheKey);
                if (player == null)
                {
                    AzureTableStorage storage = 
                            new AzureTableStorage(
                                "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");
                    await storage.SetTable("GamePlayer");
                    player = await storage.Get<GamePlayer>(GamePlayerTableStorage.AutoPartitionKey(key), key);                
                    if (player == null)
                    {
                        return null;
                    }
                    await RedisCache.Instance.Set(cacheKey, player, TimeSpan.FromHours(24));
                }

                return player;
                */
            }

                return item;
        }

        /// <summary>
        /// Saves the data for a game player to the persitent data store.
        /// </summary>
        /// <param name="player">The GamePlayer instance to save.</param>
        /// <returns>This is an async method</returns>
        public async Task Save(GamePlayer player)
        {
            /*
            string cacheKey = "GP" + player.UniqueKey;
            await RedisCache.Instance.Set(cacheKey, player, TimeSpan.FromHours(6));
             */
        }

        /// <summary>
        /// Sets the data for the provided game player to active or inactive
        /// </summary>
        /// <param name="player">The GamePlayer instance to set active or inactive</param>
        /// <param name="active">True for active, false for inactive</param>
        /// <returns>This is an async method</returns>
        public async Task SetInactive(GamePlayer player)
        {
            /*
            string cacheKey = "GP" + player.UniqueKey;
            AzureTableStorage storage =
                       new AzureTableStorage(
                           "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");
            await storage.SetTable("GamePlayer");
            await storage.InsertOrReplace(player);
            await RedisCache.Instance.Delete(cacheKey);
             * */
        }
    }
}