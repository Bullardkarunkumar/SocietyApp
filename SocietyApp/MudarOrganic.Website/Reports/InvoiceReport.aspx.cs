﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Web.Configuration;

public partial class Admin_Default : System.Web.UI.Page
{

    BranchsRolesEmployees_BL branchObj = new BranchsRolesEmployees_BL();
    Buyer_BL buyerObj = new Buyer_BL();
    Reports_BL reportObj = new Reports_BL();
    Order_BL orderObj = new Order_BL();
    MudarApp mudarObj = new MudarApp();
    MudarUser mu = new MudarUser();
    Invoice_BL invoiceObj = new Invoice_BL();
    Reports_Type rtypeObj = new Reports_Type();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BuyerDetails();
            BindPODetails();
            BindMainBranchDetails();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        bool result = false;
        int i= Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true));
        int j = Convert.ToInt32(lblbranchorderID.Text);
        result = invoiceObj.InvoiceInsert(i,j, lblPriceTerms.Text, lblTransport.Text, "India", string.Empty, lblPayment.Text, lblFreight.Text, lblDCountry.Text, lblDPort.Text, Convert.ToDateTime(lblPODate.Text), "aslam", "aslam", MudarApp.Insert, lblInvoice.Text);
        if (result)
        {
            foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            {
                string test = gvPurchaseOrder.DataKeys[gvr.DataItemIndex].Value.ToString();
                result = invoiceObj.InvoiceProductDetailsINSandUPDandDEL(lblInvoice.Text, Convert.ToInt32(gvPurchaseOrder.DataKeys[gvr.DataItemIndex].Value.ToString()),Convert.ToDecimal((gvr.FindControl("lblQty") as Label).Text),Convert.ToDecimal((gvr.FindControl("lblGQty") as Label).Text),Convert.ToDecimal(gvr.Cells[3].Text),0,Convert.ToDecimal(gvr.Cells[4].Text), "aslam", string.Empty, MudarApp.Insert);
            }
            generatePDFforPO();
        }
        Response.Redirect("~/Mudar/UpdateAdminOrder.aspx");
    }
    private void BuyerDetails()
    {
        string BuAddress = string.Empty;
        string BcAdress = string.Empty;
        string BnAddress = string.Empty;
        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            if (dtBuyer.Rows[0]["CCountry"].ToString() == "INDIA")
                lblmoney.Text = "INR";
            else
                lblmoney.Text = "USD";
            lblBuyerAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            lblBuyerAddress.Text += "<br/>" + Address[0].ToString();
            if (Address.Length > 1)
                lblBuyerAddress.Text += "<br/>" + Address[1].ToString() + "," + Address[2].ToString();
            lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCity"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CState"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCountry"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CContactPerson"].ToString();
            lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
            lblNotifyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] nAddress = dtBuyer.Rows[0]["NAddress"].ToString().Split('@');
            lblNotifyAddress.Text += "<br/>" + nAddress[0].ToString();
            if (nAddress.Length > 1)
                lblNotifyAddress.Text += "<br/>" + nAddress[1].ToString() + "," + nAddress[2].ToString();
            lblNotifyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NCity"].ToString();
            lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NState"].ToString();
            lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NCountry"].ToString();
            lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NContactPerson"].ToString();
            lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NContactPhoneNo"].ToString();
            lblConsigneeAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] cAddress = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            lblNotifyAddress.Text += "<br/>" + cAddress[0].ToString();
            if (cAddress.Length > 1)
                lblNotifyAddress.Text += "<br/>" + cAddress[1].ToString() + "," + cAddress[2].ToString();
            lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CCity"].ToString();
            lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CState"].ToString();
            lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CCountry"].ToString();
            lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CContactPerson"].ToString();
            lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
        }
    }
    private void BindPODetails()
    {
        DataTable dtPO = orderObj.OrderList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtPO.Rows.Count > 0)
        {
            lblPO.Text = dtPO.Rows[0]["PurchaseOrderID"].ToString();
            lblPODate.Text = string.Format("{0: dd MMM yyyy}", dtPO.Rows[0]["OrderDate"]);
            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
            decimal Total_Amount = 0, Total_Durm = 0;
            lblbranchorderID.Text = dtPO.Rows[0]["BranchOrderId"].ToString();
            
            if (dtPOProductList.Rows.Count > 0)
            {
                for (int count = 0; count < dtPOProductList.Rows.Count; count++)
                {
                    Total_Amount += Convert.ToDecimal(dtPOProductList.Rows[count]["TotalPrice"].ToString());
                    Total_Durm += Convert.ToDecimal(dtPOProductList.Rows[count]["Packing25"].ToString()) + Convert.ToDecimal(dtPOProductList.Rows[count]["Packing180"].ToString());
                    if(Convert.ToInt32(dtPOProductList.Rows[count]["25dfrom"]) != 0)
                    {
                        dtPOProductList.Rows[count]["DrumF"] = dtPOProductList.Rows[count]["25dfrom"].ToString();
                        dtPOProductList.Rows[count]["DrumT"] = dtPOProductList.Rows[count]["25dto"].ToString(); 
                    }
                    if (Convert.ToInt32(dtPOProductList.Rows[count]["180dfrom"]) != 0)
                    {
                        dtPOProductList.Rows[count]["DrumF"] = dtPOProductList.Rows[count]["180dfrom"].ToString();
                        dtPOProductList.Rows[count]["DrumT"] = dtPOProductList.Rows[count]["180dto"].ToString();
                    }
                }
                lblTotalAmout.Text = string.Format("{0:n0}", Total_Amount);
                lblTotalDrum.Text = Total_Durm.ToString();
                DataTable test = orderObj.Convert_NumberToWord(Total_Amount);
                lblAmount_word.Text = test.Rows[0][0].ToString() + " Only";
                gvPurchaseOrder.DataSource = dtPOProductList;
                gvPurchaseOrder.DataBind();
            }
            lblPriceTerms.Text = dtPO.Rows[0]["FreightTerms"].ToString();
            lblTransport.Text = dtPO.Rows[0]["Transport"].ToString();
            lblPayment.Text = dtPO.Rows[0]["PaymentTerms"].ToString();
            if (dtPO.Rows[0]["FreightTerms"].ToString() == "FOB INDIA")
                lblFreight.Text = "Collect";
            else
                lblFreight.Text = "Pre-Paid";
            lblDCountry.Text = dtPO.Rows[0]["DestinationCountry"].ToString();
            lblDPort.Text = dtPO.Rows[0]["DestinationPort"].ToString();
            lblLport.Text = "";
        }
    }
    private void BindMainBranchDetails()
    {
        DataTable dtBranch = branchObj.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            string BAddress = string.Empty;
            lblMudarAddress.Text = dtBranch.Rows[0]["Bname"].ToString();
            BAddress = dtBranch.Rows[0]["Address"].ToString();
            lblMudarAddress.Text += "<br/>" + BAddress;
            lblMudarAddress.Text += "<br/>" + dtBranch.Rows[0]["City"].ToString();
            lblMudarAddress.Text += "<br/>" + dtBranch.Rows[0]["State"].ToString();
            lblMudarAddress.Text += "<br/>" + dtBranch.Rows[0]["Country"].ToString();
            lblContactPerson.Text = dtBranch.Rows[0]["ContactPerson"].ToString();
            lblPhone.Text = dtBranch.Rows[0]["Phone_Fax"].ToString();
            lblMobile.Text = dtBranch.Rows[0]["Mobile"].ToString();
            lblIECode.Text = dtBranch.Rows[0]["IECode"].ToString();
            lblEmail.Text = dtBranch.Rows[0]["Email"].ToString();
            lblFDA.Text = dtBranch.Rows[0]["FDA"].ToString();
            lblWebsite.Text = dtBranch.Rows[0]["Website"].ToString();
            lblAPVAT.Text = dtBranch.Rows[0]["AP_VAT"].ToString();
            lblBank.Text = dtBranch.Rows[0]["BankName"].ToString();
            lblBankAccount.Text = dtBranch.Rows[0]["BankAcct_no"].ToString();
            lblBankADC.Text = dtBranch.Rows[0]["Bank_ADC_Code"].ToString();

            lblInvoiceDate.Text = string.Format("{0: dd MMM yyyy}", DateTime.Now);
            string year = DateTime.Now.Year.ToString();
            string[] yy = year.Split('2');
            string[] yy2 = yy[0].Split('0');
            lblInvoice.Text = mudarObj.GenerateInvoiceID();
        }
    }
    //private void BindGvPurchaseOrder()
    //{
    //    decimal tPrice = 0, tDrums = 0;
    //    DataTable dtOrder = new DataTable();
    //    if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
    //    {
    //        dtOrder = (Session["sDtOrder"] as DataTable);
    //        foreach (DataRow dr in dtOrder.Rows)
    //        {
    //            tPrice += Convert.ToDecimal(dr["TotalPrices"].ToString());
    //            tDrums += Convert.ToDecimal(dr["PackingDetails25"].ToString()) + Convert.ToDecimal(dr["PackingDetails180"].ToString());
    //        }
    //    }

    //}
    private void generatePDFforPO()
    {
        string imageFilePath1 = Server.MapPath("~/images/USDAOrganic.jpg");
        string imageFilePath2 = Server.MapPath("~/images/IndianOrganic.jpg");
        string ss = "<img src='" + imageFilePath1 + "' height='100%' width='100%'/>";
        string ss1 = "<img src='" + imageFilePath2 + "'height='100%' width='100%'/>";
        string strpdf = "<table><tr><td align='left'>" + ss + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ss1 + "</td></tr></table>";
        strpdf += "<table align='center' style='font-family:Verdana;font-size:8px;width:885px;'>";
        strpdf += "<tr><td colspan='6'><table align='center' width='100%' border='1'>";
        strpdf += "<tr><td bgcolor='#CCCC99'align='center' style='font-family:Verdana;font-size:x-large;vertical-align:middle;height:60px;'>INVOICE</td></tr>";
        strpdf += "<tr><td align='center'>CUCERT-025367</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'>";
        strpdf += "<tr><td width='40%' align='center' bgcolor='#CCCC99'>EXPORT</td><td width='60%' align='center' colspan='4' bgcolor='#CCCC99'>EXPORTER  INFO</td></tr>";
        strpdf += "<tr><td align='left' rowspan='5'>" + lblMudarAddress.Text + "</td><td align='center' bgcolor='#CCCC99' width='13%'>Contact</td><td align='center' >" + lblContactPerson.Text + "</td><td align='center'width='13%' bgcolor='#CCCC99' >Invoice No</td><td align='center' >" + lblInvoice.Text + "</td></tr>";
        strpdf += "<tr><td align='left' width='13%' bgcolor='#CCCC99' align='center'>Phone/Fax</td><td align='center' >" + lblPhone.Text + "</td><td align='center' width='13%' bgcolor='#CCCC99' >Date</td><td align='center'  >" + lblInvoiceDate.Text + "</td></tr>";
        strpdf += "<tr><td align='left' width='13%' bgcolor='#CCCC99' align='center'>Mobile</td><td align='center' >" + lblMobile.Text + "</td><td align='center' width='13%' bgcolor='#CCCC99'>IE Code</td><td align='center' >" + lblIECode.Text + "</td></tr>";
        strpdf += "<tr><td align='left' width='13%' bgcolor='#CCCC99' align='center'>E-Mail</td><td align='center' >" + lblEmail.Text + "</td><td align='center' width='13%' bgcolor='#CCCC99' >FDA</td><td align='center' >" + lblFDA.Text + "</td></tr>";
        strpdf += "<tr><td align='left' width='13%' bgcolor='#CCCC99' align='center'>Website</td><td align='center' >" + lblWebsite.Text + "</td><td align='center' width='13%' bgcolor='#CCCC99' >AP VAT</td><td align='center' >" + lblAPVAT.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr><td align='center' bgcolor='#CCCC99'>Buyer</td><td align='center' bgcolor='#CCCC99'>Notify </td><td align='center' bgcolor='#CCCC99'>Consignee </td></tr>";
        strpdf += "<tr align='left'><td>" + lblBuyerAddress.Text + "</td><td>" + lblNotifyAddress.Text + "</td><td>" + lblConsigneeAddress.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'>";
        strpdf += "<tr align='center'><td  bgcolor='#CCCC99'>PO Number</td><td>" + lblPO.Text + "</td><td bgcolor='#CCCC99'>PO Date</td><td>" + lblPODate.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr align='center'><td colspan='6'><table widht='100%' border='1'>";
        //strpdf += "<tr align='center' bgcolor='#CCCC99'><td>Batch No</td><td>Product Name</td><td>Quantity</td><td>GrossQuantity</td><td>Amount (USD)</td></tr>";
        strpdf += "<tr align='center' bgcolor='#CCCC99'><td>Marking</td><td>Product Name</td><td><table><tr><td colspan='2'>Qty(KG)</td></tr><tr><td>Net Wt</td><td>Gross Wt</td></tr></table></td><td>Rate/KG<br/>(" + lblmoney.Text + ")</td><td>Amount<br/>(" + lblmoney.Text + ")</td></tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            strpdf += "<tr align='center'><td><table><tr><td colspan='2'>Lot :" + (gvr.Cells[0].FindControl("lblBlendID") as Label).Text + "</td></tr><tr><td>" + (gvr.Cells[0].FindControl("lblDrumF") as Label).Text + "</td><td>" + (gvr.Cells[0].FindControl("lblDrumT") as Label).Text + "</td></tr></table></td><td>" + gvr.Cells[1].Text + "</td><td><table><tr><td>" + (gvr.Cells[0].FindControl("lblQty") as Label).Text + "</td><td>" + (gvr.Cells[0].FindControl("lblGQty") as Label).Text + "</td></tr></table></td><td>" + gvr.Cells[3].Text + "</td><td>" + gvr.Cells[4].Text + "</td></tr>";
              //strpdf += "<tr align='center'><td colspan='2'>" + (gvr.Cells[0].FindControl("lblBlendID") as Label).Text + "</td></tr><tr><td>" + (gvr.Cells[0].FindControl("lblDrumF") as Label).Text + "</td><td>" + (gvr.Cells[0].FindControl("lblDrumT") as Label).Text + "</td></tr></table></td><td><table><tr><td>" + (gvr.Cells[0].FindControl("lblQty") as Label).Text + "</td><td>" + (gvr.Cells[0].FindControl("lblGQty") as Label).Text + "</td></tr></table></td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td><td>" + gvr.Cells[4].Text + "</td></tr>";
        strpdf += "<tr align='center' bgcolor='#CCCC99'><td colspan='2'>Total Drums</td><td></td><td>'"+lblTotalDrum.Text+"'</td><td></td><td>'" + lblTotalAmout.Text + "'</td></tr>";
        strpdf += "<tr align='center' bgcolor='#CCCC99'><td colspan='1'>Total Drums</td><td colspan='5'>'" + lblTotalAmout.Text + "'</td></tr>";
        strpdf += "</table></td></tr><tr>";
        strpdf += "</td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr align='center'><td colspan='6'><table width='100%' border='1'><tr><td bgcolor='#CCCC99'>Price Terms</td><td>" + lblPriceTerms.Text + "</td><td bgcolor='#CCCC99'>Payment Terms</td><td>" + lblPayment.Text + "</td></tr>";
        strpdf += "<tr><td bgcolor='#CCCC99'>Transport </td><td>" + lblTransport.Text + "</td><td bgcolor='#CCCC99'>Freight Terms</td><td>" + lblFreight.Text + "</td></tr>";
        strpdf += "<tr><td bgcolor='#CCCC99'>Origin Country</td><td>India</td><td bgcolor='#CCCC99'>Destination Country</td><td>" + lblDCountry.Text + "</td></tr>";
        strpdf += "<tr><td bgcolor='#CCCC99'>Loading Port</td><td>MUMBAI</td><td bgcolor='#CCCC99'>Destination Port</td><td>" + lblDPort.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr><td width='40%' align='center' bgcolor='#CCCC99'> Bank Info</td><td width='30%' rowspan='6'>Declaration:&nbsp; We declare that this Invoice shows the actual price of the goods described and that all particulars are true and correct.</td><td width='30%' rowspan='6' align='right'>Authorized Signature<br/>Mudar India Exports</td></tr>";
        strpdf += "<tr align='center'><td width='35%'>" + lblBank.Text + "</td></tr>";
        strpdf += "<tr align='center'><td width='35%' bgcolor='#CCCC99'>A/C Number</td></tr>";
        strpdf += "<tr align='center'><td width='35%'>" + lblBankAccount.Text + "</td></tr>";
        strpdf += "<tr align='center'><td width='35%' bgcolor='#CCCC99'>ADC Code</td></tr>";
        strpdf += "<tr align='center'><td width='35%'>" + lblBankADC.Text + "</td></tr></table>";
        strpdf += "</td></tr></table>";

        //old code
        //string strpdf = "<table align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td align='center' style='font-size: 12px;'>Mudar India Exports</td></tr><tr>";
        //strpdf += "<td align='center' style='font-size: 9px'>6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table>";
        //strpdf += "<table border='1' align='center' style='font-family: Verdana; font-size: 12px; width: 885px'><tr>";
        //strpdf += "<td colspan='6' align='center' style='font-family: Verdana;'>Invoice</td></tr><tr>";
        //strpdf += "<td colspan='6' align='center' style='font-family: Verdana;'>CUCERT - 025367</td></tr><tr>";
        //strpdf += "<td colspan='2' align='center' bgcolor='#ffcc66'> EXPORT </td>";
        //strpdf += "<td colspan='4' align='center' bgcolor='#ffcc66'> EXPORTER &nbsp; INFO </td></tr><tr>";
        //strpdf += "<td colspan='2' rowspan='5'>Mudar India Exports<br />" + lblMudarAddress.Text + "</td>";
        //strpdf += "<td>Contact</td><td>" + lblContactPerson.Text + "</td>";
        //strpdf += "<td>Invoice No</td><td>" + lblInvoice.Text + "</td></tr><tr>";
        //strpdf += "<td>Phone/Fax</td><td>" + lblPhone.Text + "</td>";
        //strpdf += "<td>Date</td><td>" + lblInvoiceDate.Text + "</td></tr><tr>";
        //strpdf += "<td>Mobile</td><td>" + lblMobile.Text + "</td>";
        //strpdf += "<td>IE Code</td><td>" + lblIECode.Text + "</td></tr><tr>";
        //strpdf += "<td>E-Mail</td><td>" + lblEmail.Text + "</td>";
        //strpdf += "<td>FDA</td><td>" + lblFDA.Text + "</td></tr><tr>";
        //strpdf += "<td>Website</td><td>" + lblWebsite.Text + "</td>";
        //strpdf += "<td>AP VAT</td><td>" + lblAPVAT.Text + "</td></tr></table>";
        //strpdf += "<table border='1' align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td width='33%' align='center' bgcolor='#ffcc66'>Buyer</td>";
        //strpdf += "<td width='33%' align='center' bgcolor='#ffcc66'>Notify</td>";
        //strpdf += "<td width='33%' align='center' bgcolor='#ffcc66'>Consignee</td></tr><tr align='center'>";
        //strpdf += "<td>" + lblBuyerAddress.Text + "</td>";
        //strpdf += "<td>" + lblNotifyAddress.Text + "</td>";
        //strpdf += "<td>" + lblConsigneeAddress.Text + "</td></tr></table>";
        //strpdf += "<table border='1' align='center' style='font-family: Verdana; width: 885px'><tr style=' border-left-color:White;'><td colspan='6' style='color:White; border-left-color:White;'>t &nbsp;</td></tr><tr>";
        //strpdf += "<td align='center'>PO no</td><td colspan='2'>" + lblPO.Text + "</td>";
        //strpdf += "<td colspan='2' align='center'>Date</td><td>" + lblPODate.Text + "</td></tr><tr>";

        //strpdf += "<td colspan='6'><table border='1'>";
        //grid start
        //strpdf += "<tr align='center'><td>Batch No.</td><td>Product Name</td><td>Quantity</td><td>GrossQuantity</td><td>Amount (USD)</td></tr>";
        //foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        //    strpdf += "<tr align='center'><td>" + gvr.Cells[0].Text + "</td><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td><td>" + gvr.Cells[4].Text + "</td></tr>";//<td>" + gvr.Cells[5].Text + "</td><td>" + gvr.Cells[6].Text + "</td>

        //grid end
        //strpdf += "</table></td></tr><tr>";
        //strpdf += "<td align='center'>Total No. of Drums</td><td colspan='2' align='center'>" + lblTotalDrum.Text + "</td>";
        //strpdf += "<td align='center'>Total Amount</td><td colspan='2' align='center'>" + lblTotalAmout.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='2' align='center'>Amount in Words</td><td colspan='4'>" + lblAmount_word.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='6' style='color:White;'>t &nbsp;</td></tr><tr>";
        //strpdf += "<td>Price Terms</td><td colspan='2'>" + lblPriceTerms.Text + "</td>";
        //strpdf += "<td colspan='2'>Payment Terms</td><td>" + lblPayment.Text + "</td></tr><tr>";
        //strpdf += "<td>Transport</td><td colspan='2'>" + lblTransport.Text + "</td>";
        //strpdf += "<td colspan='2'>Freight Terms</td><td>" + lblFreight.Text + "</td></tr><tr>";
        //strpdf += "<td>Origin Country</td><td colspan='2'>India</td>";
        //strpdf += "<td colspan='2'>Destination Country</td><td>" + lblDCountry.Text + "</td></tr><tr>";
        //strpdf += "<td>Loading Port</td><td colspan='2'>MUMBAI</td>";
        //strpdf += "<td colspan='2'>Destination Port</td><td>" + lblDPort.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='6'style='color:White;'>t &nbsp;</td></tr><tr>";
        //strpdf += "<td colspan='2' align='center' bgcolor='#ffcc66'>Bank Info</td>";
        //strpdf += "<td colspan='4' rowspan='6'><br /><br />Declaration: We declare that this Invoice shows the actual price of<br /> thegoods described and that all particulars are true and correct.</td></tr><tr>";
        //strpdf += "<td colspan='2'>" + lblBank.Text + "</td></tr><tr>";
        //strpdf += " <td colspan='2'>A/C. No:</td></tr><tr>";
        //strpdf += "<td colspan='2'>" + lblBankAccount.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='2'>ADC Code:</td></tr><tr>";
        //strpdf += "<td colspan='2'>" + lblBankADC.Text + "</td></tr></table>";



        ////report
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            string orderid = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + lblInvoice.Text + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + lblInvoice.Text + ".pdf";

            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();




            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();


            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            ////add the collection to the document
            //for (int k = 0; k < htmlarraylist.Count; k++)
            //{
            //    document.Add((IElement)htmlarraylist[k]);
            //}

            //document.Add(new Paragraph("And the same with indentation...."));

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();


            bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid),Convert.ToInt32(lblbranchorderID.Text) , Pdf_path, "Bhanu", string.Empty, rtypeObj.Invoice);


        }
        catch (Exception exx)
        {
            Response.Write("<br>____________________________________<br>");
            Response.Write("<br>Error: " + exx + "<br>");
            Response.Write("<br>StackTrace: " + exx.StackTrace + "<br>");
            Response.Write("<br>strPDFDocument: " + strpdf.ToString() + "<br>");
            Response.Write("<br>strSelectUserListBuilder: " + strpdf.ToString() + "<br>");

            //Console.Error.WriteLine(exx.StackTrace);
            //Console.Error.WriteLine(exx.StackTrace);
        }
        finally
        {
            //document.Close();
        }
    }
}
