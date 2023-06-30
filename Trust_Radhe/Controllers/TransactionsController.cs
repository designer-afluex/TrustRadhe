using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrustRadhe.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrustRadhe.Filter;

namespace TrustRadhe.Controllers
{
    public class TransactionsController : AdminBaseController
    {

        #region UpdateMemberLogin
        public ActionResult UpdateMemberLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateMemberLogin(Transactions obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateMemberLogin();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["UpdateMemberLogin"] = "Login ID updated successfully";
                        FormName = "UpdateMemberLogin";
                        Controller = "Transactions";
                    }
                    else
                    {
                        TempData["UpdateMemberLogin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UpdateMemberLogin";
                        Controller = "Transactions";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateMemberLogin"] = ex.Message;
                FormName = "UpdateMemberLogin";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetMemberDetails(string LoginID)
        {
            Common obj = new Common();
            obj.ReferBy = LoginID;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["Fullname"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid Login ID"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ActivateDeactivateUser
        public ActionResult ActivateUser(Transactions model)
        {
            List<Transactions> lst = new List<Transactions>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Transactions obj = new Transactions();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.TemPermanent = (r["TemPermanent"].ToString());
                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            return View(model);
        }

        public ActionResult ActivateUserByAdmin(string MemberID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Transactions obj = new Transactions();
                obj.LoginID = MemberID;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ActivateUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ActivateUser"] = "Associate activated successfully";
                        FormName = "ActivateUser";
                        Controller = "Transactions";
                    }
                    else
                    {
                        TempData["ActivateUser"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ActivateUser";
                        Controller = "Transactions";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ActivateUser"] = ex.Message;
                FormName = "ActivateUser";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult DeactivateUserByAdmin(string MemberID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Transactions obj = new Transactions();
                obj.LoginID = MemberID;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeactivateUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["ActivateUser"] = "Associate Deactivated successfully";
                        FormName = "ActivateUser";
                        Controller = "Transactions";
                    }
                    else
                    {
                        TempData["ActivateUser"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ActivateUser";
                        Controller = "Transactions";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ActivateUser"] = ex.Message;
                FormName = "ActivateUser";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region eWalletDeduction
        public ActionResult EwalletDeduction()
        {
            List<SelectListItem> TransactionType = Common.TransactionType();
            ViewBag.ddlTransactionType = TransactionType;
            return View();
        }

        public ActionResult DeductEwallet(Transactions obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.EwalletDeduction();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        if (obj.TransactionAction == "Debit")
                        {
                            TempData["ewallet"] = "Ewallet Wallet Debited Successfully.";
                        }
                        else
                        {
                            TempData["ewallet"] = "Ewallet Wallet Credited Successfully.";
                        }
                        FormName = "EwalletDeduction";
                        Controller = "Transactions";
                    }
                    else
                    {
                        TempData["ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EwalletDeduction";
                        Controller = "Transactions";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ewallet"] = ex.Message;
                FormName = "EwalletDeduction";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region PayoutWalletDeduction
        public ActionResult PayoutDeduction()
        {
            List<SelectListItem> TransactionType = Common.TransactionType();
            ViewBag.ddlTransactionType = TransactionType;
            return View();
        }

        public ActionResult PayoutWalletDeduction(Transactions obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.PayoutDeduction();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        if (obj.TransactionAction == "Debit")
                        {
                            TempData["pwallet"] = "Payout Wallet Debited Successfully.";
                        }
                        else
                        {
                            TempData["pwallet"] = "Payout Wallet Credited Successfully.";
                        }
                        FormName = "PayoutDeduction";
                        Controller = "Transactions";
                    }
                    else
                    {
                        TempData["pwallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PayoutDeduction";
                        Controller = "Transactions";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["pwallet"] = ex.Message;
                FormName = "PayoutDeduction";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region distributePayment

        public ActionResult DistributePayment()
        {
            Transactions model = new Transactions();
            List<Transactions> lst = new List<Transactions>();

            ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetDitributePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Transactions obj = new Transactions();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.BinaryIncome = r["BinaryIncome"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.GrossIncome = (r["GrossIncome"].ToString());
                    obj.TDS = (r["TDS"].ToString());
                    obj.NetIncome = (r["NetIncome"].ToString());

                    obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    ViewBag.Binary = Convert.ToDecimal(ViewBag.Binary) + Convert.ToDecimal(r["BinaryIncome"].ToString());
                    ViewBag.Direct = Convert.ToDecimal(ViewBag.Direct) + Convert.ToDecimal(r["DirectIncome"].ToString());
                    ViewBag.Gross = Convert.ToDecimal(ViewBag.Gross) + Convert.ToDecimal(r["GrossIncome"].ToString());
                    ViewBag.TDS = Convert.ToDecimal(ViewBag.TDS) + Convert.ToDecimal(r["TDS"].ToString());
                    ViewBag.Processing = Convert.ToDecimal(ViewBag.Processing) + Convert.ToDecimal(r["Processing"].ToString());
                    ViewBag.NetIncome = Convert.ToDecimal(ViewBag.NetIncome) + Convert.ToDecimal(r["NetIncome"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
                
            }
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }

        public ActionResult DistiributePayemntToMembers(Transactions obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.ClosingDate = Common.ConvertToSystemDate(obj.ClosingDate, "dd/MM/yyyy");
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.AutoDistributePayment();

                TempData["DistributePayment"] = "Payment distributed successfully";
                FormName = "DistributePayment";
                Controller = "Transactions";
            }
            catch (Exception ex)
            {
                TempData["DistributePayment"] = ex.Message;
                FormName = "DistributePayment";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("DistiributePayemntToMembers")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcel()
        {
            Transactions model = new Transactions();
            List<Transactions> lst = new List<Transactions>();

            ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetDitributePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "DistributePayment.xls";
                GridView GridView1 = new GridView();
                //ds.Tables[0].Columns.Remove("Pk_PaidBoosterId");
                //ds.Tables[0].Columns.Remove("Description");
                //ds.Tables[0].Columns.Remove("TransactionNo");
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

        #endregion

        #region ProductDistributePayment

        public ActionResult ProductDistributePayment()
        {
            Transactions model = new Transactions();
            List<Transactions> lst = new List<Transactions>();

            ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetProductDitributePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Transactions obj = new Transactions();
                    obj.LoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.BinaryIncome = r["BinaryIncome"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.GrossIncome = (r["GrossIncome"].ToString());
                    obj.TDS = (r["TDS"].ToString());
                    obj.Processing = (r["Processing"].ToString());
                    obj.NetIncome = (r["NetIncome"].ToString());

                    obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    ViewBag.Binary = Convert.ToDecimal(ViewBag.Binary) + Convert.ToDecimal(r["BinaryIncome"].ToString());
                    ViewBag.Direct = Convert.ToDecimal(ViewBag.Direct) + Convert.ToDecimal(r["DirectIncome"].ToString());
                    ViewBag.Gross = Convert.ToDecimal(ViewBag.Gross) + Convert.ToDecimal(r["GrossIncome"].ToString());
                    ViewBag.TDS = Convert.ToDecimal(ViewBag.TDS) + Convert.ToDecimal(r["TDS"].ToString());
                    ViewBag.Processing = Convert.ToDecimal(ViewBag.Processing) + Convert.ToDecimal(r["Processing"].ToString());
                    ViewBag.NetIncome = Convert.ToDecimal(ViewBag.NetIncome) + Convert.ToDecimal(r["NetIncome"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;

            }
            model.LastClosingDate = ds.Tables[1].Rows[0]["ClosingDate"].ToString();
            model.PayoutNo = ds.Tables[1].Rows[0]["PayoutNo"].ToString();
            return View(model);
        }

        public ActionResult ProductDistributePaymentToMembers(Transactions obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.ClosingDate = Common.ConvertToSystemDate(obj.ClosingDate, "dd/MM/yyyy");
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.AutoProductDistributePayment();

                TempData["DistributePayment"] = "Payment distributed successfully";
                FormName = "ProductDistributePayment";
                Controller = "Transactions";
            }
            catch (Exception ex)
            {
                TempData["ProductDistributePayment"] = ex.Message;
                FormName = "DistributePayment";
                Controller = "Transactions";
            }

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("ProductDistributePaymentToMembers")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ProductExportToExcel()
        {
            Transactions model = new Transactions();
            List<Transactions> lst = new List<Transactions>();

            ViewBag.Binary = ViewBag.Direct = ViewBag.Gross = ViewBag.TDS = ViewBag.Processing = ViewBag.NetIncome = 0;
            DataSet ds = model.GetProductDitributePaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "DistributePayment.xls";
                GridView GridView1 = new GridView();
                //ds.Tables[0].Columns.Remove("Pk_PaidBoosterId");
                //ds.Tables[0].Columns.Remove("Description");
                //ds.Tables[0].Columns.Remove("TransactionNo");
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

        #endregion

    }
}
