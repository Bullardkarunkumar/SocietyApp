using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Data;
using System.Web.Configuration;
using System.Net;
using System.Text.RegularExpressions;

public partial class Farmer_FarmerPreOrder : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    Settings_BL settObj = new Settings_BL();
    Product_BL ProductObj = new Product_BL();
    Reports_BL reportObj = new Reports_BL();
    FarmPlantation_BL PlantObj = new FarmPlantation_BL();
    Reports_Type rtypeObj = new Reports_Type();
    Settings_BL set = new Settings_BL();
    Farmer_BL frmObj = new Farmer_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MasterControlbtnPreorder();
            BindddlOrderProductDetails();
            divCollectingDetails.Visible = false;
            BindIcsCodes(chkICSList);
            BindIcsCodes(chkICSListInner);
            BindPreOrder();
            if (!string.IsNullOrEmpty(Request.QueryString["nav"]) && !string.IsNullOrEmpty(Request.QueryString["pid"]) && !string.IsNullOrEmpty(Request.QueryString["sel"]))
            {
                btnNavPreorderLast.Visible = false;
                btnNavPreorderFirst.Visible = false;
                btnNewPreOrder_Click(sender, e);
            }
            else
            {
                btnNavPreorderLast.Visible = false;
                btnNavPreorderFirst.Visible = false;
            }

        }
    }
    private void BindddlOrderProductDetails()
    {
        //  string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
        ddlSelectProduct.DataSource = ProductObj.GetProductDetails();
        ddlSelectProduct.DataTextField = "ProductName";
        ddlSelectProduct.DataValueField = "ProductID";
        ddlSelectProduct.DataBind();
        ddlSelectProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    protected void ddlSelectProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvCollecingDetails(Convert.ToInt32(ddlSelectProduct.SelectedValue.ToString()));
    }
    private void BindgvCollecingDetails(int productID)
    {
        lblBatchID.Text = string.Empty;
        List<string> selectedValues = chkICSListInner.Items.Cast<ListItem>()
.Where(li => li.Selected)
.Select(li => "'" + li.Value + "'")
.ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        List<string> selectedVillageValues = chkVillageist.Items.Cast<ListItem>()
        .Where(li => li.Selected)
        .Select(li => "'" + li.Value + "'")
        .ToList();
        string selectedVillageValue = string.Empty;
        if (selectedVillageValues.Count > 0)
            selectedVillageValue = string.Join(",", selectedVillageValues.ToArray());
        DataTable Pyear = new DataTable();
        DataTable dtBranchOrder = new DataTable();
        DataTable dtDate = new DataTable();
        DataTable dtOrderCollect = new DataTable();
        if (productID == 4 || productID == 10 || productID == 11)
        {
            if (productID == 4)
                dtDate = settObj.GetStandDetails("2", (DateTime.Now.Year - 1).ToString());
            if (productID == 10)
                dtDate = settObj.GetStandDetails("3", (DateTime.Now.Year - 1).ToString());
            if (productID == 11)
                dtDate = settObj.GetStandDetails("8", (DateTime.Now.Year - 1).ToString());
        }
        else
            dtDate = settObj.GetStandDetails(productID.ToString(), (DateTime.Now.Year - 1).ToString());
        if (dtDate.Rows.Count > 0)
            Pyear = settObj.GetProductionYear(Convert.ToDateTime(dtDate.Rows[0]["Date"].ToString()));
        //divgvCollectDetails.Visible = true;
        if (Pyear.Rows.Count > 0)
        {
            dtOrderCollect = orderObj.CollectedProductDetailsBasedonProductandICs(productID, Pyear.Rows[0]["ProductionYear"].ToString(), selectedVillageValue);
            //DataTable dtOrderCollect = orderObj.CollectedProductDetailsBasedonProductandICsNew(productID, chkSelectedVal);
            if (dtOrderCollect.Rows.Count > 0)
            {
                trCollect.Visible = true;
                trProductName.Visible = true;
                divgvCollectDetails.Visible = true;
                gvCollecingDetails.DataSource = dtOrderCollect;
                gvCollecingDetails.DataBind();
                btncollectSubmit.Enabled = true;
                btnGenerateBatchID.Visible = true;
            }
        }
    }
    protected void chkICSList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPreOrder();
    }
    protected void cbCollecting_CheckedChanged1(object sender, EventArgs e)
    {
        decimal chktest = 0;
        decimal X = 0M;
        foreach (GridViewRow Row in gvCollecingDetails.Rows)
        {
            if (((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked)
            {
                string quantity = ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text;
                Regex regEx = new Regex(@"^\d{1,5}(\.\d{1,2})?$");
                if (string.IsNullOrEmpty(quantity))
                {
                    (gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting") as CheckBox).Checked = false;
                    ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please Enter the Quantity !!!');</script>");
                }
                else if (!regEx.Match(quantity).Success)
                {
                    (gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting") as CheckBox).Checked = false;
                    ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please Enter valid Quantity !!! ');</script>");
                    return;
                }
                else if (Convert.ToDecimal(quantity) <= 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Quantity must be atleast one KG !!! ');</script>");

                }
                X = Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[6].Text);
                if (Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text) != 0)
                {
                    if (X >= Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text))
                    {
                        chktest += Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text);
                    }
                    else
                    {
                        (gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting") as CheckBox).Checked = false;
                        ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Check the Available Quantity');</script>");
                    }
                }
                else
                {
                    (gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting") as CheckBox).Checked = false;
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Enter the Quantity');</script>");
                }
            }
            else
                ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty") as TextBox).Text = "0";

        }
        if (!string.IsNullOrEmpty(txtOrderQuantity.Text))
        {
            if (chktest <= Convert.ToDecimal(txtOrderQuantity.Text))
                lblpresentqty.Text = chktest.ToString();
            else
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Colleted Quantity is not Greater than the Reqiured Qty');</script>");
        }
        else
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Enter the Required Quantity');</script>");
    }
    protected void btncollectSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblBatchID.Text))
        {
            bool result = false;
            string FarmerId = string.Empty;
            string FarmId = string.Empty;
            string PlantationId = string.Empty;
            string CollectDt = string.Empty;
            string LotNumber = string.Empty;

            int ProductID = Convert.ToInt32(ddlSelectProduct.SelectedValue);
            // string OrderID = Encrypt_Decrypt.Decrypt(Session["sOrderID"].ToString().Trim(), true);
            string FarmerCollectCheck = "";
            decimal collect = 0;
            foreach (GridViewRow Row in gvCollecingDetails.Rows)
            {
                if (((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked)
                {
                    //decimal x = Convert.ToDecimal("688.00");
                    collect += Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text);
                    if (Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text) > Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[6].Text))
                    {
                        FarmerCollectCheck = FarmerCollectCheck + gvCollecingDetails.Rows[Row.RowIndex].Cells[0].Text + ", ";
                    }
                }
            }
            if (FarmerCollectCheck.Length == 0)
            {
                if (!string.IsNullOrEmpty(txtCollectQTY.Text))
                {
                    string cmpre = txtCollectQTY.Text.Replace(";", ",");
                    String[] otherqty = cmpre.Split(',');
                    if (otherqty.Length > 0)
                    {
                        for (int i = 0; i < otherqty.Length; i++)
                        {
                            collect += Convert.ToDecimal(otherqty[i].ToString());
                        }
                    }
                }
                if (collect <= Convert.ToDecimal(txtOrderQuantity.Text))
                {
                    foreach (GridViewRow Row in gvCollecingDetails.Rows)
                    {
                        if (((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked)
                        {
                            CollectDt += ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text + ";";
                            LotNumber += gvCollecingDetails.Rows[Row.RowIndex].Cells[3].Text + ";";
                            DataKey dk = gvCollecingDetails.DataKeys[Row.RowIndex];
                            FarmerId += dk.Values["FarmerID"].ToString() + ";";
                            FarmId += dk.Values["FarmID"].ToString() + ";";
                            PlantationId += dk.Values["PlantationId"].ToString() + ";";

                            //string test = Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text).ToString();
                            //string test2 = Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[6].Text.Trim()).ToString();
                            //string stes = dk.Values["PlantationId"].ToString();
                            result = PlantObj.SoldQuantity_Update(Convert.ToInt32(dk.Values["PlantationId"].ToString()), Convert.ToDecimal(((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text) + Convert.ToDecimal(gvCollecingDetails.Rows[Row.RowIndex].Cells[5].Text.Trim()), "Aslam");
                        }
                    }

                    CollectDt = string.IsNullOrEmpty(CollectDt) ? ";" : CollectDt;
                    LotNumber = string.IsNullOrEmpty(LotNumber) ? ";" : LotNumber;
                    FarmerId = string.IsNullOrEmpty(FarmerId) ? ";" : FarmerId;
                    FarmId = string.IsNullOrEmpty(FarmId) ? ";" : FarmId;
                    PlantationId = string.IsNullOrEmpty(PlantationId) ? ";" : PlantationId;

                    result = orderObj.ProductsPreorderCollectionTran_Insert_New(lblBatchID.Text, ProductID, FarmerId, txtOtherFarmers.Text, "0", txtCollectQTY.Text, CollectDt, FarmId, LotNumber, "Aslam", string.Empty, MudarApp.Insert, PlantationId, collect, collect, 0, 0);

                    if (result)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Pre-Order Collection successful');</script>");
                        if (string.IsNullOrEmpty(Request.QueryString["nav"]))
                        {
                            BindgvCollecingDetails(Convert.ToInt32(ddlSelectProduct.SelectedValue));
                            ClearControls();
                            BindPreOrder();
                        }
                        else
                        {
                            Response.Redirect("~/mudar/UpdateOrderNew.aspx?pid=" + Request.QueryString["pid"] + "&sel=" + Request.QueryString["sel"]);
                        }
                        
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Pre-Order Collection Error');</script>");
                }
                else
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity Shouldnot be greater than Required Quantity');</script>");
            }
            else
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity Shouldnot be greater than Actual Quantity For Farmers " + FarmerCollectCheck + "');</script>");
        }
        else
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Generate the Lotnumber');</script>");

    }
    private void ClearControls()
    {
        BindddlOrderProductDetails();
        txtOrderQuantity.Text = string.Empty;
        trCollect.Visible = false;
        lblpresentqty.Text = "0";
        divgvCollectDetails.Visible = false;
        divCollcetionGrid.Visible = true;
        trProductName.Visible = false;
    }
    protected void btnGenerateBatchID_Click(object sender, EventArgs e)
    {
        if (txtOrderQuantity.Text == lblpresentqty.Text)
        {
            int checkCount = 0;
            foreach (GridViewRow item in gvCollecingDetails.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = item.Cells[8].FindControl("cbCollecting") as CheckBox;
                    if (chk.Checked)
                    {
                        string quantity = (item.Cells[7].FindControl("txtCollectQty") as TextBox).Text.Trim();

                        Regex regEx = new Regex(@"^\d{1,5}(\.\d{1,2})?$");
                        if (string.IsNullOrEmpty(quantity))
                        {
                            Show("!!! Please Enter the Quantity !!!");
                            return;
                        }
                        else if (!regEx.Match(quantity).Success)
                        {
                            Show("!!! Please Enter valid Quantity !!!");
                            return;
                        }
                        else if (Convert.ToDecimal(quantity) <= 0)
                        {
                            Show("!!! Quantity must be atleast one KG !!!");
                            return;
                        }
                        checkCount += 1;
                    }
                }
            }
            if (checkCount <= 0)
            {
                Show("!!! Plz Select the Farmer List and Enter the Quantity !!!");
                return;
            }
            MudarApp APP = new MudarApp();
            DataTable dt = ProductObj.GetProductCode(Convert.ToInt32(ddlSelectProduct.SelectedValue));
            //string year = DateTime.Now.Year.ToString();
            //string[] yy = year.Split('2');
            //string[] yy2 = yy[1].Split('0');
            //int plus = Convert.ToInt32(yy2[1].ToString()) + 1;
            //string finyear = yy2[1].ToString() + plus.ToString();
            DataTable dtFinyear = set.GetFinicalYear();
            lblBatchID.Text = APP.GenerateLotNumber(dt.Rows[0]["ProductCode"].ToString(), dtFinyear.Rows[0]["FinYear"].ToString());
            if (!string.IsNullOrEmpty(lblBatchID.Text))
                btnGenerateBatchID.Visible = false;

        }
        else
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Collected Quantity Should be equal to the Required Quantity');</script>");
    }
    public static void Show(string message)
    {
        string cleanMessage = message.Replace("'", "\'");
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("alert('{0}');", cleanMessage);
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
        }
    }
    protected void btnNewPreOrder_Click(object sender, EventArgs e)
    {
        divCollectingDetails.Visible = true;
        divCollcetionGrid.Visible = false;
        //divgvCollectDetails.Visible = false;
        trProductName.Visible = false;
        chkICSListInner_SelectedIndexChanged(sender, e);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divCollectingDetails.Visible = false;
        divCollcetionGrid.Visible = true;
    }

    private void BindPreOrder()
    {
        //gvPreOrder.DataSource = orderObj.PreOrderList("");
        //gvPreOrder.DataBind();


        List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
.Where(li => li.Selected)
.Select(li => "'" + li.Value + "'")
.ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(chkSelectedVal))
        {
            dt = orderObj.PreOrderList(chkSelectedVal);

        }
        else
        {
            dt = new DataTable();
            DataColumn dc = new DataColumn("CollectionTransactionID", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("Blending_BatchID", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("ProductName", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("CreatedDate", typeof(DateTime));
            dt.Columns.Add(dc);
        }
        gvPreOrder.DataSource = dt;
        gvPreOrder.DataBind();
    }

    public void BindIcsCodes(CheckBoxList cbl)
    {
        DataTable dt = frmObj.GetNewICSCodes();
        cbl.DataTextField = "Branchcode";
        cbl.DataValueField = "Branchcode";
        cbl.DataSource = dt;
        cbl.DataBind();
        foreach (ListItem item in cbl.Items)
            item.Selected = true;
    }
    public void ICSVillageList(string Type)
    {
        DataTable dt = frmObj.GetICSVillagelist(Type);
        chkVillageist.DataTextField = "City_Village";
        chkVillageist.DataValueField = "City_Village";
        chkVillageist.DataSource = dt;
        chkVillageist.DataBind();
    }

    protected void gvPreOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "PreOrder")
        {

            string PreOrderID = gvPreOrder.DataKeys[Index].Value.ToString();
            DataTable PreOrderDt = orderObj.PreOrderList(Convert.ToInt32(PreOrderID));
            int productID = Convert.ToInt32(PreOrderDt.Rows[0]["ProductID"].ToString());

            ddlSelectProduct.ClearSelection();
            ddlSelectProduct.Items.FindByValue(productID.ToString()).Selected = true;
            if (PreOrderDt.Rows.Count > 0)
            {
                BindgvCollecingDetails(productID);
                txtOtherFarmers.Text = PreOrderDt.Rows[0]["OtherFarmersName"].ToString();
                txtCollectQTY.Text = PreOrderDt.Rows[0]["OtherFarmerQty"].ToString();
                lblBatchID.Text = PreOrderDt.Rows[0]["BatchID"].ToString();
                if (!string.IsNullOrEmpty(lblBatchID.Text))
                    btnGenerateBatchID.Enabled = false;
                if (!string.IsNullOrEmpty(PreOrderDt.Rows[0]["CollectionTransactionID"].ToString()))
                {
                    string[] plantationID = PreOrderDt.Rows[0]["PlantationId"].ToString().Split(';');
                    string[] cQuantity = PreOrderDt.Rows[0]["CollectionQty"].ToString().Split(';');
                    for (int pcount = 0; pcount < plantationID.Length; pcount++)
                        foreach (GridViewRow Row in gvCollecingDetails.Rows)
                        {
                            DataKey dk = gvCollecingDetails.DataKeys[Row.RowIndex];
                            if (plantationID[pcount] == dk.Values["PlantationId"].ToString())
                            {
                                ((CheckBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("cbCollecting")).Checked = true;
                                ((TextBox)gvCollecingDetails.Rows[Row.RowIndex].Cells[0].FindControl("txtCollectQty")).Text = cQuantity[pcount];
                            }
                        }
                }
            }
            divCollectingDetails.Visible = true;
            divCollcetionGrid.Visible = false;
        }
    }
    protected void chkICSListInner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(chkICSListInner.SelectedValue))
        {
            List<string> selectedValues = chkICSListInner.Items.Cast<ListItem>()
        .Where(li => li.Selected)
        .Select(li => "'" + li.Value + "'")
        .ToList();
            string chkSelectedVal = string.Empty;
            if (selectedValues.Count > 0)
                chkSelectedVal = string.Join(",", selectedValues.ToArray());
            ICSVillageList(chkSelectedVal);
            chkVillageist.Visible = true;
            //divgvCollectDetails.Visible = true;
        }
       // chkVillageist_SelectedIndexChanged(sender, e);
    }
    protected void btnNavPreorderFirst_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/mudar/UpdateOrderNew.aspx?pid=" + Request.QueryString["pid"] + "&sel=" + Request.QueryString["sel"]);
    }
    protected void btnNavPreorderLast_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/mudar/UpdateOrderNew.aspx?pid=" + Request.QueryString["pid"] + "&sel=" + Request.QueryString["sel"]);
    }
    protected void chkVillageist_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCollectingDetails.Visible = true;
        if (!string.IsNullOrEmpty(chkVillageist.SelectedValue))
        {
            trProductName.Visible = true;
            if (!string.IsNullOrEmpty(ddlSelectProduct.SelectedValue.ToString()))
            BindgvCollecingDetails(Convert.ToInt32(ddlSelectProduct.SelectedValue.ToString()));
        }
    }
}
