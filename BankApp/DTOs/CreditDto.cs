using System; 
namespace BankApp.DTOs 
{ 
    public class CreditDto 
    { 
        public int Id { get; set; } 
        public string CreditName { get; set; } 
        public int CustomerId { get; set; } 
        public string CustomerFirstName { get; set; } 
        public string CustomerLastName { get; set; } 
        public long CustomerPeselNumber { get; set; } 
        public int ProductId { get; set; } 
        public string ProductName { get; set; } 
        public long ProductValue { get; set; } 
        public static CreditDto From(Models.Credit credit) 
        { 
            if (credit == null) 
            { 
                throw new ArgumentNullException(nameof(credit), "Credit cannot be null"); 
            } 
            return new CreditDto 
            { 
                Id = credit.Id, 
                CreditName = credit.CreditName, 
                CustomerId = credit.Customer.Id, 
                CustomerFirstName = credit.Customer.FirstName, 
                CustomerLastName = credit.Customer.LastName, 
                CustomerPeselNumber = credit.Customer.PeselNumber, 
                ProductId = credit.Product.Id, 
                ProductName = credit.Product.ProductName, 
                ProductValue = credit.Product.ProductValue 
            }; 
        } 
    } 
}