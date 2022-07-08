using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Identity;
using API.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nas_Pos.Data;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()){
                var service =scope.ServiceProvider;
                var loggerFactory =service.GetRequiredService<ILoggerFactory>();
                try{
                    // var userManager = service.GetRequiredService<UserManager<AppUser>>();
                    // var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                    // var identityDbContext = service.GetRequiredService<IdentityDbContext>();
                    // await identityDbContext.Database.MigrateAsync();
                    // await IdentityContextSeed.SeedUserAsync(userManager,roleManager,identityDbContext);
                    var context =service.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context,loggerFactory);
                }
                catch(Exception ex){
                    loggerFactory.CreateLogger(ex.Message);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                     webBuilder.UseUrls("http://0.0.0.0:5000");
                });
    }
}
