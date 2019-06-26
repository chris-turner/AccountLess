using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AccountLess.Models;
using Microsoft.Extensions.Options;

namespace AccountLess.Controllers
{
    public class HomeController : Controller
    {

        private readonly IOptions<AppSettings> config;

        public HomeController(IOptions<AppSettings> config)
        {
            this.config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                UserDataAccess uda = new UserDataAccess();
                bool isValidUsername = uda.validateUserName(username, "login");
                string userID = "";
                if (isValidUsername)
                {
                    userID = uda.getUserIDForUserName(username);

                    if (String.IsNullOrEmpty(userID.Trim()))
                    {
                        TempData["ErrorMessage"] = "Invalid Username+Login";
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        TempData["UserID"] = userID;
                        TempData["Username"] = username;
                        return Redirect("/Home/Index");
                    }

                }
                else
                {
                    TempData["UserID"] = null;
                    TempData["ErrorMessage"] = "Invalid Username";
                    return Redirect("/Home/Index");
                }
            }
            else
            {
                TempData["UserID"] = null;
                TempData["ErrorMessage"] = "Invalid Username";
                return Redirect("/Home/Index");
            }

        }

        public IActionResult About()
        {
            ViewData["Message"] = "AccountLess.";
            return View();
        }

        public IActionResult Logout()
        {
            TempData["UserID"] = null;
            return Redirect("/Home/Index");
        }

        public IActionResult Register(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                UserDataAccess uda = new UserDataAccess();
                bool isValidUserName = uda.validateUserName(username, "register");
                if (isValidUserName)
                {
                    TempData["UserID"] = uda.registerNewUser(username);
                    TempData["Username"] = username;
                }
                else
                {
                    TempData["UserID"] = null;
                    TempData["ErrorMessage"] = "Invalid Username";
                }
            }
            else
            {
                TempData["UserID"] = null;
                TempData["ErrorMessage"] = "Invalid Username";
            }
            return Redirect("/Home/Index");
        }

        public IActionResult OpenRegisterDiv()
        {
            TempData["LoginViewMode"] = "Register";
            return Redirect("/Home/Index");
        }

        public IActionResult OpenLoginDiv()
        {
            TempData["LoginViewMode"] = "Login";
            return Redirect("/Home/Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
