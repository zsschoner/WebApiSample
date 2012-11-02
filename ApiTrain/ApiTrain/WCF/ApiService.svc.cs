using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Common.Model;
using System.ServiceModel.Activation;

namespace ApiWCF
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApiService : IApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>TODO: Cannot display IQueryable</remarks>
        public IEnumerable<UserModel> Values()
        {
            return Data.UserOperations.List();//.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Route parameter must be string in WCF otherwise it won't be bound</remarks>
        public UserModel Get(string id)
        {
            return Data.UserOperations.Get(Guid.Parse(id));
        }

        public UserModel Post(UserModel model)
        {
            return Data.UserOperations.Create(model);
        }

        public UserModel Put(string id, UserModel model)
        {
            return Data.UserOperations.Update(Guid.Parse(id), model);
        }

        public UserModel PutUri(string id, string username, string name, string isAnonymous)
        {
            return Put(id, new UserModel() { UserName = username, Name = name, IsAnonymous = bool.Parse(isAnonymous) });
        }

        public UserModel Delete(string id)
        {
            return Data.UserOperations.Delete(new UserModel() { Id = Guid.Parse(id) });
        }
    }
}
