using System.Diagnostics;

namespace School.Presentation
{
    public class LoggingMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation($"Incoming request: method:{context.Request.Method} | url:{context.Request.Path}");

            //Call Next
            await _next.Invoke(context);

            stopwatch.Stop();
            _logger.LogInformation($"Complested request: method:{context.Request.Method} | url:{context.Request.Path} | Duration:{stopwatch.ElapsedMilliseconds}");
        }
    }
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }

}
