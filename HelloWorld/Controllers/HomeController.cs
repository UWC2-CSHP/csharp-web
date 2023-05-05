using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IProductRepository productRepository;

        // constructor : DI 
        public HomeController(ILogger<HomeController> logger,
                         MyJsonSettings myJsonSettings, // add DI for MySetting
                         IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            _logger = logger;
        }

        // Add Action
        [HttpGet]
        public IActionResult Products()
        {
           
            return View(productRepository.Products);
        }

        // Add Action
        [HttpGet]
        public IActionResult Product()
        {
            
            return View(productRepository.Products.First());
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
           // int x = 1;  // add me
          // x = x / (x - 1); // add me
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
// used to have some code here...for error
// move it all into the ErrorController.cs
// include the using statements into the platform
        
    }
}