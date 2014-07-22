namespace KS.PizzaEmpire.WebAPI.Controllers
{
    using Business.Cache;
    using Business.Common;
    using Business.Logic;
    using Business.Result;
    using Business.StorageInformation;
    using Business.TableStorage;
    using DataAccess.DataProvider;
    using GameLogic.GamePlayerLogic;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    //[Authorize]
    public class ValuesV1Controller : ApiController
    {
        // GET api/values
        public void Get()
        {
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
