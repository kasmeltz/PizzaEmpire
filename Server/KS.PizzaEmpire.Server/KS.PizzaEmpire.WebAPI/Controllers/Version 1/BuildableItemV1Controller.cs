namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    using KS.PizzaEmpire.Common.APITransfer;
    using KS.PizzaEmpire.Common.BusinessObjects;
    using KS.PizzaEmpire.Common.Utility;
    using KS.PizzaEmpire.GameLogic.ItemLogic;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class BuildableItemV1Controller : ApiController
    {
        // GET api/buildableitem
        public IEnumerable<BuildableItemAPI> Get()
        {
            BuildableItemAPIMorph morph = new BuildableItemAPIMorph();
            List<BuildableItemAPI> items = new List<BuildableItemAPI>();
            foreach (BuildableItem item in ItemManager.Instance.BuildableItems.Values)
            {
                items.Add((BuildableItemAPI)morph.ToAPIFormat(item));             
            }
            return items;
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
