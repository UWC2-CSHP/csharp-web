using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HelloWorld
{
    public class IPAddressExcludeActionFilter : IActionFilter
    {
        private IHttpContextAccessor httpContextAccessor;

        public IPAddressExcludeActionFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        // Called by the ASP.NET MVC framework BEFORE the action method executes.
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if another filter has already set the result
            if (filterContext.Result != null)
            {
                return;
            }

            var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            
            // Note: comment out this part prior to start of next lecture
            //if (ipAddress == "::1")
            //{
            //    filterContext.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            //}
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // no response for this one - nothing to do we block the hacker
        }

    }
}
