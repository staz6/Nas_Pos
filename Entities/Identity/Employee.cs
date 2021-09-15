using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Employee : IdentityUser
    {
        public string DisplayName { get; set; }

    }
}