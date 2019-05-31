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

public partial class CoverLetter : System.Web.UI.Page
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
            BuyerDetails();
            BindPODetails();
            BindInvoiceDetails();
            BindReportDocus();
        }
    }
    private void BuyerDetails()
    {
         DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
         if (dtBuyer.Rows.Count > 0)
         {
             lblCompanyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
             string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
             lblCompanyAddress.Text += "<br/>" + Address[0].ToString();
             if (Address.Length > 1)
             {
                 lblCompanyAddress.Text += "<br/>" + Address[1].ToString() + "," + Address[2].ToString();
             }
             lblCompanyAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCity"].ToString();
             lblCompanyAddress.Text += "<br/>" + dtBuyer.Rows[0]["CState"].ToString();
             lblCompanyAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCountry"].ToString();
             lblCompanyAddress.Text += "<br/>" + dtBuyer.Rows[0]["CContactPerson"].ToString();
             lblCompanyAddress.Text += "<br/>" + dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
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
            gvPurchaseOrder.DataSource = dtPOProductList;
            gvPurchaseOrder.DataBind();
        }
    }
    private void BindInvoiceDetails()
    {
        DataTable dtInvoiceDetails = invoiceObj.InvoiceDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtInvoiceDetails.Rows.Count > 0)
        {
            lblInvoiceNo.Text = dtInvoiceDetails.Rows[0]["InvoiceId"].ToString();
            lblInvoiceDate.Text = string.Format("{0: dd MMM yyyy}",dtInvoiceDetails.Rows[0]["CreatedDate"]);
        }
    }
    private void BindReportDocus()
    {
        gvReports.DataSource = reportObj.GetReports();
        gvReports.DataBind();
    }
    private void generatePDF()
    {

         string strpdf = "<table align='center' style='font-family:Verdana;width:885px;'><tr>";
         strpdf += "<td width='50%'></td><td width='50%' align='right'> Date: "+lblTodayDate.Text+"</td></tr><tr>";
         strpdf +="<td colspan='2' align='left'>"+lblCompanyAddress.Text+"</td></tr><tr>";
         strpdf +="<td colspan='2'></td></tr><tr>";
         strpdf += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Attn:<span style='font-size:10px;'>" + txtAttn.Text + "</td></tr><tr>";
         strpdf += "<td colspan='2'></td></tr><tr>";
         strpdf += "<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dear Sir:<span style='font-size:10px;'>" + txtDearsir.Text + "</td></tr><tr>";
         strpdf += "<td colspan='2'></td></tr><tr>";
         strpdf += "<td>&nbsp;Sub :- <span style='font-size:10px;'>Original Docs against Inv No :" + lblInvoiceNo.Text + "</span></td><td align='center' style='font-size: 10px;'>Date: " + lblInvoiceDate.Text + "</td></tr><tr>";
         strpdf +="<td colspan='2'></td></tr><tr>";
         strpdf += "<td >&nbsp;Ref :- <span style='font-size:10px;'>Your PO Number :" + lblPO.Text + "</td><td align='center' style='font-size: 10px;'>Date: " + lblPODate.Text + "</td></tr><tr>";
         strpdf +="<td colspan='2'></td></tr><tr>";
         strpdf += "<td colspan='2'align='center' style='font-size: 10px;';>We have dispatched the below mentioned items as per the PO and Inv referances given above:</td></tr><tr>";
         strpdf +=" <td colspan='2' align='center'>&nbsp;</td></tr><tr>";

         strpdf+="<td colspan='2'><table border='1'>";
         //grid start
         strpdf += "<tr align='center' width='20%'><td>S.No</td><td align='center'width='50%'>Product Name</td><td align='center' width='30%'>Qty in KG</td></tr>";
         foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
             strpdf += "<tr><td align='center' width='20%'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "";
         ////grid end
         strpdf += "</table></td></tr><tr>";

         strpdf+="<td colspan='2' align='center'>&nbsp;</td></tr><tr>";
         strpdf += "<td colspan='2' align='center'style='font-size: 10px;' >We are here with enclosing the below mentioned original documents:</td> </tr><tr>";      
         strpdf+=" <td colspan='2' align='center'>&nbsp;</td></tr><tr>";


         strpdf += "<td colspan='2'><table border='1'>";

         strpdf += "<tr><td align='center'>S.No</td><td align='center'>Document</td><td align='center'>No of Copies</td></tr>";
         foreach (GridViewRow gvr in gvReports.Rows)
         {
             if ((gvr.Cells[2].FindControl("cbDoc") as CheckBox).Checked)
                 strpdf += "<tr><td align='center' width='20%' >" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + (gvr.Cells[3].FindControl("txtNoOfCopies") as TextBox).Text + "</td></tr>";
         }
         strpdf += "</table></td></tr>";


         strpdf+="<td colspan='2' align='center'>&nbsp;</td></tr><tr>";      
         strpdf+=" <td colspan='2' align='center'>Please acknowledge the receipt of the documents.</td></tr><tr>";
         strpdf+="<td colspan='2'></td></tr></table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            string orderid = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/Covering Letter_" + orderid + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/Covering Letter_" + orderid + ".pdf";

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


            bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Cover_Letter);


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
    protected void btnCoverSubmit_Click(object sender, EventArgs e)
    {
        generatePDF();
        Response.Redirect("~/Mudar/UpdateAdminOrder.aspx");
    }
}

