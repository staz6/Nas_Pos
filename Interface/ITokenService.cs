using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user,string roleName);
    }
}