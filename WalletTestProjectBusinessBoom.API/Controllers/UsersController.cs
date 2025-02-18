using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;

namespace WalletTestProjectBusinessBoom.API.Controllers
{
    public class UsersController(IUserService userService) : ApiController
    {
        // POST: /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        { 
            ResponseUserDTO? responseUserDTO = await userService.CreateUser(createUserDTO);
            if (responseUserDTO != null)
                return Ok(responseUserDTO);
            return StatusCode(500, "The user hasn't been created");
        }
    }
}
