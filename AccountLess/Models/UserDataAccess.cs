using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class UserDataAccess
    {
        public string login(string userName, string password)
        {
            GeneralDataAccess gda = new GeneralDataAccess();

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@userName";
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Value = userName;

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@password";
            param2.SqlDbType = SqlDbType.NVarChar;
            param2.Value = password;

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@userID";
            param3.SqlDbType = SqlDbType.UniqueIdentifier;
            param3.Direction = ParameterDirection.Output;

            List<SqlParameter> sqlParams = new List<SqlParameter> { param1, param2, param3 };
            string userID = gda.runStoredProcOutput("dbo.spLogin", sqlParams, "@userID");

            if (String.IsNullOrEmpty(userID))
            {
                return "";
            }
            else
                return userID;

        }

        public string registerNewUser(string userName, string password)
        {
            GeneralDataAccess gda = new GeneralDataAccess();
            Guid userID = Guid.NewGuid();


            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@userID";
            param1.SqlDbType = SqlDbType.UniqueIdentifier;
            param1.Value = userID;

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@userName";
            param2.SqlDbType = SqlDbType.VarChar;
            param2.Value = userName;

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@password";
            param3.SqlDbType = SqlDbType.NVarChar;
            param3.Value = password;

            List<SqlParameter> sqlParams = new List<SqlParameter> { param1, param2, param3 };
            string storedProcName = "dbo.spRegisterNewUser";


            gda.runStoredProc(storedProcName, sqlParams);
            return userID.ToString();
        }

        public string validateUserName(string userName, string action) {

            string errorMessage = "";

            var regex = new Regex("^[a-zA-Z0-9 ]*$");
            if (!(regex.IsMatch(userName)) || userName.Contains(" "))
            {
                errorMessage = "Invalid Username. Username cannot contain spaces or special characters.";
                return errorMessage;
            }

            if (action == "register")
            {
                GeneralDataAccess gda = new GeneralDataAccess();

                DataSet ds = gda.GetDataSet($"select 1 from [User] where Username = '{userName}'");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    errorMessage = "Invalid Username. Username already exists.";
                    return errorMessage;
                }
            }

            return errorMessage;
        }


        public bool validatePassword(string password)
        {
            var regex = new Regex("^[a-zA-Z0-9 ]*$");
            if (!(regex.IsMatch(password)) || password.Contains(" "))
            {
                return false;
            }
            else
            {
                return true;
            }

        }


  


    }
}

    