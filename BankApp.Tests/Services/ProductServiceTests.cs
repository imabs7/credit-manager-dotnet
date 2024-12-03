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
    public class ProductServiceTests 
    { 
        private readonly Mock<IProductRepository> _productRepoMock; 
        private readonly Mock<ILogger<ProductService>> _loggerMock; 
        private readonly ProductService _productService; 
        public ProductServiceTests() 
        { 
            _productRepoMock = new Mock<IProductRepository>(); 
            _loggerMock = new Mock<ILogger<ProductService>>(); 
            _productService = new ProductService(_productRepoMock.Object, _loggerMock.Object); 
        } 
        [Fact] 
        public async Task GetAllProductsAsync_ReturnsAllProducts() 
        { 
            // Arrange 
            var products = new List<Product> 
            { 
                new Product { Id = 1, ProductName = "Product 1" }, 
                new Product { Id = 2, ProductName = "Product 2" } 
            }; 
            _productRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products); 
            // Act 
            var result = await _productService.GetAllProductsAsync(); 
            // Assert 
            Assert.Equal(2, ((List<Product>)result).Count); 
        } 
        [Fact] 
        public async Task CreateProductAsync_ValidProduct_ReturnsProduct() 
        { 
            // Arrange 
            var product = new Product { Id = 1, ProductName = "Product 1" }; 
            _productRepoMock.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product); 
            // Act 
            var result = await _productService.CreateProductAsync(product); 
            // Assert 
            Assert.Equal(product, result); 
        } 
    } 
} 