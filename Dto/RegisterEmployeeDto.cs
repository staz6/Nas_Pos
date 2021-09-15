using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class RegisterEmployeeDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        
        
    }
}