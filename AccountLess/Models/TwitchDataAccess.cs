using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class TwitchDataAccess
    {
        public TwitchFollowedChannels getTwitchChannelInfo(string userID)
        {
            TwitchFollowedChannels tfc = new TwitchFollowedChannels();
            tfc.twitchFollowedChannels = getTwitchFollowedChannels(userID);
            tfc.twitchLiveChannels = getTwitchLiveChannels(tfc.twitchFollowedChannels);
            return tfc;

        }

        private List<TwitchLiveChannel> getTwitchLiveChannels(List<TwitchChannel> twitchFollowedChannels)
        {
            List<TwitchLiveChannel> tlc = new List<TwitchLiveChannel>();
            foreach (TwitchChannel channel in twitchFollowedChannels)
            {
                TwitchLiveChannel liveChannel = getTwitchLiveChannelInfo(channel.twitchChannelName);
                if (liveChannel != null)
                {
                    tlc.Add(liveChannel);
                }
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

        public TwitchLiveChannel getTwitchLiveChannelInfo(string channelName) {

            AppSettings ap = new AppSettings();
            string twitchAPIURL = $"https://api.twitch.tv/helix/streams?user_login={channelName}";
            GeneralDataAccess gda = new GeneralDataAccess();
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Client-ID", ap.TwitchAPIKey));
            string twitchJson = gda.callAPIWithWebReq(twitchAPIURL, headers);

            TwitchFollowedChannels tfc = new TwitchFollowedChannels();
            TwitchLiveChannel tlc = null;
            if (!twitchJson.Contains("data: []pagination: {}"))
            {
                tlc = new TwitchLiveChannel();
                twitchJson = "{" + twitchJson.Substring(0, twitchJson.IndexOf("pagination")) + "}";
                var jsonObj = JsonConvert.DeserializeObject<dynamic>(twitchJson);
                
                tlc.twitchChannelName = jsonObj["data"][0].user_name;
                tlc.game = getTwitchGameFromID(jsonObj["data"][0].game_id.ToString());
                tlc.streamTitle = jsonObj["data"][0].title;
                tlc.thumbnailURL = jsonObj["data"][0].thumbnail_url.ToString().Replace("{width}", "210").Replace("{height}", "118");
                tlc.viewerCount = jsonObj["data"][0].viewer_count;
                
            }

            return tlc;
        }

        private string getTwitchGameFromID(string gameID)
        {
            AppSettings ap = new AppSettings();
            string twitchAPIURL = $"https://api.twitch.tv/helix/games?id={gameID}";
            GeneralDataAccess gda = new GeneralDataAccess();
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Client-ID", ap.TwitchAPIKey));
            string twitchJson = gda.callAPIWithWebReq(twitchAPIURL, headers);
            twitchJson = "{" + twitchJson + "}";
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(twitchJson);
            return jsonObj["data"][0].name;
        }
    }
}
