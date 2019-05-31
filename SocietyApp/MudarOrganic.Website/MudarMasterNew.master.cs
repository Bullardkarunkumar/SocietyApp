using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MudarMasterNew : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtLoginDetails"] != null)
        {
            var dt = Session["dtLoginDetails"] as DataTable;
            lblUserName.Text = dt.Rows[0]["UserLoginID"].ToString();
        }
    }

    protected void lnklogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login.aspx");
    }
}
