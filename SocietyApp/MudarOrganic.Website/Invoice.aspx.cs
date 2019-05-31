using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;
using System.Text;
using HtmlAgilityPack;

public partial class Invoice : System.Web.UI.Page
{
    BranchsRolesEmployees_BL branchObj = new BranchsRolesEmployees_BL();
    Buyer_BL buyerObj = new Buyer_BL();
    Reports_BL reportObj = new Reports_BL();
    Order_BL orderObj = new Order_BL();
    MudarApp mudarObj = new MudarApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindBranchAddress();
            BindOrderBuyerBranchDetails();
            BindgvInvoiceDetails();
            
        }

    }
    private void BindBranchAddress()
    {
        DataTable dtBranch = branchObj.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            Session["branchID"] = dtBranch.Rows[0]["branchID"].ToString();
            lblExporterAddress.Text = dtBranch.Rows[0]["Address"].ToString();
            lblExporterAddress.Text += "<br/>" + dtBranch.Rows[0]["City"].ToString();
            lblExporterAddress.Text += "<br/>" + dtBranch.Rows[0]["Taluk"].ToString();
            lblExporterAddress.Text += "<br/>" + dtBranch.Rows[0]["District"].ToString();
            lblExporterAddress.Text += "<br/>" + dtBranch.Rows[0]["State"].ToString();
            lblExporterAddress.Text += "<br/>" + dtBranch.Rows[0]["Country"].ToString();
        }
    }
    private void BindOrderBuyerBranchDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtOBBD = orderObj.Order_Buyer_Branch_Details(Convert.ToInt32(OrderID));
        if (dtOBBD.Rows.Count > 0)
        {
            //Buyer Addres
            lblBuyerAddress.Text = dtOBBD.Rows[0]["BuyerCompanyName"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CAddress"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CCity"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CState"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CCountry"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CContactPhoneNo"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtOBBD.Rows[0]["CContactPerson"].ToString();
            //Notify address bind
            lblNotifyAddress.Text = dtOBBD.Rows[0]["BuyerCompanyName"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NAddress"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NCity"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NState"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NCountry"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NContactPhoneNo"].ToString();
            lblNotifyAddress.Text += "<br/>" + dtOBBD.Rows[0]["NContactPerson"].ToString();
            //Consignee address bind
            lblConsigneeAddress.Text = dtOBBD.Rows[0]["BuyerCompanyName"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CAddress"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CCity"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CState"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CCountry"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CContactPhoneNo"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtOBBD.Rows[0]["CContactPerson"].ToString();

        }
        
    }
    private void BindgvInvoiceDetails()
    {
        decimal tPrice = 0, tDrums = 0;
        //DataTable dtOrder = new DataTable();
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtOBBD = orderObj.Order_Buyer_Branch_Details(Convert.ToInt32(OrderID));
        if (dtOBBD.Rows.Count > 0)
        {

            foreach (DataRow dr in dtOBBD.Rows)
            {
                tPrice += Convert.ToDecimal(dr["TotalPrice"].ToString());
                tDrums += Convert.ToDecimal(dr["Packing25"].ToString()) + Convert.ToDecimal(dr["Packing180"].ToString());
            }
        }
        lblTotalDrums.Text = tDrums.ToString();
        lblTotalAmount.Text = tPrice.ToString();
        gvInvoiceOrder.DataSource = dtOBBD;
        gvInvoiceOrder.DataBind();
    }
}
