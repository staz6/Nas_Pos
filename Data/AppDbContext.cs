using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Nas_Pos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products{get;set;}
        public DbSet<ProductType> ProductTypes { get; set; }
        public void MigrateDb()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.EnsureCreated());
        }
        
    }
}