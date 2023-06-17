using TrustRadhe.Filter;
using TrustRadhe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace TrustRadhe.Controllers
{
    public class AdminReportsController : AdminBaseController
    {

        public ActionResult GetAllMessages()
        {

            DashBoard newdata = new DashBoard();
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


            return View(newdata);
        }

        public ActionResult SaveMessages(string Message, string MessageBy, string Fk_UserId)
        {
            DashBoard obj = new DashBoard();
            try
            {
                obj.Message = Message;
                obj.MessageBy = MessageBy;
                obj.Fk_UserId = Fk_UserId;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveMessage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Result = "1";
                    }
                    else
                    {
                        obj.Result = "Message Not Send";
                    }
                }
                else
                {
                    obj.Result = "Message Not Send";
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopupReport()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();
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
					Obj.TransactionNo = r["TransactionNo"].ToString();
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
					Obj.TransactionNo = r["TransactionNo"].ToString();
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
        //public ActionResult PrintTopUp(string ToLoginID)
        //{
        //    List<Reports> list = new List<Reports>();
        //    Reports model = new Reports();
        //    if (ToLoginID != null)
        //    {

        //        model.ToLoginID = ToLoginID;
        //        try
        //        {
        //            DataSet ds = model.PrintTopUp();
        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    Reports obj = new Reports();

        //                    obj.Package = r["Package"].ToString();
        //                    obj.Amount = r["Amount"].ToString();
        //                    obj.Quantity = r["Quantity"].ToString();
        //                    obj.ProductName = r["ProductName"].ToString();
        //                    obj.HSNCode = r["HSNCode"].ToString();
        //                    ViewBag.RecieptType = ds.Tables[0].Rows[0]["RecieptType"].ToString();
        //                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                    ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //                    ViewBag.UpgradtionDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
        //                    ViewBag.PrintingDate = ds.Tables[0].Rows[0]["PrintingDate"].ToString();
        //                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
        //                    ViewBag.ProductPrice = ds.Tables[1].Rows[0]["PinAMount"].ToString();
        //                    ViewBag.ProductPriceInWords = ds.Tables[1].Rows[0]["ProductPriceInWords"].ToString();
        //                    ViewBag.TaxAmount = ds.Tables[1].Rows[0]["TaxAmount"].ToString();
        //                    ViewBag.Taxable = ds.Tables[1].Rows[0]["Taxable"].ToString();
        //                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
        //                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
        //                    ViewBag.Pin1 = SoftwareDetails.Pin1;
        //                    ViewBag.State1 = SoftwareDetails.State1;
        //                    ViewBag.City1 = SoftwareDetails.City1;
        //                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
        //                    ViewBag.LandLine = SoftwareDetails.LandLine;
        //                    ViewBag.Website = SoftwareDetails.Website;
        //                    ViewBag.EmailID = SoftwareDetails.EmailID;
        //                    list.Add(obj);

        //                }


        //                model.lsttopupreport = list;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //    }
        //    return View(model);
        //}

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
                            ViewBag.UpgradtionDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();


                            ViewBag.Pincode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                            ViewBag.State = ds.Tables[3].Rows[0]["statename"].ToString();
                            ViewBag.City = ds.Tables[3].Rows[0]["Districtname"].ToString();

                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
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

        public ActionResult TransactionLog()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = newdata.GetTransactionLog();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Action = r["Action"].ToString();
                    Obj.Remarks = r["Remarks"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttransactionlog = lst1;
            }


            return View(newdata);
        }
        [HttpPost]
        [ActionName("TransactionLog")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransactionLogBy(Reports newdata)
        {

            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = newdata.GetTransactionLog();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Action = r["Action"].ToString();
                    Obj.Remarks = r["Remarks"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttransactionlog = lst1;
            }


            return View(newdata);
        }


        public ActionResult AssociateIncomeReport()
        {
            Reports incomeReport = new Reports();
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = incomeReport.GetIncomeReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.TransactionDate = r["DATE"].ToString();
                    Obj.FromName = r["FromName"].ToString();
                    Obj.FromLoginId = r["FromLoginID"].ToString();
                    Obj.ToName = r["ToName"].ToString();
                    Obj.ToLoginID = r["ToLoginID"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.IncomeType = r["IncomeType"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lst1.Add(Obj);
                }
                incomeReport.lsttopupreport = lst1;
            }


            #region PaidStatus
            List<SelectListItem> PaidStatus = Common.PaidStatus();
            ViewBag.PaidStatus = PaidStatus;
            #endregion PaidStatus

            return View(incomeReport);
        }

        [HttpPost]
        [ActionName("AssociateIncomeReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateIncomeReportBy(Reports incomeReport)
        {

            List<Reports> lst1 = new List<Reports>();
            incomeReport.FromDate = string.IsNullOrEmpty(incomeReport.FromDate) ? null : Common.ConvertToSystemDate(incomeReport.FromDate, "dd/MM/yyyy");
            incomeReport.ToDate = string.IsNullOrEmpty(incomeReport.ToDate) ? null : Common.ConvertToSystemDate(incomeReport.ToDate, "dd/MM/yyyy");
            if (incomeReport.Status == "null")
            {
                incomeReport.Status = null;
            }
            incomeReport.ToLoginID = string.IsNullOrEmpty(incomeReport.ToLoginID) ? null : incomeReport.ToLoginID.Trim();
            DataSet ds11 = incomeReport.GetIncomeReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.TransactionDate = r["DATE"].ToString();
                    Obj.FromName = r["FromName"].ToString();
                    Obj.FromLoginId = r["FromLoginID"].ToString();
                    Obj.ToName = r["ToName"].ToString();
                    Obj.ToLoginID = r["ToLoginID"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.IncomeType = r["IncomeType"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lst1.Add(Obj);
                }
                incomeReport.lsttopupreport = lst1;
            }
            #region PaidStatus
            List<SelectListItem> PaidStatus = Common.PaidStatus();
            ViewBag.PaidStatus = PaidStatus;
            #endregion PaidStatus
            return View(incomeReport);
        }


        public ActionResult PayoutDetail()
        {
            //Reports payoutDetail = new Reports();
            //List<Reports> lst1 = new List<Reports>();

            //DataSet ds11 = payoutDetail.GetPayoutReport();

            //if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds11.Tables[0].Rows)
            //    {
            //        Reports Obj = new Reports();
            //        Obj.LoginId = r["LoginId"].ToString();
            //        Obj.DisplayName = r["FirstName"].ToString();
            //        Obj.PayoutNo = r["PayoutNo"].ToString();
            //        Obj.ClosingDate = r["ClosingDate"].ToString();
            //        Obj.BinaryIncome = r["BinaryIncome"].ToString();
            //        Obj.LeadershipBonus=   r["DirectLeaderShipBonus"].ToString();
            //        Obj.GrossAmount = r["GrossAmount"].ToString();
            //        Obj.TDSAmount = r["TDSAmount"].ToString();
            //        Obj.ProcessingFee = r["ProcessingFee"].ToString();
            //        Obj.NetAmount = r["NetAmount"].ToString();
            //        Obj.DirectIncome= r["DirectIncome"].ToString();
            //        lst1.Add(Obj);
            //    }
            //    payoutDetail.lsttopupreport = lst1;
            //}
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            return View();
        }
        [HttpPost]
        [ActionName("PayoutDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailBy(Reports payoutDetail)
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            List<Reports> lst1 = new List<Reports>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            DataSet ds11 = payoutDetail.GetPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.EncryptName = Crypto.Encrypt(r["LoginId"].ToString());
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
                payoutDetail.lsttopupreport = lst1;
            }
            return View(payoutDetail);
        }
        public ActionResult PrintReciept()
        {
            return View();
        }
        public ActionResult PrintReciept1(string id, string PrintingDate)
        {
            Session["Date"] = null;
            Session["Name"] = null;
            Session["Amount"] = null;
            Session["AmountInWords"] = null;
            Session["Address"] = null;
            Session["Title"] = null;
            Session["Relation"] = null;
            Session["GaurdianName"] = null;
            Session["Against"] = null;
            Session["LoginId"] = null;
            DataSet ds = new DataSet();
            Reports obj = new Reports();
            obj.LoginId = id;
            obj.PrintingDate = Common.ConvertToSystemDate(PrintingDate, "dd/MM/yyyy");
            ds = obj.GetPrintData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["Date"] = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                Session["Amount"] = ds.Tables[0].Rows[0]["PinAmount"].ToString();
                Session["AmountInWords"] = ds.Tables[0].Rows[0]["AmountInWords"].ToString();
                Session["Address"] = ds.Tables[0].Rows[0]["Address"].ToString();
                Session["Title"] = ds.Tables[0].Rows[0]["Title"].ToString();
                Session["Relation"] = ds.Tables[0].Rows[0]["Relation"].ToString();
                Session["GaurdianName"] = ds.Tables[0].Rows[0]["GaurdianName"].ToString();
                Session["Against"] = ds.Tables[0].Rows[0]["Against"].ToString();
                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
            }
            return Json("1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Direct()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("Direct")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DirectList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");

            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Leg = r["Leg"].ToString();
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }
        public ActionResult DownLine()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("DownLine")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DownLineList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }

        #region TransferPinReport
        public ActionResult TransferPinReport()
        {
            return View();
        }
        [HttpPost]
        [ActionName("TransferPinReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetTransferPinReport(Reports newdata)
        {
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = newdata.GetTransferReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.FromLoginId = r["OwnerId"].ToString();
                    Obj.FromName = r["OwnerName"].ToString();
                    Obj.EPinNo = r["EPinNo"].ToString();
                    Obj.ToLoginID = r["TransferToId"].ToString();
                    Obj.ToName = r["TransferToName"].ToString();
                    Obj.TransferDate = r["TransferDate"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            return View(newdata);
        }
        #endregion

        #region TDSReport

        public ActionResult TDSReport()
        {
            return View();
        }
        [HttpPost]
        [ActionName("TDSReport")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetTDSReport(Reports payoutDetail)
        {

            List<Reports> lst1 = new List<Reports>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            DataSet ds11 = payoutDetail.GetTDSReport();
            ViewBag.Total = "0";
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.TDSAmount = r["TDS"].ToString();
                    Obj.PAN = r["PanNumber"].ToString();
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["TDS"].ToString());

                    lst1.Add(Obj);
                }
                payoutDetail.lsttopupreport = lst1;
            }
            return View(payoutDetail);
        }

        public ActionResult TDSReportByLoginID()
        {
            return View();
        }
        [HttpPost]
        [ActionName("TDSReportByLoginID")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetTDSReportByLoginID(Reports payoutDetail)
        {

            List<Reports> lst1 = new List<Reports>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            DataSet ds11 = payoutDetail.GetTDSReportByLoginID();
            ViewBag.Total = "0";
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.TDSAmount = r["TDS"].ToString();
                    Obj.PAN = r["PanNumber"].ToString();
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["TDS"].ToString());

                    lst1.Add(Obj);
                }
                payoutDetail.lsttopupreport = lst1;
            }
            return View(payoutDetail);
        }
        #endregion

        #region IncomeStatement
        public ActionResult GenerateDailyIncome()
        {
            Reports model = new Reports();
            DataSet ds = model.FetchNextDate();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.ClosingDate = ds.Tables[0].Rows[0]["NextDate"].ToString();
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("GenerateDailyIncome")]
        [OnAction(ButtonName = "GenerateIncome")]
        public ActionResult GenerateDailyIncome(Reports model)
        {
            try
            {
                model.ClosingDate = Common.ConvertToSystemDate(model.ClosingDate, "dd/MM/yyyy");
                DataSet ds = model.GenerateDailyIncomeNew();

                TempData["GenerateIncomeNew"] = "Income Generated Successfully";

            }
            catch (Exception ex)
            {
                TempData["GenerateIncomeNew"] = ex.Message;
            }
            return RedirectToAction("GenerateDailyIncome");
        }




        public ActionResult DailyIncomeReport(Reports model)
        {
            //model.ClosingDate = string.IsNullOrEmpty(model.ClosingDate)?
            try
            {
                DataSet ds = model.GetDailyIncomeNewReport();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.ClosingDate = ds.Tables[0].Rows[0]["CurrentDate"].ToString();
                    List<Reports> lstDailyIncomeReport = new List<Reports>();
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Reports obj = new Reports();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.ClosingDate = r["CurrentDate"].ToString();
                        obj.DirectIncome = r["DirectIncome"].ToString();
                        obj.BinaryIncome = r["BinaryIncome"].ToString();
                        obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                        lstDailyIncomeReport.Add(obj);
                    }
                    model.lsttopupreport = lstDailyIncomeReport;
                }
            }
            catch (Exception ex)
            {
                TempData["IncomeReport"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DailyIncomeReport")]
        [OnAction(ButtonName = "btnPublish")]
        public ActionResult PublishIncome(Reports model)
        {
            try
            {
                model.ClosingDate = Common.ConvertToSystemDate(model.ClosingDate, "dd/MM/yyyy");
                model.Fk_UserId = Session["Pk_AdminId"].ToString();

                DataSet ds = model.PublishDailyIncome();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Publish"] = "Daily Income was published successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Publish"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Publish"] = ex.Message;
            }
            return RedirectToAction("DailyIncomeReport");
        }




        public ActionResult GetDailyIncome()
        {
            Reports model = new Reports();
            model.FromDate = DateTime.Now.ToString("dd/MM/yyyy");
            model.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            return View(model);
        }
        [HttpPost]
        [ActionName("GetDailyIncome")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetDailyIncomeReport(Reports payoutDetail)
        {
            List<Reports> lst1 = new List<Reports>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            DataSet ds11 = payoutDetail.GetDailyIncomeReport();
            ViewBag.Direct = ViewBag.Harmoney = ViewBag.Total = "0";

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.ClosingDate = r["IncomeDate"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.DirectIncome = r["Direct"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.Amount = r["TotalIncome"].ToString();
                    //Obj.PreviousCFRight = r["PreviousCFRight"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    ViewBag.Direct = Convert.ToDecimal(ViewBag.Direct) + Convert.ToDecimal(r["Direct"].ToString());
                    ViewBag.Harmoney = Convert.ToDecimal(ViewBag.Harmoney) + Convert.ToDecimal(r["BinaryIncome"].ToString());
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(r["TotalIncome"].ToString());
                    //ViewBag.PreviousCFRight = Convert.ToDecimal(ViewBag.PreviousCFRight) + Convert.ToDecimal(r["PreviousCFRight"].ToString());

                    lst1.Add(Obj);
                }
                payoutDetail.lsttopupreport = lst1;
            }
            return View(payoutDetail);
        }
        #endregion

        public ActionResult PayoutReceipt(string lid, string pno)
        {
            Reports model = new Reports();
            try
            {
                model.LoginId = Crypto.Decrypt(lid);
                model.PayoutNo = Crypto.Decrypt(pno);

                ViewBag.CompanyName = SoftwareDetails.CompanyName;
                ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                ViewBag.Phone = SoftwareDetails.ContactNo;
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

                    //ViewBag.Income = Convert.ToDecimal(model.BinaryIncome) + Convert.ToDecimal(model.DirectIncome) + Convert.ToDecimal(model.LeadershipBonus);
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

        #region ProductTopuplist
        public ActionResult ProductTopupReport()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = newdata.GetTopupreportProduct();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.ToLoginID = r["Pk_ProductInvestmentId"].ToString();
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
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[2].Rows)
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
        [ActionName("ProductTopupReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductTopupReportBy(Reports newdata)
        {

            List<Reports> lst1 = new List<Reports>();
            newdata.Package = newdata.Package == "0" ? null : newdata.Package;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            DataSet ds11 = newdata.GetTopupreportProduct();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.ToLoginID = r["Pk_ProductInvestmentId"].ToString();
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
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[2].Rows)
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
        #endregion ProductTopuplist

        #region ProductPayoutDetails
        public ActionResult ProductPayoutDetails()
        {

            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;

            return View();
        }
        [HttpPost]
        [ActionName("ProductPayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductPayoutDetailsBy(Reports payoutDetail)
        {
            #region ddlLeg
            List<SelectListItem> ddlLeg = Common.Leg();
            ViewBag.ddlLeg = ddlLeg;
            #endregion ddlLeg
            List<Reports> lst1 = new List<Reports>();
            payoutDetail.FromDate = string.IsNullOrEmpty(payoutDetail.FromDate) ? null : Common.ConvertToSystemDate(payoutDetail.FromDate, "dd/MM/yyyy");
            payoutDetail.ToDate = string.IsNullOrEmpty(payoutDetail.ToDate) ? null : Common.ConvertToSystemDate(payoutDetail.ToDate, "dd/MM/yyyy");
            DataSet ds11 = payoutDetail.GetProductPayoutReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.EncryptName = Crypto.Encrypt(r["LoginId"].ToString());
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
                payoutDetail.lsttopupreport = lst1;
            }
            return View(payoutDetail);
        }
        #endregion  ProductPayoutDetails

        public ActionResult ProductAssociateIncomeReport()
        {
            Reports incomeReport = new Reports();
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = incomeReport.GetProductIncomeReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.TransactionDate = r["DATE"].ToString();
                    Obj.FromName = r["FromName"].ToString();
                    Obj.FromLoginId = r["FromLoginID"].ToString();
                    Obj.ToName = r["ToName"].ToString();
                    Obj.ToLoginID = r["ToLoginID"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.IncomeType = r["IncomeType"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lst1.Add(Obj);
                }
                incomeReport.lsttopupreport = lst1;
            }


            #region PaidStatus
            List<SelectListItem> PaidStatus = Common.PaidStatus();
            ViewBag.PaidStatus = PaidStatus;
            #endregion PaidStatus

            return View(incomeReport);
        }

        [HttpPost]
        [ActionName("ProductAssociateIncomeReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductAssociateIncomeReportBy(Reports incomeReport)
        {

            List<Reports> lst1 = new List<Reports>();
            incomeReport.FromDate = string.IsNullOrEmpty(incomeReport.FromDate) ? null : Common.ConvertToSystemDate(incomeReport.FromDate, "dd/MM/yyyy");
            incomeReport.ToDate = string.IsNullOrEmpty(incomeReport.ToDate) ? null : Common.ConvertToSystemDate(incomeReport.ToDate, "dd/MM/yyyy");
            if (incomeReport.Status == "null")
            {
                incomeReport.Status = null;
            }
            incomeReport.ToLoginID = string.IsNullOrEmpty(incomeReport.ToLoginID) ? null : incomeReport.ToLoginID.Trim();
            DataSet ds11 = incomeReport.GetProductIncomeReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.TransactionDate = r["DATE"].ToString();
                    Obj.FromName = r["FromName"].ToString();
                    Obj.FromLoginId = r["FromLoginID"].ToString();
                    Obj.ToName = r["ToName"].ToString();
                    Obj.ToLoginID = r["ToLoginID"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.IncomeType = r["IncomeType"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lst1.Add(Obj);
                }
                incomeReport.lsttopupreport = lst1;
            }
            #region PaidStatus
            List<SelectListItem> PaidStatus = Common.PaidStatus();
            ViewBag.PaidStatus = PaidStatus;
            #endregion PaidStatus
            return View(incomeReport);
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

        #region ProductBusinessReport

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
        public ActionResult ProductBusinessReportBy(Reports model)
        {

            #region ddlleg
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.Leg = Leg;
            #endregion ddlleg

            List<Reports> lst1 = new List<Reports>();
            model.Leg = string.IsNullOrEmpty(model.Leg) ? null : model.Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");


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

        #region RewardAchiever

        public ActionResult RewardAchiever(Reports model)
        {

            #region ddlReward
            int count1 = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            Reports model1 = new Reports();
            DataSet ds1P = model1.ProductRewardList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Select Reward", Value = "0" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["PK_ProdRewardItemId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlReward = ddlReward;
            #endregion



            return View(model);
        }

        [HttpPost]
        [ActionName("RewardAchiever")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRewards(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.RewardID = model.RewardID == "0" ? null : model.RewardID;
            DataSet ds = model.RewardListForAchiever();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.PK_RewardItemId = r["PK_UserProdRewardID"].ToString();
                    obj.RewardID = r["FK_RewardId"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Name = r["FirstName"].ToString();

                    lst1.Add(obj);
                }
                model.lstassociate = lst1;


            }

            #region ddlReward
            int count1 = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            Reports model1 = new Reports();
            DataSet ds1P = model1.ProductRewardList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Select Reward", Value = "0" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["PK_ProdRewardItemId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlReward = ddlReward;
            #endregion


            return View(model);
        }
        public ActionResult ApprovePayment(string PK_RewardItemId, string Description, string ApprovedDate, string PaidDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Reports model = new Reports();
                model.PK_RewardItemId = PK_RewardItemId;
                model.TransactionNo = Description;
                model.TransactionDate = ApprovedDate;
                model.PaymentDate = PaidDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                DataSet ds = model.ApprovePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ApprovePayment"] = "Paid successfully !";
                    }
                    else
                    {
                        TempData["ApprovePayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["ApprovePayment"] = ex.Message;
            }
            FormName = "RewardAchiever";
            Controller = "AdminReports";

            return RedirectToAction(FormName, Controller);

        }
        #endregion


        public ActionResult BookingRequestReport(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.GetBookingRequestList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.Name = r["Name"].ToString();
                    Obj.BookingRequestId = r["BookingRequestId"].ToString();
                    Obj.FatherName = r["FathersName"].ToString();
                    Obj.DOB = r["DOB"].ToString();
                    Obj.Nominee = r["Nominee"].ToString();
                    Obj.Nationality = r["Nationality"].ToString();
                    Obj.MobileNo = r["MobileNo"].ToString();
                    Obj.Address = r["Address"].ToString();
                    Obj.Category = r["Category"].ToString();
                    Obj.Religion = r["Religion"].ToString();
                    Obj.PlotNo = r["PlotNo"].ToString();
                    Obj.Cast = r["Cast"].ToString();
                    Obj.BookingAmount = r["BookingAmount"].ToString();
                    Obj.CustomerId = r["CustomerId"].ToString();
                    Obj.Gender = r["Gender"].ToString();
                    Obj.Relationship = r["Relation"].ToString();
                    Obj.PinCode = r["PinCode"].ToString();
                    Obj.AdharCardPhoto = r["AdharCardPhoto"].ToString();
                    Obj.AdharCardNo = r["AdharNo"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.BankName = r["BankName"].ToString();
                    Obj.DDChequeNo = r["DDChequeNo"].ToString();
                    Obj.DDChequeDate = r["DDChequeDate"].ToString();
                    Obj.BankBranch = r["BankBranch"].ToString();
                    Obj.Pancard = r["PanCard"].ToString();
                    Obj.PanCardPhoto = r["PanCardPhoto"].ToString();
                    Obj.BankPhoto = r["BankPhoto"].ToString();
                    Obj.Area = r["Area"].ToString();
                    Obj.AdharBackSide = r["AdharBackSide"].ToString();
                    lst1.Add(Obj);
                }
                model.lstRequestlist = lst1;
            }
            return View(model);
        }

        public ActionResult DirectLevelTwo()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("DirectLevelTwo")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DirectLevelTwoList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");

            DataSet ds = model.GetDirectListL2();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Leg = r["Leg"].ToString();
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }

		public ActionResult DCMIReport(Reports model)
		{
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.GetDCMIReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Pk_DCMIId = r["Pk_DCMIId"].ToString();
                    Obj.Month = r["Month"].ToString();
                    Obj.TotalMatching = r["TotalMatching"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.DCMIIncome = r["DCMIIncome"].ToString();
                    Obj.TotalBV = r["TotalBV"].ToString();
                    Obj.Name = r["Name"].ToString();
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

            model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
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

        public ActionResult KYCDetails(KYCDocuments obj)
        {
            return View(obj);
        }

        [HttpPost]
        [OnAction(ButtonName = "Search")]
        [ActionName("KYCDetails")]
        public ActionResult GetKYCDetails(KYCDocuments objKYC)
        {

            List<KYCDocuments> list = new List<KYCDocuments>();
            objKYC.LoginId = objKYC.LoginId;
            DataSet ds = objKYC.GetKYCDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                KYCDocuments obj = new KYCDocuments();
                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.MemberAccNo = ds.Tables[1].Rows[0]["MemberAccNo"].ToString();
                obj.MemberBankName = ds.Tables[1].Rows[0]["MemberBankName"].ToString();
                obj.IFSCCode = ds.Tables[1].Rows[0]["IFSCCode"].ToString();
                obj.MemberBranch = ds.Tables[1].Rows[0]["MemberBranch"].ToString();
                list.Add(obj);
            }
            objKYC.KycDetailList = list;

            return View(objKYC);
        }

    }
}
