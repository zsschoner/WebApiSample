using System;
using System.Collections.Generic;
using System.Linq;
using Common.Model;
using ServiceContracts;

namespace Data
{
    public interface IUserOperations
    {
        IEnumerable<UserModel> List();
        UserModel Get(Guid id);
        UserModel Create(UserModel value);
        UserModel Update(Guid id, UserModel value);
        UserModel Delete(UserModel model);
    }

    /// <summary>
    /// The instance which can be reached from services and it makes the mapping between model and viewmodel entities
    /// </summary>
    public class UserOperations : IUserOperations
    {
        /// <summary>
        /// The repository instance
        /// </summary>
        private static ICrudRepository<Users> UsersRepository;
        private static UserOperations instance;

        private static ICrudRepository<Users> CrudRepository
        {
            get { return UsersRepository ?? (UsersRepository = new UsersRepository()); }
        }

        private UserOperations()
        {
            
        }

        /// <summary>
        /// Just for the simple usage
        /// </summary>
        public static IUserOperations Instance
        {
            get
            {
                return instance ?? (instance = new UserOperations());
            }
        }

        /// <summary>
        /// Returns with list of users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> List()
        {
            return CrudRepository.List()
                                 .Select(value => new UserModel()
                                 {
                                     Id = value.Id,
                                     Name = value.Name,
                                     UserName = value.UserName,
                                     IsAnonymous = value.IsAnonymous
                                 });
        }

        /// <summary>
        /// Gets a specified user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel Get(Guid id)
        {
            var data = CrudRepository.Get(id);
            return new UserModel() { Id = data.Id, Name = data.Name, UserName = data.UserName, IsAnonymous = data.IsAnonymous };
        }

        /// <summary>
        /// Creates user based on params
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public UserModel Create(UserModel value)
        {
            var result = CrudRepository.Create(new Users()
            {
                Id = Guid.NewGuid(),
                Name = value.Name,
                UserName = value.UserName,
                IsAnonymous = value.IsAnonymous
            });

            value.Id = result.Id;

            // Created item
            return value;
        }

        /// <summary>
        /// Updates user based on params
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public UserModel Update(Guid id, UserModel value)
        {
            var result = CrudRepository.Update(id, new Users()
            {
                Id = value.Id,
                Name = value.Name,
                UserName = value.UserName,
                IsAnonymous = value.IsAnonymous
            });

            value.Id = id;

            return value;
        }

        /// <summary>
        /// Deletes user based on params
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserModel Delete(UserModel model)
        {
            var result = CrudRepository.Delete(model.Id);
            if (result != null)
            {
                model.Name = result.Name;
                model.UserName = result.UserName;
                model.IsAnonymous = result.IsAnonymous;
            }

            return model;
        }
    }
}
