using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Attributes
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiKey = context.HttpContext.Request.Headers["ApiKey"].SingleOrDefault();
            //var todoId = (Guid)context.ActionArguments["Id"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Result = new BadRequestObjectResult("ApiKey header is missing");
                return;
            }

            var userRepository = context.HttpContext.RequestServices.GetService<IUsersRepository>();

            var apiKeyObj = userRepository.GetApiKey(apiKey);

            if (apiKeyObj is null)
            {
                context.Result = new NotFoundObjectResult("ApiKey was not found");
                return;
            }

            if (!apiKeyObj.Result.IsActive)
            {
                context.Result = new BadRequestObjectResult("ApiKey is expired");
                return;
            }
            
            context.HttpContext.Items.Add("userId", apiKeyObj.Result.UserId);

            await next();
        }
    }
}
