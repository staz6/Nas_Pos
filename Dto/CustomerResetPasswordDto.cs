using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class CustomerResetPasswordDto
    {
        [Required]
        public string   Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}