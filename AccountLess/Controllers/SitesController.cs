﻿using System;
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

            ys = yda.getYouTubeSubscribedChannelsAndVideos(u);

            ViewData["YouTubeView"] = viewType;


            return View(ys);
        }

        [HttpPost]
        public IActionResult addYouTubeChannel(string youTubeChannel)
        {
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string userID = TempData.Peek("UserID").ToString();
            YouTubeDataAccess yda = new YouTubeDataAccess();
            if (!String.IsNullOrEmpty(youTubeChannel))
            {
                List<String>[] channels = yda.addYouTubeChannel(userID, youTubeChannel);
                foreach (string channel in channels[0])
                {
                    TempData["ErrorMessage"] = $"Invalid Channel: {channel}\n";
                }

                foreach (string channel in channels[2])
                {
                    TempData["ErrorMessage"] = $"Duplicate Channel: {channel}\n";
                }
            }

            return Redirect("/Sites/YouTube?viewType=Channels");
        }

        [HttpPost]
        public IActionResult deleteYouTubeChannel(string youTubechannel)
        {
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string u = TempData.Peek("UserID").ToString();
            YouTubeDataAccess yda = new YouTubeDataAccess();
            yda.deleteYouTubeChannel(u, youTubechannel);
            return Redirect("/Sites/YouTube?viewType=Channels");
        }
        public IActionResult Twitch(string viewType = "Feed")
        {
            //get multireddit for the userid
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string userId = TempData.Peek("UserID").ToString();
            TwitchDataAccess tda = new TwitchDataAccess();
            var channelInfo = tda.getTwitchChannelInfo(userId, viewType);

            ViewData["TwitchView"] = viewType;

            return View(channelInfo);
        }

        [HttpPost]
        public IActionResult addTwitchChannel(string twitchChannel)
        {
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string u = TempData.Peek("UserID").ToString();
            TwitchDataAccess tda = new TwitchDataAccess();
            if (!String.IsNullOrEmpty(twitchChannel))
            {
                List<String>[] channels = tda.addTwitchChannel(u, twitchChannel);
                foreach (string channel in channels[0])
                {
                    TempData["ErrorMessage"] = $"Invalid Channel: {channel}\n";
                }

                //int subsAdded = 0;
                //foreach (string sub in subs[1])
                //{
                //    subsAdded += 1;
                //}

                foreach (string channel in channels[2])
                {
                    TempData["ErrorMessage"] = $"Duplicate Channel: {channel}\n";
                }
            }

            return Redirect("/Sites/Twitch?viewType=Channels");
        }

        [HttpPost]
        public IActionResult deleteTwitchChannel(string twitchChannel)
        {
            if (TempData.Peek("UserID") == null)
            {
                return Redirect("/Home/Index");
            }
            string u = TempData.Peek("UserID").ToString();
            TwitchDataAccess tda = new TwitchDataAccess();
            tda.deleteTwitchChannel(u, twitchChannel);
            return Redirect("/Sites/Twitch?viewType=Channels");
        }



    }
}
  