using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging; 
using BankApp.Services; 
using System.Threading.Tasks; 
using System; 
namespace BankApp.Controllers 
{ 
    public class HomeController : Controller 
    { 
        private readonly ILogger<HomeController> _logger; 
        private readonly CreditService _creditService; 
        public HomeController(ILogger<HomeController> logger, CreditService creditService) 
        { 
            _logger = logger; 
            _creditService = creditService; 
        } 
        public IActionResult Index() 
        { 
            try 
            { 
                _logger.LogInformation("Navigating to Home Page."); 
                return View(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while loading the Home Page."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        public IActionResult CreateCredit() 
        { 
            try 
            { 
                _logger.LogInformation("Navigating to Create Credit Page."); 
                return View(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while loading the Create Credit Page."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPost] 
        public async Task<IActionResult> CreateCredit(CreditDto creditDto) 
        { 
            try 
            { 
                if (ModelState.IsValid) 
                { 
                    _logger.LogInformation("Creating a new credit."); 
                    await _creditService.CreateCreditAsync(Credit.From(creditDto)); 
                    _logger.LogInformation("Credit created successfully."); 
                    return RedirectToAction("CreditList"); 
                } 
                _logger.LogWarning("Invalid model state for creating credit."); 
                return View(creditDto); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a credit."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        public async Task<IActionResult> CreditList() 
        { 
            try 
            { 
                _logger.LogInformation("Fetching all credits."); 
                var credits = await _creditService.GetAllCreditsAsync(); 
                _logger.LogInformation("Credits fetched successfully."); 
                return View(credits); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching the credits list."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
}