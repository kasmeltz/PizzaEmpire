using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.DataAccess.DataProvider;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace KS.PizzaEmpire.WebAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            GamePlayerStorageInformation storageInfo = new GamePlayerStorageInformation("kasmeltz@lakeheadu.ca");
            GamePlayer player = await ConfigurableDataProvider.Instance
                .Get<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(storageInfo);

            if (player == null)
            {
                player = new GamePlayer();
                player.Coins = 1000;
                player.Coupons = 5;
                player.StorageInformation = storageInfo;
                await ConfigurableDataProvider.Instance
                    .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player);
            }

            return new string[] 
            {
                player.StorageInformation.UniqueKey, 
                player.StorageInformation.CacheKey, 
                player.StorageInformation.TableName, 
                player.StorageInformation.PartitionKey,
                player.StorageInformation.RowKey,
                player.StorageInformation.FromCache.ToString(),
                player.StorageInformation.FromTableStorage.ToString(),
                player.Coins.ToString(), 
                player.Coupons.ToString(), 
            };
        }

        // GET api/values/5
        public string Get(string uniqueKey)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
