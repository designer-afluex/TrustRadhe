using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TrustRadhe
{
    public partial class ApproveKitRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["rid"] != null)
            {
                ViewState["RequestID"] = Request["rid"].ToString();
            }
        }

        protected void grdKitDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdKitDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}