using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;
using Nas_Pos.Data;

namespace API.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(AppDbContext conntext , ILoggerFactory loggerFactory){
            try{
                if(!conntext.ProductTypes.Any()){
                    var file = File.ReadAllText("/home/staz/Documents/pos/API/Helper/SeedData/types.json");
                    var data =JsonSerializer.Deserialize<List<ProductType>>(file);
                    foreach(var item in data){
                        conntext.ProductTypes.Add(item);
                    }
                    await conntext.SaveChangesAsync();

                }
                if(!conntext.ProductShelves.Any()){
                    var file = File.ReadAllText("/home/staz/Documents/pos/API/Helper/SeedData/shelves.json");
                    var data =JsonSerializer.Deserialize<List<ProductShelves>>(file);
                    foreach(var item in data){
                        conntext.ProductShelves.Add(item);
                    }
                    await conntext.SaveChangesAsync();

                }
                if(!conntext.Products.Any()){
                    var file = File.ReadAllText("/home/staz/Documents/pos/API/Helper/SeedData/products.json");
                    var data =JsonSerializer.Deserialize<List<Product>>(file);
                    foreach(var item in data){
                        conntext.Products.Add(item);
                    }
                    await conntext.SaveChangesAsync();

                }
                if(!conntext.DeliveryMethods.Any()){
                    var file = File.ReadAllText("/home/staz/Documents/pos/API/Helper/SeedData/DeliveryMethod.json");
                    var data =JsonSerializer.Deserialize<List<DeliveryMethod>>(file);
                    foreach(var item in data){
                        conntext.DeliveryMethods.Add(item);
                    }
                    await conntext.SaveChangesAsync();

                }
            }
            catch(Exception ex){
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}