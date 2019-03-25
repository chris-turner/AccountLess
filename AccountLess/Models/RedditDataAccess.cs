using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccountLess.Models
{
    public class RedditDataAccess
    {
        public static Multireddit mr = new Multireddit();

        public Multireddit getMultireddit(string userID)
        {
            mr.userID = Guid.Parse(userID);
            GeneralDataAccess gda = new GeneralDataAccess();
            DataTable mrTable = gda.GetDataSet($"select subreddit from reddit where UserID = '{userID}';").Tables[0];
            mr.subreddits = new List<string>();
            foreach (DataRow row in mrTable.Rows)
            {
                mr.subreddits.Add(row.ItemArray[0].ToString());
            }
            return mr;
        }

        public void addSubreddit(string userID, string subreddit)
        {
            bool isValid = validateSubreddit(subreddit);

            if (isValid)
            {
                GeneralDataAccess sqlAccess = new GeneralDataAccess();
                sqlAccess.runSQLQuery($"insert into Reddit(UserID, Subreddit) values ('{userID}', '{subreddit}');");
            }
        }

        private bool validateSubreddit(string subreddit)
        {
            GeneralDataAccess gda = new GeneralDataAccess(); 
            string jsonString  = gda.getJSONFromURL($"https://www.reddit.com/subreddits/search.json?q='subreddit:{subreddit}'&include_over_18=on&limit=1");
            jsonString.IndexOf(",");
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);
            string results = jsonObj["data"]["children"].ToString();
            if (results == "[]")
            {
                return false;
            }
            else
            {
                string subName = ((jsonObj["data"]["children"][0]["data"]["url"]).ToString().Replace("{", "")).Replace("}", "");
                if (subName.ToLower() == $"/r/{subreddit.ToLower()}/")
                {
                    return true;
                }
                else
                    return false;
            }

        }

        public void deleteSubreddit(string userID, string subreddit)
        {
            GeneralDataAccess sqlAccess = new GeneralDataAccess();
            sqlAccess.runSQLQuery($"delete from Reddit where UserID = '{userID}' and Subreddit = '{subreddit}';");
        }


    }
}
