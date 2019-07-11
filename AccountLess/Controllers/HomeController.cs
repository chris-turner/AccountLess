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
        public IActionResult Login(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                UserDataAccess uda = new UserDataAccess();
                string usernameMessage = uda.validateUserName(username, "login");
                string userID;
                if (usernameMessage == "")
                {
                    userID = uda.login(username, password);

                    if (String.IsNullOrEmpty(userID.Trim()))
                    {
                        setLoginErrorMessage("Invalid Username or Password");
                    }
                    else
                    {
                        TempData["UserID"] = userID;
                        TempData["Username"] = username;
                    }

                }
                else
                {
                    setLoginErrorMessage("Invalid Username or Password");
                }
            }
            else
            {
                setLoginErrorMessage("Invalid Username or Password");
            }
            return Redirect("/Home/Index");
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

        public IActionResult Register(string username, string password)
        {
            TempData["LoginViewMode"] = "Register";
            if (String.IsNullOrEmpty(username))
            {
                setLoginErrorMessage("Invalid Username.");
            }
            else if (String.IsNullOrEmpty(password))
            {
                setLoginErrorMessage("Invalid Password.");
            }
            else if (password.Length > 50)
            {
                setLoginErrorMessage("Invalid Password. Password should be 50 characters or less.");
            }
            else if (password.Length < 5)
            {
                setLoginErrorMessage("Invalid Password. Password should be 5 or more characters.");
            }
            else if (username.Length < 3)
            {
                setLoginErrorMessage("Invalid Username. Username should be 3 or more characters.");
            }
            else if (username.Length > 25)
            {
                setLoginErrorMessage("Invalid Username. Username should be 25 characters or less.");
            }
            else {
                UserDataAccess uda = new UserDataAccess();
                string usernameMessage = uda.validateUserName(username, "register");
                if (usernameMessage == "")
                {
                    try
                    {
                        TempData["UserID"] = uda.registerNewUser(username, password);
                    }
                    catch (Exception ex)
                    {
                        setLoginErrorMessage("Invalid Username or Password");
                    }
                    TempData["Username"] = username;
                    TempData["LoginViewMode"] = "";
                }
                else
                {
                    setLoginErrorMessage(usernameMessage);
                }
            }
            return Redirect("/Home/Index");
        }

        public void setLoginErrorMessage(string errorMessage)
        {
            TempData["UserID"] = null;
            TempData["ErrorMessage"] = errorMessage;
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
