using GameLogic.GamePlayerLogic;
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

namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    public class FinishWorkV1Controller : ApiController
    {
        // GET api/finishwork
        public void Get()
        {
        }

        // GET api/finishwork/5
        public async Task<Result> Get(string id)
        {           
            try
            {
                GamePlayerStorageInformation storageInfo = new GamePlayerStorageInformation(id);
                GamePlayer player = await ConfigurableDataProvider.Instance
                    .Get<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(storageInfo);

                if (player == null)
                {
                    return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT };
                }

                List<DelayedItem> items = GamePlayerManager.FinishWork(player);

                if (items.Count > 0)
                {
                    await ConfigurableDataProvider.Instance
                        .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player);
                }

                return new Result { ErrorCode = ErrorCodes.ERROR_OK, Item = items };
            }
            catch (Exception ex)
            {
                return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT, Item = ex.Message };
            }
        }

        // POST api/finishwork
        public void Post([FromBody]string value)
        {
        }

        // PUT api/finishwork/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/finishwork/5
        public void Delete(int id)
        {
        }
    }
}
