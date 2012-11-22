using System;

using Common.Model;

using Http = System.Web.Http;
using Mvc = System.Web.Mvc;
using Data;

namespace ApiMvc.Controllers
{
    /// <summary>
    /// Sample Mvc controller for Mvc REST web API
    /// </summary>
    public class UserMvcController : Mvc.Controller
    {
        /// <summary>
        /// Repository instance can be injected in the constructor
        /// </summary>
        private readonly IUserOperations repository_;
        
        //
        // GET: /UserMvc/
        public  UserMvcController()
        {
            // Only for sample, it can be injected into the constructor
            repository_ = Data.UserOperations.OperationFactory;
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns></returns>
        /// /user
        [Mvc.HttpGet]
        public Mvc.JsonResult List()
        {
            return new Mvc.JsonResult() { Data = repository_.List(), JsonRequestBehavior = Mvc.JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Gets a user specified by uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Mvc.HttpGet]
        public Mvc.JsonResult Index(Guid uid)
        {
            return new Mvc.JsonResult() { Data = repository_.Get(uid), JsonRequestBehavior = Mvc.JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Creates a user based on value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Mvc.HttpPost]
        public Mvc.JsonResult Index([Http.FromBody]UserModel value)
        {
            return new Mvc.JsonResult() { Data = repository_.Create(value) };
        }

        /// <summary>
        /// Updates the user based on value
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Mvc.HttpPut]
        public Mvc.JsonResult Index(Guid uid, [Http.FromBody]UserModel value)
        {
            value.Id = uid;
            return new Mvc.JsonResult() { Data = repository_.Update(uid, value) };
        }

        /// <summary>
        /// Deletes a user specified by uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [Mvc.HttpDelete]
        public Mvc.JsonResult Index(string uid)
        {
            return new Mvc.JsonResult() { Data = repository_.Delete(new UserModel() { Id = Guid.Parse(uid) }) };
        }

    }
}
