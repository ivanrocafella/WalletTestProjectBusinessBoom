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
using WalletTestProjectBusinessBoom.Сore.Entities.Enums;
using WalletTestProjectBusinessBoom.Сore.Interfaces;

namespace WalletTestProjectBusinessBoom.BAL.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private const string noUserMessage = "User not found";
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
                Console.WriteLine(noUserMessage);
                return null;
            }  
        }

        public async Task<ResponseUserBalanceDTO?> GetBalanceAsync(Guid guid)
        {
            User? user = await userRepository.GetByIdAsync(guid);
            if (user != null)
                return MakeResponseUserBalanceDTO(user);
            else
            {
                Console.WriteLine(noUserMessage);
                return null;
            }
        }

        public async Task<ResponseUserNewBalanceDTO?> MakeTransactionAsync(Guid guid, AmountDTO amountDTO, KindTransaction kindTransaction)
        {
            User? user = await userRepository.GetByIdAsync(guid);
            if (user != null)
            {
                if (kindTransaction == KindTransaction.Deposit)
                    user.Balance += amountDTO.Amount;
                else
                {
                    user.Balance -= amountDTO.Amount;
                    if (user.Balance < 0)
                        return MakeResponseUserNewBalanceDTO(user);
                }
                await userRepository.SaveChangesAsync();
                return MakeResponseUserNewBalanceDTO(user);
            }
            else
            {
                Console.WriteLine(noUserMessage);
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
