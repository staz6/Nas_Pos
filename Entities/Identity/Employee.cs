using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class Employee : AppUser
    {
        public string DisplayName { get; set; }

    }
}