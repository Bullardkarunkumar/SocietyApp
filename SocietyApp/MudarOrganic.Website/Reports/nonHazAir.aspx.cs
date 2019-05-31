using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Configuration;
using System.IO;

public partial class Reports_nonHazAir : System.Web.UI.Page
{
    BranchsRolesEmployees_BL branchObj = new BranchsRolesEmployees_BL();
    Buyer_BL buyerObj = new Buyer_BL();
    Order_BL orderObj = new Order_BL();
    Reports_BL reportObj = new Reports_BL();
    Invoice_BL invoiceObj = new Invoice_BL();
    MudarUser mu = new MudarUser();
    Reports_Type rtypeObj = new Reports_Type();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblDate.Text = string.Format("{0: dd MMM yyyy}", DateTime.Now);
            BindPODetails();
            BindMainBranchDetails();
            BuyerDetails();
            BindInvoice();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string strpdf = "<table align='center' style='font-family: Verdana; width: 885px'><tr bgcolor='#ffcc66'>";
        strpdf += "<td><table align='center' style='font-family: Verdana;'><tr><td align='center' style='font-size: 12px;'>Mudar India Exports</td></tr><tr>";
        strpdf += "<td align='center' style='font-size: 10px;'>6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
        strpdf += "<td align='right'><span style='font-size:10px;'>Date:" + lblDate.Text + "</td></tr><tr>";
        strpdf += "<td align='center'>Shipper’s Certificate for Non-Hazardous Cargo</td></tr><tr>";
        strpdf += "<td><table width='100%'border='1'><span style='font-size:10px;'><tr align='center'><td>AWB no</td><td>Airport of Departure</td><td>Airport of Destination</td></tr><tr>";
        strpdf += "<tr align='center'><td>"+txtAWB.Text+"</td><td>"+lblDeparture.Text+"</td><td>"+lblDestination.Text+"</td></tr><tr></table></td></tr><tr>";
        strpdf += "<td style='font-size: 10px;'>This to certify that the articles / substances of this shipment are properly described by name, that they are <br/>not listed in the current edition of IATA.Dangerous Goods Regulation (DGR), Alphabetical list of Dangerous <br/>Goods nor do they correspond to any hazard classes appearing in DGR.  Section 3, classification of <br/>Dangerous Goods and they are known to be not Dangerous i.e. Non restricted.  Furthermore the shipper confirms that the goods are in proper Condition for Transportation on Passenger Carrying aircraft (DGR, para 8.1.23)";
        strpdf += "</td></tr><tr>";
        strpdf += "<td><table border='1'>";
        strpdf += "<span style='font-size:10px;'><tr align='center'><td>Number of Packages</td><td>Proper description of Goods (trade Name not permitted) specify each article separately</td><td>Net Qty for Package</td></tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            strpdf += "<tr align='center'><td>" + gvr.Cells[0].Text + "</td><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td></tr>";
        strpdf += "</table></td></tr><tr>";
        strpdf += "<td></td></tr><tr>";
        strpdf += "<td><table width='100%' border='1'><span style='font-size:10px;'><tr align='center'><td width='50%'>" + lblShipperAddress.Text.Replace("@", " ") + "</td><td width='50%'>" + lblBranchAddress.Text.Replace("@", " ") + "</td></tr></table></tr></table>";

        //string strpdf ="<table align='center' style='font-family: Verdana; width: 885px'><tr bgcolor='#ffcc66'>";
        //strpdf += "<td><table align='center' style='font-family: Verdana;'><tr><td align='center' style='font-size: 12px;'> <h1>Mudar India Exports</h1> </td> </tr> <tr><td align='center' style='font-size: 10px;'> 6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
        //strpdf += "<td align='right'>Date:" + lblDate.Text + "</td></tr><tr>";
        //strpdf += "<td align='center'>Shipper’s Certificate for Non-Hazardous Cargo </td></tr><tr>";
        //strpdf += "<td><table width='100%' border='1'><tr align='center'><td width='24%'>AWB no</td><td width='38%'>Airport of Departure </td> <td width='38%'>Airport of Destination</td></tr><tr align='center'>";
        //strpdf += "<td width='24%'>" + txtAWB.Text + "</td><td width='%'>" + lblDeparture.Text + "</td><td width='38%'>" + lblDestination.Text + "</td></tr></table></td></tr><tr>";
        //strpdf += "<td style='font-size: 10px;'>This to certify that the articles / substances of this shipment are properly described by name, that they are not<br/>listed in the current edition of IATA. Dangerous Goods Regulation (DGR), Alphabetical list of Dangerous Goods nor do they correspond to any hazard classes appearing in DGR. Section 3, classification of Dangerous Goods and they are known to be not Dangerous i.e. Non restricted. Furthermore the shipper confirms that the goods are in proper Condition for Transportation on Passenger Carrying aircraft (DGR, para 8.1.23)</td></tr><tr>";
        //strpdf += "<td>datagrid</td></tr><tr>";
        //strpdf += "<td><table width='100%' border='1'><tr align='center'> <td width='50%'>" + lblShipperAddress.Text + "</td><td width='50%'>" + lblBranchAddress.Text + "</td></tr><tr align='center'>";
        //strpdf += "<td width='50%' rowspan='3'> &nbsp;</td><td width='50%' rowspan='3'>&nbsp;</td></tr></table></td></tr></table>";

        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            string orderid = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/NonHazAir_" + orderid + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/NonHazAir_" + orderid + ".pdf";

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

            bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Non_Haz_Air);
            Response.Redirect("~/Mudar/UpdateOrder.aspx");
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
    private void BindInvoice()
    {
        DataTable dtInvoice = invoiceObj.InvoiceDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtInvoice.Rows.Count > 0)
        {
            lblDeparture.Text = dtInvoice.Rows[0]["DestinationCountry"].ToString();
            lblDeparture.Text = dtInvoice.Rows[0]["LoadingPort"].ToString();
        }
    }
    private void BuyerDetails()
    {

        DataTable dtBuyer = buyerObj.BuyerDetails(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            //lblNotifyAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NAddress"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NCity"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NState"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NCountry"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NContactPhoneNo"].ToString();
            //lblNotifyAddress.Text += "<br>" + dtBuyer.Rows[0]["NContactPhoneNo"].ToString();
            lblShipperAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            lblShipperAddress.Text += "<br/>" + Address[0].ToString();
            if (Address.Length > 1)
            {
                lblShipperAddress.Text += "<br/>" + Address[1].ToString() + "," + Address[2].ToString();
            }
            //lblShipperAddress.Text += "<br>" + dtBuyer.Rows[0]["CAddress"].ToString();
            lblShipperAddress.Text += "<br>" + dtBuyer.Rows[0]["CCity"].ToString();
            lblShipperAddress.Text += "<br>" + dtBuyer.Rows[0]["CState"].ToString();
            lblShipperAddress.Text += "<br>" + dtBuyer.Rows[0]["CCountry"].ToString();
          
        }
    }
    private void BindMainBranchDetails()
    {
        DataTable dtBranch = branchObj.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            lblBranchAddress.Text = dtBranch.Rows[0]["Bname"].ToString();
            lblBranchAddress.Text += "<br/>" + dtBranch.Rows[0]["Address"].ToString();
            lblBranchAddress.Text += "<br/>" + dtBranch.Rows[0]["City"].ToString();
            lblBranchAddress.Text += "<br/>" + dtBranch.Rows[0]["State"].ToString();
            lblBranchAddress.Text += "<br/>" + dtBranch.Rows[0]["Country"].ToString();
            
        }
    }
    private void BindPODetails()
    {
        DataTable dtPO = orderObj.OrderList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
        if (dtPO.Rows.Count > 0)
        {
            DataTable dtPOProductList = orderObj.OrderProductList(Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true)));
            gvPurchaseOrder.DataSource = dtPOProductList;
            gvPurchaseOrder.DataBind();
            decimal Total_Net_Qty = 0, Total_Gross_Qty = 0, Total_Durm = 0;
            if (dtPOProductList.Rows.Count > 0)
            {
                for (int count = 0; count < dtPOProductList.Rows.Count; count++)
                {
                    //Total_Amount += Convert.ToDecimal(dtPOProductList.Rows[count]["TotalPrice"].ToString());
                    Total_Net_Qty += Convert.ToDecimal(dtPOProductList.Rows[count]["Quantity"].ToString());
                    Total_Gross_Qty += Convert.ToDecimal(dtPOProductList.Rows[count]["GrossQuantity"].ToString());
                    Total_Durm += Convert.ToDecimal(dtPOProductList.Rows[count]["Packing25"].ToString()) + Convert.ToDecimal(dtPOProductList.Rows[count]["Packing180"].ToString());
                }
                //lblNetQty.Text = Total_Net_Qty.ToString();
                //lblGrossQty.Text = Total_Gross_Qty.ToString();

                //lblTotalAmout.Text = string.Format("{0:n0}", Total_Amount);
                //lblTotalDrum.Text = Total_Durm.ToString();
                //DataTable test = orderObj.Convert_NumberToWord(Total_Amount);
                //lblAmount_word.Text = test.Rows[0][0].ToString() + " Only";
            }
            lblDestination.Text = dtPO.Rows[0]["DestinationCountry"].ToString();
            lblDeparture.Text = dtPO.Rows[0]["DestinationPort"].ToString();

        }
    }
}