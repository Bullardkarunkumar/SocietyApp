using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddNewFarmer_Click(object sender, EventArgs e)
    {

    }
    protected void lnklblLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
        //Response.Write("<script>alert('Do you want to Logout');</script>");
        //if(true)
        //{
        //    Response.Write("<script>alert('Category Saved Successfully');</script>");
        //}
        //else
        //{
        //    Response.Write("<script>alert('Save failed');</script>");

        //}
    }
   
}
