using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ApiWCF
{
    public class ApiService : IApiService
    {
        public IEnumerable<string> Values()
        {
            return CommonServer.ValuesOperations.GetValues();
        }
    }
}
