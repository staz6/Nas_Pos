using Nas_Pos.Entities.Identity;

namespace API.Entities.OrderAggregate
{
    public class OrderItem : BaseClass
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, int price, decimal quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemOrdered { get; set; }
        public int Price { get; set; }
        public decimal Quantity { get; set; }
        
    }
}