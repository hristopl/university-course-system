using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Entities
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        public int CourseId { get; set; }
        [Display(Name = "Име на курса")]
        public string CourseName { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}