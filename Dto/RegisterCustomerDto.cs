using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class RegisterCustomerDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber {get;set;}
        [Required]
        public string Occupation { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zipcode { get; set; }
    }
}