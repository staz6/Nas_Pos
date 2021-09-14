using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API.Entities.Identity
{
    public class Employee : IdentityDbContext
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}