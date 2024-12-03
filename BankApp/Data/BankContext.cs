using Microsoft.EntityFrameworkCore; 
using BankApp.Models; 
namespace BankApp.Data 
{ 
    public class BankContext : DbContext 
    { 
        public BankContext(DbContextOptions<BankContext> options) : base(options) { } 
        public DbSet<Credit> Credits { get; set; } 
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Product> Products { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            // Configure entity properties and relationships if necessary 
        } 
    } 
} 