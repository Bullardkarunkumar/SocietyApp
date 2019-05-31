using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mudar_vidhyareports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btncollReg_Click(object sender, EventArgs e)
    {
//        Response.Redirect("~/BranchReports/CollectionRegister.aspx");
        Response.Redirect("~/FarmerReports/Reception Register.aspx");
    }
    protected void btnbleReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchReports/BlendingRegister.aspx");
    }
    protected void btnpackReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchReports/PackingRegister.aspx");
    }
    protected void btndisReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchReports/DispatchRegister.aspx");
    }
    protected void btnFreezeReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchReports/FreezeRegister.aspx");
    }
}