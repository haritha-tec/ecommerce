using Microsoft.EntityFrameworkCore;

namespace ECommerse.Api.Orders.Db
{
    public class OrderDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
