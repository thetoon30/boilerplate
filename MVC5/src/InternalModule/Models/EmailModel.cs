using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalModule.Boilerplate.Models
{
    public class EmailModel
    {
        public string subject { get; set; }
        public string sender { get; set; }
        public string senderPassword { get; set; }
        public string receiver { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string newPassword { get; set; }
    }
}