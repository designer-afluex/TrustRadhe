using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TrustRadhe.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string Leg1 { get; set; }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }
        public DataSet BindUserTypeForRegistration()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeForRegistration");

            return ds;

        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
        #region Form Permissions By User
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = DBHelper.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetMobileNo()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MobileNo", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMobileNo", para);

            return ds;
        }
        public DataSet GetMemberDetailsForSale()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", ReferBy), };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberNameForSale", para);
            return ds;
        }
        public DataSet GetMemberDetailsForFranchiseSale()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", ReferBy), };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberDetailsForFranchiseSale", para);
            return ds;
        }
        public DataSet BindProduct()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductList");
            return ds;
        }

        public DataSet BindProductForFranchisee()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForFranchisee");
            return ds;
        }
        public DataSet BindFranchiseType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseType");
            return ds;
        }
        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "UPI", Value = "UPI" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }
        public static List<SelectListItem> BindPaymentModeForList()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "All", Value = null });
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Supplier", Value = "Supplier" });


            return PaymentMode;
        }

        public static List<SelectListItem> BindReligion()
        {
            List<SelectListItem> Religion = new List<SelectListItem>();
            Religion.Add(new SelectListItem { Text = "Select Religion", Value = null });
            Religion.Add(new SelectListItem { Text = "Hindu", Value = "Hindu" });
            Religion.Add(new SelectListItem { Text = "Muslim", Value = "Muslim" });
            Religion.Add(new SelectListItem { Text = "Christian", Value = "Christian" });

            return Religion;
        }


        public static List<SelectListItem> BindCategory()
        {
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "Select Category", Value = null });
            Category.Add(new SelectListItem { Text = "General", Value = "General" });
            Category.Add(new SelectListItem { Text = "OBC", Value = "OBC" });
            Category.Add(new SelectListItem { Text = "SC", Value = "SC" });
            Category.Add(new SelectListItem { Text = "ST", Value = "ST" });
            return Category;
        }

        public static List<SelectListItem> BindGender()
        {
            List<SelectListItem> Gender = new List<SelectListItem>();
            Gender.Add(new SelectListItem { Text = "Male", Value = "M" });
            Gender.Add(new SelectListItem { Text = "Female", Value = "F" });

            return Gender;
        }
        public static List<SelectListItem> BindPasswordType()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Profile Password", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Transaction Password", Value = "T" });

            return PasswordType;
        }
        public static List<SelectListItem> TransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            TransactionType.Add(new SelectListItem { Text = "Credit", Value = "Credit" });
            TransactionType.Add(new SelectListItem { Text = "Debit", Value = "Debit" });

            return TransactionType;
        }
        public static List<SelectListItem> BindKYCStatus()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Not Uploaded", Value = "N" });
            PasswordType.Add(new SelectListItem { Text = "Pending", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Approved", Value = "A" });
			PasswordType.Add(new SelectListItem { Text = "Rejected", Value = "R" });
            return PasswordType;
        }
        public static List<SelectListItem> AssociateStatus()
        {
            List<SelectListItem> AssociateStatus = new List<SelectListItem>();
            AssociateStatus.Add(new SelectListItem { Text = "All", Value = null });
            AssociateStatus.Add(new SelectListItem { Text = "Active", Value = "P" });
            AssociateStatus.Add(new SelectListItem { Text = "Inactive", Value = "T" });

            return AssociateStatus;
        }
        public static List<SelectListItem> Leg()
        {
            List<SelectListItem> Leg = new List<SelectListItem>();
            Leg.Add(new SelectListItem { Text = "All", Value = null });
            Leg.Add(new SelectListItem { Text = "Left", Value = "L" });
            Leg.Add(new SelectListItem { Text = "Right", Value = "R" });

            return Leg;
        }
        public static List<SelectListItem> BindTopupStatus()
        {
            List<SelectListItem> IncomeStatus = new List<SelectListItem>();
            IncomeStatus.Add(new SelectListItem { Text = "All", Value = null });
            IncomeStatus.Add(new SelectListItem { Text = "Calculated", Value = "1" });
            IncomeStatus.Add(new SelectListItem { Text = "Pending", Value = "0" });

            return IncomeStatus;
        }
        public static List<SelectListItem> BindRealation()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "S/O", Value = "S/O" });
            PaymentMode.Add(new SelectListItem { Text = "D/O", Value = "D/O" });
            PaymentMode.Add(new SelectListItem { Text = "W/O", Value = "W/O" });

            return PaymentMode;
        }
        public static List<SelectListItem> PaidStatus()
        {
            List<SelectListItem> PaidStatus = new List<SelectListItem>();
            PaidStatus.Add(new SelectListItem { Text = "All", Value = "null" });
            PaidStatus.Add(new SelectListItem { Text = "Paid", Value = "1" });
            PaidStatus.Add(new SelectListItem { Text = "Unpaid", Value = "0" });

            return PaidStatus;
        }

        public static List<SelectListItem> KycStatus()
        {
            List<SelectListItem> PaidStatus = new List<SelectListItem>();
            PaidStatus.Add(new SelectListItem { Text = "All", Value = "null" });
            PaidStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PaidStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PaidStatus.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
            return PaidStatus;
        }

        

        public string Fk_UserId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);

            return ds;
        }
        public int GenerateRandomNo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }


        public DataSet GetLegDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),
                                        new SqlParameter("@Leg", Leg1),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetLegDetails", para);

            return ds;
        }
    }

    public class SMSCredential
    {
        public static string UserName = "dreamuser";
        public static string Password = "jUED8rm9rTJMBPE";
        public static string SenderId = "DRMCRS";
    }
    
    public class SoftwareDetails
    {
        public static string CompanyName = "SRK";
        public static string CompanyAddress = "L-12-303, Budh Vihar Colony Dev Ghat Jhalwa Allahabad - 211016 U.P. India.";
        public static string Pin1 = "211016";
        public static string State1 = "UP";
        public static string City1 = "Allahabad";
        public static string ContactNo = "(+91) 8318941233";
        public static string LandLine = "(+91) 8318941233";
        public static string GSTNO = "09AAHCD3782E1ZJ";
        public static string Website = "www.shriradhekunj.in";
        public static string EmailID = "admin@shriradheykunj.com, info@shriradheykunj.com";
    }

}