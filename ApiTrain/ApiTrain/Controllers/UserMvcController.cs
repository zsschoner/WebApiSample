using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ApiMvc.Models;
using Common.Model;

using Http = System.Web.Http;
using Mvc = System.Web.Mvc;

namespace ApiMvc.Controllers
{
    public class UserMvcController : Mvc.Controller
    {
        //
        // GET: /UserMvc/

        [Mvc.HttpGet]
        public Mvc.JsonResult List()
        {
            return new Mvc.JsonResult() { Data = Data.UserOperations.List(), JsonRequestBehavior = Mvc.JsonRequestBehavior.AllowGet };
        }

        [Mvc.HttpGet]
        public Mvc.JsonResult Index(Guid uid)
        {
            return new Mvc.JsonResult() { Data = Data.UserOperations.Get(uid), JsonRequestBehavior = Mvc.JsonRequestBehavior.AllowGet };
        }

        // POST api/values
        [Mvc.HttpPost]
        public Mvc.JsonResult Index([Http.FromBody]UserModel value)
        {
            return new Mvc.JsonResult() { Data = Data.UserOperations.Create(value) };
        }

        [Mvc.HttpPut]
        public Mvc.JsonResult Index(Guid uid, [Http.FromBody]UserModel value)
        {
            value.Id = uid;
            return new Mvc.JsonResult() { Data = Data.UserOperations.Update(uid, value) };
        }

        [Mvc.HttpDelete]
        public Mvc.JsonResult Index(string uid)
        {
            return new Mvc.JsonResult() { Data = Data.UserOperations.Delete(new UserModel() { Id = Guid.Parse(uid) }) };
        }

    }
}
