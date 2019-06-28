using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class TwitchLiveChannel
    {
        public string twitchChannelName { get; set; }
        public TwitchGame game { get; set; }
        public string streamTitle { get; set; }
        public int viewerCount { get; set; }
        public string thumbnailURL { get; set; }

    }
}
