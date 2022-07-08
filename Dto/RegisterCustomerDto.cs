using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class RegisterCustomerDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public int ShopId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public string street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}