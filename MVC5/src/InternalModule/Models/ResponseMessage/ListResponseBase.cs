using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalModule.Boilerplate.Models.ResponseMessage
{

        public class ListResponseBase<T> where T : class
        {
            public string userMessage { get; set; }
            public string devMessage { get; set; }
            public int statusCode { get; set; }
            public PageProperty page { get; set; }
            public IEnumerable<T> items { get; set; }
            public int total { get; set; }
    }
    
}