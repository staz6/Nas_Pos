using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Identity;
using API.Entities.Identity;
using API.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nas_Pos.Data;
using Nas_Pos.Helper;
using Nas_Pos.Interface;
using Newtonsoft.Json;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<IdentityDbContext>(x =>
             x.UseSqlite(_config.GetConnectionString("IdentityConnection")));

             services.AddDbContext<AppDbContext>(x =>
             x.UseSqlite(_config.GetConnectionString("StoreConnection")));

              services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IAccountRepository,AccountRepository>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<Employee, IdentityRole>(options => 
                    options.Password = new PasswordOptions{
                        RequiredLength = 8,
                        RequireNonAlphanumeric=false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    })
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();


            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<IdentityDbContext>().MigrateDb();
            }
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<AppDbContext>().MigrateDb();
            }
        }
    }
}
