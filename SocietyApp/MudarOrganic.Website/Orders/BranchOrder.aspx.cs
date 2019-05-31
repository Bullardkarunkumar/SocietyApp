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
using System.Web.Configuration;
using System.IO;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Orders_BranchOrder : System.Web.UI.Page
{
    BranchsRolesEmployees_BL bre = new BranchsRolesEmployees_BL();
    Order_BL orderobj = new Order_BL();
    CustomAgent_BL customagentobj = new CustomAgent_BL();
    MudarApp objApp = new MudarApp();
    Reports_BL reportObj = new Reports_BL();
    MudarUser mu = new MudarUser();
    Product_BL pr = new Product_BL();
    ProductPriceUpdate_BL ppu = new ProductPriceUpdate_BL();
    Supplier_BL supplierObj = new Supplier_BL();
    DataTable dtOrder = new DataTable();
    Emailtest email = new Emailtest();
    protected void Page_Load(object sender, EventArgs e)
    {
        string QorderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        if (!IsPostBack)
        {
            dtOrder = orderobj.OrderList(Convert.ToInt32(QorderID));
            if (dtOrder.Rows.Count > 0)
            {
                DataRow dr = dtOrder.Rows[0];
                if (dr["OrgType"].ToString() == "1")
                    lblOrgType.Text = "FT";
                else
                    lblOrgType.Text = string.Empty;

                Session["PurchaseOrderID"] = dr["PurchaseOrderID"].ToString();
                Session["PODate"] = dtOrder.Rows[0]["OrderDate"].ToString();
            }
            //lblBPO.Text = objApp.GenerateBPOno();
            lblBPO.Text = QorderID;
            DateTime Tdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            lblDate.Text = string.Format("{0:dd MMM yyyy}", Tdate);
            lblPO.Text = Session["PurchaseOrderID"].ToString();
            DateTime date = Convert.ToDateTime(Session["PODate"].ToString());
            lblPODate.Text = string.Format("{0:dd MMM yyyy}", date);
            BindSupplierList(rblBranchSupplier.SelectedValue);
            //lblPO.Text = "LS.2";
            BindProductDetails();
            BindDelivaryDetails();
            BindgvReports();
            BindCustomAgent();
            BindBranchAddress();
        }
    }
    protected void ddlSelectBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCompanyAddress.Text = string.Empty;
        if (ddlSelectBranch.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            if (rblBranchSupplier.SelectedValue == "1")
            {
                dt = supplierObj.GetSupplierDetails(ddlSelectBranch.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    lblPayTer.Text = dt.Rows[0]["PaymentTerm"].ToString();
                    if (Convert.ToBoolean(dt.Rows[0]["Exworks"].ToString()) == true)
                        lblpriceterm.Text = "Ex-works";
                    if (lblDestCountry.Text == "INDIA")
                    {
                        lblSalesTax.Text = "against form" + " 'C' ";
                        lblTax.Text = "as applicable";
                    }
                    else
                    {
                        lblSalesTax.Text = "against form" + " 'H' ";
                        lblTax.Text = "not applicable";
                    }
                    lblTIN.Text = "37770277330";// dt.Rows[0]["Tin"].ToString();
                    txtCompanyAddress.Text = dt.Rows[0]["SupplierCompanyName"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CAddress"].ToString().Replace("@", "<br/>");
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CCity"].ToString() + "-" + dt.Rows[0]["CPincode"].ToString();
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CPincode"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CState"].ToString();
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CCountry"].ToString();

                    //lblSalesTax.Text = "against form" + " 'F' ";
                    //txtCompanyAddress.Text = dt.Rows[0]["BranchCode"].ToString();
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["Address"].ToString().Replace("@", "<br/>");
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["City"].ToString();
                    ////txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["Taluk"].ToString();
                    ////txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["District"].ToString();
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["State"].ToString();
                    //txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["Country"].ToString();
                    //lblOrgPreimum.Text = dt.Rows[0]["Organic_Premium"].ToString();
                }
            }
            else if (rblBranchSupplier.SelectedValue == "2")
            {
                dt = supplierObj.GetSupplierDetails(ddlSelectBranch.SelectedValue);
                lblSalesTax.Text = "against form" + " 'C' ";
                if (dt.Rows.Count > 0)
                {
                    lblpriceterm.Text = dt.Rows[0]["CCity"].ToString();
                    txtCompanyAddress.Text = dt.Rows[0]["SupplierCompanyName"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CAddress"].ToString().Replace("@", "<br/>");
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CCity"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CState"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CCountry"].ToString();
                    txtCompanyAddress.Text += "<br/>" + dt.Rows[0]["CPincode"].ToString();
                }
            }
        }
    }
    protected void ddlMandyTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMandyTax.Text = string.Empty;
        if (ddlMandyTax.SelectedValue == "0")
            Response.Write("<script>alert('plz select the mandy tax')</script>");
        else
            lblMandyTax.Text = ddlMandyTax.SelectedItem.Text;
    }
    protected void ddlModeofTransport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPlaceOfDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        bool result = true;
        int BranchOrderID = 0;
        int productID = 0;
        decimal TotalNetQuantity = 0;
        decimal NetQuantity = 0;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string comment = string.Empty;
        string OrderToID = string.Empty;
        int OrderTo = 1;
        if (ddlSelectBranch.SelectedIndex > 0)
        {
            if (rblBranchSupplier.SelectedValue == "1")
                OrderTo = 1;
            else if (rblBranchSupplier.SelectedValue == "2")
                OrderTo = 2;
            OrderToID = ddlSelectBranch.SelectedValue;
            comment = "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "INPROCESS";
            result = orderobj.BranchOrderInsert(Convert.ToInt32(OrderID), Convert.ToDecimal(lblTotalprice.Text), BranchOrderStatus.New, lblBPO.Text, Convert.ToDateTime(lblDate.Text), "Bhanu", string.Empty, comment, MudarApp.Insert, ref BranchOrderID, OrderTo, OrderToID);
            if (result)
            {
                decimal TotalPriceAmt = 0;
                foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
                {
                    var lblQuantity = gvr.FindControl("lblQuantity") as Label;
                    var lblPacking25 = gvr.FindControl("lblPacking25") as Label;
                    var lblPacking180 = gvr.FindControl("lblPacking180") as Label;
                    var lblPOUPPrice = gvr.FindControl("lblPOUPPrice") as Label;
                    var lblTotalPriceAmount = gvr.FindControl("lblTotalPriceAmount") as Label;
                    var lblProductID = gvr.FindControl("lblProductID") as Label;
                    (gvr.Cells[7].FindControl("lblTotalPriceAmount") as Label).Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblQuantity.Text) * Convert.ToDecimal(lblPOUPPrice.Text)));
                    productID = Convert.ToInt32(lblProductID.Text);
                    NetQuantity = Convert.ToDecimal(lblQuantity.Text);
                    TotalNetQuantity = TotalNetQuantity + NetQuantity;
                    result = orderobj.BranchOrderProduct_INSandUPDandDEL(BranchOrderID, productID, NetQuantity, 0, Convert.ToDecimal(lblTotalPriceAmount.Text), Convert.ToDecimal(lblPOUPPrice.Text), "Bhanu", string.Empty, MudarApp.Insert);
                    TotalPriceAmt += Convert.ToDecimal(lblTotalPriceAmount.Text);
                }
                result = orderobj.OrderDetails_UPD(Convert.ToInt32(OrderID), OrderStatus.Inprocess, "Bhanu", string.Empty);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Please select Branch/Supplier to place Order THanks...!');</script>");
            return;
        }
        if (result)
        {
            dtOrder = orderobj.OrderList(Convert.ToInt32(OrderID));
            if (dtOrder.Rows.Count > 0)
            {
                DataRow dr = dtOrder.Rows[0];
                if (dr["OrderType"].ToString() != "order")
                {
                    generatePDFforLotsample(BranchOrderID, OrderID);
                    SendmailforOrderDetails(dr["OrderType"].ToString(), OrderID.ToString());
                }
                else
                {
                    generatePDFforPO(BranchOrderID, lblBPO.Text);
                    SendmailforOrderDetails(dr["OrderType"].ToString(), OrderID.ToString());
                }
            }
            Response.Redirect("../mudar/UpdateAdminOrder.aspx");
        }
    }
    public void SendmailforOrderDetails(string OrderType, string OrderID)
    {
        bool result;
        string Emailpurpose = string.Empty;
        Emailpurpose += "<table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.No</td><td align='center'>Product Name</td><td align='center'>Quantity<br/>(KG)</td><td align='center'>Packing<br/>(25 KG)</td><td align='center'>Packing <br/>(180 KG)</td><td align='center'>Price/KG<br/>(INR)</td><td align='center'>Amount<br/>(INR)</td></tr><tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        {
            var lblQuantity = gvr.FindControl("lblQuantity") as Label;
            var lblPacking25 = gvr.FindControl("lblPacking25") as Label;
            var lblPacking180 = gvr.FindControl("lblPacking180") as Label;
            var lblPOUPPrice = gvr.FindControl("lblPOUPPrice") as Label;
            var lblTotalPriceAmount = gvr.FindControl("lblTotalPriceAmount") as Label;
            Emailpurpose += "<td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><br/><td align='center'>" + gvr.Cells[2].Text + "</td><br/><td align='center'>" + lblQuantity.Text + "</td><br/><td align='center'>" + lblPacking25.Text + "</td><br/><td align='center'>" + lblPacking180.Text + "</td><br/><td align='center'>" + lblPOUPPrice.Text + "</td><br/><td align='right'>" + lblTotalPriceAmount.Text + "</td><br/></tr><tr>";
        }
        Emailpurpose += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td></td><td rowspan='2' align='right' bgcolor='#FFCC99'>" + lblTotalprice.Text + "</td></tr><tr>";
        Emailpurpose += "<td colspan='2' align='center' bgcolor='#CCCC99'>Total Amount in Words</td><td colspan='4'>&nbsp;" + lblAmount_word.Text + "&nbsp;Only</td></tr></table>";
        result = email.SendOrderDetails(Emailpurpose, "office@mudarindia.com", OrderID + " - " + OrderType + " " + "for UP Branch");
    }
    private void BindBranchDetailsList()
    {
        if (ddlSelectBranch.Items.Count > 0)
            ddlSelectBranch.Items.Clear();
        ddlSelectBranch.DataSource = bre.BranchDetails();
        ddlSelectBranch.DataTextField = "BranchCode";
        ddlSelectBranch.DataValueField = "BranchId";
        ddlSelectBranch.DataBind();
        ddlSelectBranch.Items.Insert(0, MudarApp.AddListItem("Select", "0"));
    }
    private void BindSupplierList(string ics)
    {
        if (ddlSelectBranch.Items.Count > 0)
            ddlSelectBranch.Items.Clear();
        ddlSelectBranch.DataSource = supplierObj.GetIcsandNonSuppliers(ics == "1" ? true : false);
        ddlSelectBranch.DataTextField = "SupplierCompanyName";
        ddlSelectBranch.DataValueField = "SupplierID";
        ddlSelectBranch.DataBind();

        ddlSelectBranch.Items.Insert(0, MudarApp.AddListItem("Select", "0"));
    }
    private void BindCustomAgent()
    {
        DataTable dt = new DataTable();
        dt = customagentobj.GetAgentDetails();
        if (dt.Rows.Count > 0)
        {
            if (ddlCustomAgent.Items.Count > 0)
                ddlCustomAgent.Items.Clear();
            ddlCustomAgent.DataSource = customagentobj.GetAgentDetails();
            ddlCustomAgent.DataTextField = "AgentCode";
            ddlCustomAgent.DataValueField = "CustomAgentId";
            ddlCustomAgent.DataBind();
            ddlCustomAgent.Items.Insert(0, MudarApp.AddListItem("Select", "0"));
        }
    }
    private void BindProductDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dt = new DataTable();
        dt = orderobj.OrderProduct_BPO(Convert.ToInt32(OrderID));
        if (dt.Rows.Count > 0)
        {
            gvPurchaseOrder.DataSource = dt;
            gvPurchaseOrder.DataBind();
        }
        decimal totalamount = 0, totaldrum = 0, tDrums25 = 0, tDrums180 = 0; ;
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        {
            var lblQuantity = gvr.FindControl("lblQuantity") as Label;
            var lblPacking25 = gvr.FindControl("lblPacking25") as Label;
            var lblPacking180 = gvr.FindControl("lblPacking180") as Label;
            var lblPOUPPrice = gvr.FindControl("lblPOUPPrice") as Label;
            var lblTotalPriceAmount = gvr.FindControl("lblTotalPriceAmount") as Label;
            lblTotalPriceAmount.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblQuantity.Text) * Convert.ToDecimal(lblPOUPPrice.Text)));
            totalamount += Convert.ToDecimal(lblTotalPriceAmount.Text);
            tDrums25 += Convert.ToDecimal(lblPacking25.Text);
            tDrums180 += Convert.ToDecimal(lblPacking180.Text);
        }
        lblTotalprice.Text = totalamount.ToString();
        lblTotalDrums.Text = totaldrum.ToString();
        lblDrum25.Text = tDrums25.ToString();
        lblDrum180.Text = tDrums180.ToString();
        lblTotalprice.Text = string.Format("{0:0.00}", totalamount);
        string[] word = lblTotalprice.Text.Split('.');
        string main = NumberToWords(Convert.ToInt32(word[0].ToString()));
        lblAmount_word.Text += main + ' ' + "Rupees";
        if (Convert.ToInt32(word[1].ToString()) > 0)
        {
            string sub = NumberToWords(Convert.ToInt32(word[1].ToString()));
            lblAmount_word.Text += ' ' + " and " + ' ' + sub + ' ' + "Paise";
        }
    }
    public static string NumberToWords(int number)
    {
        int inputNo = number;

        if (inputNo == 0)
            return "Zero";
        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }
        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs
        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }
    private void BindDelivaryDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtDelivaryDetails = new DataTable();
        dtDelivaryDetails = orderobj.OrderList(Convert.ToInt32(OrderID));
        if (dtDelivaryDetails.Rows.Count > 0)
        {
            lblDestCountry.Text = dtDelivaryDetails.Rows[0]["CCountry"].ToString();
        }
    }
    private void BindBranchAddress()
    {
        DataTable dtBranch = bre.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            Session["branchID"] = dtBranch.Rows[0]["branchID"].ToString();
            lblMainAddress.Text = dtBranch.Rows[0]["Bname"].ToString();
            lblMainAddress.Text += "<br/>" + dtBranch.Rows[0]["Address"].ToString();
            lblMainAddress.Text += "<br/>" + dtBranch.Rows[0]["City"].ToString();
            lblMainAddress.Text += "<br/>" + dtBranch.Rows[0]["State"].ToString();
            lblMainAddress.Text += "<br/>" + dtBranch.Rows[0]["Country"].ToString();
        }
    }
    private void BindgvReports()
    {
        gvReports.DataSource = reportObj.GetReports();
        gvReports.DataBind();
        foreach (GridViewRow gvr in gvReports.Rows)
        {
            (gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked = true;
        }
    }
    public void generatePDFforPO(int Borderid, string Bpoid)
    {
        string ID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string imageFilePath1 = Server.MapPath("~/images/logo2.jpg");
        string ss = "<img src='" + imageFilePath1 + "' height='100%' width='100%' />";
        string strpdf = "<table align='left' style='font-family:Verdana;font-size:9px;width: 885px'><tr><td align='left'>" + ss + "</td><td align='right'>" + lblMainAddress.Text + "</td></tr></table>";
        strpdf += "<table align='center' style='font-family:Verdana;font-size:9px;width:885px'><tr>";
        strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6' align='center' style='font-family:Verdana;font-size:xx-large;'>Purchase Order  -  " + ID + "  " + lblOrgType.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center' rowspan='5' colspan='2'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center' bgcolor='#CCCC99' colspan='2'>Purchase Order INFO</td></tr><tr align='center'>";
        strpdf += "<td bgcolor='#CCCC99'>PO Number</td><td>" + lblBPO.Text + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>Date</td><td>" + lblDate.Text + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>TIN</td><td>" + lblTIN.Text + "</td></tr></table></td><tr>";
        strpdf += "<td colspan='6' align='center'>Details of Overseas PO received</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%'border='1'><tr align='center'><td bgcolor='#CCCC99'>Overseas PO/<br />Intent Number</td><td align='center'>" + lblPO.Text + "</td><td align='center' bgcolor='#CCCC99'>Date</td><td align='center'>" + lblPODate.Text + "</td><td style='width:90px' bgcolor='#CCCC99'>Destination Country</td><td>" + lblDestCountry.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6' align='center'>Please arrange the shipment as per the Terms & Conditions mentioned</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.No</td><td align='center'>Product Name</td><td align='center'>Quantity<br/>(KG)</td><td align='center'>Packing<br/>(25 KG)</td><td align='center'>Packing <br/>(180 KG)</td><td align='center'>Price/KG<br/>(INR)</td><td align='center'>Amount<br/>(INR)</td></tr><tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        {
            var lblQuantity = gvr.FindControl("lblQuantity") as Label;
            var lblPacking25 = gvr.FindControl("lblPacking25") as Label;
            var lblPacking180 = gvr.FindControl("lblPacking180") as Label;
            var lblPOUPPrice = gvr.FindControl("lblPOUPPrice") as Label;
            var lblTotalPriceAmount = gvr.FindControl("lblTotalPriceAmount") as Label;
            strpdf += "<td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><br/><td align='center'>" + gvr.Cells[2].Text + "</td><br/><td align='center'>" + lblQuantity.Text + "</td><br/><td align='center'>" + lblPacking25.Text + "</td><br/><td align='center'>" + lblPacking180.Text + "</td><br/><td align='center'>" + lblPOUPPrice.Text + "</td><br/><td align='right'>" + lblTotalPriceAmount.Text + "</td><br/></tr><tr>";
        }
        strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td></td><td rowspan='2' align='right' bgcolor='#FFCC99'>" + lblTotalprice.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum25.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum180.Text + "</td><td rowspan='2' align='right' bgcolor='#FFB6C1'>" + lblTotalprice.Text + "</td></tr><tr>";    
        strpdf += "<td colspan='2' align='center' bgcolor='#CCCC99'>Total Amount in Words</td><td colspan='4'>&nbsp;" + lblAmount_word.Text + "&nbsp;Only</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center'>Organic Premium @ Rs " + lblOrgPreimum.Text + " / Kg will be transferred to the societies handling ICS </td></tr></td></tr></table><tr>";

        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center' width='35%' bgcolor='#CCCC99'>Price Terms</td><td align='center' width='35%'>" + lblpriceterm.Text + "</td><td align='center' width='30%' bgcolor='#CCCC99'>Address for Delivery</td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Payment Terms</td><td align='center'> " + lblPayTer.Text + "</td><td align='center' rowspan='6'> " + lblAddressDelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Additional Taxes & Duties</td><td align='center'>" + lblTax.Text + " </td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Additional Mandy Tax</td><td align='center'>" + lblMandyTax.Text + "</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Mode of Transport</td><td align='center'>convenient mode</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Place of Delivery</td><td align='center'>" + lblplacedelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Sales Tax Terms</td><td align='center'>" + lblSalesTax.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6' align='center'>E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment</td></tr><tr>";
        strpdf += "<td colspan='4' align='left'><table width='100%'border='1'><tr bgcolor='#CCCC99'><td align='center'>S.No</td><td align='center'>Report&nbsp; Name</td><td align='center'>Date Of Email</td></tr><tr>";
        foreach (GridViewRow gvr in gvReports.Rows)
        {
            if ((gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked)
                strpdf += "<td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtReportDate") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td><td colspan='2' align='right'><table align='right'><tr><td><br/><br/><br/><br/><h5>Authorized Signatory<br/>&nbsp;<b>Mudar India Exports</b></h5></td></tr></table></td></tr></table>";

        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true),
                MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true) + "/" + Borderid.ToString() + "_" + "BR" + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + Borderid.ToString() + "_" + Bpoid.ToString() + ".pdf";

            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();

            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();


            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);


            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 36;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();
            orderobj.BranchOrderDetails_UPD(Borderid, Pdf_path, 1, "bhanu");
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

    protected void rblBranchSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCompanyAddress.Text = string.Empty;
        //if (rblBranchSupplier.SelectedValue == "1")
        //    BindBranchDetailsList();
        //else if (rblBranchSupplier.SelectedValue == "2")
        BindSupplierList(rblBranchSupplier.SelectedValue);
    }
    public void generatePDFforLotsample(int Borderid, string Bpoid)
    {
        string ID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string imageFilePath1 = Server.MapPath("~/images/MUDAR LOGO.bmp");
        string ss = "<img src='" + imageFilePath1 + "' height='100%' width='100%'/>";
        string strpdf = "<table align='left' style='font-family:Verdana;font-size:9px;width: 885px'><tr><td align='left'>" + ss + "</td><td align='right'>" + lblMainAddress.Text + "</td></tr></table>";
        strpdf += "<table align='center' style='font-family:Verdana;font-size:9px;width:885px'><tr>";
        strpdf += "<td colspan='6'><table align='center' style='font-family: Verdana; width: 885px'><tr><td colspan='6' align='center' style='font-family:Verdana;font-size:xx-large;'>Lot Sample - " + ID + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td width='50%' align='center' rowspan='5' colspan='2'>" + txtCompanyAddress.Text + "</td><td width='50%' align='center' bgcolor='#CCCC99' colspan='2'>Lot Sample INFO</td></tr><tr align='center'>";
        strpdf += "<td bgcolor='#CCCC99'>Lot Sample ID</td><td>" + Bpoid + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>Date</td><td>" + lblDate.Text + "</td></tr><tr align='center'><td bgcolor='#CCCC99'>TIN</td><td>" + lblTIN.Text + "</td></tr></table></td><tr>";
        strpdf += "<td colspan='6' align='center'>Details of Overseas PO received</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%'border='1'><tr align='center'><td bgcolor='#CCCC99'>Overseas PO/<br />Intent Number</td><td align='center'>" + lblPO.Text + "</td><td align='center' bgcolor='#CCCC99'>Date</td><td align='center'>" + lblPODate.Text + "</td><td style='width:90px' bgcolor='#CCCC99'>Destination Country</td><td>" + lblDestCountry.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6' align='center'>Please arrange the shipment as per the Terms & Conditions mentioned</td></tr><tr>";
        strpdf += "<td colspan='6'><table width='100%' border='1'><tr bgcolor='#CCCC99'><td align='center' style='width:20px'>S.No</td><td align='center'>Product Name</td><td align='center'>Quantity<br/>(KG)</td><td align='center'>Packing<br/>(25 KG)</td><td align='center'>Packing <br/>(180 KG)</td><td align='center'>Price/KG<br/>(INR)</td><td align='center'>Amount<br/>(INR)</td></tr><tr>";
        foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
            strpdf += "<td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsnnumber") as Label).Text + "</td><br/><td align='center'>" + gvr.Cells[2].Text + "</td><br/><td align='center'>" + gvr.Cells[3].Text + "</td><br/><td align='center'>" + gvr.Cells[4].Text + "</td><br/><td align='center'>" + gvr.Cells[5].Text + "</td><br/><td align='center'>" + gvr.Cells[6].Text + "</td><br/><td align='right'>" + (gvr.Cells[7].FindControl("lblTotalPriceAmount") as Label).Text + "</td><br/></tr><tr>";
        strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td align='center'>" + lblDrum25.Text + "</td><td align='center'>" + lblDrum180.Text + "</td><td></td><td rowspan='2' align='right' bgcolor='#FFCC99'>" + lblTotalprice.Text + "</td></tr><tr>";
        //strpdf += "<td colspan='3' align='center' bgcolor='#CCCC99'>&nbsp;Total No of Drums </td><td colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum25.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + lblDrum180.Text + "</td><td rowspan='2' align='right' bgcolor='#FFB6C1'>" + lblTotalprice.Text + "</td></tr><tr>";    
        strpdf += "<td colspan='2' align='center' bgcolor='#CCCC99'>Total Amount in Words</td><td colspan='4'>&nbsp;" + lblAmount_word.Text + "&nbsp;Only</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6'></td></tr><tr>";
        //strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center'>Organic Premium @ Rs " + lblOrgPreimum.Text + " / Kg will be transferred to the societies handling ICS </td></tr></td></tr></table><tr>";

        strpdf += "<td colspan='6'><table width='100%' border='1'><tr><td align='center' width='35%' bgcolor='#CCCC99'>Price Terms</td><td align='center' width='35%'>" + lblpriceterm.Text + "</td><td align='center' width='30%' bgcolor='#CCCC99'>Address for Delivery</td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Payment Terms</td><td align='center'> " + lblPayTer.Text + "</td><td align='center' rowspan='6'> " + lblAddressDelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Additional Taxes & Duties</td><td align='center'>" + lblTax.Text + " </td></tr><tr>";
        strpdf += "<td align='center'bgcolor='#CCCC99'>Additional Mandy Tax</td><td align='center'>" + lblMandyTax.Text + "</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Mode of Transport</td><td align='center'>convenient mode</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Place of Delivery</td><td align='center'>" + lblplacedelivery.Text + "</td></tr><tr>";
        strpdf += "<td align='center' bgcolor='#CCCC99'>Sales Tax Terms</td><td align='center'>" + lblSalesTax.Text + "</td></tr></table></td></tr><tr>";
        strpdf += "<td colspan='6' align='center'>E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment</td></tr><tr>";
        strpdf += "<td colspan='4' align='left'><table width='100%'border='1'><tr bgcolor='#CCCC99'><td align='center'>S.No</td><td align='center'>Report&nbsp; Name</td><td align='center'>Date Of Email</td></tr><tr>";
        foreach (GridViewRow gvr in gvReports.Rows)
        {
            if ((gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked)
                strpdf += "<td align='center' style='width:20px'>" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtReportDate") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td><td colspan='2' align='right'><table align='right'><tr><td><br/><br/><br/><br/><h5>Authorized Signatory<br/>&nbsp;<b>Mudar India Exports</b></h5></td></tr></table></td></tr></table>";

        //old code
        //string strpdf = "<table border='1'><tr>";
        //strpdf += "<td colspan='3' align='center'>Purchase Order</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td rowspan='4'  style='width:321px; height:auto'>Mudar India Exports<br />"+txtCompanyAddress.Text+"</td>";
        //strpdf += "<td style='width: 154px; height:auto'>PurchaseOrder#</td>";
        //strpdf += "<td style='width: 238px; height:auto'>" + lblPO.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td style='width:321px; height:auto'>Date</td>";
        //strpdf += "<td style='width: 238px; height:auto'>" + lblDate.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td style='width:321px; height:auto'>TIN#</td>";
        //strpdf += "<td style='width: 238px; height:auto'>" + lblTIN.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td style='width:321px; height:auto'>PO#</td>";
        //strpdf += "<td style='width: 238px; height:auto'>" + lblPO.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td style='width:321px; height:auto'>Select Branch</td>";
        //strpdf += "<td style='width: 238px; height:auto'>" + ddlSelectBranch.SelectedItem.Text +"</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td rowspan='3'  style='width:321px; height:auto'>Details of Overseas <br />Purchase Order received</td>";
        //strpdf += "<td style='width: 154px; height:auto'>Overseas PurchaseOrder/Intent Number</td>";
        //strpdf += "<td>" + lblOverSeasPOIntent.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td>Date</td>";
        //strpdf += "<td>" + lblOverSeaDate.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td>Destination Country</td>";
        //strpdf += "<td>" + lblDestCountry.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td colspan='3' align='center'>Please arrange the shipment as per the Terms &amp; Conditions mentioned</td>";
        //strpdf += "</tr><tr >";
        //strpdf += "<td colspan='3'><table border='0'>";
        //strpdf += "<tr><td>Product ID</td><td>Product Name</td><td>Quantity</td><td>Packing (25 KG)</td><td>Packing (180 KG)</td><td>Price (RS) /KG</td><td>Total Amount (RS)</td></tr>";
        //foreach (GridViewRow gvr in gvPurchaseOrder.Rows)
        //    strpdf += "<tr><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td><td>" + gvr.Cells[4].Text + "</td><td>" + gvr.Cells[5].Text + "</td><td>" + gvr.Cells[6].Text + "</td><td>" + (gvr.Cells[7].FindControl("lblTotalPriceAmount") as Label).Text + "</td></tr>";
        //strpdf += "</table></td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td align='center'>Total Drums  :" + lblTotalDrums.Text + "</td>";
        //strpdf += "<td colspan='2' align='center'> Total Amount :" + lblTotalprice.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td colspan='1' align='center'>Total Amount in Words</td>";
        //strpdf += "<td colspan='2'>    </td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td style='width: 238px; height:auto'></td>";
        //strpdf += "<td></td>";
        //strpdf += "<td rowspan='2' align='center'>Address</br>" + lblAddressDelivery.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td>Mode of Transport</td>";
        //strpdf += "<td>" + ddlModeofTransport.SelectedItem.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td>Place of Delivery</td>";
        //strpdf += "<td>" + ddlPlaceOfDelivery.SelectedItem.Text + "</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td colspan='3' align='center'>E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment</td>";
        //strpdf += "</tr><tr>";
        //strpdf += "<td colspan='2' border='0' ><table>";
        //strpdf += "<tr><td>S.No</td><td>Report Name</td><td>Date Of Email</td></tr>";
        //foreach (GridViewRow gvr in gvReports.Rows)
        //{
        //    if ((gvr.Cells[3].FindControl("cbReport") as CheckBox).Checked)
        //        strpdf += "<tr><td width='20' >" + (gvr.Cells[0].FindControl("lblsno") as Label).Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + (gvr.Cells[4].FindControl("txtReportDate") as TextBox).Text + "</td></tr>";
        //}
        //strpdf += "</table></td>";
        //strpdf += "<td colspan='1' align='center'>Authorized Signatory<br />&nbsp;Mudar India Exports</td>";
        //strpdf += "</tr></table>";

        //report
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true) + "/" + Borderid.ToString() + "_" + "LS" + ".pdf" : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + Borderid.ToString() + "_" + Bpoid.ToString() + ".pdf";

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
            orderobj.BranchOrderDetails_UPD(Borderid, Pdf_path, 2, "bhanu");
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

    protected void ddlCustomAgent_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAddressDelivery.Text = string.Empty;
        if (ddlCustomAgent.SelectedIndex > 0)
        {
            DataTable dtCustomAgent = new DataTable();
            dtCustomAgent = customagentobj.GetAgentDetails(ddlCustomAgent.SelectedValue.ToString());
            if (dtCustomAgent.Rows.Count > 0)
            {
                string[] DAddress = dtCustomAgent.Rows[0]["AddressforDelivery"].ToString().Split('\n');
                for (int i = 0; i <= DAddress.Length - 1; i++)
                {
                    if (i == 0)
                        lblAddressDelivery.Text = DAddress[0].ToString();
                    else
                        lblAddressDelivery.Text += "<br/>" + DAddress[i].ToString();
                }
                lblAddressDelivery.Text += "<br/>" + dtCustomAgent.Rows[0]["contactperson"].ToString();
                lblAddressDelivery.Text += "<br/>" + dtCustomAgent.Rows[0]["Mphone"].ToString();
                lblplacedelivery.Text = dtCustomAgent.Rows[0]["Place"].ToString();
            }
            divPurchseOrder.Visible = true;
            divbutton.Visible = true;
        }
    }
}
