using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace TrustRadhe.Models
{
    public class ProjectStatusResponse
    {
        public string Response { get; set; }
    }
    public class PlotPin
    {
        public string Fk_AssociateId { get; set; }
        public string Amount { get; set; }
        public string PlotDetails { get; set; }
        public string FK_BookingId { get; set; }
        public string LoginId { get; set; }
        public DataSet GeneratePin()
        {
            SqlParameter[] para ={
                                new SqlParameter ("@Fk_AssociateId",Fk_AssociateId),
                                new SqlParameter("@Amount",Amount),
                                new SqlParameter("@PlotDetails",PlotDetails),
                                new SqlParameter("@FK_BookingId",FK_BookingId),
                                new SqlParameter("@LoginId",LoginId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinForRealestate", para);
            return ds;
        }

    }
    public class Home
    {
        public string ProductDetails { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public List<Home> lstGallery { get; set; }
        public string PK_GalleryID { get; set; }

        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string GrossAmount { get; set; }
        public string AddedBy { get; set; }
        public int EarnerValue { get; set; }
        public string FK_UserId { get; set; }
        public string ProfilePic { get; set; }
        public List<Home> listhighestearner { get; set; }
        public string UnderPlaceName { get; set; }
        public string UnderPlaceId { get; set; }
        public string Password { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string AdharBackSide { get; set; }
		[Required(ErrorMessage = "Please Enter Aadhar No.")]
        public string AdharNo { get; set; }
        public string Area { get; set; }
        public string Relation { get; set; }
        public string AssociateName { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AdharCardPhoto { get; set; }
        public string PanCardPhoto { get; set; }
        public string BankPhoto { get; set; }
        public string DDChequeDate { get; set; }
        public string DDChequeNo { get; set; }
        public string PaymentMode { get; set; }
        public string DOB { get; set; }
        public string Nominee { get; set; }
        public string Nationality { get; set; }
        public string Category { get; set; }
        public string Religion { get; set; }
        public string PlotNo { get; set; }
        public string BookingAmount { get; set; }
        public string Cast { get; set; }


        public string LoginId { get; set; }
        [Required(ErrorMessage = "Please Fill SponsorId ")]
        public string SponsorId { get; set; }
        [Required(ErrorMessage = "Please Fill Sponsor Name ")]
        public string SponsorName { get; set; }
        [Required(ErrorMessage = "Please Fill FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Fill Email Id ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Fill Mobile No ")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please select DreamCrusher amount")]
        public string Commitment { get; set; }
        [Required(ErrorMessage = "Please select payment method ")]
        public List<Home> lstMenu { get; set; }

        public string PaymentMethod { get; set; }
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }

        public DataSet GetGalleryList()
        {
           
            DataSet ds = DBHelper.ExecuteQuery("GetGalleryImages");
            return ds;
        }

        public DataSet GetHighestEarnerList()
        {
           
            DataSet ds = DBHelper.ExecuteQuery("GetHighestEarnerList");
            return ds;
        }


        public DataSet UpdatingEarnerValue()
        {
            SqlParameter[] para ={

                new SqlParameter ("@UpdatedBy",AddedBy),
                  new SqlParameter ("@FromDate",FromDate),
                    new SqlParameter ("@ToDate",ToDate),
                new SqlParameter("@EarnerValue",EarnerValue)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEarnerValue", para);
            return ds;
        }



        public DataSet FranchiseLogin()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("FranchiseLogin", para);
            return ds;
        }

        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@PanCard",PanCard),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",Leg),
                                     new SqlParameter("@Password",Password),
                                       new SqlParameter("@DOB",DOB),
                                        new SqlParameter("@AdharNo",AdharNo)
,
                                         new SqlParameter("@UnderPlaceId",UnderPlaceId)
                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }


        public DataSet BookingRequest()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Name", Name),
                                 new SqlParameter("@FathersName", FatherName),
                                 new SqlParameter("@DOB", DOB),
                                 new SqlParameter("@Nominee", Nominee),
                                 new SqlParameter("@Nationality", Nationality),
                                 new SqlParameter("@MobileNo", MobileNo),
                                 new SqlParameter("@Address", Address),
                                 new SqlParameter("@Category", Category),
                                 new SqlParameter("@Religion", Religion),
                                 new SqlParameter("@PlotNo", PlotNo),
                                 new SqlParameter("@BookingAmount", BookingAmount),
                                 new SqlParameter("@CustomerId", CustomerId),
                                 new SqlParameter("@Cast", Cast),
                                  new SqlParameter("@Gender", Gender),
                                   new SqlParameter("@Relation", Relation),
                                    new SqlParameter("@PinCode", PinCode),
                                     new SqlParameter("@AdharCardPhoto", AdharCardPhoto),
                                      new SqlParameter("@AdharNo", AdharNo),
                                       new SqlParameter("@AssociateName", AssociateName),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                         new SqlParameter("@BankName", BankName),
                                           new SqlParameter("@DDChequeNo", DDChequeNo),
                                             new SqlParameter("@DDChequeDate", DDChequeDate),
                                             new SqlParameter("@BankBranch", BankBranch),
                                             new SqlParameter("@PanCard", PanCard),
                                              new SqlParameter("@PanCardPhoto", PanCardPhoto),
                                               new SqlParameter("@BankPhoto", BankPhoto),
                                               new SqlParameter("@Area", Area),
                                               new SqlParameter("@AdharBackSide", AdharBackSide)
            };

            DataSet ds = DBHelper.ExecuteQuery("BookingRequest", para);
            return ds;
        }

        public string RegistrationBy { get; set; }

        public string Response { get; set; }

        public string Gender { get; set; }
        public string PanCard { get; set; }
        public string Address { get; set; }
		[Required(ErrorMessage = "Please Enter Pin Code")]
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string OTP { get; set; }

        public string Leg { get; set; }

        public DataTable PermissionDBSet { get; set; }
        public List<Home> lstsubmenu { get; set; }
        public string Pk_AdminId { get; set; }
        public string UserType { get; set; }
        public string Url { get; set; }
        public string MenuName { get; set; }
        public string MenuId { get; set; }
   public string Icon { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }

        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Icon = r["Icon"].ToString();
                        obj.Url = r["Url"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }
    }
}