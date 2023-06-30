using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TrustRadhe.Models;
using TrustRadhe.Filter;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace TrustRadhe.Controllers
{
    public class FranchiseAdminController : FranchiseAdminBaseController
    {
        DataTable dt = new DataTable();

        #region Dashboard
        public ActionResult DashBoard()
        {
            Franchise model = new Franchise();
            DataSet Ds = model.GetDashBoardDetails();

            ViewBag.TotalPurchase = Ds.Tables[0].Rows[0]["Total"].ToString();
            ViewBag.TotalSale = Ds.Tables[0].Rows[1]["Total"].ToString();
            ViewBag.TotalRequest = Ds.Tables[0].Rows[2]["Total"].ToString();
            ViewBag.TotalApprovedRequest = Ds.Tables[0].Rows[3]["Total"].ToString();
            ViewBag.TotalRejectedRequest = Ds.Tables[0].Rows[4]["Total"].ToString();
            ViewBag.TotalFranchiseSale = Ds.Tables[0].Rows[5]["Total"].ToString();
            ViewBag.TotalStock = Ds.Tables[0].Rows[6]["Total"].ToString();
            ViewBag.SupplierBalance = Ds.Tables[0].Rows[6]["Total"].ToString();
            return View(model);
        }
        public ActionResult SalePurchaseDetails()
        {
            List<Franchise> dataList = new List<Franchise>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            Franchise model = new Franchise();

            Ds = model.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[1].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[1].Rows)
                {
                    Franchise details = new Franchise();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPayoutStatus()
        {
            List<Franchise> dataList = new List<Franchise>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            Franchise newdata = new Franchise();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[2].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[2].Rows)
                {
                    Franchise details = new Franchise();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJoiningDeatils()
        {
            List<Franchise> dataList = new List<Franchise>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            Franchise newdata = new Franchise();

            Ds = newdata.GetDashBoardDetails();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[3].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[3].Rows)
                {
                    Franchise details = new Franchise();


                    details.Total = (dr["Total"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetailsForSale();
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
            else { obj.Result = "Invalid Franshide Id"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region FranchiseRegistration

        public ActionResult Registration(string id)
        {
            #region BindFranchiseType
            Common obj = new Common();
            List<SelectListItem> ddlFranchiseType = new List<SelectListItem>();
            DataSet ds1 = obj.BindFranchiseType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlFranchiseType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlFranchiseType.Add(new SelectListItem { Text = r["FranchiseType"].ToString(), Value = r["PK_FranchiseTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlFranchiseType = ddlFranchiseType;

            #endregion
            if (id != null)
            {
                Franchise model = new Franchise();
                model.PK_FranchiseID = Crypto.Decrypt(id);
                DataSet ds = model.FranchiseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_FranchiseID = ds.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                    model.ReferBy = ds.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.FranchiseType = ds.Tables[0].Rows[0]["FK_FranchiseTypeID"].ToString();
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
            return View();
        }
        [HttpPost]
        [ActionName("FranchiseRegistration")]
        [OnAction(ButtonName = "btnRegister")]
        public ActionResult FranchiseRegistrationAction(Franchise model)
        {
            try
            {
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(pass);
                model.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.FranchiseRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Franchise"] = "Franchise Registation successfull. Login ID : " + ds.Tables[0].Rows[0]["LoginID"].ToString() + "\nPassword : " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());

                        string passwrd = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        string loginid = Crypto.Decrypt(ds.Tables[0].Rows[0]["LoginID"].ToString());
                        string mob = model.Contact;

                        try
                        {
                            string str = "Dear " + model.FranchiseName + ", You have been successfully registered as Maker Property Franchise !Your Login ID is " + ds.Tables[0].Rows[0]["LoginID"].ToString() + " and Password is  " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString() + ".");
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }


                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Franchise"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Franchise"] = ex.Message;
            }
            return RedirectToAction("Registration");
        }
        [HttpPost]
        [ActionName("FranchiseRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFranchise(Franchise model)
        {
            try
            {
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(pass);
                model.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.UpdateFranchise();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Franchise"] = "Franchise details updated successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Franchise"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Franchise"] = ex.Message;
            }
            return RedirectToAction("Registration");
        }

        public ActionResult FranchiseList()
        {
            Franchise model = new Franchise();
            try
            {
                DataSet ds = model.FranchiseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_FranchsieID"].ToString());
                        obj.Password = Crypto.Decrypt(r["Password"].ToString());
                        obj.LoginID = r["LoginID"].ToString();
                        obj.PK_FranchiseID = r["PK_FranchsieID"].ToString();
                        obj.FranchiseType = r["FK_FranchiseTypeID"].ToString();
                        obj.FirmName = r["FirmName"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.PAN = r["PAN"].ToString();
                        obj.GSTNo = r["GSTNo"].ToString();
                        obj.AdharNo = r["AdharNo"].ToString();
                        obj.PinCode = r["Pincode"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["FranchiseList"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("FranchiseList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FranchiseListBy(Franchise model)
        {

            try
            {
                DataSet ds = model.FranchiseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_FranchsieID"].ToString());
                        obj.Password = Crypto.Decrypt(r["Password"].ToString());
                        obj.LoginID = r["LoginID"].ToString();
                        obj.PK_FranchiseID = r["PK_FranchsieID"].ToString();
                        obj.FranchiseType = r["FK_FranchiseTypeID"].ToString();
                        obj.FirmName = r["FirmName"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.PAN = r["PAN"].ToString();
                        obj.GSTNo = r["GSTNo"].ToString();
                        obj.AdharNo = r["AdharNo"].ToString();
                        obj.PinCode = r["Pincode"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                //TempData["Class"] = "alert alert-danger";
                TempData["FranchiseList"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeleteFranchise(string id)
        {
            try
            {
                Franchise model = new Franchise();
                model.PK_FranchiseID = Crypto.Decrypt(id);
                model.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.DeleteFranchise();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["FranchiseList"] = "Franchise deleted successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["FranchiseList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["FranchiseList"] = ex.Message;
            }
            return RedirectToAction("FranchiseList");
        }
        #endregion

        #region FranchiseStock

        public ActionResult FranchiseStock()
        {
            Franchise model = new Franchise();
            try
            {
                #region ProductBind
                Common objcomm = new Common();
                List<SelectListItem> ddlProduct = new List<SelectListItem>();
                DataSet ds1 = objcomm.BindProductForFranchisee();
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

                List<SelectListItem> ddlAction = Common.TransactionType();
                ViewBag.ddlAction = ddlAction;

                #endregion

                DataSet ds = model.FranchiseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_FranchsieID"].ToString());
                        obj.PK_FranchiseID = r["PK_FranchsieID"].ToString();
                        obj.FranchiseType = r["FranchiseType"].ToString();
                        obj.FirmName = r["FirmName"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.PAN = r["PAN"].ToString();
                        obj.GSTNo = r["GSTNo"].ToString();
                        obj.AdharNo = r["AdharNo"].ToString();
                        obj.PinCode = r["Pincode"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult SaveFranchiseStock(Franchise model)
        {
            try
            {
                DataTable dtStock = new DataTable();
                dtStock.Columns.Add("ID", typeof(string));
                dtStock.Columns.Add("FranchiseID", typeof(string));
                dtStock.Columns.Add("ProductID", typeof(string));
                dtStock.Columns.Add("Qty", typeof(string));

                string rowcount = Request["hdRowCount"].ToString();

                for (int i = 1; i < int.Parse(rowcount); i++)
                {
                    string franchiseid = Request["hdFranchiseID_" + i].ToString();
                    string productid = model.ProductID;
                    string qty = Request["txtStockQty_" + i].ToString();

                    DataRow dr = dtStock.NewRow();
                    //dr["FranchiseID"] = franchiseid;
                    //dr["ProductID"] = productid;
                    //dr["Qty"] = qty;

                    dtStock.Rows.Add(i, franchiseid, productid, qty);
                }
                model.dtStock = dtStock;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveFranchiseStock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Stock"] = "Stock " + model.Action + "ed successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Stock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Stock"] = ex.Message;
            }
            return RedirectToAction("FranchiseStock");
        }

        #endregion

        #region ProductMaster

        public ActionResult Product(string id)
        {
            Franchise model = new Franchise();
            #region ddlUnit

            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds11 = model.UnitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitId"].ToString() }); }
            }

            ViewBag.ddlUnit = ddlUnit;
            #endregion

            if (id != null)
            {

                model.ProductID = Crypto.Decrypt(id);
                DataSet ds = model.ProductList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.EncryptKey = ds.Tables[0].Rows[0]["PK_ProductId"].ToString();
                    model.ProductID = ds.Tables[0].Rows[0]["PK_ProductId"].ToString();
                    model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    model.Size = ds.Tables[0].Rows[0]["Size"].ToString();
                    model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();
                    model.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                    model.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                    model.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                    model.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    model.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                    model.DP = ds.Tables[0].Rows[0]["DP"].ToString();
                    model.HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    model.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    model.ProductCode = ds.Tables[0].Rows[0]["ProductCode"].ToString();
                    model.TradeIn = ds.Tables[0].Rows[0]["TradeIn"].ToString();
                }
                return View(model);
            }
            return View();
        }


        [HttpPost]
        [ActionName("Product")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProduct(Franchise model)
        {
            string FormName = "";
            string Controller = "";
            try
            {


                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.SaveProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["FProduct"] = " Product saved successfully !";

                    }
                    else
                    {
                        TempData["FProduct"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FProduct"] = ex.Message;
            }
            FormName = "Product";
            Controller = "FranchiseAdmin";

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("Product")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateProduct(Franchise model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.UpdateFranchiseProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["FProduct"] = " Product updated successfully !";

                    }
                    else
                    {
                        TempData["FProduct"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["FProduct"] = ex.Message;
            }
            FormName = "Product";
            Controller = "FranchiseAdmin";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ProductList()
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

            #region ddlUnit
            int count2 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds11 = model1.UnitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitId"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;
            #endregion

            Franchise model = new Franchise();
            try
            {
                DataSet ds = model.ProductList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_ProductId"].ToString());
                        obj.ProductID = r["PK_ProductId"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Size = r["Size"].ToString();
                        obj.UnitID = r["FK_UnitID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.DP = r["DP"].ToString();
                        obj.HSNCode = r["HSNCode"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ProductList"] = ex.Message;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ProductList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductListBy(Franchise model)
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

            #region ddlUnit
            int count2 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds11 = model1.UnitList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitId"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;
            #endregion



            try
            {
                model.UnitID = model.UnitID == "0" ? null : model.UnitID;
                model.ProductID = model.ProductID == "0" ? null : model.ProductID;
                DataSet ds = model.ProductList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_ProductId"].ToString());
                        obj.ProductID = r["PK_ProductId"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Size = r["Size"].ToString();
                        obj.UnitID = r["FK_UnitID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.DP = r["DP"].ToString();
                        obj.HSNCode = r["HSNCode"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ProductList"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeleteFranchiseProduct(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Franchise obj = new Franchise();
                obj.ProductID = id;
                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.DeleteProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["fProduct"] = "Product deleted successfully";
                        FormName = "ProductList";
                        Controller = "FranchiseAdmin";
                    }
                    else
                    {
                        TempData["fProduct"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ProductList";
                        Controller = "FranchiseAdmin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["fProduct"] = ex.Message;
                FormName = "ProductList";
                Controller = "FranchiseAdmin";
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region  SupplierRegisrtration
        public ActionResult SupplierRegistration(string id)
        {
            Franchise model = new Franchise();

            if (id != null)
            {

                model.SupplierID = Crypto.Decrypt(id);
                DataSet ds = model.SupplierList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.EncryptKey = ds.Tables[0].Rows[0]["PK_SupplierID"].ToString();
                    model.SupplierID = ds.Tables[0].Rows[0]["PK_SupplierID"].ToString();
                    model.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                    model.GSTNo = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                    model.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();


                }
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ActionName("SupplierRegistration")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSupplier(Franchise model)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.SaveSupplier();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Supplier"] = " Supplier saved successfully !";

                    }
                    else
                    {
                        TempData["Supplier"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Supplier"] = ex.Message;
            }
            FormName = "SupplierRegistration";
            Controller = "FranchiseAdmin";

            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("SupplierRegistration")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateSupplier(Franchise model)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.UpdateSupplier();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Supplier"] = " Supplier Updated successfully !";

                    }
                    else
                    {
                        TempData["Supplier"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Supplier"] = ex.Message;
            }
            FormName = "SupplierRegistration";
            Controller = "FranchiseAdmin";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult SupplierList()
        {
            Franchise model = new Franchise();
            try
            {
                DataSet ds = model.SupplierList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_SupplierID"].ToString());
                        obj.SupplierID = r["PK_SupplierID"].ToString();
                        obj.SupplierName = r["SupplierName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.GSTNo = r["GSTNo"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.State = r["State"].ToString();
                        obj.City = r["City"].ToString();
                        obj.PinCode = r["PinCode"].ToString();


                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SupplierList"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult DeleteSupplier(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Franchise obj = new Franchise();
                obj.SupplierID = id;
                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.DeleteSupplier();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["DeleteSupplier"] = "Supplier deleted successfully";
                        FormName = "SupplierList";
                        Controller = "FranchiseAdmin";
                    }
                    else
                    {
                        TempData["DeleteSupplier"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "SupplierList";
                        Controller = "FranchiseAdmin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteSupplier"] = ex.Message;
                FormName = "SupplierList";
                Controller = "FranchiseAdmin";
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region  PurchaseOrder
        public ActionResult PurchaseOrder(PurchaseOrder model)
        {
            Session["tmpData"] = null;
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            DataSet ds11 = model.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }


            }

            ViewBag.ddlSupplier = ddlSupplier;
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

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            return View(model);
        }
        public ActionResult BindPurchaseItemList(string ProductID, string Product, string MRP, string IGST, string CGST, string SGST,
                                                string Quantity, string TaxableAmount, string FinalAmount, string batchNo, string mfgdate, string expdate)
        {
            DataTable dtResult = new DataTable();
            PurchaseOrder model = new PurchaseOrder();
            try
            {
                if (Session["tmpData"] != null)
                {
                    string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    //  string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");
                    // changed reverse calc. to simple calc. asked by murari sir on 12 feb ,2020 
                    string Taxamt = ((decimal.Parse(mrp) * (decimal.Parse(SGST) + decimal.Parse(CGST)))/100).ToString("0.00");
                    dt = (DataTable)Session["tmpData"];

                    try
                    {
                        dtResult = dt.Select("Fk_ProductId=" + ProductID + " AND BatchNo=" + batchNo).CopyToDataTable();
                        if (dtResult.Rows.Count > 0)
                        {
                            model.Result = "1";
                            return Json(model, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch { }

                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();

                        dr["Fk_ProductId"] = ProductID;
                        dr["Product"] = Product;
                        dr["MRP"] = MRP;
                        dr["IGST"] = IGST;
                        dr["CGST"] = CGST;
                        dr["SGST"] = SGST;
                        dr["PurchaseQty"] = Quantity;
                        dr["TaxableAmount"] = Taxamt;
                        dr["FinalAmount"] = mrp;
                        dr["BatchNo"] = batchNo;
                        dr["MfgDate"] = mfgdate;
                        dr["ExpDate"] = expdate;

                        dt.Rows.Add(dr);
                        Session["tmpData"] = dt;
                    }
                }
                else
                {
                    string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    //string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST)))).ToString("0.00");
                    // changed reverse calc. to simple calc. asked by murari sir on 12 feb ,2020 
                    string Taxamt = ((decimal.Parse(mrp) * (decimal.Parse(SGST) + decimal.Parse(CGST))) / 100).ToString("0.00");
                    dt.Columns.Add("Fk_ProductId", typeof(string));
                    dt.Columns.Add("Product", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("PurchaseQty", typeof(string));
                    dt.Columns.Add("TaxableAmount", typeof(string));
                    dt.Columns.Add("FinalAmount", typeof(string));
                    dt.Columns.Add("MfgDate", typeof(string));
                    dt.Columns.Add("ExpDate", typeof(string));

                    dt.Columns.Add("BatchNo", typeof(string));
                    DataRow dr = dt.NewRow();

                    dr["Fk_ProductId"] = ProductID;
                    dr["Product"] = Product;
                    dr["MRP"] = MRP;
                    dr["IGST"] = IGST;
                    dr["CGST"] = CGST;
                    dr["SGST"] = SGST;
                    dr["PurchaseQty"] = Quantity;
                    dr["TaxableAmount"] = Taxamt;
                    dr["FinalAmount"] = mrp;
                    dr["BatchNo"] = batchNo;
                    dr["MfgDate"] = mfgdate;
                    dr["ExpDate"] = expdate;

                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;
                }

                dt = (DataTable)Session["tmpData"];
                List<PurchaseOrder> lstTmpData = new List<PurchaseOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["PurchaseQty"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.MfgDate = r["MfgDate"].ToString();
                        obj.ExpDate = r["ExpDate"].ToString();

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
        [ActionName("PurchaseOrder")]
        [OnAction(ButtonName = "btnPurchase")]
        public ActionResult SavePurchaseOrder(PurchaseOrder model)
        {
            try
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
                string mfgdate = "";
                string expdate = "";

                DataTable dtst = new DataTable();

                dtst.Columns.Add("Fk_ProductId", typeof(string));
                dtst.Columns.Add("PurchaseQty", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("TaxableAmount", typeof(string));
                dtst.Columns.Add("FinalAmount", typeof(string));
                dtst.Columns.Add("BatchNo", typeof(string));
                dtst.Columns.Add("MfgDate", typeof(string));
                dtst.Columns.Add("ExpDate", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    Productid = Request["txtproductID_ " + i].ToString();
                    mrp = Request["txtMrp_ " + i].ToString();
                    igst = Request["txtIGST_ " + i].ToString();
                    cgst = Request["txtCGST_ " + i].ToString();
                    sgct = Request["txtSGST_ " + i].ToString();
                    purchaseqty = Request["txtPurchaseQty_ " + i].ToString();
                    taxamt = Request["txtTaxableAmount_ " + i].ToString();
                    finalamt = Request["txtFinalAmount_ " + i].ToString();
                    batchno = Request["txtBatchNo_ " + i].ToString();
                    mfgdate = string.IsNullOrEmpty(Request["txtMfgDate_ " + i] ) ? null : Common.ConvertToSystemDate(Request["txtMfgDate_ " + i].ToString(), "dd/MM/yyyy");
                    expdate = string.IsNullOrEmpty(Request["txtExpDate_ " + i]) ? null : Common.ConvertToSystemDate(Request["txtExpDate_ " + i].ToString(), "dd/MM/yyyy");

                    dtst.Rows.Add(batchno, Productid, purchaseqty, mrp, igst, cgst, sgct, taxamt, finalamt, mfgdate, expdate);

                }
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/MM/yyyy");
                model.PurchaseDate = string.IsNullOrEmpty(model.PurchaseDate) ? null : Common.ConvertToSystemDate(model.PurchaseDate, "dd/MM/yyyy");
               
                model.AddedBy = Session["FranchiseAdminID"].ToString();
                model.dtPurchaseOrder = dtst;

                DataSet ds = model.SavePurchaseOrder();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["PO"] = " Purchase order generated successfully. Purchase Order No. : " + ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();

                        //string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        //string LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        //try
                        //{
                        //    string str = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Your login ID  " + ds.Tables[0].Rows[0]["LoginId"].ToString() + "   is top up by  " + Crypto.Decrypt(ds.Tables[0].Rows[0]["KitName"].ToString() + ".");
                        //    //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                        //    BLSMS.SendSMSNew(mob, str);
                        //}
                        //catch { }
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["PO"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["PO"] = ex.Message;
            }
            return Redirect("PurchaseOrder");

        }
        public ActionResult DeletePurchaseRow(string rowid)
        {
            PurchaseOrder model = new PurchaseOrder();
            try
            {
                DataTable dt = Session["tmpData"] as DataTable;
                dt.Rows.RemoveAt(int.Parse(rowid) - 1);
                dt.AcceptChanges();
                Session["tmpData"] = dt;

                decimal finalAmount = 0;
                dt = (DataTable)Session["tmpData"];
                List<PurchaseOrder> lstTmpData = new List<PurchaseOrder>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();

                        obj.ProductID = r["Fk_ProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["PurchaseQty"].ToString();
                        obj.TaxableAmount = r["TaxableAmount"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.MfgDate = r["MfgDate"].ToString();
                        obj.ExpDate = r["ExpDate"].ToString();

                        finalAmount = finalAmount + Convert.ToDecimal(r["FinalAmount"].ToString());
                        lstTmpData.Add(obj);
                    }
                    model.lstFranchise = lstTmpData;
                    model.FinalAmount = finalAmount.ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductdetails(string ProductID, string batchno)
        {
            PurchaseOrder obj = new PurchaseOrder();
            try
            {
                obj.ProductID = ProductID;
                obj.BatchNo = batchno;
                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.ProductList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.ProductID = ds.Tables[0].Rows[0]["PK_ProductId"].ToString();
                    obj.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    obj.DP = ds.Tables[0].Rows[0]["DP"].ToString();
                    obj.Size = ds.Tables[0].Rows[0]["Size"].ToString();
                    obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                    obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                    obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                    obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                    obj.TaxableAmount = ds.Tables[0].Rows[0]["TaxableAmount"].ToString();
                    obj.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();
                    obj.Stock = ds.Tables[0].Rows[0]["Stock"].ToString();
                    obj.TradeIn = ds.Tables[0].Rows[0]["TradeIn"].ToString();

                    obj.Response = "1";

                }
            }
            catch (Exception ex)
            {
            }
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
                    obj.Balance = ds.Tables[0].Rows[0]["Balance"].ToString();

                    obj.Response = "1";


                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);


        }

        public ActionResult SupplierBalancePayment(PurchaseOrder model)
        {
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            DataSet ds11 = model.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }


            }

            ViewBag.ddlSupplier = ddlSupplier;
            #endregion

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("SupplierBalancePayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSupplierBalancePayment(PurchaseOrder model)
        {
            try
            {
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/MM/yyyy");
                model.PurchaseDate = string.IsNullOrEmpty(model.PurchaseDate) ? null : Common.ConvertToSystemDate(model.PurchaseDate, "dd/MM/yyyy");
                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.SaveSupplierBalancePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BalPayment"] = " Balance paid successfully !";

                    }
                    else
                    {
                        TempData["BalPayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Redirect("SupplierBalancePayment");

        }

        #endregion

        #region PurchaseOrderList
        public ActionResult PurchaseOrderList(PurchaseOrder model)
        {
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            PurchaseOrder model1 = new PurchaseOrder();
            DataSet ds11 = model1.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }


            }

            ViewBag.ddlSupplier = ddlSupplier;
            #endregion

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentModeForList();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion

            try
            {
                DataSet ds = model.PurchaseOrderList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<PurchaseOrder> lstFranchise = new List<PurchaseOrder>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_PurchaseId"].ToString());
                        obj.PurchaseID = r["Pk_PurchaseId"].ToString();
                        obj.PurchaseDate = r["PurchaseDate"].ToString();
                        obj.SupplierID = r["Fk_SupplierId"].ToString();
                        obj.SupplierName = r["SupplierName"].ToString();
                        obj.PurchaseOrderNumber = r["PurchaseOrderNo"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.PurchaseType = r["PurchaseType"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
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
        [ActionName("PurchaseOrderList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PurchaseOrderListBy(PurchaseOrder model)
        {

            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.SupplierID = model.SupplierID == "0" ? null : model.SupplierID;

                DataSet ds = model.PurchaseOrderList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<PurchaseOrder> lstFranchise = new List<PurchaseOrder>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        PurchaseOrder obj = new PurchaseOrder();
                        obj.EncryptKey = Crypto.Encrypt(r["Pk_PurchaseId"].ToString());
                        obj.PurchaseID = r["Pk_PurchaseId"].ToString();
                        obj.PurchaseDate = r["PurchaseDate"].ToString();
                        obj.SupplierID = r["Fk_SupplierId"].ToString();
                        obj.SupplierName = r["SupplierName"].ToString();
                        obj.PurchaseOrderNumber = r["PurchaseOrderNo"].ToString();
                        obj.PaidAmount = r["PaidAmount"].ToString();
                        obj.Balance = r["Balance"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.PurchaseType = r["PurchaseType"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";

            }
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            PurchaseOrder model1 = new PurchaseOrder();
            DataSet ds11 = model1.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }


            }

            ViewBag.ddlSupplier = ddlSupplier;
            #endregion

            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentModeForList();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View(model);

        }
        public ActionResult PurchaseOrderView(string PurchaseID)
        {
            List<PurchaseOrder> list = new List<PurchaseOrder>();
            PurchaseOrder model = new PurchaseOrder();
            if (PurchaseID != null)
            {

                model.PurchaseID = Crypto.Decrypt(PurchaseID);
                try
                {

                    DataSet ds = model.GetPurchaseDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            PurchaseOrder obj = new PurchaseOrder();

                            obj.PurchaseItemID = r["Pk_PurchaseItemId"].ToString();
                            obj.PurchaseID = r["Fk_PurchaseId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["Fk_PurchaseId"].ToString());
                            obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.Quantity = r["PurchaseQty"].ToString();
                            obj.MRP = r["MRP"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.BatchNo = r["BatchNo"].ToString();

                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["PurchaseDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                            ViewBag.Mobile = ds.Tables[0].Rows[0]["Contact"].ToString();
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

                    DataSet ds = model.GetPurchaseDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                        //ViewBag.PurchaseOrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        //ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["PurchaseDate"].ToString();
                        //ViewBag.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                        //ViewBag.mobileno = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                        //ViewBag.TotalAmount = ds.Tables[0].Rows[0]["MRP"].ToString();
                        //ViewBag.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();



                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }

        public ActionResult PurchaseOrderReceipt(string PurchaseID)
        {
            List<PurchaseOrder> list = new List<PurchaseOrder>();
            PurchaseOrder model = new PurchaseOrder();
            if (PurchaseID != null)
            {
                model.PurchaseID = Crypto.Decrypt(PurchaseID);
                try
                {

                    DataSet ds = model.GetPurchaseDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            PurchaseOrder obj = new PurchaseOrder();

                            obj.PurchaseItemID = r["Pk_PurchaseItemId"].ToString();
                            obj.PurchaseID = r["Fk_PurchaseId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["Fk_PurchaseId"].ToString());
                            obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.Quantity = r["PurchaseQty"].ToString();
                            obj.MRP = r["MRP"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            obj.Amount = (decimal.Parse(r["MRP"].ToString()) * decimal.Parse(r["PurchaseQty"].ToString())).ToString();
                            obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.HSNCode = r["HSNCode"].ToString();
                            obj.MfgDate = r["MfgDate"].ToString();
                            obj.ExpDate = r["ExpDate"].ToString();
                            obj.BatchNo = r["BatchNo"].ToString();


                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["ValueBeforeTax"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAdded"].ToString();


                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["PurchaseDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                            ViewBag.Mobile = ds.Tables[0].Rows[0]["Contact"].ToString();
                            ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();

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

                    DataSet ds = model.GetPurchaseDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {


                        //ViewBag.PurchaseOrderNo = ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();
                        //ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["PurchaseDate"].ToString();
                        //ViewBag.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                        //ViewBag.mobileno = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                        //ViewBag.TotalAmount = ds.Tables[0].Rows[0]["MRP"].ToString();
                        //ViewBag.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();



                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }

        #endregion

        #region SaleOrder
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
        public ActionResult SaleOrder(SaleOrder model)
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
        public ActionResult getProductBatchWise(string batchno)
        {
            SaleOrder model = new SaleOrder();
            try
            {
                //model.BatchNo = batchno;

                List<SelectListItem> ddlProduct = new List<SelectListItem>();
                //DataSet ds = model.getProductBatchWise();
                DataSet ds = model.ProductList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //model.ProductID = ds.Tables[0].Rows[0]["PK_ProductID"].ToString();
                    //model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    model.Response = "1";

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    }

                    model.ddlProducts = ddlProduct;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);

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
                        dtResult = dt.Select("Fk_ProductId=" + ProductID + " AND BatchNo=" + batchNo).CopyToDataTable();
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
        [ActionName("SaleOrder")]
        [OnAction(ButtonName = "btnSale")]
        public ActionResult SaveSaleOrder(SaleOrder model)
        {
            try
            {
                if (Request["hdRows"] == null)
                {
                    TempData["purchaseerrro"] = "Please add at least one Product for Sale";
                    return Redirect("SaleOrder");
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
                    model.AddedBy = Session["FranchiseAdminID"].ToString();
                    model.dtPurchaseOrder = dtst;
                    DataSet ds = model.SaveSaleOrder();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["sale"] = " Sale Order generated successfully !";
                            TempData["saleNo"] = "Sale Order No. : " + ds.Tables[0].Rows[0]["PurchaseOrderNo"].ToString();


                            //string name = ds.Tables[0].Rows[0]["Name"].ToString();
                            //string LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            //try
                            //{
                            //    string str = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Your login ID  " + ds.Tables[0].Rows[0]["LoginId"].ToString() + "   is top up by  " + Crypto.Decrypt(ds.Tables[0].Rows[0]["KitName"].ToString() + ".");
                            //    //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            //    BLSMS.SendSMSNew(mob, str);
                            //}
                            //catch { }
                        }
                        else
                        {
                            TempData["purchaseerrro"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Redirect("SaleOrder");
        }

        public ActionResult SaleOrderList(SaleOrder model)
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            try
            {
                DataSet ds = model.SaleOrderList();
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

                DataSet ds = model.SaleOrderList();
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

                    DataSet ds = model.GetSaleDetails();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            SaleOrder obj = new SaleOrder();

                            obj.SaleID = r["Fk_SaleOrderId"].ToString();
                            obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            obj.ProductName = r["ProductName"].ToString();
                            obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.Quantity = r["SaleQty"].ToString();
                            obj.TradeQty = r["TradeQty"].ToString();
                            obj.TotalQty = r["TotalQty"].ToString();
                            obj.DP = r["Amount"].ToString();
                            obj.MRP = r["MRP"].ToString();
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

        public ActionResult SaleOrderReceipt(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {

                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {
                    DataSet ds = model.GetSaleDetails();
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

        #region FranchiseProductRequestList
        public ActionResult RequestList()
        {
            Franchise model = new Franchise();
            #region ddlBatchNo
            int ctrBatch = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            Franchise model12 = new Franchise();
            DataSet dsBatchNo = model12.GetBatchNo();
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

            try
            {
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.GetproductRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.RequestFrom = r["RequestFrom"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.RequestImage = r["RequestImage"].ToString();
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

        [HttpPost]
        [ActionName("RequestList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult RequestListSearch(Franchise model)
        {
            #region ddlBatch
            int count12 = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            Franchise model12 = new Franchise();
            DataSet ds1PB = model12.GetBatchNo();
            if (ds1PB != null && ds1PB.Tables.Count > 0 && ds1PB.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1PB.Tables[0].Rows)
                {
                    if (count12 == 0)
                    {
                        ddlBatch.Add(new SelectListItem { Text = "Select Batch", Value = "0" });
                    }
                    ddlBatch.Add(new SelectListItem { Text = r["BatchNo"].ToString(), Value = r["BatchNo"].ToString() });
                    count12 = count12 + 1;
                }

            }

            ViewBag.ddlBatch = ddlBatch;
            #endregion


            try
            {
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.GetproductRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.RequestFrom = r["RequestFrom"].ToString();


                        obj.TotalAmount = r["TotalAmount"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.RequestImage = r["RequestImage"].ToString();
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

        #endregion

        #region Reports
        public ActionResult StockReport(Franchise model)
        {
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

            try
            {
                DataSet ds = model.StockList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.PK_FranchiseID = r["FK_FranchiseID"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Balance = r["Balance"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("StockReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult StockReportBy(Franchise model)
        {
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
                DataSet ds = model.StockList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.LoginID = r["LoginID"].ToString();
                        obj.PK_FranchiseID = r["FK_FranchiseID"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.BatchNo = r["BatchNo"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Balance = r["Balance"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult StockReportProductWise(string ProductID, string FranchiseID, string batchno)
        {
            List<Franchise> list = new List<Franchise>();
            Franchise model = new Franchise();
            if (ProductID != null && FranchiseID != null)
            {

                model.ProductID = ProductID;
                model.PK_FranchiseID = FranchiseID;
                model.BatchNo = batchno;
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

        public ActionResult SupplierLedger(Franchise model)
        {
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            DataSet ds11 = model.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }

            }

            ViewBag.ddlSupplier = ddlSupplier;
            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("SupplierLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SupplierLedgerBy(Franchise model)
        {


            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.SupplierLedger();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.TransactionDate = r["TransactionDate"].ToString();
                        obj.Credit = r["Credit"].ToString();
                        obj.Debit = r["Debit"].ToString();
                        obj.Narration = r["Narration"].ToString();
                        obj.SupplierName = r["SupplierName"].ToString();
                        obj.Balance = r["Balance"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            #region ddlSupplier
            int count = 0;
            List<SelectListItem> ddlSupplier = new List<SelectListItem>();
            Franchise model1 = new Franchise();
            DataSet ds11 = model1.SupplierList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSupplier.Add(new SelectListItem { Text = "Select Supplier", Value = "0" });
                    }
                    ddlSupplier.Add(new SelectListItem { Text = r["SupplierName"].ToString(), Value = r["PK_SupplierID"].ToString() });
                    count = count + 1;
                }

            }

            ViewBag.ddlSupplier = ddlSupplier;
            #endregion
            return View(model);
        }

        public ActionResult SaleOrderListFranchise(SaleOrder model)
        {
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            try
            {
                DataSet ds = model.SaleOrderListFranchise();
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
                        obj.FranchiseName = r["FranchiseName"].ToString();
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
        [ActionName("SaleOrderListFranchise")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SaleOrderListFranchiseBy(SaleOrder model)
        {
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

                DataSet ds = model.SaleOrderListFranchise();
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
                        obj.FranchiseName = r["FranchiseName"].ToString();
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
        public ActionResult SaleOrderViewFranchise(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {

                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {

                    DataSet ds = model.GetSaleDetails();
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
                            obj.MRP = r["Amount"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            obj.TaxableAmount = r["TaxableAmount"].ToString();

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

        public ActionResult SaleOrderReceiptFranchise(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {

                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {
                    DataSet ds = model.GetSaleDetails();
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
                            obj.MRP = r["Amount"].ToString();
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

        public ActionResult TopupReport()
        {
            Reports model = new Reports();
            try
            {
                DataSet ds = model.GetTopupReportByKit();

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
                obj.UpdatedBy = Session["FranchiseAdminID"].ToString();
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangefPassword"] = "Password updated successfully..";
                        FormName = "FranchiseLogin";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["ChangefPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePassword";
                        Controller = "FranchiseAdmin";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangefPassword"] = ex.Message;
                FormName = "FranchiseLogin";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult ChangeFranchisePassword()
        {
            return View();
        }
        public ActionResult ChangeFPassword(Franchise obj)
        {
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.ChangeFPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangeFpassword"] = "Password Changed successfully";

                    }
                    else
                    {
                        TempData["ChangeFpassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangeFpassword"] = ex.Message;
            }
            return RedirectToAction("ChangeFranchisePassword", "FranchiseAdmin");
        }



        #endregion

        #region KitMaster

        public ActionResult KitMaster(Franchise model)
        {
            #region ddlProduct

            List<SelectListItem> ddlMProduct = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.ProductListMLM();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlMProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlMProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlMProduct = ddlMProduct;
            #endregion
            #region FranchiseProducts
            DataSet ds = model.ProductList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                List<Franchise> lstFranchise = new List<Franchise>();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Franchise obj = new Franchise();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_ProductId"].ToString());
                    obj.ProductID = r["PK_ProductId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Size = r["Size"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.SGST = r["SGST"].ToString();
                    obj.CGST = r["CGST"].ToString();
                    obj.IGST = r["IGST"].ToString();
                    obj.MRP = r["MRP"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.DP = r["DP"].ToString();
                    obj.HSNCode = r["HSNCode"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.TaxableAmount = r["TaxableAmount"].ToString();
                    obj.FinalAmount = r["FinalAmount"].ToString();
                    lstFranchise.Add(obj);
                }
                model.lstFranchise = lstFranchise;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("KitMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEmployeeAttendance(Franchise obj)
        {
            try
            {

                string noofrows = Request["hdRows"].ToString();

                string productid = "";
                string qty = "";
                string chk = "";
                DataTable dtproduct = new DataTable();

                dtproduct.Columns.Add("FK_FProductId", typeof(string));
                dtproduct.Columns.Add("Quantity", typeof(string));
                for (int i = 1; i < int.Parse(noofrows); i++)
                {
                    chk = Request["checkBoxId_ " + i];
                    if (chk == "on")
                    {

                        productid = Request["fprodid " + i].ToString();
                        qty = Request["txtQty " + i].ToString();

                        dtproduct.Rows.Add(productid, qty);
                    }

                }
                obj.dtStock = dtproduct;

                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveKit();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Kit"] = "Kit saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Kit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Kit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Attendancemsg"] = ex.Message;
            }
            return RedirectToAction("KitMaster", "FranchiseAdmin");
        }

        public ActionResult KitList()
        {
            Franchise model = new Franchise();
            try
            {
                DataSet ds = model.KitList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.ProductName = r["ProductName"].ToString();
                        obj.KitName = r["KitName"].ToString();
                        obj.TotalAmount = r["ManagedAmount"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

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
            #region ddlBatch
            int count12 = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            Franchise model12 = new Franchise();
            DataSet ds1PB = model12.BatchNoList();
            if (ds1PB != null && ds1PB.Tables.Count > 0 && ds1PB.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1PB.Tables[0].Rows)
                {
                    if (count12 == 0)
                    {
                        ddlBatch.Add(new SelectListItem { Text = "Select Batch", Value = "0" });
                    }
                    ddlBatch.Add(new SelectListItem { Text = r["BatchNo"].ToString(), Value = r["BatchNo"].ToString() });
                    count12 = count12 + 1;
                }
            }

            ViewBag.ddlBatch = ddlBatch;
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
                if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
                {
                    model.FranchiseName = dsSiteRate.Tables[0].Rows[0]["FranchiseName"].ToString();
                    model.PK_FranchiseID = dsSiteRate.Tables[0].Rows[0]["PK_FranchsieID"].ToString();
                    model.Result = "yes";
                }

                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetProductByKit(string PK_FranchiseID, string KitID, string BatchID)
        {
            try
            {
                Franchise model = new Franchise();
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                model.KitID = KitID;
                model.BatchID = BatchID;

                #region GetProductByKit
                DataSet dsSiteRate = model.GetProductByKit();
                Session["dsSiteRate"] = dsSiteRate.Tables[0];

                if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    model.Total = dsSiteRate.Tables[0].Rows[0]["ManagedAmount"].ToString();
                    foreach (DataRow r in dsSiteRate.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.ProductName = r["ProductName"].ToString();
                        obj.StockID = r["Stock"].ToString();
                        obj.Quantity = r["Quantity"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetDetailsRequest(string RequestCode)
        {
            #region ddlBatch
            int count12 = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            Franchise model12 = new Franchise();
            DataSet ds1PB = model12.GetBatchNo();
            if (ds1PB != null && ds1PB.Tables.Count > 0 && ds1PB.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1PB.Tables[0].Rows)
                {
                    if (count12 == 0)
                    {
                        ddlBatch.Add(new SelectListItem { Text = "Select Batch", Value = "0" });
                    }
                    ddlBatch.Add(new SelectListItem { Text = r["BatchNo"].ToString(), Value = r["BatchNo"].ToString() });
                    count12 = count12 + 1;
                }

            }

            ViewBag.ddlBatch = ddlBatch;
            #endregion
            try
            {
                Franchise model = new Franchise();

                model.RequestCode = RequestCode;
                #region GetSiteRate
                DataSet dsSiteRate = model.FranchiseProductRequestList();

                if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();

                    foreach (DataRow r in dsSiteRate.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.RequestID = r["PK_RequestID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Quantity = r["RequestQty"].ToString();
                        obj.TotalAmount = r["TotalAmount"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
                #endregion

                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult CheckStock(string Quantity, string KitID, string BatchID)
        {
            Franchise model = new Franchise();
            model.Quantity = Quantity;
            model.KitID = KitID;
            model.BatchID = BatchID;
            model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
            DataSet dsSiteRate = model.GetProductByKit();


            if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
            {
                List<Franchise> lstFranchise = new List<Franchise>();
                //  model.Total = dsSiteRate.Tables[0].Rows[0]["ManagedAmount"].ToString();
                foreach (DataRow r in dsSiteRate.Tables[0].Rows)
                {
                    Franchise obj = new Franchise();

                    obj.ProductName = r["ProductName"].ToString();
                    obj.StockID = r["Stock"].ToString();
                    obj.Quantity = (Convert.ToDecimal(Quantity) * Convert.ToDecimal(r["Quantity"])).ToString();
                    lstFranchise.Add(obj);
                }
                model.lstFranchise = lstFranchise;
                Session["tbl"] = lstFranchise;
            }



            //DataTable ds = Session["dsSiteRate"] as DataTable;
            //List<Franchise> lstFranchise = new List<Franchise>();
            //foreach (DataRow r in ds.Rows)
            //{
            //    Franchise obj = new Franchise();

            //    obj.ProductName = r["ProductName"].ToString();
            //    obj.StockID = r["Stock"].ToString();
            //    obj.Quantity = (Convert.ToDecimal(Quantity) * Convert.ToDecimal(r["Quantity"])).ToString();

            //    lstFranchise.Add(obj);
            //}
            //model.lstFranchise = lstFranchise;
            //Session["tbl"] = lstFranchise;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("DirectKitTransfer")]
        [OnAction(ButtonName = "Transfer")]
        public ActionResult Transfer(Franchise obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string qty = "";
                string stock = "";

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    qty = Request["Quantity " + i].ToString();
                    stock = Request["StockID " + i].ToString();

                    if (Convert.ToInt32(stock) < Convert.ToInt32(qty))
                    {
                        TempData["TransferKit"] = "Out Of Stock";
                        return RedirectToAction("DirectKitTransfer", "FranchiseAdmin");
                    }
                }

                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = new DataSet();
                obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                ds = obj.TransferKit();
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
            return RedirectToAction("DirectKitTransfer", "FranchiseAdmin");
        }

        public ActionResult GetProductdetailsForKit(string ProductID)
        {
            PurchaseOrder obj = new PurchaseOrder();
            try
            {

                obj.ProductID = ProductID;

                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.GetProductdetailsForKit();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.ProductID = ds.Tables[0].Rows[0]["PK_ProductId"].ToString();
                    obj.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    obj.DP = ds.Tables[0].Rows[0]["DP"].ToString();
                    obj.Size = ds.Tables[0].Rows[0]["Size"].ToString();
                    obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                    obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                    obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                    obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                    //obj.TaxableAmount = ds.Tables[0].Rows[0]["TaxableAmount"].ToString();
                    //obj.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();
                    //obj.Stock = ds.Tables[0].Rows[0]["Stock"].ToString();
                    obj.Response = "1";

                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddKit(Franchise model)
        {
            Session["tmpData"] = null;

            #region ddlProductMLM

            List<SelectListItem> ddlMProduct = new List<SelectListItem>();
            int countp = 0;
            DataSet ds11 = model.ProductListMLM();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    if (countp == 0)
                    {
                        ddlMProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlMProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    countp = countp + 1;
                }

            }

            ViewBag.ddlMProduct = ddlMProduct;
            #endregion

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

            return View(model);
        }

        public ActionResult BindList(string ProductID, string Product, string MRP, string IGST, string CGST, string SGST,
         string Quantity, string TotalAmont, string BV, string DP, string TotalMRP, string Size, string batchno)
        {
            Franchise model = new Franchise();
            try
            {
                if (Session["tmpData"] != null)
                {
                    //string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    //string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST) + decimal.Parse(IGST)))).ToString("0.00");
                    //string finalamt = (decimal.Parse(Taxamt) + decimal.Parse(mrp)).ToString("0.00");
                    dt = (DataTable)Session["tmpData"];
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr["FK_FProductId"] = ProductID;
                        dr["Product"] = Product;
                        dr["MRP"] = MRP;
                        dr["IGST"] = IGST;
                        dr["CGST"] = CGST;
                        dr["SGST"] = SGST;
                        dr["Quantity"] = Quantity;

                        dr["BV"] = BV;
                        dr["DP"] = DP;
                        dr["Size"] = Size;
                        dr["TotalMRP"] = TotalMRP;
                        dr["batchno"] = batchno;

                        dt.Rows.Add(dr);
                        Session["tmpData"] = dt;
                    }
                }
                else
                {
                    //string mrp = (decimal.Parse(MRP) * decimal.Parse(Quantity)).ToString("0.00");
                    //string Taxamt = (decimal.Parse(mrp) - ((decimal.Parse(mrp) * 100) / (100 + decimal.Parse(SGST) + decimal.Parse(CGST) + decimal.Parse(IGST)))).ToString("0.00");
                    //string finalamt = (decimal.Parse(Taxamt) + decimal.Parse(mrp)).ToString("0.00");
                    dt.Columns.Add("FK_FProductId", typeof(string));
                    dt.Columns.Add("Product", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("TaxableAmount", typeof(string));
                    dt.Columns.Add("FinalAmount", typeof(string));

                    dt.Columns.Add("BV", typeof(string));
                    dt.Columns.Add("DP", typeof(string));
                    dt.Columns.Add("Size", typeof(string));
                    dt.Columns.Add("TotalMRP", typeof(string));
                    dt.Columns.Add("batchno", typeof(string));
                    DataRow dr = dt.NewRow();


                    dr["FK_FProductId"] = ProductID;
                    dr["Product"] = Product;
                    dr["MRP"] = MRP;
                    dr["IGST"] = IGST;
                    dr["CGST"] = CGST;
                    dr["SGST"] = SGST;
                    dr["Quantity"] = Quantity;

                    dr["BV"] = BV;
                    dr["DP"] = DP;
                    dr["Size"] = Size;
                    dr["TotalMRP"] = TotalMRP;
                    dr["batchno"] = batchno;
                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;

                }

                dt = (DataTable)Session["tmpData"];
                List<Franchise> lstTmpData = new List<Franchise>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //ViewBag.CGST = "0";
                    foreach (DataRow r in dt.Rows)
                    {
                        Franchise obj = new Franchise();

                        obj.ProductID = r["FK_FProductId"].ToString();
                        obj.ProductName = r["Product"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.Quantity = r["Quantity"].ToString();

                        obj.BV = r["BV"].ToString();
                        obj.DP = r["DP"].ToString();
                        obj.Size = r["Size"].ToString();
                        obj.TotalMRP = r["TotalMRP"].ToString();
                        obj.BatchNo = r["batchno"].ToString();
                        lstTmpData.Add(obj);
                    }

                    //ViewBag.totaligst = lstTmpData.TotalAmount) * (data.lstSizeTemp[i].PurchaseIGST) / 100;
                    //ViewBag.totalcgst = (data.lstSizeTemp[i].TotalAmount) * (data.lstSizeTemp[i].PurchaseCGST) / 100;
                    //ViewBag.totalsgst = (data.lstSizeTemp[i].TotalAmount) * (data.lstSizeTemp[i].PurchaseSGST) / 100;
                    //ViewBag.totalamount = (Number)(data.lstSizeTemp[i].TotalAmount) + (Number)(totaligst) + (Number)(totalcgst) + (Number)(totalsgst);
                    //ViewBag.totaligst = Convert.ToDecimal(model.TotalAmount=r["TotalAmount"].ToString()) - Convert.ToDecimal(r["PurchaseIGST"].ToString()/100);


                    model.lstFranchise = lstTmpData;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("AddKit")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveKit(Franchise obj)
        {
            try
            {

                string noofrows = Request["hdrows"].ToString();

                string productid = "";
                string qty = "";
                string chk = "";
                string batch = "";
                DataTable dtproduct = new DataTable();

                dtproduct.Columns.Add("FK_FProductId", typeof(string));
                dtproduct.Columns.Add("Quantity", typeof(string));
                dtproduct.Columns.Add("batchno", typeof(string));
                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    //chk = Request["checkBoxId_ " + i];


                    productid = Request["txtproductID_ " + i].ToString();
                    qty = Request["txtPurchaseQty_ " + i].ToString();
                    batch = "0";
                    dtproduct.Rows.Add(productid, qty, batch);


                }
                obj.dtStock = dtproduct;

                obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveKit();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Kit"] = "Kit saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Kit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Kit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Attendancemsg"] = ex.Message;
            }
            return RedirectToAction("AddKit", "FranchiseAdmin");
        }
        public ActionResult GetMLMProductdetails(string MLMProductID)
        {
            Franchise obj = new Franchise();
            try
            {

                obj.MLMProductID = MLMProductID;
                //  obj.AddedBy = Session["FranchiseAdminID"].ToString();
                DataSet ds = obj.ProductListMLM();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.MLMPoductName = ds.Tables[0].Rows[0]["ProductPrice"].ToString();

                    obj.Response = "1";

                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion

        public ActionResult ApproveRequest(string requestID, string approveQty, string batch)
        {
            Franchise model = new Franchise();
            try
            {
                model.RequestID = requestID;
                model.Quantity = approveQty;
                model.BatchID = batch;
                model.AddedBy = Session["FranchiseAdminID"].ToString();

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
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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

        public ActionResult ApproveKitRequest()
        {
            Franchise model = new Franchise();
            #region ddlBatch
            int count12 = 0;
            List<SelectListItem> ddlBatch = new List<SelectListItem>();
            Franchise model12 = new Franchise();
            DataSet ds1PB = model12.GetBatchNo();
            if (ds1PB != null && ds1PB.Tables.Count > 0 && ds1PB.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1PB.Tables[0].Rows)
                {
                    if (count12 == 0)
                    {
                        ddlBatch.Add(new SelectListItem { Text = "Select Batch", Value = "0" });
                    }
                    ddlBatch.Add(new SelectListItem { Text = r["BatchNo"].ToString(), Value = r["BatchNo"].ToString() });
                    count12 = count12 + 1;
                }

            }

            ViewBag.ddlBatch = ddlBatch;
            #endregion

            try
            {
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
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
                        obj.LoginID = r["LoginID"].ToString();
                        obj.RequestImage = r["RequestImage"].ToString();

                        obj.EncryptKitID = Crypto.Encrypt(r["FK_KitID"].ToString());
                        obj.EncryptRequestID = Crypto.Encrypt(r["PK_KitRequestID"].ToString());
                        obj.EncryptLoginID = Crypto.Encrypt(r["LoginID"].ToString());
                        obj.EncryptRequestQty = Crypto.Encrypt(r["RequestQty"].ToString());

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
        [HttpPost]
        [ActionName("KitRequestDetails")]
        [OnAction(ButtonName = "btnApprove")]
        public ActionResult KitRequestApprove(Franchise model)
        {
            try
            {
                string error = "0";
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductID", typeof(string));
                dt.Columns.Add("BatchNo", typeof(string));

                string rowCount = Request["hdRows"].ToString();

                for (int i = 0; i < int.Parse(rowCount); i++)
                {
                    string pid = Request["hdProductID_" + i].ToString();
                    string bno = Request["ddlBatchNo_" + i];
                    
                    if (bno == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["KRequestApprovedA"] = "Please select all records.";
                        error = "1";
                        break;
                    }

                    dt.Rows.Add(pid, bno);
                }
                if(error == "0")
                {
                    model.dtKitDetails = dt;
                    model.AddedBy = Session["FranchiseAdminID"].ToString();

                    DataSet ds = model.ApproveKitRequestByAdmin();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Class"] = "alert alert-success";
                            TempData["KRequestApprovedA"] = "Request approved.";
                            model.Result = "1";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Class"] = "alert alert-danger";
                            TempData["KRequestApprovedA"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            model.Result = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("ApproveKitRequest");
        }
        public ActionResult ApproveRequestKit(string requestID, string approveQty, string loginid, string kitid, string batch)
        {
            Franchise model = new Franchise();
            List<Franchise> ds1 = Session["tbl"] as List<Franchise>;

            try
            {
                for (int i = 0; i < ds1.Count; i++)
                {

                    if (Convert.ToInt32(ds1[i].StockID) < Convert.ToInt32(ds1[i].Quantity))
                    {
                        model.Result = "3";
                        //TempData["KRequestApprovedA"] = "Out Of Stock";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                }


                //for (int i = 0; i < ds1.Rows.Count - 1; i++)
                //{

                //    if (Convert.ToInt32(ds1.Rows[i]["Stock"].ToString()) < Convert.ToInt32(ds1.Rows[i]["Quantity"].ToString()))
                //    {
                //        TempData["KRequestApprovedA"] = "Out Of Stock";
                //        return RedirectToAction("ApproveKitRequest", "FranchiseAdmin");
                //    }
                //}
                model.RequestID = requestID;
                model.Quantity = approveQty;
                model.KitID = kitid;
                model.LoginID = loginid;
                model.BatchID = batch;
                model.AddedBy = Session["FranchiseAdminID"].ToString();

                DataSet ds = model.ApproveKitRequestByAdmin();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["KRequestApprovedA"] = "Request approved.";
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["KRequestApprovedA"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        model.Result = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["KRequestApprovedA"] = ex.Message;
                model.Result = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferKitReport(Franchise model)
        {

            try
            {
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.TransferKitReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.FromDate = r["TransactionDate"].ToString();
                        obj.LoginID = r["LoginId"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.KitName = r["KitName"].ToString();
                        obj.TotalAmount = r["KitAmount"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.Narration = r["Narration"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.TransactionNo = r["TransactionNo"].ToString();
                        obj.TransactionDate = r["BankTransactionDate"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.BankBranch = r["BankBranch"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("TransferKitReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransferKitReportBy(Franchise model)
        {

            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                DataSet ds = model.TransferKitReport();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstFranchise = new List<Franchise>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.FromDate = r["TransactionDate"].ToString();
                        obj.LoginID = r["LoginId"].ToString();
                        obj.FranchiseName = r["FranchiseName"].ToString();
                        obj.KitName = r["KitName"].ToString();
                        obj.TotalAmount = r["KitAmount"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.FinalAmount = r["FinalAmount"].ToString();
                        obj.Narration = r["Narration"].ToString();
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.TransactionNo = r["TransactionNo"].ToString();
                        obj.TransactionDate = r["BankTransactionDate"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.BankBranch = r["BankBranch"].ToString();

                        lstFranchise.Add(obj);
                    }
                    model.lstFranchise = lstFranchise;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult Receipt(string SaleID)
        {
            List<SaleOrder> list = new List<SaleOrder>();
            SaleOrder model = new SaleOrder();
            if (SaleID != null)
            {

                model.SaleID = Crypto.Decrypt(SaleID);
                try
                {
                    DataSet ds = model.GetSaleDetails();
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
                            obj.HSNCode = r["HSNCode"].ToString();
                            //obj.MfgDate = r["MfgDate"].ToString();
                            //obj.ExpDate = r["ExpDate"].ToString();
                            obj.BatchNo = r["BatchNo"].ToString();

                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["ValueBeforeTax"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAdded"].ToString();

                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.OrderNo = ds.Tables[0].Rows[0]["SaleOrderNo"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["SaleDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();

                            ViewBag.GSTNO = SoftwareDetails.GSTNO;
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

        public ActionResult PrintTopup(string invid)
        {
            List<Reports> list = new List<Reports>();
            Reports model = new Reports();
            if (invid != null)
            {
                model.ToLoginID = Crypto.Decrypt(invid);
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

        public ActionResult KitRequestDetails(string fid, string kid, string rid, string lid, string rqty)
        {
            Franchise model = new Franchise();
            try
            {
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

                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                model.KitID = Crypto.Decrypt(kid);
                model.RequestID = Crypto.Decrypt(rid);
                model.LoginID = Crypto.Decrypt(lid);
                model.ApprovedQuantity = Crypto.Decrypt(rqty);

                #region GetProductByKit
                DataSet dsSiteRate = model.GetProductByKit();
                Session["dsSiteRate"] = dsSiteRate.Tables[0];

                if (dsSiteRate != null && dsSiteRate.Tables[0].Rows.Count > 0)
                {
                    List<Franchise> lstRequest = new List<Franchise>();
                    model.Total = dsSiteRate.Tables[0].Rows[0]["ManagedAmount"].ToString();
                    foreach (DataRow r in dsSiteRate.Tables[0].Rows)
                    {
                        Franchise obj = new Franchise();
                        obj.ProductID = r["PK_ProductId"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.StockID = r["Stock"].ToString();
                        obj.Quantity = r["Quantity"].ToString();

                        lstRequest.Add(obj);
                    }
                    model.lstRequest = lstRequest;
                }
                #endregion
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(model);
        }

        public ActionResult GetProductStockByBatch(string bno, string pid, string rqty)
        {
            Franchise model = new Franchise();
            try
            {
                model.BatchNo = bno;
                model.ProductID = pid;

                DataSet ds = model.GetProductStockByBatch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(rqty) > int.Parse(ds.Tables[0].Rows[0]["StockBalance"].ToString()))
                    {
                        //Insufficent Stock
                        model.Result = "0";
                        model.Status = "Available Stock : " + ds.Tables[0].Rows[0]["StockBalance"].ToString();
                    }
                    else
                    {
                        model.Result = "1";
                        model.Status = "Available Stock : " + ds.Tables[0].Rows[0]["StockBalance"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet); 
        }

    }
}