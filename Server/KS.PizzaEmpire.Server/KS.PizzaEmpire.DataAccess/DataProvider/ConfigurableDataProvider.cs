using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Conversion;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.Services;
using KS.PizzaEmpire.Services.Caching;
using KS.PizzaEmpire.Services.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
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
                            instance.CacheDuration = ServiceHelper.IntValueFromConfig("DataProviderCacheDuration");
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
        /// The length of time to store an item in the cache
        /// </summary>
        public int CacheDuration { get; set; }

        /// <summary>
        /// Gets an item from Table Storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ILogicEntity> GetFromTableStorage<T, K>(IStorageInformation storageInfo)
            where T : ILogicEntity
            where K : TableEntity, ITableStorageEntity, IToLogicEntity
        {
            if (!_useTableStorage)
            {
                return default(T);
            }

            try
            {
                AzureTableStorage storage = new AzureTableStorage();
                await storage.SetTable(storageInfo.TableName);

                K storageItem = await storage.Get<K>(storageInfo.PartitionKey, storageInfo.RowKey);
                if (storageItem == null)
                {
                    return default(T);
                }

                storageInfo.FromTableStorage = true;
                ILogicEntity item = storageItem.ToLogicEntity();
                item.StorageInformation = storageInfo;

                return item;
            }
            catch (Exception)
            {
                //@ TODO what bahevior do we want if we are unable to get item from Table Storage?
                return default(T);
            }
        }

        /// <summary>
        /// Gets an item from the Redis Ccache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ILogicEntity> GetFromCache<T, K>(IStorageInformation storageInfo)
            where T : ILogicEntity
            where K : ICacheEntity, IToLogicEntity
        {
            if (!_useCache)
            {
                return default(T);
            }

            try
            {
                K cachedItem = await RedisCache.Instance.Get<K>(storageInfo.CacheKey);
                if (cachedItem == null)
                {
                    return default(T);
                }

                storageInfo.FromCache = true;
                ILogicEntity item = cachedItem.ToLogicEntity();
                item.StorageInformation = storageInfo;

                return item;
            }
            catch (Exception)
            {
                //@ TODO what bahevior do we want if we are unable to get item from the cache?
                return default(T);
            }
        }

        /// <summary>
        /// Retrieves the persistent data for a game player from the data store.
        /// </summary>
        /// <param name="key">The unique key of the game player whose data is requested</param>
        /// <returns>An ILogicEntity instance representing the persistent data requested</returns>
        public async Task<T> Get<T, K, V>(IStorageInformation storageInfo)
            where T : ILogicEntity, IToCacheEntity
            where K : ICacheEntity, IToLogicEntity
            where V : TableEntity, ITableStorageEntity, IToLogicEntity
        {
            T item = (T)await GetFromCache<T, K>(storageInfo);

            if (item == null)
            {
                item = (T)await GetFromTableStorage<T, V>(storageInfo);
                if (item != null)
                {
                    await SaveToCache<T, K>(item);
                }
            }

            return item;
        }

        /// <summary>
        /// Saves an item to the Table Storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task SaveToTableStorage<T, K>(T item)
            where T : ILogicEntity, IToTableStorageEntity
            where K : TableEntity, ITableStorageEntity
        {
            if (!_useTableStorage)
            {
                return;
            }

            try
            {
                AzureTableStorage storage = new AzureTableStorage();
                await storage.SetTable(item.StorageInformation.TableName);
                await storage.InsertOrReplace<K>((K)item.ToTableStorageEntity());
            }
            catch (Exception)
            {
                //@ TODO what bahevior do we want if we are unable to save to table storage?
            }
        }

        /// <summary>
        /// Saves an item to the Redis Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task SaveToCache<T, K>(T item)
            where T : ILogicEntity, IToCacheEntity
            where K : ICacheEntity
        {
            if (!_useCache)
            {
                return;
            }

            try
            {
                await RedisCache.Instance
                    .Set<K>(item.StorageInformation.CacheKey,
                    (K)item.ToCacheEntity(), TimeSpan.FromSeconds(CacheDuration));
            }
            catch (Exception)
            {
                //@ TODO what bahevior do we want if we are unable to save to the cache?
            }
        }

        /// <summary>
        /// Saves the data for an ILogicEntity to the persitent data store.
        /// </summary>
        /// <param name="player">The ILogicEntity instance to save.</param>
        /// <returns>This is an async method</returns>
        public async Task Save<T, K, V>(T item)
            where T : ILogicEntity, IToCacheEntity, IToTableStorageEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            await SaveToCache<T,K>(item);
            await SaveToTableStorage<T,V>(item);            
        }

        /// <summary>
        /// Informs the data proivder that the provided item is no longer being actively used
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public async Task SetInactive<T, K, V>(T item)
            where T : ILogicEntity, IToTableStorageEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            if (!_useCache)
            {
                return;
            }

            await SaveToTableStorage<T, V>(item);   
            await RedisCache.Instance.Delete<K>(item.StorageInformation.CacheKey);
        }
    }
}