using System.Threading.Tasks;
using API.Dto;

namespace API.Interface
{
    public interface IOrderService
    {
        Task AddOrder(BasketDto model);
}   }