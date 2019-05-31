using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Buyer_BuyerInfo : System.Web.UI.Page
{
    Buyer_BL objBuyer = new Buyer_BL();
    public static string SortExpression_bi = "BuyerCompanyName";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() != LoginType.SuperAdmin.ToLower())
        {
            Response.Redirect("~/NoAccess.aspx");
        }
        if (!Page.IsPostBack)
        {
            ViewState["dirState"] = "";
            BindBuyerDetails();
        }
    }
    private void BindBuyerDetails()
    {
        DataTable dtBuyer = objBuyer.BuyerDetails(true);
        Session["BuyerInfo"] = dtBuyer;
        gvBuyerDetails.DataSource = (DataTable)Session["BuyerInfo"];
        gvBuyerDetails.DataBind();
        SortingBuyerInfo(SortExpression_bi);
    }
    protected void gvBuyerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "BuyerCompanyName" && e.CommandArgument.ToString() != "email" && e.CommandArgument.ToString() != "MobileforTextingpurpose" && e.CommandArgument.ToString() != "CState" && e.CommandArgument.ToString() != "CCountry")
            index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "buyer":
                {
                    HttpContext.Current.Session["BuyerId"] = gvBuyerDetails.DataKeys[index].Value;
                    Response.Redirect("~/Buyer/BuyerDoc.aspx?BackUrl=~/Buyer/BuyerInfo.aspx");
                }
                break;
            case "Edit":
                {
                    Session["BuyerId"] = gvBuyerDetails.DataKeys[index].Value;
                    Response.Redirect("~/Buyer/BuyerUpdate.aspx");
                }
                break;
        }
    }
    public string dir
    {
        get
        {
            if (ViewState["dirState"].ToString() == "desc")
            {
                ViewState["dirState"] = "asc";
            }
            else
            {
                ViewState["dirState"] = "desc";
            }
            return ViewState["dirState"].ToString();
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingBuyerInfo(string SortExpression)
    {
        DataTable dt = (DataTable)Session["BuyerInfo"];
        Session["BuyerInfo"] = dt;
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvBuyerDetails.DataSource = sortedView;
        gvBuyerDetails.DataBind();
    }
    protected void gvBuyerDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_bi = e.SortExpression.ToString();
        SortingBuyerInfo(SortExpression_bi);
    }
}
