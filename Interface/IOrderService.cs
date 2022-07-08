using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto;
using API.Dto.Ledger;
using API.Entities.Ledger;
using API.Entities.OrderAggregate;

namespace API.Interface
{
    public interface IOrderService
    {
        Task AddOrder(BasketDto model);
        Task<IReadOnlyList<Order>> GetAllOrder();
        Task<IReadOnlyList<Order>> GetOrderByCustomerId(string id);
        Task<Order> GetOrderById(int id);
        Task<IReadOnlyList<Order>> GetOrderBySort(int id); 

        Task AddTransaction(int ledgerId,Transaction model);
}   }