using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class UserDataAccess
    {

        public string getUserIDForUserName(string userName)
        {
            GeneralDataAccess gda = new GeneralDataAccess();
            DataSet ds = gda.GetDataSet($"select UserID from [User] where UserName = '{userName}'");
            return ds.Tables[0].Rows[0]["UserID"].ToString();

        }

    }
}

    