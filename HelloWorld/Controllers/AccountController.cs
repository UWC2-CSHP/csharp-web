﻿using HelloWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace HelloWorld.Controllers
{
    public class AccountController : Controller
    {

        private IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // Security Exercise 2: Add Denied Action
        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LogOut()
        {
            // Get rid of the authentication cookie
            HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme);

            // Go to home page
            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult LogOn()
        {
            // Keep track of returnUrl
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public IActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(model.UserName, model.Password);
                if (user != null)
                {

                    // Exercise 2: Security - Modify to address the roles
                    var claims = new List<Claim>
                        {
                            // Keep track of user name
                            new Claim(ClaimTypes.Name, model.UserName),
                            //new Claim(ClaimTypes.Role, “Admin, User”),
                        };

                    // Exercise 2: Security - Split Admin and User and get rid of any spaces before and after         
                    var roles = user.Role.Split(",");
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                    }


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

                        IsPersistent = model.RememberMe,
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

                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }
    }
}