using Nas_Pos.Entities.Identity;

namespace API.Entities
{
    public class Customer : BaseClass
    {
        public string FullName { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public string street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        
    }
}