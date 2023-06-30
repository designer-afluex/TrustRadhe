using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using System.Data;
using TrustRadhe.Filter;
using BusinessLayer;

namespace TrustRadhe.Controllers
{
    public class ForgotPasswordController : Controller
    {

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        [OnAction(ButtonName = "btnGetPassword")]
        //Validate Data and Send Password to Associate's Mobile
        public ActionResult ValidateData(ForgotPassword obj)
        {

            DataSet ds = obj.ValidateData();

            if (ds != null && ds.Tables.Count > 0)
            {
                //ViewBag["OTP"] = ds.Tables[0].Rows[0]["OTP"].ToString();
                try
                {



                    string passwordRecoveryMessage = BLSMS.ForgetPassword(ds.Tables[0].Rows[0]["FirstName"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                    string str = BLSMS.SendSMS2(SMSCredential.UserName, SMSCredential.Password, SMSCredential.SenderId, ds.Tables[0].Rows[0]["Mobile"].ToString(), passwordRecoveryMessage);
                }
                catch { }

                TempData["recoverPassword"] = "Password is send on your registered mobile no.";

            }
            else
            {
                TempData["recoverPassword"] = "Invalid Login ID or Mobile Number..";
            }
            return RedirectToAction("ForgotPassword", "ForgotPassword");
        }

    }
}
