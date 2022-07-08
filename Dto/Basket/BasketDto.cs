using System.Collections.Generic;
using API.Dto.Basket;
using API.Dto.EmployeeBasket;
using API.Entities.OrderAggregate;

namespace API.Dto
{
    public class BasketDto
    {
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public List<EmployeeBasketItem> BasketProducts { get; set; }
        public int DeliveryMethodId { get; set; }
        public PostAddressDto Address { get; set; }
        public int PaymentMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal AmountPaid { get; set; }

    }
}