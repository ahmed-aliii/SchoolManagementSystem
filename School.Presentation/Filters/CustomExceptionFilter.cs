using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School.Presentation
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"Exception Filter: Caught an error - {context.Exception.Message}");

            context.Result = new ContentResult()
            {
                Content = $"An unexpected error occurred: {context.Exception.Message}"
            };

            context.ExceptionHandled = true;
        }
    }
}
