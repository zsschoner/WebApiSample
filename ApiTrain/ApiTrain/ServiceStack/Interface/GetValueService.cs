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
    public class GetValueService : IRestGetService<GetValue>, IService
    {
        public object Get(GetValue request)
        {
            return CommonServer.ValuesOperations.GetValue(request.Id);
        }
    }
}
