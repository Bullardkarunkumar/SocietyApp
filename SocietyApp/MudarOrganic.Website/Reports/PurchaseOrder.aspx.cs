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


public partial class Reports_PurchaseOrder : System.Web.UI.Page
{
    BranchsRolesEmployees_BL branchObj = new BranchsRolesEmployees_BL();
    Buyer_BL buyerObj = new Buyer_BL();
    Reports_BL reportObj = new Reports_BL();
    Order_BL orderObj = new Order_BL();
    MudarUser mu = new MudarUser();
    MudarApp mudarObj = new MudarApp();
    string LotsampleID = string.Empty;
    Emailtest email = new Emailtest();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindBranchAddress();
            BindPriceTerms();
            BindGvPurchaseOrder();
            BindBuyerDetails();
            lblOrganicType.Text = Session["OrganicType"].ToString();
            lblplaceofdelivery.Text = Session["placeofdelivery"].ToString();
            lblOverSeaDate.Text = DateTime.Now.ToShortDateString();
            ddlPlaceOfDelivery_SelectedIndexChanged(sender, e);
            BindgvReports();
            if (Session["OrderType"].ToString() == "LotSample")
            {
                //LotsampleID = mudarObj.SystemGenerateBuyerLotSample();
                LotsampleID = "";
                DateTime Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                lblDate.Text = string.Format("{0:dd MMM yyyy}", Date);
            }
            else
            {
                string[] po = Session["s_PODetails"].ToString().Split('$');
                if (!string.IsNullOrEmpty(po[0].ToString()))
                {
                    lblBPO.Text = po[0].ToString();
                    lblComments.Text = po[3].ToString();
                    DateTime date = Convert.ToDateTime(!string.IsNullOrEmpty(po[1].ToString()) ? po[1].ToString() : DateTime.Now.ToShortDateString());
                    lblDate.Text = string.Format("{0:dd MMM yyyy}", date);
                }
                else
                {
                    //lblPO.Text = mudarObj.SystemGenerateBuyerPO();
                    lblPO.Text = "";
                    lblComments.Text = po[3].ToString();
                    DateTime date = Convert.ToDateTime(!string.IsNullOrEmpty(po[1].ToString()) ? po[1].ToString() : DateTime.Now.ToShortDateString());
                    lblDate.Text = string.Format("{0:dd MMM yyyy}", date);
                }
            }
            btnConfirm_Click(sender, e);

        }
    }
    private void BindGvPurchaseOrder()
    {
        decimal tPrice = 0, tDrums25 = 0, tDrums180 = 0;
        DataTable dtOrder = new DataTable();
        if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
        {
            dtOrder = (Session["sDtOrder"] as DataTable);
            foreach (DataRow dr in dtOrder.Rows)
            {
                tPrice += Convert.ToDecimal(dr["TotalPrices"].ToString());
                tDrums25 += Convert.ToDecimal(dr["PackingDetails25"].ToString());
                tDrums180 += Convert.ToDecimal(dr["PackingDetails180"].ToString());
                if (dr["money"].ToString() == "1")
                {
                    lblmoney.Text = "USD";
                    lblTaxesDuties.Text = "not applicable";
                    lblSalesTaxTerms.Text = "not applicable";
                }
                else
                {
                    lblmoney.Text = "INR";

                    lblTaxesDuties.Text = "as applicable";
                    lblSalesTaxTerms.Text = "as applicable";
                }
            }
        }
        lblDrum25.Text = tDrums25.ToString();
        lblDrum180.Text = tDrums180.ToString();
        lblTotalprice.Text = tPrice.ToString();

        lblTotalprice.Text = string.Format("{0:0.00}", tPrice);
        string[] word = lblTotalprice.Text.Split('.');
        string main = NumberToWords(Convert.ToInt32(word[0].ToString()));
        if (lblmoney.Text == "USD")
        {
            lblAmountword.Text += main + ' ' + " USD";
            if (Convert.ToInt32(word[1].ToString()) > 0)
            {
                string sub = NumberToWords(Convert.ToInt32(word[1].ToString()));
                lblAmountword.Text += ' ' + " and " + ' ' + sub + ' ' + "Cents";
            }
        }
        else
        {
            lblAmountword.Text += main + ' ' + " Rupees";
            if (Convert.ToInt32(word[1].ToString()) > 0)
            {
                string sub = NumberToWords(Convert.ToInt32(word[1].ToString()));
                lblAmountword.Text += ' ' + " and " + ' ' + sub + ' ' + "Paise";
            }
        }
        gvPurchaseOrder.DataSource = dtOrder;
        gvPurchaseOrder.DataBind();

    }
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " Million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();
        sbHtml.Append(File.ReadAllText(Server.MapPath("~/Reports/pdfReport.htm")));

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition",
         "attachment;filename=GridViewExport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        StringReader sr = new StringReader(sbHtml.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();

    }
    private void BindBranchAddress()
    {
        DataTable dtBranch = branchObj.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            Session["branchID"] = dtBranch.Rows[0]["branchID"].ToString();
            txtCompanyAddress.Text = dtBranch.Rows[0]["Bname"].ToString();
            txtCompanyAddress.Text += "<br/>" + dtBranch.Rows[0]["Address"].ToString();
            txtCompanyAddress.Text += "<br/>" + dtBranch.Rows[0]["City"].ToString();
            txtCompanyAddress.Text += "<br/>" + dtBranch.Rows[0]["State"].ToString();
            txtCompanyAddress.Text += "<br/>" + dtBranch.Rows[0]["Country"].ToString();
        }
    }
    private void BindBuyerDetails()
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(Session["BuyerId"].ToString());
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            Session["BuyerDetails"] = dr;
            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
            item.Text = dr["CCity"].ToString();
            item.Value = "C";
            ddlPlaceOfDelivery.Items.Add(item);
            item = new System.Web.UI.WebControls.ListItem();
            item.Text = dr["NCity"].ToString();
            item.Value = "N";
            ddlPlaceOfDelivery.Items.Add(item);
            for (int count = 0; count < ddlPriceTerms.Items.Count; count++)
            {
                string coloumn = ddlPriceTerms.Items[count].Text;
                if ((bool)dr[coloumn])
                {
                    ddlPriceTerms.SelectedIndex = count;
                }
                //ddlPriceTerms.Items[count].Value = dr["BuyerPriceID"].ToString();
            }
            ddlModeofTransport.SelectedValue = dr["ModeofTransport"].ToString();
            // buyer address
            lblBuyerCopmany.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();

            lblBuyerAddress.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            if (!string.IsNullOrEmpty(Address[0].ToString()))
                lblBuyerAddress.Text += "<br/>" + Address[0].ToString();
            if (Address.Length > 1)
            {
                if (!string.IsNullOrEmpty(Address[1].ToString()))
                    lblBuyerAddress.Text += "<br/>" + Address[1].ToString();
                if (!string.IsNullOrEmpty(Address[2].ToString()))
                    lblBuyerAddress.Text += "," + Address[2].ToString();
            }
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["CCity"].ToString()))
                lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCity"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["CState"].ToString()))
                lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CState"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["CCountry"].ToString()))
                lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CCountry"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["CPincode"].ToString()))
                lblBuyerAddress.Text += "<br/>" + dtBuyer.Rows[0]["CPincode"].ToString();

            //Notify Address
            lblNotifyAddress.Text = dtBuyer.Rows[0]["NotifyName"].ToString();

            string[] nAddress = dtBuyer.Rows[0]["NAddress"].ToString().Split('@');
            if (!string.IsNullOrEmpty(nAddress[0].ToString()))
                lblNotifyAddress.Text += "<br/>" + nAddress[0].ToString();
            if (nAddress.Length > 1)
            {
                if (!string.IsNullOrEmpty(nAddress[1].ToString()))
                    lblNotifyAddress.Text += "<br/>" + nAddress[1].ToString();
                if (!string.IsNullOrEmpty(nAddress[2].ToString()))
                    lblNotifyAddress.Text += "," + nAddress[2].ToString();
            }
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["NCity"].ToString()))
                lblNotifyAddress.Text += "<br/>" + dtBuyer.Rows[0]["NCity"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["NState"].ToString()))
                lblNotifyAddress.Text += "<br/>" + dtBuyer.Rows[0]["NState"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["NCountry"].ToString()))
                lblNotifyAddress.Text += "<br/>" + dtBuyer.Rows[0]["NCountry"].ToString();
            if (!string.IsNullOrEmpty(dtBuyer.Rows[0]["NPincode"].ToString()))
                lblNotifyAddress.Text += "<br/>" + dtBuyer.Rows[0]["NPincode"].ToString();
            //buyer bank address
            lblBankAddress.Text = dtBuyer.Rows[0]["BankName"].ToString();
            string[] bAddress = dtBuyer.Rows[0]["BankAddress"].ToString().Split('@');
            lblBankAddress.Text += "<br/>" + bAddress[0].ToString();
            if (bAddress.Length > 1)
            {
                lblBankAddress.Text += "<br/>" + bAddress[1].ToString() + "," + bAddress[2].ToString();
            }

            lblBankAddress.Text += "<br/>" + dtBuyer.Rows[0]["BankCity"].ToString();
            lblBankAddress.Text += "<br/>" + dtBuyer.Rows[0]["BankState"].ToString();
            lblBankAddress.Text += "<br/>" + dtBuyer.Rows[0]["BankCountry"].ToString();
            lblBankAddress.Text += "<br/>" + dtBuyer.Rows[0]["BankPincode"].ToString();
        }
    }
    protected void ddlPlaceOfDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(Session["BuyerId"].ToString());
        if (dtBuyer.Rows.Count > 0)
        {
            DataRow dr = dtBuyer.Rows[0];
            lblAddress.Text = dr[ddlPlaceOfDelivery.SelectedValue + "Address"].ToString().Replace("@", "<br/>");
            lblAddress.Text += "<br/>" + dr[ddlPlaceOfDelivery.SelectedValue + "City"].ToString();
            lblAddress.Text += "<br/>" + dr[ddlPlaceOfDelivery.SelectedValue + "State"].ToString();
            lblAddress.Text += "<br/>" + dr[ddlPlaceOfDelivery.SelectedValue + "Country"].ToString();
            lblDestCountry.Text = dr[ddlPlaceOfDelivery.SelectedValue + "Country"].ToString();
        }
    }
    private void BindgvReports()
    {
        gvReports.DataSource = reportObj.GetReports();
        gvReports.DataBind();
    }
    private void BindPriceTerms()
    {
        ddlPriceTerms.DataSource = OrderPriceTerms.PriceTerms.ToArray<string>();
        ddlPriceTerms.DataBind();
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string str = string.Empty;
        DataRow dr = Session["BuyerDetails"] as DataRow;
        DataTable dtnew = Session["sDtOrder"] as DataTable;
        if (dtnew.Rows.Count > 0)
        {
            lblPriceTerm.Text = dtnew.Rows[0]["FTerms"].ToString();
            lblPaymentTerms.Text = dtnew.Rows[0]["PTerms"].ToString();
        }
        bool result = true;
        int orderId = 0;
        int buyerpriceid = Convert.ToInt32(dr["BuyerPriceID"]);
        int buyertransportid = Convert.ToInt32(dr["BuyerTransportID"]);
        string freight = string.Empty;
        string[] po = Session["s_PODetails"].ToString().Split('$');
        if (!string.IsNullOrEmpty(Session["TransportMode"].ToString()))
            lblTransportmode.Text = Session["TransportMode"].ToString();
        if (Session["Order_PO"].ToString() == "1")
        {
            str = string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "Order Placed";
            result = orderObj.OrderInsert(orderId, Session["BuyerId"].ToString(),
                lblAddress.Text, OrderStatus.New, false, Convert.ToDecimal(lblTotalprice.Text),
                Convert.ToDateTime(lblDate.Text), buyerpriceid, buyertransportid, string.Empty,
                string.Empty, lblPaymentTerms.Text, lblPriceTerm.Text, lblDestCountry.Text, lblplaceofdelivery.Text,
                DateTime.Now, Session["branchID"].ToString(), string.Empty,
                !string.IsNullOrEmpty(lblPO.Text) ? lblPO.Text : lblBPO.Text, "Shaik Aslam", "Shaik Aslam",
                string.Empty, string.IsNullOrEmpty(po[3].ToString()) ? str : po[3].ToString(),
                Session["OrderType"].ToString(), string.Empty, Convert.ToInt32(lblOrganicType.Text), str,
                lblTransportmode.Text, MudarApp.Insert, ref orderId);
            if (result)
            {
                foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
                {
                    string qty = (gvr.Cells[3].Text).Replace(".00", "").Trim();
                    result = orderObj.OrderProducts_INT_UPT_DEL(orderId, Convert.ToInt32(gvr.Cells[1].Text), Convert.ToDecimal(gvr.Cells[6].Text), Convert.ToDecimal(gvr.Cells[7].Text), "Shaik Aslam", "Shaik Aslam", Convert.ToInt32(qty), Convert.ToInt32(gvr.Cells[4].Text), Convert.ToInt32(gvr.Cells[5].Text), MudarApp.Insert);
                }
            }
            if (result)
            {
                result = generatePDFforPO(orderId, lblPO.Text, po[2].ToString());
                SendmailforOrderDetails(Session["OrderType"].ToString(), orderId.ToString());
            }
            if (result)
            {
                if (Session["BuyerId"].ToString() != MudarOrderConstants.DefaultBuyerId)
                    Response.Redirect("~/buyer/Orderhistory.aspx");
                else
                {
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(orderId.ToString(), true);
                    Response.Redirect("~/Orders/BranchOrder.aspx");
                }
            }
        }
        else if (Session["Order_PO"].ToString() == "2")
        {
            str = string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "Order Placed";
            string OrderId = Session["OrderID"].ToString();
            result = orderObj.OrderInsert(Convert.ToInt32(OrderId), OrderStatus.New, Convert.ToDecimal(lblTotalprice.Text), Convert.ToDateTime(lblDate.Text), lblPaymentTerms.Text, lblPriceTerm.Text, lblplaceofdelivery.Text, string.Empty, !string.IsNullOrEmpty(lblPO.Text) ? lblPO.Text : lblBPO.Text, "Shaik Aslam", string.IsNullOrEmpty(po[3].ToString()) ? str : po[3].ToString(), Session["OrderType"].ToString(), string.Empty, lblTransportmode.Text, MudarApp.Update);
            if (result)
            {
                result = orderObj.OrderDetails_UPD(Convert.ToInt32(OrderId));
                foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
                {
                    string qty = (gvr.Cells[3].Text).Replace(".00", "").Trim();
                    result = orderObj.OrderProducts_INT_UPT_DEL(Convert.ToInt32(OrderId), Convert.ToInt32(gvr.Cells[1].Text), Convert.ToDecimal(gvr.Cells[6].Text), Convert.ToDecimal(gvr.Cells[7].Text), "Shaik Aslam", "Shaik Aslam", Convert.ToInt32(qty), Convert.ToInt32(gvr.Cells[4].Text), Convert.ToInt32(gvr.Cells[5].Text), MudarApp.Update);
                }
            }
            if (result)
            {
                result = generatePDFforPO(Convert.ToInt32(OrderId), lblPO.Text, po[2].ToString());
                SendmailforOrderDetails(Session["OrderType"].ToString(), OrderId.ToString());
            }
            if (result)
            {
                Session["dtUpdateOrder"] = null;
                Session["Order_PO"] = null;
                Response.Redirect("~/buyer/Orderhistory.aspx");
            }
        }
        else if (Session["Order_PO"].ToString() == "3")
        {
            str = string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "Order Placed";
            string OrderId = Session["OrderID"].ToString();
            result = orderObj.OrderInsert(Convert.ToInt32(OrderId), Session["BuyerId"].ToString(), lblAddress.Text, OrderStatus.New, false, Convert.ToDecimal(lblTotalprice.Text), Convert.ToDateTime(lblDate.Text), buyerpriceid, buyertransportid, string.Empty, string.Empty, lblPaymentTerms.Text, lblPriceTerm.Text, lblDestCountry.Text, lblplaceofdelivery.Text, DateTime.Now, Session["branchID"].ToString(), string.Empty, !string.IsNullOrEmpty(lblPO.Text) ? lblPO.Text : lblBPO.Text, "Shaik Aslam", "Shaik Aslam", string.Empty, string.IsNullOrEmpty(po[3].ToString()) ? str : po[3].ToString(), Session["OrderType"].ToString(), string.Empty, Convert.ToInt32(lblOrganicType.Text), str, lblTransportmode.Text, 3, ref orderId);
            if (result)
            {
                foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
                {
                    string qty = (gvr.Cells[3].Text).Replace(".00", "").Trim();
                    result = orderObj.OrderProducts_INT_UPT_DEL(orderId, Convert.ToInt32(gvr.Cells[1].Text), Convert.ToDecimal(gvr.Cells[6].Text), Convert.ToDecimal(gvr.Cells[7].Text), "Shaik Aslam", "Shaik Aslam", Convert.ToInt32(qty), Convert.ToInt32(gvr.Cells[4].Text), Convert.ToInt32(gvr.Cells[5].Text), 3);
                }
            }
            if (result)
            {
                result = generatePDFforPO(Convert.ToInt32(OrderId), lblPO.Text, po[2].ToString());
                //SendmailforOrderDetails(Session["OrderType"].ToString(), orderId.ToString());
            }
            if (result)
                Response.Redirect("~/buyer/Orderhistory.aspx");
        }
        else if (Session["Order_PO"].ToString() == "0")
        {
            //with out OP
            result = orderObj.OrderInsert(orderId, Session["BuyerId"].ToString(), lblAddress.Text, OrderStatus.New, false, Convert.ToDecimal(lblTotalprice.Text), Convert.ToDateTime(lblDate.Text), buyerpriceid, buyertransportid, string.Empty, string.Empty, lblPaymentTerms.Text, lblPriceTerm.Text, lblDestCountry.Text, lblplaceofdelivery.Text, DateTime.Now, Session["branchID"].ToString(), string.Empty, string.Empty, "Shaik Aslam", "Shaik Aslam", string.Empty, po[3].ToString(), Session["OrderType"].ToString(), LotsampleID, 0, string.Empty, lblTransportmode.Text, MudarApp.Insert, ref orderId);
            if (result)
            {
                foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
                {
                    string qty = (gvr.Cells[3].Text).Replace(".00", "").Trim();
                    result = orderObj.OrderProducts_INT_UPT_DEL(orderId, Convert.ToInt32(gvr.Cells[1].Text), Convert.ToDecimal(gvr.Cells[6].Text), Convert.ToDecimal(gvr.Cells[7].Text), "Shaik Aslam", "Shaik Aslam", Convert.ToInt32(qty), Convert.ToInt32(gvr.Cells[4].Text), Convert.ToInt32(gvr.Cells[5].Text), MudarApp.Insert);
                }
            }
            if (result)
            {
                generatePDFforLotsample(orderId, LotsampleID, "");
                SendmailforOrderDetails(Session["OrderType"].ToString(), orderId.ToString());
                Response.Redirect("~/buyer/LotSample.aspx");
            }

        }

    }
    // email purpose code
    public void SendmailforOrderDetails(string OrderType, string OrderID)
    {
        string Emailpurpose = string.Empty;
        Emailpurpose += "<table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.NO</td><td align='center'>Product Name</td><td align='center'>Quantity</td><td align='center'>Rate/KG <br/>(" + lblmoney.Text + ")</td><td align='center'>Drums<br/>(25 KG)</td><td align='center'>Drums<br/>(180 KG)</td><td align='right'>Total Amount<br/>(" + lblmoney.Text + ")</td></tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            Emailpurpose += "<tr><td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='right'>" + gvr.Cells[7].Text + "</td></tr>";
        Emailpurpose += "<tr><td colspan='4' align='center' bgcolor='#CCCC99'>Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td rowspan='2' align='right' bgcolor='#FFCC99'>&nbsp;" + lblTotalprice.Text + "</td></tr>";
        Emailpurpose += "<tr><td colspan='2' align='center' bgcolor='#CCCC99'>Total Amount in Words</td><td colspan='4'>" + lblAmountword.Text + "</td></tr></table>";
        bool result;
        result = email.SendOrderDetails(Emailpurpose, "office@mudarindia.com", OrderID + " - " + OrderType + " placed by" + " " + lblBuyerCopmany.Text + " ");
    }
    public bool generatePDFforPO(int orderid, string poid, string poPath)
    {
        string imageFilePath1 = string.Empty;
        string Msg = string.Empty;
        string Split = string.Empty;
        string buyerId = string.Empty;
        DataTable dtOrder = orderObj.GetOrderDetails(orderid);
        if (lblOrganicType.Text == "1")
        {
            Msg = "(Fair Trade)";
        }
        if (Session["Order_PO"].ToString() == "3")
        {
            DataTable dtSplitCount = orderObj.GetSplitIDCount(orderid.ToString());
            Split = ".S" + dtSplitCount.Rows[0]["Column1"].ToString();
        }
        if (dtOrder.Rows.Count > 0)
        {
            buyerId = Convert.ToString(dtOrder.Rows[0]["BuyerID"]).ToLower();
        }
        if (!string.IsNullOrEmpty(buyerId))
        {
            string fileName = buyerId + ".JPG";
            if (File.Exists(Server.MapPath("~/Attachments/BuyerLogo/" + fileName)))
            {
                imageFilePath1 = Server.MapPath("~/Attachments/BuyerLogo/" + fileName);
            }
            else if (File.Exists(Server.MapPath("~/Attachments/BuyerLogo/" + buyerId + ".PNG")))
            {
                imageFilePath1 = Server.MapPath("~/Attachments/BuyerLogo/" + buyerId + ".PNG");
            }
            else
            {
                imageFilePath1 = Server.MapPath("~/images/blank.png");
            }
        }
        else
        {
            imageFilePath1 = Server.MapPath("~/images/blank.png");
        }
        bool result = false;
        string ss = "<img src='" + imageFilePath1 + "' height='100%' width='100%' />";
        //new code start
        string strpdf = "<table align='left' style='font-family:Verdana;font-size:9px;width: 885px'><tr><td align='left'>" + ss + "</td><td align='right'>" + lblBuyerAddress.Text + "</td></tr></table>";
        strpdf += "<table align='center' style='font-family:Verdana;font-size:9px;width:885px'><tr>";
        strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6' align='center' style='font-family:Verdana;font-size:xx-large;'>Purchase Order&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;" + orderid + Split + " " + Msg + " </td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='3'><table width='100%' border='1'><tr><td align='center'rowspan='6' colspan='2'>" + txtCompanyAddress.Text + "</td></tr></table></td><td colspan='3'><table width='100%' border='1'><tr><td align='center' bgcolor='#CCCC99' colspan='2'>Purchase Order INFO</td></tr><tr align='center'><td bgcolor='#CCCC99'>System Generated PO Ref</td><td>" + orderid + "<br/></td></tr><tr align='center'><td bgcolor='#CCCC99'>Date<br/></td><td>" + lblDate.Text + "<br/></td></tr><tr><td>sss</td><td>ss</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center' rowspan='5' colspan='2'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center' bgcolor='#CCCC99' colspan='2'>Purchase Order INFO</td></tr><tr>";
        strpdf += "<td bgcolor='#CCCC99' align='center'>System PO Ref</td><td align='center'>" + orderid + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>Buyer PO</td><td></td></tr><tr align='center'><td bgcolor='#CCCC99'>Date</td><td>" + lblDate.Text + "</td></tr></table></td><tr>";
        strpdf += "<td colspan='6' align='center'>Please arrange the shipment as per the Terms & Conditions mentioned</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.NO</td><td align='center'>Product Name</td><td align='center'>Quantity</td><td align='center'>Packing<br/>(25 KG)</td><td align='center'>Packing<br/>(180 KG)</td><td align='center'>Rate/KG<br/>(" + lblmoney.Text + ")</td><td align='center'>Total Amount<br/>(" + lblmoney.Text + ")</td></tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            strpdf += "<tr><td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='right'>" + gvr.Cells[7].Text + "</td></tr><tr>";
        strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td></td><td rowspan='2' align='right' bgcolor='#FFCC99'>" + lblTotalprice.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum25.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum180.Text + "</td><td rowspan='2' align='right' bgcolor='#FFB6C1'>" + lblTotalprice.Text + "</td></tr><tr>";
        strpdf += "<td colspan='1' align='center' bgcolor='#CCCC99'>Total Amount<br/>in Words</td><td colspan='5'>&nbsp;&nbsp;&nbsp;" + lblAmountword.Text + "&nbsp;Only</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td  colspan='6'><table width='100%' border='1'><tr><td align='center'>we accept the specifications of the above products mentioned on our page on the portal</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Mode of Transport</td><td align='center' width='50%'>" + lblTransportmode.Text + "</td></tr><tr>";
        strpdf += " <td align='center' width='50%' bgcolor='#CCCC99'>Price Terms</td><td align='center' width='50%'>" + lblPriceTerm.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Payment Terms</td><td align='center' width='50%'>" + lblPaymentTerms.Text + "" + lblCreditDays.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Taxes & Duties</td><td align='center' width='50%'>" + lblTaxesDuties.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Place of Delivery</td><td align='center' width='50%'>" + lblplaceofdelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Sales Tax Terms</td><td align='center' width='50%'>" + lblSalesTaxTerms.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center' bgcolor='#CCCC99'>Buyer (Bank Details) if the documents are through Bank</td><td align='center' bgcolor='#CCCC99'>Notify / Clearing Agent at the Destination</td></tr><tr>";
        strpdf += "<td align='center'>" + lblBankAddress.Text + "</td><td align='center'>" + lblNotifyAddress.Text + " </td></tr></table></td><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='70%' align='center' bgcolor='#CCCC99'>Special Instructions</td><td width='30%' align='right' rowspan='2' >Authorized Signatory<br/><b>" + lblBuyerCopmany.Text + "</b></td></tr><tr>";
        strpdf += "<td width='70%' align='center'>" + lblComments.Text + "</td></table></td></tr><tr>";
        strpdf += "<td  colspan='6'><table width='100%' ><tr><td align='center'>!!!! System Generated   - Signature is not required !!!! </td></tr></table>";
        strpdf += "</td></tr></table>";

        // new code end

        //string strpdf = "<table align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td style='width: 885px'><table align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td colspan='6' align='center' bgcolor='#ffcc66' style='font-size: 12px;' >Mudar India Exports</td></tr><tr>";
        //strpdf += "<td colspan='6' align='center' bgcolor='#ffcc66' style='font-size: 9px;' >6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6'  align='center' style='font-family: Verdana;'>Purchase Order</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center'  rowspan='3'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center'>PO INFO</td></tr><tr>";
        //strpdf += "<td ><table width='100%'><tr><td width='50%'>PO Number</td><td width='50%'>" + lblPO.Text + "</td></tr><tr>";
        //strpdf += "<td width='50%'>Date</td><td width='50%'>" + lblDate.Text + "</td></tr><tr><td width='50%'>TIN</td><td width='50%'>" + lblTAN.Text + "</td></tr></table></td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>Details of Overseas PO received</td></tr><tr>";
        //strpdf += "<td><table width='100%'border='1'><tr><td>Overseas PO/Intent Number</td><td >" + lblOverSeasPOIntent.Text + "</td><td >Date</td><td >14 sep 2012</td><td>Destination Country</td><td>gfgfgdfgfgffffff</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>Please arrange the shipment as per the Terms & Conditions mentioned</td></tr><tr>";

        //strpdf += "<td colspan='6'><table width='100%' border='1'>";
        //strpdf += "<tr><td align='center'>S.NO</td><td align='center'>Product Name</td><td align='center'>Quantity</td><td align='center'>Packing (25 KG)</td><td align='center'>Packing (180 KG)</td><td align='center'>Price (RS) /KG</td><td align='center'>Amount in RS</td></tr>";
        //foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        //    strpdf += "<tr><td align='center'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='right'>" + gvr.Cells[7].Text + "</td></tr>";
        //strpdf += "<tr><td colspan='3' align='center'>Total Drums  :" + lblTotalDrums.Text + "</td><td align='center' colspan='4'>Total Amount :" + lblTotalprice.Text + "</td></tr>";
        //strpdf += "<tr><td colspan='7' align='left'>Amount in Words:&nbsp;" + lblAmountword.Text + " </td></tr>";
        //strpdf += "</table></td></tr><tr>";
        //strpdf += "<td colspan='6'></td></tr><tr>";
        //strpdf += "<td><table width='100%' border='1'><tr><td align='center' width='35%'>Price Terms</td><td align='center' width='35%'>sss</td><td align='center' width='30%'>Address for Delivery</td></tr><tr>";
        //strpdf += "<td align='center'>Taxes &amp; Duties</td><td align='center'>" + lblTaxesDuties.Text + "</td><td align='center' rowspan='5'>" + lblAddress.Text + "</td></tr><tr>";
        //strpdf += "<td align='center'>Mandy Tax</td><td align='center'>" + lblMandyTax.Text + "</td></tr><tr>";
        //strpdf += "<td align='center'>Mode of Transport</td><td align='center'>sss</td></tr><tr>";
        //strpdf += "<td align='center'>Place of Delivery</td><td align='center'>sss</td></tr><tr>";
        //strpdf += "<td align='center'>Sales Tax Terms</td><td align='center'>" + lblSalesTaxTerms.Text + "</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment</td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%'border='1'";
        //strpdf += "<tr><td align='center'>S.No</td><td align='center'>Report Name</td><td align='center'>Date Of Email</td></tr>";
        //foreach (GridViewRow gvr in gvReports.Rows)
        //{
        //    if ((gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked)
        //        strpdf += "<tr><td align='center' width='20' >" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtReportDate") as TextBox).Text + "</td></tr>";
        //}
        //strpdf += "</table></td></tr>";
        //strpdf += " </table>";
        //report
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + orderid.ToString() + "_" + "BU" + Split.ToString() + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + orderid.ToString() + "_" + poid.ToString() + Split.ToString() + ".pdf";

            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();

            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();

            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            //string imageFilePath = Server.MapPath("~/images/MUDAR LOGO.bmp");
            //string imageFilePath = Server.MapPath("~/Attachments/BuyerLogo/35108aea-b4a6-4cfc-9be7-c17a65d5aaef.JPG");
            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
            //jpg.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //jpg.ScaleToFit(150f, 120f);
            mypara.IndentationLeft = 36;
            mypara.InsertRange(0, htmlarraylist);
            //document.Add(jpg);
            document.Add(mypara);
            document.Close();
            if (!string.IsNullOrEmpty(poPath))
            {
                string newpath = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + orderid.ToString() + "_" + poid.ToString() : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + orderid.ToString() + "_" + poid.ToString();
                File.Move(Server.MapPath("~/Attachments/") + poPath, Server.MapPath(newpath));
                orderObj.OrderDetails_UPD(orderid, newpath, 1, "bhanu");
            }
            else
                orderObj.OrderDetails_UPD(orderid, Pdf_path, Convert.ToInt32(Session["Order_PO"].ToString()), "bhanu");
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
        return result;
    }
    public bool generatePDFforLotsample(int orderid, string poid, string poPath)
    {
        string imageFilePath1 = string.Empty;
        string buyerId = string.Empty;
        string Msg = string.Empty;
        DataTable dtOrder = orderObj.GetOrderDetails(orderid);
        if (lblOrganicType.Text == "1")
        {
            Msg = "(Fair Trade)";
        }
        if (dtOrder.Rows.Count > 0)
        {
            buyerId = Convert.ToString(dtOrder.Rows[0]["BuyerID"]).ToLower();
        }
        if (!string.IsNullOrEmpty(buyerId))
        {
            string fileName = buyerId + ".JPG";
            if (File.Exists(Server.MapPath("~/Attachments/BuyerLogo/" + fileName)))
            {
                imageFilePath1 = Server.MapPath("~/Attachments/BuyerLogo/" + fileName);
            }
            else if (File.Exists(Server.MapPath("~/Attachments/BuyerLogo/" + buyerId + ".PNG")))
            {
                imageFilePath1 = Server.MapPath("~/Attachments/BuyerLogo/" + buyerId + ".PNG");
            }
            else
            {
                imageFilePath1 = Server.MapPath("~/images/blank.png");
            }
        }
        else
        {
            imageFilePath1 = Server.MapPath("~/images/blank.png");
        }
        bool result = false;
        string ss = "<img src='" + imageFilePath1 + "' height='100%' width='100%' />";
        //new code start
        string strpdf = "<table align='left' style='font-family:Verdana;font-size:9px;width: 885px'><tr><td align='left'>" + ss + "</td><td align='right'>" + lblBuyerAddress.Text + "</td></tr></table>";
        strpdf += "<table align='center' style='font-family:Verdana;font-size:9px;width:885px'><tr>";
        strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6' align='center' style='font-family:Verdana;font-size:xx-large;'>Lot Sample&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;" + orderid + " " + Msg + " </td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center' rowspan='5' colspan='2'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center' bgcolor='#CCCC99' colspan='2'>Lot Sample(LS)&nbsp;&nbsp;INFO</td></tr><tr align='center'>";
        strpdf += "<td bgcolor='#CCCC99'>System LS Ref</td><td>" + orderid + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>Buyer PO</td><td></td></tr><tr align='center'><td bgcolor='#CCCC99'>Date</td><td>" + lblDate.Text + "</td></tr></table></td><tr>";
        strpdf += "<td colspan='6' align='center'>Pl.arrange to send the following LOT SAMPLES to Analyze and CONFIRM the PO</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.NO</td><td align='center'>Product Name</td><td align='center'>Quantity</td><td align='center'>Packing<br/>(25 KG)</td><td align='center'>Packing<br/>(180 KG)</td><td align='center'>Rate/KG<br/>(" + lblmoney.Text + ")</td><td align='center'>Total Amount<br/>(" + lblmoney.Text + ")</td></tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            strpdf += "<tr><td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='right'>" + gvr.Cells[7].Text + "</td></tr><tr>";
        strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td></td><td rowspan='2' align='right' bgcolor='#FFCC99'>" + lblTotalprice.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum25.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum180.Text + "</td><td rowspan='2' align='right' bgcolor='#FFB6C1'>" + lblTotalprice.Text + "</td></tr><tr>";
        strpdf += "<td colspan='1' align='center' bgcolor='#CCCC99'>Total Amount<br/>in Words</td><td colspan='5'>&nbsp;" + lblAmountword.Text + "&nbsp;Only</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Mode of Transport</td><td align='center' width='50%'>" + lblTransportmode.Text + "</td></tr><tr>";
        strpdf += " <td align='center' width='50%' bgcolor='#CCCC99'>Price Terms</td><td align='center' width='50%'>" + lblPriceTerm.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Payment Terms</td><td align='center' width='50%'>" + lblPaymentTerms.Text + "" + lblCreditDays.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Taxes & Duties</td><td align='center' width='50%'>" + lblTaxesDuties.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Place of Delivery</td><td align='center' width='50%'>" + lblplaceofdelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%' bgcolor='#CCCC99'>Sales Tax Terms</td><td align='center' width='50%'>" + lblSalesTaxTerms.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center' bgcolor='#CCCC99'>Buyer (Bank Details) if the documents are through Bank</td><td align='center' bgcolor='#CCCC99'>Notify / Clearing Agent at the Destination</td></tr><tr>";
        strpdf += "<td align='center'>" + lblBankAddress.Text + "</td><td align='center'>" + lblNotifyAddress.Text + " </td></tr></table></td><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='70%' align='center' bgcolor='#CCCC99'>Special Instructions</td><td width='30%' align='right' rowspan='2' >Authorized Signatory<br/><b>" + lblBuyerCopmany.Text + "</b></td></tr><tr>";
        strpdf += "<td width='70%' align='center'>" + lblComments.Text + "</td></table></td></tr><tr>";
        strpdf += "<td  colspan='6'><table width='100%' ><tr><td align='center'>!!!! System Generated   - Signature is not required !!!! </td></tr></table>";
        strpdf += "</td></tr></table>";

        // new code end

        //string strpdf = "<table align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td style='width: 885px'><table align='center' style='font-family: Verdana; width: 885px'><tr>";
        //strpdf += "<td colspan='6' align='center' bgcolor='#ffcc66' style='font-size: 12px;' >Mudar India Exports</td></tr><tr>";
        //strpdf += "<td colspan='6' align='center' bgcolor='#ffcc66' style='font-size: 9px;' >6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6'  align='center' style='font-family: Verdana;'>Purchase Order</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center'  rowspan='3'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center'>PO INFO</td></tr><tr>";
        //strpdf += "<td ><table width='100%'><tr><td width='50%'>PO Number</td><td width='50%'>" + lblPO.Text + "</td></tr><tr>";
        //strpdf += "<td width='50%'>Date</td><td width='50%'>" + lblDate.Text + "</td></tr><tr><td width='50%'>TIN</td><td width='50%'>" + lblTAN.Text + "</td></tr></table></td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>Details of Overseas PO received</td></tr><tr>";
        //strpdf += "<td><table width='100%'border='1'><tr><td>Overseas PO/Intent Number</td><td >" + lblOverSeasPOIntent.Text + "</td><td >Date</td><td >14 sep 2012</td><td>Destination Country</td><td>gfgfgdfgfgffffff</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>Please arrange the shipment as per the Terms & Conditions mentioned</td></tr><tr>";

        //strpdf += "<td colspan='6'><table width='100%' border='1'>";
        //strpdf += "<tr><td align='center'>S.NO</td><td align='center'>Product Name</td><td align='center'>Quantity</td><td align='center'>Packing (25 KG)</td><td align='center'>Packing (180 KG)</td><td align='center'>Price (RS) /KG</td><td align='center'>Amount in RS</td></tr>";
        //foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        //    strpdf += "<tr><td align='center'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='right'>" + gvr.Cells[7].Text + "</td></tr>";
        //strpdf += "<tr><td colspan='3' align='center'>Total Drums  :" + lblTotalDrums.Text + "</td><td align='center' colspan='4'>Total Amount :" + lblTotalprice.Text + "</td></tr>";
        //strpdf += "<tr><td colspan='7' align='left'>Amount in Words:&nbsp;" + lblAmountword.Text + " </td></tr>";
        //strpdf += "</table></td></tr><tr>";
        //strpdf += "<td colspan='6'></td></tr><tr>";
        //strpdf += "<td><table width='100%' border='1'><tr><td align='center' width='35%'>Price Terms</td><td align='center' width='35%'>sss</td><td align='center' width='30%'>Address for Delivery</td></tr><tr>";
        //strpdf += "<td align='center'>Taxes &amp; Duties</td><td align='center'>" + lblTaxesDuties.Text + "</td><td align='center' rowspan='5'>" + lblAddress.Text + "</td></tr><tr>";
        //strpdf += "<td align='center'>Mandy Tax</td><td align='center'>" + lblMandyTax.Text + "</td></tr><tr>";
        //strpdf += "<td align='center'>Mode of Transport</td><td align='center'>sss</td></tr><tr>";
        //strpdf += "<td align='center'>Place of Delivery</td><td align='center'>sss</td></tr><tr>";
        //strpdf += "<td align='center'>Sales Tax Terms</td><td align='center'>" + lblSalesTaxTerms.Text + "</td></tr></table></td></tr><tr>";
        //strpdf += "<td colspan='6' align='center'>E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment</td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%'border='1'";
        //strpdf += "<tr><td align='center'>S.No</td><td align='center'>Report Name</td><td align='center'>Date Of Email</td></tr>";
        //foreach (GridViewRow gvr in gvReports.Rows)
        //{
        //    if ((gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked)
        //        strpdf += "<tr><td align='center' width='20' >" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtReportDate") as TextBox).Text + "</td></tr>";
        //}
        //strpdf += "</table></td></tr>";
        //strpdf += " </table>";
        //report
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + orderid.ToString() + "_" + "LS" + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + orderid.ToString() + "_" + poid.ToString() + ".pdf";
            orderObj.OrderDetails_UPD(orderid, Pdf_path, 2, "bhanu");
            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();

            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();

            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            //string imageFilePath = Server.MapPath("~/images/MUDAR LOGO.bmp");
            //string imageFilePath = Server.MapPath("~/Attachments/BuyerLogo/35108aea-b4a6-4cfc-9be7-c17a65d5aaef.JPG");
            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
            //jpg.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //jpg.ScaleToFit(150f, 120f);
            mypara.IndentationLeft = 36;
            mypara.InsertRange(0, htmlarraylist);
            //document.Add(jpg);
            document.Add(mypara);
            document.Close();
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
        return result;
    }
}
