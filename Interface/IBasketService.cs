using System.Threading.Tasks;
using API.Entities;

namespace API.Interface
{
    public interface IBasketService
    {
        Task addItem(string employeeId,int productId,decimal quantity);
        Task<Basket> getBasket(string employeeId);
        Task removeItem(string employeeId,int productId);
        Task deleteBasket(int id);

    }
}