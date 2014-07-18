using KS.PizzaEmpire.Business.Cache;
using KS.PizzaEmpire.Business.Logic;
using KS.PizzaEmpire.DataAccess.DataProvider;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace KS.PizzaEmpire.WebAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            GamePlayer player = new GamePlayer { UniqueKey = "KevinSmeltzer" };
            player = await ConfigurableDataProvider.Instance.Get<GamePlayer, GamePlayerCacheable>(player);

            if (player == null)
            {
                player = new GamePlayer { UniqueKey = "KevinSmeltzer" };
                await ConfigurableDataProvider.Instance.Save(player);
                return new string[] { "New", player.UniqueKey };
            }
            else
            {
                return new string[] { "From Data", player.UniqueKey };
            }                        
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
