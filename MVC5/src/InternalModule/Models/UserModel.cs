using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalModule.Boilerplate.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string EmployeeCode { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceId { get; set; }
        public string Role { get; set; }
    }
}