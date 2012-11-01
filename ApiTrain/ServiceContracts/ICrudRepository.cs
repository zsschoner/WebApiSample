using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceContracts
{
    public interface ICrudRepository<TEntity>
    {
        IQueryable<TEntity> ListValues();
        TEntity GetValue(int id);
        TEntity AddValue(TEntity value);
        TEntity UpdateValue(int id, TEntity valueClass);
        TEntity DeleteValue(int id);
    }
}
