using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Entities
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }

        public int StudentId { get; set; }
        [Display (Name ="Име")]
        public string StudentFirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string StudentLastName { get; set; }
        [Display(Name = "Никнейм")]
        public string StudentUsername { get; set; }
        [Display(Name = "Парола")]
        public string StudentPassword { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}