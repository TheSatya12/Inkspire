using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Inkspire.Filters.ActionFilters
{
    public class CacheResourceFilter : IAsyncResourceFilter
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheResourceFilter> _logger;

        public CacheResourceFilter(ILogger<CacheResourceFilter> logger,IMemoryCache cache)
        {
            _cache = cache;
            _logger = logger;
        }


        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var cacheKey = context.HttpContext.Request.Path.ToString();

            if (_cache.TryGetValue(cacheKey, out var cachedResponse))
            {
                _logger.LogInformation("Cache hit. Returning cached response.");

                context.Result = new ObjectResult(cachedResponse);

                ((ObjectResult)context.Result).Value = new
                {
                    success = true,
                    message = "Response From cache Resource Filter",
                    OriginalData = cachedResponse
                };


                return;
            }
            else
            {
                _logger.LogInformation("Cache miss. Proceeding with request.");
            }

            var executedContext = await next();

            
            if (executedContext.Result != null)
            {
                _logger.LogInformation("Caching the response.");
                _cache.Set(cacheKey, ((JsonResult)executedContext.Result).Value, TimeSpan.FromMinutes(2));
            }

        }
    }
}
