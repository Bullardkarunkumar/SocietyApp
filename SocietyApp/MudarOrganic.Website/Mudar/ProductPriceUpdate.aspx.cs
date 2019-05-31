using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;


public partial class Admin_ProductPriceUpdate : System.Web.UI.Page
{
    DataTable dtProductDetails;
    Product_BL pr = new Product_BL();
    Buyer_BL BBL = new Buyer_BL();
    ProductPriceUpdate_BL ppu = new ProductPriceUpdate_BL();
    // decimal PriceMB, POPriceMB, FOBPrice, USA_Sea, USA_Air, Europe_Sea, Europe_Air, India_Price, Non_organic_India, Non_organic_USA, USDollar, OtherPrice;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtProductDate.Text = DateTime.Now.ToShortDateString();
            // BindCurrentPrices();
            //Master.MasterControlbtnAddPrices();
            Session["dtPricedata"] = new object();
            //Session["sDtBuyers"] = BBL.BuyerDetails(Session["BuyerId"].ToString());
            Session["Organic_S"] = "1";
            Session["Order_PO"] = "0";
            BindData();
        }
        ////if (Request.QueryString["priceupdate"].ToString() == "1")
        ////    divAdminprice.Visible = true;
        ////else
        ////    divAdminprice.Visible = false;
    }

    private void BindData()
    {
        foreach (DataRow dr in (Session["dtLoginDetails"] as DataTable).Rows)
        {
            if (dr["roleName"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
            {
                divAdminprice.Visible = true;
                divDetailButton.Visible = true;
                BindProductDetails(1);
                BindCurrentPrices(1);
                //divUpdatePrice.Visible = true;
            }
            else
            {
                divAdminprice.Visible = false;
                BindProductDetails(2);
                BindCurrentPrices(2);
            }
        }
    }
    private void BindProductDetails(int type)
    {
        dtProductDetails = pr.GetProductPrice(type);
        if (dtProductDetails.Rows.Count > 0)
        {
            for (int i = 0; i < dtProductDetails.Rows.Count; i++)
            {
                if (dtProductDetails.Rows[i]["PriceId"].ToString() == string.Empty)
                {
                    dtProductDetails.Rows[i]["PriceId"] = 20;
                    dtProductDetails.Rows[i]["PriceMB"] = 2023;
                }
            }
            gvProductPrice.DataSource = dtProductDetails;
            gvProductPrice.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (gvProductPrice.Rows.Count > 0)
        {
            DataTable dtPrice = new DataTable();
            dtPrice.Columns.Add("ProductId");
            dtPrice.Columns.Add("ProductName");
            dtPrice.Columns.Add("PriceMB");
            string Date = string.Empty;
            //for (int i = 0; i < gvProductPrice.Rows.Count; i++)
            //{
            //    dtPrice.Rows.Add();
            //    dtPrice.Rows[0]["ProductId"] = gvProductPrice.Rows[0].Cells[0].ToString();
            //    dtPrice.Rows[0]["ProductName"] = gvProductPrice.Rows[0].Cells[1].ToString();
            //    string check = (gvProductPrice.Rows[0].FindControl("txtNewPrice") as TextBox).Text;
            //    if (check == "0")
            //    {
            //        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('ProductPrice Should not be Zero');</script>");
            //        return;
            //    }
            //    dtPrice.Rows[0]["PriceMB"] = (gvProductPrice.Rows[0].FindControl("txtNewPrice") as TextBox).Text;
            //    Date = gvProductPrice.Rows[0].Cells[5].Text;
            //}
            foreach (GridViewRow gvr in gvProductPrice.Rows)
            {
                int RowIndx = gvr.RowIndex;
                dtPrice.Rows.Add();
                dtPrice.Rows[RowIndx]["ProductId"] = gvProductPrice.DataKeys[RowIndx].Value.ToString();
                dtPrice.Rows[RowIndx]["ProductName"] = gvr.Cells[1].Text;
                string check = (gvProductPrice.Rows[0].FindControl("txtNewPrice") as TextBox).Text;
                //string check =(gvr.FindControl("txtNewPrice") as TextBox).Text;
                if (check == "0")
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('ProductPrice Should not be Zero');</script>");
                    return;
                }
                dtPrice.Rows[RowIndx]["PriceMB"] = (gvr.FindControl("txtNewPrice") as TextBox).Text;
                Date = gvr.Cells[5].Text;
            }
            bool result = false;
            if (CheckEmpty() == true)
            {
                result = pr.GetUpdatePriceCalculation(dtPrice, Convert.ToDouble(txtUsDolar.Text), Convert.ToDouble(txtTransportation.Text),
                    Convert.ToDouble(txtAddonProfit.Text), Convert.ToDateTime(txtProductDate.Text.Trim()), "Ravi", "", 1, Convert.ToDecimal(txtMandyTax.Text), 
                    Convert.ToDecimal(txtBMar.Text), Convert.ToDecimal(txtInsurance.Text), Convert.ToDecimal(txtLocalTax.Text), Convert.ToDecimal(txtAdonupPrice.Text));
            }
            if (result == true)
            {
                BindData();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('New prices are upgraded');", true);
                //ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('new prices are upgraded');</script>");
                // Response.Redirect("~/Mudar/ProductPriceUpdate.aspx");
                //BindCurrentPrices();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Please Contact Admin...');", true);
                //ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Plz Contact Admin..');</script>");
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        BindCurrentPrices(txtProductDate.Text);
        cbPriceD.Checked = false;
    }
    private bool CheckEmpty()
    {
        if (string.IsNullOrEmpty(txtProductDate.Text))
        {
            txtProductDate.Focus();
            return false;
        }
        else if (string.IsNullOrEmpty(txtUsDolar.Text) || txtUsDolar.Text == "0")
        {
            txtUsDolar.Focus();
            return false;
        }
        else if (string.IsNullOrEmpty(txtAddonProfit.Text) || txtAddonProfit.Text == "0")
        {
            txtAddonProfit.Focus();
            return false;
        }
        else if (string.IsNullOrEmpty(txtTransportation.Text) || txtTransportation.Text == "0")
        {
            txtTransportation.Focus();
            return false;
        }
        else
            return true;
    }
    private void BindCurrentPrices(string date)
    {
        DataTable dtCAl = pr.GetUpdateProductPrice(date);
        if (dtCAl.Rows.Count > 0)
        {
            for (int i = 0; i < gvProductPrice.Rows.Count; i++)
            {
                if (i < dtCAl.Rows.Count)
                {
                    //if (gvProductPrice.Rows[i].Cells[0].Text == dtCAl.Rows[i]["ProductId"].ToString())
                    //{
                    //    return;
                    //}
                    if (!string.IsNullOrEmpty(dtCAl.Rows[i]["PriceMB"].ToString()))
                    {
                        (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtCAl.Rows[i]["PriceMB"].ToString()));
                        gvProductPrice.Rows[i].Cells[3].Text = string.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dtCAl.Rows[i]["CreateDate"].ToString()));
                        gvProductPrice.Rows[i].Cells[5].Text = string.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dtCAl.Rows[i]["CreateDate"].ToString()));
                    }
                    else
                    {
                        (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = "0";
                        gvProductPrice.Rows[i].Cells[5].Text = string.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dtCAl.Rows[0]["CreateDate"].ToString()));
                    }
                }
                else
                {
                    (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = "0";
                }
                //if (!string.IsNullOrEmpty(dtCAl.Rows[i]["CreateDate"].ToString()))
                //    (gvProductPrice.Rows[i].Cells[4].FindControl("lblDate") as Label).Text = Convert.ToDateTime(dtCAl.Rows[0]["CreateDate"].ToString()).ToShortDateString();
                //else
                //    (gvProductPrice.Rows[i].Cells[4].FindControl("lblDate") as Label).Text = DateTime.Now.ToShortDateString();

            }
            dtUpdatedPrice.DataSource = dtCAl;
            dtUpdatedPrice.DataBind();
            txtUsDolar.Text = dtCAl.Rows[0]["USDollar"].ToString();
            txtTransportation.Text = dtCAl.Rows[0]["TransportPrice"].ToString();
            txtAddonProfit.Text = dtCAl.Rows[0]["OtherPrice"].ToString();
            txtMandyTax.Text = dtCAl.Rows[0]["MandyTax"].ToString();
            txtBMar.Text = dtCAl.Rows[0]["BMar"].ToString();
            txtInsurance.Text = dtCAl.Rows[0]["insurance"].ToString();
            txtLocalTax.Text = dtCAl.Rows[0]["Localtax"].ToString();
            txtAdonupPrice.Text = dtCAl.Rows[0]["AddUPPrice"].ToString();
            //if (!string.IsNullOrEmpty(dtCAl.Rows[0]["CreateDate"].ToString()))
            //    txtProductDate.Text = Convert.ToDateTime(dtCAl.Rows[0]["CreateDate"].ToString()).ToShortDateString();
            //else
            txtProductDate.Text = dtCAl.Rows[0]["CreateDate"].ToString();//System.DateTime.Now.ToShortDateString();
        }

    }
    private void BindCurrentPrices(int type)
    {
        DataTable dtCAl = pr.GetUpdateProductPrice(type);
        if (dtCAl.Rows.Count > 0)
        {
            for (int i = 0; i < gvProductPrice.Rows.Count; i++)
            {
                string ss = gvProductPrice.Rows[i].Cells[0].Text;
                if (i < dtCAl.Rows.Count)
                {
                    if (ss == dtCAl.Rows[i]["ProductId"].ToString())
                    {
                        if (!string.IsNullOrEmpty(dtCAl.Rows[i]["PriceMB"].ToString()))
                        {
                            (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtCAl.Rows[i]["PriceMB"].ToString()));
                            (gvProductPrice.Rows[i].FindControl("txtNewPrice") as TextBox).Text = "0";
                        }
                        else
                        {
                            DataTable dtProd = pr.GetMaxHistoryIDSupplierProducts(ss);
                            if (dtProd.Rows.Count > 0)
                            {
                                (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = string.Format("{0:0.00}", Convert.ToDecimal(dtProd.Rows[0]["PriceMB"].ToString()));
                                (gvProductPrice.Rows[i].FindControl("txtNewPrice") as TextBox).Text = "0";
                                gvProductPrice.Rows[i].Cells[3].Text = string.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dtProd.Rows[0]["CreatedDate"].ToString()));
                            }
                        }
                    }
                    else
                        (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = "0";
                }
                else
                {
                    (gvProductPrice.Rows[i].Cells[0].FindControl("lblOldPrice") as Label).Text = "0";
                }
                //if (!string.IsNullOrEmpty(dtCAl.Rows[i]["CreateDate"].ToString()))
                //    (gvProductPrice.Rows[i].Cells[4].FindControl("lblDate") as Label).Text = Convert.ToDateTime(dtCAl.Rows[0]["CreateDate"].ToString()).ToShortDateString();
                //else
                //    (gvProductPrice.Rows[i].Cells[4].FindControl("lblDate") as Label).Text = DateTime.Now.ToShortDateString();

            }
            dtUpdatedPrice.DataSource = dtCAl;
            dtUpdatedPrice.DataBind();
            txtUsDolar.Text = dtCAl.Rows[0]["USDollar"].ToString();
            txtTransportation.Text = dtCAl.Rows[0]["TransportPrice"].ToString();
            txtAddonProfit.Text = dtCAl.Rows[0]["OtherPrice"].ToString();
            txtMandyTax.Text = dtCAl.Rows[0]["MandyTax"].ToString();
            txtBMar.Text = dtCAl.Rows[0]["BMar"].ToString();
            txtInsurance.Text = dtCAl.Rows[0]["insurance"].ToString();
            txtLocalTax.Text = dtCAl.Rows[0]["Localtax"].ToString();
            txtAdonupPrice.Text = dtCAl.Rows[0]["AddUPPrice"].ToString();
            //if (!string.IsNullOrEmpty(dtCAl.Rows[0]["CreateDate"].ToString()))
            //    txtProductDate.Text = Convert.ToDateTime(dtCAl.Rows[0]["CreateDate"].ToString()).ToShortDateString();
            //else
            txtProductDate.Text = System.DateTime.Now.ToShortDateString();
        }
    }
    protected void btnDetailed_Click(object sender, EventArgs e)
    {
        divDetailsList.Visible = true;
        divBranch.Visible = false;
        btnDetailed.Visible = false;
        divAdminprice.Visible = false;
        txtFindDate.Text = DateTime.Now.ToShortDateString();
        //btnClose.Visible = false;
    }
    protected void btnFOB_Click(object sender, EventArgs e)
    {
        if (ddlQty.SelectedValue == "--select--")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Quantity')", true);
            divAdminprice.Visible = false;
        }
        else
        {
            lblDataHeader.Text = string.Format("{0} {1}", btnFOB.Text, " Data");
            divPrices.Visible = true;
            divHeaderText.Visible = true;
            divAdminprice.Visible = false;
            btnBack.Visible = true;
            //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            BindDetails(1, Convert.ToInt32(ddlQty.SelectedValue), Convert.ToDateTime(txtFindDate.Text));

        }
    }
    protected void btnCIFsea_Click(object sender, EventArgs e)
    {
        if (ddlQty.SelectedValue == "--select--")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Quantity')", true);
            divAdminprice.Visible = false;
        }
        else
        {
            lblDataHeader.Text = string.Format("{0} {1}", btnCIFsea.Text, " Data");
            divPrices.Visible = true;
            divHeaderText.Visible = true;
            btnBack.Visible = true;
            divAdminprice.Visible = false;
            //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            BindDetails(2, Convert.ToInt32(ddlQty.SelectedValue), Convert.ToDateTime(txtFindDate.Text));
        }
    }
    protected void btnCIFair_Click(object sender, EventArgs e)
    {
        if (ddlQty.SelectedValue == "--select--")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Quantity')", true);
            divAdminprice.Visible = false;
        }
        else
        {
            lblDataHeader.Text = string.Format("{0} {1}", btnCIFair.Text, " Data");
            divPrices.Visible = true;
            divHeaderText.Visible = true;
            btnBack.Visible = true;
            divAdminprice.Visible = false;
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            BindDetails(3, Convert.ToInt32(ddlQty.SelectedValue), Convert.ToDateTime(txtFindDate.Text));
        }
    }
    protected void FOR_Click(object sender, EventArgs e)
    {
        if (ddlQty.SelectedValue == "--select--")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Quantity')", true);
            divAdminprice.Visible = false;
        }
        else
        {
            lblDataHeader.Text = string.Format("{0} {1}",  btnFOR.Text, " Data");
            divPrices.Visible = true;
            divHeaderText.Visible = true;
            btnBack.Visible = true;
            divAdminprice.Visible = false;
            //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            BindDetails(4, Convert.ToInt32(ddlQty.SelectedValue), Convert.ToDateTime(txtFindDate.Text));
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/ProductPriceUpdate.aspx");
        //divPrices.Visible = false;
        //btnBack.Visible = false;
        //divBranch.Visible = true;
        //divDetailsList.Visible = false;
        //btnDetailed.Visible = true;
        ////divAdminprice.Visible = false;
        //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    private void BindDetails(int typeoperation, int Qty, DateTime createdDate)
    {
        gvAllPrices.DataSource = pr.GetDetailsAllPriceswithDate(typeoperation, Qty, createdDate);
        gvAllPrices.DataBind();
    }
    protected void btnCIFWestair_Click(object sender, EventArgs e)
    {
        if (ddlQty.SelectedValue == "--select--")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Quantity')", true);
            divAdminprice.Visible = false;
        }
        else
        {
            lblDataHeader.Text = string.Format("{0} {1}", btnCIFWestair.Text, " Data");
            divPrices.Visible = true;
            divHeaderText.Visible = true;
            btnBack.Visible = true;
            divAdminprice.Visible = false;
            //btnCIFWestair.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnCIFWestair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOB.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFsea.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFsea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnCIFair.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnCIFair.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnFOR.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            //btnFOR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            BindDetails(5, Convert.ToInt32(ddlQty.SelectedValue), Convert.ToDateTime(txtFindDate.Text));
        }
    }
}
