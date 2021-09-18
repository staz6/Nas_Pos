using Nas_Pos.Helper;

namespace Nas_Pos.Dto.ProductDtos
{
    public class PostProductDto
    {
        public string Title { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int stock { get; set; }
        public int MinimumThreshold { get; set; }       
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public int ProductShelvesId { get; set; }
        
    }
}