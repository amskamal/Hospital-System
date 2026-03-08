using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required(ErrorMessage = "Please enter the department Name")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(15, ErrorMessage = "Name you mustn't exceed 15 characters.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Please enter the department description ")]
        [MinLength(5, ErrorMessage = "Name mustn't be less than 5 characters.")]
        public string DedpartmentDescription { get; set; }

        // relation between Doctor table & Department table

        public ICollection<Doctor> Doctors { get; set; }
    }
}
