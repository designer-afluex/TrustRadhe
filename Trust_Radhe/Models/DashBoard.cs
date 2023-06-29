using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrustRadhe.Models
{
    public class DashBoard:Common
    {
        public string JoiningDate { get; set; }
        public string Total { get; set; }

        public string Status { get; set; }
        public string Fk_UserId { get; set; }

        public DataSet GetDashBoardDetails()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails");
            return ds;
        }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId",Fk_UserId ) , };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate",para);
            return ds;
        }

        public string LoginId { get; set; }


     

        public List<DashBoard> lstmessages { get; set; }

        public string Pk_MessageId { get; set; }

        public string MessageTitle { get; set; }

        public string Message { get; set; }

        public DataSet GetAllMessages()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetMessages", para);
            return ds;
        }


        public string cssclass { get; set; }

        public string MessageBy { get; set; }

        public DataSet SaveMessage()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@Message", Message),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@MessageBy", MessageBy),
                                      new SqlParameter("@Fk_UserId", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("InsertMessage", para);
            return ds;
        }

        public string ProfilePic { get; set; }

        public string MemberName { get; set; }

        public List<DashBoard> lstinvestment { get; set; }

        public string Amount { get; set; }

        public string ProductName { get; set; }

        public string Month { get; set; }
    }
}