using MudarOrganic.BL;
using MudarOrganic.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UpdateOrder : System.Web.UI.Page
{

    public static string SortExpression_p = "FarmerCode";
    Order_BL orderObj = new Order_BL();
    Settings_BL settObj = new Settings_BL();
    Reports_BL reportObj = new Reports_BL();
    FarmPlantation_BL PlantObj = new FarmPlantation_BL();
    Reports_Type rtypeObj = new Reports_Type();
    Product_BL pr = new Product_BL();
    Settings_BL set = new Settings_BL();
    Farmer_BL frmObj = new Farmer_BL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderReportPathdetails();
            string QorderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
            BindOrderDetails(Convert.ToInt32(QorderID));
        }
        if (!string.IsNullOrEmpty(hlInvoice.Text))
        {
            btnInvoice.Visible = false;
            btnDisableInv.Enabled = false;
            btnDisableInv.Visible = true;
            btnDisableInv.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlPacking.NavigateUrl))
        {
            btnPacking.Visible = false;
            btnDisablePack.Enabled = false;
            btnDisablePack.Visible = true;
            btnDisablePack.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlCoverLetter.NavigateUrl))
        {
            btnCoverLetter.Visible = false;
            btnDisableCL.Enabled = false;
            btnDisableCL.Visible = true;
            btnDisableCL.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlFIRCover.NavigateUrl))
        {
            btnFLetter.Visible = false;
            btnDisableFL.Enabled = false;
            btnDisableFL.Visible = true;
            btnDisableFL.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlHazSea.NavigateUrl))
        {
            btnHazsea.Visible = false;
            btnDisableHazSea.Enabled = false;
            btnDisableHazSea.Visible = true;
            btnDisableHazSea.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlHazAir.NavigateUrl))
        {
            btnHazAir.Visible = false;
            btnDisableHazAir.Enabled = false;
            btnDisableHazAir.Visible = true;
            btnDisableHazAir.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!Page.IsPostBack)
        {
            string orderStatus = Convert.ToString(Session["AdminOrderStatus"]);
            if (orderStatus.Contains("ETA"))
            {
                orderStatus="INTRANSIT";
                Session["AdminOrderStatus"] = orderStatus;
            }
            BinOrderStatusDetails(orderStatus);
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageStartupTabs('" + Convert.ToString(Session["AdminOrderStatus"]) + "','" + Convert.ToString(Session["AdminOrderStatus"]) + "');", true);
        }
    }
    private void ReportControlsOperations()
    {
        if (!string.IsNullOrEmpty(hlInvoice.Text))
        {
            btnInvoice.Enabled = false;
        }
        if (!string.IsNullOrEmpty(hlPacking.Text))
        {
            btnInvoice.Enabled = false;
        }
    }
    private void Enablecontrols()
    {
        btnInvoice.Visible = true;
        btnPacking.Visible = true;
        btnHazsea.Visible = true;
        btnHazAir.Visible = true;
        btnCoverLetter.Visible = true;
        btnFLetter.Visible = true;
        btnLable.Visible = true;
        btnPP.Visible = true;
        btnAR.Visible = true;
        btnSP.Visible = true;
        btnbo.Visible = true;
        btnCRY.Visible = true;
        btnCRYp.Visible = true;
        btnDisableInv.Visible = false;
        btnDisablePack.Visible = false;
        btnDisableHazSea.Visible = false;
        btnDisableHazAir.Visible = false;
        btnDisableCL.Visible = false;
        btnDisableFL.Visible = false;
        btnDisableLabel.Visible = false;
        btnDisablePP.Visible = false;
        btnDisableAR.Visible = false;
        btnDisableSP.Visible = false;
        btnDisablebo.Visible = false;
        btnDisableCRY.Visible = false;
        btnDisableCRYp.Visible = false;
    }
    private void Enacontrols()
    {
        btnInvoice.Visible = true;
        btnDisableInv.Visible = false;
        btnCoverLetter.Visible = true;
        btnDisableCL.Visible = false;
    }
    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/InvoiceReport.aspx");
    }
    protected void btnPacking_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/PackingReport.aspx");
    }
    protected void btnLetter_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/CoverLetter.aspx");
    }
    protected void btnLable_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/LabelReport.aspx");
    }
    private void bindReports(DataTable dtORPD)
    {
        int pCount = 0;// ddlSelectProduct.Items.Count - 1;
        DataTable report = new DataTable();
        report.Columns.Add("ProductName");
        report.Columns.Add("PP");
        report.Columns.Add("AR");
        report.Columns.Add("SP");
        report.Columns.Add("CRY");
        report.Columns.Add("CRY_P");
        report.Columns.Add("BO");
        report.Columns.Add("LABEL");
        for (int count = 0; count < pCount; count++)
        {
            string[] PP = dtORPD.Rows[0]["PP"].ToString().Split('$');
            string[] LABEL = dtORPD.Rows[0]["LABEL"].ToString().Split('$');
            string[] AR = dtORPD.Rows[0]["AR"].ToString().Split('$');
            string[] SP = dtORPD.Rows[0]["SP"].ToString().Split('$');
            string[] CRY = dtORPD.Rows[0]["CRY"].ToString().Split('$');
            string[] CRY_P = dtORPD.Rows[0]["CRY_P"].ToString().Split('$');
            string[] BO = dtORPD.Rows[0]["BO"].ToString().Split('$');
            DataRow drNew = report.NewRow();
            drNew["ProductName"] = 0;// ddlSelectProduct.Items[count + 1].Text;
            drNew["PP"] = PP.Length > 1 ? PP[count].ToString() : string.Empty;
            drNew["AR"] = AR.Length > 1 ? AR[count].ToString() : string.Empty;
            drNew["SP"] = SP.Length > 1 ? SP[count].ToString() : string.Empty;
            drNew["CRY"] = CRY.Length > 1 ? CRY[count].ToString() : string.Empty;
            drNew["CRY_P"] = CRY_P.Length > 1 ? CRY_P[count].ToString() : string.Empty;
            drNew["BO"] = BO.Length > 1 ? BO[count].ToString() : string.Empty;
            drNew["LABEL"] = LABEL.Length > 1 ? LABEL[count].ToString() : string.Empty;
            report.Rows.Add(drNew);
            btnLable.Enabled = LABEL.Length > 1 ? false : true;
            btnPP.Enabled = PP.Length > 1 ? false : true;
            btnAR.Enabled = AR.Length > 1 ? false : true;
            btnSP.Enabled = SP.Length > 1 ? false : true;
            btnbo.Enabled = BO.Length > 1 ? false : true;
            btnCRY.Enabled = CRY.Length > 1 ? false : true;
            btnCRYp.Enabled = CRY_P.Length > 1 ? false : true;
        }
        gvReports.DataSource = report;
        gvReports.DataBind();
    }
    protected void btnPlaceBrnchOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Orders/BranchOrder.aspx");
    }

    protected void btnPP_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-PP.aspx");
    }
    protected void btnAR_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-AR.aspx");
    }
    protected void btnSP_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-SP.aspx");
    }
    protected void btnCRY_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-CRY.aspx");
    }
    protected void btnCRYp_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-CRY-P.aspx");
    }
    protected void btnFLetter_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/FirCoverLetter.aspx");
    }
    protected void btnHazsea_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/nonHazSea.aspx");
    }
    protected void btnHazAir_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/nonHazAir.aspx");
    }
    protected void btnbo_Click(object sender, EventArgs e)
    {
        //Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/COA-BO.aspx");
    }

    private void BindOrderReportPathdetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtORPD = reportObj.OrderReportsPathGetDetails(Convert.ToInt32(OrderID));//Convert.ToInt32(lblBranchOrderID.Text));
        if (dtORPD.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dtORPD.Rows[0]["Invoice"].ToString()))
            {
                string[] path = dtORPD.Rows[0]["Invoice"].ToString().Split('/');
                string[] path2 = path[path.Length - 1].Split('.');
                string InvoiceID = path2[0].ToString();
                hlInvoice.Text = InvoiceID;
            }
            hlInvoice.NavigateUrl = dtORPD.Rows[0]["Invoice"].ToString();
            hlPacking.NavigateUrl = dtORPD.Rows[0]["Packing"].ToString();
            hlCoverLetter.NavigateUrl = dtORPD.Rows[0]["Cover_Letter"].ToString();
            hlFIRCover.NavigateUrl = dtORPD.Rows[0]["Fir_Cover_Letter"].ToString();
            hlHazSea.NavigateUrl = dtORPD.Rows[0]["Non_Haz_Sea"].ToString();
            hlHazAir.NavigateUrl = dtORPD.Rows[0]["Non_Haz_Air"].ToString();
            bindReports(dtORPD);
        }
    }

    private void BindOrderDetails(int OrderID)
    {
        //string Bcommet = string.Empty;
        DataTable dtOrder = orderObj.OrderList(OrderID);
        if (dtOrder.Rows.Count > 0)
        {
            DataRow dr = dtOrder.Rows[0];
            lblOrderId.Text = dr["OrderID"].ToString();
            lblPOID.Text = dr["PurchaseOrderID"].ToString();
            Session["PurchaseOrderID"] = lblPOID.Text;
            Session["PODate"] = dtOrder.Rows[0]["OrderDate"].ToString();
            if(!string.IsNullOrEmpty(dr["PurchaseOrderPath"].ToString()))
                hfPdf.NavigateUrl = dr["PurchaseOrderPath"].ToString();
            else
                hfPdf.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            if (!string.IsNullOrEmpty(dr["LSpath"].ToString()))
                hfLS.NavigateUrl = dr["LSpath"].ToString();
            else
                hfLS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            lblOtype.Text = dr["OrderType"].ToString();
            if (dr["OrderType"].ToString() == "order")
            {
                lblLotsample.Text = dr["LotSampleID"].ToString();
            }
            if (!string.IsNullOrEmpty(lblLotsample.Text))
                lblLotsample.Visible = true;
            else
                lblLotsample.Visible = false;
            if (dr["AdminOrderStatus"].ToString() == "NEW"||!string.IsNullOrEmpty(lblLotsample.Text))
            {
                btnPlaceBrnchOrder.Visible = true;
                btnDisable.Visible = false;
            }
            else if (!string.IsNullOrEmpty(dr["BranchOrderId"].ToString()))
            {
                //btnPlaceBrnchOrder.Enabled = false;
                btnPlaceBrnchOrder.Visible = false;
                btnDisable.Enabled = false; ;
                btnDisable.Visible = true;
                btnDisable.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
            }
            else
            {
                btnPlaceBrnchOrder.Visible = false;
                btnDisable.Visible = true;
            }
            if (dr["AdminOrderStatus"].ToString() == "DISPATCH" || dr["AdminOrderStatus"].ToString() == "SAMPLE DISPATCH")
                Enablecontrols();
            if (dr["AdminOrderStatus"].ToString() == "BLENDING")
            {
                if (dr["OrderType"].ToString() == "LotSample")
                    Enacontrols();
            }
            if (dr["AdminOrderStatus"].ToString() == "INPROCESS")
            {
                btnPlaceBrnchOrder.Visible = false;
                btnDisable.Enabled = false;
                btnDisable.Visible = true;
            }

            Session["AdminOrderStatus"] = dr["AdminOrderStatus"].ToString().ToUpper();
            //ddlOrderStatus.Items.FindByValue(dr["OrderStatus"].ToString().ToUpper()).Selected = true;
            //Session["BuyerId"] = dr["BuyerID"].ToString();
            //hlBuyer.Text = dr["BuyerCompanyName"].ToString();
            //lblBranchOrderID.Text = dr["BranchOrderId"].ToString();
            //lblBOtype.Text = dr["BOrderType"].ToString();
            //lblBPOID.Text = dr["BranchPOID"].ToString();
            //lbtnBranchName.Text = dr["Bname"].ToString();
            //hlBranchPDF.NavigateUrl = dr["BranchOrderPath"].ToString();
            //txtComments.Text = dr["O_Comments"].ToString().Replace("<br/>", System.Environment.NewLine);
            //txtBComments.Text = dr["B_Comments"].ToString().Replace("<br/>", System.Environment.NewLine);
            //ddlBStatus.ClearSelection();
            //if (!string.IsNullOrEmpty(dr["bOrderStatus"].ToString()))
            //    ddlBStatus.Items.FindByValue(dr["bOrderStatus"].ToString().ToUpper()).Selected = true;
        }
    }
    protected void btnClearance_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), "UNDER<BR/>CUSTOM<BR/>CLEARANCE", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "UNDER CUSTOM CLEARANCE");
        Session["AdminOrderStatus"] = "UNDER<BR/>CUSTOM<BR/>CLEARANCE";
        BinOrderStatusDetails("UNDER CUSTOM CLEARANCE");
        btnIntransitSubmit.Visible = true;
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('INTRANSIT','" + Convert.ToString(Session["AdminOrderStatus"]) + "');", true);
    }
    protected void btnCloseOrder_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), "CLOSE", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "CLOSE");
        Response.Redirect("~/Admin/OrderAdmin.aspx");
    }
    protected void btnIntransitSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtIntransitDate.Text))
        {
            DateTime intransitDate = DateTime.ParseExact(txtIntransitDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            string etaStr = "ETA : " + intransitDate.ToString("dd MMM yyyy");            
            string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
            orderObj.UpdateOrderETA(Convert.ToInt32(OrderID), intransitDate.ToString());
            orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), etaStr, "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + etaStr);
            Session["AdminOrderStatus"] = "INTRANSIT";
            BinOrderStatusDetails("INTRANSIT");
            lblETAText.Text = etaStr;
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('CLOSE','" + Convert.ToString(Session["AdminOrderStatus"]) + "');", true);
        }
    }
    protected void btnAdminSkip_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('UNDER<BR/>CUSTOM<BR/>CLEARANCE','" + Convert.ToString(Session["AdminOrderStatus"]) + "');", true);
    }

    private void BinOrderStatusDetails(string orderStatus)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtOrder = orderObj.GetOrderDetails(Convert.ToInt32(OrderID));
        if (dtOrder.Rows.Count > 0)
        {
            DataRow dr = dtOrder.Rows[0];
            orderStatus = orderStatus.ToUpper();
            if (orderStatus.Contains("ETA"))
            {
                orderStatus = "INTRANSIT";
            }


            if (orderStatus == "CLOSE")
            {
                btnDisableCloseOrder.Visible = true;
                btnCloseOrder.Visible = false;

                btnClearance.Visible = false;
                btnDisableClearance.Visible = true;


                string eta = Convert.ToString(dr["ETA"]);
                if (eta != "N/A")
                {
                    lblETAText.Text = "ETA : " + Convert.ToDateTime(eta).ToString("dd MMM yyyy");
                }

                tblETA.Visible = false;
                lblETAText.Visible = true;
            }
            else if (orderStatus == "UNDER CUSTOM CLEARANCE" || orderStatus == "UNDER<BR/>CUSTOM<BR/>CLEARANCE")
            {
                btnClearance.Visible = false;
                btnDisableClearance.Visible = true;

                tblETA.Visible = true;
                lblETAText.Visible = false;

                btnDisableCloseOrder.Visible = false;
                btnCloseOrder.Visible = true;

                btnIntransitSubmit.Visible = true;
            }
            else if (orderStatus == "INTRANSIT")
            {
                btnClearance.Visible = false;
                btnDisableClearance.Visible = true;

                string eta = Convert.ToString(dr["ETA"]);
                if (eta != "N/A")
                {
                    lblETAText.Text = "ETA : " + Convert.ToDateTime(eta).ToString("dd MMM yyyy");
                }

                tblETA.Visible = false;
                lblETAText.Visible = true;

                btnDisableCloseOrder.Visible = false;
                btnCloseOrder.Visible = true;
            }
        }
    }
}