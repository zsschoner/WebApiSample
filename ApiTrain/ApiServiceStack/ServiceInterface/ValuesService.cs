using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ApiServiceStack.ServiceModel;
using ApiServiceStack.ServiceModel.ValuesOperations;

namespace ApiServiceStack.ServiceInterface
{
    public class ValuesService : IService<Values>
    {
        public object Execute(Values request)
        {
            return new GetValuesResponse() { Result = GetValues() };
        }

        private IEnumerable<string> GetValues()
        {
            return CommonServer.ValuesOperations.GetValues();
        }
    }
}
