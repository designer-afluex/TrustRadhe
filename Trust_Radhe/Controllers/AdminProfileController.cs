using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace TrustRadhe.Controllers
{
    public class AdminProfileController : AdminBaseController
    {
        #region BoosterAchieverCurrent

        public ActionResult BoosterAchieverCurrent()
        {
            return View();
        }
        [HttpPost]
        [ActionName("BoosterAchieverCurrent")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BoosterAchieverCurrent(Reports model)
        {
            List<Reports> lst = new List<Reports>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBoosterAchieverCurrent();
            ViewBag.Total = "0";
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.AchievementDate = r["AchievementDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    obj.EncryptName = Crypto.Encrypt((r["Name"].ToString()));
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            return View(model);
        }

        #endregion

        #region PayBoosterAchieverCurrent

        public ActionResult PayBoosterAchieverCurrent()
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            Reports model = new Reports();
            List<Reports> lst = new List<Reports>();
            // model.LoginId = Session["LoginId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPayBoosterAchieverCurrent();

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
                    obj.MaturityDate = (r["MaturityDate"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    obj.Description = (r["Description"].ToString());
                    obj.Pk_PaidBoosterId = (r["Pk_PaidBoosterId"].ToString());
                    obj.TransactionDate = (r["TransactionDate"].ToString());
                    obj.TransactionNo = (r["TransactionNo"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            Session["Lst"] = model.lstassociate;
            return View(model);
        }
        [HttpPost]
        [ActionName("PayBoosterAchieverCurrent")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult PayBoosterAchieverCurrent(Reports model)
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPayBoosterAchieverCurrent();
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
                    obj.MaturityDate = (r["MaturityDate"].ToString());
                    obj.Amount = (r["Amount"].ToString());
                    obj.Description = (r["Description"].ToString());
                    obj.Pk_PaidBoosterId = (r["Pk_PaidBoosterId"].ToString());
                    obj.TransactionDate = (r["TransactionDate"].ToString());
                    obj.TransactionNo = (r["TransactionNo"].ToString());
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["Amount"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            Session["Lst"] = model.lstassociate;
            return View(model);
        }

        [HttpPost]
        [ActionName("PayBoosterAchieverCurrent")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcelBoosterCurrent(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            // model.LoginId = Session["LoginId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPayBoosterAchieverCurrent();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "PayBoosterCurrent.xls";
                GridView GridView1 = new GridView();
                ds.Tables[0].Columns.Remove("Pk_PaidBoosterId");
                ds.Tables[0].Columns.Remove("Description");
                ds.Tables[0].Columns.Remove("TransactionNo");
                ds.Tables[0].Columns.Remove("TransactionDate");
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
        [ActionName("PayBoosterAchieverCurrent")]
        [OnAction(ButtonName = "Save")]
        public ActionResult PayBoosterAchieverCurrentAction(Reports model)
        {
            string hdrows2 = Request["hdRows2"].ToString();
            string amount = "";
            string description = "";
            string transactiono = "";
            string transactiondate = "";
            string Pk_PaidBoosterId_ = "";
            for (int i = 1; i < int.Parse(hdrows2); i++)
            {
                Pk_PaidBoosterId_ = Request["Pk_PaidBoosterId_ " + i].ToString();
                amount = "";
                description = Request["txtdecription_ " + i].ToString();
                transactiono = Request["txttranno_ " + i].ToString();
                transactiondate = Request["txttransdate_ " + i].ToString();
                model.Amount = amount;
                model.Pk_PaidBoosterId = Pk_PaidBoosterId_;
                model.Description = description;
                model.TransactionNo = transactiono;
                DataSet ds = null;
                if (!string.IsNullOrEmpty(transactiondate))
                {

                    model.TransactionDate = transactiondate;

                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    ds = model.PayBooster();
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
            TempData["BoosterPay"] = "Paymnent Done";
            return RedirectToAction("PayBoosterAchieverCurrent");
        }


        #endregion

        #region PaidBooster
        public ActionResult PaidBooster()
        {
            return View();
        }
        [HttpPost]
        [ActionName("PaidBooster")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPaidBooster(Wallet objewallet)
        {
            List<Wallet> lst = new List<Wallet>();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            objewallet.PaymentFromDate = string.IsNullOrEmpty(objewallet.PaymentFromDate) ? null : Common.ConvertToSystemDate(objewallet.PaymentFromDate, "dd/MM/yyyy");
            objewallet.PaymentToDate = string.IsNullOrEmpty(objewallet.PaymentToDate) ? null : Common.ConvertToSystemDate(objewallet.PaymentToDate, "dd/MM/yyyy");

            DataSet ds = objewallet.GetPaidBooster();
            ViewBag.Total = "0";
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.LoginId = dr["Loginid"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.PaymentDate = dr["Paymentdate"].ToString();
                    Objload.Description = dr["Description"].ToString();
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

        public ActionResult AssociateList(Reports model)
        {
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            List<Reports> lst = new List<Reports>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password =Crypto.Decrypt( r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateListBy(Reports model)
        {
            List<Reports> lst = new List<Reports>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            return View(model);
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
        
        public ActionResult UpdateAssociateProfile(string LoginID)
        {
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
            #region Relation
            List<SelectListItem> Relation = Common.BindRealation();
            ViewBag.Relation = Relation;
            #endregion
            Profile obj = new Profile();
            obj.LoginId = LoginID;
           
            DataSet ds = obj.GetUserProfile();
            
            if (ds != null && ds.Tables.Count > 0)
            {
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString(); 
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.Relation = ds.Tables[0].Rows[0]["GaurdianRelation"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.RealtionName = ds.Tables[0].Rows[0]["GaurdianName"].ToString();
                obj.AccountHolder = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                // return View(obj);
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("UpdateAssociateProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAssociateProfile(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAssociateProfileByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfilebyadmin"] = "Profile updated successfully..";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfilebyadmin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfilebyadmin"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BlockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.BlockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UnblockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UnblockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult AdminProfile()
        {
            Profile obj = new Profile();
            obj.PK_UserID = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.GetAdminProfile();


            if (ds != null && ds.Tables.Count > 0)
            {
                obj.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                obj.LoginId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                obj.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();

            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("AdminProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAdminProfile(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAdminProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Profile updated successfully..";
                        FormName = "AdminProfile";
                        Controller = "Profile";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AdminProfile";
                        Controller = "Profile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "AdminProfile";
                Controller = "Profile";
            }
            return RedirectToAction(FormName, Controller);
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

        public ActionResult WelcomeLetter(string id)
        {
            Reports model = new Reports();
            model.LoginId = id;
            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.RefNo = ds.Tables[0].Rows[0]["RefNo"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.DATE = ds.Tables[0].Rows[0]["DATE"].ToString();
            }
            return View();
        }
        public ActionResult ActivateUser(string FK_UserID)
        {
            Profile model = new Profile();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.ProductID = "1";
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ActivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User activated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminProfile");
        }
        public ActionResult DeactivateUser(string lid)
        {
            Profile model = new Profile();
            try
            {
                model.LoginId = lid;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeactivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User deactivated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminProfile");
        }


      


    }
}
