using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;

namespace TrustRadhe.Controllers
{
    public class SettingController : AdminBaseController
    {
        //
        // GET: /Setting/

        public ActionResult ChangeAssociatePassword()
        {
            return View();
        }
        
        public ActionResult ChangeAssPassword(Setting obj)
        {
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ChangeAssociatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangeAsspassword"] = "Password Changed successfully";
                        
                    }
                    else
                    {
                        TempData["ChangeAsspassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangeAsspassword"] = ex.Message;
            }
            return RedirectToAction("ChangeAssociatePassword", "Setting");
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

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangeAdminPassword(Setting obj)
        {
            try
            {
                obj.LoginId = Session["LoginId"].ToString();
                DataSet ds = obj.ChangeAdminPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangeAsspassword"] = "Password Changed successfully";
                    }
                    else
                    {
                        TempData["ChangeAsspassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangeAsspassword"] = ex.Message;
            }
            return RedirectToAction("ChangePassword", "Setting");
        }

    }
}
