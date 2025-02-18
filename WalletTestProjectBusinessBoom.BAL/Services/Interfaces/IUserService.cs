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
        Task<ResponseUserDTO?> CreateUser(CreateUserDTO createUserDTO);
    }
}
