using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class TwitchDataAccess
    {
        public TwitchFollowedChannels getTwitchChannelInfo(string userID, string viewType)
        {
            TwitchFollowedChannels tfc = new TwitchFollowedChannels();

            tfc.twitchFollowedChannels = getTwitchFollowedChannels(userID);

            if (viewType != "Channels")
            {
                tfc.twitchLiveChannels = getTwitchLiveChannels(tfc.twitchFollowedChannels);
            }
            return tfc;

        }

        private List<TwitchLiveChannel> getTwitchLiveChannels(List<TwitchChannel> twitchFollowedChannels)
        {
            List<TwitchLiveChannel> tlc = new List<TwitchLiveChannel>();

            List<string> channelNames = new List<string>();
            if (twitchFollowedChannels.Count > 0)
            {
                foreach (TwitchChannel channel in twitchFollowedChannels)
                {
                    channelNames.Add(channel.twitchChannelName);
                }
                tlc  = getTwitchLiveChannelInfo(channelNames);
            }


            return tlc;
        }

        private List<TwitchChannel> getTwitchFollowedChannels(string userID)
        {
            GeneralDataAccess sqlAccess = new GeneralDataAccess();
            DataTable dtTwitchChannels = sqlAccess.GetDataSet($"select ChannelName from Twitch where UserID = '{userID}';").Tables[0];
            List<TwitchChannel> twitchFollowedChannels = new List<TwitchChannel>();

            foreach (DataRow row in dtTwitchChannels.Rows)
            {
                TwitchChannel tc = new TwitchChannel();
                tc.twitchChannelName = row.ItemArray[0].ToString();
                twitchFollowedChannels.Add(tc);
            }

            return twitchFollowedChannels;
        }

        public List<TwitchLiveChannel> getTwitchLiveChannelInfo(List<string> channelNames) {

            AppSettings ap = new AppSettings();
            string twitchAPIURL = "https://api.twitch.tv/helix/streams?user_login=";
            for (int i = 0; i < channelNames.Count; i++)
            {
                if (i == channelNames.Count - 1)
                {
                    twitchAPIURL += channelNames[i];
                }
                else {
                    twitchAPIURL += $"{channelNames[i]}&user_login=";
                }
            }
            
            GeneralDataAccess gda = new GeneralDataAccess();
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Client-ID", ap.TwitchAPIKey));
            string twitchJson = gda.callAPIWithWebReq(twitchAPIURL, headers);
            List<TwitchLiveChannel> twitchLiveChannels = new List<TwitchLiveChannel>();

            if (twitchJson == "Too Many Requests")
            {
                twitchLiveChannels = null;
            }

            else
            {
                if (!twitchJson.Contains("data: []pagination: {}"))
                {

                    twitchJson = "{" + twitchJson.Substring(0, twitchJson.IndexOf("pagination")) + "}";
                    var jsonObj = JsonConvert.DeserializeObject<dynamic>(twitchJson);
                    for (int i = 0; i < jsonObj["data"].Count; i++)
                    {
                        TwitchLiveChannel tlc = new TwitchLiveChannel();
                        tlc.twitchChannelName = jsonObj["data"][i].user_name;
                        string game = getTwitchGameFromID(jsonObj["data"][i].game_id.ToString());
                        if (game == "Too Many Requests")
                        {
                            twitchLiveChannels = null;
                        }
                        else
                        {
                            tlc.game = game;
                            tlc.streamTitle = jsonObj["data"][i].title;
                            tlc.thumbnailURL = jsonObj["data"][i].thumbnail_url.ToString().Replace("{width}", "210").Replace("{height}", "118");
                            tlc.viewerCount = jsonObj["data"][i].viewer_count;
                            twitchLiveChannels.Add(tlc);
                        }
                    }


                }
            }

            return twitchLiveChannels;
        }

        private string getTwitchGameFromID(string gameID)
        {
            AppSettings ap = new AppSettings();
            string twitchAPIURL = $"https://api.twitch.tv/helix/games?id={gameID}";
            GeneralDataAccess gda = new GeneralDataAccess();
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Client-ID", ap.TwitchAPIKey));
            string twitchJson = gda.callAPIWithWebReq(twitchAPIURL, headers);
            if (twitchJson == "Too Many Requests")
            {
                return twitchJson;
            }
            twitchJson = "{" + twitchJson + "}";
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(twitchJson);
            return jsonObj["data"][0].name;
        }

        internal List<string>[] addTwitchChannel(string u, string twitchChannel)
        {
            twitchChannel = twitchChannel.ToLower();
            List<string> invalidChannels = new List<string>();
            List<string> validChannels = new List<string>();
            List<string> duplicateChannels = new List<string>();

            twitchChannel = Regex.Replace(twitchChannel, @"\s+", "");
            List<String>[] channels = validateTwitchChannel(twitchChannel);
            invalidChannels.AddRange(channels[0]);
            validChannels.AddRange(channels[1]);

            TwitchFollowedChannels twitchFollowedChannels = new TwitchFollowedChannels();

            twitchFollowedChannels.twitchFollowedChannels = getTwitchFollowedChannels(u);

            if (twitchFollowedChannels.twitchFollowedChannels.Any(s => s.twitchChannelName.Contains(twitchChannel)))
            {
                validChannels.RemoveAll(channel => channel == twitchChannel);
                duplicateChannels.Add(twitchChannel);
            }


            if (validChannels.Count > 0)
            {
                foreach (string channel in validChannels)
                {
                    GeneralDataAccess sqlAccess = new GeneralDataAccess();
                    sqlAccess.runSQLQuery($"insert into Twitch(UserID, ChannelName) values ('{u}', '{channel}');");
                }
            }

            foreach (string channel in duplicateChannels)
            {
                validChannels.Remove(channel);
            }

            List<String>[] finalSubs = { invalidChannels, validChannels, duplicateChannels };
            return finalSubs;
        }

        private List<string>[] validateTwitchChannel(string twitchChannel)
        {
            var regex = new Regex("^[a-zA-Z0-9_-]*$");
            string[] youTubeURL = { "twitch.com/"};
            foreach (string url in youTubeURL)
            {
                if (twitchChannel.Contains(url))
                {
                    twitchChannel = twitchChannel.Substring(twitchChannel.IndexOf(url) + url.Length);
                }
            }

            if (twitchChannel[twitchChannel.Length - 1] == '/')
            {
                twitchChannel = twitchChannel.Substring(0, twitchChannel.Length - 1);
            }

            List<string> validChannels = new List<string>();
            List<string> invalidChannels = new List<string>();

            if (regex.IsMatch(twitchChannel))
                {
                    validChannels.Add(twitchChannel);

                }
                else
                {
                    invalidChannels.Add(twitchChannel);
                }


            List<string>[] twitchChannelLists = { invalidChannels, validChannels };
            return twitchChannelLists;
        }

        internal void deleteTwitchChannel(string u, string twitchChannel)
        {
            GeneralDataAccess sqlAccess = new GeneralDataAccess();
            sqlAccess.runSQLQuery($"delete from Twitch where UserID = '{u}' and ChannelName = '{twitchChannel}';");
        }
    }
}
