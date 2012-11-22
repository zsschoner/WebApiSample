using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Model;
using System.Runtime.Serialization;

namespace HttpClientTest
{
    [DataContract(Name="UserModel")]
    public class UserResource : UserModel
    {
    }
}
