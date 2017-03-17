using System;
using System.Collections.Generic;

namespace InternalModeule.Boilerplate.Core.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}