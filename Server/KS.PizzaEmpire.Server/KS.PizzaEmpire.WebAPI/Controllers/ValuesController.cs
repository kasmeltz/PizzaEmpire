using KS.PizzaEmpire.Business;
using KS.PizzaEmpire.DataAccess.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            IGamePlayerDataProvider provider = new CachedTableStoreGamePlayerDataProvider();

            GamePlayer player = await provider.Get("KevinSmeltzer");

            if (player == null)
            {
                player = new GamePlayer { UniqueKey = "KevinSmeltzer" };
            }

            await provider.Save(player);

            return new string[] { player.ETag, player.PartitionKey, player.RowKey, player.UniqueKey };
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
