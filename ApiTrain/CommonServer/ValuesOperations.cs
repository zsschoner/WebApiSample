using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonServer.Model;
using Data;
using ServiceContracts;

namespace CommonServer
{
    public static class UserOperations
    {
        private static ICrudRepository<Users> usersRepository;

        private static ICrudRepository<Users> CrudRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UsersRepository();
                }

                return usersRepository;
            }
        }

        public static IEnumerable<UserModel> ListValues()
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

        public static UserModel GetValue(Guid id)
        {
            var data = CrudRepository.Get(id);
            return new UserModel() { Id = data.Id, Name = data.Name };
        }

        public static UserModel CreateValue(UserModel value)
        {
            var result = CrudRepository.Add(new Users()
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

        public static UserModel UpdateValue(Guid id, UserModel value)
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

        public static UserModel DeleteValue(UserModel model)
        {
            var result = CrudRepository.Delete(model.Id);
            model.Name = result.Name;
            model.UserName = result.UserName;
            model.IsAnonymous = result.IsAnonymous;

            return model;
        }
    }
}
