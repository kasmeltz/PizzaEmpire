namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
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

    public class StartWorkV1Controller : ApiController
    {
        // GET api/startwork
        public void Get()
        {
        }

        // GET api/startwork/uniqueKey@@itemCode
        public async Task<Result> Get(string id)
        {
            string[] parts = id.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
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

                WorkItem item = GamePlayerManager.StartWork(player, (BuildableItemEnum)itemCode);

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
