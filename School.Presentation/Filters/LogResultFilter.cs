using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace School.Presentation
{
    public class LogResultFilter : Attribute, IResultFilter
    {
        private readonly ILogger<LogResultFilter> _logger;
        private Stopwatch _stopwatch;

        public LogResultFilter(ILogger<LogResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            _logger.LogInformation($"[ResultFilter] Result Execution Starting: {context.ActionDescriptor.DisplayName}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _stopwatch.Stop();
            _logger.LogInformation($"[ResultFilter] Result Execution Finished: {context.ActionDescriptor.DisplayName} in {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
