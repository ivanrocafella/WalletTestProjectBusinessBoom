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

namespace WalletTestProjectBusinessBoom.BAL.Services
{
    public class UserService(UserRepository userRepository) : IUserService
    {
        public async Task<ResponseUserDTO?> CreateUser(CreateUserDTO createUserDTO)
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
            {
                ResponseUserDTO responseUserDTO = new()
                { 
                    UserId = userFromDb.Id,
                    Email = userFromDb.Email,
                    Balance = userFromDb.Balance
                };
                return responseUserDTO;
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
                return null;
            }
        }
    }
}
