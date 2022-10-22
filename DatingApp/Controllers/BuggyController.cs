using DatingApp.Data;
using DatingApp.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]

    public class BuggyController : Controller
    {
        private readonly DataContext _DataContext;

        public BuggyController(DataContext DataContext)
        {
            _DataContext = DataContext;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {

            return "My Secret Test"; 
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _DataContext.Users.Find(-1);
           

            if (thing == null) return NotFound();

            return Ok(thing);

        }

        [HttpGet("server-error")]
        public ActionResult<string> GetSeverError()
        {
            var thing = _DataContext.Users.Find(-1);// we are just trying to find something that we know does not exist

            string thingToReturn = thing.ToString(); 

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {

            return BadRequest("This is a bad request");
        }

       


    }
}
