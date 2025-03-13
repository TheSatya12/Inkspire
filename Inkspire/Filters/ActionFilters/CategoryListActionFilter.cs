using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inkspire.Filters.ActionFilters
{
    public class CategoryListActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<CategoryListActionFilter> _logger;

        public CategoryListActionFilter(ILogger<CategoryListActionFilter> logger)
        {
            _logger = logger;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("CategoryList Action Filter : OnActionExecuting");
            await next();
            _logger.LogInformation("CategoryList Action Filter : OnActionExecuted");
        }
    }
}
