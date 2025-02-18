using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.BAL.Validators
{
    public class NonNegativeDecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal decimalValue)
            {
                if (decimalValue <= 0)
                    return new ValidationResult(ErrorMessage ?? "The value must be non-negative or greater than 0.");
            }
            return ValidationResult.Success;
        }
    }
}
