using Microsoft.AspNetCore.Mvc.Filters;

namespace Inkspire.Filters.ActionFilters
{
    public class CategoryCreateAndEditPostActionFilter : IAsyncActionFilter
    {
        


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
        }
    }
}
