using System.Security.Claims;

namespace DatingApp.Extensions
{
    public static   class ClaimsPrincipalExtensions
    {
        public static string GetuserName(this ClaimsPrincipal user ) {
           var Myuser = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Myuser;

        }
    }
}
