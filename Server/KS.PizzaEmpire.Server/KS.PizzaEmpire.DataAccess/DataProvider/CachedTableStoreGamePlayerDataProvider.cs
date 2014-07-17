using KS.PizzaEmpire.Business;
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
    /// a player of the game using a combination of caching and table storage.
    /// </summary>
    public class CachedTableStoreGamePlayerDataProvider : IGamePlayerDataProvider
    {
        /// <summary>
        /// Retrieves the persistent data for a game player from the data store.
        /// </summary>
        /// <param name="key">The unique key of the game player whose data is requested</param>
        /// <returns>A GamePlayer instance representing the persistent data associated with the 
        /// game player</returns>
        public async Task<GamePlayer> Get(string key)
        {
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

            return null;
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
