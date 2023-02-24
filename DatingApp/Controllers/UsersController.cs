using AutoMapper;
using CloudinaryDotNet;
using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Extensions;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseAPIController
    {
        public static System.Text.Json.Serialization.ReferenceHandler IgnoreCycles { get; }

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoServices _photoServices;
        
        public UsersController(IUserRepository userRepository, IMapper Mapper, IPhotoServices photoServices)
        {
            _userRepository = userRepository;
            _mapper = Mapper;
            _photoServices = photoServices;
        }
        [HttpGet]
        //public IActionResult then we specify the type of thing that we gonna return inside this result  <> 
        // IEnu,erable allows us to use simple iteration of a collection 
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            var usertoReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            if (users is null)
            {
                return Ok(" no users can be find");
            }
            return Ok(usertoReturn);


        }
        [HttpGet("{username}",Name ="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);

            if (user is null)
            {
                return Ok(" no users can be find");
            }
            return _mapper.Map<MemberDto>(user);


        }
        //[HttpGet("{byid}")]

        //public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserById(int id)
        //{
        //    var user = await _userRepository.GetUserByIdAsync(id);

        //    if (user is null)
        //    {
        //        return Ok("A7A no users can be find");
        //    }
        //    return Ok(_mapper.Map<MemberDto>(user));


        //}
        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<AppUser>>> PostUsers(AppUser user) {
        //    //  var users = await _Context.Users.ToListAsync();
        //    //if (!ModelState.IsValid )
        //    //{
        //    //    return BadRequest("A7A");
        //    //}
        //    //await IUserRepository.AddAsync(user);

        //    //try
        //    //{
        //    //    _Context.SaveChanges();

        //    //}
        //    //catch (Exception ex) { }
        //    //catch (Exception ex)
        //    //{
        //    //    if (EmpExist(ins.Ins_Id))
        //    //    {
        //    //        return Conflict();
        //    //    }
        //    //    throw;

        //    //}
        //    return Ok(user);
        //}
        [HttpPut]
        public async Task<ActionResult> UpdateUsers(MemberUpdateDto memberUpdateDto)
        {
            // var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetUserByNameAsync(User.GetuserName());
            _mapper.Map(memberUpdateDto, user);
            _userRepository.UpadateUser(user);
            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");


        }
        //[JsonIgnore]

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> addPhoto(IFormFile File)//allow user to add file 
        {
           

            var user = await _userRepository.GetUserByNameAsync(User.GetuserName());
            var res = await _photoServices.addPhotostoAsync(File);
            if (res.Error is not null)
            {
                return BadRequest(res.Error.Message); // this result is going to come from cloudinary by the way
            }

            var photo = new Photo
            {
                Url = res?.SecureUrl?.AbsoluteUri,
                PublicId = res?.PublicId
            };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
              

                // return _mapper.Map<PhotoDto>(photo);
                // return CreatedAtRoute("GetUser",_mapper.Map<Photo>(photo)); // return a route which contains the photo
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<Photo>(photo));
            };
            return BadRequest("Problem adding photo");


        }
        [HttpPut("set-main-photo/{photoId}")]
         public async Task<ActionResult> SetMainPhoto(int PhotoId)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetuserName());
            var photo = user.Photos.FirstOrDefault(x=> x.Id == PhotoId);

            if (photo.IsMain) return BadRequest("This is already your main photo");
            var currenMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currenMain is not null )currenMain.IsMain = false;
            photo.IsMain = true;
            if(await _userRepository.SaveAllAsync())  return NoContent();

            return BadRequest("Failed to set the main photo");
           
        

        }
    }
   
  
}
