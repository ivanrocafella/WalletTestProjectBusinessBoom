using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.BAL.DTOs.User
{
    public class ErrorDTO(string? error)
    {
        public string? Error { get; set; } = error;
    }
}
