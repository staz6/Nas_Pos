using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task<string> Login(LoginDto model); 
        Task RegisterEmployee(Employee employee,AppUser model,string password);
        Task RegisterAdmin(Admin admin,AppUser appUser,string password);
        Task RegisterCustomer(CustomerIdentity customer,AppUser model,string password);
        Task CustomerResetPassword(CustomerResetPasswordDto model);
        
    }
}