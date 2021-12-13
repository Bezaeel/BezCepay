using System;
using System.IO;
using BezCepay.Data;
using BezCepay.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BezCepay.Test
{
    public class Setup
    {
        public readonly AppDbContext dbContext;
        private readonly IConfiguration _configuration;
        public Setup()
        {
            _configuration = InitConfiguration();
            var str = _configuration.GetConnectionString("Default");
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
            .UseNpgsql(str);

            dbContext = new AppDbContext(dbOptions.Options);
            SeedDB();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"Config/test.json", false, false)
                .Build();
                return config;
        }

        public void SeedDB(){
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            dbContext.Orders.AddRange(
                new Order{
                    ConsumerAddress= "Lagos",
                    ConsumerFullname = "Talabi",
                    CreatedAt = DateTime.UtcNow,
                },
                new Order{
                    ConsumerAddress= "London",
                    ConsumerFullname = "Tye",
                    CreatedAt = DateTime.UtcNow,
                }
            );
            dbContext.SaveChanges();
            dbContext.Payments.AddRange(
                new Payment{
                    OrderId = 1,
                    Amount= 1000,
                    CurrencyCode = "USD"
                }
            );
            dbContext.SaveChanges();
        }
    
    }
}