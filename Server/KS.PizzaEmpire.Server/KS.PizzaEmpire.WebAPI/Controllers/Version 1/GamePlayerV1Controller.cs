namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    using Business.Cache;
    using Business.StorageInformation;
    using Business.TableStorage;
    using Common.BusinessObjects;
    using DataAccess.DataProvider;
    using Common;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using KS.PizzaEmpire.Common.APITransfer;

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
                    return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
                }

                GamePlayerAPIMorph morph = new GamePlayerAPIMorph();
                return new Result { ErrorCode = ErrorCode.ERROR_OK, Item = morph.ToAPIFormat(player) };
            }
            catch (Exception)
            {
                return new Result { ErrorCode = ErrorCode.ERROR_RETRIEVING_ACCOUNT };
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
