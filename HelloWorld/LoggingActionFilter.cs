using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace HelloWorld
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch stopwatch;

        private IWebHostEnvironment env;

        // IWebHostEnvironment gives us variables about the environment
        public LoggingActionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }

        // Called when request begins
        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }


        // Called when request ends
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            // NOTE: YOU MAY WANT TO STOP THE PIPLINE WHEN FACING A HACKER OR A PROBLEM
            if (actionExecutedContext.Result !=null) { return; }

            stopwatch.Stop();

            // Get the controller name and action method
            var controller = (Microsoft.AspNetCore.Mvc.Controller)actionExecutedContext.Controller;

            var controllerContext = controller.ControllerContext;

            var actionDescriptor = controllerContext.ActionDescriptor;

            var controllerName = actionDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;

            // Here's an example of variables about the environment
            // WebRootPath = path to location of the web site root
            var webroot = env.WebRootPath;
            var filepath = Path.Combine(webroot, "logger.txt");

            var logline = string.Format("{0} : {1}-{2} Elapsed={3}\n",
                System.DateTime.Now, controllerName, actionName, stopwatch.Elapsed);

            File.AppendAllText(filepath, logline);
        }
    }
}