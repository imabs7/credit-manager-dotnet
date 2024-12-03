using System; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using BankApp.Models; 
using BankApp.Repositories; 
using BankApp.Services; 
using Microsoft.Extensions.Logging; 
using Moq; 
using Xunit; 
namespace BankApp.Tests.Services 
{ 
    public class CreditServiceTests 
    { 
        private readonly Mock<ICreditRepository> _creditRepoMock; 
        private readonly Mock<ICustomerRepository> _customerRepoMock; 
        private readonly Mock<IProductRepository> _productRepoMock; 
        private readonly Mock<ILogger<CreditService>> _loggerMock; 
        private readonly CreditService _creditService; 
        public CreditServiceTests() 
        { 
            _creditRepoMock = new Mock<ICreditRepository>(); 
            _customerRepoMock = new Mock<ICustomerRepository>(); 
            _productRepoMock = new Mock<IProductRepository>(); 
            _loggerMock = new Mock<ILogger<CreditService>>(); 
            _creditService = new CreditService(_creditRepoMock.Object, _customerRepoMock.Object, _productRepoMock.Object, _loggerMock.Object); 
        } 
        [Fact] 
        public async Task GetAllCreditsAsync_ReturnsAllCredits() 
        { 
            // Arrange 
            var credits = new List<Credit> 
            { 
                new Credit { Id = 1, CreditName = "Test Credit 1" }, 
                new Credit { Id = 2, CreditName = "Test Credit 2" } 
            }; 
            _creditRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(credits); 
            // Act 
            var result = await _creditService.GetAllCreditsAsync(); 
            // Assert 
            Assert.Equal(2, ((List<Credit>)result).Count); 
        } 
        [Fact] 
        public async Task CreateCreditAsync_ValidCredit_ReturnsCredit() 
        { 
            // Arrange 
            var credit = new Credit { Id = 1, CreditName = "Test Credit" }; 
            _creditRepoMock.Setup(repo => repo.AddAsync(credit)).ReturnsAsync(credit); 
            // Act 
            var result = await _creditService.CreateCreditAsync(credit); 
            // Assert 
            Assert.Equal(credit, result); 
        } 
        [Fact] 
        public async Task CreateCreditAsync_InvalidCredit_ThrowsArgumentException() 
        { 
            // Arrange 
            var invalidCredit = new Credit(); 
            // Act & Assert 
            await Assert.ThrowsAsync<ArgumentException>(() => _creditService.CreateCreditAsync(invalidCredit)); 
        } 
    } 
} 