using System.Web.Http;
using CommonServer.Model;
using System.Collections.Generic;
using CommonServer;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ApiMvc.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<ValueModel> Get()
        {
            return ValuesOperations.ListValues();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return Json.Encode(CommonServer.ValuesOperations.GetValue(id));
        }

        // POST api/values
        public ValueModel Post([FromBody]ValueModel value)
        {
            return CommonServer.ValuesOperations.CreateValue(value);
        }

        // PUT api/values/5
        public ValueModel Put(int id, [FromBody]ValueModel value)
        {
            return CommonServer.ValuesOperations.UpdateValue(id, value);
        }

        // DELETE api/values/5
        public ValueModel Delete(int id)
        {
            return CommonServer.ValuesOperations.DeleteValue(new ValueModel() { Id = id });
        }
    }
}