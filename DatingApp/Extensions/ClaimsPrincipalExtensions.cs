using System.Security.Claims;

namespace DatingApp.Extensions
{
    public static   class ClaimsPrincipalExtensions
    {
        public static string GetuserName(this ClaimsPrincipal user ) {
           var Myuser = user.FindFirst(ClaimTypes.Name)?.Value; //represents the unique name property that we have set
            return Myuser;

        }
        public static int GetuserId(this ClaimsPrincipal user)
        {
            var Myuser = int.Parse( user.FindFirst(ClaimTypes.NameIdentifier)?.Value); //represents the unique name property that we have set
            return  Myuser;

        }
    }
}
