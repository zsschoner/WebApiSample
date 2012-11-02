using System.Linq;
using ServiceStack.ServiceHost;
using ApiServiceStack.ServiceModel.ValuesOperations;
using Common.Model;
using Common;
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
                result = Data.UserOperations.List();
            else
            {
                result = Data.UserOperations.Get(request.Id);
            }

            return result;
        }

        public override object OnPost(UserViewModel request)
        {
            return Data.UserOperations.Create(request);
        }

        public override object OnPut(UserViewModel request)
        {
            return Data.UserOperations.Update(request.Id, request);
        }

        public override object OnDelete(UserViewModel request)
        {
            return Data.UserOperations.Delete(request);
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
