using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ziggle.WebSite.Models;
using Ziggle.Business;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Ziggle.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryManager categoryManager;
        private readonly IProductManager productManager;
        private readonly IUserManager userManager;

        public HomeController(ILogger<HomeController> logger,
                              ICategoryManager categoryManager,
                              IProductManager productManager,
                              IUserManager userManager)
        {
            this.categoryManager = categoryManager;
            this.productManager = productManager;
            this.userManager = userManager;
            _logger = logger;
        }

        // User Registeration Action
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                userManager.Register(registerModel.UserName, registerModel.Password);
                
               var user = userManager.Register(registerModel.UserName, registerModel.Password);
                
                // check if user is not database
                if (user == null)
                {
                    ModelState.AddModelError("msg", "The email is already in use.");
                    return View();
                }

                return Redirect("~/"); // keep user on the same page
            }

            return View();
        }


        // User Login and Logoff
        [HttpGet]
        public ActionResult LogIn()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            ModelState.Remove("returnUrl");
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new Ziggle.WebSite.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }


        // Category actions:
        public ActionResult Category(int id)
        {
            var category = categoryManager.Category(id);
            var products = productManager
                                .ForCategory(id)
                                .Select(t =>
                                    new Ziggle.WebSite.Models.ProductModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Price = t.Price,
                                        Quantity = t.Quantity
                                    }).ToArray();

            var model = new CategoryViewModel
            {
                Category = new Ziggle.WebSite.Models.CategoryModel(category.Id, category.Name),
                Products = products
            };

            return View(model);
        }


        public IActionResult Index()
        {
            var categories = categoryManager.Categories
                                    .Select(t => new Ziggle.WebSite.Models.CategoryModel(t.Id, t.Name))
                                    .ToArray();
            var model = new IndexModel { Categories = categories };
            return View(model);
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