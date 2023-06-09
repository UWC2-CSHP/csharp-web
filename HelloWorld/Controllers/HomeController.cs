﻿using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization; // Add me to usings


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
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
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

        // EF Actions
        [HttpGet]
        public IActionResult Classes()
        {
            var context = new Db.SchoolContext(); // Get access to the database

            var classes = context.ClassMasters
                                 .Include(xx => xx.Users) // Include Users table 
                                 .OrderBy(t => t.ClassName); // sort results by classname field

            return View(classes);
        }

        // for Session
        public PartialViewResult IncrementCount()
        {
            return PartialView();
        }
        
        // Exercise Step 2
        public PartialViewResult DisplayLoginName()
        {
            return PartialView();
        }

        // Exercise Step 4 and 5
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // right click to create your Login View
        }
        
        
        // Exercise Step 6
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            HttpContext.Session.SetString("UserName", loginModel.UserName);
            return RedirectToAction("Index");
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index");
        }

        // Set Cookies

        [HttpGet]
        public IActionResult SetCookie()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };

            // Add the cookie to the response to send it to the browser
            // Name: MyCookie
            // Value: myUserName
            // expiring in 1 minute
            Response.Cookies.Append("MyCookie", "myUserName", cookieOptions);
            return View();
        }

        // Get Cookies

        [HttpGet]
        public IActionResult GetCookie()
        {
            var cookieValue = Request.Cookies["MyCookie"];
            return View((object)cookieValue);
        }

        // Security
        // Authorization to access Notes
        [Authorize(Roles = "User")] // define roles
        public IActionResult Notes()
        {
            return View();
        }

        // Added for Exercise 2
        [Authorize(Roles = "Admin")] // define roles
        public IActionResult Admin()
        {
            return View();
        }


    }
}