using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrustRadhe.Models
{
    public class SaleOrder : Common
    {
        #region Properties
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public DataTable dtPurchaseOrder { get; set; }
        public string TotalQty { get; set; }
        public string TradeIn { get; set; }
        public string TradeQty { get; set; }
        public string TotalFinalAmount { get; set; }
        public string BatchNo { get; set; }
        public string Stock { get; set; }
        public string SaleDate { get; set; }
        public string LoginID { get; set; }
        public DataTable dtStock { get; set; }
        public string ProductID { get; set; }
        public string Action { get; set; }
        public string EncryptKey { get; set; }
        public string PK_FranchiseID { get; set; }
        public string FranchiseType { get; set; }
        public string FirmName { get; set; }
        public string FranchiseName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string AdharNo { get; set; }
        public string PAN { get; set; }
        public string GSTNo { get; set; }
        public string Password { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string SGST { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string MRP { get; set; }
        public string BV { get; set; }
        public string DP { get; set; }
        public string HSNCode { get; set; }
        public string Quantity { get; set; }
        public List<SaleOrder> lstFranchise { get; set; }
        public List<SaleOrder> lstStock { get; set; }
        public List<SelectListItem> ddlProducts { get; set; }
        public string SupplierID { get; set; }
        public string TIN { get; set; }
        public string SupplierName { get; set; }
        public string PaymentMode { get; set; }
        public string PurchaseDate { get; set; }
        public string TaxableAmount { get; set; }
        public string FinalAmount { get; set; }
        public string TotalAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Balance { get; set; }
        public string Response { get; set; }
        public string PurchaseID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string PurchaseItemID { get; set; }
        public string BankName { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string SaleID { get; set; }
        public string SaleOrderNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        #endregion

        public DataSet SaveSaleOrder()
        {
            SqlParameter[] para = { new SqlParameter("@SaleOrderDetails", dtPurchaseOrder),
                                    new SqlParameter("@SaleDate", SaleDate),
                                    new SqlParameter("@AssociateCode", ReferBy),
                                    new SqlParameter("@TotalAmount", TotalFinalAmount),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@PaidAmount", PaidAmount),
                                    new SqlParameter("@Balance", Balance),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@TransactionNo", DDChequeNo),
                                    new SqlParameter("@TransactionDate", DDChequeDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch), };
            DataSet ds = DBHelper.ExecuteQuery("SaveSaleOrder", para);
            return ds;
        }
        public DataSet SaveSaleOrderForFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@SaleOrderDetails", dtPurchaseOrder),
                                    new SqlParameter("@SaleDate", SaleDate),
                                    new SqlParameter("@AssociateCode", ReferBy),
                                    new SqlParameter("@TotalAmount", TotalFinalAmount),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@PaidAmount", PaidAmount),
                                    new SqlParameter("@Balance", Balance),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@TransactionNo", DDChequeNo),
                                    new SqlParameter("@TransactionDate", DDChequeDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch), };
            DataSet ds = DBHelper.ExecuteQuery("SaveSaleOrderForFranchise", para);
            return ds;
        }
        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FProductId", ProductID),
                                    new SqlParameter("@ProductName", ProductName),
            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductList", para);
            return ds;
        }
        public DataSet BatchNoList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetBatchNo");
            return ds;
        }
        public DataSet SaleOrderList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_SaleOrderId", SaleID),
                                    new SqlParameter("@PK_FranchiseID", PK_FranchiseID),
                                    new SqlParameter("@SaleOrderNo", SaleOrderNumber),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@LoginId", LoginID), };
            DataSet ds = DBHelper.ExecuteQuery("SaleOrderList", para);
            return ds;
        }
        public DataSet SaleListForFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_SaleOrderId", SaleID),
                                    new SqlParameter("@PK_FranchiseID", PK_FranchiseID),
                                    new SqlParameter("@SaleOrderNo", SaleOrderNumber),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@LoginId", LoginID), };
            DataSet ds = DBHelper.ExecuteQuery("SaleListForFranchise", para);
            return ds;
        }
        
        public DataSet SaleOrderListFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_SaleOrderId", SaleID),
                                    new SqlParameter("@PK_FranchiseID", PK_FranchiseID),
                                     new SqlParameter("@SaleOrderNo", SaleOrderNumber),
                             new SqlParameter("@PaymentMode", PaymentMode),
                               new SqlParameter("@FromDate", FromDate),
                                 new SqlParameter("@ToDate", ToDate),
                                   new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@FLogiID", FranchiseType),
                                   new SqlParameter("@FranchiseName", FranchiseName),

            };
            DataSet ds = DBHelper.ExecuteQuery("SaleOrderListForFranchise", para);
            return ds;
        }
        public DataSet GetSaleDetails()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_SaleOrderId", SaleID), };
            DataSet ds = DBHelper.ExecuteQuery("GetSaleOrderDetails", para);
            return ds;
        }
        public DataSet GetSaleOrderDetailsForFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_SaleOrderId", SaleID), };
            DataSet ds = DBHelper.ExecuteQuery("GetSaleOrderDetailsForFranchise", para);
            return ds;
        }
        public DataSet getProductBatchWise()
        {
            SqlParameter[] para = { new SqlParameter("@BatchNo", BatchNo), };
            DataSet ds = DBHelper.ExecuteQuery("GetProductBatchWise", para);
            return ds;
        }

    }
}