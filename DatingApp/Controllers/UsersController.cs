using AutoMapper;
using DatingApp.Data;
using DatingApp.DTO;
using DatingApp.Entity;
using DatingApp.Helper;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController :BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper Mapper)
        {
            _userRepository = userRepository;
            _mapper = Mapper;
        }
        [HttpGet]
         //public IActionResult then we specify the type of thing that we gonna return inside this result  <> 
         // IEnu,erable allows us to use simple iteration of a collection 
         public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var  users =await _userRepository.GetMembersAsync();
           var usertoReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            if (users is null)
            {
                return Ok("A7A no users can be find");
            }
            return Ok( usertoReturn);


        }
        [HttpGet("{username}")]
        public async Task< ActionResult<MemberDto>> GetUser(string username)
        {
            var user =await _userRepository.GetMemberAsync(username); 
            
            if (user is null)
            {
                return Ok("A7A no users can be find");
            }
            return  _mapper.Map<MemberDto>(user) ;


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
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AppUser>>> PostUsers(AppUser user) {
            //  var users = await _Context.Users.ToListAsync();
            //if (!ModelState.IsValid )
            //{
            //    return BadRequest("A7A");
            //}
            //await IUserRepository.AddAsync(user);

            //try
            //{
            //    _Context.SaveChanges();

            //}
            //catch (Exception ex) { }
            //catch (Exception ex)
            //{
            //    if (EmpExist(ins.Ins_Id))
            //    {
            //        return Conflict();
            //    }
            //    throw;

            //}
            return Ok(user);
        }

    }
   
  
}
