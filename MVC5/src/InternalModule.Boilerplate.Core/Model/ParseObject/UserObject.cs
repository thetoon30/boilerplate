using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Model.ParseObject
{
    public class UserObjectResponse
    {
        public List<UserObject> results { get; set; }
    }

    public class UserObject
    {
        public string createdAt { get; set; }
        public string objectId { get; set; }
        public string updatedAt { get; set; }
        public string username { get; set; }
    }


}
