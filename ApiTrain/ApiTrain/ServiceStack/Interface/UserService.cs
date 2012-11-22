using System;
using ServiceStack.ServiceInterface;
using ServiceStack.Model;
using Data;

namespace ServiceStack.Interface
{
    public class UserRestService : RestServiceBase<UserViewModel>
    {
        private readonly IUserOperations repository_;

        public UserRestService()
        {
            repository_ = Data.UserOperations.Instance;
        } 

        public override object OnGet(UserViewModel request)
        {
            object result = null;
            if (request == null || request.Id == Guid.Empty)
                result = repository_.List();
            else
            {
                result = repository_.Get(request.Id);
            }

            return result;
        }

        public override object OnPost(UserViewModel request)
        {
            return repository_.Create(request);
        }

        public override object OnPut(UserViewModel request)
        {
            return repository_.Update(request.Id, request);
        }

        public override object OnDelete(UserViewModel request)
        {
            return repository_.Delete(request);
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
