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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    public class StartWorkV1Controller : ApiController
    {
        // GET api/startwork
        public void Get()
        {
        }

        // GET api/startwork/5
        public async Task<Result> Get(string id)
        {
            string[] parts = id.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries );
            string playerKey = parts[0];
            int itemCode = Int32.Parse(parts[1]);

            try
            {
                GamePlayerStorageInformation storageInfo = new GamePlayerStorageInformation(playerKey);
                GamePlayer player = await ConfigurableDataProvider.Instance
                    .Get<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(storageInfo);

                if (player == null)
                {
                    return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT };
                }

                DelayedItem item = GamePlayerManager.StartWork(player, itemCode);

                if (item != null)
                {
                    await ConfigurableDataProvider.Instance
                        .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player);
                }

                return new Result { ErrorCode = ErrorCodes.ERROR_OK, Item = item };
            }
            catch (Exception ex)
            {
                return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT, Item = ex.Message };
            }
        }

        // POST api/startwork
        public void Post([FromBody]string value)
        {
        }

        // PUT api/startwork/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/startwork/5
        public void Delete(int id)
        {
        }
    }
}
