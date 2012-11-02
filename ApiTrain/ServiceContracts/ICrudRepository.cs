using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceContracts
{
    public interface ICrudRepository<TEntity>
    {
        IQueryable<TEntity> List();
        TEntity Get(Guid id);
        TEntity Add(TEntity value);
        TEntity Update(Guid id, TEntity value);
        TEntity Delete(Guid id);
    }
}
