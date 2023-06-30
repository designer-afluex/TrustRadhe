using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TrustRadhe.Models
{
    public class Password
    {
        public string PKUserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UpdatedBy { get; set; }
        public string PasswordType { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = { new SqlParameter("@PasswordType",PasswordType ) ,
                                      new SqlParameter("@OldPassword", OldPassword) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangePassword", para);
            return ds;
        }

    }
}