using System;
using System.Collections.Generic;
using System.ServiceModel;
using Common.Model;
using System.ServiceModel.Activation;

namespace WCF
{
    /// <summary>
    /// Wcf REST sample
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Wcf : IWcf
    {
        private readonly Data.IUserOperations repository_;
        
        public Wcf()
        {
            repository_ = Data.UserOperations.Instance;
        }

        /// <summary>
        /// Gets the list of users
        /// </summary>
        /// <returns></returns>
        /// <remarks>Can't call two contract with the same name so this is the list getter</remarks>
        public IEnumerable<UserModel> GetList()
        {
            return repository_.List();
        }

        /// <summary>
        /// The get request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Route parameter must be string in WCF otherwise it won't be bound</remarks>
        public UserModel Get(string id)
        {
            return repository_.Get(Guid.Parse(id));
        }

        /// <summary>
        /// The post request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserModel Post(UserModel model)
        {
            return repository_.Create(model);
        }

        /// <summary>
        /// The put request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserModel Put(string id, UserModel model)
        {
            return repository_.Update(Guid.Parse(id), model);
        }

        /// <summary>
        /// The put request with URI parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <param name="isAnonymous"></param>
        /// <returns></returns>
        public UserModel PutUri(string id, string username, string name, string isAnonymous)
        {
            return Put(id, new UserModel() { UserName = username, Name = name, IsAnonymous = bool.Parse(isAnonymous) });
        }

        /// <summary>
        /// The Delete request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel Delete(string id)
        {
            return repository_.Delete(new UserModel() { Id = Guid.Parse(id) });
        }
    }
}
