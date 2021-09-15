using Nas_Pos.Helper;

namespace Nas_Pos.Dto
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int MinimumThreshold { get; set; }
    }
}