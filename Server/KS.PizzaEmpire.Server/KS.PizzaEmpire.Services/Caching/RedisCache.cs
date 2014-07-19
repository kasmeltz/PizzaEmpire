using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Services.Serialization;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Caching
{
    /// <summary>
    /// Represents an item that can communicate with a Redis Cache for
    /// storing and retrieving items.
    /// Is to be uesed as a Singleton as there should only ever be
    /// one Connection for the entire application.
    /// The ConnectionString should be set before the Cache is used:
    /// RedisCache.Instance.ConnectionString = "conoso.www.com,ssl=true,password=";   
    /// </summary>
    public sealed class RedisCache
    {
        private static volatile RedisCache instance;
        private static object syncRoot = new Object();

        private RedisCache() { }

        /// <summary>
        /// Provides the Singleton instance of the RedisCache
        /// </summary>
        public static RedisCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new RedisCache();
                            instance.CacheSerializer = new ProtoBufSerializer();
                            instance.ConnectionString = Microsoft.WindowsAzure.CloudConfigurationManager.GetSetting("RedisCacheConnectionString");
                            instance.MaxRetryAttempts = ServiceHelper.IntValueFromConfig("RedisCacheMaxRetryAttempts");
                            instance.RetryMillis = ServiceHelper.IntValueFromConfig("RedisCacheRetryMillis");
                            instance.ThrottleMillis = ServiceHelper.IntValueFromConfig("RedisCacheThrottleMillis");
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The address of the cache to use. Set this value
        /// before using the Cache.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///  The serializer to use when storing or retrieving items.
        ///  Defaults to BinaryFormatSerializer
        /// </summary>
        public ICacheSerializer CacheSerializer { get; set; }

        private ConnectionMultiplexer connection;
        private ConnectionMultiplexer Connection
        {
            get
            {
                if (connection == null || !connection.IsConnected)
                {
                    connection = ConnectionMultiplexer.
                        Connect(ConnectionString);                            
                }
                return connection;
            }
        }

        /// <summary>
        /// Forces a reconnection to the Redis Server
        /// </summary>
        public void Reconnect()
        {
            connection = null;
        }

        /// <summary>
        /// The number of times to retry a failed cache operation before giving up and throwing an Exception.
        /// </summary>
        public int MaxRetryAttempts { get; set; }

        /// <summary>
        /// The number of milliseconds to wait before retrying a failed cache operation.
        /// </summary>
        public int RetryMillis { get; set; }

        /// <summary>
        /// The number of milliseconds to wait before performing a cache operation.
        /// </summary>
        public int ThrottleMillis { get; set; }
        
        /// <summary>.
        /// Returns an instance of type T from the cache
        /// </summary>
        /// <typeparam name="T">The data type of the item that is stored in the cache.</typeparam>
        /// <param name="key">The key of the item in the cache.</param>
        /// <returns>The item from the cache.</returns>
        public async Task<T> Get<T>(string key, CommandFlags flags = CommandFlags.None) 
            where T : ICacheEntity
        {
            return await ServiceHelper.RetryAsync<T>(async () =>
            {
                IDatabase cache = Connection.GetDatabase();
                RedisValue value = await cache.StringGetAsync(key, flags);
                if (value.IsNull)
                {
                    return default(T);
                }
                return CacheSerializer.Deserialize<T>(value);
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }
       
        /// <summary>
        /// Stores an item in the cache.
        /// </summary>
        /// <param name="key">The key of the item in the cache.</param>
        /// <param name="value">The item tio store</param>
        public async Task Set<T>(string key, T value,
            TimeSpan ts, When when = When.Always, CommandFlags flags = CommandFlags.None) 
            where T : ICacheEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                IDatabase cache = Connection.GetDatabase();
                await cache.StringSetAsync(key, CacheSerializer.Serialize(value), ts, when, flags);
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }

        /// <summary>
        /// Removes the item with the specified key from the cache if it exists.
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns></returns>
        public async Task Delete<T>(string key, CommandFlags flags = CommandFlags.None) 
            where T : ICacheEntity
        {
            await ServiceHelper.RetryAsync<int>(async () =>
            {
                IDatabase cache = Connection.GetDatabase();
                if (await cache.KeyExistsAsync(key))
                {
                    await cache.KeyDeleteAsync(key);
                }
                return 1;
            }, RetryMillis, MaxRetryAttempts, ThrottleMillis);
        }
    }
}