using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;

namespace WalletTestProjectBusinessBoom.BAL.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUserDTO?> CreateUserAsync(CreateUserDTO createUserDTO);
        Task<ResponseUserBalanceDTO?> GetBalanceAsync(Guid guid);
        Task<ResponseUserNewBalanceDTO?> MakeDepositAsync(Guid guid, AmountDepositDTO amountDepositDTO);
    }
}
