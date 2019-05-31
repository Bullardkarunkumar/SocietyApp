using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;

public partial class Mudar_BranchComplaints : System.Web.UI.Page
{
    bool result = false;
    Buyer_BL BBL = new Buyer_BL();
    Invoice_BL IBL = new Invoice_BL();
    Order_BL objOrder = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnComplaints();
            divComplaintForm.Visible = false;
            BindBuyerComplaintDetails();
            txtAction.Enabled = false;
        }
    }
    protected void txtInvno_TextChanged(object sender, EventArgs e)
    {
        BindInvoiceProductsDetails();
    }
    private void BindInvoiceProductsDetails()
    {
        DataTable dtInvProducts = new DataTable();

        dtInvProducts = objOrder.BranchOrderDetails(txtInvno.Text);
        if (dtInvProducts.Rows.Count > 0)
        {
            txtInvDate.Text = dtInvProducts.Rows[0]["BranchOrderDate"].ToString();
            ddlProduct.DataSource = dtInvProducts;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
    }
    //protected void gvBranchComplaint(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "cmd_Select")
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument);

    //        BindBuyerComplaintDetails(Convert.ToInt32(gvBranchComplaint.DataKeys[index].Value.ToString()));

    //    }
    //}
    protected void gvBranchComplaint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            BindBuyerComplaintDetails(Convert.ToInt32(gvBranchComplaint.DataKeys[index].Value.ToString()));

        }
    }
    private void BindBuyerComplaintDetails(int ComplaintID)
    {
        DataTable dtBuyerComplaint = BBL.GetBuyerComplaintDetails(ComplaintID);
        if (dtBuyerComplaint.Rows.Count > 0)
        {
            divComplaintForm.Visible = true;
            divgvCompliant.Visible = false;
            DataRow dr = dtBuyerComplaint.Rows[0];
            txtCompBy.Text = dr["ComplaintBy"].ToString();
            txtInvno.Text = dr["InvoiceID"].ToString();
            txtInvDate.Text = dr["InvoiceDate"].ToString();
            //if(dr["InvoiceProductID"].ToString() == 
            //ddlProduct.ClearSelection();
            BindInvoiceProductsDetails();
            ddlProduct.ClearSelection();
            ddlProduct.Items.FindByValue(dr["InvoiceProductID"].ToString()).Selected = true;
            //ddlProduct.Text = dr["ProductName"].ToString();
            txtBatch.Text = dr["BatchNo"].ToString();
            txtQty.Text = dr["ProductQuantity"].ToString();
            txtComplaintDesc.Text = dr["Complaint"].ToString();
        }
        else
            divComplaintForm.Visible = false;
    }
    private void BindBuyerComplaintDetails()
    {
        divgvCompliant.Visible = true;
        btnComplaint.Visible = true;
        gvBranchComplaint.DataSource = BBL.GetBuyerComplaintDetails(Complaints.BRANCH, Session["BranchId"].ToString());
        gvBranchComplaint.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblBuyerID.Text = Session["BranchId"].ToString();
        result = BBL.BuyerComplaintInsertDetails(txtComplaintDesc.Text, txtCompBy.Text, lblBuyerID.Text, txtInvno.Text, Convert.ToInt32(ddlProduct.SelectedItem.Value), Convert.ToDateTime(txtInvDate.Text), Convert.ToDecimal(txtQty.Text), txtBatch.Text, txtAction.Text, "Bhanu", string.Empty, MudarApp.Insert, Complaints.BRANCH);
        BindBuyerComplaintDetails();
    }
    protected void btnComplaint_Click(object sender, EventArgs e)
    {
        divComplaintForm.Visible = true;
        divgvCompliant.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divComplaintForm.Visible = false;
        divgvCompliant.Visible = true;
    }
}
