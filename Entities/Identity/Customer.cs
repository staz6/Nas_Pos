using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Customer : IdentityUser
    { 

       public string DisplayName{get;set;}
       public string ContactNumber {get;set;}
       public string Occupation { get; set; }
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
       
    }
}