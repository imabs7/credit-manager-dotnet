using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using BankApp.Models; 
using BankApp.Data; 
using Microsoft.Extensions.Logging; 
using Microsoft.EntityFrameworkCore; 
namespace BankApp.Repositories 
{ 
    public interface ICreditRepository 
    { 
        Task<IEnumerable<Credit>> GetAllAsync(); 
        Task<Credit> AddAsync(Credit credit); 
        Task<Credit> GetByIdAsync(int id); 
    } 
    public class CreditRepository : ICreditRepository 
    { 
        private readonly BankContext _context; 
        private readonly ILogger<CreditRepository> _logger; 
        public CreditRepository(BankContext context, ILogger<CreditRepository> logger) 
        { 
            _context = context; 
            _logger = logger; 
        } 
        public async Task<IEnumerable<Credit>> GetAllAsync() 
        { 
            try 
            { 
                _logger.LogInformation("Retrieving all credits from the database."); 
                return await _context.Credits.Include(c => c.Customer).Include(c => c.Product).ToListAsync(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while retrieving all credits."); 
                throw; 
            } 
        } 
        public async Task<Credit> AddAsync(Credit credit) 
        { 
            try 
            { 
                _logger.LogInformation("Adding a new credit to the database."); 
                await _context.Credits.AddAsync(credit); 
                await _context.SaveChangesAsync(); 
                return credit; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while adding a new credit."); 
                throw; 
            } 
        } 
        public async Task<Credit> GetByIdAsync(int id) 
        { 
            try 
            { 
                _logger.LogInformation($"Retrieving credit with ID: {id} from the database."); 
                return await _context.Credits.Include(c => c.Customer).Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"An error occurred while retrieving credit with ID: {id}."); 
                throw; 
            } 
        } 
    } 
}