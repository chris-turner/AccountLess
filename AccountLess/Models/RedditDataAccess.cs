using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AccountLess.Models
{
    public class RedditDataAccess
    {
        
        public Multireddit getMultireddit(string userID)
        {
            Multireddit mr = new Multireddit();
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

        public List<string>[] addSubreddit(string userID, string subreddit)
        {
            subreddit = Regex.Replace(subreddit, @"\s+", "");
            List<String>[] subs = validateSubreddit(subreddit);
            List<string> invalidSubs = subs[0];
            List<string> validSubs = subs[1];
            List<string> duplicateSubs = new List<string>();
            Multireddit mr = getMultireddit(userID);

            if (mr.subreddits.Any(s => s.Contains(subreddit)))
            {
                validSubs.RemoveAll(sub => sub == subreddit);
                duplicateSubs.Add(subreddit);
            }


            if (validSubs.Count > 0)
            {
                foreach (string sub in validSubs)
                {
                    GeneralDataAccess sqlAccess = new GeneralDataAccess();
                    sqlAccess.runSQLQuery($"insert into Reddit(UserID, Subreddit) values ('{userID}', '{sub}');");
                }
            }

            foreach (string sub in duplicateSubs)
            {
                validSubs.Remove(sub);
            }
            
            List<String>[] finalSubs = { invalidSubs, validSubs, duplicateSubs };
            return finalSubs;

        }

        private List<string>[] validateSubreddit(string subreddit)
        {
          
            var regex = new Regex("^[a-zA-Z0-9_-]*$");
            string redditURL = "reddit.com/r/";
            if (subreddit.Contains(redditURL))
            {
                subreddit = subreddit.Substring(subreddit.IndexOf(redditURL) + redditURL.Length);
            }
            if (subreddit[subreddit.Length - 1] == '/') {
                subreddit = subreddit.Substring(0, subreddit.Length - 1    );
            }
            List<string> subs = subreddit.Split('+').ToList();

            List<string> validSubs = new List<string>();
            List<string> invalidSubs = new List<string>();
            

            foreach (string sub in subs)
            {
                if (regex.IsMatch(sub))
                {
                    validSubs.Add(sub);
                    
                }
                else
                {
                    invalidSubs.Add(sub);
                }

            }

            List<string>[] subredditLists = { invalidSubs, validSubs };
            return subredditLists;

        }

        //currently not working 100%, returning some valid subreddits as invalid. will have to mess around more
        private List<string>[]  validateSubredditWithRedditApi(string sub) {
            List<string> validSubs = new List<string>();
            List<string> invalidSubs = new List<string>();
            GeneralDataAccess gda = new GeneralDataAccess();
            string jsonString = gda.getJSONFromURL($"https://www.reddit.com/subreddits/search.json?q='subreddit:{sub}'&include_over_18=on&limit=1");
            jsonString.IndexOf(",");
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);
            string results = jsonObj["data"]["children"].ToString();
            //
            if (results == "[]")
            {
                bool isValid = validateSubredditAlt(sub);
                if (isValid)
                {
                    validSubs.Add(sub);
                }
                else
                    invalidSubs.Add(sub);
            }
            else
            {
                string subName = ((jsonObj["data"]["children"][0]["data"]["url"]).ToString().Replace("{", "")).Replace("}", "");
                if (subName.ToLower() == $"/r/{sub.ToLower()}/")
                {
                    validSubs.Add(sub);
                }
                else
                {
                    bool isValid = validateSubredditAlt(sub);
                    if (isValid)
                    {
                        validSubs.Add(sub);
                    }
                    else
                        invalidSubs.Add(sub);
                }
            }

            List<string>[] subredditLists = { invalidSubs, validSubs };
            return subredditLists;
             
        }

        private bool validateSubredditAlt(string subreddit) {
            GeneralDataAccess gda = new GeneralDataAccess();
            string jsonString = gda.getJSONFromURL($"https://www.reddit.com/subreddits/search.json?q='{subreddit}'&include_over_18=on");
            if (jsonString.ToLower().Contains($"url\": \"/r/{subreddit.ToLower()}/"))
            {
                return true;
            }
            else
                return false;
        }

        public void deleteSubreddit(string userID, string subreddit)
        {
            GeneralDataAccess sqlAccess = new GeneralDataAccess();
            sqlAccess.runSQLQuery($"delete from Reddit where UserID = '{userID}' and Subreddit = '{subreddit}';");
        }


    }
}
