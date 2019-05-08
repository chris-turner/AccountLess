using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class YouTubeVideoFeed
    {
        public List<YouTubeVideo> ytVideos { get; set; }
        
        public YouTubeVideoFeed()
        {
            ytVideos = new List<YouTubeVideo>();
        }
    }

}
