using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;


public partial class Buyer_SampleRequest : System.Web.UI.Page
{
    Product_BL pr = new Product_BL();
    Order_BL or = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.MasterControlbtnSampleRequest();
            BindOrderSampleDetails();
        }
    }
    private void BindOrderSampleDetails()
    {
        Session["SOrderDt_s"] = or.OrderSampleDetails(Session["BuyerId"].ToString());
        dlSampleRequestDetails.DataSource = (DataTable)Session["SOrderDt_s"];
        dlSampleRequestDetails.DataBind();
        foreach (RepeaterItem dli in dlSampleRequestDetails.Items)
        {
            int SampleID = Convert.ToInt32(((Label)dli.FindControl("lblSampleID")).Text);
            //lblReceiveDate

            (dli.FindControl("gvOrderSampleProduct") as GridView).DataSource = or.OrderSampleProduct(SampleID);
            (dli.FindControl("gvOrderSampleProduct") as GridView).DataBind();
        }
    }
    protected void dlSampleRequestDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        var item = e.Item;
        switch (command)
        {
            case "Exp_Col":
                {
                    //Label test = item.FindControl("lblC_P") as Label;
                    ImageButton img = item.FindControl("ibtnNOExpColap") as ImageButton;
                    if (img.ImageUrl == "~/images/expand.JPG")
                    {
                        ((ImageButton)dlSampleRequestDetails.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                        ((GridView)dlSampleRequestDetails.Items[Index].FindControl("gvOrderSampleProduct")).Visible = true;

                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)dlSampleRequestDetails.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)dlSampleRequestDetails.Items[Index].FindControl("gvOrderSampleProduct")).Visible = false;

                    }
                }
                break;
            case "RUpdate":
                {
                    int sampleID = Convert.ToInt32(((Label)item.FindControl("lblSampleID")).Text);
                    bool result = true;
                    result = or.OrderSampleDetails(sampleID, Session["BuyerId"].ToString(), DateTime.Today, BranchOrderStatus.New, string.Empty, DateTime.Today, (item.FindControl("txtReceivedDate") as TextBox).Text, string.Empty, "bhanu", "Aslam", ref sampleID, 4);
                    if (result)
                        BindOrderSampleDetails();
                }
                break;
        }

    }
    protected void dlSampleRequestDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (string.IsNullOrEmpty(((Label)e.Item.FindControl("lblReceiveDate")).Text))
            {
                ((Panel)e.Item.FindControl("pReceive")).Visible = true;
                ((TextBox)e.Item.FindControl("txtReceivedDate")).Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                ((Panel)e.Item.FindControl("pReceive")).Visible = false;
            }
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataRow[] drs = ((DataTable)Session["SOrderDt_s"]).Select("Status = '" + ddlStatus.SelectedValue + "'");
            DataView dv = new DataView(((DataTable)Session["OrderDt_s"]));
            dv.RowFilter = " OrderStatus = '" + ddlStatus.SelectedValue + "'";
            dlSampleRequestDetails.DataSource = dv;
            dlSampleRequestDetails.DataBind();
            foreach (RepeaterItem dli in dlSampleRequestDetails.Items)
            {
                int SampleID = Convert.ToInt32(((Label)dli.FindControl("lblSampleID")).Text);
                (dli.FindControl("gvOrderSampleProduct") as GridView).DataSource = or.OrderSampleProduct(SampleID);
                (dli.FindControl("gvOrderSampleProduct") as GridView).DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error:" + ex.Message);
        }
    }
}
