using Application.Extensions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var userId = resultContext.HttpContext.User.GetUserId();
            var repository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repository.GetUserById(userId);
            user.LastActive = DateTime.Now;
            await repository.SaveAllAsync();
        }
    }
}
