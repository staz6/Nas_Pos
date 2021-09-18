using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class BasketItem : BaseClass
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PictureUrl { get; set; }

    }
}