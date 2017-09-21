using Mi_Share.ViewModels;
using Mi_Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mi_Share.Model;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mi_Share.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;

        }
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == null || username.Replace(" ", string.Empty) == "" ||
                password == null || password.Replace(" ", string.Empty) == "")
            {
                ModelState.AddModelError("", "Username and passsword must be entered.");
            }

            else
            {
                var userViewModel = new UserViewModel();


                //get user details from Database
                var user = _userService.GetUserByCredentials(username, password);

                if (user != null)
                {
                    userViewModel = Mapper.Map<User, UserViewModel>(user);

                    var ident = new ClaimsIdentity(
                                    new[] {
                                    // adding following 2 claim just for supporting default antiforgery provider
                                  new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userViewModel.ID)),
                                  new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                                  new Claim(ClaimTypes.Name,$"{userViewModel.FirstName} {userViewModel.LastName} "),
                                  new Claim(ClaimTypes.Email,Convert.ToString(username))

                                    }, DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties
                    { IsPersistent = false }, ident);

                    return RedirectToAction("Index", "Home");

                }


                ModelState.AddModelError("", "Invalid Username or Password.");
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}