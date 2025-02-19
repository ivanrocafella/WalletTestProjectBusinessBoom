using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.BAL.Validators;

namespace WalletTestProjectBusinessBoom.BAL.DTOs.User
{
    public class AmountDTO
    {
        [NonNegativeDecimal(ErrorMessage = "Amount must be a non-negative number.")]
        public decimal Amount { get; set; } 
    }
}
