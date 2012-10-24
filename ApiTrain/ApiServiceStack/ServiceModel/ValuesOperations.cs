using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace ApiServiceStack.ServiceModel.ValuesOperations
{
    #region Values

    [Route("api/values", "GET")]
    public class Values
    {

    }

    public class GetValuesResponse
    {
        public IEnumerable<string> Result { get; set; }
    }

    #endregion
}
