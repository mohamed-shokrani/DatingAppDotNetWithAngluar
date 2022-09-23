using DatingApp.Data;
using DatingApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _Context;

        public UsersController(DataContext Context)
        {
            _Context = Context;
        }
        [HttpGet]
         //public IActionResult then we specify the type of thing that we gonna return inside this result  <> 
         // IEnu,erable allows us to use simple iteration of a collection 
         public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var  users =await _Context.Users.ToListAsync();
            if (users is null)
            {
                return Ok("A7A no users can be find");
            }
            return Ok(users);


        }
        [HttpGet("{id}")]
        public async Task< ActionResult<IEnumerable<AppUser>>> GetUsersById(int id)
        {
            var user =await _Context.Users.FindAsync(id);   
            if (user is null)
            {
                return Ok("A7A no users can be find");
            }
            return Ok(user);


        }

    };
}
