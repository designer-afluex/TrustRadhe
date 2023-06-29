using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using TrustRadhe.Models;

namespace TrustRadhe.Controllers
{
    public class UserReportsController : Controller
    {

        public ActionResult UnPaidIncomes(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            objreports.LoginId = Session["LoginId"].ToString();
            DataSet ds = objreports.GetUnPaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();

                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                   

                    lst.Add(obj);
                }
                objreports.lstunpaidincomes = lst;


            }
            return View(objreports);
        }

        public ActionResult PayoutReport()
        {
            Profile payoutDetail = new Profile();
            List<Profile> lst1 = new List<Profile>();

            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Profile Obj = new Profile();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome=   r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }
        [HttpPost]
        [ActionName("PayoutReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutReportBy()
        {
            Profile payoutDetail = new Profile();
            List<Profile> lst1 = new List<Profile>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Profile Obj = new Profile();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }

        #region BoosterLedger

        public ActionResult BoosterLedger()
        {
            #region loadBoosterLedger
            List<Reports> lstVoucher = new List<Reports>();
            Reports model = new Reports();

            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetBoosterLedger();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports objUser = new Reports();
                    objUser.FK_InvestmentID = Crypto.Encrypt(r["PK_PaidBoosterID"].ToString());
                    objUser.MaturityDate = r["MaturityDate"].ToString();
                    objUser.UpgradtionDate = r["PaymentDate"].ToString();
                    objUser.Amount = r["Amount"].ToString();
                    objUser.TDSAmount = r["TDS"].ToString();
                    objUser.GrossAmount = r["GrossAmount"].ToString();
                    objUser.ProcessingFee = r["ProcessingCharge"].ToString();
                    objUser.Status = r["Status"].ToString();

                    lstVoucher.Add(objUser);
                }
                model.lstVoucherLedger = lstVoucher;
            }

            #endregion

            return View(model);
        }

        #endregion

        #region BoosterAchiever

        public ActionResult BoosterAchiever()
        {
            Reports model = new Reports();
            List<Reports> lst1 = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBoosterAchieverForAssociate();

            ViewBag.TotalAmount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.AchievementDate = r["AchievementDate"].ToString();
                    Obj.PermanentDate = r["PermanentDate"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CssClass = r["CssClass"].ToString();

                    ViewBag.TotalAmount = Convert.ToDecimal(ViewBag.TotalAmount) + Convert.ToDecimal(r["Amount"].ToString());
                    lst1.Add(Obj);
                }
                model.lsttopupreport = lst1;
            }

            return View(model);
        }

        #endregion

        public ActionResult PayoutReceipt(string lid, string pno)
        {
            Profile model = new Profile();
            try
            {
                model.LoginId = Crypto.Decrypt(lid);
                model.PayoutNo = Crypto.Decrypt(pno);

                ViewBag.CompanyName = SoftwareDetails.CompanyName;
                ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                ViewBag.Phone = SoftwareDetails.LandLine;
                ViewBag.Email = SoftwareDetails.EmailID;
                ViewBag.Website = SoftwareDetails.Website;

                DataSet ds = model.GetPayoutReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.PayoutNo = ds.Tables[0].Rows[0]["PayoutNo"].ToString();
                    model.ClosingDate = ds.Tables[0].Rows[0]["ClosingDate"].ToString();
                    model.BinaryIncome = ds.Tables[0].Rows[0]["BinaryIncome"].ToString();
                    model.DirectIncome = ds.Tables[0].Rows[0]["DirectIncome"].ToString();
                    model.GrossAmount = ds.Tables[0].Rows[0]["GrossAmount"].ToString();
                    model.TDSAmount = ds.Tables[0].Rows[0]["TDSAmount"].ToString();
                    model.ProcessingFee = ds.Tables[0].Rows[0]["ProcessingFee"].ToString();
                    model.NetAmount = ds.Tables[0].Rows[0]["NetAmount"].ToString();
                    model.LeadershipBonus = ds.Tables[0].Rows[0]["DirectLeaderShipBonus"].ToString();
                    model.ProductWallet = ds.Tables[0].Rows[0]["ProductWallet"].ToString();

                    model.FromDate = ds.Tables[1].Rows[0]["FromDate"].ToString();
                    model.ToDate = ds.Tables[1].Rows[0]["ToDate"].ToString();

                    ViewBag.Income = Convert.ToDecimal(model.BinaryIncome) + Convert.ToDecimal(model.LeadershipBonus);
                    ViewBag.Deduction = Convert.ToDecimal(model.TDSAmount) + Convert.ToDecimal(model.ProcessingFee) + Convert.ToDecimal(model.ProductWallet);
                    ViewBag.NetIncome = Convert.ToDecimal(ViewBag.Income) - Convert.ToDecimal(ViewBag.Deduction);

                    ViewBag.TDSPercent = Convert.ToDecimal(model.TDSAmount) * 100 / Convert.ToDecimal(ViewBag.Income);
                    ViewBag.TDSPercent = ViewBag.TDSPercent + "%";


                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        #region BusinessReport

        public ActionResult BusinessReport(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BusinessReportBy(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            List<Reports> lst1 = new List<Reports>();
            model.Leg = string.IsNullOrEmpty(model.Leg) ? null : model.Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginID"].ToString();
           
           // model.IsDownline = Request["Chk_"].ToString(); 
            DataSet ds11 = model.BusinessReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.Leg = r["Leg"].ToString();
                    Obj.ClosingDate = r["CalculationDate"].ToString(); 
                    Obj.NetAmount = r["AMount"].ToString();
                    Obj.LeadershipBonus = r["BV"].ToString();
                  
                    lst1.Add(Obj);
                }
                model.lstassociate = lst1;
                ViewBag.TotalNetAmount = double.Parse(ds11.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
                ViewBag.TotalBV = double.Parse(ds11.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }


            return View(model);
        }

        #endregion

        public ActionResult UnPaidProductIncomes(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            objreports.LoginId = Session["LoginId"].ToString();
            DataSet ds = objreports.GetUnPaidProductIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();

                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());


                    lst.Add(obj);
                }
                objreports.lstunpaidincomes = lst;


            }
            return View(objreports);
        }
        public ActionResult ProductPayoutReport()
        {
            Profile payoutDetail = new Profile();
            List<Profile> lst1 = new List<Profile>();

            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetProductPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Profile Obj = new Profile();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }
        [HttpPost]
        [ActionName("ProductPayoutReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPayoutReportBy()
        {
            Profile payoutDetail = new Profile();
            List<Profile> lst1 = new List<Profile>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            payoutDetail.LoginId = Session["LoginID"].ToString();
            DataSet ds11 = payoutDetail.GetProductPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Profile Obj = new Profile();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                payoutDetail.lstPayoutDetail = lst1;
            }
            return View(payoutDetail);
        }

        public ActionResult ProductPayoutReceipt(string lid, string pno)
        {
            Reports model = new Reports();
            try
            {
                model.LoginId = Crypto.Decrypt(lid);
                model.PayoutNo = Crypto.Decrypt(pno);

                ViewBag.CompanyName = SoftwareDetails.CompanyName;
                ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                ViewBag.Phone = SoftwareDetails.LandLine;
                ViewBag.Email = SoftwareDetails.EmailID;

                DataSet ds = model.GetProductPayoutReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.PayoutNo = ds.Tables[0].Rows[0]["PayoutNo"].ToString();
                    model.ClosingDate = ds.Tables[0].Rows[0]["ClosingDate"].ToString();
                    model.BinaryIncome = ds.Tables[0].Rows[0]["BinaryIncome"].ToString();
                    model.DirectIncome = ds.Tables[0].Rows[0]["DirectIncome"].ToString();
                    model.GrossAmount = ds.Tables[0].Rows[0]["GrossAmount"].ToString();
                    model.TDSAmount = ds.Tables[0].Rows[0]["TDSAmount"].ToString();
                    model.ProcessingFee = ds.Tables[0].Rows[0]["ProcessingFee"].ToString();
                    model.NetAmount = ds.Tables[0].Rows[0]["NetAmount"].ToString();
                    model.LeadershipBonus = ds.Tables[0].Rows[0]["DirectLeaderShipBonus"].ToString();
                    model.ProductWallet = ds.Tables[0].Rows[0]["ProductWallet"].ToString();

                    model.FromDate = ds.Tables[1].Rows[0]["FromDate"].ToString();
                    model.ToDate = ds.Tables[1].Rows[0]["ToDate"].ToString();

                    ViewBag.Income = Convert.ToDecimal(model.BinaryIncome) + Convert.ToDecimal(model.DirectIncome) + Convert.ToDecimal(model.LeadershipBonus);
                    ViewBag.Deduction = Convert.ToDecimal(model.TDSAmount) + Convert.ToDecimal(model.ProcessingFee) + Convert.ToDecimal(model.ProductWallet);
                    ViewBag.NetIncome = Convert.ToDecimal(ViewBag.Income) - Convert.ToDecimal(ViewBag.Deduction);

                    ViewBag.TDSPercent = Convert.ToDecimal(model.TDSAmount) * 100 / Convert.ToDecimal(ViewBag.Income);
                    ViewBag.TDSPercent = ViewBag.TDSPercent + "%";
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        #region BusinessReport

        public ActionResult ProductBusinessReport(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            return View(model);
        }
        [HttpPost]
        [ActionName("ProductBusinessReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetBusinessReportBy(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            List<Reports> lst1 = new List<Reports>();
            model.Leg = string.IsNullOrEmpty(model.Leg) ? null : model.Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginID"].ToString();

            // model.IsDownline = Request["Chk_"].ToString(); 
            DataSet ds11 = model.ProductBusinessReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.Leg = r["Leg"].ToString();
                    Obj.ClosingDate = r["CalculationDate"].ToString();
                    Obj.NetAmount = r["AMount"].ToString();
                    Obj.LeadershipBonus = r["BV"].ToString();

                    lst1.Add(Obj);
                }
                model.lstassociate = lst1;
                ViewBag.TotalNetAmount = double.Parse(ds11.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
                ViewBag.TotalBV = double.Parse(ds11.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }


            return View(model);
        }

        #endregion




        public ActionResult PaidPayoutDetails(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds11 = model.PaidPayoutDetailsList();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();

                    Obj.Amount = r["Amount"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentDate = r["Paymentdate"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.TransactionNo = r["TransactionNo"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    lst1.Add(Obj);
                }
                model.PaidPayoutlist = lst1;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("PaidPayoutDetails")]
        [OnAction(ButtonName = "btnDetails")]
        public ActionResult FilterPaidPayoutDetails(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds11 = model.PaidPayoutDetailsList();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();

                    Obj.Amount = r["Amount"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentDate = r["Paymentdate"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.TransactionNo = r["TransactionNo"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    lst1.Add(Obj);
                }
                model.PaidPayoutlist = lst1;
            }
            return View(model);
        }
		  public ActionResult TopupReport()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();
			 newdata.LoginId = Session["LoginId"].ToString();
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.PlotNumber = r["PlotNumber"].ToString();
                    Obj.Description = r["Description"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
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
            return View(newdata);
        }
        [HttpPost]
        [ActionName("TopupReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopupReportBy(Reports newdata)
        {

            List<Reports> lst1 = new List<Reports>();
			newdata.LoginId=Session["LoginId"].ToString();
            newdata.Package = newdata.Package == "0" ? null : newdata.Package;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.PlotNumber = r["PlotNumber"].ToString();
                    Obj.Description = r["Description"].ToString();
                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
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

            return View(newdata);
        }
        public ActionResult PrintTopUp(string ToLoginID)
        {
            List<Reports> list = new List<Reports>();
            Reports model = new Reports();
            if (ToLoginID != null)
            {
                model.ToLoginID = ToLoginID;
                try
                {
                    DataSet ds = model.PrintTopUp();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Reports obj = new Reports();

                            obj.FK_InvestmentID = r["Pk_InvestmentId"].ToString();
                            //obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            //obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.Quantity = r["Quantity"].ToString();
                            obj.MRP = r["Amount"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["Amount"].ToString();
                            //obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.HSNCode = r["HSNCode"].ToString();

                            ViewBag.OrderNo = r["Pk_InvestmentId"].ToString();

                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString();

                            ViewBag.CompanyName = SoftwareDetails.CompanyName;
                            ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                            ViewBag.Pin1 = SoftwareDetails.Pin1;
                            ViewBag.GSTNO = SoftwareDetails.GSTNO;
                            ViewBag.State1 = SoftwareDetails.State1;
                            ViewBag.City1 = SoftwareDetails.City1;
                            ViewBag.ContactNo = SoftwareDetails.ContactNo;
                            ViewBag.LandLine = SoftwareDetails.LandLine;
                            ViewBag.Website = SoftwareDetails.Website;
                            ViewBag.EmailID = SoftwareDetails.EmailID;
                            list.Add(obj);

                        }
                        model.lsttopupreport = list;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }
		public ActionResult DCMIReport(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds11 = model.GetDCMIReportForAssociate();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Pk_DCMIId = r["Pk_DCMIId"].ToString();
                    Obj.Month = r["Month"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.TotalMatching = r["TotalMatching"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.DCMIIncome = r["DCMIIncome"].ToString();
                    Obj.TotalBV = r["TotalBV"].ToString();
                    Obj.DCMIGrossIncome = r["DCMIGrossIncome"].ToString();
                    Obj.TDS = r["TDS"].ToString();
                    Obj.AdminCharge = r["AdminCharge"].ToString();

                    lst1.Add(Obj);
                }
                model.lstdcmireport = lst1;
            }
            return View(model);
		}
		[HttpPost]
        [ActionName("DCMIReport")]
        [OnAction(ButtonName = "Search")]
		public ActionResult GetDCMIReport(Reports model)
		{
		     
            List<Reports> lst1 = new List<Reports>();
			model.LoginId=Session["LoginId"].ToString();
            DataSet ds11 = model.GetDCMIReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Pk_DCMIId = r["Pk_DCMIId"].ToString();
                    Obj.Month = r["Month"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.TotalMatching = r["TotalMatching"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.DCMIIncome = r["DCMIIncome"].ToString();
                    Obj.TotalBV = r["TotalBV"].ToString();
                    Obj.DCMIGrossIncome = r["DCMIGrossIncome"].ToString();
                    Obj.TDS = r["TDS"].ToString();
                    Obj.AdminCharge = r["AdminCharge"].ToString();
                    lst1.Add(Obj);
                }
                model.lstdcmireport = lst1;
            }
            return View(model);
		}
    }
}
