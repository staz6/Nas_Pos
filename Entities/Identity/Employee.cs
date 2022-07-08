using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Employee 
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
       
        public string DisplayName { get; set; }
        public int ShopId { get; set; }

    }
}