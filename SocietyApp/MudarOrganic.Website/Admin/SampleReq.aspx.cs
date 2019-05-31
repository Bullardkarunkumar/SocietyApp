using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Admin_SampleReq : System.Web.UI.Page
{
    Product_BL pr = new Product_BL();
    Order_BL or = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderSampleDetails();
        }
    }
    private void BindOrderSampleDetails()
    {
        //Session["SOrderDt_s"] = or.OrderSampleDetails(Session["BuyerId"].ToString());
        dlSampleRequestDetails.DataSource = or.OrderSampleDetails();
        dlSampleRequestDetails.DataBind();
        foreach (RepeaterItem dli in dlSampleRequestDetails.Items)
        {
            var lblSampleID = dli.FindControl("lblSampleID") as Label;
            int SampleID = Convert.ToInt32(lblSampleID.Text);
            //lblReceiveDate

            (dli.FindControl("gvOrderSampleProduct") as GridView).DataSource = or.OrderSampleProduct(SampleID);
            (dli.FindControl("gvOrderSampleProduct") as GridView).DataBind();
        }
    }
    protected void dlSampleRequestDetails_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        switch (command)
        {
            case "Exp_Col":
                {

                    RepeaterItem item = e.Item;
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
                    var lblSampleId = e.Item.FindControl("lblSampleID") as Label;
                    int sampleID = Convert.ToInt32(lblSampleId.Text);
                    bool result = true;
                    result = or.OrderSampleDetails(sampleID, Session["BuyerId"].ToString(), DateTime.Today, BranchOrderStatus.New,
                        string.Empty, DateTime.Today, (e.Item.FindControl("txtReceivedDate") as TextBox).Text, string.Empty,
                        "bhanu", "Aslam", ref sampleID, 4);
                    if (result)
                        BindOrderSampleDetails();
                }
                break;
        }

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataRow[] drs = ((DataTable)Session["SOrderDt_s"]).Select(" OrderStatus = '" + ddlStatus.SelectedValue + "'");
            DataView dv = new DataView(((DataTable)Session["OrderDt_s"]));
            dv.RowFilter = " OrderStatus = '" + ddlStatus.SelectedValue + "'";
            dlSampleRequestDetails.DataSource = dv;
            dlSampleRequestDetails.DataBind();
            foreach (RepeaterItem dli in dlSampleRequestDetails.Items)
            {
                var lblSampleID = dli.FindControl("lblSampleID") as Label;
                int SampleID = Convert.ToInt32(lblSampleID.Text);
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