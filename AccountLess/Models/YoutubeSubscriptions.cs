using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class YoutubeSubscriptions
    {
        public Guid userID { get; set; }
        public List<string> youtubeChannels { get; set; }
    }
}
