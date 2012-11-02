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
    public class UserRestService : RestServiceBase<UserViewModel>
    {
        public override object OnGet(UserViewModel request)
        {
            object result = null;
            if (request == null || request.Id == Guid.Empty)
                result = CommonServer.UserOperations.ListValues();
            else
            {
                result = CommonServer.UserOperations.GetValue(request.Id);
            }

            return result;
        }

        public override object OnPost(UserViewModel request)
        {
            return CommonServer.UserOperations.CreateValue(request);
        }

        public override object OnPut(UserViewModel request)
        {
            return CommonServer.UserOperations.UpdateValue(request.Id, request);
        }

        public override object OnDelete(UserViewModel request)
        {
            return CommonServer.UserOperations.DeleteValue(request);
        }

        public override string ServiceName
        {
            get
            {
                return "User service";
            }
        }
    }
}
