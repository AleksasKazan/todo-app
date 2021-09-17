using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using TodoApp.Options;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Attributes
{
    public class ReleaseDateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.RequestServices.GetService<IOptions<AppSettings>>()
                .Value.ReleaseDate >= DateTime.Now)
            {
                context.Result = new BadRequestObjectResult("Feature was not released yet, try again later");
                return;
            }
            await next();
        }
    }
}
