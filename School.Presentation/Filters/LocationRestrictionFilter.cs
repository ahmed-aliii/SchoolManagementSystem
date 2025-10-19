using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School.Presentation
{
    public class LocationRestrictionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            // Simulated location check (replace logic later if needed)
            // We'll assume "127.*" or "::1" means Egypt (local dev)
            // and "3.*" means USA (example AWS IP)
            bool isEgypt = ip != null && (ip.StartsWith("127.") || ip == "::1" || ip.StartsWith("41."));
            bool isUSA = ip != null && ip.StartsWith("3.");

            if (!isEgypt && !isUSA)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Access Denied: Only available in Egypt or USA."
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Nothing needed after execution
        }
    }
}
