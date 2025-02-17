using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTestProjectBusinessBoom.Core.Entities
{
    public class User
    {
        public required string Id { get; set; }
        public string? Email { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
