using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContohWeb.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Data First Name harus diisi !")]
        [StringLength(50, ErrorMessage = "First Name tidak boleh lebih dari 50 karakter")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstMidName;
            }
        }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }

    }
}
