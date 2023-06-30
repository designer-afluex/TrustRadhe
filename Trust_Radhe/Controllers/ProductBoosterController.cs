using TrustRadhe.Filter;
using TrustRadhe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrustRadhe.Controllers
{
    public class ProductBoosterController : AdminBaseController
    {
        // GET: ProductBooster
        #region ProductBooster
        #region BoosterAchieverCurrent

        public ActionResult ProductBoosterAchiever()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ProductBoosterAchiever")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult ProductBoosterAchieverDetails(Reports model)
        {
            List<Reports> lst = new List<Reports>();

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.ProductBoosterAchieverDetails();
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

        #region PayProductBoosterAchiever 

        public ActionResult PayProductBoosterAchiever()
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
            DataSet ds = model.GetPayProductBoosterAchiever();

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
        [ActionName("PayProductBoosterAchiever")]
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
            DataSet ds = model.GetPayProductBoosterAchiever();
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
        [ActionName("PayProductBoosterAchiever")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcelBoosterCurrent(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            // model.LoginId = Session["LoginId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPayProductBoosterAchiever();

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
        [ActionName("PayProductBoosterAchiever")]
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
            return RedirectToAction("PayProductBoosterAchiever");
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

            DataSet ds = objewallet.GetPaidProductBooster();
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

        #endregion
    }
}