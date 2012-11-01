using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonServer.Model;
using Data;
using ServiceContracts;

namespace CommonServer
{
    public static class ValuesOperations
    {
        private static ICrudRepository<ValueSet> valuesRepository;

        private static ICrudRepository<ValueSet> CrudRepository
        {
            get
            {
                if (valuesRepository == null)
                {
                    valuesRepository = new ValueRepository();
                }

                return valuesRepository;
            }
        }

        public static IEnumerable<ValueModel> ListValues()
        {
            return CrudRepository.ListValues().Select(value => new ValueModel() { Id = value.Id, Name = value.Name });
        }

        public static ValueModel GetValue(int id)
        {
            var data = CrudRepository.GetValue(id);
            return new ValueModel() { Id = data.Id, Name = data.Name };
        }

        public static ValueModel CreateValue(ValueModel value)
        {
            var result = CrudRepository.AddValue(new ValueSet() { Name = value.Name });
            value.Id = result.Id;

            // Created item
            return value;
        }

        public static ValueModel UpdateValue(int id, ValueModel value)
        {
            var result = CrudRepository.UpdateValue(id, new ValueSet() { Id = value.Id, Name = value.Name });

            return value;
        }

        public static ValueModel DeleteValue(ValueModel model)
        {
            var result = CrudRepository.DeleteValue(model.Id);
            return new ValueModel() { Id = result.Id, Name = result.Name };
        }
    }
}
