using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TrustRadhe.Models;

namespace TrustRadhe
{
    public partial class DirectKitTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getKitList();
            }
        }
        
        private void getKitList()
        {
            try
            { 
                Franchise model = new Franchise();
                DataSet ds = model.KitList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlKit.DataSource = ds.Tables[0];
                    ddlKit.DataTextField = "KitName";
                    ddlKit.DataValueField = "PK_KitId";
                    ddlKit.DataBind();
                    ddlKit.Items.Insert(0, new ListItem("Select Kit", "0"));
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlKit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Franchise model = new Franchise();
                model.PK_FranchiseID = Session["FranchiseAdminID"].ToString();
                model.KitID = ddlKit.SelectedValue;

                #region GetKitDetails
                DataSet dsKitRate = model.GetProductByKit();
                Session["dsSiteRate"] = dsKitRate.Tables[0];

                if (dsKitRate != null && dsKitRate.Tables[0].Rows.Count > 0)
                {
                    txtKitAmount.Text = dsKitRate.Tables[0].Rows[0]["ManagedAMount"].ToString();
                    List<Franchise> lstFranchise = new List<Franchise>();
                    model.Total = dsKitRate.Tables[0].Rows[0]["ManagedAmount"].ToString();
                    grdKitDetails.DataSource = dsKitRate.Tables[0];
                    grdKitDetails.DataBind();
                    dvPaymentMode.Visible = true;
                    btnTransfer.Visible = true;
                }
                else
                {
                    txtKitAmount.Text = "";
                    grdKitDetails.DataSource = null;
                    grdKitDetails.DataBind();
                    dvPaymentMode.Visible = false;
                    btnTransfer.Visible = false;
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdKitDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = (DropDownList)(e.Row.FindControl("ddlBatch"));
                SaleOrder model = new SaleOrder();
                DataSet ds = model.BatchNoList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds.Tables[0];
                    ddl.DataTextField = "BatchNo";
                    ddl.DataValueField = "BatchNo";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("Select Batch", "0"));
                }
            }
        }

        protected void grdKitDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlBatch_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = sender as DropDownList;
                string id = ddl.ID;

                DropDownList ddlClicked = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddlClicked.NamingContainer;

                string productID = ((Label)(row.FindControl("lblProductID"))).Text;
                string requestedQty = ((Label)(row.FindControl("lblQuantity"))).Text;

                Franchise model = new Franchise();
                model.BatchNo = ddl.SelectedValue;
                model.ProductID = productID;

                DataSet ds = model.GetProductStockByBatch();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(requestedQty) > int.Parse(ds.Tables[0].Rows[0]["StockBalance"].ToString()))
                    {
                        //Insufficent Stock
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Insufficent Stock');", true);
                        //((Label)(row.FindControl("lblQuantity"))).Text = ds.Tables[0].Rows[0]["StockBalance"].ToString();
                        ((Label)(row.FindControl("lblStockMessage"))).ForeColor = System.Drawing.Color.Red;
                        ((Label)(row.FindControl("lblStockMessage"))).Text = "Available Stock : " + ds.Tables[0].Rows[0]["StockBalance"].ToString();
                        ((Label)(row.FindControl("lblStockMessage"))).Visible = true;
                        return;
                    }
                    else
                    {
                        ((Label)(row.FindControl("lblStockMessage"))).ForeColor = System.Drawing.Color.Green;
                        ((Label)(row.FindControl("lblStockMessage"))).Text = "Available Stock : " + ds.Tables[0].Rows[0]["StockBalance"].ToString();
                        ((Label)(row.FindControl("lblStockMessage"))).Visible = true;
                    }
                }
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + ddl.SelectedItem.Text + "');", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTotalAmount.Text = (decimal.Parse(txtKitAmount.Text) * decimal.Parse(txtQuantity.Text)).ToString();
                foreach (GridViewRow row in grdKitDetails.Rows)
                {
                    ((Label)(row.FindControl("lblQuantity"))).Text = txtQuantity.Text;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                Franchise model = new Franchise();
                DataTable dtKitDetails = new DataTable();
                dtKitDetails.Columns.Add("ProductID", typeof(string));
                dtKitDetails.Columns.Add("BatchNo", typeof(string));

                foreach (GridViewRow row in grdKitDetails.Rows)
                {
                    string pid = ((Label)(row.FindControl("lblProductID"))).Text;
                    string bno = ((DropDownList)(row.FindControl("ddlBatch"))).SelectedValue;

                    if (bno == "0")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please select all records.');", true);
                        break;                        
                    }
                    else
                    {
                        dtKitDetails.Rows.Add(pid, bno);
                    }
                }

                model.dtKitDetails = dtKitDetails;
                model.LoginID = txtLoginID.Text;
                model.KitID = ddlKit.SelectedValue;
                model.Quantity = txtQuantity.Text;
                model.AddedBy = Session["FranchiseAdminID"].ToString();
                model.PaymentMode = ddlPaymentMode.SelectedValue;
                model.TransactionNo = string.IsNullOrEmpty(txtTransactionNo.Text) ? null : txtTransactionNo.Text;
                model.TransactionDate = string.IsNullOrEmpty(txtTransactionDate.Text) ? null : Common.ConvertToSystemDate(txtTransactionDate.Text, "dd/MM/yyyy");
                model.BankName = string.IsNullOrEmpty(txtBankName.Text) ? null : txtBankName.Text; 
                model.BankBranch = string.IsNullOrEmpty(txtBankBranch.Text) ? null : txtBankBranch.Text;

                DataSet ds = model.TransferKit();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        //TempData["TransferKit"] = "Kit transferred successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        //TempData["TransferKit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    //TempData["TransferKit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch(Exception ex)
            {
                //TempData["TransferKit"] = ex.Message;
            }
        }

    }
}