using DatingApp.Extensions;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.Helper;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)// what's gonna happen next
                                                                                                    // after the action excuted 
    {
        var resultContext = await next();
        // check if the user authenticated
        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
        var userId =  context.HttpContext.User.GetuserId();
        var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(userId);
        user.LastActive = DateTime.Now;
        await repo.SaveAllAsync();

    }
}
