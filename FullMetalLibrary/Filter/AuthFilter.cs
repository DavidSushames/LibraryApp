using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FullMetalLibrary.Filter
{
    public class AuthFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            //Checks if the admin is logged in
            var user = httpContext.Session.GetString("AdminUser");

            var controllerName = context.RouteData.Values["controller"]?.ToString();

            if (controllerName?.Equals("Home", StringComparison.OrdinalIgnoreCase) == true)
            {
                base.OnActionExecuting(context);
                return;
            }

            if (string.IsNullOrEmpty(user))
            {
                // If not logged in, redirect to the login page
                context.Result = new RedirectToActionResult("Login", "Admins", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
