using System.Reflection;
using System;
using API.Entities;
using API.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Polly;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using API.Entities.Ledger;

namespace Nas_Pos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products{get;set;}
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductShelves> ProductShelves { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Ledger> Ledgers{get;set;}
        public DbSet<Transaction> Transactions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
            }
        }

        // public void MigrateDb()
        // {
        //     Policy
        //         .Handle<Exception>()
        //         .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
        //         .Execute(() => Database.EnsureCreated());
        // }
        
    }
}