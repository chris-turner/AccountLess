using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class RedditDataAccess
    {
        public static Multireddit mr = new Multireddit();

        public Multireddit getMultireddit(string userID)
        {
            mr.userID = Guid.Parse(userID);
            SqlAccess sqlAccess = new SqlAccess();
            DataTable mrTable = sqlAccess.GetDataSet($"select subreddit from reddit where UserID = '{userID}';").Tables[0];
            mr.subreddits = new List<string>();
            foreach (DataRow row in mrTable.Rows)
            {
                mr.subreddits.Add(row.ItemArray[0].ToString());
            }
            return mr;
        }

        public void addSubreddit(string userID, string subreddit)
        {
            SqlAccess sqlAccess = new SqlAccess();
            sqlAccess.runSQLQuery($"insert into Reddit(UserID, Subreddit) values ('{userID}', '{subreddit}');");
        }

        public void deleteSubreddit(string userID, string subreddit)
        {
            SqlAccess sqlAccess = new SqlAccess();
            sqlAccess.runSQLQuery($"delete from Reddit where UserID = '{userID}' and Subreddit = '{subreddit}';");
        }
    }
}
