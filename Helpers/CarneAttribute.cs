using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace ApiControlDeColegio.Helpers
{
    public class CarneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString())){
                return ValidationResult.Success;
            }

            if(!Information.IsNumeric(value.ToString()) || value.ToString().Length != 7) {
                return new ValidationResult("El carné es invalido");
            }

            return ValidationResult.Success;
        }
    }
}