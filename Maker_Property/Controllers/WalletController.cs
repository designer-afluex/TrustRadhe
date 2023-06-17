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
    public class WalletController : BaseController
    {

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

        #region Ewallet
        public ActionResult EWalletRequest()
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            Wallet obj = new Wallet();
            obj.LoginId = Session["LoginId"].ToString();
            return View(obj);
        }
        [HttpPost]
        public ActionResult SaveEwalletRequest(Wallet obj, HttpPostedFileBase fileProfilePicture)
        {

            try
            {

                obj.AddedBy = Session["Pk_UserId"].ToString();
                obj.DocumentImg = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImg)));


                DataSet ds = obj.SaveEWalletRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Ewallet"] = "Ewallet Request Save Successfully.";

                    }
                    else
                    {
                        TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ewallet"] = ex.Message;

            }
            return RedirectToAction("EWalletRequest", "Wallet");
        }

        public ActionResult EwalletRequestList()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetEWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.RequestCode = dr["RequestCode"].ToString();
                    Objload.PaymentMode = dr["PaymentMode"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.DocumentImg = dr["ImageURL"].ToString();
                    Objload.BankName = dr["BankName"].ToString();
                    Objload.BankBranch = dr["BankBranch"].ToString();
                    Objload.DDChequeNo = dr["ChequeDDNo"].ToString();
                    Objload.DDChequeDate = dr["ChequeDDDate"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }

        public ActionResult EwalletLedger()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.EwalletLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstewalletledger = lst;
            }
            return View(objewallet);
        }
        public ActionResult TransferEwalletToEwallet()
        {
            Wallet obj = new Wallet();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetEwalletBalnce();
            obj.EwalletBalance = ds.Tables[0].Rows[0]["Balance"].ToString();
            return View(obj);
        }
        public ActionResult TransferEwallet(Wallet obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_UserId"].ToString();

                DataSet ds = obj.TransferEwalletToEwallet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["TransferEwallet"] = "Ewallet Transfer Save Successfully.";

                    }
                    else
                    {
                        TempData["TransferEwallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["TransferEwallet"] = ex.Message;

            }
            return RedirectToAction("TransferEwalletToEwallet", "Wallet");

        }
        #endregion Ewallet

        #region Payout
        public ActionResult PayoutRequest()
        {
            Wallet obj = new Wallet();
            obj.LoginId = Session["LoginId"].ToString();
            obj.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds = obj.GetpayoutBalance();
            obj.PayoutBalance = ds.Tables[0].Rows[0]["Balance"].ToString();
            return View(obj);
        }
        public ActionResult SavePayoutRequest(Wallet obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_UserId"].ToString();

                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Payoutwallet"] = "Payout Request Save Successfully.";

                    }
                    else
                    {
                        TempData["Payoutwallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Payoutwallet"] = ex.Message;

            }
            return RedirectToAction("PayoutRequest", "Wallet");
        }
        public ActionResult PayoutRequestList()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetPayoutRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Amount = dr["Amount"].ToString();

                    Objload.Status = dr["Status"].ToString();

                    Objload.AddedOn = dr["RequestedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstPayoutrequest = lst;
            }
            return View(objewallet);
        }
        public ActionResult PayoutLedger()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.PayoutLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["TransactionDate"].ToString();
                    Objload.PayoutBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return View(objewallet);
        }
        [HttpPost]
        [ActionName("PayoutLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutLedgerBy(Wallet objewallet)
        {

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.PayoutLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["TransactionDate"].ToString();
                    Objload.PayoutBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            return View(objewallet);
        }
        #endregion Payout

        #region ProductWallet
        public ActionResult ProductWalletRequest()
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            Wallet obj = new Wallet();
            obj.LoginId = Session["LoginId"].ToString();
            return View(obj);
        }
        [HttpPost]
        public ActionResult SaveProductWalletRequest(Wallet obj, HttpPostedFileBase fileProfilePicture)
        {
            try
            {
                obj.AddedBy = Session["Pk_UserId"].ToString();
                obj.DocumentImg = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImg)));


                DataSet ds = obj.SaveProductWalletRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["ProductWallet"] = "Product Wallet Request Saved Successfully.";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["ProductWallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ProductWallet"] = ex.Message;
            }
            return RedirectToAction("ProductWalletRequest", "Wallet");
        }
        public ActionResult ProductWalletRequestList()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetProductWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.RequestCode = dr["RequestCode"].ToString();
                    Objload.PaymentMode = dr["PaymentMode"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.DocumentImg = dr["ImageURL"].ToString();
                    Objload.BankName = dr["BankName"].ToString();
                    Objload.BankBranch = dr["BankBranch"].ToString();
                    Objload.DDChequeNo = dr["ChequeDDNo"].ToString();
                    Objload.DDChequeDate = dr["ChequeDDDate"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }
        public ActionResult ProductWalletLedger()
        {
            Wallet objewallet = new Wallet();

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.ProductWalletLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.ProductWalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstewalletledger = lst;
            }
            return View(objewallet);
        }
        #endregion

        public ActionResult Topup()
        {
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

            Wallet obj = new Wallet();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetEwalletBalnce();
            obj.EwalletBalance = ds.Tables[0].Rows[0]["Balance"].ToString();
            return View(obj);
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

        public ActionResult TopUpEwallet(Wallet obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_UserId"].ToString();

                DataSet ds = obj.TopUpIdByEWallet();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Topup"] = "TopUp Done successfully";

                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string Product = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        try
                        {
                            string str = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Your login ID  " + ds.Tables[0].Rows[0]["LoginId"].ToString() + "   is top up by e Wallet by product " + ds.Tables[0].Rows[0]["ProductName"].ToString() + ".";

                            BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            { TempData["Topup"] = ex.Message; }
            return RedirectToAction("Topup", "Wallet");
        }

        public ActionResult UnusedPins()
        {
            Wallet objewallet = new Wallet();
            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

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

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
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

        public ActionResult UsedPins()
        {
            Wallet objewallet = new Wallet();
            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();

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

            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            objewallet.Package = objewallet.Package == "0" ? null : objewallet.Package;
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
                obj.AddedBy = Session["PK_UserId"].ToString();

                DataSet ds = obj.TopupByEpin();
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
            return RedirectToAction("TopUpByPin", "Wallet");
        }

    }
}
