using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class ProductShelves : BaseClass
    {
        public string Title { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
    }
}