using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AccountLess.Models;
using Microsoft.AspNetCore.Mvc;


namespace AccountLess.Controllers
{
    public class SitesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
       
        public IActionResult Reddit(string u = "227126EF-E405-4302-AA80-82773149DA1D")
        {
            //u is the user ID
            //get multireddit for the userid
            RedditDataAccess rda = new RedditDataAccess();
            return View(rda.getMultireddit(u));
        }

        
        [HttpPost]
        public IActionResult addSubreddit(string subreddit)
        {
            string u = "227126EF-E405-4302-AA80-82773149DA1D";
            RedditDataAccess rda = new RedditDataAccess();
            List<String>[] subs = rda.addSubreddit(u, subreddit.ToLower());

            foreach (string sub in subs[0])
            {
                TempData["ErrorMessage"] = $"Invalid Subreddit: {sub}\n";
            }

            //int subsAdded = 0;
            //foreach (string sub in subs[1])
            //{
            //    subsAdded += 1;
            //}

            foreach (string sub in subs[2])
            {
                TempData["ErrorMessage"] = $"Duplicate Subreddit: {sub}\n";
            }
            

            
            return Redirect("/Sites/Reddit");
        }

        [HttpPost]
        public IActionResult deleteSubreddit(string subreddit)
        {
            string u = "227126EF-E405-4302-AA80-82773149DA1D";
            RedditDataAccess rda = new RedditDataAccess();
            rda.deleteSubreddit(u, subreddit);
            return Redirect("/Sites/Reddit");
        }

        public IActionResult YouTube()
        {
            return View();
        }

    }
}
