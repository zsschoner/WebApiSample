using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace Common.Model
{
    // User model for service operations
    [DataContract]
    public class UserModel
    {
        // Id of the user
        [DataMember]
        public Guid Id { get; set; }
        // For user information display
        public string DisplayName
        {
            get
            {
                return String.Format("{0} ({1})", this.UserName, this.Name);
            }
        }
        // UserName for login
        [DataMember]
        public string UserName { get; set; }
        // Fullname of the user
        [DataMember]
        public string Name { get; set; }
        // User has any permission or not
        [DataMember]
        public bool IsAnonymous { get; set; }
    }
}
