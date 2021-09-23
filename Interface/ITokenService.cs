using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user,string roleName);
    }
}