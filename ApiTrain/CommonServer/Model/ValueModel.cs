using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace CommonServer.Model
{
    public class ValueModel
    {        
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
