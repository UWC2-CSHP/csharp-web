using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Add Action
        [HttpGet]
        public IActionResult Products()
        {
            // add our count field into the list
            var products = new Product[]
            {
        new Product{ ProductId = 1, Name = "First One", Price = 1.11m, ProductCount=0},
        new Product{ ProductId = 2, Name="Second One", Price = 2.22m, ProductCount=1},
        new Product{ ProductId = 3, Name="Third One", Price = 3.33m, ProductCount=10},
        new Product{ ProductId = 4, Name="Fourth One", Price = 4.44m, ProductCount=100},

            };

            return View(products);
        }

        // Add Action
        [HttpGet]
        public IActionResult Product()
        {
            var myProduct = new Product
            {
                ProductId = 1,
                Name = "Kayak",
                Description = "A boat for one person",
                Category = "water-sports",
                Price = 200m,
            };

            return View(myProduct);
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
            // create an exception
            int x = 1;  // add me
            x = x / (x - 1); // add me
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // revise Error action as below
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
        HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var msg = exceptionHandlerPathFeature.Error.Message;

            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = msg,
            };

            return View(model);
        }
    }
}