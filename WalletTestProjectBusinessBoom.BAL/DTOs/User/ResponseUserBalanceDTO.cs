using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.BAL.DTOs.User
{
    public class ResponseUserBalanceDTO
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
