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

public partial class FirCoverLetter : System.Web.UI.Page
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
        }
    }
    protected void btnFirCoverSubmit_Click(object sender, EventArgs e)
    {
        lblFIRnum.Text = txtFIRno.Text;
        generatePDF();
        Response.Redirect("~/Mudar/UpdateOrder.aspx");
    }
    private void BuyerDetails()
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
            lblCompanyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
        }
    }
    private void BindPODetails()
    {
        DataTable dtPO = orderObj.OrderList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtPO.Rows.Count > 0)
        {
            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
            decimal Total_Amount = 0;
            for (int count = 0; count < dtPOProductList.Rows.Count; count++)
            {
                Total_Amount += Convert.ToDecimal(dtPOProductList.Rows[count]["TotalPrice"].ToString());
                
            }
            lblInvAmout.Text = string.Format("{0:n0}", Total_Amount);
            lblDestCountry.Text = dtPO.Rows[0]["DestinationCountry"].ToString();
           
        }
    }
    private void BindInvoiceDetails()
    {
        DataTable dtInvoiceDetails = invoiceObj.InvoiceDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtInvoiceDetails.Rows.Count > 0)
        {
            lblInviNo.Text = dtInvoiceDetails.Rows[0]["InvoiceId"].ToString();
            lblInv.Text = dtInvoiceDetails.Rows[0]["InvoiceId"].ToString();
            lblInvoice.Text = dtInvoiceDetails.Rows[0]["InvoiceId"].ToString(); 
            lblInvoiceDt.Text = string.Format("{0: dd MMM yyyy}", dtInvoiceDetails.Rows[0]["CreatedDate"]);
            lblInvoiceDate.Text = string.Format("{0: dd MMM yyyy}", dtInvoiceDetails.Rows[0]["CreatedDate"]);
            lblPackDt.Text = string.Format("{0: dd MMM yyyy}", dtInvoiceDetails.Rows[0]["CreatedDate"]);
        }
    }
    private void generatePDF()
    {
        
        string  strpdf ="<table align='center' style='font-family:Verdana;width:885px;'><tr>";
        strpdf +="<td width='50%'></td><td width='50%' align='right'> Date:"+lblTodayDate.Text+"</td></tr><tr>";
        strpdf += "<td colspan='2' align='left'>" + txtAddress.Text.Replace("\r\n", "<br/>") + "</td></tr><tr>";
        strpdf +="<td colspan='2'></td></tr><tr>";
        strpdf +="<td colspan='2'></td></tr><tr>";
        strpdf +="<td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dear Sir:<span style='font-size:10px;'>"+txtDearsir.Text + "</td></tr><tr>";
        strpdf +="<td colspan='2'></td></tr><tr>";
        strpdf +="<td>&nbsp;Sub:- <span style='font-size:10px;'>Submission of Export Documents</td></tr><tr>";
        strpdf +="<td colspan='2'></td></tr><tr>";
        strpdf += "<td>&nbsp;Ref:- <span style='font-size:10px;'>Our Inv No : " + lblInviNo.Text + " &nbsp;&nbsp;&nbsp;  Dt : " + lblInvoiceDate.Text + " </td><td align='center'><span style='font-size:10px;'>FIR no:" + txtFIRno.Text + "</td></tr><tr>";
        strpdf +="<td colspan='2'></td></tr><tr>"; 
        strpdf +="<td colspan='2'align='center' style='font-size: 10px;';>Please find the below mentioned documents against the above mentioned FIR</td></tr><tr>";
        strpdf +="<td colspan='2' align='center'>&nbsp;</td></tr><tr>";         
        strpdf+="<td colspan='2'align='center'><table border='1'>'<tr>";
        strpdf+="<td bgcolor='#FFCC66'><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></td><td bgcolor='#FFCC66' colspan='3' ><b>Document Reference</b></td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>Buyer</td><td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+lblCompanyAddress.Text+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;&nbsp;"+lblDestCountry.Text+" &nbsp;&nbsp;&nbsp;</td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>Inv No</td><td>"+lblInvoice.Text+"</td><td>Dt</td><td>"+lblInvoiceDt.Text+"</td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>Packing List No</td><td><asp:Label ID="+lblInv.Text+" /></td><td>Dt</td><td><asp:Label ID='lblPackDt' runat='server' /></td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>AWB / BoL</td><td colspan='3'>"+lblAWB.Text+"</td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>SDF No</td><td>"+txtSDFNo.Text+"</td><td>Dt</td><td>"+txtSDFNoDt.Text+"</td></tr><tr>";
        strpdf+="<td bgcolor='#FFCC66'>EP Copy</td><td>"+txtEPcopy.Text+"</td><td>Dt</td><td>"+txtEPcopyDt.Text+"</td></tr></table><tr>";
        strpdf+="<td colspan='2' align='center'>&nbsp;</td></tr><tr>";
        strpdf+="<td colspan='2' align='center'><span style='font-size:10px;'>Please find the details of FIR against the above mentioned shipment</td></tr><tr>";
        strpdf+="<td colspan='2'><table border='1'><tr align='center'>";
        strpdf+="<td bgcolor='#FFCC66'><b>Invoice<br />Amount in USD</b></td><td bgcolor='#FFCC66'><b>&nbsp;&nbsp;&nbsp;FIR NO&nbsp;&nbsp;&nbsp;</b></td><td bgcolor='#FFCC66'><b>FIR Date</b></td><td bgcolor='#FFCC66'><b>FIR Amount in USD</b></td><td bgcolor='#FFCC66'><b>Amount<br /> Against the Invoice in USD</b></td></tr><tr>";
        strpdf+="<td><b>"+lblInvAmout.Text+"</b></td><td><b>&nbsp;&nbsp;"+lblFIRnum.Text+"&nbsp;&nbsp;</b></td><td><b>"+txtFIRDate.Text+"</b></td><td><b>"+txtFIRAmt.Text+"</b></td><td><b>"+txtAmtAgnistINV.Text+"</b></td></tr></table><tr align='center'>";
        strpdf+="<td colspan='2' align='center'>&nbsp;</td></tr><tr>";
        strpdf+="<td colspan='2' align='center'>Please acknowledge the receipt of all the documents in order</td></tr><tr>";
        strpdf+="<td colspan='2' align='center'>&nbsp;</td></tr></table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            string orderid = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/FIR_Covering Letter_" + orderid + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/FIR_Covering Letter_" + orderid + ".pdf";

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


            bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Fir_Cover_Letter);

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
