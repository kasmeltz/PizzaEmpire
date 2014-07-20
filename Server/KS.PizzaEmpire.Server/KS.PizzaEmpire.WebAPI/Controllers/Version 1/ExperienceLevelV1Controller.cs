using GameLogic.ExperienceLevelLogic;
using KS.PizzaEmpire.Business.Logic;
using System.Collections.Generic;
using System.Web.Http;

namespace KS.PizzaEmpire.WebAPI.Controllers.Version_1
{
    public class ExperienceLevelV1Controller : ApiController
    {
        // GET api/experiencelevel
        public IEnumerable<ExperienceLevel> Get()
        {
            return ExperienceLevelManager.Instance.ExperienceLevels.Values;
        }

        // GET api/experiencelevel/5
        public void Get(string id)
        {            
        }

        // POST api/experiencelevel
        public void Post([FromBody]string value)
        {
        }

        // PUT api/experiencelevel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/experiencelevel/5
        public void Delete(int id)
        {
        }
    }
}
