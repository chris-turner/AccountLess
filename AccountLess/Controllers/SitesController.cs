using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountLess.Controllers
{
    public class SitesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        //
        // GET: /Site/Reddit?u='x12345'
        public IActionResult Reddit(string u)
        {
            //u is the user ID
            //get multireddit for the userid
            return View();
        }

        //
        // GET: /Store/Browse?genre=Disco
        public string Browse(string genre)
        {
            string message = HttpUtility.HtmlEncode("Store.Browse, Genre = "
        + genre);

            return message;
        }


    }
}
