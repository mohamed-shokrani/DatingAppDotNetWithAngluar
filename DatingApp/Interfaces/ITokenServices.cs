using DatingApp.Entity;

namespace DatingApp.Interfaces
{
    public interface ITokenServices
    {
        string GetToken(AppUser token);
    }
}
