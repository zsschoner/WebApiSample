using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceContracts;

namespace Data
{
    public class ValueRepository : ICrudRepository<ValueSet>
    {
        private static readonly Data.DataContainer container = new DataContainer();

        public ValueRepository()
        {
            
        }

        public IQueryable<ValueSet> ListValues()
        {
            return container.ValueSet;
        }

        public ValueSet GetValue(int id)
        {
            return container.ValueSet.First(value=>value.Id == id);
        }

        public ValueSet AddValue(ValueSet value)
        {            
            container.AddToValueSet(value);
            container.SaveChanges();
            
            return value;
        }

        public ValueSet UpdateValue(int id, ValueSet valueClass)
        {
            var result = container.ValueSet.First(val => val.Id == id);
            result.Name = valueClass.Name;
            container.SaveChanges();
            
            return result;
        }

        public ValueSet DeleteValue(int id)
        {            
            var result = container.ValueSet.First(val=>val.Id == id);            
            container.DeleteObject(result);
            container.SaveChanges();

            return result;
        }
        
    }
}
