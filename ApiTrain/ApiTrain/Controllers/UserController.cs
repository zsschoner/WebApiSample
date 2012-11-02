using System.Web.Http;
using Common.Model;
using System.Collections.Generic;
using Common;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web;
using System.Runtime.Serialization;
using System.Linq;
using System;

namespace ApiMvc.Controllers
{
    public class UserController : ApiController
    {
        // GET api/values
        // Result will be sent based on Accept header.
        // If Accept header is in chrome like default: Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        // the result will be sent in xml, but if the client set Accept: application/json header the result will be sent
        // in JSON format
        public IEnumerable<UserModel> Get()
        {
            return Data.UserOperations.List();
        }

        // GET api/values/5
        public UserModel Get(Guid id)
        {
            return Data.UserOperations.Get(id);
        }

        // POST api/values
        public UserModel Post([FromBody]UserModel value)
        {
            return Data.UserOperations.Create(value);
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c
        // body: {"Name":"Zsolt Schoner", Username:"zsschoner", "IsAnonymous":"false"}
        public UserModel Put(Guid uid, [FromBody]UserModel value)
        {
            value.Id = uid;
            return Data.UserOperations.Update(uid, value);
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c/zsschoner/Zsolt_Schoner/true
        public UserModel Put([FromUri]UserModel value)
        {
            return Data.UserOperations.Update(value.Id, value);
        }

        // DELETE api/values/5
        public UserModel Delete(Guid id)
        {
            return Data.UserOperations.Delete(new UserModel() { Id = id });
        }
    }
}