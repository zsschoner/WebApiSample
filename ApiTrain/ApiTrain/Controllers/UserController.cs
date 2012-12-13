using System.Web.Http;
using Common.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Web.Http.Filters;

namespace ApiMvc.Controllers
{
    /// <summary>
    /// Sample web api controller
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// Repository instance can be injected in the constructor
        /// </summary>
        private readonly Data.IUserOperations repository_;

        public UserController()
        {
            repository_ = Data.UserOperations.OperationFactory;
        }

        

        /// <summary>
        /// Gets a user specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>GET api/user/abf959cf-9bb5-4ea0-9d34-83693aa4046a</remarks>
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

        // GET api/user
        // Gets the list of users
        // Result will be sent based on Accept header.
        // If Accept header is in chrome like default: Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
        // the result will sent in xml, but if the client set Accept: application/json header the result will be sent
        // in JSON format
        // With the Queryable attribute that comes from Asp.Net OData extension you can use basic OData filters like
        // $top=1 or $orderby=Name desc etc.
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

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>POST api/user</remarks>
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

        /// <summary>
        /// Updates a user based on body
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="value"></param>
        /// <remarks>PUT /api/user/66cfbd2dd52f418e88152260f24aed7c
        ///          json body: {"Name":"Zsolt Schoner", Username:"zsschoner", "IsAnonymous":"false"}
        /// </remarks>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the specified user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// PUT /api/user/66cfbd2dd52f418e88152260f24aed7c/zsschoner/Zsolt_Schoner/true
        /// </remarks>
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

        /// <summary>
        /// Removes the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>// DELETE api/user/abf959cf-9bb5-4ea0-9d34-83693aa4046a</remarks>
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

    public class AmbiguousExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var resp = actionExecutedContext.ActionContext.Response;

            resp.StatusCode = HttpStatusCode.Ambiguous;
            resp.Content = new StringContent("Ambiguous call");
            
            base.OnException(actionExecutedContext);
        }
    }
}