using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctor
    {
        [Key]
        public int DocId { get; set; }


        [Required(ErrorMessage = "Please enter the Doctor's name.")]
        [MaxLength(50, ErrorMessage = "You have enter more than the maximax size of the Doctor's name.")]
        [MinLength(10, ErrorMessage = "You have enter less than the maximax size of the Doctor's name.")]
        [DisplayName("Doctor Full Name")]
        [Column("Doctor Full Name")]
        public string DoctorName { get; set; }


        [Required(ErrorMessage = "The degree is must.")]
        [DisplayName("Doctor's Degree")]
        [Column("Doctor's Degree")]
        public string DoctorScienceDegrees { get; set; }

        [DataType(DataType.Date)]
        public string DegreeDate { get; set; }

        [Required(ErrorMessage = "Please enter the day when the Doctor will be available.")]
        [DisplayName("Doctor's days")]
        [Column("Doctor's days")]
        public string DoctorDaysPerWeek { get; set; }

        [Required(ErrorMessage = "Please enter the time when the Doctor will be available.")]
        [DisplayName("Doctor's time")]
        [Column("Doctor's time")]
        [DataType(DataType.Time)]
        public DateTime DoctorTimePerDay { get; set; }

        [Required(ErrorMessage = "The fee is must")]
        [Range(0, 1500, ErrorMessage = "The check up fee must be in range between 0 and 1500.")]
        [DisplayName("Check Up Fee")]
        [Column("Check Up Fee")]
        public double CheckUpFee { get; set; }

        [Required(ErrorMessage = "If there is no additional fee, please enter a zero value.")]
        [Range(0, double.MaxValue)]  // this means that if the user enter a 0 value will be accepted, and if enter a value till infinity will be                                  accepted too, according to the needs and the codition of the patient.
        [DisplayName("Addotional Fee")]
        [Column("Addotional Fee")]
        public double AdditionalFee { get; set; }


        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        [DisplayName("Doctor's phone number 1")]
        [Column("Doctor's phone number 1")]
        public string PhoneNo1 { get; set; }


        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        [DisplayName("Doctor's phone number 2")]
        [Column("Doctor's phone number 2")]
        public string PhoneNo2 { get; set; }


        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DisplayName("Doctor's Email")]
        [Column("Doctor's Email")]
        public string Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "please enter an Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [Compare("Email", ErrorMessage = "the email and the conformed email does not match.")]
        public string ConfirmEmaill { get; set; }


        [DataType(DataType.Date)]
        [DoctorLegalAge(ErrorMessage = "Illegal Doctor age.")]
        public DateTime DoctorBirthDate { get; set; }

        public DateTime CreatingTime { get; set; }
        public DateTime UpdateDataTime { get; set; }

        // relation between Doctor table & Department table
        [ForeignKey("Department")]
        [DisplayName("Department")]
        [Column("Department ID")]
        [Range(1, int.MaxValue, ErrorMessage = ("Please select a department."))]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }      //Navigation property

        // relation between Doctor table & visit table

        public ICollection<Visit> Visits { get; set; }




    }
}
