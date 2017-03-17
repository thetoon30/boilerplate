using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.Model.ParseObject
{
    public class InstallationObject
    {
        public string deviceType { get; set; }
        public string deviceToken { get; set; }
        public List<string> channels { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string objectId { get; set; }
    }

    public class InstallationObjectResponse
    {
        public string deviceType { get; set; }
        public string deviceToken { get; set; }
        public List<string> channels { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string objectId { get; set; }
    }

    public class InstallationCollection
    {
        public List<InstallationObjectResponse> results { get; set; }
    }
}
