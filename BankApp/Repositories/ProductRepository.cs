using System; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using BankApp.Models; 
using BankApp.Data; 
using Microsoft.Extensions.Logging; 
using Microsoft.EntityFrameworkCore; 
namespace BankApp.Repositories 
{ 
    public interface IProductRepository 
    { 
        Task<IEnumerable<Product>> GetAllAsync(); 
        Task<Product> AddAsync(Product product); 
        Task<Product> GetByIdAsync(int id); 
    } 
    public class ProductRepository : IProductRepository 
    { 
        private readonly BankContext _context; 
        private readonly ILogger<ProductRepository> _logger; 
        public ProductRepository(BankContext context, ILogger<ProductRepository> logger) 
        { 
            _context = context; 
            _logger = logger; 
        } 
        public async Task<IEnumerable<Product>> GetAllAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Retrieving all products from the database."); 
                return await _context.Products.ToListAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while retrieving all products."); 
                throw; 
            } 
        } 
        public async Task<Product> AddAsync(Product product) 
        { 
            try 
            { 
                _logger.LogInformation("Adding a new product to the database."); 
                await _context.Products.AddAsync(product); 
                await _context.SaveChangesAsync(); 
                return product; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while adding a new product."); 
                throw; 
            } 
        } 
        public async Task<Product> GetByIdAsync(int id) 
        { 
            try 
            { 
                _logger.LogInformation($"Retrieving product with ID: {id} from the database."); 
                return await _context.Products.FirstOrDefaultAsync(p => p.Id == id); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"An error occurred while retrieving product with ID: {id}."); 
                throw; 
            } 
        } 
    } 
}