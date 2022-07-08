using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class CustomerIdentity
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public string street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}