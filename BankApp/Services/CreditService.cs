using System; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using BankApp.Models; 
using BankApp.Repositories; 
using Microsoft.Extensions.Logging; 
namespace BankApp.Services 
{ 
    public class CreditService 
    { 
        private readonly ICreditRepository _creditRepository; 
        private readonly ICustomerRepository _customerRepository; 
        private readonly IProductRepository _productRepository; 
        private readonly ILogger<CreditService> _logger; 
        public CreditService(ICreditRepository creditRepository, ICustomerRepository customerRepository, IProductRepository productRepository, ILogger<CreditService> logger) 
        { 
            _creditRepository = creditRepository; 
            _customerRepository = customerRepository; 
            _productRepository = productRepository; 
            _logger = logger; 
        } 
        public async Task<IEnumerable<Credit>> GetAllCreditsAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all credits."); 
                return await _creditRepository.GetAllAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching all credits."); 
                throw; 
            } 
        } 
        public async Task<Credit> CreateCreditAsync(Credit credit) 
        { 
            try 
            { 
                if (!ValidateCredit(credit)) 
                { 
                    _logger.LogWarning("Credit validation failed."); 
                    throw new ArgumentException("Invalid credit data."); 
                } 
                _logger.LogInformation("Creating a new credit."); 
                return await _creditRepository.AddAsync(credit); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new credit."); 
                throw; 
            } 
        } 
        private bool ValidateCredit(Credit credit) 
        { 
            return !string.IsNullOrEmpty(credit.CreditName) && 
                   !string.IsNullOrEmpty(credit.Product?.ProductName) && 
                   credit.Product?.ProductValue > 0 && 
                   !string.IsNullOrEmpty(credit.Customer?.FirstName) && 
                   !string.IsNullOrEmpty(credit.Customer?.LastName) && 
                   credit.Customer?.PeselNumber.ToString().Length == 11; 
        } 
        public async Task<Credit> GetCreditByIdAsync(int id) 
        { 
            try 
            { 
                _logger.LogInformation($"Fetching credit with ID: {id}"); 
                var credit = await _creditRepository.GetByIdAsync(id); 
                if (credit == null) 
                { 
                    throw new KeyNotFoundException($"Credit with ID {id} not found."); 
                } 
                return credit; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"An error occurred while fetching credit with ID: {id}"); 
                throw; 
            } 
        } 
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all customers."); 
                return await _customerRepository.GetAllAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching all customers."); 
                throw; 
            } 
        } 
        public async Task<Customer> CreateCustomerAsync(Customer customer) 
        { 
            try 
            { 
                _logger.LogInformation("Creating a new customer."); 
                return await _customerRepository.AddAsync(customer); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new customer."); 
                throw; 
            } 
        } 
        public async Task<IEnumerable<Product>> GetAllProductsAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all products."); 
                return await _productRepository.GetAllAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching all products."); 
                throw; 
            } 
        } 
        public async Task<Product> CreateProductAsync(Product product) 
        { 
            try 
            { 
                _logger.LogInformation("Creating a new product."); 
                return await _productRepository.AddAsync(product); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new product."); 
                throw; 
            } 
        } 
    } 
}