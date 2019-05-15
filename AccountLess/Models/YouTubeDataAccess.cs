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
        public YouTubeSubscriptions getYouTubeSubscribedChannelsAndVideos(string userID)
        {
            var youtubeChannels = new List<YouTubeChannel>();
            GeneralDataAccess gda = new GeneralDataAccess();
            DataTable dtYtChannelIDs = gda.GetDataSet($"select ChannelID from YouTube where UserID = '{userID}';").Tables[0];

            YouTubeSubscriptions ytSubscriptions = new YouTubeSubscriptions();
            ytSubscriptions.userID = Guid.Parse(userID);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.MaxCharactersFromEntities = 1024;

            List<SyndicationItem> ytRssFeed = new List<SyndicationItem>();

            foreach (DataRow row in dtYtChannelIDs.Rows)
            {
                string channelRssURL = $"https://www.youtube.com/feeds/videos.xml?channel_id={row.ItemArray[0].ToString()}";
                XmlReader reader = XmlReader.Create(channelRssURL, settings);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                YouTubeChannel ytChannel = new YouTubeChannel();
                ytChannel.ChannelName = feed.Authors[0].Name;
                ytChannel.ChannelLink = feed.Authors[0].Uri;
                ytChannel.ChannelID = feed.Id;
                youtubeChannels.Add(ytChannel);
                ytRssFeed.AddRange(feed.Items);
            }

            ytSubscriptions.youtubeChannels = youtubeChannels;
            ytRssFeed.Sort(CompareDates);

            SyndicationFeed finalYtRssFeed = new SyndicationFeed();
            finalYtRssFeed.Title = new TextSyndicationContent("YouTube Feed");
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
            ytSubscriptions.youtubeVideoFeed = youtubeVideoFeed;
            return ytSubscriptions;
        }


        private int CompareDates(SyndicationItem x, SyndicationItem y)
        {
            return y.PublishDate.CompareTo(x.PublishDate);
        }
    }
}



  