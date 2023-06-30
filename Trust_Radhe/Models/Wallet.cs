using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrustRadhe.Models
{
    public class Wallet : Common
    {
        public string ProductWalletBalance { get; set; }
        public string PlotNumber { get; set; }
        public string Leg { get; set; }
        public bool IsDownline { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentToDate { get; set; }
        public string PaymentFromDate { get; set; }
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string EwalletBalance { get; set; }
        public string PayoutBalance { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string DocumentImg { get; set; }
        public string Package { get; set; }
        public string TopUpDate { get; set; }
        public string NoofPins { get; set; }
        public string FinalAmount { get; set; }

        public DataSet SaveProductWalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@DocumentImg", DocumentImg),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@AddedBy", AddedBy)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("ProductWalletRequest", para);
            return ds;
        }
        public DataSet SaveEWalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@DocumentImg", DocumentImg),
                                          new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }

        public List<Wallet> WalletRequestList { get; set; }

        public DataSet GetEWalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequest", para);
            return ds;
        }
        public DataSet GetProductWalletRequest()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetProductWalletRequest", para);
            return ds;
        }
        public string Status { get; set; }

        public string RequestCode { get; set; }

        public string Narration { get; set; }

        public string DrAmount { get; set; }

        public string CrAmount { get; set; }

        public List<Wallet> lstewalletledger { get; set; }

        public DataSet EwalletLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletLedger", para);
            return ds;
        }

        public DataSet ProductWalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetProductWalletLedger", para);
            return ds;
        }

        public DataSet GetEwalletBalnce()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletBalance", para);
            return ds;
        }

        public DataSet TransferEwalletToEwallet()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", AddedBy),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("TransferEwalletToEwallet", para);
            return ds;
        }

        public DataSet GetpayoutBalance()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }

        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("PayoutRequest", para);
            return ds;
        }

        public List<Wallet> lstPayoutrequest { get; set; }

        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),


                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }

        public List<Wallet> lstpayoutledger { get; set; }

        public DataSet PayoutLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                       new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutLedger", para);
            return ds;
        }

        public DataSet BindPriceByProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductId", Package) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }

        public DataSet TopUpIdByEWallet()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Fk_ProductId", Package)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("TopUpIdByEWallet", para);
            return ds;
        }

        public DataSet TopUpIdByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@Fk_ProductId", Package),
                                    new SqlParameter("@TopupDate", TopUpDate),
                                    new SqlParameter("@Amount", Amount),
									new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
									  new SqlParameter("@BankName", BankName),
                                    //new SqlParameter("@PlotNumber", PlotNumber),
                                    new SqlParameter("@Description", Description) };
            DataSet ds = DBHelper.ExecuteQuery("TopUpByAdmin", para);
            return ds;
        }
        public DataSet TopUpIdByAdminProduct()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@Fk_ProductId", Package),
                                    new SqlParameter("@TopupDate", TopUpDate),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@PlotNumber", PlotNumber),
                                    new SqlParameter("@Description", Description) };
            DataSet ds = DBHelper.ExecuteQuery("ProductTopUpByAdmin", para);
            return ds;
        }
        public DataSet ReTopup()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@Fk_ProductId", Package),
                                    new SqlParameter("@TopupDate", TopUpDate),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@PlotNumber", PlotNumber),
                                    new SqlParameter("@Description", Description) };
            DataSet ds = DBHelper.ExecuteQuery("ReTopup", para);
            return ds;
        }

        public string Pk_RequestId { get; set; }

        public DataSet ApprovePayputRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclinePayoutRequest", para);
            return ds;
        }

        public DataSet ApproveEwalletRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineEwalletRequest", para);
            return ds;
        }

        public DataSet CreatePin()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@Fk_PackageId", Package),
                                        new SqlParameter("@NoofPins", NoofPins),
                                         new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@CreatedBy", AddedBy),
                                         new SqlParameter("@Amount", Amount)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinByAdmin", para);
            return ds;
        }

        public List<Wallet> lstBooster { get; set; }
        public List<Wallet> lstunusedpins { get; set; }

        public string ePinNo { get; set; }

        public string RegisteredTo { get; set; }
        public string RegisteredToName { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet GetUsedUnUsedPins()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@Fk_UserId", Fk_UserId),
                                        new SqlParameter("@EPinNo", ePinNo),
                                        new SqlParameter("@Package", Package),
                                        new SqlParameter("@OwnerID", OwnerID ),
                                        new SqlParameter("@OwnrName",OwnerName ),
                                        new SqlParameter("@RegToId", RegisteredTo ),
                                        new SqlParameter("@RegToName",RegisteredToName ),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedUsedPins", para);
            return ds;
        }
        public DataSet TopupByEpinByAdmin()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@ePinNo", ePinNo),
                                        new SqlParameter("@TopUpTo", LoginId),
                                         new SqlParameter("@CreatedBy", AddedBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("TopupByEpinByAdmin", para);
            return ds;
        }
        public DataSet TopupByEpin()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@ePinNo", ePinNo),
                                        new SqlParameter("@TopUpTo", LoginId),
                                         new SqlParameter("@CreatedBy", AddedBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("TopupByEpin", para);
            return ds;
        }
        public DataSet TransferPin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Epino", ePinNo), };
            DataSet ds = DBHelper.ExecuteQuery("ePinTransfer", para);
            return ds;
        }
        public DataSet GetPaidBooster()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FromPaymentDate", PaymentFromDate),
                                      new SqlParameter("@ToPaymentDate", PaymentToDate),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetPaidBoosterDetails", para);
            return ds;
        }

        #region PaidPayout
        public DataSet GetPaidPayout()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = DBHelper.ExecuteQuery("GetPaidPayoutDetails", para);
            return ds;
        }
        #endregion

        public DataSet SaveAdvancePayment()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@Description", Description),
                                    new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch),
                                    new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("AdvancePayment", para);
            return ds;
        }

        public DataSet AdvancePaymentReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = DBHelper.ExecuteQuery("AdvancePaymentReport", para);
            return ds;
        }
        public DataSet GetPaidProductBooster()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FromPaymentDate", PaymentFromDate),
                                      new SqlParameter("@ToPaymentDate", PaymentToDate),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetProductPaidBoosterDetails", para);
            return ds;
        }
    }
}
