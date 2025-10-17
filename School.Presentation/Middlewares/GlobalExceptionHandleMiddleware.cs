using System.Net;
using System.Text.Json;

namespace School.Presentation
{
    //Middleware
    public class GlobalExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //Invoke
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        //Exception Handling
        private static void HandleException(HttpContext context , Exception exception)
        {
            context.Response.Redirect("/Home/Error");
        }

         #region API Handler
        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var response = new
        //    {
        //        message = exception.Message,
        //        details = exception.InnerException?.Message,
        //        statusCode = (int)HttpStatusCode.InternalServerError
        //    };


        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    return context.Response.WriteAsync(JsonSerializer.Serialize(response)); 
        //}
            #endregion
    }

    //Extention Method
    public static class GlobalExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandleMiddleware>();
        }
    }
}
