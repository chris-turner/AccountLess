using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;

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
                ytChannel.ChannelID = feed.Id.Replace("yt:channel:", "");
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

        public string getYouTubeIDFromUsername(string username)
        {
            
                AppSettings ap = new AppSettings();
                string googleAPIURL = $"https://www.googleapis.com/youtube/v3/channels?part=id%2Csnippet%2Cstatistics%2CcontentDetails%2CtopicDetails&forUsername={username}&key={ap.GoogleAPIKey}";
                GeneralDataAccess gda = new GeneralDataAccess();
                string jsonString = gda.getJSONFromURL(googleAPIURL);
                var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);
                string id = jsonObj["items"][0].id;
                return id;
            
           
        }
        public bool validateYouTubeChannelID(string channelID)
        {

            AppSettings ap = new AppSettings();
            string googleAPIURL = $"https://www.googleapis.com/youtube/v3/channels?part=id%2Csnippet%2Cstatistics%2CcontentDetails%2CtopicDetails&id={channelID}&key={ap.GoogleAPIKey}";
            GeneralDataAccess gda = new GeneralDataAccess();
            string jsonString = gda.getJSONFromURL(googleAPIURL);
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);
            if (jsonObj["items"].Count == 0)
            {
                return false;
            }
            else
            return true;


        }



        public List<string>[] addYouTubeChannel(string userID, string youTubeChannel)
        {
            List<string> invalidChannels = new List<string>();
            List<string> validChannels = new List<string>();
            List<string> duplicateChannels = new List<string>();

            string[] youTubeURL = { "youtube.com/channel/", "youtube.com/user/" };

            if (youTubeChannel.Contains(youTubeURL[0]))
            {
                youTubeChannel = youTubeChannel.Substring(youTubeChannel.IndexOf(youTubeURL[0]) + youTubeURL[0].Length);
            }
            else if (youTubeChannel.Contains(youTubeURL[1]))
            {
                youTubeChannel = youTubeChannel.Substring(youTubeChannel.IndexOf(youTubeURL[1]) + youTubeURL[1].Length);
                try
                {
                    youTubeChannel = getYouTubeIDFromUsername(youTubeChannel);
                }
                catch (Exception ex)
                {
                    invalidChannels.Add(youTubeChannel);
                    List<String>[] tempChannel = { invalidChannels, validChannels, duplicateChannels };
                    return tempChannel;

                }

            }

            else
            {
                invalidChannels.Add(youTubeChannel);
                List<String>[] tempChannel = { invalidChannels, validChannels, duplicateChannels };
                return tempChannel;
            }

            if (youTubeChannel.Contains("/"))
            {
                youTubeChannel = youTubeChannel.Substring(0,youTubeChannel.IndexOf("/"));
            }

            youTubeChannel = Regex.Replace(youTubeChannel, @"\s+", "");
            List<String>[] channels = validateYouTubeChannel(youTubeChannel);
            invalidChannels.AddRange(channels[0]);
            validChannels.AddRange(channels[1]);
           
            YouTubeSubscriptions ytSub = getYouTubeSubscribedChannelsAndVideos(userID);

            if (ytSub.youtubeChannels.Any(s => s.ChannelID == youTubeChannel))
            {
                validChannels.RemoveAll(channel => channel == youTubeChannel);
                duplicateChannels.Add(youTubeChannel);
            }


            if (validChannels.Count > 0)
            {
                foreach (string channel in validChannels)
                {
                    GeneralDataAccess sqlAccess = new GeneralDataAccess();
                    sqlAccess.runSQLQuery($"insert into YouTube(UserID, ChannelID) values ('{userID}', '{channel}');");
                }
            }

            foreach (string channel in duplicateChannels)
            {
                validChannels.Remove(channel);
            }

            List<String>[] finalChannel = { invalidChannels, validChannels, duplicateChannels };
            return finalChannel;
        }

        private List<string>[] validateYouTubeChannel(string channel)
        {

            var regex = new Regex("^[a-zA-Z0-9_-]*$");
            
            
            if (channel[channel.Length - 1] == '/')
            {
                channel = channel.Substring(0, channel.Length - 1);
            }

            List<string> validChannels = new List<string>();
            List<string> invalidChannels = new List<string>();

            if (validateYouTubeChannelID(channel))
            {
                if (regex.IsMatch(channel))
                {
                    validChannels.Add(channel);

                }
                else
                {
                    invalidChannels.Add(channel);
                }
            }
            else
            {
                invalidChannels.Add(channel);
            }
               

            List<string>[] ytChannelLists = { invalidChannels, validChannels };
            return ytChannelLists;

        }

        public void deleteYouTubeChannel(string userID, string youTubeChannel)
        {
            GeneralDataAccess sqlAccess = new GeneralDataAccess();
            string youTubeChannelID = youTubeChannel.Replace("yt:channel:","");
            sqlAccess.runSQLQuery($"delete from YouTube where UserID = '{userID}' and ChannelID = '{youTubeChannelID}';");
        }
    }
}



  