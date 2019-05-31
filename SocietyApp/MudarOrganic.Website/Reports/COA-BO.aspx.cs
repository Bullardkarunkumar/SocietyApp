using System;
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

public partial class Reports_COA_BO : System.Web.UI.Page
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
            lblTodayDate.Text = string.Format("{0: dd MMM yyyy}", DateTime.Today);
            BindPOProductDetails();
            BuyerDetails();
            BindInvoiceDetails();
        }
    }
    private void BindPOProductDetails()
    {
        DataTable dtPO = orderObj.OrderList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtPO.Rows.Count > 0)
        {
            lblPO.Text = dtPO.Rows[0]["PurchaseOrderID"].ToString();
            lblPODate.Text = string.Format("{0: dd MMM yyyy}", dtPO.Rows[0]["OrderDate"]);
            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
            dlCOAdetails.DataSource = dtPOProductList;
            dlCOAdetails.DataBind();
            decimal Total_Durm = 0;
            if (dtPOProductList.Rows.Count > 0)
            {
                for (int count = 0; count < dtPOProductList.Rows.Count; count++)
                {
                    Total_Durm += Convert.ToDecimal(dtPOProductList.Rows[count]["Packing25"].ToString()) + Convert.ToDecimal(dtPOProductList.Rows[count]["Packing180"].ToString());
                }
            }
        }
    }
    private void BuyerDetails()
    {

        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            lblConsigneeAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            lblConsigneeAddress.Text += "<br/>" + Address[0].ToString();
            if (Address.Length > 1)
            {
                lblConsigneeAddress.Text += "<br/>" + Address[1].ToString() + "," + Address[2].ToString();
            }
            lblConsigneeAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCity"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtBuyer.Rows[0]["CState"].ToString();
            lblConsigneeAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCountry"].ToString();
            //lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CContactPerson"].ToString();
            //lblConsigneeAddress.Text += "<br>" + dtBuyer.Rows[0]["CContactPhoneNo"].ToString();

        }
    }
    private void BindInvoiceDetails()
    {
        DataTable invoiceDT = invoiceObj.InvoiceDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (invoiceDT.Rows.Count > 0)
        {
            lblInvoice.Text = invoiceDT.Rows[0]["InvoiceID"].ToString();
            lblInvoiceDate.Text = string.Format("{0: dd MMM yyyy}", Convert.ToDateTime(invoiceDT.Rows[0]["CreatedDate"].ToString()));
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        generatePdf();
        Response.Redirect("~/Mudar/UpdateOrder.aspx");
    }
    private bool generatePdf()
    {
        bool result = false;
        int orderid = Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true));
        DataTable dtPOProductList = orderObj.OrderProductList(orderid);
        string path = string.Empty;
        for (int count = 0; count < dtPOProductList.Rows.Count; count++)
        {
            string strpdf = string.Empty;
            strpdf += "<table align='center' style='font-family: Verdana; width: 885px'><tr bgcolor='#ffcc66'>";
            strpdf += "<td colspan='4'><table align='center' style='font-family: Verdana;'><tr><td align='center' style='font-size: 12px;'>Mudar India Exports</td></tr><tr>";
            strpdf += "<td colspan='4' align='center' style='font-size: 10px;'>6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
            strpdf += "<td colspan='4'></td></tr><tr>";
            strpdf += "<td colspan='2' align='left'><table width='40%'align='left' border='1'><tr><td><span style='font-size:10px;'>COA No</td><td><span style='font-size:10px;'>&nbsp;02657</td></tr></table></td><td colspan='2' align='center'><table width='55%'align='right' border='1'><tr><td>Date</td><td align='center'><span style='font-size:10px;'>" + lblTodayDate.Text + "</td></tr></table></td></tr><tr>";
            strpdf += "<td colspan='4'></td></tr><tr>";
            strpdf += "<td align='center' colspan='4'>Certificate of Analysis</td></tr><tr>";
            strpdf += "<td colspan='4'></td></tr><tr>";
            strpdf += "<td colspan='4'><table  width='100%' border='1'><tr align='center'>";
            strpdf += "<td rowspan='4'>" + lblConsigneeAddress.Text + "</td>";
            strpdf += "<td>Buyers PO</td><td>" + lblPO.Text + "</td></tr><tr align='center'>";
            strpdf += "<td>PO Date<br /></td><td>" + lblPODate.Text + "</td></tr><tr align='center'>";
            strpdf += "<td>Invoice No #</td><td>" + lblInvoice.Text + "</td></tr><tr align='center'>";
            strpdf += "<td>Invoice Date</td><td>" + lblInvoiceDate.Text + "</td></tr></table></td></tr><tr>";
            strpdf += "<td colspan='4'></td></tr>";
            strpdf += "<td colspan='4'><table width='100%' border='1'><tr align='center'>";
            strpdf += "<td colspan='2'>Name of the Product </td><td colspan='2'>" + dtPOProductList.Rows[count]["ProductName"].ToString() + "</td></tr><tr align='center'>";
            strpdf += "<td>Lot Qty in KG</td><td>" + dtPOProductList.Rows[count]["Quantity"].ToString() + "</td><td>Year of Production</td><td>" + (dlCOAdetails.Items[count].FindControl("txtYearProduction") as TextBox).Text + "</td></tr><tr align='center'>";
            strpdf += "<td>Lot No</td><td>" + dtPOProductList.Rows[count]["BatchID"].ToString() + "</td><td>Drums Ref</td><td>" + dtPOProductList.Rows[count]["TotalDrums"].ToString() + "</td></tr><tr></table></td></tr><tr align='center'>";
            strpdf += "<td colspan='4'></td></tr><tr>";
            strpdf += "<td colspan='4'><table width='100%' border='1'><tr>";
            strpdf += "<td rowspan='2' align='center'>Parameter</td><td rowspan='2'align='center'>Analysis Value </td><td colspan='2'align='center'>Standard Specification</td><td rowspan='2'align='center'>Testing Method<br />Adopted</td></tr><tr>";
            strpdf += "<td align='center'>Low</td><td align='center'>High</td></tr><tr align='center'>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtApperance") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtApperanceAnalysis") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtApperanceLow") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtApperanceHigh") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtApperanceTMA") as TextBox).Text + "</td></tr><tr align='center'>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtOdor") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtOdorAnalysis") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtOdorLow") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtOdorHigh") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtOdorTMA") as TextBox).Text + "</td></tr><tr align='center'>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtMethyleChevicol") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtMethyleChevicolAnalysis") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtMethyleChevicolLow") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtMethyleChevicolHigh") as TextBox).Text + "</td>";
            strpdf += "<td>" + (dlCOAdetails.Items[count].FindControl("txtMethyleChevicolTMA") as TextBox).Text + "</td></tr></table></td></tr><tr>";
            strpdf += "<td colspan='4'></td></tr></table>";

            Document document = new Document();
            try
            {
                string Pdf_path = string.Empty;
                Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/COA-BO(" + orderid.ToString() + "_" + dtPOProductList.Rows[count]["ProductID"].ToString() + ").pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/COA-BO(" + orderid.ToString() + "_" + dtPOProductList.Rows[count]["ProductID"].ToString() + ").pdf";
                path += Pdf_path;
                if (count < dtPOProductList.Rows.Count - 1)
                    path += "$";
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
                mypara.IndentationLeft = 36;
                mypara.InsertRange(0, htmlarraylist);
                document.Add(mypara);
                document.Close();
                //orderObj.OrderDetails_UPD(orderid, Pdf_path, "bhanu");
                result = true;
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
                result = false;
            }
            finally
            {
                //document.Close();
            }
        }
        result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), path, "Bhanu", string.Empty, rtypeObj.COA_BO);
        return result;
    }
}
