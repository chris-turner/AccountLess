using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class TwitchFollowedChannels
    {
        public Guid userID { get; set; }
        public List<TwitchChannel> twitchFollowedChannels { get; set; }

        public List<TwitchLiveChannel> twitchLiveChannels  { get; set; }

        public TwitchFollowedChannels()
        {
            twitchLiveChannels = new List<TwitchLiveChannel>();
        }
    }
}
