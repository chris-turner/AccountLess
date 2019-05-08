using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class YouTubeVideo
    {
        public string url { get; set; }
        public string title { get; set; }
        public string channel { get; set; }
        public DateTime uploadedDate { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
