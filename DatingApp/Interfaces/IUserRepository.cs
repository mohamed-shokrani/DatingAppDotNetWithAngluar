using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Helper;
using System.Collections.Generic;

namespace DatingApp.Interfaces
{
    public interface IUserRepository 
    {
        void UpadateUser(AppUser appUser);
        Task<IEnumerable<AppUser>> GetAppUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<bool> SaveAllAsync( );

        Task<AppUser> GetUserByNameAsync(string username);
        Task <PageList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username);



    }
}
