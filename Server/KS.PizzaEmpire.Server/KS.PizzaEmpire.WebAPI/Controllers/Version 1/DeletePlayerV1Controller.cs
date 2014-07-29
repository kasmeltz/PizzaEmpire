namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Common;
    using Common.BusinessObjects;
    using Common.GameLogic;
    using DataAccess.DataProvider;
    using KS.PizzaEmpire.Common.APITransfer;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[Authorize]
    public class DeletePlayerV1Controller : ApiController
    {
        // GET api/deleteplayer
        public void Get()
        {
        }

        // GET api/deleteplayer/key
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
                    return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
                }

                await ConfigurableDataProvider.Instance
                    .Delete<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player, storageInfo);

                return new Result { ErrorCode = ErrorCode.ERROR_OK };
            }
            catch (Exception)
            {
                return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
            }
        }

        // POST api/deleteplayer
        public void Post([FromBody]string value)
        {
        }

        // PUT api/deleteplayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/deleteplayer/5
        public void Delete(int id)
        {
        }
    }
}
