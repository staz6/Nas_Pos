using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class CustomerIdentity : AppUser
    {
        public string DisplayName { get; set; }
        public int CustomerId { get; set; }
    }
}