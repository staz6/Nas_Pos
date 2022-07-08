using System.ComponentModel.DataAnnotations;

namespace Nas_Pos.Dto
{
    public class PostProductTypeDto
    {
        [Required]
        public string Title { get; set; }   
        [Required]
        public int ShopId { get; set; }     
    }
}