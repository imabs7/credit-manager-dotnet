using System.Collections.Generic; 
using System.Threading.Tasks; 
using BankApp.Models; 
namespace BankApp.Repositories 
{ 
    public interface IProductRepository 
    { 
        Task<IEnumerable<Product>> GetAllAsync(); 
        Task<Product> AddAsync(Product product); 
        Task<Product> GetByIdAsync(int id); 
    } 
}