using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletTestProjectBusinessBoom.BAL.DTOs;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;
using WalletTestProjectBusinessBoom.Сore.Entities;
using WalletTestProjectBusinessBoom.Сore.Entities.Enums;

namespace WalletTestProjectBusinessBoom.API.Controllers
{
    public class UsersController(IUserService userService, ILogger<UsersController> logger) : ApiController
    {
        private const string routeGetBalanceAsync = "{userId}/balance";
        private const string routeMakeDeposit = "{userId}/deposit";
        private const string routeMakeWithdraw = "{userId}/withdraw";
        private const string noUserMessage = "User not found";
        private const string noCreatedUserMessage = "The user hasn't been created";
        private readonly string noFunds = "Insufficient funds";

        // POST: /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(e => e.Errors)
                                                .Select(em => em.ErrorMessage)
                                                .First();
                logger.LogWarning("Invalid model state in CreateUserAsync: {ErrorMessage}", errorMessage);
                return StatusCode(500, new ErrorDTO(errorMessage));
            }
            else
            {
                ResponseUserDTO? responseUserDTO = await userService.CreateUserAsync(createUserDTO);
                if (responseUserDTO != null)
                {
                    logger.LogInformation("User created successfully with id {UserId}", responseUserDTO.UserId);
                    return Ok(responseUserDTO);
                }
                logger.LogError("Failed to create user. {NoCreatedUserMessage}", noCreatedUserMessage);
                return StatusCode(500, new ErrorDTO(noCreatedUserMessage));
            }
                
        }

        // GET: /api/users/{userId}/balance
        [HttpGet(routeGetBalanceAsync)]
        public async Task<IActionResult> GetBalanceAsync(Guid userId)
        {
            ResponseUserBalanceDTO? responseUserBalanceDTO = await userService.GetBalanceAsync(userId);
            if (responseUserBalanceDTO != null)
            {
                logger.LogInformation("Retrieved balance for user {UserId}: {Balance}", userId, responseUserBalanceDTO.Balance);
                return Ok(responseUserBalanceDTO);
            }
            else
            {
                logger.LogWarning("User {UserId} not found when retrieving balance", userId);
                return StatusCode(500, new ErrorDTO(noUserMessage));
            }
        }

        // POST: /api/users/{userId}/deposit
        [HttpPost(routeMakeDeposit)]
        public async Task<IActionResult> MakeDeposit(Guid userId, [FromBody] AmountDTO amountDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseUserNewBalanceDTO? responseUserNewBalanceDTO = await userService.MakeTransactionAsync(userId, amountDTO, KindTransaction.Deposit);
                if (responseUserNewBalanceDTO != null)
                {
                    logger.LogInformation("Deposit made for user {UserId}. New balance: {NewBalance}", userId, responseUserNewBalanceDTO.NewBalance);
                    return Ok(responseUserNewBalanceDTO);
                }
                logger.LogWarning("User {UserId} not found when making deposit", userId);
                return StatusCode(500, new ErrorDTO(noUserMessage));
            }
            else
            {
                var errorMessage = ModelState.Values.SelectMany(e => e.Errors)
                                                    .Select(em => em.ErrorMessage)
                                                    .First();
                logger.LogWarning("Invalid model state in MakeDeposit: {ErrorMessage}", errorMessage);
                return StatusCode(500, new ErrorDTO(errorMessage));
            }
        }

        // POST: /api/users/{userId}/withdraw
        [HttpPost(routeMakeWithdraw)]
        public async Task<IActionResult> MakeWithdraw(Guid userId, [FromBody] AmountDTO amountDTO)
        {
            if (ModelState.IsValid)
            {
                ResponseUserNewBalanceDTO? responseUserNewBalanceDTO = await userService.MakeTransactionAsync(userId, amountDTO, KindTransaction.Withdraw);
                if (responseUserNewBalanceDTO != null)
                {
                    if (responseUserNewBalanceDTO.NewBalance >= 0)
                    {
                        logger.LogInformation("Withdraw made for user {UserId}. New balance: {NewBalance}", userId, responseUserNewBalanceDTO.NewBalance);
                        return Ok(responseUserNewBalanceDTO);
                    }
                    else
                    {
                        logger.LogWarning("Insufficient funds for user {UserId}. An attempt to withdraw funds will result in a negative balance: {NewBalance}", userId, responseUserNewBalanceDTO.NewBalance);
                        return StatusCode(500, new ErrorDTO(noFunds));
                    }
                }
                else
                {
                    logger.LogWarning("User {UserId} not found when making withdrawal", userId);
                    return StatusCode(500, new ErrorDTO(noUserMessage));
                }
            }
            else
            {
                var errorMessage = ModelState.Values.SelectMany(e => e.Errors)
                                                .Select(em => em.ErrorMessage)
                                                .First();
                logger.LogWarning("Invalid model state in MakeWithdraw: {ErrorMessage}", errorMessage);
                return StatusCode(500, new ErrorDTO(ModelState.Values.SelectMany(e => e.Errors).Select(em => em.ErrorMessage).First()));
            }
        }
    }
}
