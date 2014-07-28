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
    public class CreatePlayerV1Controller : ApiController
    {
        // GET api/createplayer
        public void Get()
        {
        }

        // GET api/createplayer/key
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
                    player = GamePlayerLogic.Instance.CreateNewGamePlayer();
                    player.StateChanged = false;
                    await ConfigurableDataProvider.Instance
                        .Save<GamePlayer, GamePlayerCacheable, GamePlayerTableStorage>(player, storageInfo);
                }

                GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
                return new Result { ErrorCode = ErrorCode.ERROR_OK, Item = morph.ToAPIFormat(player) };
            }
            catch (Exception)
            {
                return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
            }
        }

        // POST api/createplayer
        public void Post([FromBody]string value)
        {
        }

        // PUT api/createplayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/createplayer/5
        public void Delete(int id)
        {
        }
    }
}
