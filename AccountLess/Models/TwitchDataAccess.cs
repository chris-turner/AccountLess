using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class TwitchDataAccess
    {
        public TwitchFollowedChannels getTwitchChannelInfo(string username)
        {

            AppSettings ap = new AppSettings();
            string twitchAPIURL = $"https://api.twitch.tv/helix/streams?user_login={username}";
            GeneralDataAccess gda = new GeneralDataAccess();
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Client-ID", ap.TwitchAPIKey));
            string twitchJson = gda.callAPIWithWebReq(twitchAPIURL, headers);
            //jsonString.IndexOf(",");
            // var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);
            // string id = jsonObj["items"][0].id;
            TwitchFollowedChannels tfc = new TwitchFollowedChannels();
            tfc.ChannelName = twitchJson;
            return tfc;


        }
    }
}
