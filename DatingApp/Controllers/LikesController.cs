using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Extensions;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly ILikesRepository _likes;

        public LikesController(IUserRepository user, ILikesRepository likes)
        {
            _user = user;
            _likes = likes;
        }
        [HttpPost("{userName}")]
        public async Task<ActionResult> AddLike(string userName)
        {
            var sourecUserId = User.GetuserId();
            var likedUser    = await _user.GetUserByNameAsync(userName);
            var sourceUser   = await _likes.GetUserWithLikes(sourecUserId);
            if (likedUser   == null)
                return NotFound();
            if (sourceUser.UserName == userName)
                return NotFound("You can not like yourself");
            var userLike = await _likes.GetUserLike(sourecUserId, likedUser.Id);
            if (userLike != null)
            {
                return BadRequest("You already liked this user before");
            }
            var userLikes = new UserLike
            {
                SourceUserId = sourecUserId,
                LikedUserId = likedUser.Id,
            };
            sourceUser.LikedUsers.Add(userLikes);
            if (await _user.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("Failed to like the user");

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var users = await _likes.GetUserLikes(predicate, User.GetuserId());
            return Ok(users);
        }

    }
}
