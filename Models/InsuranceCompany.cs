using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class InsuranceCompany
    {
        [Key]
        public int InsuranceCompanyId { get; set; }

        [Required(ErrorMessage = "Please enter the Company name.")]
        [DisplayName("Name")]
        [Column("Insurance Company Name")]
        public string InsuranceCompanyName { get; set; }

        [Required(ErrorMessage = "Please enter the Discount Percentage.")]
        [DisplayName("Discount Percentage")]
        [Column("Discount Percentage")]
        public double DiscountPercentage { get; set; }

        public ICollection<Visit> Visits { get; set; }




    }
}
