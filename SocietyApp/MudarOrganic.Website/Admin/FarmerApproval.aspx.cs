using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;

public partial class Admin_FarmerApproval : System.Web.UI.Page
{
    Farmer_BL farmerObj = new Farmer_BL();
    public static string code = string.Empty;
    public static string SortExpression_fa = "FirstName";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnFarmerApproval();
            BindFarmerList();
        }
    }
    private void BindFarmerList()
    {
        DataTable dt = farmerObj.ApprovedFamer_Inspection(true);
        Session["FarmerApproval"] = null;
        Session["FarmerApproval"] = dt;
        gvFarmer.DataSource = (DataTable)Session["FarmerApproval"];
        gvFarmer.DataBind();
        SortingFarmerApproval(SortExpression_fa); 
        
    }
    protected void gvFarmer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "FarmerCode" && e.CommandArgument.ToString() != "FirstName" && e.CommandArgument.ToString() != "City_Village" && e.CommandArgument.ToString() != "TotalAreaInHectares" && e.CommandArgument.ToString() != "Organic")
            index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "Farmer":
                {
                    code = gvFarmer.DataKeys[index].Values[1].ToString();
                    Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=2&FarmerCode=" + code + "&BackUrl=~/Admin/FarmerApproval.aspx");
                }
                break;
            case "FarmerCode":
                {
                    code = gvFarmer.DataKeys[index].Values[1].ToString(); //gvFarmer.Rows[index].Cells[0].Text;
                    Response.Redirect("~/Farmer/FarmerView.aspx?FarmerCode=" + code + "&BackUrl=~/Admin/FarmerApproval.aspx");
                }
                break;
        }
    }
    protected void btnApproveFarmer_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvFarmer.Rows)
        {
            bool result = false;
            string farmerID = gvFarmer.DataKeys[gvr.RowIndex].Value.ToString();
            if ((gvr.Cells[0].FindControl("cbBApproval") as CheckBox).Checked)
            {
                result = farmerObj.FarmerApproval(farmerID, string.Empty, string.Empty, "Aslam", 1, ref result);                
            }
        }
        BindFarmerList();
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingFarmerApproval(string SortExpression)
    {
        DataTable dt = (DataTable)Session["FarmerApproval"];
        Session["FarmerApproval"] = dt;
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        else
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + sortingDirection;
        gvFarmer.DataSource = sortedView;
        gvFarmer.DataBind();
    }
    protected void gvFarmer_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_fa = e.SortExpression.ToString();
        SortingFarmerApproval(SortExpression_fa);
    }
}
