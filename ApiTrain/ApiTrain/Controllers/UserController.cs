using System.Web.Http;
using Common.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Linq;

namespace ApiMvc.Controllers
{
    public class UserController : ApiController
    {
        private readonly Data.IUserOperations repository_;

        public  UserController()
        {
            repository_ = Data.UserOperations.Instance;
        }

        // GET api/values
        // Result will be sent based on Accept header.
        // If Accept header is in chrome like default: Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        // the result will sent in xml, but if the client set Accept: application/json header the result will be sent
        // in JSON format
        [Queryable]
        public IQueryable<UserModel> Get()
        {
            try
            {
                return repository_.List().AsQueryable();
            }
            catch (Exception ex)
            {
                // Send the error message to the client
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        // GET api/values/5
        public UserModel Get(Guid id)
        {
            try
            {
                return repository_.Get(id);
            }
            catch (Exception ex)
            {
                var emsg = new System.Net.Http.HttpResponseMessage(HttpStatusCode.BadRequest);
                // Send the error message to the client
                emsg.ReasonPhrase = ex.Message;
                throw new HttpResponseException(emsg);
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]UserModel value)
        {
            try
            {
                var resultValue = repository_.Create(value);

                return Request.CreateResponse<UserModel>(HttpStatusCode.Created, resultValue);
            }
            catch (Exception ex)
            {
                // Send the error message to the client
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c
        // body: {"Name":"Zsolt Schoner", Username:"zsschoner", "IsAnonymous":"false"}
        public UserModel Put(Guid uid, [FromBody]UserModel value)
        {
            try
            {
                value.Id = uid;
                return repository_.Update(uid, value);
            }
            catch (Exception ex)
            {
                // Send the error message to the client
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        // PUT /api/user/66cfbd2dd52f418e88152260f24aed7c/zsschoner/Zsolt_Schoner/true
        public UserModel Put([FromUri]UserModel value)
        {
            try
            {
                return repository_.Update(value.Id, value);
            }
            catch (Exception ex)
            {
                // Send the error message to the client
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }

        // DELETE api/values/5
        public UserModel Delete(Guid id)
        {
            try
            {
                return repository_.Delete(new UserModel() { Id = id });
            }
            catch (Exception ex)
            {
                // Send the error message to the client
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }
        }
    }
}