using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;

public partial class Admin_TracktheLot : System.Web.UI.Page
{
    Reports_BL reportObj = new Reports_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnTracktheLot();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvTrack.DataBind();
        gvTrack.DataSource = reportObj.Trach_Lot(Convert.ToInt16(ddlSearchBy.SelectedValue), txtSearch.Text);
        gvTrack.DataBind();
    }
    protected void gvTrack_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cmd = e.CommandName;
        
        if (cmd == "FarmerCode")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string farmerid = gvTrack.DataKeys[index].Value.ToString();
            string productid = (gvTrack.Rows[index].Cells[0].FindControl("hfProductID") as HiddenField).Value;

            gvInvoiceList.DataSource = reportObj.GetInvoiceList_Farmer(farmerid, productid);
            gvInvoiceList.DataBind();
        }
    }
}
