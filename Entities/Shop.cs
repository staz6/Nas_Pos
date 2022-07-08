using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class Shop : BaseClass
    {
        public string OwnerId  { get; set; }
        public string Title { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
    }
}