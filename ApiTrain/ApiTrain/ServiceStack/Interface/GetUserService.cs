using ServiceStack.ServiceHost;
using ApiServiceStack.ServiceModel.ValuesOperations;

namespace ApiServiceStack.ServiceInterface
{
    public class GetUserService : IRestGetService<GetUserViewModel>, IService
    {
        public object Get(GetUserViewModel request)
        {
            return Data.UserOperations.Get(request.Id);
        }
    }
}
