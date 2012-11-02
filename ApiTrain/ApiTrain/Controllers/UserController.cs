using System.Web.Http;
using CommonServer.Model;
using System.Collections.Generic;
using CommonServer;
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
            return UserOperations.ListValues();
        }

        // GET api/values/5
        public UserModel Get(Guid id)
        {
            return CommonServer.UserOperations.GetValue(id);
        }

        // POST api/values
        public UserModel Post([FromBody]UserModel value)
        {
            return CommonServer.UserOperations.CreateValue(value);
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c
        // body: {"Name":"Zsolt Schoner", Username:"zsschoner", "IsAnonymous":"false"}
        public UserModel Put(Guid uid, [FromBody]UserModel value)
        {
            value.Id = uid;
            return CommonServer.UserOperations.UpdateValue(uid, value);
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c/zsschoner/Zsolt_Schoner/true
        public UserModel Put([FromUri]UserModel value)
        {
            return CommonServer.UserOperations.UpdateValue(value.Id, value);
        }

        // DELETE api/values/5
        public UserModel Delete(Guid id)
        {
            return CommonServer.UserOperations.DeleteValue(new UserModel() { Id = id });
        }
    }
}