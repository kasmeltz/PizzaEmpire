using GameLogic.GamePlayerLogic;
using GameLogic.ItemLogic;
using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Common;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.Business.Result;
using KS.PizzaEmpire.Business.StorageInformation;
using KS.PizzaEmpire.Business.TableStorage;
using KS.PizzaEmpire.DataAccess.DataProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace KS.PizzaEmpire.WebAPI.Controllers
{
    //[Authorize]
    public class ValuesV1Controller : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<BuildableItem>> Get()
        {
            ItemManager instance = ItemManager.Instance;            
            Dictionary<int, BuildableItem> dict = instance.BuildableItems;
            IEnumerable<BuildableItem> items = dict.Values;

            return items;
        }

        // GET api/values/key
        [System.Web.Http.HttpGet]
        [AcceptVerbs("GET")]
        public async Task<Result> Get(string id)
        {
            try
            {
                GamePlayerStorageInformation storageInfo = new GamePlayerStorageInformation(id.ToString());
                GamePlayer player = await ConfigurableDataProvider.Instance
                    .Get<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(storageInfo);

                if (player == null)
                {
                    player = GamePlayerManager.CreateNewGamePlayer();
                    player.StorageInformation = storageInfo;
                    await ConfigurableDataProvider.Instance
                        .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player);
                }

                return new Result { ErrorCode = ErrorCodes.ERROR_OK, Item = player };
            }
            catch (Exception)
            {
                return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT };
            }
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
