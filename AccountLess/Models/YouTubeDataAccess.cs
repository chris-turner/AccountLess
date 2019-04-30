using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class YouTubeDataAccess
    {
        public YoutubeSubscriptions getYouTubeSubscriptions(string userID)
        {
            YoutubeSubscriptions ys = new YoutubeSubscriptions();
            ys.userID = Guid.Parse(userID);
            GeneralDataAccess gda = new GeneralDataAccess();
            //DataTable mrTable = gda.GetDataSet($"select subreddit from reddit where UserID = '{userID}';").Tables[0];
            ys.youtubeChannels = new List<string>();
            return ys;
        }
    }
}
  