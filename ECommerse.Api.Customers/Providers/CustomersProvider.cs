using AutoMapper;
using ECommerse.Api.Customers.Db;
using ECommerse.Api.Customers.Interfaces;
using ECommerse.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerse.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;
        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Haritha", Address="Canada" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Arun", Address="India" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Athira", Address="America" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Anandu", Address="Dubai" });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? ErrorMessage)> 
            GetAllCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                
                if(customers!= null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }

                return (false, null, "NotFound");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
