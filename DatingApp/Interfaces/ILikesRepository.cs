using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Helper;

namespace DatingApp.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId ,int likeUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PageList<LikeDto>> GetUserLikes(LikeParams likeParams);

    }
}
