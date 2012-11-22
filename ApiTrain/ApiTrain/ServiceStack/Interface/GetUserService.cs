using ServiceStack.ServiceHost;
using ServiceStack.Model;

namespace ServiceStack.Interface
{
    public class GetUserService : IRestGetService<GetUserViewModel>, IService
    {
        private readonly Data.IUserOperations repository_;

        public GetUserService()
        {
            repository_ = Data.UserOperations.Instance;
        }

        public object Get(GetUserViewModel request)
        {
            return repository_.Get(request.Id);
        }
    }
}
