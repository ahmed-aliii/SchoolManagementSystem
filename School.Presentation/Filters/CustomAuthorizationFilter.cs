using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School.Presentation
{
    //Before Action Or Controller
    public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool notAuthorized = true;
            
            if (notAuthorized)
            {
                context.Result = new ContentResult()
                {
                    Content = "Access Denied: User not authorized."
                };
            }
        }
    }
}
