namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using DataAccess.DataProvider;
    using Common;
    using Common.BusinessObjects;
    using Common.GameLogic;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

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
                    return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
                }

                DateTime now = DateTime.UtcNow;
                List<WorkItem> items = GamePlayerLogic.Instance.FinishWork(player, now);

                if (player.StateChanged)
                {
                    player.StateChanged = false;
                    await ConfigurableDataProvider.Instance
                        .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player, storageInfo);
                }

                return new Result { ErrorCode = ErrorCode.ERROR_OK, Item = now };
            }
            catch (Exception ex)
            {
                return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT, Item = ex.Message };
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
