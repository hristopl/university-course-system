using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Display(Name = "Име на учителя")]
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherUsername { get; set; }
        public string TeacherPassword { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}