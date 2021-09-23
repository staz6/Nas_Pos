using Nas_Pos.Entities.Identity;
using Nas_Pos.Helper;

namespace API.Entities
{
    public class Product : BaseClass
    {
        public string Title { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string PictureUrl { get; set; }
        public decimal PurchasedPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public string Description { get; set; }
        public int MinimumThreshold { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductShelves ProductShelves{get;set;}
        public int? ProductShelvesId { get; set; }
    }
}