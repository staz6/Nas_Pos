using Nas_Pos.Helper;

namespace Nas_Pos.Dto.ProductDtos
{
    public class PutProductDto
    {
        public string Title { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int MinimumThreshold { get; set; }       
        public int ProductTypeId { get; set; }
    }
}