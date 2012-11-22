using ServiceStack.ServiceInterface;
using ServiceStackSample.Model;

namespace ServiceStackSample.Interface
{
    /// <summary>
    /// ServiceStack implementation for getting a specified user
    /// </summary>
    /// <remarks>
    /// ServiceStack rest doesn't allow the method overloading so one service can have only one Get,.... method
    /// so you have to 
    /// </remarks>
    public class UserService : RestServiceBase<UserResource>
    {
        private readonly Data.IUserOperations repository_;

        public UserService()
        {
            repository_ = Data.UserOperations.OperationFactory;
        }

        public override object OnGet(UserResource request)
        {
            return repository_.Get(request.Id);
        }

        public override object OnPost(UserResource request)
        {
            return repository_.Create(request);
        }

        public override object OnPut(UserResource request)
        {
            return repository_.Update(request.Id, request);
        }

        public override object OnDelete(UserResource request)
        {
            return repository_.Delete(request);
        }
    }
}
