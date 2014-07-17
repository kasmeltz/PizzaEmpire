using KS.PizzaEmpire.Services.Serialization;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace KS.PizzaEmpire.Services.Caching
{
    /// <summary>
    /// A wrapper class for an Azure Redis Cache instance.
    /// Is to be uesed as a Singleton as there should only ever be
    /// one Connection for the entire application.
    /// The ConnectionString should be set before the Cache is used:
    /// AzureRedisCache.Instance.ConnectionString = "conoso.www.com,ssl=true,password=";   
    /// </summary>
    public sealed class AzureRedisCache
    {
        private static volatile AzureRedisCache instance;
        private static object syncRoot = new Object();

        private AzureRedisCache() { }

        /// <summary>
        /// Provides the Singleton instance of the AzureRedisCache
        /// </summary>
        public static AzureRedisCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AzureRedisCache();
                            instance.CacheSerializer = new BinaryFormatSerializer();
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
        ///  Defaults to BinaryCacheSerializer
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
        
        /// <summary>.
        /// Returns an instance of type T from the cache
        /// </summary>
        /// <typeparam name="T">The data type of the item that is stored in the cache.</typeparam>
        /// <param name="key">The key of the item in the cache.</param>
        /// <returns>The item from the cache.</returns>
        public async Task<T> Get<T>(string key, CommandFlags flags = CommandFlags.None)
        {
            IDatabase cache = Connection.GetDatabase();
            return CacheSerializer.Deserialize<T>(await cache.StringGetAsync(key, flags));
        }
       
        /// <summary>
        /// Stores an item in the cache.
        /// </summary>
        /// <param name="key">The key of the item in the cache.</param>
        /// <param name="value">The item tio store</param>
        public async Task Set(string key, object value, 
            TimeSpan timeSpan, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            IDatabase cache = Connection.GetDatabase();
            await cache.StringSetAsync(key, CacheSerializer.Serialize(value), timeSpan, when, flags);
        }        
    }
}