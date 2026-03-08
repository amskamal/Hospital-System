using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }


        [Required(ErrorMessage = "Please enter the Patient's name.")]
        [MaxLength(50, ErrorMessage = "You have enter more than the maximax size of the Patient's name.")]
        [MinLength(10, ErrorMessage = "You have enter less than the maximax size of the Patient's name.")]
        [DisplayName("Patient Full Name")]
        [Column("Patient Full Name")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Please enter the Patient age.")]
        [DisplayName("Patient's age")]
        [Column("Patient's age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        [DisplayName("Patient's phone number")]
        [Column("Patient's phone number")]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "please enter an emial.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format. Example of correct format: joe.example@example.org")]
        [DisplayName("Patient's Emial")]
        [Column("Patient's Emial")]
        public string Email { get; set; }

        public DateTime CreatingTime { get; set; }
        public DateTime UpdateDataTime { get; set; }

        // relation between visit table & Patient table

        public ICollection<Visit> Visits { get; set; }




    }
}
