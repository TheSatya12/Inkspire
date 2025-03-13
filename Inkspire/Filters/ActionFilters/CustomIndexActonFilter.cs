using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inkspire.Filters.ActionFilters
{
    public class CustomIndexActonFilter : IAsyncActionFilter
    {
        public readonly ILogger<CustomIndexActonFilter> _logger;

        public CustomIndexActonFilter(ILogger<CustomIndexActonFilter> logger)
        {
            _logger = logger;
        }



        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Index Action is Executing");

            if (context.ActionArguments.ContainsKey("searchQuery"))
            {
                if (context.ActionArguments["searchQuery"] is string searchQuery1 && !string.IsNullOrWhiteSpace(searchQuery1))
                {
                    context.ActionArguments["searchQuery"] = "Romance";
                }
                var searchQuery = context.ActionArguments["searchQuery"] as string;
                if (!string.IsNullOrEmpty(searchQuery)&& searchQuery.Length < 3)
                {
                    _logger.LogWarning("search query is too short");
                    context.Result = new BadRequestObjectResult("searchQuery must be atleast 2 characters long");
                    return;
                }
            }

            await next();

            _logger.LogInformation("Index Action is Executed");
        }
    }
}
