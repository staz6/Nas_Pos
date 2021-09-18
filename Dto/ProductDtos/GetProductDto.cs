using Nas_Pos.Helper;

namespace Nas_Pos.Dto
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int stock { get; set; }
        public int MinimumThreshold { get; set; }
        public string Shelve { get; set; }
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public string ProductTypeName { get; set; }
    }
}