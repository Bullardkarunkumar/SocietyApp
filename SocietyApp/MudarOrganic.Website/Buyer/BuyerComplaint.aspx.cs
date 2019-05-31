using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Buyer_BuyerComplaint : System.Web.UI.Page
{
    bool result = false;
    Buyer_BL BBL = new Buyer_BL();
    Invoice_BL IBL = new Invoice_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MasterControlbtnComplaintsBuyer();
            divComplaintForm.Visible = false;
            divgvAdminCompliant.Visible = false;
            BindBuyerComplaintDetails();
            txtAction.Enabled = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblBuyerID.Text = Session["BuyerId"].ToString();
        result = BBL.BuyerComplaintInsertDetails(txtComplaintDesc.Text,txtCompBy.Text, lblBuyerID.Text, txtInvno.Text,Convert.ToInt32(ddlProduct.SelectedItem.Value),Convert.ToDateTime(txtInvDate.Text),Convert.ToDecimal(txtQty.Text), txtBatch.Text, txtAction.Text, "Bhanu", string.Empty, MudarApp.Insert, Complaints.BUYER);
        BindBuyerComplaintDetails();
    }
    protected void btnInvestigationReport_Click(object sender, EventArgs e)
    {

    }
    private void BindInvoiceProductsDetails()
    {
        DataTable dtInvProducts = new DataTable();
        dtInvProducts = IBL.GetProductsBasedonInvoice(txtInvno.Text);
        if (dtInvProducts.Rows.Count > 0)
        {
            txtInvDate.Text = dtInvProducts.Rows[0]["InvDate"].ToString();
            ddlProduct.DataSource = IBL.GetProductsBasedonInvoice(txtInvno.Text);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
    }
    protected void txtInvno_TextChanged(object sender, EventArgs e)
    {
        BindInvoiceProductsDetails();
    }
    protected void gvBuyerComplaint_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            BindBuyerComplaintDetails(Convert.ToInt32(gvBuyerComplaint.DataKeys[index].Value.ToString()));
           
        }
    }
    private void BindBuyerComplaintDetails()
    {
        divgvCompliant.Visible = true;
        btnComplaint.Visible = true;
        gvBuyerComplaint.DataSource = BBL.GetBuyerComplaintDetails(Complaints.BUYER, Session["BuyerId"].ToString());
        gvBuyerComplaint.DataBind();
    }
    private void BindBuyerComplaintDetails(int ComplaintID)
    {
        DataTable dtBuyerComplaint =  BBL.GetBuyerComplaintDetails(ComplaintID);
        if (dtBuyerComplaint.Rows.Count > 0)
        {
            divComplaintForm.Visible = true;
            divgvCompliant.Visible = false;
            DataRow dr = dtBuyerComplaint.Rows[0];
            txtCompBy.Text = dr["ComplaintBy"].ToString();
            txtInvno.Text = dr["InvoiceId"].ToString();
            //txtInvDate.Text = dr["InvoiceDate"].ToString();
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
    protected void btnComplaint_Click(object sender, EventArgs e)
    {
        divComplaintForm.Visible = true;
        divgvCompliant.Visible = false;
    }
    protected void btnComplaintProof_Click(object sender, EventArgs e)
    {
        //string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        //string Pdf_path = string.Empty;
        //if (fuBLR.FileName.Length > 0)
        //{
        //    Pdf_path = WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BLR" + OrderID.ToString() + "_" + fuBLR.FileName;
        //    fuBLR.PostedFile.SaveAs(Server.MapPath(Pdf_path));
        //    reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.BLR);
        //    BindBranchOrderReport();
        //}
    }
}
