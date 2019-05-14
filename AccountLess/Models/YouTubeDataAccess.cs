using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Specialized;
using System.Configuration;

namespace AccountLess.Models
{
    public class YouTubeDataAccess
    {
        public YouTubeSubscriptions getYouTubeSubscriptions(string userID)
        {
            //youtube api link
            AppSettings ap = new AppSettings();
            string googleAPIKey = ap.GoogleAPIKey;
            string youTubeUserName = "";
            string youTubeAPIUrl = $"https://www.googleapis.com/youtube/v3/channels?key={googleAPIKey}&part=id&forUsername={youTubeUserName}";
            YouTubeSubscriptions ys = new YouTubeSubscriptions();
            ys.userID = Guid.Parse(userID);
            GeneralDataAccess gda = new GeneralDataAccess();
            //DataTable mrTable = gda.GetDataSet($"select subreddit from reddit where UserID = '{userID}';").Tables[0];
            ys.youtubeChannels = new List<string>();
            return ys;
        }

        public YouTubeVideoFeed getYouTubeRssFeed()
        {
            string[] youTubeChannels = {
                "https://www.youtube.com/feeds/videos.xml?channel_id=UCvOEO35ieBuL-KdV0fXiuag",
            "https://www.youtube.com/feeds/videos.xml?channel_id=UCXuqSBlHAE6Xw-yeJA0Tunw",
            "https://www.youtube.com/feeds/videos.xml?channel_id=UCLNWyduFVhxjj0r1tPrE_-A"
            };
            List<SyndicationItem> ytRssFeed = new List<SyndicationItem>();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.MaxCharactersFromEntities = 1024;

            foreach (string channel in youTubeChannels)
            {
                
                XmlReader reader = XmlReader.Create(channel, settings);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                ytRssFeed.AddRange(feed.Items);
            }

            ytRssFeed.Sort(CompareDates);

            SyndicationFeed finalYtRssFeed = new SyndicationFeed();
            finalYtRssFeed.Title = new TextSyndicationContent("YouTube Feed");
            finalYtRssFeed.Description = new TextSyndicationContent
            ("RSS Feed Generated .NET Syndication Classes");
            finalYtRssFeed.Generator = "My RSS Feed Generator";
            finalYtRssFeed.Items = ytRssFeed;

            YouTubeVideoFeed youtubeVideoFeed = new YouTubeVideoFeed();
            string videoIDStart = "?v=";
            foreach (var ytRssFeedItem in finalYtRssFeed.Items)
            {
                YouTubeVideo ytVideo = new YouTubeVideo();
                ytVideo.title = ytRssFeedItem.Title.Text;
                ytVideo.url = ytRssFeedItem.Links[0].Uri.AbsoluteUri;
                ytVideo.channel = ytRssFeedItem.Authors[0].Name;
                ytVideo.uploadedDate = Convert.ToDateTime(ytRssFeedItem.PublishDate.ToString());
                int videoIDStartIndex = ytVideo.url.LastIndexOf(videoIDStart) + videoIDStart.Length;
                ytVideo.thumbnailUrl = $"https://img.youtube.com/vi/{ytVideo.url.Substring(videoIDStartIndex)}/0.jpg";
                youtubeVideoFeed.ytVideos.Add(ytVideo);

            }

            return youtubeVideoFeed;

        }

        private int CompareDates(SyndicationItem x, SyndicationItem y)
        {
            return y.PublishDate.CompareTo(x.PublishDate);
        }
    }
}



  