using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class YouTubeSubscriptions
    {
        public Guid userID { get; set; }
        public List<YouTubeChannel> youtubeChannels { get; set; }
        public YouTubeVideoFeed youtubeVideoFeed { get; set; }
    }
}
