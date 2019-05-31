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

public partial class LabelSamplePage : System.Web.UI.Page
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
            generatePDF();
            Response.Redirect("~/Mudar/UpdateOrder.aspx");
        }

    }
    private void BuyerDetails()
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            lblCompanyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
           
        }
    } 
   
    private void BindPODetails()
    {
         DataTable dtPO = orderObj.OrderList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
         if (dtPO.Rows.Count > 0)
         {
             DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
             decimal Total_Durm = 0, Tare_wt= 0;
             if (dtPOProductList.Rows.Count > 0)
             {
                 for (int count = 0; count < dtPOProductList.Rows.Count; count++)
                 {
                     Total_Durm += Convert.ToDecimal(dtPOProductList.Rows[count]["Packing25"].ToString()) + Convert.ToDecimal(dtPOProductList.Rows[count]["Packing180"].ToString());
                 }
               
             }
             lblGrossWt.Text = Convert.ToDecimal(dtPOProductList.Rows[0]["GrossQuantity"]).ToString();
             lblNetWt.Text = Convert.ToDecimal(dtPOProductList.Rows[0]["Quantity"]).ToString();
             Tare_wt = Convert.ToDecimal(dtPOProductList.Rows[0]["GrossQuantity"].ToString()) - Convert.ToDecimal(dtPOProductList.Rows[0]["Quantity"].ToString());
             lblTareWt.Text = Tare_wt.ToString();
             lblDrumNo.Text = Total_Durm.ToString();
             lblDCountry.Text = dtPO.Rows[0]["DestinationCountry"].ToString();
            
         }
           
    }
    private bool generatePDF()
    {
        bool result = false;
        int orderid = Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true));
        DataTable dtPOProductList = orderObj.OrderProductList(orderid);
        string path = string.Empty;
        for (int count = 0; count < dtPOProductList.Rows.Count; count++)
        {
            string strpdf = string.Empty;
            for (int dCount = 0; dCount < Convert.ToInt32(dtPOProductList.Rows[count]["TotalDrums"].ToString()); dCount++)
            {
                strpdf += "<table width='100%' align='center' border='1' style='font-family:Verdana;'><tr>";
                strpdf += "<td  colspan='4' align='center' bgcolor='#ffcc66'>" + dtPOProductList.Rows[count]["ProductName"].ToString() + "</td></tr><tr>";
                strpdf += "<td colspan='4' style='font-size: 9px' align='center'> ( Product Produced &amp; Processed in accordance with requirements of India’s National Program for Organic Production (NPOP) which is considered equivalent to Council Regulation (EC) 834/2007 &amp; also as per USDA-NOP)</td></tr><tr>";
                strpdf += "<td  colspan='4' style='font-size: 15px' align='center'> Licensee Producer</td></tr><tr>";
                strpdf += "<td colspan='4' align='center'> <b>Mudar India Exports</b></td></tr><tr>";
                strpdf += "<td colspan='4' style='font-size: 12px' align='center'> 6-1-744, Kovur Nagar, ANANTAPUR - 515004 Andhra Pradesh, India</td></tr><tr>";
                strpdf += "<td colspan='4' style='font-size: 12px' align='center'> <b>Certified Organic by CU-025367</b></td></tr><tr>";
                strpdf += "<td colspan='2' width='50%' align='center'> Buyer</td><td colspan='2'>&nbsp;&nbsp;&nbsp; <b>CompanyName</b></td></tr><tr> ";
                strpdf += "<td  width='25%' align='center'> Country of Origin</td><td  width='25%' align='center'> &nbsp;&nbsp;India</td><td  width='25%' align='center'> Country of Destination</td><td  width='25%' align='center'>" + lblDCountry.Text + "</td></tr><tr>";
                strpdf += "<td  width='25%' align='center'> Gross Weight (KG)</td><td  width='25%' align='center'> " + dtPOProductList.Rows[count]["GrossQuantity"].ToString() + "</td> <td  width='25%' align='center' colspan='2' style='width: 50%'> <b>Do Not Fumigate</b></td></tr><tr>";
                strpdf += "<td  width='25%' align='center'> Tare Weight (KG)</td><td  width='25%' align='center'> " + (Convert.ToDecimal(dtPOProductList.Rows[count]["GrossQuantity"].ToString()) - Convert.ToDecimal(dtPOProductList.Rows[count]["Quantity"].ToString())).ToString() + "</td><td  width='25%' align='center'> Lot Number</td><td  width='25%' align='center'> " + dtPOProductList.Rows[count]["BatchID"].ToString() + "</td></tr><tr>";
                strpdf += "<td  width='25%' align='center'> Net Weight(KG)</td><td  width='25%' align='center'> " + dtPOProductList.Rows[count]["Quantity"].ToString() + "</td><td  width='25%' align='center'> Drum Number</td><td  width='25%' align='center'> " + (dCount + 1) + "</td></tr></table>";
            }
            Document document = new Document();
            try
            {
                string Pdf_path = string.Empty;
                Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/Label(" + orderid.ToString() + "_" + dtPOProductList.Rows[count]["ProductID"].ToString() + ").pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/Label(" + orderid.ToString() + "_" + dtPOProductList.Rows[count]["ProductID"].ToString() + ").pdf";
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
        result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), path, "Bhanu", string.Empty, rtypeObj.LABEL);
        return result;
    }
}
