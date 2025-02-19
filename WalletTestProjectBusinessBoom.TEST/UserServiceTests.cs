using Moq;
using System.Collections.Generic;
using WalletTestProjectBusinessBoom.BAL.DTOs.User;
using WalletTestProjectBusinessBoom.BAL.Services;
using WalletTestProjectBusinessBoom.BAL.Services.Interfaces;
using WalletTestProjectBusinessBoom.DAL.Repositories;
using WalletTestProjectBusinessBoom.Ñore.Entities;
using WalletTestProjectBusinessBoom.Ñore.Entities.Enums;
using WalletTestProjectBusinessBoom.Ñore.Interfaces;

namespace WalletTestProjectBusinessBoom.TEST
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreaterUserAsync_ReturnsResponseUserDTO_WhenUserIsCreated()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO { Email = "test@example.com" };

            // Create a sample user that would be returned from the repository after creation.
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserDTO.Email,
                Balance = 0m
            };

            // Set up the repository mocks:
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                               .Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                               .Returns(Task.FromResult(1));
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _userService.CreateUserAsync(createUserDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.UserId);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Balance, result.Balance);
        }

        [Fact]
        public async Task GetBalanceAsync_ReturnsResponseUserBalanceDTO_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                Balance = 120.4m
            };

            // Set up the repository mocks:
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(user);

            // Act
            var result = await _userService.GetBalanceAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(user.Balance, result.Balance);
        }

        [Fact]
        public async Task MakeTransactionAsync_Deposit_UpdatesBalanceCorrectly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 45m;
            var depositAmount = 10m;

            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                Balance = 45m
            };

            // Set up the repository mocks:
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                               .Returns(Task.FromResult(1));

            var amountDTO = new AmountDTO { Amount = depositAmount };

            // Act
            var result = await _userService.MakeTransactionAsync(userId, amountDTO, KindTransaction.Deposit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(initialBalance + depositAmount, result.NewBalance);
        }

        [Fact]
        public async Task MakeTransactionAsync_Withdraw_WithInsufficientFunds_ReturnsNewBalanceButNegative()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 5m;
            var withdrawAmount = 10m;

            var user = new User
            {
                Id = userId,
                Email = "test@example.com",
                Balance = initialBalance
            };

            // Set up the repository mocks:
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                               .Returns(Task.FromResult(1));

            var amountDTO = new AmountDTO { Amount = withdrawAmount };

            // Act
            var result = await _userService.MakeTransactionAsync(userId, amountDTO, KindTransaction.Withdraw);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(initialBalance - withdrawAmount, result.NewBalance);
        }
    }
}