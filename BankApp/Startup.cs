using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.EntityFrameworkCore; 
using BankApp.Services; 
using BankApp.Repositories; 
using BankApp.Data; 
using BankApp.Middleware;  // Add this using statement at the top 
namespace BankApp 
{ 
    public class Startup 
    { 
        public Startup(IConfiguration configuration) 
        { 
            Configuration = configuration; 
        } 
        public IConfiguration Configuration { get; } 
        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services) 
        { 
            services.AddControllersWithViews(); 
            // Add DbContext with SQL Server provider 
            services.AddDbContext<BankContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("BankDatabase"))); 
            // Register repositories and services 
            services.AddScoped<ICreditRepository, CreditRepository>(); 
            services.AddScoped<ICustomerRepository, CustomerRepository>(); 
            services.AddScoped<IProductRepository, ProductRepository>(); 
            services.AddTransient<CreditService>(); 
            services.AddTransient<CustomerService>(); 
            services.AddTransient<ProductService>(); 
        } 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        { 
            if (env.IsDevelopment()) 
            { 
                app.UseDeveloperExceptionPage(); 
            } 
            else 
            { 
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts(); 
            } 
            app.UseHttpsRedirection(); 
            app.UseStaticFiles(); 
            app.UseRouting(); 
            // Add the exception handling middleware 
            app.UseMiddleware<ExceptionHandlingMiddleware>(); 
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllerRoute( 
                    name: "default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}"); 
            }); 
        } 
    } 
}