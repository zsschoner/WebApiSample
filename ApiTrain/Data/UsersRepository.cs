using System;
using System.Linq;
using ServiceContracts;

namespace Data
{
    /// <summary>
    /// Makes user operation abstraction that can be called from services
    /// </summary>
    public class UsersRepository : ICrudRepository<Users>
    {
        private static readonly Data.DataContainer Container = new DataContainer();

        /// <summary>
        /// Lists users
        /// </summary>
        /// <returns></returns>
        public IQueryable<Users> List()
        {
            return Container.Users;
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Get(Guid id)
        {
            return Container.Users.First(value=>value.Id == id);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Users Create(Users value)
        {            
            Container.AddToUsers(value);
            Container.SaveChanges();
            
            return value;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Users Update(Guid id, Users value)
        {
            var result = Container.Users.First(user => user.Id == id);
            result.Name = value.Name;
            result.IsAnonymous = value.IsAnonymous;
            Container.SaveChanges();
            
            return result;
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Delete(Guid id)
        {            
            var result = Container.Users.First(user=>user.Id == id);            
            Container.DeleteObject(result);
            Container.SaveChanges();

            return result;
        }
    }
}
