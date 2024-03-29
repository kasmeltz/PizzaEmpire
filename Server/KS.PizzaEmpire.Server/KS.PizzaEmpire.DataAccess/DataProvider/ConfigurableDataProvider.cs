﻿namespace KS.PizzaEmpire.DataAccess.DataProvider
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Common.BusinessObjects;
    using Microsoft.WindowsAzure.Storage.Table;
    using Services;
    using Services.Caching;
    using Services.Storage;
    using System;
    using System.Threading.Tasks;

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
        public async Task<IBusinessObjectEntity> GetFromTableStorage<T, K>(IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : TableEntity, ITableStorageEntity
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

                storageInfo.FoundInTableStorage = true;
                IBusinessObjectEntity item = storageInfo.FromTableStorage(storageItem);

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
        public async Task<IBusinessObjectEntity> GetFromCache<T, K>(IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : ICacheEntity
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

                storageInfo.FoundInCache = true;
                IBusinessObjectEntity item = storageInfo.FromCache(cachedItem);

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
            where T : IBusinessObjectEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            T item = (T)await GetFromCache<T, K>(storageInfo);

            if (item == null)
            {
                item = (T)await GetFromTableStorage<T, V>(storageInfo);
                if (item != null)
                {
                    await SaveToCache<T, K>(item, storageInfo);
                }
            }

            return item;
        }

        /// <summary>
        /// Saves an item to the Table Storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task SaveToTableStorage<T, K>(T item, IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : TableEntity, ITableStorageEntity
        {
            if (!_useTableStorage)
            {
                return;
            }

            try
            {
                AzureTableStorage storage = new AzureTableStorage();
                await storage.SetTable(storageInfo.TableName);

                //@ TODO What we really want here is lazy saving of information from
                // the cache to the Table Storage layer. The question is how to design this?
                // The following is safest - just write to the table storage every time 
                // we write to the cache. The goal is to make it more effiicent while
                // still ensuring safety
                await storage.InsertOrReplace<K>((K)storageInfo.ToTableStorage(item));
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
        public async Task SaveToCache<T, K>(T item, IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : ICacheEntity
        {
            if (!_useCache)
            {
                return;
            }

            try
            {
                await RedisCache.Instance
                    .Set<K>(storageInfo.CacheKey,
                    (K)storageInfo.ToCache(item), TimeSpan.FromSeconds(CacheDuration));
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
        public async Task Save<T, K, V>(T item, IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            try
            {
                await SaveToCache<T, K>(item, storageInfo);
            }
            catch
            {
                Task.WaitAll(RedisCache.Instance.Delete<K>(storageInfo.CacheKey));
            }

            await SaveToTableStorage<T, V>(item, storageInfo);
        }

        /// <summary>
        /// Deelete the data represented by the storage info class
        /// </summary>
        /// <param name="player">The ILogicEntity instance to save.</param>
        /// <returns>This is an async method</returns>
        public async Task Delete<T, K, V>(T item, IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            await RedisCache.Instance.Delete<K>(storageInfo.CacheKey);
            AzureTableStorage storage = new AzureTableStorage();
            await storage.SetTable(storageInfo.TableName);
            await storage.Delete<V>((V)storageInfo.ToTableStorage(item));
        }

        /// <summary>
        /// Informs the data proivder that the provided item is no longer being actively used
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public async Task SetInactive<T, K, V>(T item, IStorageInformation storageInfo)
            where T : IBusinessObjectEntity
            where K : ICacheEntity
            where V : TableEntity, ITableStorageEntity
        {
            if (!_useCache)
            {
                return;
            }

            try
            {
                await SaveToTableStorage<T, V>(item, storageInfo);
                await RedisCache.Instance.Delete<K>(storageInfo.CacheKey);
            }
            catch
            {
                // @ TO DO WHAT TO DO IF TABLE STORAGE FAILS?
                // AT LEAST DON'T REMOVE IT FROM CACHE .. AND THEN PANIC!
            }
        }
    }
}