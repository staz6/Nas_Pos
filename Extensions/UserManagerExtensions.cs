// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using API.Entities.Identity;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;

// namespace API.Extensions
// {
//     public class UserManagerExtensions
//     {
//         public static  int FindShopIdAsync(this UserManager<AppUser> input,ClaimsPrincipal user)
//         {
//             int shopId = int.Parse(user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value);
//             return shopId;
//         }
//     }
// }