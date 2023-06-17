using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TrustRadhe.Models
{
    public class Franchise : Common
    {
        #region Properties
        public string EncryptKitID { get; set; }
        public string EncryptRequestID { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptRequestQty { get; set; }
        public string Status { get; set; }
        public string ApprovedQuantity { get; set; }
        public string FranchiseTypeName { get; set; }
        public string Total { get; set; }
        public string Month { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string TaxableAmount { get; set; }
        public string LoginID { get; set; }
        public DataTable dtStock { get; set; }
        public DataTable dtKitDetails { get; set; }
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
        public List<Franchise> lstFranchise { get; set; }
        public List<Franchise> lstStock { get; set; }
        public string SupplierID { get; set; }
        public string TIN { get; set; }
        public string SupplierName { get; set; }
        public string FinalAmount { get; set; }
        public string TotalAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StockID { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Narration { get; set; }
        public string TransactionDate { get; set; }
        public string Balance { get; set; }
        public DataTable dtRequest { get; set; }
        public string RequestID { get; set; }
        public string RequestCode { get; set; }
        public string RequestFrom { get; set; }
        public List<Franchise> lstRequest { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string PaidAmount { get; set; }
        public string ProductCode { get; set; }
        public string TradeIn { get; set; }
        public string MLMProductID { get; set; }
        public string MLMPoductName { get; set; }
        public string KitName { get; set; }
        public string TotalMRP { get; set; }
        public string SubTotal { get; set; }
        public string Description { get; set; }
        public string KitID { get; set; }
        public string MLMProduct { get; internal set; }
        public string Response { get; internal set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string RequestImage { get; set; }
        public string BatchID { get; set; }
        public string BatchNo { get; set; }
        #endregion

        #region FranchiseRegistration
        public DataSet FranchiseRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@SponsorCode", ReferBy),
                                    new SqlParameter("@FK_FranchiseTypeID", FranchiseType),
                                    new SqlParameter("@FirmName", FirmName),
                                    new SqlParameter("@FranchiseName", FranchiseName),
                                    new SqlParameter("@Contact",Contact),
                                    new SqlParameter("@Email", Email),
                                    new SqlParameter("@AdharNo", AdharNo),
                                    new SqlParameter("@PAN", PAN),
                                    new SqlParameter("@GSTNo",GSTNo),
                                    new SqlParameter("@Pincode",PinCode),
                                    new SqlParameter("@Address",Address),
                                    new SqlParameter("@Password",Password),
                                    new SqlParameter("@AddedBy", AddedBy), };

            DataSet ds = DBHelper.ExecuteQuery("FranchiseRegistration", para);
            return ds;
        }

        public DataSet FranchiseList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FranchiseID", PK_FranchiseID),
                new SqlParameter("@LoginID", LoginID),
                new SqlParameter("@FranchiseName", FranchiseName),
                new SqlParameter("@FirmName", FirmName),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseList", para);
            return ds;
        }

        public DataSet DeleteFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FranchiseID", PK_FranchiseID),
                                    new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = DBHelper.ExecuteQuery("DeleteFranchise", para);
            return ds;
        }

        public DataSet UpdateFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@FK_FranchiseID", PK_FranchiseID),
                                    new SqlParameter("@FK_FranchiseTypeID", FranchiseType),
                                    new SqlParameter("@FirmName", FirmName),
                                    new SqlParameter("@FranchiseName", FranchiseName),
                                    new SqlParameter("@Contact",Contact),
                                    new SqlParameter("@Email", Email),
                                    new SqlParameter("@AdharNo", AdharNo),
                                    new SqlParameter("@PAN", PAN),
                                    new SqlParameter("@GSTNo",GSTNo),
                                    new SqlParameter("@Pincode",PinCode),
                                    new SqlParameter("@Address",Address),

                                    new SqlParameter("@UpdatedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("UpdateFranchise", para);
            return ds;
        }
        #endregion

        #region ManualStockEntry
        public DataSet SaveFranchiseStock()
        {
            SqlParameter[] para = { new SqlParameter("@dtStock", dtStock),
                                    new SqlParameter("@Action", Action),
                                    new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseManualStock", para);
            return ds;
        }
        public DataSet UnitList()
        {
            DataSet ds = DBHelper.ExecuteQuery("UnitList");
            return ds;
        }
        public DataSet SaveProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@Size", Size),
                                    new SqlParameter("@FK_UnitID", UnitID),
                                    new SqlParameter("@SGST", SGST),
                                    new SqlParameter("@CGST", CGST),
                                    new SqlParameter("@IGST", IGST),
                                    new SqlParameter("@MRP", MRP),
                                    new SqlParameter("@BV", BV),
                                   new SqlParameter("@DP", DP),
                                   new SqlParameter("@HSNCode", HSNCode),
                                    new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@ProductCode", ProductCode),
                                        new SqlParameter("@TradeIn", TradeIn),
            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProduct", para);
            return ds;
        }
        #endregion

        #region FranchiseStockList
        public DataSet FranchiseStockList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_FranchiseID", PK_FranchiseID), };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductStockList", para);
            return ds;
        }
        #endregion

        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FProductId", ProductID),
                                    new SqlParameter("@ProductName", ProductName),

                                     new SqlParameter("@Size", Size),
                                      new SqlParameter("@HSNCode", HSNCode),
                                       new SqlParameter("@FK_UnitID", UnitID),


            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductList", para);
            return ds;
        }
        public DataSet UpdateFranchiseProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@Size", Size),

                                    new SqlParameter("@SGST", SGST),
                                    new SqlParameter("@CGST", CGST),
                                    new SqlParameter("@IGST", IGST),
                                    new SqlParameter("@MRP", MRP),
                                    new SqlParameter("@BV", BV),
                                   new SqlParameter("@DP", DP),
                                   new SqlParameter("@HSNCode", HSNCode),

                                    new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@ProductID", ProductID),
                                      new SqlParameter("@UnitID", UnitID),

                                       new SqlParameter("@ProductCode", ProductCode),
                                        new SqlParameter("@TradeIn", TradeIn),

            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateFranchiseProduct", para);
            return ds;
        }
        public DataSet SupplierList()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@PK_SupplierID", SupplierID),

            };
            DataSet ds = DBHelper.ExecuteQuery("SupplierList", para);
            return ds;
        }
        public DataSet SaveSupplier()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@SupplierName", SupplierName),
                                   new SqlParameter("@Contact", Contact),
                                   new SqlParameter("@Email", Email),
                                   new SqlParameter("@GSTNo", GSTNo),
                                   new SqlParameter("@Pincode", PinCode),
                                   new SqlParameter("@State", State),
                                   new SqlParameter("@City", City),
                                   new SqlParameter("@Address", Address),
                                   new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("SupplierRegistration", para);
            return ds;
        }
        public DataSet StockList()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_FranchiseID", PK_FranchiseID),
                                   new SqlParameter("@FranchiseName", FranchiseName),
                                   new SqlParameter("@FK_ProductID", ProductID),
                                   new SqlParameter("@LoginID", LoginID),
                                   new SqlParameter("@BatchNo", BatchNo), };
            DataSet ds = DBHelper.ExecuteQuery("StockReport", para);
            return ds;
        }
        public DataSet FranchiseProductRequestList()
        {
            SqlParameter[] para = { new SqlParameter("@RequestCode", RequestCode), };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductRequestList", para);
            return ds;
        }
        public DataSet GetproductRequestList()
        {
            SqlParameter[] para = {
                new SqlParameter("@RequestCode", RequestCode),
                new SqlParameter("@RequestFrom", RequestFrom),
                new SqlParameter("@RequestLoginId", LoginID),
                new SqlParameter("@PaymentMode", PaymentMode),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetproductRequestList", para);
            return ds;
        }
        public DataSet ApproveRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                    new SqlParameter("@ApproveQty", Quantity),
                                    new SqlParameter("@ApprovedBy", AddedBy),
                                    new SqlParameter("@BatchNo", BatchID),};
            DataSet ds = DBHelper.ExecuteQuery("ApproveFranchiseRequest", para);
            return ds;
        }
        public DataSet GetUpperLevelFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID), };
            DataSet ds = DBHelper.ExecuteQuery("GetUpperLevelFranchise", para);
            return ds;
        }
        public DataSet SaveFranchiseProductRequest()
        {
            SqlParameter[] para = { new SqlParameter("@dtProductRequest", dtRequest),

                                   new SqlParameter("@FK_FranchiseID", AddedBy),
                                   new SqlParameter("@TotalAmount", TotalAmount),
                                   new SqlParameter("@PaymentMode", PaymentMode),
                                   new SqlParameter("@TransactionNo", TransactionNo),
                                   new SqlParameter("@TransactionDate", TransactionDate),
                                   new SqlParameter("@BankName", BankName),
                                   new SqlParameter("@BankBranch", BankBranch),
                                   new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@RequestImage", RequestImage),
                                   

            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseProductRequest", para);
            return ds;
        }
        public DataSet StockReportProductWise()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_ProductID", ProductID),
                  new SqlParameter("@FK_FranchiseID", PK_FranchiseID),
                  new SqlParameter("@BatchNo", BatchNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("StockReportProductWise", para);
            return ds;
        }
        public DataSet DeleteProduct()
        {
            SqlParameter[] para = {
                new SqlParameter("@ProductID", ProductID),
                  new SqlParameter("@DeletedBy", AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteFranchiseProduct", para);
            return ds;
        }
        public DataSet DeleteSupplier()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_SupplierID", SupplierID),
                  new SqlParameter("@DeletedBy", AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteSupplier", para);
            return ds;
        }
        public DataSet UpdateSupplier()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@SupplierName", SupplierName),
                                   new SqlParameter("@Contact", Contact),
                                   new SqlParameter("@Email", Email),
                                   new SqlParameter("@TINNo", TIN),
                                   new SqlParameter("@Pincode", PinCode),
                                   new SqlParameter("@State", State),
                                   new SqlParameter("@City", City),
                                   new SqlParameter("@Address", Address),
                                   new SqlParameter("@UpdatedBy", AddedBy),
                                     new SqlParameter("@PK_SupplierID", SupplierID),

            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSupplier", para);
            return ds;
        }
        public DataSet SupplierLedger()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_SupplierId", SupplierID),
                  new SqlParameter("@FromDate", FromDate),
                   new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("SupplierLedger", para);
            return ds;
        }
        public DataSet BatchNoList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetBatchNo");
            return ds;
        }
        public DataSet RequestStatusReport()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_FranchiseID", PK_FranchiseID),

            };
            DataSet ds = DBHelper.ExecuteQuery("RequestStatusReport", para);
            return ds;
        }
        public DataSet GetDashBoardDetails()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDataForFranchiseAdminDashboard");
            return ds;
        }
        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                 new SqlParameter("@PK_FranchiseID", UpdatedBy),
                                        new SqlParameter("@OldPassword", OldPassword),
                                      new SqlParameter("@NewPassword", NewPassword)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangeFranchiseAdminPassword", para);
            return ds;
        }
        public DataSet ProductListMLM()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", MLMProductID) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }
        public DataSet SaveKit()
        {
            SqlParameter[] para = {
                            new SqlParameter("@FK_ProductId", MLMProductID),
                                        new SqlParameter("@KitName", KitName),
                                      new SqlParameter("@TotalAmount", Total),
                                        new SqlParameter("@ManagedAmount", TotalAmount),
                                          new SqlParameter("@Description ", Description),
                                            new SqlParameter("@KitProduct", dtStock),
                                              new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveKit", para);
            return ds;
        }
        public DataSet KitList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_KitId", KitID), };
            DataSet ds = DBHelper.ExecuteQuery("KitList", para);
            return ds;
        }

        public DataSet GetProductStockByBatch()
        {
            SqlParameter[] para = { new SqlParameter("@BatchNo", BatchNo),
                                    new SqlParameter("@FK_ProductID", ProductID) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductBatchWise", para);
            return ds;
        }
        public DataSet GetProductByKit()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ToFranchiseID", PK_FranchiseID),
                                    new SqlParameter("@FK_KitId", KitID),
                                    new SqlParameter("@BatchNo", BatchID), };
            DataSet ds = DBHelper.ExecuteQuery("GetKitDetails", para);
            return ds;
        }
        public DataSet TransferKit()
        {
            SqlParameter[] para = { new SqlParameter("@dtKitTransferDetails", dtKitDetails),
                                    new SqlParameter("@FranchiseCode", LoginID),
                                    new SqlParameter("@Fk_KitId", KitID),
                                    new SqlParameter("@Quantity", Quantity),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch),
                                    new SqlParameter("@BatchNo", BatchID), };
            DataSet ds = DBHelper.ExecuteQuery("DirectKitTransfer", para);
            return ds;
        }
        
        public DataSet TransferKitByFranchise()
        {
            SqlParameter[] para = {
                            new SqlParameter("@FranchiseCode", LoginID),
                            new SqlParameter("@Fk_KitId", KitID),
                             new SqlParameter("@Quantity", Quantity),
                            new SqlParameter("@AddedBy", AddedBy),
                               new SqlParameter("@PaymentMode", PaymentMode),
                              new SqlParameter("@TransactionNo", DDChequeNo),
                                 new SqlParameter("@TransactionDate", DDChequeDate),
                                 new SqlParameter("@BankName", BankName),
                                 new SqlParameter("@BankBranch", BankBranch),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DirectKitTransferByFranchise", para);
            return ds;
        }

        public DataSet KitListForFranchise()
        {
            SqlParameter[] para = { new SqlParameter("@FK_FromFranchiseID", PK_FranchiseID),
                                    new SqlParameter("@Fk_KitId", KitID), };
            DataSet ds = DBHelper.ExecuteQuery("KitStockReportForFranchise", para);
            return ds;
        }

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                    new SqlParameter("@Status", Status), };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateList", para);
            return ds;
        }
        public DataSet TopUpByKit()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                new SqlParameter("@Fk_KitId", KitID),
                new SqlParameter("@AddedBy", AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("TopUpByKit", para);
            return ds;
        }

        public DataSet SaveFranchiseKitRequest()
        {
            SqlParameter[] para = { new SqlParameter("@dtProductRequest", dtRequest),
                                
                                   new SqlParameter("@FK_FranchiseID", AddedBy),
                                   new SqlParameter("@TotalAmount", TaxableAmount),
                                   new SqlParameter("@PaymentMode", PaymentMode),
                                   new SqlParameter("@TransactionNo", TransactionNo),
                                   new SqlParameter("@TransactionDate", TransactionDate),
                                   new SqlParameter("@BankName", BankName),
                                   new SqlParameter("@BankBranch", BankBranch),
                                   new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@RequestImage", RequestImage),
            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseKitRequest", para);
            return ds;
        }

        public DataSet ApproveKitRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_FranchiseID", PK_FranchiseID),

            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseKitRequestList", para);
            return ds;
        }
        
        public DataSet ApproveKitRequestList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RequestID", RequestID),
                                  new SqlParameter("@ApproveQty", Quantity),
                                  new SqlParameter("@ApprovedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("ApproveFranchiseKitRequest", para);
            return ds;
        }
        
        public DataSet KitStatusList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_FranchiseID", PK_FranchiseID),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApprovedKitList", para);
            return ds;
        }

        public DataSet ApproveKitRequestByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@dtKitTransferDetails", dtKitDetails),
                                    new SqlParameter("@FranchiseCode", LoginID),
                                    new SqlParameter("@Fk_KitId", KitID),
                                    new SqlParameter("@Quantity", Quantity),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@RequestID", RequestID) };
            DataSet ds = DBHelper.ExecuteQuery("ApproveKitRequestByAdmin", para);
            return ds;
        }
        public DataSet ChangeFPassword()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                        new SqlParameter("@Password", Password),
                                         new SqlParameter("@UpdatedBy", AddedBy),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangeFranchisePasswordByFAdmin", para);
            return ds;
        }
        public DataSet TransferKitReport()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_FranchiseId", PK_FranchiseID),
                                        new SqlParameter("@LoginID", LoginID),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseKitStock", para);
            return ds;
        }
        public DataSet PurchaseOrderList()
        {
            SqlParameter[] para = {

                
                         new SqlParameter("@SupplierName", SupplierID),
                          
                             new SqlParameter("@PaymentMode", PaymentMode),
                               new SqlParameter("@FromDate", FromDate),
                                 new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PurchaseOrderList", para);
            return ds;
        }
        public DataSet GetBatchNo()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetBatchNo");
            return ds;
        }
    }
    
}