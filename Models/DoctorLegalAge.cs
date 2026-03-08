using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class DoctorLegalAge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Doctor doctor = (Doctor)validationContext.ObjectInstance;
            TimeSpan timeSpan = DateTime.Now - doctor.DoctorBirthDate;
            double DoctorAge = timeSpan.TotalDays / 365;

            if (DoctorAge < 25)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
