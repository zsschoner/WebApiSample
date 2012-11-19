using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceContracts;

namespace Data
{
    public class UsersRepository : ICrudRepository<Users>
    {
        private static readonly Data.DataContainer container = new DataContainer();

        public UsersRepository()
        {
            
        }

        public IQueryable<Users> List()
        {
            return container.Users;
        }

        public Users Get(Guid id)
        {
            return container.Users.First(value=>value.Id == id);
        }

        public Users Add(Users value)
        {            
            container.AddToUsers(value);
            container.SaveChanges();
            
            return value;
        }

        public Users Update(Guid id, Users value)
        {
            var result = container.Users.First(user => user.Id == id);
            result.Name = value.Name;
            result.IsAnonymous = value.IsAnonymous;
            container.SaveChanges();
            
            return result;
        }

        public Users Delete(Guid id)
        {            
            var result = container.Users.First(user=>user.Id == id);            
            container.DeleteObject(result);
            container.SaveChanges();

            return result;
        }
    }
}
