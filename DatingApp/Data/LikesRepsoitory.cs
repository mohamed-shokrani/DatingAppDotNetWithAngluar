using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Extensions;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class LikesRepsoitory : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepsoitory(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likeUserId)
        {
             return await _context.UserLikes.FindAsync(sourceUserId, likeUserId);

        }

        public async Task<PageList<LikeDto>> GetUserLikes(LikeParams likeParams)
        {
            var users = _context.Users.OrderBy(x => x.UserName).AsQueryable();
            var likes =  _context.UserLikes.AsQueryable();  
            if (likeParams.Predicate == "liked")
            {
                likes =likes.Where(like=>like.SourceUserId == likeParams.UserId);
                users =likes.Select(like=>like.LikedUser);
            }
            if (likeParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.LikedUserId == likeParams.UserId);
                users = likes.Select(like => like.SourceUser);//list of users thas have liked the currnetly logged in user
            }
           var usersList= users.Select(user=> new LikeDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotUrl = user.Photos.FirstOrDefault(p=>p.IsMain).Url,
                City = user.City ,
                Id = user.Id ,
            });
            return await PageList<LikeDto>.CreateAsync(usersList, likeParams.PageNumber, likeParams.PageSize);

        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users.Include(l=>l.LikedUsers).FirstOrDefaultAsync(x=>x.Id ==userId);
        }
    }
}
