using System.Threading.Tasks;
using API.Dto;
using API.Entities.Identity;

namespace API.Interface
{
    public interface IAccountRepository
    {
        Task Login(LoginDto model);
        Task RegisterEmployee(Employee model);
        Task RegisterCustomer(Customer model);
    }
}