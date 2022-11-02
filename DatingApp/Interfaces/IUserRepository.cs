using DatingApp.Entity;

namespace DatingApp.Interfaces
{
    public interface IUserRepository 
    {
        void UpadateUser(AppUser appUser);
        Task<IEnumerable<AppUser>> GetAppUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<bool> SaveAllAsync( );

        Task<AppUser> GetUserByNameAsync(string username);



    }
}
