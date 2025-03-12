using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inkspire.Filters.ActionFilters
{
    public class CategoryListActionFilter : IActionFilter
    {
        private readonly ILogger<CategoryListActionFilter> _logger;

        public CategoryListActionFilter(ILogger<CategoryListActionFilter> logger)
        {
            _logger = logger;
        }
        //After
        public void OnActionExecuted(ActionExecutedContext context)
        { 
            _logger.LogInformation("CategoryList Action Filter : OnActionExecuted");
        }

        //Before
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("CategoryList Action Filter : OnActionExecuting");
        }
    }
}
