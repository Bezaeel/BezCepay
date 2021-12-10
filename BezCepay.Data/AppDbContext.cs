using BezCepay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BezCepay.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
