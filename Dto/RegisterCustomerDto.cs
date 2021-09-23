namespace API.Dto
{
    public class RegisterCustomerDto
    {
        public int CustomerId { get; set; }
        public string DisplayName { get; set; }

        public int ShopId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}