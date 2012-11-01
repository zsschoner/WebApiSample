using System.Web.Http;
using CommonServer.Model;
using System.Collections.Generic;
using CommonServer;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web;
using System.Runtime.Serialization;
using System.Linq;

namespace ApiMvc.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        // Result will be sent based on Accept header.
        // If Accept header is in chrome like default: Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        // the result will be sent in xml, but if the client set Accept: application/json header the result will be sent
        // in JSON format
        public IEnumerable<ValueModel> Get()
        {
            return ValuesOperations.ListValues();
        }

        // GET api/values/5
        public ValueModel Get(int id)
        {
            return CommonServer.ValuesOperations.GetValue(id);
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