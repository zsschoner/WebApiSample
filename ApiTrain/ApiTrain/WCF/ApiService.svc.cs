using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CommonServer.Model;

namespace ApiWCF
{
    [KnownType(typeof(ValueModel))]
    [KnownType(typeof(IEnumerable<ValueModel>))]
    public class ApiService : IApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>TODO: Cannot display IQueryable</remarks>
        public IEnumerable<ValueModel> Values()
        {
            return CommonServer.ValuesOperations.ListValues().Select(vs => new ValueModel() { Id = vs.Id, Name = vs.Name });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Route parameter must be string in WCF</remarks>
        public ValueModel Get(string id)
        {
            return CommonServer.ValuesOperations.GetValue(int.Parse(id));
        }

        public ValueModel Post(ValueModel model)
        {
            return CommonServer.ValuesOperations.CreateValue(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>Query string parameter can be int, long etc.</remarks>
        public ValueModel Put(int id, ValueModel model)
        {
            return CommonServer.ValuesOperations.UpdateValue(id, model);
        }

        public ValueModel Delete(ValueModel model)
        {
            return CommonServer.ValuesOperations.DeleteValue(model);
        }
    }
}
