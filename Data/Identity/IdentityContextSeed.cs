using System.Linq;
using System.Threading.Tasks;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Nas_Pos.Helper;

namespace API.Data.Identity
{
    public class IdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IdentityDbContext context)
        {
            if(!roleManager.Roles.Any())
            {
                var role =await roleManager.RoleExistsAsync(Roles.Admin);
                if(!role){
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
            }
            if(!userManager.Users.Any())
            {
                var user = new AppUser{
                    Id="fa5f4ad6-a005-4d10-8b9f-904cedef7iu8",
                    Email="admin@email.com",
                    UserName="admin@email.com",
                    
                };
                var employee = new Employee{
                    DisplayName="Admin",
                    AppUser=user
                };
                await context.Employees.AddAsync(employee);
                await userManager.CreateAsync(user,"Pa$$w0rd");
                await userManager.AddToRoleAsync(user,Roles.Admin);
                
                
            }
        }
    }
}