using BusinessLayer;
using TrustRadhe.Filter;
using TrustRadhe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace TrustRadhe.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(Reports obj)
        {
            //if (Session["Count"] == null)
            //{
            //    DataSet ds = obj.RewardListForWebsite();
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            //    {
            //        Session["Count"] = "1";
            //        return RedirectToAction("RewardData");
            //    }
            //    else
            //    {
            //        Session["Count"] = null;
            //        return View();
            //    }
            //}
            //else
            //{
            //    Session["Count"] = null;
            //    return View();
            //}

            return View();

        }
    
        
        public ActionResult RewardData(Reports obj)
        {
            List<Reports> lsttop4 = new List<Reports>();
            List<Reports> lst = new List<Reports>();
            DataSet ds = obj.RewardListForWebsite();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalCount = ds.Tables[1].Rows.Count;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj1 = new Reports();

                    obj1.RewardImage = r["RewardImage"].ToString();
                    obj1.RewardName = r["RewardName"].ToString();

                    lsttop4.Add(obj1);
                }
                obj.lstassociate = lsttop4;

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Reports obj1 = new Reports();

                    obj1.PK_RewardItemId = r["PK_UserProdRewardID"].ToString();
                    obj1.RewardID = r["FK_RewardId"].ToString();
                    obj1.Status = r["Status"].ToString();
                    obj1.QualifyDate = r["QualifyDate"].ToString();
                    obj1.LoginId = r["LoginId"].ToString();
                    obj1.RewardName = r["RewardName"].ToString();
                    obj1.Name = r["FirstName"].ToString();
                    obj1.RewardImage = r["RewardImage"].ToString();

                    lst.Add(obj1);
                }
                obj.lsttopupreport = lst;
            }

            return View(obj);
        }
       
      
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                            Session["TransPassword"] = ds.Tables[0].Rows[0]["TransPassword"].ToString();
                            Session["Profile"] = ds.Tables[0].Rows[0]["Profile"].ToString();
                            FormName = "AssociateDashBoard";
                            Controller = "User";
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            FormName = "Login";
                            Controller = "Home";

                        }
                    }
                    else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                        Session["UsertypeName"] = ds.Tables[0].Rows[0]["UsertypeName"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                        if (ds.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                        {
                            Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            FormName = "Registration";
                            Controller = "FranchiseAdmin";
                        }
                        else
                        {
                            FormName = "AdminDashBoard";
                            Controller = "Admin";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        FormName = "Login";
                        Controller = "Home";
                    }

                }

                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    FormName = "Login";
                    Controller = "Home";

                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);



        }

        public ActionResult Registration()
        {
            Home obj = new Home();
            #region ForQueryString
            if (Request.QueryString["Pid"] != null)
            {
                obj.SponsorId = Request.QueryString["Pid"].ToString();
            }
            if (Request.QueryString["lg"] != null)
            {
                obj.Leg = Request.QueryString["lg"].ToString();
                if (obj.Leg == "Right")
                {
                    ViewBag.RightChecked = "checked";
                    ViewBag.LeftChecked = "";
                }
                else
                {
                    ViewBag.RightChecked = "";
                    ViewBag.LeftChecked = "checked";
                }
            }
            if (Request.QueryString["Pid"] != null)
            {
                Common objcomm = new Common();
                objcomm.ReferBy = obj.SponsorId;
                DataSet ds = objcomm.GetMemberDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();



                }
            }
            else
            {
                ViewBag.RightChecked = "";
                ViewBag.LeftChecked = "checked";
            }
            #endregion ForQueryString

            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
            return View(obj);
        }

        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMobileNo(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMobileNo();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
            {
                obj.Result = "Mobile no. already registered!";

            }
            else { obj.Result = "Yes"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Address, string Gender, string OTP, string PinCode, string Leg, string DOB, string AdharNo, string UnderPlaceId)
        {
            Home obj = new Home();

            try
            {
                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.Email = Email;
                obj.MobileNo = MobileNo;
                obj.PanCard = PanCard;
                obj.Address = Address;
                obj.RegistrationBy = "Web";
                obj.Gender = Gender;
                obj.PinCode = PinCode;
                obj.AdharNo = AdharNo;

                obj.UnderPlaceId = UnderPlaceId;
                obj.DOB = DOB;
                obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");
                obj.Leg = Leg;
                string password = Common.GenerateRandom();
                obj.Password = Crypto.Encrypt(password);
                DataSet ds = obj.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Transpassword"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        try
                        {
                            string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch (Exception ex) { }
                        obj.Response = "1";

                    }
                    else
                    {
                        obj.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Response = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult GetStateCity(string PinCode)
        {
            Common obj = new Common();
            obj.PinCode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Decrypt()
        {
            return View();
        }

        //[HttpPost]
        //[ActionName("Decrypt")]
        //[OnAction(ButtonName = "btndecript")]
        //public ActionResult GetPassword(Home obj)
        //{
        //    obj.DecriptPass = Crypto.Decrypt(obj.Password);
        //    return View(obj);
        //}

        public ActionResult GetName(Common obj)
        {
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid LoginId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePlotPin(PlotPin obj)
        {
            DataSet ds = obj.GeneratePin();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //public virtual ActionResult Menu()
        //{
        //    Home Menu = null;

        //    if (Session["_Menu"] != null)
        //    {
        //        Menu = (Home)Session["_Menu"];
        //    }
        //    else
        //    {

        //        Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserTypeName"].ToString()); // pass employee id here
        //        Session["_Menu"] = Menu;
        //    }
        //    return PartialView("_Menu", Menu);
        //}

        #region FranchiseLogin
        public ActionResult FranchiseLogin()
        {
            return View();
        }
        public ActionResult FranchiseLoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.FranchiseLogin();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0]["FranchiseAdmin"].ToString() == "True"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FranchiseName"].ToString();
                            Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                            Session["UserTypeName"] = "Franchise";
                            FormName = "DashBoard";
                            Controller = "FranchiseAdmin";
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect Password";
                            FormName = "FranchiseLogin";
                            Controller = "Home";
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["FranchiseAdmin"].ToString() == "False")
                    {
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["FranchiseID"] = ds.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                        Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session["FK_FranchiseTypeId"] = ds.Tables[0].Rows[0]["FK_FranchiseTypeId"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["FranchiseName"].ToString();

                        FormName = "DashBoard";
                        Controller = "Franchise";
                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        FormName = "FranchiseLogin";
                        Controller = "Home";
                    }
                }
                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    FormName = "FranchiseLogin";
                    Controller = "Home";

                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "FranchiseLogin";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Website
        public ActionResult LandingPage()
        {
            return View();
        }
       
        
        public ActionResult AboutUs()
        {
            return View();
        }
       

        public ActionResult AddProperties()
        {
            return View();
        }

        public ActionResult HighestEarnerClub()
        {
            Home model = new Home();
            List<Home> list = new List<Home>();
            DataSet ds = model.GetHighestEarnerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    //obj.FK_UserId = r["FK_UserId"].ToString();
                  //  obj.LoginId = r["LoginId"].ToString();
                    //obj.Name = r["Name"].ToString();
                    //obj.ProfilePic = r["ProfilePic"].ToString();
                   // obj.GrossAmount = r["GrossAmount"].ToString();
                   // obj.ToDate = r["AddedOn"].ToString();
                    //obj.City = r["City"].ToString();
                    list.Add(obj);
                }
                model.listhighestearner = list;
            }
            return View(model);
        }

      
        public ActionResult Events()
        {
            return View();
        }
        public ActionResult Gallery(Home model )
        {
            List<Home> list = new List<Home>();
            DataSet ds = model.GetGalleryList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    obj.PK_GalleryID = r["PK_GalleryID"].ToString();
                    obj.Image = r["Image"].ToString();

                    list.Add(obj);
                }
                model.lstGallery = list;
            }

            return View(model);
        }
       
        public ActionResult ContactUs()
        {
            return View();
        }
        #endregion Website

        public ActionResult BookingRequest(Home model)
        {
            List<SelectListItem> ddlReligion = Common.BindReligion();
            ViewBag.ddlReligion = ddlReligion;

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender

            List<SelectListItem> ddlcategory = Common.BindCategory();
            ViewBag.ddlcategory = ddlcategory;
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "request")]
        [ActionName("BookingRequest")]
        public ActionResult BookRequest(Home model, IEnumerable<HttpPostedFileBase> postedFile3, IEnumerable<HttpPostedFileBase> postedFile2, IEnumerable<HttpPostedFileBase> postedFile1, IEnumerable<HttpPostedFileBase> postedFile4)
        {
            List<SelectListItem> ddlReligion = Common.BindReligion();
            ViewBag.ddlReligion = ddlReligion;


            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            List<SelectListItem> ddlcategory = Common.BindCategory();
            ViewBag.ddlcategory = ddlcategory;

            try
            {

                foreach (var file in postedFile3)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.BankPhoto = "/images/BankPhoto/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.BankPhoto)));
                    }
                }


                foreach (var file in postedFile2)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.PanCardPhoto = "/images/PanCardPhoto/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.PanCardPhoto)));
                    }
                }

                foreach (var file in postedFile4)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.AdharBackSide = "/images/AdharCard/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.AdharBackSide)));
                    }
                }

                foreach (var file in postedFile1)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.AdharCardPhoto = "/images/AdharCard/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.AdharCardPhoto)));
                    }
                }
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/MM/yyyy");

                DataSet ds = model.BookingRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["BookingRequest"] = "Request Send Successully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BookingRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BookingRequest"] = ex.Message;
            }

            return RedirectToAction("BookingRequest");
        }

        public ActionResult GetSponserDetails1(string ReferBy, string Leg)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            obj.Leg1 = Leg;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else
            {
                obj.Result = "Invalid SponsorId";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


            DataSet ds1 = obj.GetLegDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {

                    obj.Result = "Yes";
                }
                else
                {
                    obj.Result = "Legs are not blank";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetEarnerValue(Home model)
        {
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "Update")]
        [ActionName("SetEarnerValue")]
        public ActionResult UpdateEarnerValue(Home model)
        {

            Session["Pk_AdminId"]=Session["Pk_AdminId"].ToString();
            Session["UserTypeName"] = Session["UserTypeName"].ToString();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.UpdatingEarnerValue();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["SetEarnerValue"] = "Updated Successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["SetEarnerValue"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("SetEarnerValue");
        }
    }
}
