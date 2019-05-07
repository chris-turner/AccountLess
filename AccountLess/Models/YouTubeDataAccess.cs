using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;

namespace AccountLess.Models
{
    public class YouTubeDataAccess
    {
        public YoutubeSubscriptions getYouTubeSubscriptions(string userID)
        {
            YoutubeSubscriptions ys = new YoutubeSubscriptions();
            ys.userID = Guid.Parse(userID);
            GeneralDataAccess gda = new GeneralDataAccess();
            //DataTable mrTable = gda.GetDataSet($"select subreddit from reddit where UserID = '{userID}';").Tables[0];
            ys.youtubeChannels = new List<string>();
            return ys;
        }

        public SyndicationFeed getYouTubeRssFeed()
        {
            string[] youTubeChannels = {
            "https://www.youtube.com/user/LinusTechTips",
            "https://www.youtube.com/user/LiveattheBikecom" };
            List<SyndicationItem> ytFeed = new List<SyndicationItem>();
            foreach (string channel in youTubeChannels)
            {
                XmlReader reader = XmlReader.Create(channel);
                Rss20FeedFormatter formatter = new Rss20FeedFormatter();
                formatter.ReadFrom(reader);
                reader.Close();
                ytFeed.AddRange(formatter.Feed.Items);
            }

            ytFeed.Sort(CompareDates);

            SyndicationFeed finalYtFeed = new SyndicationFeed();
            finalYtFeed.Title = new TextSyndicationContent("YouTube Feed");
            finalYtFeed.Description = new TextSyndicationContent
            ("RSS Feed Generated .NET Syndication Classes");
            finalYtFeed.Generator = "My RSS Feed Generator";
            finalYtFeed.Items = ytFeed;

            foreach (var ytVideo in finalYtFeed.Items)
            {
                
            }

            return finalYtFeed;

            /*
            Response.ContentType = "text/xml";
            XmlWriter writer = XmlWriter.Create(Response.Body);
            Rss20FeedFormatter finalFormatter =
            new Rss20FeedFormatter(finalFeed);
            finalFormatter.WriteTo(writer);
            writer.Close();
            Response.Body.Flush();
            */

        }

        private int CompareDates(SyndicationItem x, SyndicationItem y)
        {
            return y.PublishDate.CompareTo(x.PublishDate);
        }
    }
}



  