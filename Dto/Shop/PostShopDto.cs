using System.ComponentModel.DataAnnotations;

namespace API.Dto.Shop
{
    public class PostShopDto
    {
        [Required]
        public string OwnerId  { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Description { get; set; }
    }
}