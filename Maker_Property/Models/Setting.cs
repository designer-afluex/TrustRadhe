using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrustRadhe.Models
{
    public class Setting:Common
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public DataSet ChangeAssociatePassword()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@LoginId", LoginId) ,
                                      new SqlParameter("@Password", Password) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) 
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangeAssociatePasswordByAdmin", para);
            return ds;
        }

        public DataSet ChangeAdminPassword()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@LoginId", LoginId) ,
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) 
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangeAdminPassword", para);
            return ds;
        }

    }
}