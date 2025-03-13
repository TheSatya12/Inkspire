using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mail;

namespace Inkspire.Filters.ActionFilters
{
    public class CustomAsyncResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<CustomAsyncResultFilter> _logger;
        public CustomAsyncResultFilter(ILogger<CustomAsyncResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            
            _logger.LogInformation("{FilterName}.{MethodName} Method", nameof(CustomAsyncResultFilter), nameof(OnResultExecutionAsync));

            context.HttpContext.Response.Headers["X-Custom-Header"] = "Filtered Response";

            if(context.Result is JsonResult objCategory)
            {
                objCategory.Value = new
                {
                    message = "Response from Custom Result Filter",
                    OrginalData = objCategory.Value
                };
            }

            await next();

            _logger.LogInformation("{FilterName}.{MethodName} Method", nameof(CustomAsyncResultFilter), nameof(OnResultExecutionAsync));


        }
    }
}
