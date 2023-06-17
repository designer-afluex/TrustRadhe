using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrustRadhe.Models
{
    public class Transactions : Common
    {
        public string PK_UserID { get; set; }
        public string LoginID { get; set; }
        public string NewLoginID { get; set; }
        public string MemberName { get; set; }
        public string ClosingDate { get; set; }
        public string Name { get; set; }
        public string JoiningDate { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string isBlocked { get; set; }
        public string Amount { get; set; }
        public string TransactionAction { get; set; }
        public string TemPermanent { get; set; }
        public List<Transactions> lstassociate { get; set; }
        public DataSet GetDitributePaymentList()
        {
            //SqlParameter[] para = { new SqlParameter("@LoginId", LoginID) };
            DataSet ds = DBHelper.ExecuteQuery("MakePaymentList");
            return ds;
        }
        public DataSet GetProductDitributePaymentList()
        {
            //SqlParameter[] para = { new SqlParameter("@LoginId", LoginID) };
            DataSet ds = DBHelper.ExecuteQuery("ProductMakePaymentList");
            return ds;
        }
        public DataSet DistributePayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ClosingDate),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePayment", para);
            return ds;
        }
        public DataSet UpdateMemberLogin()
        {
            SqlParameter[] para = { new SqlParameter("@OldLoginID", LoginID),
                                    new SqlParameter("@NewLoginID", NewLoginID),
                                    new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = DBHelper.ExecuteQuery("UpdateMemberLogin", para);
            return ds;
        }

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateList", para);
            return ds;
        }

        public DataSet ActivateUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@UpdatedBy", UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);
            return ds;
        }

        public DataSet DeactivateUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@UpdatedBy", UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("DeactivateUser", para);
            return ds;
        }

        public DataSet EwalletDeduction()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@TransactionType", TransactionAction),
                                    new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("EwalletDeduction", para);
            return ds;
        }


        public DataSet PayoutDeduction()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@TransactionType", TransactionAction),
                                    new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("PayoutDeduction", para);
            return ds;
        }

        public string FirstName { get; set; }

        public string BinaryIncome { get; set; }

        public string GrossIncome { get; set; }

        public string TDS { get; set; }

        public string Processing { get; set; }

        public string NetIncome { get; set; }

        public string DirectIncome { get; set; }
        public string LastClosingDate { get; set; }
        public string PayoutNo { get; set; }
        public DataSet AutoDistributePayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ClosingDate) };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePayment", para);
            return null;
        }
        public string LeadershipBonus { get; set; }
        public DataSet AutoProductDistributePayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ClosingDate) };
            DataSet ds = DBHelper.ExecuteQuery("AutoProductDistributePayment", para);
            return null;
        }
    }
}