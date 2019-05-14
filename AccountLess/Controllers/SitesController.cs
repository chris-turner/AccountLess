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
        
       
        public IActionResult Reddit()
        {
            string u;
            //get multireddit for the userid
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            u = TempData.Peek("UserID").ToString();
            RedditDataAccess rda = new RedditDataAccess();
            return View(rda.getMultireddit(u));
        }

        
        [HttpPost]
        public IActionResult addSubreddit(string subreddit)
        {
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string u = TempData.Peek("UserID").ToString();
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
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string u = TempData.Peek("UserID").ToString();
            RedditDataAccess rda = new RedditDataAccess();
            rda.deleteSubreddit(u, subreddit);
            return Redirect("/Sites/Reddit");
        }

        public IActionResult YouTube(string viewType = "Feed")
        {
            string u;
             if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }

            u = TempData.Peek("UserID").ToString();

            YouTubeDataAccess yda = new YouTubeDataAccess();
            YouTubeSubscriptions ys = new YouTubeSubscriptions();

            ys.youtubeVideoFeed = yda.getYouTubeRssFeed();

            ViewData["YouTubeView"] = viewType;


            return View(ys);
        }


        }
}
