using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using System.IO;
using BusinessLayer;

namespace TrustRadhe.Controllers
{
    public class FranchiseController : FranchiseBaseController
    {
        DataTable dt = new DataTable();
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetailsForFranchiseSale();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    obj.Result = "Yes";
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else { obj.Result = "Invalid Associate Id"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplierDetails(string SupplierID)
        {
            PurchaseOrder obj = new PurchaseOrder();
            try
            {
                obj.SupplierID = SupplierID;

                DataSet ds = obj.SupplierList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    obj.TIN = ds.Tables[0].Rows[0]["TINNo"].ToString();
                    obj.Response = "1";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);


        }

        public ActionResult RequestStatusReport(Franchise model)
        {

            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.RequestStatusReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestID = r["PK_RequestID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.RequestFrom = r["RequestFrom"].ToString();
                        obj.ProductID = r["PK_ProductID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Quantity = r["RequestQty"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.Status = r["Status"].ToString();
                        obj.ApprovedQuantity = r["ApproveQty"].ToString();

                        lstRequest.Add(obj);
                    }
                    model.lstRequest = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult GetKitAmount(string kitid)
        {
            Franchise obj = new Franchise();
            obj.KitID = kitid;
            obj.PK_FranchiseID = Session["FranchiseID"].ToString();
            DataSet ds = obj.KitListForFranchise();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.TotalAmount = ds.Tables[0].Rows[0]["ManagedAmount"].ToString();
                obj.Result = "1";
            }
            else { obj.Result = "0"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region StockList
        public ActionResult StockList()
        {
            #region ddlProduct
            int count1 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            Franchise model1 = new Franchise();
            DataSet ds1P = model1.ProductList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductId"].ToString() });
                    count1 = count1 + 1;
                }

            }

            ViewBag.ddlProduct = ddlProduct;
            #endregion
            Franchise model = new Franchise();
            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.StockList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstStock = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.PK_FranchiseID= r["FK_FranchiseID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        lstStock.Add(obj);
                    }
                    model.lstStock = lstStock;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("StockList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult StockListBy(Franchise model)
        {
            #region ddlProduct
            int count1 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            Franchise model1 = new Franchise();
            DataSet ds1P = model1.ProductList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductId"].ToString() });
                    count1 = count1 + 1;
                }

            }

            ViewBag.ddlProduct = ddlProduct;
            #endregion
           
            try
            {
                model.ProductID = model.ProductID == "0" ? null : model.ProductID;
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.StockList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstStock = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.PK_FranchiseID = r["FK_FranchiseID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        lstStock.Add(obj);
                    }
                    model.lstStock = lstStock;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }


        public ActionResult StockReportProductWise(string ProductID, string FranchiseID)
        {
            List<Franchise> list = new List<Franchise>();
            Franchise model = new Franchise();
            if (ProductID != null && FranchiseID != null)
            {

                model.ProductID = ProductID;
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                try
                {
                    DataSet ds = model.StockReportProductWise();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Franchise obj = new Franchise();


                            obj.Credit = r["Credit"].ToString();
                            obj.Debit = r["Debit"].ToString();
                            obj.Narration = r["Narration"].ToString();
                            obj.TransactionDate = r["TransactionDate"].ToString();
                            obj.Balance = r["Balance"].ToString();


                            list.Add(obj);

                        }
                        model.lstFranchise = list;
                    }

                }
                catch (Exception ex)
                {
                }

            }
            return View(model);
        }
        #endregion

        #region ProductRequest
        public ActionResult GetProductdetails(string ProductID, string batchno)
        {
            PurchaseOrder obj = new PurchaseOrder();
            try
            {
                obj.ProductID = ProductID;
                obj.BatchNo = batchno;
                obj.AddedBy = Session["FranchiseID"].ToString();
                DataSet ds = obj.ProductList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.ProductID = ds.Tables[0].Rows[0]["PK_ProductId"].ToString();
                    obj.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                    obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                    obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                    obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                    obj.TaxableAmount = ds.Tables[0].Rows[0]["TaxableAmount"].ToString();
                    obj.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();
                    obj.Stock = ds.Tables[0].Rows[0]["Stock"].ToString();
                    obj.TradeIn = ds.Tables[0].Rows[0]["TradeIn"].ToString();
                    obj.DP = ds.Tables[0].Rows[0]["DP"].ToString();
                    obj.Response = "1";
                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult BindPurchaseItemList(string ProductID, string Product, string MRP, string IGST, string CGST, string SGST, string Quantity, string TaxableAmount, string FinalAmount)
        {
            PurchaseOrder model = new PurchaseOrder();
            try
            {
                if (Session["tmpData"] != null)
                {
                    string mrp = MRP;
                    string amount = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = ((decimal.Parse(amount)) - ((decimal.Parse(amount) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");
                    string finalamt = amount;
                    dt = (DataTable)Session["tmpData"];
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();

                        dr["Fk_ProductId"] = ProductID;
                        dr["Product"] = Product;
                        dr["MRP"] = mrp;
                        dr["Amount"] = amount;
                        dr["IGST"] = IGST;
                        dr["CGST"] = CGST;
                        dr["SGST"] = SGST;
                        dr["PurchaseQty"] = Quantity;
                        dr["TaxableAmount"] = Taxamt;
                        dr["FinalAmount"] = finalamt;

                        dt.Rows.Add(dr);
                        Session["tmpData"] = dt;
                    }
                }
                else
                {
                    string mrp = MRP;
                    string amount = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = ((decimal.Parse(amount)) - ((decimal.Parse(amount) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");
                    string finalamt = amount;

                    dt.Columns.Add("Fk_ProductId", typeof(string));
                    dt.Columns.Add("Product", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    dt.Columns.Add("Amount", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("PurchaseQty", typeof(string));
                    dt.Columns.Add("TaxableAmount", typeof(string));
                    dt.Columns.Add("FinalAmount", typeof(string));

                    DataRow dr = dt.NewRow();

                    dr["Fk_ProductId"] = ProductID;
                    dr["Product"] = Product;
                    dr["MRP"] = mrp;
                    dr["Amount"] = amount;
                    dr["IGST"] = IGST;
                    dr["CGST"] = CGST;
                    dr["SGST"] = SGST;
                    dr["PurchaseQty"] = Quantity;
                    dr["TaxableAmount"] = Taxamt;
                    dr["FinalAmount"] = finalamt;


                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;

                }

                dt = (DataTable)Session["tmpData"];
                List<PurchaseOrder> lstTmpData = new List<PurchaseOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //ViewBag.CGST = "0";
                    foreach (DataRow r in dt.Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["PurchaseQty"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();


                        lstTmpData.Add(obj);
                    }

                    model.lstFranchise = lstTmpData;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductRequest()
        {
            Session["tmpData"] = null;
            Franchise model = new Franchise();
            #region ddlProduct
            int count1 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1P = model.ProductList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductId"].ToString() });
                    count1 = count1 + 1;
                }


            }

            ViewBag.ddlProduct = ddlProduct;
            #endregion
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            #region upperLevelFranchise
            int count = 0;
            List<SelectListItem> ddlUpperFranchise = new List<SelectListItem>();
            model.LoginID = Session["LoginID"].ToString();
            DataSet dsFranchiseList = model.GetUpperLevelFranchise();
            if (dsFranchiseList != null && dsFranchiseList.Tables.Count > 0 && dsFranchiseList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsFranchiseList.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUpperFranchise.Add(new SelectListItem { Text = "Select Franchise", Value = "0" });
                    }
                    ddlUpperFranchise.Add(new SelectListItem { Text = r["FranchiseName"].ToString(), Value = r["PK_FranchsieID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUpperFranchise = ddlUpperFranchise;
            #endregion
            model.Quantity = "1";
            return View(model);
        }
        [HttpPost]
        [ActionName("ProductRequest")]
        [OnAction(ButtonName = "btnRequest")]
        public ActionResult SaveProductRequest(HttpPostedFileBase fileProfilePicture,Franchise model)
        {
            try
            {
                if (fileProfilePicture != null)
                {
                    model.RequestImage = "/RequestImage/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(model.RequestImage)));
                }
                string noofrows = Request["hdRows"].ToString();

                string Productid = "";
                string mrp = "";
                string igst = "";
                string cgst = "";
                string sgct = "";
                string purchaseqty = "";
                string taxamt = "";
                string finalamt = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("Fk_ProductId", typeof(string));
                dtst.Columns.Add("PurchaseQty", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("TaxableAmount", typeof(string));
                dtst.Columns.Add("FinalAmount", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    Productid = Request["txtproductID_ " + i].ToString();
                    purchaseqty = Request["txtPurchaseQty_ " + i].ToString();
                    mrp = "0";
                    igst = "0";
                    cgst = "0";
                    sgct = "0";
                    taxamt = "0";
                    finalamt = "0";

                    dtst.Rows.Add(Productid, purchaseqty, mrp, igst, cgst, sgct, taxamt, finalamt);
                }
                model.AddedBy = Session["FranchiseID"].ToString();
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                model.dtRequest = dtst;

                DataSet ds = model.SaveFranchiseProductRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Request"] = "Request saved successfully. Request Code is : " + ds.Tables[0].Rows[0]["RequestCode"].ToString();
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Request"] = "ERROR : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Request"] = ex.Message;
            }
            return Redirect("ProductRequest");

        }
        #endregion

        #region ProductRequestList
        public ActionResult RequestList()
        {
            Franchise model = new Franchise();
            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.FranchiseProductRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestID = r["PK_RequestID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.RequestFrom = r["RequestFrom"].ToString();
                        obj.ProductID = r["PK_ProductID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Quantity = r["RequestQty"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();

                        lstRequest.Add(obj);
                    }
                    model.lstRequest = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult ApproveRequest(string requestID, string approveQty)
        {
            Franchise model = new Franchise();
            try
            {
                model.RequestID = requestID;
                model.Quantity = approveQty;
                model.AddedBy = Session["FranchiseID"].ToString();

                DataSet ds = model.ApproveRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["RequestApproved"] = "Request approved.";
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["RequestApproved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        model.Result = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["RequestApproved"] = ex.Message;
                model.Result = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Sale
        public ActionResult DeleteSaleRow(string rowid)
        {
            SaleOrder model = new SaleOrder();
            try
            {
                DataTable dt = Session["tmpData"] as DataTable;
                dt.Rows.RemoveAt(int.Parse(rowid) - 1);
                dt.AcceptChanges();
                Session["tmpData"] = dt;

                dt = (DataTable)Session["tmpData"];
                List<SaleOrder> lstTmpData = new List<SaleOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //ViewBag.CGST = "0";
                    foreach (DataRow r in dt.Rows)
                    {
                        SaleOrder obj = new SaleOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["Amount"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.TradeQty = r["TradeInQty"].ToString();
                        obj.TotalQty = r["TotalQty"].ToString();

                        lstTmpData.Add(obj);
                    }

                    model.lstFranchise = lstTmpData;
                }
            }
            catch (Exception ex)
            {

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sale(SaleOrder model)
        {
            Session["tmpData"] = null;

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            #region ddlProduct
            int count1 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1P = model.ProductList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductId"].ToString() });
                    count1 = count1 + 1;
                }


            }

            ViewBag.ddlProduct = ddlProduct;
            #endregion

            #region ddlBatchNo
            int ctrBatch = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            DataSet dsBatchNo = model.BatchNoList();
            if (dsBatchNo != null && dsBatchNo.Tables.Count > 0 && dsBatchNo.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBatchNo.Tables[0].Rows)
                {
                    if (ctrBatch == 0)
                    {
                        ddlBatch.Add(new SelectListItem { Text = "Select Batch No", Value = "0" });
                    }
                    ddlBatch.Add(new SelectListItem { Text = r["BatchNo"].ToString(), Value = r["BatchNo"].ToString() });
                    ctrBatch = ctrBatch + 1;
                }
            }
            ViewBag.ddlBatch = ddlBatch;
            #endregion
            return View(model);
        }

        public ActionResult BindSaleItemList(string ProductID, string Product, string MRP, string IGST, string CGST, string SGST,
                                                string Quantity, string TaxableAmount, string FinalAmount, string batchNo, string TradeIn)
        {
            int tradeQty = 0;
            DataTable dtResult = new DataTable();
            SaleOrder model = new SaleOrder();
            try
            {
                if (Session["tmpData"] != null)
                {
                    dt = (DataTable)Session["tmpData"];

                    try
                    {
                        dtResult = dt.Select("Fk_ProductId=" + ProductID).CopyToDataTable();
                        if (dtResult.Rows.Count > 0)
                        {
                            model.Result = "1";
                            return Json(model, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch { }
                    string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");

                    dt = (DataTable)Session["tmpData"];

                    TradeIn = string.IsNullOrEmpty(TradeIn) ? "0" : TradeIn;
                    if (TradeIn != "0")
                    {
                        if (TradeIn == "100.00")
                        {
                            tradeQty = (int.Parse(Quantity) * 2) - int.Parse(Quantity);
                        }
                        else if (TradeIn == "50.00")
                        {
                            if ((int.Parse(Quantity) % 2) == 0)
                            {
                                tradeQty = int.Parse(Quantity) / 2;
                            }
                            else
                            {
                                tradeQty = (int.Parse(Quantity) - 1) / 2;
                            }
                        }
                        else if (TradeIn == "25.00")
                        {
                            if ((int.Parse(Quantity) % 4) == 0)
                            {
                                tradeQty = int.Parse(Quantity) / 4;
                            }
                            else
                            {
                                int remainder = int.Parse(Quantity) % 4;
                                tradeQty = (int.Parse(Quantity) - remainder) / 4;
                            }
                        }
                    }

                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();

                        dr["Fk_ProductId"] = ProductID;
                        dr["Product"] = Product;
                        dr["Quantity"] = Quantity;
                        dr["Amount"] = MRP;
                        dr["IGST"] = IGST;
                        dr["CGST"] = CGST;
                        dr["SGST"] = SGST;
                        dr["TaxableAmount"] = Taxamt;
                        dr["FinalAmount"] = mrp;
                        dr["BatchNo"] = batchNo;
                        dr["TradeInQty"] = tradeQty;
                        dr["TotalQty"] = tradeQty + int.Parse(Quantity);

                        dt.Rows.Add(dr);
                        Session["tmpData"] = dt;
                    }
                }
                else
                {

                    string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");
                    dt.Columns.Add("Fk_ProductId", typeof(string));
                    dt.Columns.Add("Product", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("Amount", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("BatchNo", typeof(string));
                    dt.Columns.Add("TaxableAmount", typeof(string));
                    dt.Columns.Add("FinalAmount", typeof(string));
                    dt.Columns.Add("TradeInQty", typeof(string));
                    dt.Columns.Add("TotalQty", typeof(string));

                    TradeIn = string.IsNullOrEmpty(TradeIn) ? "0" : TradeIn;
                    if (TradeIn != "0")
                    {
                        if (TradeIn == "100.00")
                        {
                            tradeQty = (int.Parse(Quantity) * 2) - int.Parse(Quantity);
                        }
                        else if (TradeIn == "50.00")
                        {
                            if ((int.Parse(Quantity) % 2) == 0)
                            {
                                tradeQty = int.Parse(Quantity) / 2;
                            }
                            else
                            {
                                tradeQty = (int.Parse(Quantity) - 1) / 2;
                            }
                        }
                    }

                    DataRow dr = dt.NewRow();

                    dr["Fk_ProductId"] = ProductID;
                    dr["Product"] = Product;
                    dr["Quantity"] = Quantity;
                    dr["Amount"] = MRP;
                    dr["IGST"] = IGST;
                    dr["CGST"] = CGST;
                    dr["SGST"] = SGST;
                    dr["TaxableAmount"] = Taxamt;
                    dr["FinalAmount"] = mrp;
                    dr["BatchNo"] = batchNo;
                    dr["TradeInQty"] = tradeQty;
                    dr["TotalQty"] = tradeQty + int.Parse(Quantity);

                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;

                }

                dt = (DataTable)Session["tmpData"];
                List<SaleOrder> lstTmpData = new List<SaleOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        SaleOrder obj = new SaleOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["Amount"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.TradeQty = r["TradeInQty"].ToString();
                        obj.TotalQty = r["TotalQty"].ToString();

                        lstTmpData.Add(obj);
                    }

                    model.lstFranchise = lstTmpData;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("Sale")]
        [OnAction(ButtonName = "btnSale")]
        public ActionResult SaveSaleOrder(SaleOrder model)
        {
            try
            {
                if (Request["hdRows"] == null)
                {
                    TempData["purchaseerrro"] = "Please add at least one Product for Sale";
                    return Redirect("Sale");
                }
                else
                {
                    string noofrows = Request["hdRows"].ToString();
                    string Productid = "";
                    string mrp = "";
                    string igst = "";
                    string cgst = "";
                    string sgct = "";
                    string purchaseqty = "";
                    string taxamt = "";
                    string finalamt = "";
                    string batchno = "";
                    string tradeqty = "";

                    DataTable dtst = new DataTable();
                    dtst.Columns.Add("BatchNo", typeof(string));
                    dtst.Columns.Add("Fk_ProductId", typeof(string));
                    dtst.Columns.Add("Quantity", typeof(string));
                    dtst.Columns.Add("Amount", typeof(string));
                    dtst.Columns.Add("IGST", typeof(string));
                    dtst.Columns.Add("CGST", typeof(string));
                    dtst.Columns.Add("SGST", typeof(string));
                    dtst.Columns.Add("TaxableAmount", typeof(string));
                    dtst.Columns.Add("FinalAmount", typeof(string));
                    dtst.Columns.Add("TradeQty", typeof(string));

                    for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                    {
                        batchno = Request["txtBatchNo_ " + i].ToString();
                        Productid = Request["txtproductID_ " + i].ToString();
                        mrp = Request["txtMrp_ " + i].ToString();
                        igst = Request["txtIGST_ " + i].ToString();
                        cgst = Request["txtCGST_ " + i].ToString();
                        sgct = Request["txtSGST_ " + i].ToString();
                        purchaseqty = Request["txtPurchaseQty_ " + i].ToString();
                        taxamt = Request["txtTaxableAmount_ " + i].ToString();
                        finalamt = Request["txtFinalAmount_ " + i].ToString();
                        tradeqty = "0";

                        dtst.Rows.Add(batchno, Productid, purchaseqty, mrp, igst, cgst, sgct, taxamt, finalamt, tradeqty);
                    }
                    model.SaleDate = string.IsNullOrEmpty(model.SaleDate) ? null : Common.ConvertToSystemDate(model.SaleDate, "dd/MM/yyyy");
                    model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/MM/yyyy");
                    model.AddedBy = Session["FranchiseID"].ToString();
                    model.dtPurchaseOrder = dtst;
                    DataSet ds = model.SaveSaleOrderForFranchise();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Class"] = "alert alert-success";
                            TempData["Sale"] = "Sale Order generated successfully. Sale Order No. : " + ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        }
                        else
                        {
                            TempData["Class"] = "alert alert-danger";
                            TempData["Sale"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Sale"] = ex.Message;
            }
            return Redirect("Sale");
        }

        public ActionResult SaleOrderList(SaleOrder model)
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.SaleListForFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<SaleOrder> lstFranchise = new List<SaleOrder>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SaleOrder obj = new SaleOrder();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_SaleOrderId"].ToString());
                        obj.SaleID = r["Pk_SaleOrderId"].ToString();
                        obj.SaleDate = r["SaleDate"].ToString();
                        obj.ReferBy = r["LoginId"].ToString();
                        obj.DisplayName = r["FirstName"].ToString();
                        obj.SaleOrderNumber = r["SaleOrderNo"].ToString();
                        obj.PaidAmount = r["Paid"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.BankName = r["BankDetails"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SaleOrderList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SaleOrderListBy(SaleOrder model)
        {
            try
            {
                if (model.PaymentMode == "0")
                {
                    model.PaymentMode = null;
                }
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.SaleListForFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<SaleOrder> lstFranchise = new List<SaleOrder>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SaleOrder obj = new SaleOrder();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_SaleOrderId"].ToString());
                        obj.SaleID = r["Pk_SaleOrderId"].ToString();
                        obj.SaleDate = r["SaleDate"].ToString();
                        obj.ReferBy = r["LoginId"].ToString();
                        obj.DisplayName = r["FirstName"].ToString();
                        obj.SaleOrderNumber = r["SaleOrderNo"].ToString();
                        obj.PaidAmount = r["Paid"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.BankName = r["BankDetails"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";

            }
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View(model);
        }
        
        public ActionResult SaleOrderView(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {
                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {
                    DataSet ds = model.GetSaleOrderDetailsForFranchise();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            SaleOrder obj = new SaleOrder();
                            
                            obj.SaleID = r["Fk_SaleOrderId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.Quantity = r["SaleQty"].ToString();
                            obj.TradeQty = r["TradeQty"].ToString();
                            obj.TotalQty = r["TotalQty"].ToString();
                            obj.DP = r["Amount"].ToString();
                            obj.MRP= r["MRP"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.BatchNo = r["BatchNo"].ToString();

                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["SaleOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["SaleDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["FirstName"].ToString();

                            list.Add(obj);

                        }
                        model.lstFranchise = list;
                    }

                }
                catch (Exception ex)
                {
                }
                try
                {
                    DataSet ds = model.GetSaleDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        //ViewBag.PurchaseOrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        //ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["PurchaseDate"].ToString();

                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }

        public ActionResult SaleReceipt(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {

                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {
                    DataSet ds = model.GetSaleOrderDetailsForFranchise();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            SaleOrder obj = new SaleOrder();


                            obj.SaleID = r["Fk_SaleOrderId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            obj.ProductID = r["Fk_ProductId"].ToString();
                            //obj.ProductName = r["ProductName"].ToString();
                            obj.Quantity = r["Quantity"].ToString();
                            obj.DP = r["Amount"].ToString();
                            obj.MRP = r["MRP"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.HSNCode = r["HSNCode"].ToString();


                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["SaleOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["SaleDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();

                            ViewBag.CompanyName = SoftwareDetails.CompanyName;
                            ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                            ViewBag.Pin1 = SoftwareDetails.Pin1;
                            ViewBag.State1 = SoftwareDetails.State1;
                            ViewBag.City1 = SoftwareDetails.City1;
                            ViewBag.ContactNo = SoftwareDetails.ContactNo;
                            ViewBag.LandLine = SoftwareDetails.LandLine;
                            ViewBag.Website = SoftwareDetails.Website;
                            ViewBag.EmailID = SoftwareDetails.EmailID;
                            list.Add(obj);

                        }
                        model.lstFranchise = list;
                    }

                }
                catch (Exception ex)
                {
                }
                try
                {
                    DataSet ds = model.GetSaleDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //ViewBag.PurchaseOrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();                  
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }
        #endregion
        
        #region EditProfile

        public ActionResult EditProfile(Franchise model)
        {
            
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.FranchiseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                model.PK_FranchiseID = ds.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                model.ReferBy = ds.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                model.DisplayName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                model.FranchiseType = ds.Tables[0].Rows[0]["FK_FranchiseTypeID"].ToString();
                model.FranchiseTypeName= ds.Tables[0].Rows[0]["FranchiseType"].ToString();
                model.FirmName = ds.Tables[0].Rows[0]["FirmName"].ToString();
                model.FranchiseName = ds.Tables[0].Rows[0]["FranchiseName"].ToString();
                model.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                model.AdharNo = ds.Tables[0].Rows[0]["AdharNo"].ToString();
                model.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                model.GSTNo = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                model.PinCode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.State = ds.Tables[0].Rows[0]["State"].ToString();
                model.City = ds.Tables[0].Rows[0]["City"].ToString();

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("EditProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFranchise(Franchise model)
        {
            try
            {
                model.AddedBy = Session["FranchiseID"].ToString();
                DataSet ds = model.UpdateFranchise();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Franchise1"] = "Franchise details updated successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Franchise1"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Franchise"] = ex.Message;
            }
            return RedirectToAction("EditProfile");
        }

        #endregion

        #region ChangePassword
        public ActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePassword(Franchise obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["FranchiseID"].ToString();
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangefpPassword"] = "Password updated successfully..";
                        FormName = "FranchiseLogin";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["ChangefpPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "Franchise";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangefpPassword"] = ex.Message;
                FormName = "FranchiseLogin";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region KitList

        public ActionResult KitListForFranchise(Franchise model)
        {
            #region ddlKit

            List<SelectListItem> ddlKit = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.KitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlKit.Add(new SelectListItem { Text = "Select Kit", Value = "0" });
                    }
                    ddlKit.Add(new SelectListItem { Text = r["KitName"].ToString(), Value = r["PK_KitId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlKit = ddlKit;
            #endregion

            
            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.KitListForFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstStock = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                       
                        obj.KitName = r["KitName"].ToString();
                        obj.Debit = r["Debit"].ToString();     
                        obj.Credit = r["Credit"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        lstStock.Add(obj);
                    }
                    model.lstStock = lstStock;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("KitListForFranchise")]
        [OnAction(ButtonName = "Search")]
        public ActionResult KitListForFranchiseBy(Franchise model)
        {
            #region ddlKit

            List<SelectListItem> ddlKit = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.KitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlKit.Add(new SelectListItem { Text = "Select Kit", Value = "0" });
                    }
                    ddlKit.Add(new SelectListItem { Text = r["KitName"].ToString(), Value = r["PK_KitId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlKit = ddlKit;
            #endregion


            try
            {
                model.KitID = model.KitID == "0" ? null : model.KitID;
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.KitListForFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstStock = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.KitName = r["KitName"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        lstStock.Add(obj);
                    }
                    model.lstStock = lstStock;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        #endregion

        #region DirectKitTransfer

        public ActionResult DirectKitTransfer(Franchise model)
        {
            #region ddlKit

            List<SelectListItem> ddlKit = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.KitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlKit.Add(new SelectListItem { Text = "Select Kit", Value = "0" });
                    }
                    ddlKit.Add(new SelectListItem { Text = r["KitName"].ToString(), Value = r["PK_KitId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlKit = ddlKit;
            #endregion
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View(model);
        }

        public ActionResult GetFranchiseName(string LoginID)
        {
            try
            {
                Franchise model = new Franchise();
                model.LoginID = LoginID;

                #region GetSiteRate
                DataSet dsSiteRate = model.FranchiseList();
                if (dsSiteRate != null)
                {
                    model.FranchiseName = dsSiteRate.Tables[0].Rows[0]["FranchiseName"].ToString();
                    model.PK_FranchiseID = dsSiteRate.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                    model.Result = "yes";
                }
                else { model.Result = ""; }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetProductByKit(string PK_FranchiseID, string KitID)
        {
            try
            {
                Franchise model = new Franchise();
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                model.KitID = KitID;
                #region GetSiteRate
                DataSet dsSiteRate = model.GetProductByKit();
                Session["dsSiteRate"] = dsSiteRate.Tables[0];

                if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    model.Total = dsSiteRate.Tables[0].Rows[0]["ManagedAmount"].ToString();
                   
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult CheckStock(string Quantity)
        {
            Franchise model = new Franchise();
            model.Quantity = Quantity;
            DataTable ds = Session["dsSiteRate"] as DataTable;
            List<Franchise> lstFranchise = new List<Franchise>();
            foreach (DataRow r in ds.Rows)
            {
                Franchise obj = new Franchise();

                obj.ProductName = r["ProductName"].ToString();
                obj.StockID = r["Stock"].ToString();
                obj.Quantity = (Convert.ToDecimal(Quantity) * Convert.ToDecimal(r["Quantity"])).ToString();

                lstFranchise.Add(obj);
            }
            model.lstFranchise = lstFranchise;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("DirectKitTransfer")]
        [OnAction(ButtonName = "Transfer")]
        public ActionResult Transfer(Franchise obj)
        {
            try
            {
                
                obj.AddedBy = Session["FranchiseID"].ToString();
                DataSet ds = new DataSet();
                obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                ds = obj.TransferKitByFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["TransferKit"] = "Kit transferred successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["TransferKit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["TransferKit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["TransferKit"] = ex.Message;
            }
            return RedirectToAction("DirectKitTransfer", "Franchise");
        }

        #endregion

        #region TopUpByKit

        public ActionResult TopUpByKit(Franchise model)
        {
            #region ddlKit

            List<SelectListItem> ddlKit = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.KitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlKit.Add(new SelectListItem { Text = "Select Kit", Value = "0" });
                    }
                    ddlKit.Add(new SelectListItem { Text = r["KitName"].ToString(), Value = r["PK_KitId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlKit = ddlKit;
            #endregion

            return View(model);
        }

        public ActionResult GetAssociateName(string LoginID)
        {
            Franchise obj = new Franchise();
            obj.LoginID = LoginID;
            obj.Status = "T";
            DataSet ds = obj.GetAssociateList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid  ID"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKitDetails(string KitID)
        {
            Franchise obj = new Franchise();
            obj.KitID = KitID;
            obj.PK_FranchiseID = Session["FranchiseID"].ToString();
            DataSet ds = obj.KitListForFranchise();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.TotalAmount = ds.Tables[0].Rows[0]["ManagedAmount"].ToString();
                obj.StockID = ds.Tables[0].Rows[0]["Balance"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = ""; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("TopUpByKit")]
        [OnAction(ButtonName = "TopUp")]
        public ActionResult Topup(Franchise model)
        {
            try
            {
                model.AddedBy = Session["FranchiseID"].ToString();
                
                DataSet ds = model.TopUpByKit();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["TopUp"] = "Top up done successfully.  ";


                      
                        string kitname = ds.Tables[0].Rows[0]["KitName"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        try
                        {
                            string str = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Your login ID  " + ds.Tables[0].Rows[0]["LoginId"].ToString() + "   is top up by  " + ds.Tables[0].Rows[0]["KitName"].ToString() + ".";
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        
                        TempData["Request"] = "ERROR : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Request"] = ex.Message;
            }
            return Redirect("TopUpByKit");
       
             
             
        }
        #endregion
        
        #region KitRequest
       
        public ActionResult BindKitRequestList(string ProductID, string Product, string MRP, string IGST, string CGST, string SGST, string Quantity, string TaxableAmount, string FinalAmount)
        {
            PurchaseOrder model = new PurchaseOrder();
            try
            {
                if (Session["tmpDataKit"] != null)
                {
                    string mrp = MRP;
                    string amount = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = ((decimal.Parse(amount) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST) + decimal.Parse(IGST))).ToString("0.00");
                    string finalamt = (decimal.Parse(Taxamt) + decimal.Parse(amount)).ToString("0.00");
                    dt = (DataTable)Session["tmpDataKit"];
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();

                        dr["Fk_ProductId"] = ProductID;
                        dr["Product"] = Product;
                        dr["MRP"] = mrp;
                        dr["Amount"] = amount;
                        dr["IGST"] = IGST;
                        dr["CGST"] = CGST;
                        dr["SGST"] = SGST;
                        dr["PurchaseQty"] = Quantity;
                        dr["TaxableAmount"] = Taxamt;
                        dr["FinalAmount"] = finalamt;

                        dt.Rows.Add(dr);
                        Session["tmpDataKit"] = dt;
                    }
                }
                else
                {
                    string mrp = MRP;
                    string amount = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    string Taxamt = ((decimal.Parse(amount) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST) + decimal.Parse(IGST))).ToString("0.00");
                    string finalamt = (decimal.Parse(Taxamt) + decimal.Parse(amount)).ToString("0.00");

                    dt.Columns.Add("Fk_ProductId", typeof(string));
                    dt.Columns.Add("Product", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    dt.Columns.Add("Amount", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("PurchaseQty", typeof(string));
                    dt.Columns.Add("TaxableAmount", typeof(string));
                    dt.Columns.Add("FinalAmount", typeof(string));

                    DataRow dr = dt.NewRow();

                    dr["Fk_ProductId"] = ProductID;
                    dr["Product"] = Product;
                    dr["MRP"] = mrp;
                    dr["Amount"] = amount;
                    dr["IGST"] = IGST;
                    dr["CGST"] = CGST;
                    dr["SGST"] = SGST;
                    dr["PurchaseQty"] = Quantity;
                    dr["TaxableAmount"] = Taxamt;
                    dr["FinalAmount"] = finalamt;


                    dt.Rows.Add(dr);
                    Session["tmpDataKit"] = dt;

                }

                dt = (DataTable)Session["tmpDataKit"];
                List<PurchaseOrder> lstTmpData = new List<PurchaseOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //ViewBag.CGST = "0";
                    foreach (DataRow r in dt.Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["PurchaseQty"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();


                        lstTmpData.Add(obj);
                    }

                    model.lstFranchise = lstTmpData;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KitRequest()
        {
            Session["tmpDataKit"] = null;
            Franchise model = new Franchise();
            #region ddlKit

            List<SelectListItem> ddlKit = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.KitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlKit.Add(new SelectListItem { Text = "Select Kit", Value = "0" });
                    }
                    ddlKit.Add(new SelectListItem { Text = r["KitName"].ToString(), Value = r["PK_KitId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlKit = ddlKit;
            #endregion
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            #region upperLevelFranchise
            int count = 0;
            List<SelectListItem> ddlUpperFranchise = new List<SelectListItem>();
            model.LoginID = Session["LoginID"].ToString();
            DataSet dsFranchiseList = model.GetUpperLevelFranchise();
            if (dsFranchiseList != null && dsFranchiseList.Tables.Count > 0 && dsFranchiseList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsFranchiseList.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUpperFranchise.Add(new SelectListItem { Text = "Select Franchise", Value = "0" });
                    }
                    ddlUpperFranchise.Add(new SelectListItem { Text = r["FranchiseName"].ToString(), Value = r["PK_FranchsieID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUpperFranchise = ddlUpperFranchise;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("KitRequest")]
        [OnAction(ButtonName = "btnRequest")]
        public ActionResult SaveKitRequest(HttpPostedFileBase fileProfilePicture ,Franchise model)
        {
            try
            {
                if (fileProfilePicture != null)
                {
                    model.RequestImage = "/RequestImage/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(model.RequestImage)));
                }
                string noofrows = Request["hdRows"].ToString();

                string Productid = "";
                string mrp = "";
                string igst = "";
                string cgst = "";
                string sgct = "";
                string purchaseqty = "";
                string taxamt = "";
                string finalamt = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("Fk_ProductId", typeof(string));
                dtst.Columns.Add("PurchaseQty", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("TaxableAmount", typeof(string));
                dtst.Columns.Add("FinalAmount", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    Productid = Request["txtproductID_ " + i].ToString();
                    purchaseqty = Request["txtPurchaseQty_ " + i].ToString();
                    mrp = "0";
                    igst = "0";
                    cgst = "0";
                    sgct = "0";
                    taxamt = "0";
                    finalamt = "0";

                    dtst.Rows.Add(Productid, purchaseqty, mrp, igst, cgst, sgct, taxamt, finalamt);
                }
                model.AddedBy = Session["FranchiseID"].ToString();
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/MM/yyyy");
                model.dtRequest = dtst;

                DataSet ds = model.SaveFranchiseKitRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Request"] = "Request saved successfully. Request Code is : " + ds.Tables[0].Rows[0]["RequestCode"].ToString();
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Request"] = "ERROR : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Request"] = ex.Message;
            }
            return Redirect("KitRequest");

        }

        #endregion

        #region ApproveKitRequest

        public ActionResult ApproveKitRequest()
        {
            Franchise model = new Franchise();
            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.ApproveKitRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestID = r["PK_KitRequestID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.RequestFrom = r["RequestFrom"].ToString();
                        obj.KitID = r["FK_KitID"].ToString();
                        obj.KitName = r["KitName"].ToString();
                        obj.Quantity = r["RequestQty"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();

                        lstRequest.Add(obj);
                    }
                    model.lstRequest = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult ApproveRequestKit(string requestID, string approveQty)
        {
            Franchise model = new Franchise();
            try
            {
                model.RequestID = requestID;
                model.Quantity = approveQty;
                model.AddedBy = Session["FranchiseID"].ToString();

                DataSet ds = model.ApproveKitRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["KRequestApproved"] = "Request approved.";
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["KRequestApproved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        model.Result = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["KRequestApproved"] = ex.Message;
                model.Result = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult PrintTopup(string invid)
        {
            List<Reports> list = new List<Reports>();
            Reports model = new Reports();
            if (invid != null)
            {
                model.FK_InvestmentID = Crypto.Decrypt(invid);
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
                            obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.HSNCode = r["HSNCode"].ToString();
                            
                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["SaleOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString();
                            
                            ViewBag.CompanyName = SoftwareDetails.CompanyName;
                            ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                            ViewBag.Pin1 = SoftwareDetails.Pin1;
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

        public ActionResult GetKitAmountForKitRequest(string kitid)
        {
            Franchise obj = new Franchise();
            obj.KitID = kitid;
        
            DataSet ds = obj.KitList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.TotalAmount = ds.Tables[0].Rows[0]["ManagedAmount"].ToString();
                obj.Result = "1";
            }
            else { obj.Result = "0"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KitStatusList(Franchise model)
        {

            try
            {
                model.PK_FranchiseID = Session["FranchiseID"].ToString();
                DataSet ds = model.KitStatusList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestID = r["PK_KitRequestID"].ToString();
                        obj.RequestCode = r["RequestCode"].ToString();                   
                        obj.KitName = r["KitName"].ToString();
                        obj.Quantity = r["RequestQty"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.Status = r["Status"].ToString();
                        obj.ApprovedQuantity = r["ApproveQty"].ToString();

                        lstRequest.Add(obj);
                    }
                    model.lstRequest = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult TopupList()
        {
            Reports model = new Reports();
            try
            {
                model.LoginId = Session["LoginID"].ToString();
                DataSet ds = model.GetTopupReport();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Reports> lstTopupReport = new List<Reports>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.FK_InvestmentID = Crypto.Encrypt(r["Pk_InvestmentId"].ToString());
                        obj.Name = r["Name"].ToString() + " (" + r["LoginId"].ToString() + ")";
                        obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                        obj.ProductName = r["Package"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        lstTopupReport.Add(obj);
                    }
                    model.lsttopupreport = lstTopupReport;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

    }
}
