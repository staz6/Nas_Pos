using API.Entities.OrderAggregate;

namespace API.Dto.Order
{
    public class GetOrderItemDto
    {
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Price { get; set; }
        public decimal Quantity { get; set; }
    }
}