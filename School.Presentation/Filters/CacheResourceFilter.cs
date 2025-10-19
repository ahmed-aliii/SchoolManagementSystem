using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace School.Presentation
{
    public class CacheResourceFilter : Attribute, IResourceFilter
    {
        private readonly IMemoryCache _cache;

        public CacheResourceFilter(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var key = context.HttpContext.Request.Path;
            if (_cache.TryGetValue(key, out IActionResult cached))
            {
                context.Result = cached;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var key = context.HttpContext.Request.Path;
            if (!_cache.TryGetValue(key, out _))
            {
                _cache.Set(key, context.Result, TimeSpan.FromMinutes(1));
            }
        }
    }
}
