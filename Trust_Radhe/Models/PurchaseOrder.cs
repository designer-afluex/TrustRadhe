using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrustRadhe.Models
{
    public class PurchaseOrder : Common
    {
        #region Properties
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string TradeIn { get; set; }
        public string PurchaseType { get; set; }
        public string BatchNo { get; set; }
        public string Description { get; set; }
        public string PaymentModeID { get; set; }
        public DataTable dtPurchaseOrder { get; set; }
        public string TotalFinalAmount { get; set; }
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
        public List<PurchaseOrder> lstFranchise { get; set; }
        public List<PurchaseOrder> lstStock { get; set; }
        public string SupplierID { get; set; }
        public string TIN { get; set; }  public string Amount { get; set; }
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
        public string PurchaseItemID { get; internal set; }
        public string Stock { get; internal set; }
        public string BankName { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        
        #endregion


        public DataSet SupplierList()
        {

            SqlParameter[] para = { new SqlParameter("@PK_SupplierID", SupplierID),

            };
            DataSet ds = DBHelper.ExecuteQuery("SupplierList", para);
            return ds;
        }


        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FProductId", ProductID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@BatchNo", BatchNo), };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductList", para);
            return ds;
        }
        public DataSet GetProductdetailsForKit()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FProductId", ProductID),
                                   
                                    new SqlParameter("@BatchNo", BatchNo), };
            DataSet ds = DBHelper.ExecuteQuery("ProductDetailsForKit", para);
            return ds;
        }
        
        public DataSet SavePurchaseOrder()
        {
            SqlParameter[] para = { new SqlParameter("@PurchaseDetails", dtPurchaseOrder),
                                 new SqlParameter("@PurchaseDate", PurchaseDate),
                                 new SqlParameter("@Fk_SupplierId", SupplierID),
                                 new SqlParameter("@PaymentMode", PaymentMode),
                                 new SqlParameter("@PaidAmount", PaidAmount),
                                 new SqlParameter("@Balance", Balance),
                                 new SqlParameter("@AddedBy", AddedBy),
                                 new SqlParameter("@PaymentModeId", PaymentModeID),
                                 new SqlParameter("@TransactionNo", DDChequeNo),
                                 new SqlParameter("@TransactionDate", DDChequeDate),
                                 new SqlParameter("@BankName", BankName),
                                 new SqlParameter("@BankBranch", BankBranch),
                                 new SqlParameter("@BatchNo", BatchNo), };
            DataSet ds = DBHelper.ExecuteQuery("SavePurchaseOrder", para);
            return ds;
        }
        public DataSet PurchaseOrderList()
        {
            SqlParameter[] para = {

                new SqlParameter("@Pk_PurchaseId", PurchaseID),
                         new SqlParameter("@SupplierName", SupplierID),
                           new SqlParameter("@PurchaseOrderNo", PurchaseOrderNumber),
                             new SqlParameter("@PaymentMode", PaymentMode),
                               new SqlParameter("@FromDate", FromDate),
                                 new SqlParameter("@ToDate", ToDate),
                                 new SqlParameter("@BatchNo", BatchNo),
            };
            DataSet ds = DBHelper.ExecuteQuery("PurchaseOrderList", para);
            return ds;
        }
    

    public DataSet GetPurchaseDetails()
    {
        SqlParameter[] para = { new SqlParameter("@Fk_PurchaseId", PurchaseID), };
        DataSet ds = DBHelper.ExecuteQuery("GetPurchaseOrderDetails", para);
        return ds;
    }
    public DataSet SaveSupplierBalancePayment()
    {
        SqlParameter[] para = {


                                 new SqlParameter("@PaymemtDate", PurchaseDate),
                                 new SqlParameter("@Fk_SupplierId", SupplierID),
                                 new SqlParameter("@PaymentMode", PaymentMode),
                                 new SqlParameter("@PaidAmount", PaidAmount),

                                 new SqlParameter("@AddedBy", AddedBy),

                                 new SqlParameter("@TransactionNo", DDChequeNo),
                                 new SqlParameter("@TransactionDate", DDChequeDate),
                                 new SqlParameter("@BankName", BankName),
                                 new SqlParameter("@BankBranch", BankBranch),
                                   new SqlParameter("@Narration", Description),


            };
        DataSet ds = DBHelper.ExecuteQuery("SaveSupplierBalance", para);
        return ds;
    }

}
}