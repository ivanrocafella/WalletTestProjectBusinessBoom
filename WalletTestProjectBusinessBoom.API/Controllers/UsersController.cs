using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;
using WalletTestProjectBusinessBoom.Сore.Entities;

namespace WalletTestProjectBusinessBoom.API.Controllers
{
    public class UsersController(IUserService userService) : ApiController
    {
        private const string routeGetBalanceAsync = "{userId}/balance";
        private const string routeMakeDeposit = "{userId}/deposit";
        private const string noUserMessage = "User not found";
        private const string noCreatedUserMessage = "The user hasn't been created";

        // POST: /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return StatusCode(500, new ErrorDTO(ModelState.Values.SelectMany(e => e.Errors).Select(em => em.ErrorMessage).First()));
            else
            {
                ResponseUserDTO? responseUserDTO = await userService.CreateUserAsync(createUserDTO);
                if (responseUserDTO != null)
                    return Ok(responseUserDTO);
                return StatusCode(500, new ErrorDTO(noCreatedUserMessage));
            }
                
        }

        // GET: /api/users/{userId}/balance
        [HttpGet(routeGetBalanceAsync)]
        public async Task<IActionResult> GetBalanceAsync(Guid userId)
        {
            ResponseUserBalanceDTO? responseUserBalanceDTO = await userService.GetBalanceAsync(userId);
            if (responseUserBalanceDTO != null)
                return Ok(responseUserBalanceDTO);
            else
                return StatusCode(500, new ErrorDTO(noUserMessage));
        }

        // POST: /api/users/{userId}/deposit
        [HttpPost(routeMakeDeposit)]
        public async Task<IActionResult> MakeDeposit(Guid userId, [FromBody] AmountDepositDTO amountDepositDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseUserNewBalanceDTO? responseUserNewBalanceDTO = await userService.MakeDepositAsync(userId, amountDepositDTO);
                if (responseUserNewBalanceDTO != null)
                    return Ok(responseUserNewBalanceDTO);
                else
                    return StatusCode(500, new ErrorDTO(noUserMessage));
            }
            else
                return StatusCode(500, new ErrorDTO(ModelState.Values.SelectMany(e => e.Errors).Select(em => em.ErrorMessage).First()));
        }
    }
}
