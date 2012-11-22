using System;
using ServiceStack.Model;
using Data;
using ServiceStack.ServiceHost;

namespace ServiceStack.Interface
{
    public class UsersRestService : IRestGetService<UserListResource>,IService
    {
        private readonly IUserOperations repository_;

        public UsersRestService()
        {
            repository_ = Data.UserOperations.Instance;
        }

        public object Get(UserListResource request)
        {
            return repository_.List();
        }

    }
}
