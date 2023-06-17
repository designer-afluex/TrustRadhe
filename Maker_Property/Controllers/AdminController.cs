using BusinessLayer;
using TrustRadhe.Filter;
using TrustRadhe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrustRadhe.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult AdminDashBoard()
        {
            DashBoard newdata = new DashBoard();
            DataSet Ds = newdata.GetDashBoardDetails();

            ViewBag.TotalUsers = Ds.Tables[1].Rows[0]["TotalUsers"].ToString();
            ViewBag.BlockedUsers = Ds.Tables[1].Rows[0]["BlockedUsers"].ToString();
            ViewBag.InactiveUsers = Ds.Tables[1].Rows[0]["InactiveUsers"].ToString();
            ViewBag.ActiveUsers = Ds.Tables[1].Rows[0]["ActiveUsers"].ToString();

            #region Messages


            List<DashBoard> lst1 = new List<DashBoard>();

            DataSet ds11 = newdata.GetAllMessages();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    DashBoard Obj = new DashBoard();
                    Obj.Pk_MessageId = r["Pk_MessageId"].ToString();
                    Obj.Fk_UserId = r["Fk_UserId"].ToString();
                    Obj.MemberName = r["Name"].ToString();
                    Obj.MessageTitle = r["MessageTitle"].ToString();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Message = r["Message"].ToString();
                    Obj.cssclass = r["cssclass"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lstmessages = lst1;
            }
            #endregion Messages
            return View(newdata);
        }

        public ActionResult AssociateListForKYC(Reports model)
        {
            List<SelectListItem> ddlKYCStatus = Common.KycStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            List<Reports> lst = new List<Reports>();

            model.Status = null;
            DataSet ds = model.AssociateListForKYC();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.AdharCardNo = r["AdharNumber"].ToString();
                    obj.DocumentType = r["DocumentType"].ToString();
                    obj.AdharImage = (r["AdharImage"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.PancardImage = (r["PanImage"].ToString());
                    obj.Pancard = r["PanNumber"].ToString();

                    obj.passportNo = r["DocumentNumber"].ToString();
                    obj.PassportImage = r["DocumentImage"].ToString();

                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BankBranch = r["DocumentNumber"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            return View(model);
        }

        public ActionResult ApproveKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<Reports> lst = new List<Reports>();

                Reports model = new Reports();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Approved";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.ApproveKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Approved Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "Admin";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "Admin";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "Admin";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetUserDetails()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPayoutStatus()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[2].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[2].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvestmentDetails()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[3].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[3].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Amount = (dr["Amount"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetJoiningDeatils()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[4].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[4].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BinaryTree()
        {
            ViewBag.Fk_UserId = "1";
            return View();
        }
        public ActionResult Registration(string Pid, string lg)
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
                    ViewBag.Disabled = "Disabled";
                }
                else
                {
                    ViewBag.RightChecked = "";
                    ViewBag.LeftChecked = "checked";
                    ViewBag.Disabled = "Disabled";
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
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Address, string Gender, string OTP, string PinCode, string Leg)
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
                        Session["Transpassword"] = ds.Tables[0].Rows[0]["Password"].ToString();
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        try
                        {
                            string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch { }
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

        public ActionResult OTP(string FirstName, string MobileNo)
        {
            Home obj = new Home();
            Common objcom = new Common();
            int OTP = objcom.GenerateRandomNo();
            Session["OTP"] = OTP.ToString();
            string str2 = BLSMS.OTP(FirstName, OTP.ToString());

            string str = BLSMS.SendSMS2(SMSCredential.UserName, SMSCredential.Password, SMSCredential.SenderId, "8299051766", str2);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region PinManagement
        public ActionResult CreatePin()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[1].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View();

        }

        public ActionResult CreatePinAction(Wallet obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.CreatePin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["createpin"] = "Pin Created Successfully";
                    }
                    else
                    {
                        TempData["createpin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["createpin"] = ex.Message;
            }
            return RedirectToAction("CreatePin", "Admin");
        }

        public ActionResult UnusedPins()
        {
            Wallet objewallet = new Wallet();


            objewallet.Status = "Unused";
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.ePinNo = dr["ePinNo"].ToString();

                    Objload.Package = dr["PinType"].ToString();

                    Objload.DisplayName = dr["tOwner"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["tRegTo"].ToString();
                    Objload.Status = dr["PinStatus"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstunusedpins = lst;
            }
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(objewallet);
        }

        [HttpPost]
        [ActionName("UnusedPins")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnusedPinsBy(Wallet objewallet)
        {

            objewallet.Status = "Unused";
            List<Wallet> lst = new List<Wallet>();
            objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
            DataSet ds = objewallet.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.ePinNo = dr["ePinNo"].ToString();

                    Objload.Package = dr["PinType"].ToString();

                    Objload.DisplayName = dr["tOwner"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["tRegTo"].ToString();
                    Objload.Status = dr["PinStatus"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstunusedpins = lst;
            }
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(objewallet);
        }

        public ActionResult TopUpByPin(string Id)
        {
            Wallet obj = new Wallet();
            obj.ePinNo = Id;
            return View(obj);
        }
        public ActionResult TopUpByPinAction(Wallet obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.TopupByEpinByAdmin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EpinTopup"] = "Id Toup Successfully";
                    }
                    else
                    {
                        TempData["EpinTopup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EpinTopup"] = ex.Message;
            }
            return RedirectToAction("TopUpByPin", "Admin");
        }
        public ActionResult GetMemberName(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillAmount(string ProductId)
        {
            Wallet obj = new Wallet();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Amount = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UsedPins()
        {
            Wallet objewallet = new Wallet();


            objewallet.Status = "Used";
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.ePinNo = dr["ePinNo"].ToString();

                    Objload.Package = dr["PinType"].ToString();

                    Objload.DisplayName = dr["tOwner"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["tRegTo"].ToString();
                    Objload.Status = dr["PinStatus"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstunusedpins = lst;
            }
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(objewallet);
        }
        [HttpPost]
        [ActionName("UsedPins")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UsedPinsBy(Wallet objewallet)
        {



            objewallet.Status = "Used";
            objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.ePinNo = dr["ePinNo"].ToString();

                    Objload.Package = dr["PinType"].ToString();

                    Objload.DisplayName = dr["tOwner"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["tRegTo"].ToString();
                    Objload.Status = dr["PinStatus"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstunusedpins = lst;
            }
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(objewallet);
        }

        public ActionResult TransferPin(string Epin)
        {
            Wallet obj = new Wallet();
            obj.ePinNo = Crypto.Decrypt(Epin);
            return View(obj);
        }

        public ActionResult TransferPinAction(Wallet obj)
        {
            try
            {
                // obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.TransferPin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["TransferPin"] = "Pin Transfer Successfull.";
                    }
                    else
                    {
                        TempData["TransferPin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["TransferPin"] = ex.Message;
            }

            return RedirectToAction("TransferPin");
        }
        #endregion PinManagement

        #region AdvancePayment
        public ActionResult AdvancePayment()
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("AdvancePayment")]
        [OnAction(ButtonName = "btnSavePayment")]
        public ActionResult SaveAdvancePayment(Wallet model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");

                DataSet ds = model.SaveAdvancePayment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Advance"] = "Payment saved successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Advance"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Advance"] = ex.Message;
            }
            return RedirectToAction("AdvancePayment");
        }

        public ActionResult AdvancePaymentReport()
        {
            Wallet model = new Wallet();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.AdvancePaymentReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Wallet> lstReport = new List<Wallet>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Wallet obj = new Wallet();
                        obj.LoginId = r["LoginID"].ToString();
                        obj.DisplayName = r["FirstName"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.PaymentDate = r["PaymentDate"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.TransactionNo = r["PayMode"].ToString();
                        obj.Description = r["Description"].ToString();
                        lstReport.Add(obj);
                    }
                    model.lstewalletledger = lstReport;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AdvancePaymentReport")]
        [OnAction(ButtonName = "btnDetails")]
        public ActionResult AdvancePaymentReportSearch(Wallet model)
        {

            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.AdvancePaymentReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Wallet> lstReport = new List<Wallet>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Wallet obj = new Wallet();
                        obj.LoginId = r["LoginID"].ToString();
                        obj.DisplayName = r["FirstName"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.PaymentDate = r["PaymentDate"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.Description = r["PayMode"].ToString();
                        lstReport.Add(obj);
                    }
                    model.lstewalletledger = lstReport;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        #endregion

        public virtual PartialViewResult Tree()
        {
            ViewBag.Fk_UserId = "1";
            return PartialView("_Tree");
        }

        #region PayPayout
        public ActionResult PayPayout()
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            Reports model = new Reports();

            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetPayPayout();
            ViewBag.Total = "0";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.IFSCCode = (r["IFSCCode"].ToString());
                    obj.BankName = (r["MemberBankName"].ToString());
                    obj.Fk_UserId = (r["Pk_UserId"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPayPayout(Reports model)
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            //model.LoginId = string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId;
            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            // model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetPayPayout();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.MemberAccNo = r["MemberAccNo"].ToString();
                    obj.IFSCCode = (r["IFSCCode"].ToString());
                    obj.BankName = (r["MemberBankName"].ToString());
                    obj.Fk_UserId = (r["Pk_UserId"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }


            return View(model);
        }

        //Export to Excel for Pay Payout
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcelPayout(Reports model)
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            //model.LoginId = string.IsNullOrEmpty(model.LoginId) ? null : model.LoginId;
            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            // model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetPayPayout();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "PayPayout" + ".xls";
                GridView GridView1 = new GridView();
                ds.Tables[0].Columns.Remove("Pk_UserID");
                ds.Tables[0].Columns.Remove("MemberBranch");
                ds.Tables[0].Columns.Remove("BankHolderName");
                //ds.Tables[0].Columns.Remove("TransactionDate");
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                //string style = @" .text { mso-number-format:\@; }  ";
                string style = @"<style> td { mso-number-format:\@; } </style> ";

                Response.Clear();
                // Response.AddHeader("content-disposition", "attachment;filename=MemberDetailsReport.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter s_Write = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter h_write = new HtmlTextWriter(s_Write);
                GridView1.ShowHeader = true;
                GridView1.RenderControl(h_write);
                Response.Write(style);
                Response.Write(s_Write.ToString());
                Response.End();

            }

            return null;
        }

        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Save")]
        public ActionResult PayPayoutAction(Reports model)
        {
            string hdrows2 = Request["hdRows2"].ToString();
            string amount = "";
            string description = "";
            string transactiono = "";
            string transactiondate = "";
            string Pk_PaidBoosterId_ = "";
            for (int i = 1; i < int.Parse(hdrows2); i++)
            {
                Pk_PaidBoosterId_ = Request["Fk_UserId_ " + i].ToString();
                amount = "";

                transactiono = Request["txttranno_ " + i].ToString();
                transactiondate = Request["txttransdate_ " + i].ToString();
                model.Amount = Request["txtamount_ " + i].ToString();
                model.Fk_UserId = Pk_PaidBoosterId_;

                model.TransactionNo = transactiono;
                DataSet ds = null;
                if (!string.IsNullOrEmpty(transactiondate))
                {
                    model.TransactionDate = transactiondate;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    ds = model.SavePayPayout();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                    }
                    else
                    {
                        // TempData["BoosterPay"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            TempData["PayPayout"] = "Paymnent Done";
            return RedirectToAction("PayPayout");
        }
        #endregion

        #region PaidPayout
        public ActionResult PaidPayout()
        {
            return View();
        }
        [HttpPost]
        [ActionName("PaidPayout")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPaidPayout(Wallet objewallet)
        {
            List<Wallet> lst = new List<Wallet>();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.GetPaidPayout();
            ViewBag.Total = "0";
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.LoginId = dr["Loginid"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.PaymentDate = dr["Paymentdate"].ToString();

                    Objload.Amount = dr["Amount"].ToString();
                    Objload.TransactionDate = dr["TransactionDate"].ToString();
                    Objload.TransactionNo = dr["TransactionNo"].ToString();
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(dr["Amount"].ToString());
                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return View(objewallet);
        }
        #endregion
		public ActionResult RejectKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<Reports> lst = new List<Reports>();

                Reports model = new Reports();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Rejected";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.RejectKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Rejected Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "Admin";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "Admin";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "Admin";
            }
            return RedirectToAction(FormName, Controller);
        }
    }
}
