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

public partial class Reports_nonHazSea : System.Web.UI.Page
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
            lblDate.Text = string.Format("{0: dd MMM yyyy}", DateTime.Now);
            BindInvoice();
            BuyerDetails();
            generatePDF();
            Response.Redirect("~/Mudar/UpdateOrder.aspx");
        }
    }
    private void BindInvoice()
    {
        DataTable dtInvoice = invoiceObj.InvoiceDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtInvoice.Rows.Count > 0)
        {
            lblInvoice.Text = dtInvoice.Rows[0]["Invoiceid"].ToString();
            lblInvoiceDate.Text = string.Format("{0: dd MMM yyyy}", Convert.ToDateTime(dtInvoice.Rows[0]["CreatedDate"].ToString()));
            lblDCountry.Text = dtInvoice.Rows[0]["DestinationCountry"].ToString();

            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));

            gvProductList.DataSource = dtPOProductList;
            gvProductList.DataBind();
        }
    }
    private void BuyerDetails()
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            lblCAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            lblCAddress.Text += "<br/>" + Address[0].ToString();
            if (Address.Length > 1)
            {
                lblCAddress.Text += "<br/>" + Address[1].ToString() + "," + Address[2].ToString();
            }
            //lblCAddress.Text += "," + dtBuyer.Rows[0]["CAddress"].ToString();
            lblCAddress.Text += "," + dtBuyer.Rows[0]["CCity"].ToString();
            lblCAddress.Text += "," + dtBuyer.Rows[0]["CState"].ToString();
            lblCAddress.Text += "," + dtBuyer.Rows[0]["CCountry"].ToString();
            //lblCAddress.Text += "," + dtBuyer.Rows[0]["CContactPerson"].ToString();
            //lblCAddress.Text += "," + dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
        }
    }
    private void generatePDF()
    {
        string strpdf = "<table align='center' style='font-family: Verdana; width: 885px'><tr bgcolor='#ffcc66'>";
        strpdf += "<td><table align='center' style='font-family: Verdana;'><tr><td align='center' style='font-size: 12px;'>Mudar India Exports</td></tr><tr>";
        strpdf += "<td align='center' style='font-size: 10px;'>6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='4' align='right'><span style='font-size:10px;'>Date: " + lblDate.Text + "</td></tr><tr>";
        strpdf += "<td colspan='4' align='center'>To Whomever It may Concern</td></tr><tr>";
        strpdf += "<td colspan='4'>&nbsp;</td></tr><tr>";
        strpdf += "<td colspan='4' align='center'>We declare that the below mentioned goods</td></tr><tr>";
        strpdf += "<td colspan='4'>&nbsp;</td></tr><tr>";
        strpdf += "<td colspan='4'><table border='1'>";
        //grid start
        strpdf += "<tr align='center'><td>S.No.</td><td>Product Name</td><td>Total Drums</td><td>Total Qty in KG</td></tr>";
        foreach (GridViewRow gvr in gvProductList.Rows)
            strpdf += "<tr align='center'><td>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td></tr>";//<td>" + gvr.Cells[5].Text + "</td><td>" + gvr.Cells[6].Text + "</td>
        //grid end
        strpdf += "</table></td></tr><tr>";
        strpdf += "<td width='25%' align='center'<span style='font-size:10px;'>&nbsp;Shipped to :</td><td colspan='2' width='45%' align='left'><span style='font-size:10px;'>" + lblCAddress.Text + ".</td><td width='30%' align='center'><span style='font-size:10px;'> " + lblDCountry.Text + "</td></tr><tr>";
        strpdf += "<td width='50%' colspan='2'align='left'>&nbsp;through our Invoice No&nbsp;:<span style='font-size:10px;'>" + lblInvoice.Text + " </td><td width='25%'>&nbsp;Date&nbsp; :<span style='font-size:10px;'>" + lblInvoiceDate.Text + "</td><td width='25%'><span style='font-size:10px;'>is 100% Non-Hazardous</td></tr></table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            string orderid = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/NonHazSea_" + orderid + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/NonHazSea_" + orderid + ".pdf";

            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();
            String htmlText = strpdf.ToString();
            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();
            //updare path in order report table for nonHazSea
            bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Non_Haz_Sea);
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
