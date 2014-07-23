namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    using Business.Logic;
    using GameLogic.ItemLogic;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class BuildableItemV1Controller : ApiController
    {
        // GET api/buildableitem
        public IEnumerable<BuildableItem> Get()
        {
            return ItemManager.Instance.BuildableItems.Values;
        }

        // GET api/buildableitem/5
        public void Get(string id)
        {
        }

        // POST api/buildableitem
        public async Task<string> Post(string id)
        {
            return "hello " + id;
        }

        // PUT api/buildableitem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/buildableitem/5
        public void Delete(int id)
        {
        }
    }
}
