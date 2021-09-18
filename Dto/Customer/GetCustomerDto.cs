namespace API.Dto.Customer
{
    public class GetCustomerDto
    {

        public string FullName { get; set; } 
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Date { get; set; }
    }
}