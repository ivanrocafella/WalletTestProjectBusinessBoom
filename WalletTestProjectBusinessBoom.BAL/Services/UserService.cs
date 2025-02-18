using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;
using WalletTestProjectBusinessBoom.DAL.Repositories;
using WalletTestProjectBusinessBoom.Сore.Entities;
using WalletTestProjectBusinessBoom.Сore.Interfaces;

namespace WalletTestProjectBusinessBoom.BAL.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<ResponseUserDTO?> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            User user = new()
            { 
                Id = Guid.NewGuid(), 
                Email = createUserDTO.Email
            };
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            User? userFromDb = await userRepository.GetByIdAsync(user.Id);
            if (userFromDb != null)
                return MakeResponseUserDTO(userFromDb);
            else
            {
                Console.WriteLine("User not found");
                return null;
            }

            
        }

        public async Task<ResponseUserBalanceDTO?> GetBalanceAsync(Guid guid)
        {
            User? userFromDb = await userRepository.GetByIdAsync(guid);
            if (userFromDb != null)
                return MakeResponseUserBalanceDTO(userFromDb);
            else
            {
                Console.WriteLine("User not found");
                return null;
            }
        }

        public async Task<ResponseUserNewBalanceDTO?> MakeDepositAsync(Guid guid, AmountDepositDTO amountDepositDTO)
        {
            User? userFromDb = await userRepository.GetByIdAsync(guid);
            if (userFromDb != null)
            {
                ResponseUserNewBalanceDTO responseUserNewBalanceDTO = MakeResponseUserNewBalanceDTO(userFromDb);
                responseUserNewBalanceDTO.NewBalance += amountDepositDTO.Amount;
                return responseUserNewBalanceDTO;            
            }
            else
            {
                Console.WriteLine("User not found");
                return null;
            }
        }

        public static ResponseUserDTO MakeResponseUserDTO(User user)
        {
            ResponseUserDTO responseUserDTO = new()
            {
                UserId = user.Id,
                Email = user.Email,
                Balance = user.Balance
            };
            return responseUserDTO;
        }

        public static ResponseUserBalanceDTO MakeResponseUserBalanceDTO(User user)
        {
            ResponseUserBalanceDTO responseUserBalanceDTO = new()
            {
                UserId = user.Id,
                Balance = user.Balance
            };
            return responseUserBalanceDTO;
        }

        public static ResponseUserNewBalanceDTO MakeResponseUserNewBalanceDTO(User user)
        {
            ResponseUserNewBalanceDTO responseUserNewBalanceDTO = new()
            {
                UserId = user.Id,
                NewBalance = user.Balance
            };
            return responseUserNewBalanceDTO;
        }
    } 
}
