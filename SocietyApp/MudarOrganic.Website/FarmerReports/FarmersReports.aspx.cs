using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FarmerReports_FarmersReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnReports();
        }
    }
    protected void btnAFLEsti_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FarmerReports/AflReportFarmerProd.aspx");
    }
    protected void btnAflProd_Click(object sender, EventArgs e)
    {

    }
}