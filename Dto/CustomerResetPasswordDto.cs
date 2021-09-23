using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class CustomerResetPasswordDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}