using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;


public partial class Buyer_NewSampleRequest : System.Web.UI.Page
{
    Product_BL pr = new Product_BL();
    Order_BL or = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           ProBindDetails();
        }
        if (cbCourier.Checked == false)
        {
            txtCourierName.Text = string.Empty;
            txtCourierAcNo.Text = string.Empty;
        }
        txtCourierName.Enabled = false;
        txtCourierAcNo.Enabled = false;
    }
   
    private void ProBindDetails()
    {
        gvProduct.DataSource = pr.GetProductDetails();
        gvProduct.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool result = true;
        int SampleID = 0;
        result = or.OrderSampleDetails(SampleID, Session["BuyerId"].ToString(), DateTime.Today, BranchOrderStatus.New, txtCourierName.Text, DateTime.Today, string.Empty, txtCourierAcNo.Text, "bhanu", string.Empty, ref SampleID, MudarApp.Insert);
        if (result)
        {
            foreach (GridViewRow gvr in gvProduct.Rows)
            {
                int SampleProductID = 0;
                Session["SampleID"] = SampleID;
                string str = (gvr.Cells[0].FindControl("cbDoc") as CheckBox).Checked.ToString();
                if((gvr.Cells[0].FindControl("cbDoc") as CheckBox).Checked)
                {
                    result = or.OrderSampleProductDetails(SampleProductID, Convert.ToInt32(gvProduct.DataKeys[gvr.RowIndex].Value.ToString()), SampleID, Convert.ToInt32((gvr.Cells[0].FindControl("txtQuantity") as TextBox).Text), "bhanu", string.Empty, MudarApp.Insert);
                }
            }
        }
        Response.Redirect("~/Buyer/SampleRequest.aspx");
    }
}
