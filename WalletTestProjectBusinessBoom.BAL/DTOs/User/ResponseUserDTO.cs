using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.BAL.DTOs.User
{
    public class ResponseUserDTO
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public decimal Balance { get; set; }
    }
}
