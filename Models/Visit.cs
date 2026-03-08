using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }

        [Required(ErrorMessage = "Please enter the visiting time.")]
        [DisplayName("Visiting Time")]
        [Column("Visiting Time")]
        public DateTime VisitingTime { get; set; }

        [Required(ErrorMessage = "Please enter the prescription.")]
        public string Prescription { get; set; }

        [Required(ErrorMessage ="please enter the check Up fee.")]
        public double VisitcheckUpFree { get; set; }

        [ForeignKey("Patient")]
        [DisplayName("Patient")]
        [Column("Patient ID")]
        [Required(ErrorMessage = "Please select the Visit.")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        [DisplayName("Doctor")]
        [Column("Doctor ID")]
        [Required(ErrorMessage = "Please select the Doctor.")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("InsuranceCompany")]
        [DisplayName("Insurance Company")]
        [Column("Insurance Company")]
        public int InsuranceCompanyID { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }

    }
}
