using ServiceStack.Model;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Interface
{
    /// <summary>
    /// ServiceStack implementation for getting a specified user
    /// </summary>
    /// <remarks>
    /// ServiceStack rest doesn't allow the method overloading so one service can have only one Get,.... method
    /// so you have to 
    /// </remarks>
    public class UserService : RestServiceBase<UserResouce>
    {
        private readonly Data.IUserOperations repository_;

        public UserService()
        {
            repository_ = Data.UserOperations.Instance;
        }

        public override object OnGet(UserResouce request)
        {
            return repository_.Get(request.Id);
        }

        public override object OnPost(UserResouce request)
        {
            return repository_.Create(request);
        }

        public override object OnPut(UserResouce request)
        {
            return repository_.Update(request.Id, request);
        }

        public override object OnDelete(UserResouce request)
        {
            return repository_.Delete(request);
        }
    }
}
