using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InternalModule.Boilerplate.Models
{
    public class Person
    {
        [DisplayName("Number")]
        public int MyProperty { get; set; }

        [DisplayName("Integer")]
        public int MyProperty2 { get; set; }

        [Required(ErrorMessage = "My Property3 is required")]
        public string MyProperty3 { get; set; }

        [StringLength(30)]
        public string MyProperty4 { get; set; }

        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }
    }
}