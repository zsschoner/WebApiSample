using System;
using System.Linq;
using ServiceContracts;
using System.Transactions;

namespace Data
{
    /// <summary>
    /// Makes user operation abstraction that can be called from services
    /// </summary>
    public class UsersRepository : ICrudRepository<Users>
    {
        private readonly Data.DataContainer container_ = new DataContainer();

        /// <summary>
        /// Lists users
        /// </summary>
        /// <returns></returns>
        public IQueryable<Users> List()
        {
            return container_.Users;
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Get(Guid id)
        {
            return container_.Users.First(value => value.Id == id);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Users Create(Users value)
        {
            using (var tr = new TransactionScope())
            {
                container_.AddToUsers(value);
                container_.SaveChanges();

                tr.Complete();
            }

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
            var result = container_.Users.First(user => user.Id == id);

            using (var tr = new TransactionScope())
            {
                result.Name = value.Name;
                result.IsAnonymous = value.IsAnonymous;

                container_.SaveChanges();

                tr.Complete();
                return result;
            }

        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Delete(Guid id)
        {
            var result = container_.Users.FirstOrDefault(user => user.Id == id);
            if (result == null) return result;

            using (var tr = new TransactionScope())
            {
                container_.DeleteObject(result);
                container_.SaveChanges();
                tr.Complete();

                return result;
            }
        }
    }
}
