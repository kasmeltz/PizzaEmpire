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
            IGamePlayerDataProvider provider = new ConfigurableGamePlayerDataProvider();

            GamePlayer player = await provider.Get("KevinSmeltzer");

            if (player == null)
            {
                player = new GamePlayer { UniqueKey = "KevinSmeltzer" };
            }

            await provider.Save(player);

            return new string[] { player.UniqueKey };
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
