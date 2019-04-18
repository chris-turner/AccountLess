using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class UserDataAccess
    {
        public string getUserIDForUserName(string userName)
        {
            GeneralDataAccess gda = new GeneralDataAccess();
            DataSet ds = gda.GetDataSet($"select UserID from [User] where UserName = '{userName}'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                return "";
            }
            else
                return ds.Tables[0].Rows[0]["UserID"].ToString();

        }

        public bool validateUserName(string userName, string action) {

            bool isValidUserName;

            var regex = new Regex("^[a-zA-Z0-9 ]*$");
            if (!(regex.IsMatch(userName)) || userName.Contains(" "))
            {
                return false;
            }
            else
            {
                isValidUserName = true;
            }

            if (action == "register")
            {
                GeneralDataAccess gda = new GeneralDataAccess();

                DataSet ds = gda.GetDataSet($"select 1 from [User] where Username = '{userName}'");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    isValidUserName = true;
                }
                else
                {
                    return false;
                }
            }

            return isValidUserName;
        }

        public string registerNewUser(string userName) {
            GeneralDataAccess gda = new GeneralDataAccess();
            Guid userID = Guid.NewGuid();
            gda.runSQLQuery($"insert into [User](UserID, Username) values ('{userID}','{userName}');");
            return userID.ToString();
        }


    }
}

    