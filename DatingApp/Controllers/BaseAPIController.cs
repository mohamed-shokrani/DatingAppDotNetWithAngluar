using DatingApp.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))] 
    [Route("api/[controller]")]
    [ApiController] //features -> automatically validate the paramaters that we parse up to an API endpoint
    //based on the validation that we set
    public class BaseAPIController : ControllerBase
    {
    }
}
