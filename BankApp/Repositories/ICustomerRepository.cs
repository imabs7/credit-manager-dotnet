namespace BankApp.Repositories 
{ 
    public interface ICustomerRepository 
    { 
        IEnumerable<Customer> GetAll(); 
        Customer Add(Customer customer); 
        Customer GetById(int id); 
    } 
} 