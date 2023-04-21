using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        // ADD ACTION
        [HttpGet]
        public IActionResult RsvpForm()
        {
            return View();
        }

        // ADD ACTION: POST
        [HttpPost]
        public IActionResult RsvpForm(GuestResponse guestResponse)
        {
            // validation action to force the user to enter their name
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}