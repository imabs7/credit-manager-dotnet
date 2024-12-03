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
    public class CustomerServiceTests 
    { 
        private readonly Mock<ICustomerRepository> _customerRepoMock; 
        private readonly Mock<ILogger<CustomerService>> _loggerMock; 
        private readonly CustomerService _customerService; 
        public CustomerServiceTests() 
        { 
            _customerRepoMock = new Mock<ICustomerRepository>(); 
            _loggerMock = new Mock<ILogger<CustomerService>>(); 
            _customerService = new CustomerService(_customerRepoMock.Object, _loggerMock.Object); 
        } 
        [Fact] 
        public async Task GetAllCustomersAsync_ReturnsAllCustomers() 
        { 
            // Arrange 
            var customers = new List<Customer> 
            { 
                new Customer { Id = 1, FirstName = "John", LastName = "Doe" }, 
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith" } 
            }; 
            _customerRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers); 
            // Act 
            var result = await _customerService.GetAllCustomersAsync(); 
            // Assert 
            Assert.Equal(2, ((List<Customer>)result).Count); 
        } 
        [Fact] 
        public async Task CreateCustomerAsync_ValidCustomer_ReturnsCustomer() 
        { 
            // Arrange 
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe" }; 
            _customerRepoMock.Setup(repo => repo.AddAsync(customer)).ReturnsAsync(customer); 
            // Act 
            var result = await _customerService.CreateCustomerAsync(customer); 
            // Assert 
            Assert.Equal(customer, result); 
        } 
    } 
} 