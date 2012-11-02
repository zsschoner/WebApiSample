using System.Linq;
using ServiceStack.ServiceHost;
using ApiServiceStack.ServiceModel.ValuesOperations;
using CommonServer.Model;
using CommonServer;
using System;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

namespace ApiServiceStack.ServiceInterface
{
    public class GetUserService : IRestGetService<GetUserViewModel>, IService
    {
        public object Get(GetUserViewModel request)
        {
            return CommonServer.UserOperations.GetValue(request.Id);
        }
    }
}
