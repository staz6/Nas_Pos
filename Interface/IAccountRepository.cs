using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task<string> Login(LoginDto model);
        Task RegisterEmployee(Employee model,string password);
        Task RegisterAdmin(Employee model,string password);
        
    }
}