using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CommonServer.Model;
using System.ServiceModel.Activation;

namespace ApiWCF
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApiService : IApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>TODO: Cannot display IQueryable</remarks>
        public IEnumerable<ValueModel> Values()
        {
            return CommonServer.ValuesOperations.ListValues().AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Route parameter must be string in WCF otherwise it won't be bound</remarks>
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
        public ValueModel Put(string id, ValueModel model)
        {
            return CommonServer.ValuesOperations.UpdateValue(int.Parse(id), model);
        }

        public ValueModel Delete(string id)
        {
            return CommonServer.ValuesOperations.DeleteValue(new ValueModel() { Id = int.Parse(id) });
        }
    }
}
