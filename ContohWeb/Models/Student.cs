using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContohWeb.Models
{
    public class Student
    {
        [Display(Name ="Student ID")]
        public int StudentID { get; set; }

        [Display(Name ="Last Name")]
        [Required(ErrorMessage ="Data Last Name harus diisi !")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage ="Data First Name harus diisi !")]
        public string FirstMidName { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
