using System;
using System.Linq;

namespace ServiceContracts
{
    /// <summary>
    /// Interface for a repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICrudRepository<TEntity>
    {
        IQueryable<TEntity> List();
        TEntity Get(Guid id);
        TEntity Create(TEntity value);
        TEntity Update(Guid id, TEntity value);
        TEntity Delete(Guid id);
    }
}
