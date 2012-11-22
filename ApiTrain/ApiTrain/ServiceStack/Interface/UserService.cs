using Data;
using System.Linq;
using ServiceStack.ServiceHost;
using ServiceStackSample.Model;

namespace ServiceStack.Interface
{
    public class UsersRestService : IRestGetService<UserListResource>,IService
    {
        private readonly IUserOperations repository_;

        public UsersRestService()
        {
            repository_ = Data.UserOperations.OperationFactory;
        }

        public object Get(UserListResource request)
        {
            return repository_.List().ToList();
        }

    }
}
