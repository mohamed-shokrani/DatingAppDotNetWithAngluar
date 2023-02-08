using System.Security.Claims;

namespace DatingApp.Extensions
{
    public static   class ClaimsPrincipalExtensions
    {
        public static string GetuserName(this ClaimsPrincipal user ) {
           return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }
    }
}
