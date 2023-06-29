using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using System.Data;

namespace TrustRadhe.Controllers
{
    public class ProfileController : BaseController
    {
        //
        // GET: /Profile/

        public ActionResult ChangePassword()
        {
            List<SelectListItem> ddlPasswordType = Common.BindPasswordType();
            ViewBag.ddlPasswordType = ddlPasswordType;
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePassword(Password obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_userId"].ToString();
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangePassword"] = "Password updated successfully..";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["ChangePassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "Profile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePassword"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UpdateAssociateProfile(string LoginID)
        {
            //Profile objProfile = new Profile();
            Profile obj = new Profile();
            obj.LoginId = LoginID;
            DataSet ds = obj.GetUserProfile();

            
            if (ds != null && ds.Tables.Count > 0)
            {
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();

                // return View(obj);
            }
            return View(obj);
        }


    }
}
