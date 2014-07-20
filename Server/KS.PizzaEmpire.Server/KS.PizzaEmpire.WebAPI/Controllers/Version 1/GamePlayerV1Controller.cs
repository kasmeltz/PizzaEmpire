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
    public class GamePlayerV1Controller : ApiController
    {
        // GET api/gameplayer
        public void Get()
        {
        }

        // GET api/gameplayer/5
        public async Task<Result> Get(string id)
        {
            try
            {
                GamePlayerStorageInformation storageInfo = new GamePlayerStorageInformation(id.ToString());
                GamePlayer player = await ConfigurableDataProvider.Instance
                    .Get<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(storageInfo);

                if (player == null)
                {
                    return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT };
                }

                return new Result { ErrorCode = ErrorCodes.ERROR_OK, Item = player };
            }
            catch (Exception)
            {
                return new Result { ErrorCode = ErrorCodes.ERROR_RETRIEVING_ACCOUNT };
            }
        }

        // POST api/gameplayer
        public void Post([FromBody]string value)
        {
        }

        // PUT api/gameplayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/gameplayer/5
        public void Delete(int id)
        {
        }
    }
}
