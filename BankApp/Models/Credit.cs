using System; 
namespace BankApp.Models 
{ 
    public class Credit 
    { 
        public int Id { get; set; } 
        public string CreditName { get; set; } 
        public Customer Customer { get; set; } 
        public Product Product { get; set; } 
        public Credit() { } 
        public Credit(int id, string creditName, Customer customer, Product product) 
        { 
            Id = id; 
            CreditName = creditName; 
            Customer = customer; 
            Product = product; 
        } 
        public static Credit From(CreditDto creditDto) 
        { 
            if (creditDto == null) 
            { 
                throw new ArgumentNullException(nameof(creditDto), "CreditDto cannot be null"); 
            } 
            return new Credit 
            { 
                Id = creditDto.Id, 
                CreditName = creditDto.CreditName, 
                Customer = new Customer 
                { 
                    Id = creditDto.CustomerId, 
                    FirstName = creditDto.CustomerFirstName, 
                    LastName = creditDto.CustomerLastName, 
                    PeselNumber = creditDto.CustomerPeselNumber 
                }, 
                Product = new Product 
                { 
                    Id = creditDto.ProductId, 
                    ProductName = creditDto.ProductName, 
                    ProductValue = creditDto.ProductValue 
                } 
            }; 
        } 
    } 
    public class Customer 
    { 
        public int Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public long PeselNumber { get; set; } 
    } 
    public class Product 
    { 
        public int Id { get; set; } 
        public string ProductName { get; set; } 
        public long ProductValue { get; set; } 
    } 
}