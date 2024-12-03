using System; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using BankApp.Models; 
using BankApp.Data; 
using Microsoft.Extensions.Logging; 
using Microsoft.EntityFrameworkCore; 
namespace BankApp.Repositories 
{ 
    public interface ICustomerRepository 
    { 
        Task<IEnumerable<Customer>> GetAllAsync(); 
        Task<Customer> AddAsync(Customer customer); 
        Task<Customer> GetByIdAsync(int id); 
    } 
    public class CustomerRepository : ICustomerRepository 
    { 
        private readonly BankContext _context; 
        private readonly ILogger<CustomerRepository> _logger; 
        public CustomerRepository(BankContext context, ILogger<CustomerRepository> logger) 
        { 
            _context = context; 
            _logger = logger; 
        } 
        public async Task<IEnumerable<Customer>> GetAllAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Retrieving all customers from the database."); 
                return await _context.Customers.ToListAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while retrieving all customers."); 
                throw; 
            } 
        } 
        public async Task<Customer> AddAsync(Customer customer) 
        { 
            try 
            { 
                _logger.LogInformation("Adding a new customer to the database."); 
                await _context.Customers.AddAsync(customer); 
                await _context.SaveChangesAsync(); 
                return customer; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while adding a new customer."); 
                throw; 
            } 
        } 
        public async Task<Customer> GetByIdAsync(int id) 
        { 
            try 
            { 
                _logger.LogInformation($"Retrieving customer with ID: {id} from the database."); 
                return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"An error occurred while retrieving customer with ID: {id}."); 
                throw; 
            } 
        } 
    } 
}