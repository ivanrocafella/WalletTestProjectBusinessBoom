using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.Сore.Interfaces;

namespace WalletTestProjectBusinessBoom.Сore.Entities
{
    public class User : IDateFixEntity
    {
        public required Guid Id { get; set; }
        public string? Email { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
