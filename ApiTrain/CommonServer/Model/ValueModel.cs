using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace CommonServer.Model
{
    [DataContract]      
    public class ValueModel
    {
        [DataMember]        
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
