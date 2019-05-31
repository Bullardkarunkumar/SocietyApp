using MudarOrganic.BL;
using MudarOrganic.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
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
    DataTable dtGlobalFarmerCollect = new DataTable();
    MudarApp APP = new MudarApp();
    bool result = false;
    string FarmerId = string.Empty;
    string FarmId = string.Empty;
    string PlantationId = string.Empty;
    string CollectDt = string.Empty;
    string LotNumber = string.Empty;
    string BlendBatachID = string.Empty;
    string FarmerCollectCheck = "";
    int collect = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string QorderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        if (!Page.IsPostBack)
        {
            btnFarmerCollectSearchapply.Style.Add("display", "block");
            btnFarmerCollectSearchapplyDisabled.Style.Add("display", "none");
            btnDisablepreorderSave.Style.Add("display", "block");
            btnDisablecollectSave.Visible = false;
            ViewState["dirState"] = "";
            Session["dtNewcollect"] = new object();
            BindOrderDetails(Convert.ToInt32(QorderID));
            if (!string.IsNullOrEmpty(lblBranchOrderID.Text))
            {
                BindddlOrderProductDetails();
                BindddlBlendProductDetails();
                BindddlTestProductDetails();
                BindBranchOrderReport();
            }
            BindIcsCodes();
            DivMasterManage();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["sel"]))
        {
            ddlSelectProduct.SelectedValue = Request.QueryString["pid"];
            ddlSelectProduct_SelectedIndexChanged(sender, e);

            string selectedICS = Request.QueryString["sel"];
            foreach (ListItem item in chkICSList.Items)
            {
                if (selectedICS.Contains(item.Value))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
            chkICSList_SelectedIndexChanged(sender, e);
            btnPreorder_Click(sender, e);
        }

        if (!string.IsNullOrEmpty(hlBInvoice.NavigateUrl))
        {
            btnBInvoice.Visible = false;
            btnDisableBInvoice.Enabled = false;
            btnDisableBInvoice.Visible = true;
            
            btnDisableBInvoice.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlBGLCInfo.NavigateUrl))
        {
            btnBGLCInfo.Visible = false;
            btnDisableBGLCInfo.Enabled = false;
            btnDisableBGLCInfo.Visible = true;
            btnDisableBGLCInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlBTruckBill.NavigateUrl))
        {
            btnBTruckBill.Visible = false;
            btnDisableBTruckBill.Enabled = false;
            btnDisableBTruckBill.Visible = true;
            btnDisableBTruckBill.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!string.IsNullOrEmpty(hlBLR.NavigateUrl))
        {
            btnBLR.Visible = false;
            btnDisableBLR.Enabled = false;
            btnDisableBLR.Visible = true;
            btnDisableBLR.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        }
        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["POStatus"]) == "BLENDING" && Convert.ToString(Session["hasMenthol"]) == "True")
            {
                BindFreeze();
            }
            if (Convert.ToString(Session["POStatus"]) == "FREEZING")
            {
                BindFreeze();
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
            }
            if (Convert.ToString(Session["POStatus"]) == "TESTING")
            {
                BindFreeze();
                BindNewPackingDetail();
            }
            if (Convert.ToString(Session["POStatus"]) == "DISPATCH" || Convert.ToString(Session["POStatus"]) == "DOCUMENTING" || Convert.ToString(Session["POStatus"]) == "PACKING")
            {
                BindFreeze();
                BindNewPackingDetail();
            }
            if (Convert.ToString(Session["POStatus"]) == "DISPATCH")
            {
                BindFreeze();
                btnDispatchNext.Visible = false;
                btnDisableDispatchNext.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageStartupTabs('" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        }
    }
    private enum Phase
    {
        Collecting,
        Blending
    }

    #region ICS Code start
    public void BindIcsCodes()
    {
        DataTable dt = frmObj.GetICSCodes();
        chkICSList.DataTextField = "Branchcode";
        chkICSList.DataValueField = "Branchcode";
        chkICSList.DataSource = dt;
        chkICSList.DataBind();
    }
    protected void chkICSList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(chkICSList.SelectedValue))
        {
            btnFarmer.Enabled = true;
            btnPreorder.Enabled = true;
            trButtonDetails.Visible = true;
            BindgvCollecingDetails(Convert.ToInt32(ddlSelectProduct.SelectedValue), "1");
            BindgvCollecingDetails(Convert.ToInt32(ddlSelectProduct.SelectedValue), "2");
            txtFarmerSearch.Text = string.Empty;
            Session["SelectedCollectionItems"] = null;
            btnFarmerCollectSearchapply_Click(sender, e);
            btnDisablecollectSave.Visible = true;
            btnFarmercollt.Visible = false;
            btnDisablepreorderSave.Style.Add("display", "block");
            btnPrecollt.Style.Add("display", "none");
        }
        else
        {
            btnFarmer.Enabled = false;
            btnPreorder.Enabled = false;
            trButtonDetails.Visible = false;
            divPerOrderCollecting.Style.Add("display", "none");
            divFarmerCollecting.Style.Add("display", "none");
            trShowDetails.Visible = false;
            trOtherFarmer.Visible = false;
            btnFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            btnFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            btnPreorder.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            btnPreorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    // ics checked End 
    #endregion

    #region Collecting
    public void BindEmptyCollectionDataTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("PlantationId", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("FarmerID", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("FarmID", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("FarmerCode", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("FirstName", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("Lot_No", typeof(string));
        dt.Columns.Add(dc);
        dc = new DataColumn("TotalProductQuantity", typeof(decimal));
        dt.Columns.Add(dc);
        dc = new DataColumn("SoldTotalQty", typeof(decimal));
        dt.Columns.Add(dc);
        dc = new DataColumn("Avaliable", typeof(decimal));
        dt.Columns.Add(dc);
        dc = new DataColumn("AvailableQuantity", typeof(decimal));
        dt.Columns.Add(dc);
        dc = new DataColumn("Collect", typeof(bool));
        dc.DefaultValue = false;
        dt.Columns.Add(dc);
        //dt.Rows.Add(dt.NewRow());
        gvSelectedCollectionDetails.DataSource = dt;
        gvSelectedCollectionDetails.DataBind();
        //int totalcolums = gvSelectedCollectionDetails.Rows[0].Cells.Count;
        //gvSelectedCollectionDetails.Rows[0].Cells.Clear();
        ////gvSelectedCollectionDetails.Rows[0].Controls.Clear();
        //gvSelectedCollectionDetails.Rows[0].Cells.Add(new TableCell());
        //gvSelectedCollectionDetails.Rows[0].Cells[0].ColumnSpan = totalcolums;
        //gvSelectedCollectionDetails.Rows[0].Cells[0].Text = "No Items collected from farmer yet";
        //gvSelectedCollectionDetails.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
        //gvSelectedCollectionDetails.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //gvSelectedCollectionDetails.Rows[0].Cells[0].Font.Bold = true;
    }
    //old code
    public void DivMasterManage()
    {
        //if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        //{
        //    divOrderDetails.Visible = true;
        //    BranchOrderDetails.Visible = false;
        //    divReports.Visible = false;
        //}
        //else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Branch.ToLower())
        //{
        //    BranchOrderDetails.Visible = true;
        //}
    }
    private void BindddlOrderProductDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int branchOrderID = Convert.ToInt32(lblBranchOrderID.Text);
        ddlSelectProduct.DataSource = orderObj.OrderProductList(Convert.ToInt32(OrderID), branchOrderID);
        ddlSelectProduct.DataTextField = "ProductName";
        ddlSelectProduct.DataValueField = "ProductID";
        ddlSelectProduct.DataBind();
        ddlSelectProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    private void BindOrderDetails(int OrderID)
    {
        //string Bcommet = string.Empty;
        DataTable dtOrder = orderObj.OrderList(OrderID);
        if (dtOrder.Rows.Count > 0)
        {
            DataRow dr = dtOrder.Rows[0];
            Session["PODate"] = dtOrder.Rows[0]["OrderDate"].ToString();
            Session["BuyerId"] = dr["BuyerID"].ToString();
            lblorderID.Text = OrderID.ToString();
            lblBranchOrderID.Text = dr["BranchOrderId"].ToString();
            lblBLs.Text=dr["LotSampleID"].ToString();
            lblBOtype.Text = dr["BOrderType"].ToString();
            lblBPOID.Text = dr["BranchPOID"].ToString();
            hlBranchPDF.NavigateUrl = dr["BranchOrderPath"].ToString();
            hlLSpdf.NavigateUrl = dr["BLSpath"].ToString();
        }
        DataTable dtOrderDetail = orderObj.GetBranchOrderDetails(OrderID);
        if (dtOrderDetail.Rows.Count > 0)
        {
            string orderStatus = dtOrderDetail.Rows[0]["OrderStatus"].ToString().ToUpper();
            Session["POStatus"] = dtOrderDetail.Rows[0]["OrderStatus"].ToString().ToUpper();
            string orderType = dtOrderDetail.Rows[0]["BOrderType"].ToString();
            Session["OrderType"] = orderType;
        }
        else
        {
            Session["POStatus"] = "NEW";
        }
        DataTable dtProducts = orderObj.ListOrderProducts(Convert.ToInt32(OrderID));
        if (dtProducts.Rows.Count > 0 && dtProducts.AsEnumerable().Any(m => m.Field<int>("ProductID") == 4))
        {
            Session["hasMenthol"] = "True";
        }
        else
        {
            Session["hasMenthol"] = "False";
        }
    }
    private void OrderReports()
    {
        string QorderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dt = reportObj.OrderReportsPathGetDetails(Convert.ToInt32(QorderID));
    }
    // old code
    protected void cbCollectingPreorder_CheckedChanged1(object sender, EventArgs e)
    {
        decimal chktest2 = 0;
        decimal X = 0;
        //lblpresentqty.Text = "0";
        foreach (GridViewRow Row in gvPreorderCollection.Rows)
        {
            //CheckBox ChkBoxRows = (CheckBox)Row.FindControl("cbCollectingPreorder");
            //ChkBoxRows.Checked = true;
            if (((CheckBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder")).Checked)
            {
                string quantity = ((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text;
                Regex regEx = new Regex(@"^\d{1,5}(\.\d{1,2})?$");
                if (string.IsNullOrEmpty(quantity))
                {
                    (gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder") as CheckBox).Checked = false;
                    ((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please Enter the Quantity !!!');</script>");
                }
                else if (!regEx.Match(quantity).Success)
                {
                    (gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder") as CheckBox).Checked = false;
                    ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please Enter valid Quantity !!! ');</script>");
                    return;
                }
                else if (Convert.ToDecimal(quantity) < 1)
                {
                    (gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder") as CheckBox).Checked = false;
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Quantity must be atleast one KG !!! ');</script>");

                }
                X = Convert.ToDecimal(gvPreorderCollection.Rows[Row.RowIndex].Cells[4].Text);
                if (!string.IsNullOrEmpty(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text))
                {
                    if (X >= Convert.ToDecimal(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text))
                    {
                        chktest2 += Convert.ToDecimal(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text);
                    }
                    else
                    {
                        (gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder") as CheckBox).Checked = false;
                        ((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Check the Available Quantity');</script>");
                    }
                }
                else
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Enter the Quantity');</script>");

            }
            else
                ((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
        }
        lblpresentqty.Text = chktest2.ToString();
        if (Convert.ToDecimal(lblpresentqty.Text) + Convert.ToDecimal(lblAlrcollQty.Text) == Convert.ToDecimal(lblOrderQuantity.Text))
            lblpresentqty.Text = (Convert.ToDecimal(lblpresentqty.Text) + Convert.ToDecimal(lblAlrcollQty.Text)).ToString();

    }
    private void UpdateQuantityLabels()
    {
        decimal qtyTotal = 0;
        //DataTable dtBranchOrder = orderObj.GetBranchOrderDetails(QorderID, Convert.ToInt32(ddlSelectProduct.SelectedValue));
        //if (dtBranchOrder.Rows.Count > 0)
        ////{
        //decimal netQty = Convert.ToDecimal(lblOrderQuantity.Text);
        //lblOrderQuantity.Text = netQty.ToString("F");

        //}
        qtyTotal = qtyTotal + Convert.ToDecimal(hdnCollectionQuantity.Value);

        if (gvCollecingDetails.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvCollecingDetails.Rows)
            {
                if (((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked)
                {
                    TextBox txt = (TextBox)item.Cells[6].FindControl("txtCollectQty");
                    qtyTotal = qtyTotal + Convert.ToDecimal(txt.Text);
                }
            }
        }

        if (gvPreorderCollection.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvPreorderCollection.Rows)
            {
                if (((CheckBox)item.Cells[6].FindControl("cbCollectingPreorder")).Checked)
                {
                    TextBox txt = (TextBox)item.Cells[5].FindControl("txtCollectQty");
                    qtyTotal = qtyTotal + Convert.ToDecimal(txt.Text);
                }
            }
        }
        lblpresentqty.Text = qtyTotal.ToString();
        lblAlrcollQty.Text = (Convert.ToDecimal(lblOrderQuantity.Text) - Convert.ToDecimal(lblpresentqty.Text)).ToString();
    }
    private bool CheckQuantityStatus()
    {
        decimal qtyTotal = 0;

        qtyTotal = qtyTotal + Convert.ToDecimal(hdnCollectionQuantity.Value);

        if (gvCollecingDetails.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvCollecingDetails.Rows)
            {
                if (((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked)
                {
                    TextBox txt = (TextBox)item.Cells[6].FindControl("txtCollectQty");
                    qtyTotal = qtyTotal + Convert.ToDecimal(txt.Text);
                }
            }
        }
        if (gvPreorderCollection.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvPreorderCollection.Rows)
            {
                if (((CheckBox)item.Cells[6].FindControl("cbCollectingPreorder")).Checked)
                {
                    TextBox txt = (TextBox)item.Cells[5].FindControl("txtCollectQty");
                    qtyTotal = qtyTotal + Convert.ToDecimal(txt.Text);
                }
            }
        }
        return (Convert.ToDecimal(lblOrderQuantity.Text) >= qtyTotal);
    }
    protected void btnFarmerSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtFarmerSearch.Text))
        {
            DataTable dt = Session["FarmerCollectDetails"] as DataTable;
            decimal qtyTotal = 0;
            List<MudarItemModel> listRows = new List<MudarItemModel>();
            if (gvCollecingDetails.Rows.Count > 0)
            {
                foreach (GridViewRow item in gvCollecingDetails.Rows)
                {
                    if (((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked)
                    {
                        MudarItemModel itemModel = new MudarItemModel();
                        Guid pfarmerId = itemModel.FarmerId = new Guid(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmerID"].ToString());
                        int pfarmID = itemModel.FarmId = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmID"]);
                        int pplantationId = itemModel.PlantationId = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["PlantationId"]);

                        dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
                  .ToList().ForEach(m =>
                  {
                      itemModel.Quantity = Convert.ToDecimal(((TextBox)item.Cells[6].FindControl("txtCollectQty")).Text);
                      //m["Collect"] = ((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked;
                  });
                        listRows.Add(itemModel);
                    }
                }
                Session["SearchFarmerCollectDetails"] = listRows;
                //lblpresentqty.Text = qtyTotal.ToString();
                //lblAlrcollQty.Text = (Convert.ToDecimal(lblOrderQuantity.Text) - Convert.ToDecimal(lblpresentqty.Text)).ToString();
                UpdateSelectedQuantityLabels();
            }
            if (Session["FarmerCollectDetails"] != null)
            {
                dt = Session["FarmerCollectDetails"] as DataTable;
                string query = txtFarmerSearch.Text;
                var mgg = dt.AsEnumerable().Where(m => m.Field<bool>("Collect") == false).Where(m => m.Field<string>("FarmerCode").StartsWith(txtFarmerSearch.Text, StringComparison.OrdinalIgnoreCase) || m.Field<string>("FirstName").IndexOf(txtFarmerSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                gvCollecingDetails.DataSource = dt.AsEnumerable()
                           .Where(mgg.Contains)
                           .AsDataView();
            }
            else
            {
                gvCollecingDetails.DataSource = null;
            }
            gvCollecingDetails.DataBind();
            trSearchButtons.Visible = true;
            btnFarmerSearchCancel.Visible = true;
            btnFarmercollt.Visible = false;
            trCollectGridRow.Style.Add("display", "block");
            trTotalQtyRow.Style.Add("display", "block");
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Farmer Name / Code is mandatatory to search farmers !!!');</script>");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnFarmerSearchCancel_Click(object sender, EventArgs e)
    {
        txtFarmerSearch.Text = string.Empty;
        DataTable dt = Session["FarmerCollectDetails"] as DataTable;
        if (Session["SearchFarmerCollectDetails"] != null)
        {
            List<MudarItemModel> listPrevSelectedItems = Session["SearchFarmerCollectDetails"] as List<MudarItemModel>;
            foreach (MudarItemModel item in listPrevSelectedItems)
            {
                Guid pfarmerId = item.FarmerId;
                int pfarmID = item.FarmId;
                int pplantationId = item.PlantationId;
                dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
        .ToList().ForEach(m =>
        {
            m["AvailableQuantity"] = item.Quantity;
            m["Collect"] = true;
        });
            }
            Session["SearchFarmerCollectDetails"] = null;
        }

        gvCollecingDetails.DataSource = dt;
        gvCollecingDetails.DataBind();
        trSearchButtons.Visible = true;
        btnFarmerSearchCancel.Visible = false;
        btnFarmercollt.Visible = false;
        btnDisablecollectSave.Visible = true;
        UpdateSelectedQuantityLabels();
        trCollectGridRow.Style.Add("display", "block");
        trTotalQtyRow.Style.Add("display", "block");
        btnFarmerCollectSearchapplyDisabled.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnFarmercollt_Click(object sender, EventArgs e)
    {
        trOtherFarmer.Visible = false;
        btnFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        DataTable dtFarmerCollectionData = GetFarmerCollectionData(Convert.ToInt32(ddlSelectProduct.SelectedValue));
        Session["dtFarmercollect"] = new object();
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("FarmerId");
        dt1.Columns.Add("CollectDt");
        dt1.Columns.Add("LotNumber");
        dt1.Columns.Add("FarmId");
        dt1.Columns.Add("PlantationId");
        dt1.Columns.Add("SoldQty");
        dt1.Columns.Add("Collect", typeof(bool));
        DataRow drdt1 = dt1.NewRow();
        decimal totalQty = 0.0M;
        string collectedQty = string.Empty;
        foreach (GridViewRow Row in gvPreorderCollection.Rows)
        {
            if (((CheckBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder")).Checked)
            {
                totalQty += Convert.ToDecimal(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
            }
        }
        foreach (GridViewRow Row in gvCollecingDetails.Rows)
        {
            if (((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked)
            {
                Guid pfarmerId = new Guid(gvCollecingDetails.DataKeys[Row.RowIndex].Values["FarmerID"].ToString());
                int pfarmID = Convert.ToInt32(gvCollecingDetails.DataKeys[Row.RowIndex].Values["FarmID"]);
                int pplantationId = Convert.ToInt32(gvCollecingDetails.DataKeys[Row.RowIndex].Values["PlantationId"]);

                dtFarmerCollectionData.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
        .ToList().ForEach(m =>
        {
            m["AvailableQuantity"] = Convert.ToDecimal(((TextBox)Row.Cells[6].FindControl("txtCollectQty")).Text);
            m["Collect"] = true;
        });
                totalQty += Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
                CollectDt += ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text + ";";
                collectedQty += (Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text) + Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[4].Text.Trim())).ToString() + ";";
                LotNumber += gvCollecingDetails.Rows[Row.RowIndex].Cells[2].Text + ";";
                DataKey dk = gvCollecingDetails.DataKeys[Row.RowIndex];
                FarmerId += dk.Values["FarmerID"].ToString() + ";";
                FarmId += dk.Values["FarmID"].ToString() + ";";
                PlantationId += dk.Values["PlantationId"].ToString() + ";";
                //result = PlantObj.SoldQuantity_Update(Convert.ToInt32(dk.Values["PlantationId"].ToString()), Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text) + Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[4].Text.Trim()), "Aslam");
            }
        }
        drdt1["FarmerId"] = FarmerId;
        drdt1["LotNumber"] = LotNumber;
        drdt1["CollectDt"] = CollectDt;
        drdt1["FarmId"] = FarmId;
        drdt1["PlantationId"] = PlantationId;
        drdt1["SoldQty"] = collectedQty;
        drdt1["Collect"] = true;
        dt1.Rows.Add(drdt1);
        Session["dtFarmercollect"] = dt1;
        //lblpresentqty.Text = totalQty.ToString();
        //lblAlrcollQty.Text = (Convert.ToDecimal(lblOrderQuantity.Text) - totalQty).ToString();
        gvCollecingDetails.DataSource = dtFarmerCollectionData;
        gvCollecingDetails.DataBind();
        //divFarmerCollecting.Visible = false;
        //if (Convert.ToDecimal(lblOrderQuantity.Text) == Convert.ToDecimal(lblpresentqty.Text) && Session["dtPreorder"] != null && Session["dtFarmercollect"] != null)
        //{
        btncollectSubmit.Visible = true;
        btnDisablecollectSubmit.Visible = false;
        trbtnCollectSubmit.Visible = true;

        //}
        //else
        //{
        //    btncollectSubmit.Visible = false;
        //    trbtnCollectSubmit.Visible = false;
        //}

        btnFarmercollt.Visible = false;
        btnDisablecollectSave.Enabled = false;
        btnDisablecollectSave.Visible = true;
        UpdateSelectedQuantityLabels();
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnPrecollt_Click(object sender, EventArgs e)
    {
        DataTable dt = GetPreorderCollectionData(Convert.ToInt32(ddlSelectProduct.SelectedValue));
        Session["dtPreorder"] = new object();
        DataTable dt2 = new DataTable();
        dt2.Columns.Add("FarmerId");
        dt2.Columns.Add("CollectDt");
        dt2.Columns.Add("LotNumber");
        dt2.Columns.Add("FarmId");
        dt2.Columns.Add("PlantationId");
        dt2.Columns.Add("Blending_BatchID");
        dt2.Columns.Add("Collect", typeof(bool));
        DataRow drdt2 = dt2.NewRow();
        decimal totalQty = 0.0M;
        foreach (GridViewRow Row in gvCollecingDetails.Rows)
        {
            if (((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked)
            {
                totalQty += Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
            }
        }
        foreach (GridViewRow Row in gvPreorderCollection.Rows)
        {
            if (((CheckBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("cbCollectingPreorder")).Checked)
            {
                //Guid pfarmerId = new Guid(gvPreorderCollection.DataKeys[Row.RowIndex].Values["FarmerID"].ToString());
                //string pfarmID = Convert.ToString(gvPreorderCollection.DataKeys[Row.RowIndex].Values["FarmID"]);
                //string pplantationId = Convert.ToString(gvPreorderCollection.DataKeys[Row.RowIndex].Values["PlantationId"]);
                //int pcolltransid = Convert.ToInt32(gvPreorderCollection.DataKeys[Row.RowIndex].Values["CollectionTransactionID"]);
                string blendingBatchId = Convert.ToString(gvPreorderCollection.Rows[Row.RowIndex].Cells[0].ToString());
                if (!string.IsNullOrEmpty(blendingBatchId))
                {
                    dt.AsEnumerable().Where(a => a.Field<string>("Blending_BatchID") == blendingBatchId)
            .ToList().ForEach(m =>
            {
                m["CollectedQuantity"] = Convert.ToDecimal(((TextBox)Row.Cells[4].FindControl("txtCollectQty")).Text);
                m["Collect"] = ((CheckBox)Row.Cells[5].FindControl("cbCollectingPreorder")).Checked;
            });
                }
                //    if (pcolltransid > 0)
                //    {
                //        dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<string>("Blending_BatchID") == pfarmID && a.Field<string>("PlantationId") == PlantationId && a.Field<int>("CollectionTransactionID") == pcolltransid)
                //.ToList().ForEach(m =>
                //{
                //    m["CollectedQuantity"] = Convert.ToDecimal(((TextBox)Row.Cells[4].FindControl("txtCollectQty")).Text);
                //    m["Collect"] = ((CheckBox)Row.Cells[5].FindControl("cbCollectingPreorder")).Checked;
                //});
                //    }

                totalQty += Convert.ToDecimal(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
                //collect += collect + Convert.ToInt32(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
                CollectDt += ((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text + ";";
                LotNumber += gvPreorderCollection.Rows[Row.RowIndex].Cells[1].Text;
                DataKey dk = gvPreorderCollection.DataKeys[Row.RowIndex];
                BlendBatachID += gvPreorderCollection.Rows[Row.RowIndex].Cells[0].Text + ";";
                FarmerId += dk.Values["FarmerID"].ToString();
                FarmId += dk.Values["FarmID"].ToString();
                PlantationId += dk.Values["CollectionTransactionID"].ToString() + ";";
                //result = orderObj.PreorderSoldQtyUpdate(Convert.ToInt32(dk.Values["CollectionTransactionID"].ToString()), Convert.ToInt32(((TextBox)gvPreorderCollection.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text), "Aslam");
            }
        }
        drdt2["FarmerId"] = FarmerId;
        drdt2["LotNumber"] = LotNumber;
        drdt2["CollectDt"] = CollectDt;
        drdt2["FarmId"] = FarmId;
        drdt2["PlantationId"] = PlantationId;
        drdt2["Blending_BatchID"] = BlendBatachID;
        drdt2["Collect"] = true;
        dt2.Rows.Add(drdt2);
        UpdateSelectedQuantityLabels();
        //lblpresentqty.Text = totalQty.ToString();
        //lblAlrcollQty.Text = (Convert.ToDecimal(lblOrderQuantity.Text) - totalQty).ToString();
        //gvPreorderCollection.DataSource = dt;
        //gvPreorderCollection.DataBind();
        Session["dtPreorder"] = dt2;
        //divPerOrderCollecting.Visible = false;
        //if (Convert.ToDecimal(lblOrderQuantity.Text) == Convert.ToDecimal(lblpresentqty.Text) && Session["dtPreorder"] != null && Session["dtFarmercollect"] != null)
        //{
        btncollectSubmit.Visible = true;
        btnDisablecollectSubmit.Visible = false;
        trbtnCollectSubmit.Visible = true;
        //}
        //else
        //{
        //    btncollectSubmit.Visible = false;
        //    trbtnCollectSubmit.Visible = false;
        //}
        // btnPrecollt.Visible = false;
        btnPrecollt.Style.Add("display", "none");
        btnDisablepreorderSave.Enabled = false;

        //btnDisablepreorderSave.Visible = true;
        btnDisablepreorderSave.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void gvCollecingDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_p = e.SortExpression.ToString();
        SortingFarmerCode(SortExpression_p);
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    public string dir
    {
        get
        {
            if (ViewState["dirState"].ToString() == "desc")
            {
                ViewState["dirState"] = "asc";
            }
            else
            {
                ViewState["dirState"] = "desc";
            }
            return ViewState["dirState"].ToString();
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingFarmerCode(string SortExpression)
    {
        DataTable dt = (DataTable)Session["FarmerCollectDetails"];
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvCollecingDetails.DataSource = sortedView;
        gvCollecingDetails.DataBind();
    }
    private void SortingSelectFarmerCode(string SortExpression)
    {
        DataTable dt = (DataTable)Session["SelectedCollectionItems"];
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvSelectedCollectionDetails.DataSource = sortedView;
        gvSelectedCollectionDetails.DataBind();
    }
    protected void btnFarmer_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlSelectProduct.SelectedValue))
        {
            divFarmerCollecting.Visible = true;
            divPerOrderCollecting.Visible = true;
            divFarmerCollecting.Style.Add("display", "block");
            divPerOrderCollecting.Style.Add("display", "none");
            btnFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            btnFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            btnPreorder.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            btnPreorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            divFarmerCollecting.Style.Add("display", "block");
            trShowDetails.Visible = true;
            trOtherFarmer.Visible = false;
            if (ddlSelectProduct.SelectedValue == "4")
            {
                lblMCText.Visible = true;
                lblMCQty.Visible = true;
            }
            UpdateSelectedQuantityLabels();
        }
        else
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Select the ProductName');</script>");

        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnPreorder_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlSelectProduct.SelectedValue))
        {
            divFarmerCollecting.Visible = true;
            divPerOrderCollecting.Visible = true;
            divFarmerCollecting.Style.Add("display", "none");
            divPerOrderCollecting.Style.Add("display", "block");
            btnPreorder.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            btnPreorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            btnFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            btnFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            trShowDetails.Visible = true;
            trOtherFarmer.Visible = false;
            lblMCText.Visible = false;
            lblMCQty.Visible = false;
            UpdateSelectedQuantityLabels();
        }
        else
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Select the ProductName');</script>");
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void ddlSelectProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelectProduct.SelectedIndex > 0)
        {
            Session["dtPreorder"] = null;
            Session["dtFarmercollect"] = null;
            hdnCollectionQuantity.Value = "0";
            string QorderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
            DataTable dt = orderObj.GetCollectedOrderProduct(Convert.ToInt32(QorderID), Convert.ToInt32(ddlSelectProduct.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                trShowCollectedInfo.Visible = true;
                BindCollectedProductInfo(dt);
            }
            else
            {
                trSubFromFarmer.Visible = false;
                gvCollectedFromFarmer.DataSource = null;
                gvCollectedFromFarmer.DataBind();

                trSubFromPreorder.Visible = false;

                gvCollectedFromPreorder.DataSource = null;
                gvCollectedFromPreorder.DataBind();
            }

            if (CheckCollectionCompletedByProduct(Convert.ToInt32(QorderID), Convert.ToInt32(ddlSelectProduct.SelectedValue)))
            {
                trShowICSList.Visible = false;
                lblpresentqty.Text = "0";
                chkICSList.ClearSelection();
                chkICSList_SelectedIndexChanged(sender, e);
                btncollectSubmit.Visible = false;
                trbtnCollectSubmit.Visible = false;
                btnDisablecollectSubmit.Visible = false;
                trSearchButtons.Visible = false;
                btnFarmerSearchCancel.Visible = false;
            }
            else
            {
                trShowICSList.Visible = true;
                if (!string.IsNullOrEmpty(ddlSelectProduct.SelectedValue))
                    trShowICSList.Visible = true;
                lblpresentqty.Text = "0";
                chkICSList.ClearSelection();
                chkICSList_SelectedIndexChanged(sender, e);
                btncollectSubmit.Visible = false;
                trbtnCollectSubmit.Visible = false;
                btnDisablecollectSubmit.Visible = false;
                trSearchButtons.Visible = true;
                btnFarmerSearchCancel.Visible = false;
            }
        }
        else
        {
            chkICSList.ClearSelection();
            trShowCollectedInfo.Visible = false;
            trShowICSList.Visible = false;
            trButtonDetails.Visible = false;
            trShowDetails.Visible = false;
            trOtherFarmer.Visible = false;
            gvPreorderCollection.DataSource = null;
            gvPreorderCollection.DataBind();
            gvCollecingDetails.DataSource = null;
            gvCollecingDetails.DataBind();
            divFarmerCollecting.Visible = false;
            divPerOrderCollecting.Visible = false;
            trbtnCollectSubmit.Visible = false;
            trSearchButtons.Visible = true;
            btnFarmerSearchCancel.Visible = false;
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void BindCollectedProductInfo(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            DataTable dtNewPre = new DataTable();
            dtNewPre.Columns.Add("Blending_BatchID");
            dtNewPre.Columns.Add("CollectionQty", typeof(decimal));
            dtNewPre.Columns.Add("CreatedDate", typeof(DateTime));
            dtNewPre.Columns.Add("ProductID", typeof(int));

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("FarmerID");
            dtNew.Columns.Add("FarmID");
            dtNew.Columns.Add("FarmerName");
            dtNew.Columns.Add("Farmercode");
            dtNew.Columns.Add("PlotCode");
            dtNew.Columns.Add("Lotnumber");
            dtNew.Columns.Add("CollectionQty", typeof(decimal));
            dtNew.Columns.Add("CreatedDate", typeof(DateTime));
            dtNew.Columns.Add("ProductID", typeof(int));

            foreach (DataRow item in dt.Rows)
            {
                string[] FarmerID = item["FarmerID"].ToString().Split('@');
                string[] FarmID = item["FarmID"].ToString().Split('@');
                string[] Lotnumber = item["Lotnumber"].ToString().Split('@');
                string[] CollectionQty = item["CollectionQty"].ToString().Split('@');
                string blendingBatchId = item["Blending_BatchID"].ToString();

                if (!string.IsNullOrEmpty(blendingBatchId) && blendingBatchId.Contains("@"))
                {
                    string[] BlendingBID = item["Blending_BatchID"].ToString().Split('@');
                    if (BlendingBID[0].ToString().Trim() == string.Empty)
                    {
                        string[] FarmerID1 = FarmerID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] FarmID1 = FarmID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] Lotnumber1 = Lotnumber[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] CollectionQty1 = CollectionQty[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < FarmerID1.Length; i++)
                        {
                            DataRow drNew = dtNew.NewRow();
                            drNew["FarmerID"] = FarmerID1[i].ToString();
                            drNew["FarmID"] = FarmID1[i].ToString();
                            drNew["Lotnumber"] = Lotnumber1[i].ToString();
                            drNew["CollectionQty"] = CollectionQty1[i].ToString();
                            DataTable dtFarmer = orderObj.GetBlendFarmerDetails(FarmerID1[i].ToString(), FarmID1[i].ToString());
                            if (dtFarmer.Rows.Count > 0)
                            {
                                drNew["PlotCode"] = dtFarmer.Rows[0]["AreaCode"].ToString();
                                drNew["FarmerName"] = dtFarmer.Rows[0]["FirstName"].ToString();
                                drNew["Farmercode"] = dtFarmer.Rows[0]["FarmerCode"].ToString();
                                drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                                drNew["ProductID"] = Convert.ToInt32(item["ProductID"]);
                            }
                            dtNew.Rows.Add(drNew);
                        }


                        if (BlendingBID[1].ToString() != string.Empty)
                        {
                            DataTable dtPreOrder = new DataTable();
                            //dtPreOrder = orderObj.GetCollectionBlendDetails(dtBlending.Rows[0]["Blending_BatchID"].ToString());
                            string[] BlendingBID2 = BlendingBID[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            string[] CollectionQty2 = CollectionQty[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < BlendingBID2.Length; j++)
                            {
                                DataRow drNew = dtNewPre.NewRow();
                                drNew["Blending_BatchID"] = BlendingBID2[j].ToString();
                                drNew["CollectionQty"] = CollectionQty2[j].ToString();
                                drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                                drNew["ProductID"] = Convert.ToInt32(item["ProductID"]);
                                dtNewPre.Rows.Add(drNew);

                            }

                        }
                    }
                }
                else
                {
                    string[] BlendingBID2 = item["Blending_BatchID"].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] CollectionQty2 = CollectionQty[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < BlendingBID2.Length; j++)
                    {
                        DataRow drNew = dtNewPre.NewRow();
                        drNew["Blending_BatchID"] = BlendingBID2[j].ToString();
                        drNew["CollectionQty"] = CollectionQty2[j].ToString();
                        drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                        drNew["ProductID"] = Convert.ToInt32(item["ProductID"]);
                        dtNewPre.Rows.Add(drNew);
                    }
                }
            }

            //Session["CollectedFromFarmer"] = dtNew;
            decimal farmerCollTotal = dtNew.AsEnumerable().Sum(m => m.Field<decimal>("CollectionQty"));
            trSubFromFarmer.Visible = (dtNew.Rows.Count > 0);
            ltlFarmerTotal.Text = farmerCollTotal.ToString("F");
            gvCollectedFromFarmer.DataSource = dtNew;
            gvCollectedFromFarmer.DataBind();

            decimal preorderCollTotal = dtNewPre.AsEnumerable().Sum(m => m.Field<decimal>("CollectionQty"));
            trSubFromPreorder.Visible = (dtNewPre.Rows.Count > 0);
            ltlPreorderTotal.Text = preorderCollTotal.ToString("F");
            gvCollectedFromPreorder.DataSource = dtNewPre;
            gvCollectedFromPreorder.DataBind();
            hdnCollectionQuantity.Value = (farmerCollTotal + preorderCollTotal).ToString();
        }
    }
    private DataTable GetFarmerCollectionData(int productId)
    {
        List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
        .Where(li => li.Selected)
        .Select(li => "'" + li.Value + "'")
        .ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        DataTable Pyear = new DataTable();
        DataTable dtBranchOrder = new DataTable();
        DataTable dtDate = new DataTable();
        DataTable dtOrderCollect = new DataTable();
        if (productId == 4 || productId == 10 || productId == 11)
        {
            if (productId == 4)
                dtDate = settObj.GetStandDetails("2", (DateTime.Now.Year - 1).ToString());
            if (productId == 10)
                dtDate = settObj.GetStandDetails("3", (DateTime.Now.Year - 1).ToString());
            if (productId == 11)
                dtDate = settObj.GetStandDetails("8", (DateTime.Now.Year - 1).ToString());
        }
        else
            dtDate = settObj.GetStandDetails(productId.ToString(), (DateTime.Now.Year - 1).ToString());
        if (dtDate.Rows.Count > 0)
            Pyear = settObj.GetProductionYear(Convert.ToDateTime(dtDate.Rows[0]["Date"].ToString()));
        if (Pyear.Rows.Count > 0)
        {
            dtOrderCollect = orderObj.CollectedProductDetailsBasedonProductandICs(productId, Pyear.Rows[0]["ProductionYear"].ToString(), chkSelectedVal);
            //dtGlobalFarmerCollect = orderObj.CollectedProductDetailsBasedonProductandICs(productId, "2013", chkSelectedVal);
            DataColumn dc = new DataColumn("AvailableQuantity", typeof(decimal));
            dc.DefaultValue = 0;
            dtOrderCollect.Columns.Add(dc);
            dc = new DataColumn("Collect", typeof(bool));
            dc.DefaultValue = false;
            dtOrderCollect.Columns.Add(dc);
        }
        return dtOrderCollect;
    }
    private DataTable GetPreorderCollectionData(int productId)
    {
        List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
        .Where(li => li.Selected)
        .Select(li => "'" + li.Value + "'")
        .ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        DataTable dtOrderCollect = orderObj.PreOrderList_Product(productId, chkSelectedVal);
        DataColumn dc = new DataColumn("CollectedQuantity", typeof(decimal));
        dc.DefaultValue = 0;
        dtOrderCollect.Columns.Add(dc);

        dc = new DataColumn("Collect", typeof(bool));
        dc.DefaultValue = false;
        dtOrderCollect.Columns.Add(dc);
        return dtOrderCollect;
    }
    private void BindgvCollecingDetails(int productID, string type)
    {
        decimal OrderNetQty;
        lblBatchID.Text = string.Empty;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
   .Where(li => li.Selected)
   .Select(li => "'" + li.Value + "'")
   .ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        if (type == "1")
        {
            DataTable Pyear = new DataTable();
            DataTable dtBranchOrder = new DataTable();
            DataTable dtDate = new DataTable();
            dtBranchOrder = orderObj.GetBranchOrderDetails(OrderID, productID);
            if (productID.ToString() == "4" || productID.ToString() == "10" || productID.ToString() == "11")
            {
                if (productID.ToString() == "4")
                {
                    dtDate = settObj.GetStandDetails("2", (DateTime.Now.Year - 1).ToString());
                    productID = 2;
                }
                if (productID.ToString() == "10")
                {
                    dtDate = settObj.GetStandDetails("3", (DateTime.Now.Year - 1).ToString());
                    productID = 3;
                }
                if (productID.ToString() == "11")
                {
                    dtDate = settObj.GetStandDetails("8", (DateTime.Now.Year - 1).ToString());
                    productID = 8;
                }
            }
            else
                dtDate = settObj.GetStandDetails(productID.ToString(), (DateTime.Now.Year - 1).ToString());
            if (dtDate.Rows.Count > 0)
            {
                Pyear = settObj.GetProductionYear(Convert.ToDateTime(dtDate.Rows[0]["Date"].ToString()));
                dtGlobalFarmerCollect = orderObj.CollectedProductDetailsBasedonProductandICs(productID, Pyear.Rows[0]["ProductionYear"].ToString(), chkSelectedVal);
                //dtGlobalFarmerCollect = orderObj.CollectedProductDetailsBasedonProductandICs(productID, "2013", chkSelectedVal);
                DataColumn dc = new DataColumn("AvailableQuantity", typeof(decimal));
                dc.DefaultValue = 0;
                dtGlobalFarmerCollect.Columns.Add(dc);
                dc = new DataColumn("Collect", typeof(bool));
                dc.DefaultValue = false;
                dtGlobalFarmerCollect.Columns.Add(dc);

                if (dtGlobalFarmerCollect.Rows.Count > 0)
                {
                    decimal qtyTotal = 0;
                    if (gvCollecingDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow item in gvCollecingDetails.Rows)
                        {
                            if (((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked)
                            {
                                Guid pfarmerId = new Guid(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmerID"].ToString());
                                int pfarmID = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmID"]);
                                int pplantationId = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["PlantationId"]);
                                dtGlobalFarmerCollect.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
                        .ToList().ForEach(m =>
                        {
                            m["AvailableQuantity"] = Convert.ToDecimal(((TextBox)item.Cells[6].FindControl("txtCollectQty")).Text);
                            m["Collect"] = ((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked;
                            qtyTotal = qtyTotal + Convert.ToDecimal(((TextBox)item.Cells[6].FindControl("txtCollectQty")).Text);
                        });
                            }
                        }
                    }
                    Session["FarmerCollectDetails"] = null;
                    Session["FarmerCollectDetails"] = dtGlobalFarmerCollect;
                    //lblpresentqty.Text = qtyTotal.ToString();
                    UpdateSelectedQuantityLabels();
                    gvCollecingDetails.DataSource = dtGlobalFarmerCollect;
                    gvCollecingDetails.DataBind();
                    DataTable dtmer = new DataTable();
                    if (productID == 4)
                    {
                        OrderNetQty = Convert.ToDecimal(dtBranchOrder.Rows[0]["NetQuantity"].ToString());
                        decimal mcqty = Convert.ToDecimal(dtBranchOrder.Rows[0]["NetQuantity"].ToString());
                        lblMCQty.Text = Math.Round(mcqty, 2).ToString();
                        dtmer = set.GetMentholPerDetails(2);
                        if (dtmer.Rows.Count > 0)
                        {
                            decimal mer =Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                            decimal d1 = Math.Round(((OrderNetQty * 100) /mer), 0);
                            lblOrderQuantity.Text = Math.Round(d1, 2).ToString();
                        }
                    }
                    else
                    {
                        OrderNetQty = Convert.ToDecimal(dtBranchOrder.Rows[0]["NetQuantity"].ToString());
                        lblOrderQuantity.Text = Math.Round(OrderNetQty, 2).ToString();
                    }
                }
                else
                {
                    Session["FarmerCollectDetails"] = null;
                    gvCollecingDetails.DataSource = null;
                    gvCollecingDetails.DataBind();
                    btnFarmerCollectSearchapply.Style.Add("display", "none");
                }
            }
            else
            {
                trButtonDetails.Visible = false;
                Session["FarmerCollectDetails"] = null;
                gvCollecingDetails.DataSource = null;
                gvCollecingDetails.DataBind();
                btnFarmerCollectSearchapply.Style.Add("display", "none");
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! contact admin !!!');</script>");
                return;
            }
        }
        if (type == "2")
        {
            DataTable dtOrderCollect = new DataTable();
            if (productID.ToString() == "4" || productID.ToString() == "10" || productID.ToString() == "11")
            {
                if (productID.ToString() == "4")
                {
                    dtOrderCollect = orderObj.PreOrderList_Product(2, chkSelectedVal);
                }
                if (productID.ToString() == "10")
                {
                     dtOrderCollect = orderObj.PreOrderList_Product(10, chkSelectedVal);
                }
                if (productID.ToString() == "11")
                {
                    dtOrderCollect = orderObj.PreOrderList_Product(11, chkSelectedVal);
                    //productID = 8;
                }
            }
            else
            {
                dtOrderCollect = orderObj.PreOrderList_Product(productID, chkSelectedVal);
            }
            decimal d = dtOrderCollect.AsEnumerable().Select(m => Convert.ToDecimal(m.Field<int>("AvaliableQty"))).Sum();
            if (d > 0)
            {
                btnRedirectPreorder.Visible = false;
            }
            else
            {
                btnRedirectPreorder.Visible = false;
            }

            DataTable BranchOrderDB = orderObj.BranchOrderDetails(lblBPOID.Text);
            DataRow[] drs = BranchOrderDB.Select("ProductID = " + productID);
            DataTable dtmer = new DataTable();
            if (productID == 4)
            {
                OrderNetQty = Convert.ToDecimal(drs[0]["NetQuantity"].ToString());
                decimal mcqty = Convert.ToDecimal(drs[0]["NetQuantity"].ToString());
                lblMCQty.Text = Math.Round(mcqty, 2).ToString();
                dtmer = set.GetMentholPerDetails(2);
                if (dtmer.Rows.Count > 0)
                {
                    decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                    decimal d1 = Math.Round(((OrderNetQty * 100) / mer), 0);
                    lblOrderQuantity.Text = Math.Round(d1, 2).ToString();
                }
            }
            else
            {
                OrderNetQty = Convert.ToDecimal(drs[0]["NetQuantity"].ToString());
                lblOrderQuantity.Text = Math.Round(OrderNetQty, 2).ToString();
            }
            DataColumn dc = new DataColumn("CollectedQuantity", typeof(decimal));

            dc.DefaultValue = 0;
            dtOrderCollect.Columns.Add(dc);

            dc = new DataColumn("Collect", typeof(bool));
            dc.DefaultValue = false;
            dtOrderCollect.Columns.Add(dc);
            if (dtOrderCollect.Rows.Count > 0)
            {
                decimal qtyTotal = 0;
                if (gvPreorderCollection.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvPreorderCollection.Rows)
                    {
                        if (((CheckBox)item.Cells[5].FindControl("cbCollectingPreorder")).Checked)
                        {
                            string blendingBatchId = Convert.ToString(gvPreorderCollection.Rows[item.RowIndex].Cells[0].Text.ToString());
                            if (!string.IsNullOrEmpty(blendingBatchId))
                            {
                                dtOrderCollect.AsEnumerable().Where(a => a.Field<string>("Blending_BatchID") == blendingBatchId)
                        .ToList().ForEach(m =>
                        {
                            m["CollectedQuantity"] = Convert.ToDecimal(((TextBox)item.Cells[4].FindControl("txtCollectQty")).Text);
                            m["Collect"] = true;
                            qtyTotal = qtyTotal + Convert.ToDecimal(((TextBox)item.Cells[4].FindControl("txtCollectQty")).Text);
                        });
                            }
                        }
                    }
                }
                Session["dtPreorder"] = null;
                Session["dtPreorder"] = dtOrderCollect;
                lblpresentqty.Text = qtyTotal.ToString();
                gvPreorderCollection.DataSource = dtOrderCollect;
                gvPreorderCollection.DataBind();

                btnDisablepreorderSave.Style.Add("display", "none");
                btnPrecollt.Style.Add("display", "block");

            }
            else
            {
                Session["dtPreorder"] = null;
                gvPreorderCollection.DataSource = null;
                gvPreorderCollection.DataBind();
                btnDisablepreorderSave.Style.Add("display", "block");
                btnPrecollt.Style.Add("display", "none");
            }
        }
    }
    protected void btncollectSubmit_Click(object sender, EventArgs e)
    {
        int CollectionID = 0;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int ProductID = Convert.ToInt32(ddlSelectProduct.SelectedValue);
        DataTable dtFarmercollect = Session["dtFarmercollect"] as DataTable;
        if (dtFarmercollect != null)
        {
            var selectedFarmer = dtFarmercollect.AsEnumerable().Where(m => m.Field<bool>("Collect") == true);
            if (selectedFarmer.Count() > 0)
            {
                dtFarmercollect = dtFarmercollect.AsEnumerable().Where(m => m.Field<bool>("Collect") == true).CopyToDataTable();
            }
            else
            {
                dtFarmercollect = null;
            }
        }
        DataTable dtPreorder = Session["dtPreorder"] as DataTable;
        if (dtPreorder != null)
        {
            var selectedPreorder = dtPreorder.AsEnumerable().Where(m => m.Field<bool>("Collect") == true);
            if (selectedPreorder.Count() > 0)
            {
                dtPreorder = dtPreorder.AsEnumerable().Where(m => m.Field<bool>("Collect") == true).CopyToDataTable();
            }
            else
            {
                dtPreorder = null;
            }
        }
        if (dtFarmercollect != null && dtPreorder != null)
        {
            string plaintionIds = dtFarmercollect.Rows[0]["PlantationId"].ToString();
            string[] plantIds = plaintionIds.Split(new char[] { ';' }, StringSplitOptions.None);
            string soldTotalQtys = dtFarmercollect.Rows[0]["SoldQty"].ToString();
            string[] soldQtys = soldTotalQtys.Split(new char[] { ';' }, StringSplitOptions.None);

            for (int i = 0; i < plantIds.Length; i++)
            {
                if (!string.IsNullOrEmpty(plantIds[i]) && !string.IsNullOrEmpty(soldQtys[i]))
                {
                    PlantObj.SoldQuantity_Update(Convert.ToInt32(plantIds[i]), Convert.ToDecimal(soldQtys[i]), "Aslam");
                }
            }

            string collTransIds = dtPreorder.Rows[0]["PlantationId"].ToString();
            string[] collTranscIds = collTransIds.Split(new char[] { ';' }, StringSplitOptions.None);
            string collectedQty = dtPreorder.Rows[0]["CollectDt"].ToString();
            string[] collectedQtys = collectedQty.Split(new char[] { ';' }, StringSplitOptions.None);

            for (int i = 0; i < collTranscIds.Length; i++)
            {
                if (!string.IsNullOrEmpty(collTranscIds[i]) && !string.IsNullOrEmpty(collectedQtys[i]))
                {
                    orderObj.PreorderSoldQtyUpdateNew(Convert.ToInt32(collTranscIds[i]), Convert.ToDecimal(collectedQtys[i]), "Aslam");
                }
            }
            DataTable dtCollect = orderObj.GetCollectionID(OrderID);
            if (dtCollect.Rows.Count > 0)
            {
                result = true;
                CollectionID = Convert.ToInt32(dtCollect.Rows[0]["CollectionID"]);
            }
            else
            {
                result = orderObj.ProductsCollection_Insert(ref CollectionID, Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), "Aslam", string.Empty, MudarApp.Insert, false);
            }
            if (result)
            {
                bool IsNew = true;
                result = orderObj.ProductsCollectionTran_Insert("@" + dtPreorder.Rows[0]["Blending_BatchID"].ToString(), CollectionID, ProductID, dtFarmercollect.Rows[0]["FarmerId"].ToString() + '@' + dtPreorder.Rows[0]["FarmerId"].ToString(), txtOtherFarmers.Text, "0", txtCollectQTY.Text, dtFarmercollect.Rows[0]["CollectDt"].ToString() + '@' + dtPreorder.Rows[0]["CollectDt"].ToString(), dtFarmercollect.Rows[0]["FarmId"].ToString() + '@' + dtPreorder.Rows[0]["FarmId"].ToString(), dtFarmercollect.Rows[0]["LotNumber"].ToString() + '@' + dtPreorder.Rows[0]["LotNumber"].ToString(), "Aslam",DateTime.Now, string.Empty, IsNew == true ? MudarApp.Insert : MudarApp.Update, dtFarmercollect.Rows[0]["PlantationId"].ToString() + '@' + dtPreorder.Rows[0]["PlantationId"].ToString());

                if (CheckPhaseCompleted(Convert.ToInt32(OrderID), Phase.Collecting))
                {
                    orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "COLLECTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "COLLECTING");
                    Session["POStatus"] = "COLLECTING";
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct.SelectedIndex = 0;
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
                else
                {
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
            }
        }
        else if (dtFarmercollect != null)
        {
            string plaintionIds = dtFarmercollect.Rows[0]["PlantationId"].ToString();
            string[] plantIds = plaintionIds.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string soldTotalQtys = dtFarmercollect.Rows[0]["SoldQty"].ToString();
            string[] soldQtys = soldTotalQtys.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < plantIds.Length; i++)
            {
                if (!string.IsNullOrEmpty(plantIds[i]) && !string.IsNullOrEmpty(soldQtys[i]))
                {
                    PlantObj.SoldQuantity_Update(Convert.ToInt32(plantIds[i]), Convert.ToDecimal(soldQtys[i]), "Aslam");
                }
            }
            DataTable dtCollect = orderObj.GetCollectionID(OrderID);
            if (dtCollect.Rows.Count > 0)
            {
                result = true;
                CollectionID = Convert.ToInt32(dtCollect.Rows[0]["CollectionID"]);
            }
            else
            {
                result = orderObj.ProductsCollection_Insert(ref CollectionID, Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), "Aslam", string.Empty, MudarApp.Insert, false);
            }
            if (result)
            {
                result = orderObj.ProductsCollectionTran_Insert("@", CollectionID, ProductID, dtFarmercollect.Rows[0]["FarmerId"].ToString(), txtOtherFarmers.Text, "0", txtCollectQTY.Text, dtFarmercollect.Rows[0]["CollectDt"].ToString(), dtFarmercollect.Rows[0]["FarmId"].ToString(), dtFarmercollect.Rows[0]["LotNumber"].ToString(), "Aslam", DateTime.Now, string.Empty, MudarApp.Insert, dtFarmercollect.Rows[0]["PlantationId"].ToString());
                if (CheckPhaseCompleted(Convert.ToInt32(OrderID), Phase.Collecting))
                {
                    orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "COLLECTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "COLLECTING");
                    Session["POStatus"] = "COLLECTING";
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct.SelectedIndex = 0;
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
                else
                {
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
            }
        }
        else
        {
            string collTransIds = dtPreorder.Rows[0]["PlantationId"].ToString();
            string[] collTranscIds = collTransIds.Split(new char[] { ';' }, StringSplitOptions.None);
            string collectedQty = dtPreorder.Rows[0]["CollectDt"].ToString();
            string[] collectedQtys = collectedQty.Split(new char[] { ';' }, StringSplitOptions.None);

            for (int i = 0; i < collTranscIds.Length; i++)
            {
                if (!string.IsNullOrEmpty(collTranscIds[i]) && !string.IsNullOrEmpty(collectedQtys[i]))
                {
                    orderObj.PreorderSoldQtyUpdate(Convert.ToInt32(collTranscIds[i]), Convert.ToInt32(collectedQtys[i]), "Aslam");
                }
            }
            DataTable dtCollect = orderObj.GetCollectionID(OrderID);
            if (dtCollect.Rows.Count > 0)
            {
                result = true;
                CollectionID = Convert.ToInt32(dtCollect.Rows[0]["CollectionID"]);
            }
            else
            {
                result = orderObj.ProductsCollection_Insert(ref CollectionID, Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), "Aslam", string.Empty, MudarApp.Insert, true);
            }
            if (result)
            {
                result = orderObj.ProductsCollectionTran_Insert(dtPreorder.Rows[0]["Blending_BatchID"].ToString(), CollectionID, ProductID, "@" + dtPreorder.Rows[0]["FarmerId"].ToString(), txtOtherFarmers.Text, "0", txtCollectQTY.Text, "@" + dtPreorder.Rows[0]["CollectDt"].ToString(), "@" + dtPreorder.Rows[0]["FarmId"].ToString(), "@" + dtPreorder.Rows[0]["LotNumber"].ToString(), "Aslam", DateTime.Now, string.Empty, MudarApp.Insert, "@" + dtPreorder.Rows[0]["PlantationId"].ToString());
                if (CheckPhaseCompleted(Convert.ToInt32(OrderID), Phase.Collecting))
                {
                    
                    orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "COLLECTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "COLLECTING");
                    Session["POStatus"] = "BLENDING";
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct.SelectedIndex = 0;
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
                else
                {
                    ResetGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                    ddlSelectProduct_SelectedIndexChanged(sender, e);
                }
            }
        }
        btncollectSubmit.Visible = false;
        btnDisablecollectSubmit.Visible = true;
        
        #region oldcode

        #endregion

    }
    private void ResetGrid()
    {
        gvCollecingDetails.DataSource = null;
        gvCollecingDetails.DataBind();

        gvPreorderCollection.DataSource = null;
        gvPreorderCollection.DataBind();
    }
    private bool CheckCollectionCompletedByProduct(int orderId, int ProductId)
    {
        bool collectionCompleted = true;
        DataTable dtFlag = orderObj.CollectedProduct(orderId, ProductId, Convert.ToInt32(lblBranchOrderID.Text));
        decimal totalQty = 0.0M;
        decimal netQty = 0.0M;
        foreach (DataRow flag in dtFlag.Rows)
        {
            string collectQtyStr = Convert.ToString(flag["CollectionQty"]);
            if (collectQtyStr.Contains("@"))
            {
                string[] qtys = collectQtyStr.Split('@');
                string[] farmerQty = qtys[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] preorderQty = qtys[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                totalQty = totalQty + farmerQty.Sum(m => Convert.ToDecimal(m));
                totalQty = totalQty + preorderQty.Sum(m => Convert.ToDecimal(m));
            }
            else
            {
                string[] farmerQty = collectQtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                totalQty = totalQty + farmerQty.Sum(m => Convert.ToDecimal(m));
            }
        }
        DataTable dtmer = new DataTable();
        DataTable dtOrderDetails = orderObj.GetBranchOrderDetails(Convert.ToString(orderId), ProductId);
        if (dtOrderDetails.Rows.Count > 0)
        {
            if (ProductId == 4)
            {
                netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
                dtmer = set.GetMentholPerDetails(2);
                if (dtmer.Rows.Count > 0)
                {
                    decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                    decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                    netQty = d1;
                }
            }
            else
            {
                netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
            }
        }

        if (totalQty != netQty)
        {
            collectionCompleted = false;
        }
        return collectionCompleted;
    }
    private bool CheckPhaseCompleted(int orderId, Phase phase)
    {

        if (phase == Phase.Collecting)
        {
            bool collectionCompleted = true;
            DataTable dtProducts = orderObj.ListOrderProducts(orderId);
            foreach (DataRow item in dtProducts.Rows)
            {
                DataTable dtFlag = orderObj.CollectedProduct(orderId, Convert.ToInt32(item["ProductID"]), Convert.ToInt32(lblBranchOrderID.Text));
                decimal totalQty = 0.0M;
                decimal netQty = 0.0M;
                foreach (DataRow flag in dtFlag.Rows)
                {
                    string collectQtyStr = Convert.ToString(flag["CollectionQty"]);
                    if (collectQtyStr.Contains("@"))
                    {
                        string[] qtys = collectQtyStr.Split('@');
                        string[] farmerQty = qtys[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] preorderQty = qtys[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        totalQty = totalQty + farmerQty.Sum(m => Convert.ToDecimal(m));
                        totalQty = totalQty + preorderQty.Sum(m => Convert.ToDecimal(m));
                    }
                    else
                    {
                        string[] farmerQty = collectQtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        totalQty = totalQty + farmerQty.Sum(m => Convert.ToDecimal(m));
                    }
                }
                DataTable dtmer = new DataTable();
                DataTable dtOrderDetails = orderObj.GetBranchOrderDetails(Convert.ToString(orderId), Convert.ToInt32(item["ProductID"]));
                if (dtOrderDetails.Rows.Count > 0)
                {
                    if (Convert.ToInt32(item["ProductID"]) == 4)
                    {
                        netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
                        dtmer = set.GetMentholPerDetails(2);
                        if (dtmer.Rows.Count > 0)
                        {
                            decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                            decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                            netQty = d1;
                        }
                    }
                    else
                    {
                        netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
                    }
                }

                if (totalQty != netQty)
                {
                    collectionCompleted = false;
                    break;
                }
            }
            return collectionCompleted;
        }
        else
        {
            return false;
        }
    }
    protected void btnFarmerCollectSearchapply_Click(object sender, EventArgs e)
    {
        if (!CheckQuantityStatus())
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity should not exceed Order Quantity');</script>");
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
            return;
        }
        if (Session["FarmerCollectDetails"] != null)
        {
            List<DataRow> listSelectedItems = new List<DataRow>();
            DataTable dt = (DataTable)Session["FarmerCollectDetails"];
            if (dt.Rows.Count > 0)
            {
                dt.AsEnumerable().Where(m => m.Field<bool>("Collect") == true)
                           .ToList().ForEach(m =>
                           {
                               m["AvailableQuantity"] = 0;
                               m["Collect"] = false;
                           });
                decimal collectedQty = 0;
                if (gvPreorderCollection.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvPreorderCollection.Rows)
                    {
                        if (((CheckBox)item.Cells[5].FindControl("cbCollectingPreorder")).Checked)
                        {
                            collectedQty = collectedQty + Convert.ToDecimal(((TextBox)item.Cells[4].FindControl("txtCollectQty")).Text);

                        }
                    }
                }
                DataTable dtSelected = Session["SelectedCollectionItems"] as DataTable;
                decimal qtyTotal = 0;
                if (gvCollecingDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow item in gvCollecingDetails.Rows)
                    {
                        if (((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked)
                        {
                            Guid pfarmerId = new Guid(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmerID"].ToString());
                            int pfarmID = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["FarmID"]);
                            int pplantationId = Convert.ToInt32(gvCollecingDetails.DataKeys[item.RowIndex].Values["PlantationId"]);

                            dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
                    .ToList().ForEach(m =>
                    {
                        m["AvailableQuantity"] = Convert.ToDecimal(((TextBox)item.Cells[6].FindControl("txtCollectQty")).Text);
                        m["Collect"] = ((CheckBox)item.Cells[7].FindControl("cbCollecting")).Checked;
                        qtyTotal = qtyTotal + Convert.ToDecimal(((TextBox)item.Cells[6].FindControl("txtCollectQty")).Text);



                    });
                            //listSelectedItems.AddRange(dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
                            //            .ToList());

                        }
                    }
                    if (Convert.ToDecimal(lblOrderQuantity.Text) < (qtyTotal + collectedQty))
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity should not exceed Order Quantity !!!');</script>");
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                        return;
                    }

                    if (Session["SearchFarmerCollectDetails"] != null)
                    {
                        List<MudarItemModel> listPrevSelectedItems = Session["SearchFarmerCollectDetails"] as List<MudarItemModel>;
                        foreach (MudarItemModel item in listPrevSelectedItems)
                        {
                            Guid pfarmerId = item.FarmerId;
                            int pfarmID = item.FarmId;
                            int pplantationId = item.PlantationId;

                            dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == pfarmerId && a.Field<int>("FarmID") == pfarmID && a.Field<int>("PlantationId") == pplantationId)
                    .ToList().ForEach(m =>
                    {
                        m["AvailableQuantity"] = item.Quantity;
                        m["Collect"] = true;
                    });

                        }
                        Session["SearchFarmerCollectDetails"] = null;
                    }

                    List<DataRow> listTotalSelections = dt.AsEnumerable().Where(m => m.Field<bool>("Collect") == true).ToList();
                    foreach (DataRow item in listTotalSelections)
                    {
                        if (dtSelected != null)
                        {
                            DataRow dr = dtSelected.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == new Guid(item["FarmerID"].ToString()) && a.Field<int>("FarmID") == Convert.ToInt32(item["FarmID"]) && a.Field<int>("PlantationId") == Convert.ToInt32(item["PlantationId"])).FirstOrDefault();
                            if (dr != null)
                            {
                                dr["AvailableQuantity"] = Convert.ToDecimal(item["AvailableQuantity"]);
                                listSelectedItems.Add(dr);
                            }
                            else
                            {
                                listSelectedItems.Add(item);
                            }
                        }
                        else
                        {
                            listSelectedItems.Add(item);
                        }
                    }

                    List<DataRow> tempList = new List<DataRow>();
                    tempList.AddRange(listSelectedItems);

                    foreach (DataRow item in tempList)
                    {
                        Guid guid = new Guid(Convert.ToString(item["FarmerID"]));
                        DataRow dr = dt.AsEnumerable().Where(a => a.Field<bool>("Collect") == true && a.Field<Guid>("FarmerID") == guid && a.Field<int>("FarmID") == Convert.ToInt32(item["FarmID"]) && a.Field<int>("PlantationId") == Convert.ToInt32(item["PlantationId"])).FirstOrDefault();
                        if (dr == null)
                        {
                            listSelectedItems.Remove(item);
                        }
                    }
                    if (listSelectedItems.Count > 0)
                    {
                        DataTable selectedDT = listSelectedItems.AsEnumerable().CopyToDataTable();
                        Session["SelectedCollectionItems"] = selectedDT;
                        if (selectedDT.Rows.Count > 0)
                        {
                            gvSelectedCollectionDetails.DataSource = selectedDT;
                            gvSelectedCollectionDetails.DataBind();
                        }
                        else
                        {
                            BindEmptyCollectionDataTable();
                        }
                    }
                    else
                    {
                        Session["SelectedCollectionItems"] = null;
                        BindEmptyCollectionDataTable();
                    }

                    List<MudarItemModel> listItems = new List<MudarItemModel>();
                    foreach (DataRow item in listSelectedItems)
                    {
                        MudarItemModel mudarItem = new MudarItemModel();
                        mudarItem.FarmerId = new Guid(Convert.ToString(item["FarmerID"]));
                        mudarItem.FarmId = Convert.ToInt32(item["FarmID"]);
                        mudarItem.PlantationId = Convert.ToInt32(item["PlantationId"]);
                        mudarItem.Quantity = Convert.ToDecimal(item["AvailableQuantity"]);
                        listItems.Add(mudarItem);
                    }

                    dt.AsEnumerable().Where(m => m.Field<bool>("Collect") == true)
                    .ToList().ForEach(m =>
                    {
                        m["AvailableQuantity"] = 0;
                        m["Collect"] = false;
                    });

                    foreach (MudarItemModel item in listItems)
                    {
                        //Guid guid = new Guid(Convert.ToString(item["FarmerID"]));
                        dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == item.FarmerId && a.Field<int>("FarmID") == item.FarmId && a.Field<int>("PlantationId") == item.PlantationId)
                            .ToList().ForEach(m =>
                            {
                                m["AvailableQuantity"] = item.Quantity;
                                m["Collect"] = true;
                            });
                    }
                    gvCollecingDetails.DataSource = dt;
                    gvCollecingDetails.DataBind();
                    trSearchButtons.Visible = true;
                    btnFarmerSearchCancel.Visible = false;
                    btnFarmercollt.Visible = true;
                    btnDisablecollectSave.Visible = false;
                    txtFarmerSearch.Text = string.Empty;
                    UpdateSelectedQuantityLabels();
                    btnFarmerCollectSearchapply.Style.Add("display", "none");
                    btnFarmerCollectSearchapplyDisabled.Style.Add("display", "block");


                    if (dt.AsEnumerable().All(m => m.Field<bool>("Collect") == false))
                    {
                        trCollectGridRow.Style.Add("display", "block");
                        trTotalQtyRow.Style.Add("display", "block");
                    }
                    else
                    {
                        trCollectGridRow.Style.Add("display", "none");
                        trTotalQtyRow.Style.Add("display", "none");
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
            }
        }
        else
        {
            gvSelectedCollectionDetails.DataSource = null;
            gvSelectedCollectionDetails.DataBind();
        }
    }
    protected void gvSelectedCollectionDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_p = e.SortExpression.ToString();
        SortingSelectFarmerCode(SortExpression_p);
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void gvSelectedCollectionDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSelectedCollectionDetails.EditIndex = e.NewEditIndex;
        BindSelectedCollectionDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void gvSelectedCollectionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int PPlantationId = Convert.ToInt32(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["PlantationId"]);
        Guid PFarmerID = new Guid(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["FarmerID"].ToString());
        int PFarmID = Convert.ToInt32(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["FarmID"]);

        DataTable dt = (DataTable)Session["SelectedCollectionItems"];
        DataRow dr = dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == PFarmerID && a.Field<int>("FarmID") == PFarmID && a.Field<int>("PlantationId") == PPlantationId).FirstOrDefault();
        if (dr != null)
        {
            dt.Rows.Remove(dr);
            Session["SelectedCollectionItems"] = dt;

            DataTable dtFarmerCollection = Session["FarmerCollectDetails"] as DataTable;
            dtFarmerCollection.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == PFarmerID && a.Field<int>("FarmID") == PFarmID && a.Field<int>("PlantationId") == PPlantationId)
                        .ToList().ForEach(m =>
                        {
                            //TextBox txt = (TextBox)gvSelectedCollectionDetails.Rows[e.RowIndex].FindControl("txtCollectQty");
                            m["AvailableQuantity"] = 0;
                            m["Collect"] = false;
                        });
            //Session["FarmerCollectDetails"] = dtFarmerCollection;
            gvCollecingDetails.DataSource = dtFarmerCollection;
            gvCollecingDetails.DataBind();
            UpdateSelectedQuantityLabels();
            gvSelectedCollectionDetails.DataSource = dt;
            gvSelectedCollectionDetails.DataBind();
            btnFarmercollt.Visible = true;
            btnDisablecollectSave.Visible = false;
            //ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Selected Item removed successfully !!!');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Unable to remove selected item !!!');</script>");
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void gvSelectedCollectionDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSelectedCollectionDetails.EditIndex = -1;
        BindSelectedCollectionDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING');", true);
    }
    protected void gvSelectedCollectionDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txt = (TextBox)gvSelectedCollectionDetails.Rows[e.RowIndex].FindControl("txtCollectQty");
        if (string.IsNullOrEmpty(txt.Text))
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please enter quantity !!!');</script>");
            return;
        }


        Regex regex = new Regex(@"^\d+[\.,]?\d{0,2}$");
        Match match = regex.Match(txt.Text);
        if (!match.Success)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please enter valid quantity value !!!');</script>");
            return;
        }

        decimal collectedQuantity = Convert.ToDecimal(txt.Text);
        //if (decimal.TryParse(txt.Text, out collectedQuantity))
        //{
        //    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please enter valid quantity value !!!');</script>");
        //    return;
        //}

        if (collectedQuantity <= 0)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Quantity must be atleast one KG !!! ');</script>");
            return;
        }


        decimal availableQyantity = Convert.ToDecimal(gvSelectedCollectionDetails.Rows[e.RowIndex].Cells[2].Text.Trim());

        if (availableQyantity < collectedQuantity)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please check the available quantity !!! ');</script>");
            return;
        }
        int PPlantationId = Convert.ToInt32(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["PlantationId"]);
        Guid PFarmerID = new Guid(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["FarmerID"].ToString());
        int PFarmID = Convert.ToInt32(gvSelectedCollectionDetails.DataKeys[e.RowIndex].Values["FarmID"]);

        DataTable dt = (DataTable)Session["SelectedCollectionItems"];
        dt.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == PFarmerID && a.Field<int>("FarmID") == PFarmID && a.Field<int>("PlantationId") == PPlantationId)
                    .ToList().ForEach(m =>
                    {
                        //TextBox txt = (TextBox)gvSelectedCollectionDetails.Rows[e.RowIndex].FindControl("txtCollectQty");
                        m["AvailableQuantity"] = Convert.ToDecimal(txt.Text);
                    });
        decimal total = dt.AsEnumerable().Sum(m => m.Field<decimal>("AvailableQuantity"));
        Session["SelectedCollectionItems"] = dt;
        DataTable dtFarmerCollection = Session["FarmerCollectDetails"] as DataTable;
        dtFarmerCollection.AsEnumerable().Where(a => a.Field<Guid>("FarmerID") == PFarmerID && a.Field<int>("FarmID") == PFarmID && a.Field<int>("PlantationId") == PPlantationId)
                    .ToList().ForEach(m =>
                    {
                        //TextBox txt = (TextBox)gvSelectedCollectionDetails.Rows[e.RowIndex].FindControl("txtCollectQty");
                        m["AvailableQuantity"] = Convert.ToDecimal(txt.Text);
                        m["Collect"] = true;
                    });
        //Session["FarmerCollectDetails"] = dtFarmerCollection;
        gvCollecingDetails.DataSource = dtFarmerCollection;
        gvCollecingDetails.DataBind();
        this.UpdateSelectedQuantityLabels();
        gvSelectedCollectionDetails.EditIndex = -1;
        gvSelectedCollectionDetails.DataSource = dt;
        gvSelectedCollectionDetails.DataBind();
        btnFarmercollt.Visible = true;
        btnDisablecollectSave.Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void UpdateSelectedQuantityLabels()
    {
        decimal total = 0;
        total = total + Convert.ToDecimal(hdnCollectionQuantity.Value);
        DataTable dt = (DataTable)Session["SelectedCollectionItems"];
        if (dt != null)
        {
            total = total + dt.AsEnumerable().Sum(m => m.Field<decimal>("AvailableQuantity"));
        }
        if (gvPreorderCollection.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvPreorderCollection.Rows)
            {
                if (((CheckBox)item.Cells[5].FindControl("cbCollectingPreorder")).Checked)
                {
                    total = total + Convert.ToDecimal(((TextBox)item.Cells[4].FindControl("txtCollectQty")).Text);
                }
            }
        }
        lblpresentqty.Text = total.ToString();
        lblAlrcollQty.Text = (Convert.ToDecimal(lblOrderQuantity.Text) - total).ToString();
    }
    protected void gvSelectedCollectionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button lnkbtnresult = (Button)e.Row.FindControl("ButtonDelete");
            if (lnkbtnresult != null)
            {
                lnkbtnresult.Attributes.Add("onclick", "javascript:return confirm('Are you sure that you want remove this item?');");
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('COLLECTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void BindSelectedCollectionDetails()
    {
        DataTable dt = (DataTable)Session["SelectedCollectionItems"];
        gvSelectedCollectionDetails.DataSource = dt;
        gvSelectedCollectionDetails.DataBind();
    }
    protected void btnRedirectPreorder_Click(object sender, EventArgs e)
    {
        int productId = Convert.ToInt32(ddlSelectProduct.SelectedValue);
        List<string> listSelectedItems = chkICSList.Items.Cast<ListItem>()
   .Where(li => li.Selected)
   .Select(li => li.Value).ToList();
        string selItems = string.Join(";", listSelectedItems.ToArray());
        Response.Redirect("~/Mudar/FarmerPreOrder.aspx?nav=true&pid=" + productId + "&sel=" + selItems);
    }
    protected void gvCollecingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "50px");
        }
    }
    #endregion

    #region Blend
    private bool CheckBlendPhaseCompleted(int orderId, string collectionid, string blendid)
    {
        bool blendingCompleted = true;
        DataTable dtProducts = orderObj.ListOrderProducts(orderId);
        foreach (DataRow item in dtProducts.Rows)
        {
            DataTable dtFlag = orderObj.GetBlendDetailsBasedonBlendID(blendid, collectionid, Convert.ToString(item["ProductID"]));
            decimal totalQty = 0.0M;
            decimal netQty = 0.0M;
            foreach (DataRow flag in dtFlag.Rows)
            {
                string qtyStr = Convert.ToString(flag["BlendQty"]);
                if (!string.IsNullOrEmpty(qtyStr))
                {
                    qtyStr = qtyStr.Trim();
                    string[] farmerQtyArray = new string[] { };
                    string[] preorderQtyArray = new string[] { };
                    if (qtyStr.Contains("@"))
                    {
                        string[] qtyArray = qtyStr.Split('@');
                        farmerQtyArray = qtyArray[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        preorderQtyArray = qtyArray[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        farmerQtyArray = qtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    if (farmerQtyArray.Length > 0)
                        totalQty = totalQty + farmerQtyArray.Sum(m => Convert.ToDecimal(m));
                    if (preorderQtyArray.Length > 0)
                        totalQty = totalQty + preorderQtyArray.Sum(m => Convert.ToDecimal(m));
                }
            }
            DataTable dtmer = new DataTable();
            DataTable dtOrderDetails = orderObj.GetBranchOrderDetails(Convert.ToString(orderId), Convert.ToInt32(item["ProductID"]));
            if (dtOrderDetails.Rows.Count > 0)
            {
                if (Convert.ToInt32(item["ProductID"]) == 4)
                {
                    netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
                    dtmer = set.GetMentholPerDetails(2);
                    decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                    decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                    netQty = d1;
                }
                else
                {
                    netQty = Convert.ToDecimal(dtOrderDetails.Rows[0]["NetQuantity"].ToString());
                }
            }

            if (totalQty != netQty)
            {
                blendingCompleted = false;
                break;
            }
        }
        return blendingCompleted;
    }
    private void UpdateSelectedBlendingQuantityLabels()
    {
        decimal total = 0;
        total = total + Convert.ToDecimal(hdnBlendingQuantity.Value);

        if (gvBlending.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvBlending.Rows)
            {
                if (((CheckBox)item.Cells[4].FindControl("cbBlending")).Checked)
                {
                    total = total + Convert.ToDecimal(item.Cells[6].Text);
                }
            }
        }

        if (gvBlendPreorder.Rows.Count > 0)
        {
            foreach (GridViewRow item in gvBlendPreorder.Rows)
            {
                if (((CheckBox)item.Cells[2].FindControl("cbBlendingPreorder")).Checked)
                {
                    total = total + Convert.ToDecimal(item.Cells[1].Text);

                }
            }
        }

        lblAlrBlendQty.Text = total.ToString("F");
        lblBlendQty.Text = (Convert.ToDecimal(lblNetQty.Text) - total).ToString("F");

    }
    protected void btnBlendSubmit_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        string collectionID = string.Empty;
        if (dtcollec != null && dtcollec.Rows.Count > 0)
        {
            collectionID = dtcollec.Rows[0]["CollectionID"].ToString();
        }
        if (lblLotNum.Text == string.Empty || lblLotNum.Text == null)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz click the Generate the Lot Button');</script>");
        }
        else
        {
            decimal alreadyBlendQty = 0.0M;
            bool result = false;
            string FarmerId = string.Empty;
            string FarmId = string.Empty;
            string PlantationId = string.Empty;
            string BlendDt = string.Empty;
            string BatchNo = string.Empty;
            int BlendID = 0;
            int ProductID = Convert.ToInt32(ddlBlendProduct.SelectedValue);
            if (gvBlending.Rows.Count <= 0 && gvBlendPreorder.Rows.Count <= 0)
            {
                
            }
            else
            {
                if (gvBlending.Rows.Count > 0)
                {
                    foreach (GridViewRow Row in gvBlending.Rows)
                    {
                        if (((CheckBox)gvBlending.Rows[Row.RowIndex].Cells[0].FindControl("cbBlending")).Checked)
                        {
                            alreadyBlendQty = alreadyBlendQty + Convert.ToDecimal(((TextBox)gvBlending.Rows[Row.RowIndex].Cells[0].FindControl("txtBlendingQty")).Text);// Convert.ToDecimal(gvBlending.Rows[Row.RowIndex].Cells[6].Text);
                            BlendDt += ((TextBox)gvBlending.Rows[Row.RowIndex].Cells[0].FindControl("txtBlendingQty")).Text + ";";
                            BatchNo += gvBlending.Rows[Row.RowIndex].Cells[5].Text + ";";
                            DataKey dk = gvBlending.DataKeys[Row.RowIndex];
                            FarmerId += dk.Values["FarmerID"].ToString() + ";";
                            FarmId += dk.Values["FarmID"].ToString() + ";";
                        }
                    }
                }
                if (gvBlendPreorder.Rows.Count > 0)
                {
                    foreach (GridViewRow Row in gvBlendPreorder.Rows)
                    {
                        if (((CheckBox)gvBlendPreorder.Rows[Row.RowIndex].Cells[2].FindControl("cbBlendingPreorder")).Checked)
                        {
                            string blendingBatchId = Convert.ToString(gvBlendPreorder.Rows[Row.RowIndex].Cells[0].Text.Trim());
                            DataTable dtPreordercollection = orderObj.GetPreorderDetails(blendingBatchId);
                            if (dtPreordercollection.Rows.Count > 0)
                            {
                                string preorderFarmerid = Convert.ToString(dtPreordercollection.Rows[0]["FarmerID"]);
                                if (FarmerId.Contains("@"))
                                {
                                    FarmerId = FarmerId + preorderFarmerid;
                                }
                                else
                                {
                                    FarmerId = FarmerId + "@" + preorderFarmerid;
                                }

                                string preorderFarmid = Convert.ToString(dtPreordercollection.Rows[0]["FarmID"]);
                                if (FarmId.Contains("@"))
                                {
                                    FarmId = FarmId + preorderFarmid;
                                }
                                else
                                {
                                    FarmId = FarmId + "@" + preorderFarmid;
                                }
                            }
                            alreadyBlendQty = alreadyBlendQty + Convert.ToDecimal(((TextBox)gvBlendPreorder.Rows[Row.RowIndex].Cells[0].FindControl("txtPreBlendingQty")).Text);
                            if (BlendDt.Contains("@"))
                            {
                                BlendDt += ((TextBox)gvBlendPreorder.Rows[Row.RowIndex].Cells[0].FindControl("txtPreBlendingQty")).Text + ";";
                            }
                            else
                            {
                                BlendDt = BlendDt + "@" + ((TextBox)gvBlendPreorder.Rows[Row.RowIndex].Cells[0].FindControl("txtPreBlendingQty")).Text + ";";
                            }
                            if (BatchNo.Contains("@"))
                            {
                                BatchNo += gvBlendPreorder.Rows[Row.RowIndex].Cells[0].Text + ";";
                            }
                            else
                            {
                                BatchNo = BatchNo + "@" + gvBlendPreorder.Rows[Row.RowIndex].Cells[0].Text + ";";
                            }
                        }
                    }
                }
                DataTable dtcheck = orderObj.GetBlendDetailsBasedonBlendID(collectionID);

                if (dtcheck.Rows.Count > 0)
                {
                    BlendID = Convert.ToInt32(dtcheck.Rows[0]["BlendingID"]);
                    result = true;
                }
                else
                {
                    result = orderObj.ProductsBlending_Insert(ref BlendID, Convert.ToInt32(collectionID), "Aslam", string.Empty, MudarApp.Insert);
                }
                if (result)
                {
                    result = orderObj.ProductsBlendingTran_Insert(BlendID, ProductID, FarmerId, "0", "0", "0", BlendDt, FarmId, BatchNo, lblLotNum.Text, alreadyBlendQty, "Aslam", DateTime.Now,string.Empty, MudarApp.Insert, string.Empty);
                    if (CheckBlendPhaseCompleted(Convert.ToInt32(OrderID), collectionID, Convert.ToString(BlendID)))
                    {
                        //orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), "TESTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "TESTING");
                        orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "BLENDING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "BLENDING");
                        Session["POStatus"] = "BLENDING";

                        lblLotNum.Text = string.Empty;
                        DataTable dtProducts = orderObj.ListOrderProducts(Convert.ToInt32(OrderID));
                        if (dtProducts.Rows.Count > 0 && dtProducts.AsEnumerable().Any(m => m.Field<int>("ProductID") == 4))
                        {
                            BindFreeze();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('FREEZING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                        }
                        ddlBlendProduct.SelectedIndex = 0;
                        ddlBlendProduct_SelectedIndexChanged(sender, e);
                        return;
                    }
                    else
                    {
                        BindBlendingDetails(ddlBlendProduct.SelectedValue);
                        lblLotNum.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                        ddlBlendProduct_SelectedIndexChanged(sender, e);
                        return;
                    }
                }
            }
            return;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void cbBlending_CheckedChanged1(object sender, EventArgs e)
    {
        //lblBlendQty.Visible = true;
        //double Blend = 0.00;
        //foreach (GridViewRow Row in gvBlending.Rows)
        //{
        //    if (((CheckBox)gvBlending.Rows[Row.RowIndex].Cells[0].FindControl("cbBlending")).Checked)
        //    {
        //        Blend += Convert.ToDouble(gvBlending.Rows[Row.RowIndex].Cells[6].Text);
        //    }
        //}
        //lblBlendQty.Text = Blend.ToString();
        //lblAlrBlendQty.Text = Blend.ToString();
    }
    protected void cbBlendingPreorder_CheckedChanged1(object sender, EventArgs e)
    {
        //lblBlendQty.Visible = true;
        //double Blend1 = 0;
        //foreach (GridViewRow row in gvBlendPreorder.Rows)
        //{
        //    CheckBox ChkBoxRows = (CheckBox)row.FindControl("cbBlendingPreorder");
        //    if (ChkBoxRows.Checked)
        //        Blend1 += Convert.ToDouble(gvBlendPreorder.Rows[row.RowIndex].Cells[1].Text);
        //}
        //lblBlendQty.Text = Blend1.ToString();
        //if (Convert.ToDecimal(lblBlendQty.Text) + Convert.ToInt32(lblAlrBlendQty.Text) == Convert.ToInt32(lblNetQty.Text))
        //    lblBlendQty.Text = (Convert.ToInt32(lblBlendQty.Text) + Convert.ToInt32(lblAlrBlendQty.Text)).ToString();
    }
    protected void btnGenerateBatchID_Click(object sender, EventArgs e)
    {
        MudarApp APP = new MudarApp();
        DataTable dt = pr.GetProductCode(Convert.ToInt32(ddlSelectProduct.SelectedValue));
        string year = DateTime.Now.Year.ToString();
        string[] yy = year.Split('2');
        string[] yy2 = yy[0].Split('0');
        lblBatchID.Text = APP.GenerateLotNumber(dt.Rows[0]["ProductCode"].ToString(), yy2[0].ToString());
        if (!string.IsNullOrEmpty(lblBatchID.Text))
            btnGenerateBatchID.Visible = false;
        //lblBatchID.Text = APP.GenerateBatchID(Convert.ToInt32(ddlSelectProduct.SelectedValue));
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void GetCurrentBlendInformation(DataTable newBlend, ref DataTable farmerBlend, ref DataTable preorderBlend)
    {

        DataTable blendInfo = new DataTable();
        blendInfo.Columns.Add("FarmID");
        blendInfo.Columns.Add("Quantity");

        DataTable preorderBlendInfo = new DataTable();
        preorderBlendInfo.Columns.Add("LotNumber");
        preorderBlendInfo.Columns.Add("Quantity");

        foreach (DataRow item in newBlend.Rows)
        {
            string[] farmIds = new string[] { };
            string[] farmQtys = new string[] { };
            string[] lotNums = new string[] { };
            string[] preorderQtys = new string[] { };
            string farmIdStr = Convert.ToString(item["FarmID"]);
            if (!string.IsNullOrEmpty(farmIdStr))
            {
                if (farmIdStr.Contains("@"))
                {
                    farmIdStr = farmIdStr.Split('@')[0];
                }


                if (farmIdStr.Contains(";"))
                {
                    farmIds = farmIdStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            string farmerQtyStr = Convert.ToString(item["BlendQty"]);
            if (!string.IsNullOrEmpty(farmerQtyStr))
            {
                if (farmerQtyStr.Contains("@"))
                {
                    farmerQtyStr = farmerQtyStr.Split('@')[0];
                }


                if (farmerQtyStr.Contains(";"))
                {
                    farmQtys = farmerQtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            if (farmQtys.Length > 0 && farmIds.Length > 0)
            {
                for (int i = 0; i < farmIds.Length; i++)
                {
                    DataRow dr = blendInfo.NewRow();
                    dr["FarmID"] = farmIds[i];
                    dr["Quantity"] = farmQtys[i];
                    blendInfo.Rows.Add(dr);
                }
            }

            string lotnumberStr = Convert.ToString(item["Lotnumber"]);
            if (!string.IsNullOrEmpty(lotnumberStr))
            {
                if (lotnumberStr.Contains("@"))
                {
                    lotnumberStr = lotnumberStr.Split('@')[1];
                }

                if (lotnumberStr.Contains(";"))
                {
                    lotNums = lotnumberStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            string preorderQtyStr = Convert.ToString(item["BlendQty"]);
            if (!string.IsNullOrEmpty(preorderQtyStr))
            {
                if (preorderQtyStr.Contains("@"))
                {
                    preorderQtyStr = preorderQtyStr.Split('@')[1];
                }
                else
                {
                    preorderQtyStr = string.Empty;
                }

                if (preorderQtyStr.Contains(";"))
                {
                    preorderQtys = preorderQtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            if (lotNums.Length > 0 && preorderQtys.Length > 0)
            {
                for (int i = 0; i < lotNums.Length; i++)
                {
                    DataRow dr = preorderBlendInfo.NewRow();
                    dr["LotNumber"] = lotNums[i];
                    dr["Quantity"] = preorderQtys[i];
                    preorderBlendInfo.Rows.Add(dr);
                }
            }
        }

        //DataTable copyDT = farmerBlend.Clone();

        if (farmerBlend.Rows.Count > 0)
        {
            DataTable copyDT = farmerBlend.Copy();
            var dupData = copyDT.AsEnumerable()
                     .GroupBy(row => row["FarmID"])
                     .Select(g => new { FarmID = g.Key, Quantity = g.Sum(m => m.Field<decimal>("CollectionQty")) }).ToList();

            foreach (var drItem in dupData)
            {
                var dupItem = copyDT.AsEnumerable().Where(m => m.Field<string>("FarmID") == Convert.ToString(drItem.FarmID)).FirstOrDefault();
                if (dupItem != null)
                {
                    dupItem["CollectionQty"] = drItem.Quantity;
                    farmerBlend.AsEnumerable().Where(m => m.Field<string>("FarmID") == Convert.ToString(drItem.FarmID)).ToList().ForEach(m => m.Delete());
                    farmerBlend.ImportRow(dupItem);
                }
            }

            foreach (DataRow drItem in blendInfo.Rows)
            {
                for (int fid = 0; fid < farmerBlend.Rows.Count; fid++)
                {
                    if (Convert.ToString(drItem["FarmID"]) == farmerBlend.Rows[fid]["FarmID"].ToString())
                    {
                        decimal farmerQuntity = Convert.ToDecimal(farmerBlend.Rows[fid]["CollectionQty"]);
                        decimal blendQuntity = Convert.ToDecimal(drItem["Quantity"]);

                        if (farmerQuntity - blendQuntity == 0)
                        {
                            farmerBlend.Rows[fid].Delete();
                            farmerBlend.AcceptChanges();
                            //fid -= 1;
                        }
                        else
                        {
                            farmerBlend.Rows[fid]["CollectionQty"] = farmerQuntity - blendQuntity;
                            farmerBlend.AcceptChanges();
                        }

                    }
                }
            }
        }

        if (preorderBlend.Rows.Count > 0)
        {
            DataTable copyDT = preorderBlend.Copy();
            var dupData = copyDT.AsEnumerable()
                     .GroupBy(row => row["Blending_BatchID"])
                     .Select(g => new { Blending_BatchID = g.Key, Quantity = g.Sum(m => m.Field<decimal>("CollectionQty")) }).ToList();

            foreach (var drItem in dupData)
            {
                var dupItem = copyDT.AsEnumerable().Where(m => m.Field<string>("Blending_BatchID") == Convert.ToString(drItem.Blending_BatchID)).FirstOrDefault();
                if (dupItem != null)
                {
                    dupItem["CollectionQty"] = drItem.Quantity;
                    preorderBlend.AsEnumerable().Where(m => m.Field<string>("Blending_BatchID") == Convert.ToString(drItem.Blending_BatchID)).ToList().ForEach(m => m.Delete());
                    preorderBlend.ImportRow(dupItem);
                }
            }

            foreach (DataRow drItem1 in preorderBlendInfo.Rows)
            {
                for (int fid = 0; fid < preorderBlend.Rows.Count; fid++)
                {
                    if (Convert.ToString(drItem1["LotNumber"]) == preorderBlend.Rows[fid]["Blending_BatchID"].ToString())
                    {
                        decimal farmerQuntity = Convert.ToDecimal(preorderBlend.Rows[fid]["CollectionQty"]);
                        decimal blendQuntity = Convert.ToDecimal(drItem1["Quantity"]);

                        if (farmerQuntity - blendQuntity == 0)
                        {
                            preorderBlend.Rows[fid].Delete();
                            preorderBlend.AcceptChanges();
                            fid -= 1;
                        }
                        else
                        {
                            preorderBlend.Rows[fid]["CollectionQty"] = farmerQuntity - blendQuntity;
                            preorderBlend.AcceptChanges();
                        }
                    }
                }
            }
        }


        //if (farmerBlend.Rows.Count > 0)
        //{
        //    string[] FarmerID = new string[] { };


        //    FarmerID = farmerIdStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //    if (FarmerID.Length > 0)
        //    {
        //        for (int i = 0; i < FarmerID.Length; i++)
        //        {
        //            for (int fid = 0; fid < farmerBlend.Rows.Count; fid++)
        //            {
        //                if (FarmerID[i] == farmerBlend.Rows[fid]["FarmID"].ToString())
        //                {
        //                    farmerBlend.Rows[fid].Delete();
        //                    farmerBlend.AcceptChanges();
        //                    fid -= 1;
        //                }
        //            }
        //        }
        //    }
        //}

        //if (preorderBlend.Rows.Count > 0)
        //{
        //    DataTable copyDT = preorderBlend.Copy();
        //    var dupData = copyDT.AsEnumerable()
        //             .GroupBy(row => row["Blending_BatchID"])
        //             .Select(g => new { Blending_BatchID = g.Key, Quantity = g.Sum(m => m.Field<decimal>("CollectionQty")) }).ToList();

        //    foreach (var drItem in dupData)
        //    {
        //        var dupItem = copyDT.AsEnumerable().Where(m => m.Field<string>("Blending_BatchID") == Convert.ToString(drItem.Blending_BatchID)).FirstOrDefault();
        //        if (dupItem != null)
        //        {
        //            dupItem["CollectionQty"] = drItem.Quantity;
        //            preorderBlend.AsEnumerable().Where(m => m.Field<string>("Blending_BatchID") == Convert.ToString(drItem.Blending_BatchID)).ToList().ForEach(m => m.Delete());
        //            preorderBlend.Rows.Add(dupItem);
        //        }
        //    }

        //    foreach (DataRow drItem in preorderBlendInfo.Rows)
        //    {
        //        for (int fid = 0; fid < preorderBlend.Rows.Count; fid++)
        //        {
        //            if (Convert.ToString(drItem["LotNumber"]) == preorderBlend.Rows[fid]["FarmID"].ToString())
        //            {
        //                decimal farmerQuntity = Convert.ToDecimal(preorderBlend.Rows[fid]["CollectionQty"]);
        //                decimal blendQuntity = Convert.ToDecimal(drItem["CollectionQty"]);

        //                if (farmerQuntity - blendQuntity == 0)
        //                {
        //                    farmerBlend.Rows[fid].Delete();
        //                    farmerBlend.AcceptChanges();
        //                }
        //                else
        //                {
        //                    farmerBlend.Rows[fid]["CollectionQty"] = farmerQuntity - blendQuntity;
        //                    farmerBlend.AcceptChanges();
        //                }
        //                fid -= 1;
        //            }
        //        }
        //    }

        //    string[] Lotnumber = new string[] { };
        //    if (!string.IsNullOrEmpty(lotnumberStr))
        //        Lotnumber = lotnumberStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //    if (Lotnumber.Length > 0)
        //    {
        //        for (int i = 0; i < Lotnumber.Length; i++)
        //        {
        //            for (int fid = 0; fid < preorderBlend.Rows.Count; fid++)
        //            {
        //                if (Lotnumber[i] == preorderBlend.Rows[fid]["Blending_BatchID"].ToString())
        //                {

        //                    //decimal farmerQuntity = Convert.ToDecimal(farmerBlend.Rows[fid]["CollectionQty"]);
        //                    //decimal blendQuntity = Convert.ToDecimal(drItem["CollectionQty"]);

        //                    //if (farmerQuntity - blendQuntity == 0)
        //                    //{
        //                    //    farmerBlend.Rows[fid].Delete();
        //                    //    farmerBlend.AcceptChanges();
        //                    //}
        //                    //else
        //                    //{
        //                    //    farmerBlend.Rows[fid]["CollectionQty"] = farmerQuntity - blendQuntity;
        //                    //    farmerBlend.AcceptChanges();
        //                    //}

        //                    preorderBlend.Rows[fid].Delete();
        //                    preorderBlend.AcceptChanges();
        //                    fid -= 1;
        //                }
        //            }
        //        }
        //    }
        //}

    }
    private void BindNewBlendingDetails(int BlendID, string CollectID, DataTable newBlend)
    {
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("FarmerID");
        DataTable dtOldBlend = Session["Blend"] as DataTable;
        DataTable dtNewBlend = new DataTable();
        if (newBlend == null)
        {
            dtNewBlend = orderObj.GetBlendDetailsBasedonBlendID(BlendID.ToString(), lblCollectionID.Text, ddlBlendProduct.SelectedValue);
        }
        else
        {
            dtNewBlend = newBlend;
        }
        if (dtNewBlend.Rows.Count > 0)
        {
            string[] FarmerID = dtNewBlend.Rows[0]["FarmerID"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (FarmerID.Length > 0)
            {
                for (int count = 0; count < FarmerID.Length; count++)
                    for (int fid = 0; fid < dtOldBlend.Rows.Count; fid++)
                    {
                        if (FarmerID[count] == dtOldBlend.Rows[fid]["FarmerID"].ToString())
                        {
                            dtOldBlend.Rows[fid].Delete();
                            dtOldBlend.AcceptChanges();
                            fid -= 1;
                        }
                    }
            }
            if (dtOldBlend.Rows.Count > 0)
            {
                gvBlending.DataSource = dtOldBlend;
                gvBlending.DataBind();
                divBlendDetails.Visible = true;
                lblNetQty.Visible = true;
                lblBlendQty.Visible = true;
            }
            else
            {
                gvBlending.DataSource = null;
                gvBlending.DataBind();
                divBlendDetails.Visible = false;
                lblNetQty.Visible = false;
                lblBlendQty.Visible = false;
                lblAlrBlendQty.Visible = false;
                BindddlBlendProductDetails();
            }
        }
    }
    protected void btnGenLot_Click(object sender, EventArgs e)
    {
        int productID = Convert.ToInt32(hfProductID.Value);
        DataTable dtDate = new DataTable();
        DataTable dt = new DataTable();
        if (productID == 4)
        {
            dtDate = settObj.GetStandDetails("2", (DateTime.Now.Year - 1).ToString());
            dt = pr.GetProductCode(2);
        }
        else
        {
            dtDate = settObj.GetStandDetails(productID.ToString(), (DateTime.Now.Year - 1).ToString());
            dt = pr.GetProductCode(productID);
        }
        if (dtDate.Rows.Count > 0)
        {
            DataTable dtFinyear = set.GetLotYear(Convert.ToDateTime(dtDate.Rows[0]["Date"].ToString()));
            if (dtFinyear.Rows.Count > 0)
            {
                lblLotNum.Text = APP.GenerateLotNumber(dt.Rows[0]["ProductCode"].ToString(), dtFinyear.Rows[0]["FYear"].ToString());
                UpdateSelectedBlendingQuantityLabels();
                btnGenLot.Visible = false;
                btnDisableGenLot.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
            }
        }
        else
            return;
    }
    private void BindddlBlendProductDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int branchOrderID = Convert.ToInt32(lblBranchOrderID.Text);
        ddlBlendProduct.DataSource = orderObj.OrderProductList(Convert.ToInt32(OrderID), branchOrderID);
        ddlBlendProduct.DataTextField = "ProductName";
        ddlBlendProduct.DataValueField = "ProductID";
        ddlBlendProduct.DataBind();
        ddlBlendProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    protected void gvBlending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvr = gvBlending.Rows[index];

        switch (e.CommandName)
        {
            case "Blending":
                {
                    MudarApp APP = new MudarApp();
                    int productID = Convert.ToInt32((gvr.Cells[0].FindControl("lblBBatchID") as HiddenField).Value);
                    DataTable dt = pr.GetProductCode(productID);
                    string year = DateTime.Now.Year.ToString();
                    string[] yy = year.Split('2');
                    string[] yy2 = yy[1].Split('0');
                    int plus = Convert.ToInt32(yy2[1].ToString()) + 1;
                    string finyear = yy2[1].ToString() + plus.ToString();
                    (gvr.Cells[0].FindControl("lblBBatchID") as Label).Text = APP.GenerateLotNumber(dt.Rows[0]["ProductCode"].ToString(), finyear);
                }
                break;
        }
        CheckBlendingDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void CheckBlendingDetails()
    {
        for (int index = 0; index < gvBlending.Rows.Count; index++)
        {
            GridViewRow gvr = gvBlending.Rows[index];
            if (!string.IsNullOrEmpty((gvr.Cells[0].FindControl("lblBBatchID") as Label).Text))
            {
                (gvr.Cells[0].FindControl("lblBBatchID") as Label).Visible = true;
                (gvr.Cells[9].Controls[0] as Button).Visible = false;
            }
            else
            {
                (gvr.Cells[0].FindControl("lblBBatchID") as Label).Visible = false;
                //(gvr.Cells[0].FindControl("btnBBatchID") as Button).Visible = true;
            }
        }
    }
    private void BindBlendingDetails(string ProductID)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        double BlendQTY = 0.00;
        decimal netQty = 0.0M;
        decimal totalQty = 0.0M;
        lblBlendQty.Text = "0";
        DataTable dtBranchOrder = new DataTable();
        dtBranchOrder = orderObj.GetBranchOrderDetails(OrderID, Convert.ToInt32(ProductID));
        lblNetQty.Visible = true;
        DataTable dtmer = new DataTable();
        if (Convert.ToInt32(ProductID) == 4)
        {
            netQty = Convert.ToDecimal(dtBranchOrder.Rows[0]["NetQuantity"].ToString());
            dtmer = set.GetMentholPerDetails(2);
            decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
            decimal d1 = Math.Round(((netQty * 100) / mer), 0);
            lblNetQty.Text = d1.ToString("F");
            netQty = d1;
        }
        else
        {
            netQty = Convert.ToDecimal(dtBranchOrder.Rows[0]["NetQuantity"].ToString());
            netQty = Math.Round(netQty, 2);
            lblNetQty.Text = netQty.ToString("F");
        }
        DataTable dtAlreadyBlend = new DataTable();
        DataTable dtNewBlend = new DataTable();
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        string collectionId = string.Empty;
        if (dtcollec.Rows.Count > 0)
        {
            collectionId = dtcollec.Rows[0]["CollectionID"].ToString();
        }
        DataTable dtBlending = orderObj.GetBliendingDetails(collectionId, ProductID);
        if (dtBlending.Rows.Count > 0)
        {
            DataTable dtcheck = orderObj.GetBlendDetailsBasedonBlendID(collectionId);

            if (dtcheck.Rows.Count > 0)
            {
                dtNewBlend = orderObj.GetBlendDetailsBasedonBlendID(dtcheck.Rows[0]["BlendingID"].ToString(), collectionId, ddlBlendProduct.SelectedValue);
            }

            if (dtcheck.Rows.Count > 0 && dtNewBlend.Rows.Count > 0)
            {

                dtAlreadyBlend.Columns.Add("Blending_BatchID");
                dtAlreadyBlend.Columns.Add("BlendQty", typeof(decimal));
                foreach (DataRow item in dtNewBlend.Rows)
                {
                    decimal blendQty = 0.0M;
                    string qtyStr = Convert.ToString(item["BlendQty"]);
                    if (!string.IsNullOrEmpty(qtyStr))
                    {
                        qtyStr = qtyStr.Trim();
                        string[] farmerQtyArray = new string[] { };
                        string[] preorderQtyArray = new string[] { };
                        if (qtyStr.Contains("@"))
                        {
                            string[] qtyArray = qtyStr.Split('@');
                            farmerQtyArray = qtyArray[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            preorderQtyArray = qtyArray[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else
                        {
                            farmerQtyArray = qtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        }

                        if (farmerQtyArray.Length > 0)
                            blendQty = blendQty + farmerQtyArray.Sum(m => Convert.ToDecimal(m));
                        if (preorderQtyArray.Length > 0)
                            blendQty = blendQty + preorderQtyArray.Sum(m => Convert.ToDecimal(m));
                    }
                    DataRow drBlendNew = dtAlreadyBlend.NewRow();
                    drBlendNew["Blending_BatchID"] = Convert.ToString(item["Blending_BatchID"]);
                    drBlendNew["BlendQty"] = blendQty;
                    dtAlreadyBlend.Rows.Add(drBlendNew);
                }

                gvBlendCompleted.DataSource = dtAlreadyBlend;
                gvBlendCompleted.DataBind();

                totalQty = dtAlreadyBlend.AsEnumerable().Sum(m => m.Field<Decimal>("BlendQty"));
                hdnBlendingQuantity.Value = totalQty.ToString("F");
                if (totalQty == netQty)
                {
                    gvBlending.DataSource = null;
                    gvBlending.DataBind();
                    gvBlendPreorder.DataSource = null;
                    gvBlendPreorder.DataBind();
                    divBlendDetails.Visible = false;
                   
                    return;
                }
            }
            else
            {
                gvBlendCompleted.DataSource = null;
                gvBlendCompleted.DataBind();
                divCompletedBlendDetails.Visible = false;
            }

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("FarmerID");
            dtNew.Columns.Add("FarmID");
            dtNew.Columns.Add("FarmerName");
            dtNew.Columns.Add("Farmercode");
            dtNew.Columns.Add("PlotCode");
            dtNew.Columns.Add("Lotnumber");
            dtNew.Columns.Add("CollectionQty", typeof(decimal));

            DataTable dtPreOrder = new DataTable();
            dtPreOrder.Columns.Add("Blending_BatchID");
            dtPreOrder.Columns.Add("CollectionQty", typeof(decimal));

            foreach (DataRow item in dtBlending.Rows)
            {

                DataTable dtNewClone = dtNew.Clone();
                DataTable dtPreOrderClone = dtPreOrder.Clone();

                string[] FarmerID = item["FarmerID"].ToString().Split('@');
                string[] FarmID = item["FarmID"].ToString().Split('@');
                string[] Lotnumber = item["Lotnumber"].ToString().Split('@');
                string[] CollectionQty = item["CollectionQty"].ToString().Split('@');
                string[] BlendingBID = item["Blending_BatchID"].ToString().Split('@');
                string blendingBatchID = item["Blending_BatchID"].ToString();
                if (!string.IsNullOrEmpty(blendingBatchID) && blendingBatchID.Contains("@"))
                {
                    //hfProductID.Value = item["ProductID"].ToString();
                    //lblCollectionID.Text = item["CollectionID"].ToString();


                    string[] FarmerID1 = FarmerID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] FarmID1 = FarmID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] Lotnumber1 = Lotnumber[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] CollectionQty1 = CollectionQty[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < FarmerID1.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(FarmerID1[i].ToString()))
                        {
                            DataRow drNew = dtNewClone.NewRow();
                            drNew["FarmerID"] = FarmerID1[i].ToString();
                            drNew["FarmID"] = FarmID1[i].ToString();
                            drNew["Lotnumber"] = Lotnumber1[i].ToString();
                            drNew["CollectionQty"] = CollectionQty1[i].ToString();
                            BlendQTY = BlendQTY + Convert.ToDouble(CollectionQty1[i].ToString());
                            DataTable dtFarmer = orderObj.GetBlendFarmerDetails(FarmerID1[i].ToString(), FarmID1[i].ToString());
                            drNew["PlotCode"] = dtFarmer.Rows[0]["AreaCode"].ToString();
                            drNew["FarmerName"] = dtFarmer.Rows[0]["FirstName"].ToString();
                            drNew["Farmercode"] = dtFarmer.Rows[0]["FarmerCode"].ToString();
                            dtNewClone.Rows.Add(drNew);
                        }
                    }
                    //gvBlending.DataSource = dtNew;
                    //gvBlending.DataBind();

                    if (BlendingBID[1].ToString() != string.Empty)
                    {

                        //dtPreOrder = orderObj.GetCollectionBlendDetails(dtBlending.Rows[0]["Blending_BatchID"].ToString());
                        string[] BlendingBID2 = BlendingBID[1].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] CollectionQty2 = CollectionQty[1].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < BlendingBID2.Length; j++)
                        {
                            if (!string.IsNullOrEmpty(BlendingBID2[j].ToString()))
                            {
                                DataRow drNew = dtPreOrderClone.NewRow();
                                drNew["Blending_BatchID"] = BlendingBID2[j].ToString();
                                drNew["CollectionQty"] = CollectionQty2[j].ToString();
                                BlendQTY += Convert.ToInt32(CollectionQty2[j].ToString());
                                dtPreOrderClone.Rows.Add(drNew);
                            }
                        }
                        //gvBlendPreorder.DataSource = dtPreOrder;
                        //gvBlendPreorder.DataBind();
                    }
                }
                else if (!string.IsNullOrEmpty(blendingBatchID) && !blendingBatchID.Contains("@"))
                {
                    DataTable dtNew1 = new DataTable();


                    //dtPreOrder = orderObj.GetCollectionBlendDetails(dtBlending.Rows[0]["Blending_BatchID"].ToString());
                    string[] BlendingBID2 = blendingBatchID.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] CollectionQty2 = CollectionQty[1].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < BlendingBID2.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(BlendingBID2[j].ToString()))
                        {
                            DataRow drNew = dtPreOrderClone.NewRow();
                            drNew["Blending_BatchID"] = BlendingBID2[j].ToString();
                            drNew["CollectionQty"] = CollectionQty2[j].ToString();
                            BlendQTY += Convert.ToInt32(CollectionQty2[j].ToString());
                            dtPreOrderClone.Rows.Add(drNew);
                        }
                    }

                    //GetCurrentBlendInformation(dtNewBlend, ref dtNew1, ref dtPreOrder);

                    //gvBlending.DataSource = null;
                    //gvBlending.DataBind();

                    //gvBlendPreorder.DataSource = dtPreOrder;
                    //gvBlendPreorder.DataBind();

                }
                foreach (DataRow row in dtNewClone.Rows)
                {
                    dtNew.ImportRow(row);
                }
                foreach (DataRow row in dtPreOrderClone.Rows)
                {
                    dtPreOrder.ImportRow(row);
                }
            }
            GetCurrentBlendInformation(dtNewBlend, ref dtNew, ref dtPreOrder);
            gvBlending.DataSource = dtNew;
            gvBlending.DataBind();

            gvBlendPreorder.DataSource = dtPreOrder;
            gvBlendPreorder.DataBind();

            divCompletedBlendDetails.Visible = true;

            if (dtNew.Rows.Count > 0 || dtPreOrder.Rows.Count > 0)
            {

                divBlendDetails.Visible = true;

                lblBlendQty.Style.Add("display", "block");
                lblAlrBlendQty.Visible = true;
                //lblBlendQty.Text = netQty .ToString("F");
                lblBlendQty.Text = (netQty - totalQty).ToString("F");
                lblAlrBlendQty.Text = totalQty.ToString("F");
            }
            else
            {
                divBlendDetails.Visible = false;

            }


        }
        else
        {
            //ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Blend is over in the PreOrder Collection');</script>");
        }
    }
    protected void gvBlending_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvr = gvBlending.Rows[index];
        switch (e.CommandName)
        {
            case "Blending":
                {
                    MudarApp APP = new MudarApp();
                    int productID = Convert.ToInt32((gvr.Cells[0].FindControl("hfProductID") as HiddenField).Value);
                    DataTable dt = pr.GetProductCode(productID);
                    string year = DateTime.Now.Year.ToString();
                    string[] yy = year.Split('2');
                    string[] yy2 = yy[1].Split('0');
                    int plus = Convert.ToInt32(yy2[1].ToString()) + 1;
                    string finyear = yy2[1].ToString() + plus.ToString();
                    (gvr.Cells[0].FindControl("lblBBatchID") as Label).Text = APP.GenerateLotNumber(dt.Rows[0]["ProductCode"].ToString(), finyear);
                    int CTID = Convert.ToInt32(gvBlending.DataKeys[gvr.RowIndex].Value);
                    bool result = false;
                    result = orderObj.BlendingBatchNo(CTID, (gvr.Cells[0].FindControl("lblBBatchID") as Label).Text, "Aslam", MudarApp.Update);
                    //BindgvPackingDetails();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "');", true);
                }
                break;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        //BindBlendingDetails();
    }
    protected void ddlBlendProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlBlendProduct.SelectedValue))
        {
            hdnBlendingQuantity.Value = "0";
            divBlendDetails.Visible = true;
            divCompletedBlendDetails.Visible = true;
            BindBlendingDetails(ddlBlendProduct.SelectedValue);
            hfProductID.Value = ddlBlendProduct.SelectedValue;
            //BindTestingDetails(ddlTestProduct);
            btnGenLot.Visible = true;
            btnDisableGenLot.Visible = false;
            lblLotNum.Text = string.Empty;
        }
        else
        {
            divBlendDetails.Visible = false;
            divCompletedBlendDetails.Visible = false;
            gvBlendCompleted.DataSource = null;
            gvBlendCompleted.DataBind();

        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('BLENDING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    #endregion

    #region Testing
    protected void ddlTestProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlTestProduct.SelectedValue))
        {
            BindTestResults();
        }
        else
        {
            gvTesting.DataSource = null;
            gvTesting.DataBind();
            divTestingButtons.Visible = false;
            divTestResults.Visible = false;
            divTestingCompleted.Visible = false;
            gvTestingCompleted.DataSource = null;
            gvTestingCompleted.DataBind();

        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void BindTestingDetails(string ProductID)
    {
        DataTable dtTest = orderObj.GetTestFieldDataBasedonProduct(ProductID);
        DataColumn Blending_BatchId = new DataColumn("Blending_BatchId", typeof(string));
        Blending_BatchId.DefaultValue = string.Empty;
        dtTest.Columns.Add(Blending_BatchId);
        if (dtTest.Rows.Count > 0)
        {
            DataTable dtTestClone = dtTest.Clone();
            string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
            DataTable dtLotNumbers = orderObj.ListLotNumbers(Convert.ToInt32(OrderID), Convert.ToInt32(ProductID));
            foreach (DataRow item in dtLotNumbers.Rows)
            {
                foreach (DataRow drItem in dtTest.Rows)
                {
                    drItem["Blending_BatchId"] = Convert.ToString(item[0]);
                    dtTestClone.ImportRow(drItem);
                }
            }

            divTestResults.Visible = true;
            divTestingButtons.Visible = true;
            gvTesting.DataSource = dtTestClone;
            gvTesting.DataBind();
            Session["TestInfo"] = dtTestClone;
            btnTestinsDisable.Visible = false;
            btnTestingSubmit.Visible = true;
        }
    }
    protected void gvTesting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable ds = (DataTable)Session["TestInfo"];
            DataRow drp = ds.NewRow();
            DataRow drNew = ds.Rows[index];
            if (drNew != null)
            {
                drp["Blending_BatchId"] = Convert.ToString(drNew["Blending_BatchId"]);
            }
            ds.Rows.Add(drp);
            gvTesting.DataSource = ds;
            gvTesting.DataBind();
            ds = Session["TestInfo"] as DataTable;

        }
        if (e.CommandName == "Remove")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvr = gvTesting.Rows[index];
            if (index == 0)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! U Don't Have Permission... !!!');</script>");
                return;
            }
            else
            {
                (Session["TestInfo"] as DataTable).Rows.RemoveAt(index);
                gvTesting.DataSource = (Session["TestInfo"] as DataTable);
                gvTesting.DataBind();
            }
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnTestingSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        int collectionID = 0;
        if (dtcollec.Rows.Count > 0)
        {
            collectionID = Convert.ToInt32(dtcollec.Rows[0]["CollectionID"]);
        }
        int TestID = 0;
        DataTable dtTestingResult = orderObj.GetTestingResult(collectionID);
        if (dtTestingResult.Rows.Count > 0)
        {
            TestID = Convert.ToInt32(dtTestingResult.Rows[0]["TestID"]);
            result = true;
        }
        else
        {
            result = orderObj.TestingResults_INT_UPT_DEL(collectionID, "Bhanu", "Bhanu", MudarApp.Insert, ref TestID);
        }
        if (result == true)
        {
            foreach (GridViewRow Row in gvTesting.Rows)
            {
                string lotNumber = gvTesting.Rows[Row.RowIndex].Cells[0].Text;
                TextBox txtpara = (TextBox)gvTesting.Rows[Row.RowIndex].Cells[0].FindControl("txtPara");
                TextBox txtAValue = (TextBox)gvTesting.Rows[Row.RowIndex].Cells[0].FindControl("txtAValue");
                TextBox txtLow = (TextBox)gvTesting.Rows[Row.RowIndex].Cells[0].FindControl("txtLow");
                TextBox txtHigh = (TextBox)gvTesting.Rows[Row.RowIndex].Cells[0].FindControl("txtHigh");
                TextBox txtTmethod = (TextBox)gvTesting.Rows[Row.RowIndex].Cells[0].FindControl("txtTmethod");
                orderObj.TestingResultsTrans_INT_UPT_DEL(TestID, Convert.ToInt32(ddlTestProduct.SelectedValue), lotNumber, txtpara.Text, txtAValue.Text, txtLow.Text, txtHigh.Text, txtTmethod.Text, "Bhanu",DateTime.Now ,"Bhanu", MudarApp.Insert);
            }

            divTestResults.Visible = false;
            btnTestinsDisable.Visible = true;

            divTestingButtons.Visible = false;
            btnTestingSubmit.Visible = false;
            btnTestinsDisable.Visible = false;

            if (CheckTestingCompleted())
            {
                orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "TESTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "TESTING");
                Session["POStatus"] = "TESTING";
                if (Convert.ToString(Session["OrderType"]) == "LotSample")
                {
                    Response.Redirect("~/Mudar/BranchOrder.aspx");
                }
                else
                {
                    ddlTestProduct_SelectedIndexChanged(sender, e);
                    BindNewPackingDetail();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('PACKING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
                }
            }
            else
            {
                BindTestResults();
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        }


    }
    private void BindTestResults()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int collectionID = 0;
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        if (dtcollec.Rows.Count > 0)
        {
            collectionID = Convert.ToInt32(dtcollec.Rows[0]["CollectionID"]);
        }
        DataTable dtTestingResults = orderObj.ListTestResults(collectionID, Convert.ToInt32(ddlTestProduct.SelectedValue),"");
        if (dtTestingResults.Rows.Count > 0)
        {
            gvTestingCompleted.DataSource = dtTestingResults;
            gvTestingCompleted.DataBind();
            divTestingCompleted.Visible = true;
            divTestResults.Visible = false;
            divTestingButtons.Visible = false;
        }
        else
        {
            gvTestingCompleted.DataSource = null;
            gvTestingCompleted.DataBind();
            divTestingCompleted.Visible = false;
            BindTestingDetails(ddlTestProduct.SelectedValue);
        }
    }
    private void BindddlTestProductDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int branchOrderID = Convert.ToInt32(lblBranchOrderID.Text);
        ddlTestProduct.DataSource = orderObj.OrderProductList(Convert.ToInt32(OrderID), branchOrderID);
        ddlTestProduct.DataTextField = "ProductName";
        ddlTestProduct.DataValueField = "ProductID";
        ddlTestProduct.DataBind();
        ddlTestProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    private bool CheckTestingCompleted()
    {
        bool testingCompleted = true;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        int collectionID = 0;
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        if (dtcollec.Rows.Count > 0)
        {
            collectionID = Convert.ToInt32(dtcollec.Rows[0]["CollectionID"]);
        }

        DataTable dtProducts = orderObj.ListOrderProducts(Convert.ToInt32(OrderID));
        foreach (DataRow item in dtProducts.Rows)
        {
            DataTable dtTestingResults = orderObj.ListTestResults(collectionID, Convert.ToInt32(item["ProductID"]),"");
            if (dtTestingResults.Rows.Count <= 0)
            {
                testingCompleted = false;
                break;
            }
        }
        return testingCompleted;
    }
    #endregion

    #region Freeze
    private void BindFreeze()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtFreeze = orderObj.GetFreezeDetails(OrderID, !string.IsNullOrEmpty(lblBranchOrderID.Text) ? lblBranchOrderID.Text : "0");
        decimal netQty = 0.0M;
        DataSet dsFT = new DataSet();
        DataTable dtmer = new DataTable();
        string Pname = string.Empty;
        if (dtFreeze.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dtFreeze.Rows[0]["FreezeID"].ToString()))
            {
                dsFT = orderObj.GetFreezeTran(dtFreeze.Rows[0]["FreezeID"].ToString());
                if (dsFT.Tables[1].Rows.Count > 0)
                {
                    gvFreeze.DataSource = dtFreeze;
                    for (int i = 0; i < dtFreeze.Rows.Count; i++)
                    {
                        if (dtFreeze.Rows[i][3].ToString() == "4")
                        {
                            netQty = Convert.ToDecimal(dtFreeze.Rows[i]["Quantity"].ToString());
                            Pname = "Organic Cornmint Oil";
                            dtmer = set.GetMentholPerDetails(2);
                            decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                            decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                            dtFreeze.Rows[i][4] = d1.ToString();
                            dtFreeze.Rows[i][5] = Pname;
                            dtFreeze.Rows[i][7] = dtFreeze.Rows[i]["FBatchID"].ToString();
                        }
                    }
                    gvFreeze.DataBind();
                    gvFreeze.Rows[0].FindControl("btnFreez").Visible = false;
                    dlFreeze.DataSource = dsFT.Tables[0];
                    dlFreeze.DataBind();
                    (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dsFT.Tables[1];
                    (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
                    ((Button)dlFreeze.Items[0].FindControl("btnAddProduct")).Visible = false;
                    ((Button)dlFreeze.Items[0].FindControl("btnAddRemove")).Visible = false;
                    btnDisableFrzee.Visible = true;
                    btnFreezeSubmit.Visible = false;
                }
            }
            else
            {
                if (dtFreeze.Rows.Count > 0)
                {
                    gvFreeze.DataSource = dtFreeze;
                    for (int i = 0; i < dtFreeze.Rows.Count; i++)
                    {
                        if (dtFreeze.Rows[i][3].ToString() == "4")
                        {
                            netQty = Convert.ToDecimal(dtFreeze.Rows[i]["Quantity"].ToString());
                            Pname = "Organic Cornmint Oil";
                            dtmer = set.GetMentholPerDetails(2);
                            decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                            decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                            dtFreeze.Rows[i][4] = d1.ToString();
                            dtFreeze.Rows[i][5] = Pname;
                            dtFreeze.Rows[i][7] = dtFreeze.Rows[i]["FBatchID"].ToString();
                        }
                    }
                    gvFreeze.DataBind();
                }
                else
                {
                    btnFreezeSubmit.Visible = false;
                    btnDisableFrzee.Visible = false;
                }
            }
        }
        else
        {
            btnFreezeSubmit.Visible = false;
            btnDisableFrzee.Visible = false;
        }
    }
    protected void gvFreeze_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MudarApp APP = new MudarApp();
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        int orderid = Convert.ToInt32(Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true));
        bool result = false;
        DataKey dk = (sender as GridView).DataKeys[index];
        int fid = !string.IsNullOrEmpty(dk.Values["FreezeID"].ToString()) ? Convert.ToInt32(dk.Values["FreezeID"].ToString()) : 0;
        int pid = Convert.ToInt32(dk.Values["ProductID"].ToString());
        int qty = !string.IsNullOrEmpty((sender as GridView).Rows[index].Cells[1].Text) ? Convert.ToInt32((sender as GridView).Rows[index].Cells[1].Text) : 0;
        //int productID = Convert.ToInt32(hfProductID.Value);
        DataTable dt = pr.GetProductCode(pid);
        string year = DateTime.Now.Year.ToString();
        string[] yy = year.Split('2');
        string[] yy2 = yy[1].Split('0');
        int plus = Convert.ToInt32(yy2[1].ToString()) + 1;
        string finyear = yy2[1].ToString() + plus.ToString();
        string fBatchID = APP.GenerateFBatchID(pid);
        switch (e.CommandName)
        {
            case "Freeze":
                {
                    if (fid == 0)
                    {
                        result = orderObj.Freeze_Insert(orderid, Convert.ToInt32(lblBranchOrderID.Text), "Aslam", string.Empty, 0, pid, fBatchID, MudarApp.Insert, DateTime.Now, DateTime.Now, 0, ref fid);
                        BindFreezeTransaction(fid.ToString(), pid.ToString());
                    }
                }
                break;
        }
        BindFreeze();
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('FREEZING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void BindFreezeTransaction(string FID, string ProductID)
    {
        DataTable dtmer = new DataTable();
        DataSet dsFT = orderObj.GetFreezeTran(FID, ProductID);
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtFreeze = orderObj.GetFreezeDetails(OrderID, !string.IsNullOrEmpty(lblBranchOrderID.Text) ? lblBranchOrderID.Text : "0");
        decimal netQty = 0.0M;
        string Pname = string.Empty;
        if (dsFT.Tables[0].Rows.Count > 0)
        {
            gvFreeze.DataSource = dtFreeze;
            for (int i = 0; i < dtFreeze.Rows.Count; i++)
            {
                if (dtFreeze.Rows[i][3].ToString() == "4")
                {
                    netQty = Convert.ToDecimal(dtFreeze.Rows[i]["Quantity"].ToString());
                    Pname = "Organic Cornmint Oil";
                    if (dtmer.Rows.Count > 0)
                    {
                        dtmer = set.GetMentholPerDetails(2);
                        decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                        decimal d1 = Math.Round(((netQty * 100) / mer), 0);
                        dtFreeze.Rows[i][4] = d1.ToString();
                        dtFreeze.Rows[i][5] = Pname;
                        dtFreeze.Rows[i][7] = dtFreeze.Rows[i]["FBatchID"].ToString();
                    }
                    
                }
            }
            gvFreeze.DataBind();
        }
        dlFreeze.DataSource = dsFT.Tables[0];
        dlFreeze.DataBind();
        if (dlFreeze.Items.Count > 0)
        {
            if (dsFT.Tables[1].Rows.Count > 0)
            {
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dsFT.Tables[1];
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
            }
            else
            {
                DataTable dtFreeProd = orderObj.BindFreezeProduct();
                if (dtFreeProd.Rows.Count > 0)
                {
                    for (int j = 0; j < dtFreeProd.Rows.Count; j++)
                    {
                        DataRow dr = dsFT.Tables[1].NewRow();
                        if (j == 0)
                        {
                            dr["FreezeTransactionID"] = 0;
                            dr["ProductId"] = dtFreeProd.Rows[0]["ProductId"].ToString();
                            dr["FreezeProductName"] = dtFreeProd.Rows[0]["ProductName"].ToString();
                            dtmer = set.GetMentholPerDetails(2);
                            if (dtmer.Rows.Count > 0)
                            {
                                decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                                decimal Qty = Math.Round(((Convert.ToDecimal(dsFT.Tables[0].Rows[0]["Quntatiy"])) * mer / 100), 0);
                                dr["FreezeQuantity"] = Qty.ToString();
                            }
                        }
                        else
                        {
                            dr["FreezeTransactionID"] = 0;
                            dr["ProductId"] = dtFreeProd.Rows[1]["ProductId"].ToString();
                            dr["FreezeProductName"] = dtFreeProd.Rows[1]["ProductName"].ToString();
                            dtmer = set.GetMentholPerDetails(2);
                            if (dtmer.Rows.Count > 0)
                            {
                                decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                                decimal Qty = Math.Round(((Convert.ToDecimal(dsFT.Tables[0].Rows[0]["Quntatiy"])) * (100 - mer) / 100), 0);
                                dr["FreezeQuantity"] = Qty.ToString();
                            }
                        }
                        dsFT.Tables[1].Rows.Add(dr);
                    }
                }
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dsFT.Tables[1];
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
                Session["s_dlFreeze"] = dsFT;
            }
        }
    }
    protected void dlFreeze_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        int count = 0;
        switch (e.CommandName)
        {
            case "AddProduct":
                {
                    count = 1;
                    if (count == 1)
                        ((Button)dlFreeze.Items[index].FindControl("btnAddRemove")).Visible = true;
                    DataTable dtFreeProd = orderObj.BindFreezeProduct();
                    DataSet ds = (DataSet)Session["s_dlFreeze"];
                    DataTable dtmer = new DataTable();
                    if (dtFreeProd.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtFreeProd.Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[1].NewRow();
                            if (j == 0)
                            {
                                dr["FreezeTransactionID"] = 0;
                                dr["ProductId"] = dtFreeProd.Rows[0]["ProductId"].ToString();
                                dr["FreezeProductName"] = dtFreeProd.Rows[0]["ProductName"].ToString();
                                dtmer = set.GetMentholPerDetails(2);
                                if (dtmer.Rows.Count > 0)
                                {
                                    decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                                    decimal Qty = Math.Round(((Convert.ToDecimal(ds.Tables[0].Rows[0]["Quntatiy"])) * mer / 100), 0);
                                    dr["FreezeQuantity"] = Qty.ToString();
                                }
                                
                            }
                            else
                            {
                                dr["FreezeTransactionID"] = 0;
                                dr["ProductId"] = dtFreeProd.Rows[1]["ProductId"].ToString();
                                dr["FreezeProductName"] = dtFreeProd.Rows[1]["ProductName"].ToString();
                                dtmer = set.GetMentholPerDetails(2);
                                if (dtmer.Rows.Count > 0)
                                {
                                     decimal mer = Convert.ToDecimal(dtmer.Rows[0]["percentage"].ToString());
                                     decimal Qty = Math.Round(((Convert.ToDecimal(ds.Tables[0].Rows[0]["Quntatiy"])) * (100-mer) / 100), 0);
                                     dr["FreezeQuantity"] = Qty.ToString();
                                }
                            }
                            ds.Tables[1].Rows.Add(dr);
                            DataTable dtadd = ds.Tables[1];
                            Session["FreezeTran"] = dtadd;
                            (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dtadd;
                            (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
                        }
                    }
                }
                break;
            case "Del":
                {
                    int ss = index;
                    if (ss == 0)
                    {
                        GridViewRow gvr = (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).Rows[index];
                        (Session["FreezeTran"] as DataTable).Rows.RemoveAt(index);
                        (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = (DataTable)Session["FreezeTran"];
                        (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
                    }
                }
                break;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('FREEZING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnFreezeSubmit_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        GridView gvf = (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView);
        DataTable dt = (Session["s_dlFreeze"] as DataSet).Tables[0];
        DataTable dtDate = new DataTable();
        int qty = 0;
        string FBatch = string.Empty;
        bool status;

        for (int count = 0; count < gvf.Rows.Count; count++)
        {
            int fid = Convert.ToInt32(dt.Rows[0]["FreezeID"].ToString());
            int ftid = Convert.ToInt32(gvf.DataKeys[count].Values["FreezeTransactionID"].ToString());
            string ftPName = (gvf.Rows[count].Cells[0].FindControl("txtFProductName") as TextBox).Text;
            int pid = Convert.ToInt32(gvf.DataKeys[count].Values["ProductId"].ToString());
            string ProdCode = string.Empty;
            if (pid == 4 || pid == 6)
            {
                dtDate = settObj.GetStandDetails("2", (DateTime.Now.Year - 1).ToString());
                DataTable dtCode = pr.GetProductCode(pid);
                ProdCode = dtCode.Rows[0]["ProductCode"].ToString();
                if (dtDate.Rows.Count > 0)
                {
                    DataTable dtFinyear = set.GetLotYear(Convert.ToDateTime(dtDate.Rows[0]["Date"].ToString()));
                    if (dtFinyear.Rows.Count > 0)
                        if (dtCode.Rows.Count > 0)
                            FBatch = APP.GenerateFreezeLotnumber(dtCode.Rows[0]["ProductCode"].ToString(), dtFinyear.Rows[0]["FYear"].ToString());
                }
            }
            string ftQty = (gvf.Rows[count].Cells[0].FindControl("txtFQuantity") as TextBox).Text;
            qty += Convert.ToInt32(ftQty);
            if (qty <= Convert.ToInt32(dt.Rows[0]["Quntatiy"].ToString()))
            {
                if (ftid == 0)
                {
                    status = orderObj.FreezeTran_INSandUPDandDEL(ftid, fid, pid, FBatch, ftPName, ftQty, "Aslam", string.Empty, MudarApp.Insert);
                    //orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), "FREEZING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "FREEZING");
                    //Session["POStatus"] = "FREEZING";
                }
            }
            else
            {
                //total quantity of received product is greater than quantity.
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity Shouldnot be greater than Total Quantity from Freezing');</script>");
            }
        }
        if (true)
        {
            BindAllFreeTrandData(dt.Rows[0]["FreezeID"].ToString());
            btnDisableFrzee.Visible = true;
            btnFreezeSubmit.Visible = false;
            //BindgvPackingDetails();
            BindNewPackingDetail();
            orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "FREEZING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "FREEZING");
            Session["POStatus"] = "FREEZING";
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('TESTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        }
        //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('PACKING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    private void BindAllFreeTrandData(string FID)
    {
        DataSet dsFT = orderObj.GetFreezeTran(FID);
        dlFreeze.DataSource = dsFT.Tables[0];
        dlFreeze.DataBind();
        if (dlFreeze.Items.Count > 0)
        {
            if (dsFT.Tables[1].Rows.Count > 0)
            {
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dsFT.Tables[1];
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();
            }
            else
            {
                DataTable dtFreeProd = orderObj.BindFreezeProduct();
                if (dtFreeProd.Rows.Count > 0)
                {
                    for (int j = 0; j < dtFreeProd.Rows.Count; j++)
                    {
                        DataRow dr = dsFT.Tables[1].NewRow();
                        if (j == 0)
                        {
                            dr["FreezeTransactionID"] = 0;
                            dr["ProductId"] = dtFreeProd.Rows[0]["ProductId"].ToString();
                            dr["FreezeProductName"] = dtFreeProd.Rows[0]["ProductName"].ToString();
                            
                            decimal Qty = Math.Round(((Convert.ToDecimal(dsFT.Tables[0].Rows[0]["Quntatiy"])) * 70 / 100), 0);
                            dr["FreezeQuantity"] = Qty.ToString();
                        }
                        else
                        {
                            dr["FreezeTransactionID"] = 0;
                            dr["ProductId"] = dtFreeProd.Rows[1]["ProductId"].ToString();
                            dr["FreezeProductName"] = dtFreeProd.Rows[1]["ProductName"].ToString();
                            decimal Qty = Math.Round(((Convert.ToDecimal(dsFT.Tables[0].Rows[0]["Quntatiy"])) * 30 / 100), 0);
                            dr["FreezeQuantity"] = Qty.ToString();
                        }
                        dsFT.Tables[1].Rows.Add(dr);
                    }
                }
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataSource = dsFT.Tables[1];
                (dlFreeze.Items[0].FindControl("gvFreezeTran") as GridView).DataBind();

                ((Button)dlFreeze.Items[0].FindControl("btnAddProduct")).Visible = false;
                ((Button)dlFreeze.Items[0].FindControl("btnAddRemove")).Visible = false;
                Session["s_dlFreeze"] = dsFT;
            }
        }
    }
    #endregion

    #region Packing
    protected void btnPackingDetailsSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);

        DataTable dtOrderProduct = new DataTable();
        dtOrderProduct.Columns.Add("ProductId", typeof(int));
        dtOrderProduct.Columns.Add("GrossQuantity", typeof(decimal));
        dtOrderProduct.Columns.Add("Packing25", typeof(int));
        dtOrderProduct.Columns.Add("Packing180", typeof(int));

        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        string collectionId = string.Empty;
        if (dtcollec.Rows.Count > 0)
        {
            collectionId = dtcollec.Rows[0]["CollectionID"].ToString();
        }

        foreach (GridViewRow Row in gvPackingDetails.Rows)
        {

            string GrossQty = ((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[4].FindControl("txtGrossQty")).Text;
            DataKey dk = gvPackingDetails.DataKeys[Row.RowIndex];
            //string BatchID = dk.Values["BatchID"].ToString();
            string Lotnumber = gvPackingDetails.Rows[Row.RowIndex].Cells[2].Text;
            int ProductID = Convert.ToInt32(gvPackingDetails.Rows[Row.RowIndex].Cells[0].Text); //Convert.ToInt32(dk.Values["ProductID"].ToString());
            int Packing25 = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[5].FindControl("txtPacking25")).Text);
            int Packing180 = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[6].FindControl("txtPacking180")).Text);

            int drum25from = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[5].FindControl("txtDrum25Start")).Text);
            int drum25to = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[5].FindControl("txtDrum25End")).Text);

            int drum180from = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[6].FindControl("txtDrum180Start")).Text);
            int drum180to = Convert.ToInt32(((TextBox)gvPackingDetails.Rows[Row.RowIndex].Cells[6].FindControl("txtDrum180End")).Text);

            //int collectionId = Convert.ToInt32(gvPackingDetails.Rows[Row.RowIndex].Cells[7].Text);

            bool packingResult = orderObj.OrderProductPackingInsert(Convert.ToInt32(collectionId), ProductID, Lotnumber, Packing25, Packing180, drum25from, drum25to, drum180from, drum180to,DateTime.Now);
            if (packingResult == true)
            {
                DataRow drOrderProduct = dtOrderProduct.NewRow();
                drOrderProduct["ProductId"] = ProductID;
                drOrderProduct["GrossQuantity"] = Convert.ToDecimal(GrossQty);
                drOrderProduct["Packing25"] = Packing25;
                drOrderProduct["Packing180"] = Packing180;
                dtOrderProduct.Rows.Add(drOrderProduct);
            }
        }
        var query = (from row in dtOrderProduct.AsEnumerable()
                     group row by row.Field<int>("ProductId") into rowGroup
                     select new
                     {
                         ProductId = rowGroup.Key,
                         GrossQuantity = rowGroup.Sum(m => m.Field<decimal>("GrossQuantity")),
                         Packing25 = rowGroup.Sum(m => m.Field<int>("Packing25")),
                         Packing180 = rowGroup.Sum(m => m.Field<int>("Packing180")),
                     }).ToList();
        foreach (var item in query)
        {
            result = orderObj.PackingDetailsUpdate(Convert.ToInt32(OrderID), item.ProductId, Convert.ToInt32(lblBranchOrderID.Text), item.GrossQuantity, item.Packing25, item.Packing180, "bhanu", MudarApp.Update);
        }

        //result = orderObj.PackingDetailsUpdate(Convert.ToInt32(OrderID), ProductID, Convert.ToInt32(lblBranchOrderID.Text), Convert.ToDecimal(GrossQty), Convert.ToInt32(Packing25), Convert.ToInt32(Packing180), "bhanu", MudarApp.Update);

        if (result)
        {
            BindNewPackingDetail();
            //orderObj.OrderDetails_UPD(Convert.ToInt32(OrderID), "PACKING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "PACKING");
            orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "PACKING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "PACKING");
            Session["POStatus"] = "PACKING";
            btnPackingDetailsSubmit.Visible = false;
            btnDisablePacking.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('PACKING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
        }

    }
    private void BindgvPackingDetails()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtPackingDetils = orderObj.PackingDetails(Convert.ToInt32(OrderID), Convert.ToInt32(!string.IsNullOrEmpty(lblBranchOrderID.Text) ? lblBranchOrderID.Text : "0"));
        if (dtPackingDetils.Rows.Count > 0)
        {
            if (dtPackingDetils.Rows.Count == 1)
            {
                gvPackingDetails.DataSource = dtPackingDetils;
                gvPackingDetails.DataBind();
            }
            else
            {
                for (int i = 0; i < dtPackingDetils.Rows.Count; i++)
                {
                    if (dtPackingDetils.Rows[i]["Blending_BatchID"].ToString() == string.Empty || dtPackingDetils.Rows[i]["Blending_BatchID"].ToString() == "")
                    {
                        dtPackingDetils.Rows[i].Delete();
                    }
                }
                gvPackingDetails.DataSource = dtPackingDetils;
                gvPackingDetails.DataBind();
            }
        }
    }
    private void BindNewPackingDetail()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        DataTable dtcollec = orderObj.GetCollectionID(OrderID);
        string collectionId = string.Empty;
        if (dtcollec.Rows.Count > 0)
            collectionId = dtcollec.Rows[0]["CollectionID"].ToString();
        DataTable dtOrderPacking = orderObj.ListOrderPackingDetails(Convert.ToInt32(collectionId));
        if (dtOrderPacking.Rows.Count > 0)
        {
            gvPackingCompleted.DataSource = dtOrderPacking;
            gvPackingCompleted.DataBind();
            gvPackingDetails.DataSource = null;
            gvPackingDetails.DataBind();
            btnPackingDetailsSubmit.Visible = false;
            btnDisablePacking.Visible = false;
        }
        else
        {
            DataTable dtPacking = new DataTable();
            dtPacking.Columns.Add("OrderId", typeof(int));
            dtPacking.Columns.Add("ProductId", typeof(int));
            dtPacking.Columns.Add("CollectionID", typeof(int));
            dtPacking.Columns.Add("ProductName", typeof(string));
            dtPacking.Columns.Add("LotNumber", typeof(string));
            dtPacking.Columns.Add("ActualQuantity", typeof(decimal));
            dtPacking.Columns.Add("GrossQuantity", typeof(decimal));
            dtPacking.Columns.Add("Packing25KG", typeof(int));
            dtPacking.Columns.Add("Packing25KG_Drum_Start", typeof(int));
            dtPacking.Columns.Add("Packing25KG_Drum_End", typeof(int));
            dtPacking.Columns.Add("Packing180KG", typeof(int));
            dtPacking.Columns.Add("Packing180KG_Drum_Start", typeof(int));
            dtPacking.Columns.Add("Packing180KG_Drum_End", typeof(int));
            DataTable orderProducts = orderObj.ListOrderProducts(Convert.ToInt32(OrderID));
            foreach (DataRow prodItem in orderProducts.Rows)
            {
                DataTable dtAlreadyBlend = new DataTable();
                dtAlreadyBlend.Columns.Add("Blending_BatchID");
                dtAlreadyBlend.Columns.Add("BlendQty", typeof(decimal));
                DataTable dtNewBlend = new DataTable();
                if (Convert.ToString(prodItem["ProductID"]) == "4" || Convert.ToString(prodItem["ProductID"]) == "6")
                {
                    DataTable dtFBID = orderObj.GetFreezeBatchID(Convert.ToInt32(OrderID),Convert.ToInt32(prodItem["ProductID"]));
                    if (dtFBID.Rows.Count > 0)
                    {
                        DataRow drBlendNew = dtAlreadyBlend.NewRow();
                        drBlendNew["Blending_BatchID"] = dtFBID.Rows[0]["FreezeProductBatchID"].ToString();
                        drBlendNew["BlendQty"] = dtFBID.Rows[0]["FreezeQuantity"].ToString();
                        dtAlreadyBlend.Rows.Add(drBlendNew);
                    }
                }
                else
                {
                    DataTable dtBlending = orderObj.GetBliendingDetails(collectionId, Convert.ToString(prodItem["ProductID"]));
                    if (dtBlending.Rows.Count > 0)
                    {
                        DataTable dtcheck = orderObj.GetBlendDetailsBasedonBlendID(collectionId);
                        if (dtcheck.Rows.Count > 0)
                        {
                            dtNewBlend = orderObj.GetBlendDetailsBasedonBlendID(dtcheck.Rows[0]["BlendingID"].ToString(), collectionId, Convert.ToString(prodItem["ProductID"]));
                        }
                        if (dtcheck.Rows.Count > 0 && dtNewBlend.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtNewBlend.Rows)
                            {
                                decimal blendQty = 0.0M;
                                string qtyStr = Convert.ToString(item["BlendQty"]);
                                if (!string.IsNullOrEmpty(qtyStr))
                                {
                                    qtyStr = qtyStr.Trim();
                                    string[] farmerQtyArray = new string[] { };
                                    string[] preorderQtyArray = new string[] { };
                                    if (qtyStr.Contains("@"))
                                    {
                                        string[] qtyArray = qtyStr.Split('@');
                                        farmerQtyArray = qtyArray[0].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                        preorderQtyArray = qtyArray[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                    }
                                    else
                                    {
                                        farmerQtyArray = qtyStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                    }

                                    if (farmerQtyArray.Length > 0)
                                        blendQty = blendQty + farmerQtyArray.Sum(m => Convert.ToDecimal(m));
                                    if (preorderQtyArray.Length > 0)
                                        blendQty = blendQty + preorderQtyArray.Sum(m => Convert.ToDecimal(m));
                                }
                                DataRow drBlendNew = dtAlreadyBlend.NewRow();
                                drBlendNew["Blending_BatchID"] = Convert.ToString(item["Blending_BatchID"]);
                                drBlendNew["BlendQty"] = blendQty;
                                dtAlreadyBlend.Rows.Add(drBlendNew);
                            }
                        }
                    }
                }
                foreach (DataRow item in dtAlreadyBlend.Rows)
                {
                    DataRow drNew = dtPacking.NewRow();
                    drNew["OrderId"] = Convert.ToInt32(OrderID);
                    drNew["ProductId"] = Convert.ToInt32(prodItem["ProductID"]);
                    drNew["CollectionID"] = Convert.ToInt32(collectionId);
                    drNew["ProductName"] = Convert.ToString(prodItem["ProductName"]);
                    drNew["LotNumber"] = Convert.ToString(item["Blending_BatchID"]);
                    decimal quantity = Convert.ToDecimal(item["BlendQty"]);
                    drNew["ActualQuantity"] = quantity;
                    decimal grossQuantity = 0.0M;
                    int pack25 = 0;
                    int pack180 = 0;
                    if (item["BlendQty"].ToString() ==Convert.ToString(prodItem["Quantity"]))
                    {
                        pack25 = Convert.ToInt32(prodItem["Packing25"]);
                        pack180 = Convert.ToInt32(prodItem["Packing180"]);
                    }
                    else
                    {
                        pack180 = Convert.ToInt32(quantity / 180);
                        var rest = quantity - (180 * pack180);
                        if (rest != 0)
                        {
                            pack25 = Convert.ToInt32(rest / 25);
                            rest = rest - (25 * pack25);
                            if (rest > 0 && rest < 25)
                            {
                                pack25 += 1;
                            }
                        }
                    }
                   
                    if(Convert.ToString(prodItem["ProductID"]) =="4")
                        grossQuantity =  pack25 * 28.5M;
                    else
                        grossQuantity = pack180 * (201) + pack25 * (28);
                    drNew["GrossQuantity"] = grossQuantity;
                    drNew["Packing25KG"] = pack25;
                    drNew["Packing25KG_Drum_Start"] = pack25 == 0 ? 0 : 1;
                    drNew["Packing25KG_Drum_End"] = pack25;

                    drNew["Packing180KG"] = pack180;
                    drNew["Packing180KG_Drum_Start"] = pack180 == 0 ? 0 : 1;
                    drNew["Packing180KG_Drum_End"] = pack180;
                    dtPacking.Rows.Add(drNew);
                }
            }
            gvPackingDetails.DataSource = dtPacking;
            gvPackingDetails.DataBind();
            gvPackingCompleted.DataSource = null;
            gvPackingCompleted.DataBind();
            btnPackingDetailsSubmit.Visible = true;
            btnDisablePacking.Visible = false;
        }
    }
    #endregion

    #region Admin Reports
    private void BindBranchOrderReport()
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        if (!string.IsNullOrEmpty(lblBranchOrderID.Text))
        {
            DataTable dtORPD = reportObj.OrderReportsPathGetDetails(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text));
            if (dtORPD.Rows.Count > 0)
            {
                hlBInvoice.NavigateUrl = dtORPD.Rows[0]["BInvoice"].ToString();
                hlBLR.NavigateUrl = dtORPD.Rows[0]["BLR"].ToString();
                hlBGLCInfo.NavigateUrl = dtORPD.Rows[0]["BGLCInfo"].ToString();
                hlBTruckBill.NavigateUrl = dtORPD.Rows[0]["BTruckBill"].ToString();
                hlBOther.NavigateUrl = dtORPD.Rows[0]["others"].ToString();

                btnBInvoice.Enabled = string.IsNullOrEmpty(hlBInvoice.NavigateUrl) ? true : false;
                btnBGLCInfo.Enabled = string.IsNullOrEmpty(hlBGLCInfo.NavigateUrl) ? true : false;
                btnBTruckBill.Enabled = string.IsNullOrEmpty(hlBTruckBill.NavigateUrl) ? true : false;
                btnBLR.Enabled = string.IsNullOrEmpty(hlBLR.NavigateUrl) ? true : false;
                btnBOther.Enabled = string.IsNullOrEmpty(hlBOther.NavigateUrl) ? true : false;
            }
        }
    }
    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        Session["BranchOrderID_S"] = lblBranchOrderID.Text;
        Response.Redirect("~/Reports/InvoiceReport.aspx");
    }
    protected void btnBInvoice_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string Pdf_path = string.Empty;
        if (fuBInvoice.FileName.Length > 0)
        {
            Pdf_path = System.Web.Configuration.WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BInvoice" + OrderID.ToString() + "_" + fuBInvoice.FileName;
            fuBInvoice.PostedFile.SaveAs(Server.MapPath(Pdf_path));

            reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.BInvoice);
            BindBranchOrderReport();
            
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnBGLCInfo_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string Pdf_path = string.Empty;
        if (fuBGLCInfo.FileName.Length > 0)
        {
            Pdf_path = System.Web.Configuration.WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BGLC" + OrderID.ToString() + "_" + fuBGLCInfo.FileName;
            fuBGLCInfo.PostedFile.SaveAs(Server.MapPath(Pdf_path));
            reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.BGLCInfo);
            BindBranchOrderReport();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnBTruckBill_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string Pdf_path = string.Empty;
        if (fuBTruckBill.FileName.Length > 0)
        {
            Pdf_path = System.Web.Configuration.WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BTruckBill" + OrderID.ToString() + "_" + fuBTruckBill.FileName;
            fuBTruckBill.PostedFile.SaveAs(Server.MapPath(Pdf_path));
            reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.BTruckBill);
            BindBranchOrderReport();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnBLR_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string Pdf_path = string.Empty;
        if (fuBLR.FileName.Length > 0)
        {
            Pdf_path = System.Web.Configuration.WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BLR" + OrderID.ToString() + "_" + fuBLR.FileName;
            fuBLR.PostedFile.SaveAs(Server.MapPath(Pdf_path));
            reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.BLR);
            BindBranchOrderReport();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string str = "";//((HiddenField)dlOrderHistory.Items[Index].FindControl("hfOrderPdf")).Value.ToString();
        System.Net.WebClient req = new System.Net.WebClient();
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.ClearContent();
        response.ClearHeaders();
        response.Buffer = true;
        //response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(str) + "\"");
        byte[] data = req.DownloadData(Server.MapPath(str));
        response.BinaryWrite(data);
        response.End();
    }
    protected void btnDispatchNext_Click(object sender, EventArgs e)
    {
        orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "DISPATCH", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "DISPATCH");
        //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DISPATCH','" + Convert.ToString(Session["POStatus"]) + "');", true);
        Response.Redirect("~/Mudar/BranchOrder.aspx");
    }
    protected void btnBOther_Click(object sender, EventArgs e)
    {
        string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        string Pdf_path = string.Empty;
        if (fuBOther.FileName.Length > 0)
        {
            Pdf_path = System.Web.Configuration.WebConfigurationManager.AppSettings["orderpdf"].ToString() + OrderID.ToString() + "/BOther" + OrderID.ToString() + "_" + fuBOther.FileName;
            fuBOther.PostedFile.SaveAs(Server.MapPath(Pdf_path));
            reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(OrderID), Convert.ToInt32(lblBranchOrderID.Text), Pdf_path, "Aslam", string.Empty, rtypeObj.Other);
            BindBranchOrderReport();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DOCUMENTING','" + Convert.ToString(Session["POStatus"]) + "','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnSkip_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DISPATCH','DOCUMENTING','" + Convert.ToString(Session["hasMenthol"]) + "','" + Convert.ToString(Session["OrderType"]) + "');", true);
    }
    protected void btnReportSubmit_Click(object sender, EventArgs e)
    {
        //orderObj.BranchOrderDetails_UPD(Convert.ToInt32(lblBranchOrderID.Text), "DOCUMENTING", "Raghu", "<br/>" + string.Format("{0:dd MMM yyyy}", DateTime.Now) + ' ' + "DOCUMENTING");
        //Session["POStatus"] = "DOCUMENTING";
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DISPATCH','DOCUMENTING');", true);
        //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "ManageTabs('DISPATCH','" + Convert.ToString(Session["POStatus"]) + "');", true);
    }
    #endregion

    protected void btnInvcancel_Click(object sender, EventArgs e)
    {
        hlBInvoice.NavigateUrl = "";
        btnBInvoice.Visible = true;
        btnDisableBInvoice.Visible = false;
    }
    protected void btnGLCcancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnTBcancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnLRcancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnOthcancel_Click(object sender, EventArgs e)
    {

    }
}