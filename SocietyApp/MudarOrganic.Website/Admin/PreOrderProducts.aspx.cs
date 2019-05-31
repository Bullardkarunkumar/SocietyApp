using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.Text.RegularExpressions;
using MudarOrganic.Components;

public partial class PreOrderProducts : System.Web.UI.Page
{
    Product_BL objProduct = new Product_BL();
    Buyer_BL BBL = new Buyer_BL();
    int operation, qy, Value = 0;
    DataTable dtUpdateOrder = null;
    int count = 0;
    string defaultBuyerId = MudarOrderConstants.DefaultBuyerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["BuyerId"] = defaultBuyerId;
            //if (Session["dtUpdateOrder"] != null)
            //{
            //    dtUpdateOrder = (DataTable)Session["dtUpdateOrder"];
            //    Session["dtUpdateOrder"] = null;
            //}
            //Master.MasterControlbtnPrices();
            Session["dtPricedata"] = new object();
            Session["new"] = new object();
            Session["sDtOrder"] = new object();
            var sDtBuyers= BBL.BuyerDetails(defaultBuyerId);
            Session["sDtBuyers"] = sDtBuyers;
            Session["Organic_S"] = "1";
            Session["Order_PO"] = "0";
            Session["TransportMode"] = new object();
            Session["OrderType"] = new object();
            Session["placeofdelivery"] = new object();
            Session["OrganicType"] = new object();
            BindgvProductPricDetails("ALL", ddlDays.Text);
            txtPODate.Text = DateTime.Now.ToShortDateString();
            TabbuttonControls();
            Session["BuyerCountry_S"] = sDtBuyers.Rows[0]["CCountry"].ToString().ToUpper();
            if (Session["BuyerCountry_S"].ToString() == "INDIA")
            {
                rbForDestination.Checked = true;
                rbForDestination.Enabled = true;
                rbFobIndia.Enabled = false;
                rbCIFAir.Enabled = false;
                rbCIFSea.Enabled = false;
            }
            else
            {
                rbForDestination.Enabled = false;
                rbFobIndia.Enabled = true;
                rbCIFAir.Enabled = true;
                rbCIFSea.Enabled = true;
            }
            //if ((Session["sDtBuyers"] as DataTable).Rows[0]["Lotsample"].ToString() == "1")
            //    btnLotSample.Visible = true;
            if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
                //if (dtUpdateOrder != null)
                BindLotSampleDetails();
            else
                BindBuyerDetails();
        }
    }
    // bind the order of the lotsample
    private void BindLotSampleDetails()
    {
        Session["OrderID"] = new object();
        if (Session["BuyerCountry_S"].ToString() == "INDIA")
            lblmoneytype.Text = "0";
        else
            lblmoneytype.Text = "1";
        DataTable dtUpdateOrder = new DataTable();
        if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
        // if (dtUpdateOrder != null)
        {
            if ((Session["dtUpdateOrder"] as DataTable).Rows.Count > 0)
            //if (dtUpdateOrder.Rows.Count > 0)
            {
                dtUpdateOrder = (Session["dtUpdateOrder"] as DataTable);
                foreach (DataRow dr in dtUpdateOrder.Rows)
                {
                    foreach (GridViewRow Row in grdProdQuantity.Rows)
                    {
                        if (dr["ProductID"].ToString() == grdProdQuantity.Rows[Row.RowIndex].Cells[0].Text)
                        {
                            grdProdQuantity.Rows[Row.RowIndex].Cells[0].ForeColor = System.Drawing.Color.BlueViolet;
                            grdProdQuantity.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.Color.BlueViolet;
                            ((TextBox)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("txtQuantity")).Text = dr["Quantity"].ToString();
                            //((DropDownList)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("ddlQuantity")).SelectedValue = dr["Quantity"].ToString();
                            //((TextBox)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("txtQuantity")).ForeColor = System.Drawing.Color.BlueViolet;
                            //((TextBox)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("txtQuantity")).Enabled = true;
                            ((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbitem")).Checked = true;
                            ((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbitem")).Enabled = true;
                            ((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbitem")).ForeColor = System.Drawing.Color.BlueViolet;
                        }
                        else
                        {
                            //((TextBox)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("txtQuantity")).Enabled = false;
                            //grdProdQuantity.Rows[Row.RowIndex].ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                            //grdProdQuantity.Rows[Row.RowIndex].Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                            //grdProdQuantity.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                            //((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbitem")).Enabled = false;
                            //((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbheader")).Enabled = false;
                            //((TextBox)grdProdQuantity.Rows[Row.RowIndex].Cells[2].FindControl("txtQuantity")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                            //((CheckBox)grdProdQuantity.Rows[Row.RowIndex].Cells[3].FindControl("cbitem")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                        }

                    }
                    Session["OrderID"] = dr["OrderID"].ToString();
                    if (dr["OrgType"].ToString() == "0")
                    {
                        lblOrgFair.Text = "0.00";
                        lblOrgFTP.Text = "0.00";
                    }
                    else
                    {
                        lblOrgFair.Text = (Session["sDtBuyers"] as DataTable).Rows[0]["FairTrade"].ToString();
                        lblOrgFTP.Text = (Session["sDtBuyers"] as DataTable).Rows[0]["FairTradPremium"].ToString();
                    }
                    if (dr["FreightTerms"].ToString() == "FOB INDIA")
                    {
                        lblFTValue.Text = "1";
                        rbFobIndia.Checked = true;
                        //rbFob.Visible = true;
                        divFob.Visible = true;
                        lblprice.Text = "FOB INDIA";
                        lblCur.Text = "USD";
                        if (dr["Transport"].ToString() == "Air")
                            //rbFob.SelectedValue = "0";
                            rbtnFobAir.Checked = true;
                        else
                            //rbFob.SelectedValue = "1";
                            rbtnFobSea.Checked = true;
                    }
                    if (dr["FreightTerms"].ToString() == "CIF by SEA")
                    {
                        rbCIFSea.Checked = true;
                        txtSeaPort.Visible = true;
                        txtSeaPort.Text = dr["DestinationPort"].ToString();
                        lblFTValue.Text = "2";
                        lblprice.Text = "CIF by SEA";
                        lblCur.Text = "USD";
                    }
                    if (dr["FreightTerms"].ToString() == "FOR Destination")
                    {
                        rbForDestination.Checked = true;
                        lblPlacedelivey.Text = dr["DestinationPort"].ToString();
                        lblFTValue.Text = "4";
                        lblprice.Text = "FOR Destination";
                        lblCur.Text = "INR";
                    }
                    if (dr["FreightTerms"].ToString() == "CIF by AIR")
                    {
                        rbCIFAir.Checked = true;
                        txtAirPort.Visible = true;
                        txtAirPort.Text = dr["DestinationPort"].ToString();
                        lblFTValue.Text = "3";
                        lblprice.Text = "CIF by Air";
                        lblCur.Text = "USD";
                    }
                    if (dr["FreightTerms"].ToString() == "CIF by AIR")
                    {
                        lblFTValue.Text = "5";
                        rbCIFAir.Checked = true;
                        txtAirPort.Visible = true;
                        lblprice.Text = "CIF by Air";
                        lblCur.Text = "USD";
                        txtAirPort.Text = dr["DestinationPort"].ToString();
                    }
                    if (dr["PaymentTerms"].ToString() == "100% with PO")
                    {
                        rb100Adv.Checked = true;
                        lblPTValue.Text = "3";
                        lblPayment.Text = "100% with PO";
                    }
                    if (dr["PaymentTerms"].ToString() == "50% with PO + 50% against delivery")
                    {
                        rb50Adv50AgnDevi.Checked = true;
                        lblPTValue.Text = "4";
                        lblPayment.Text = "50% with PO + 50% against delivery";
                    }
                    if (dr["PaymentTerms"].ToString() == "100% against delivery")
                    {
                        rb100AgnDelivery.Checked = true;
                        lblPTValue.Text = "5";
                        lblPayment.Text = "100% against Delivery";
                    }
                    if (dr["PaymentTerms"].ToString() == "100% payment against of 15 DAYS")
                    {
                        rb100PaySelectDays.Checked = true;
                        ddlDays.Visible = true;
                        ddlDays.SelectedValue = "15";
                        lblPTValue.Text = "6";
                        lblPayment.Text = "100% - 15 Days from the date of Invoice";
                    }
                    if (dr["PaymentTerms"].ToString() == "100% payment against of 30 DAYS")
                    {
                        rb100PaySelectDays.Checked = true;
                        ddlDays.Visible = true;
                        ddlDays.SelectedValue = "30";
                        lblPTValue.Text = "7";
                        lblPayment.Text = "100% - 30 Days from the date of Invoice";
                    }
                    if (dr["PaymentTerms"].ToString() == "100% payment against of 45 DAYS")
                    {
                        rb100PaySelectDays.Checked = true;
                        ddlDays.Visible = true;
                        ddlDays.SelectedValue = "45";
                        lblPTValue.Text = "8";
                        lblPayment.Text = "100% - 45 Days from the date of Invoice";
                    }
                    if (dr["PaymentTerms"].ToString() == "100% payment against of 60 DAYS")
                    {
                        rb100PaySelectDays.Checked = true;
                        ddlDays.Visible = true;
                        ddlDays.SelectedValue = "60";
                        lblPTValue.Text = "9";
                        lblPayment.Text = "100% - 60 Days from the date of Invoice";
                    }
                }
            }
        }
        //mvPrice.ActiveViewIndex = 5;
        //btnLotSample.Visible = false;
        //BindNewprices(Convert.ToInt32(lblFTValue.Text), Convert.ToInt32(lblPTValue.Text), Convert.ToDecimal(lblOrgFair.Text), Convert.ToDecimal(lblOrgFTP.Text), lblmoneytype.Text);
        //btnTPT.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTPT.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        ////btnTOrderDetails.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnTPT.CssClass = "btnFarmer1";
        //btnTOrderDetails.CssClass = "btnFarmer1 bactive";
    }
    public void BindBuyerDetails()
    {
        DataTable dtBuyer = BBL.BuyerDetails(defaultBuyerId);
        if (dtBuyer.Rows.Count > 0)
        {
            //buyer country  CCountry
            if (dtBuyer.Rows[0]["CCountry"].ToString() == "INDIA")
                lblmoneytype.Text = "0";
            else
                lblmoneytype.Text = "1";
            // organic or  organifair trade
            if (Convert.ToDecimal(dtBuyer.Rows[0]["FairTrade"].ToString()) > 0 || Convert.ToDecimal(dtBuyer.Rows[0]["FairTradPremium"].ToString()) > 0)
                btnOrganicFair.Visible = true;
            //if (Convert.ToBoolean(dtBuyer.Rows[0]["Lotsample"].ToString()) == true)
            //    btnLotSample.Visible = true;
            //price terms
            if (Convert.ToBoolean(dtBuyer.Rows[0]["FOB_India"].ToString()) == true)
            {
                rbFobIndia.Checked = true;
                //rbFob.Visible = true;
                divFob.Visible = true;
                lblFTValue.Text = "1";
                //lblprice.Text = "FOB INDIA price shown in USA ($) Per KG";
                lblprice.Text = "FOB INDIA";
                lblCur.Text = "USD";
                //if (rbFob.SelectedValue == "0")
                if(rbtnFobAir.Checked)
                {
                    lblPlacedelivey.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                    lblFoBPort.Text = "Air";
                }
                else
                {
                    lblFoBPort.Text = "Sea";
                    lblPlacedelivey.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                }
            }

            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true)
            {
                //lblprice.Text = "CIF AIR price shown in USA ($) Per KG";
                lblprice.Text = "CIF AIR";
                lblFTValue.Text = "3";
                lblCur.Text = "USD";
                rbCIFAir.Checked = true;
                txtAirPort.Visible = true;
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                txtSeaPort.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                lblFTValue.Text = "3";
                lblPlacedelivey.Text = txtAirPort.Text;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_AIR_By_WEST_USA"].ToString()) == true)
            {
                //lblprice.Text = "CIF AIR West price shown in USA ($) Per KG";
                lblprice.Text = "CIF AIR";
                lblCur.Text = "USD";
                lblFTValue.Text = "5";
                rbCIFAir.Checked = true;
                txtAirPort.Visible = true;
                txtSeaPort.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                lblPlacedelivey.Text = txtAirPort.Text;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Sea_By"].ToString()) == true)
            {
                rbCIFSea.Checked = true;
                txtSeaPort.Visible = true;
                rbCIFAir.Checked = false;
                txtAirPort.Visible = false;
                txtSeaPort.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                lblFTValue.Text = "2";
                //lblprice.Text = "CIF SEA price shown in USD ($) Per KG";
                lblprice.Text = "CIF SEA";
                lblCur.Text = "USD";
                lblPlacedelivey.Text = txtSeaPort.Text;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["FORDestination"].ToString()) == true)
            {
                rbForDestination.Checked = true;
                lblPlacedelivey.Text = dtBuyer.Rows[0]["RoadDestination"].ToString();
                lblFTValue.Text = "4";
                //lblprice.Text = "For  price shown in INR (Rs) Per KG";
                lblprice.Text = "FOR Destination";
                lblCur.Text = "INR";
            }
            // payment terms
            if (Convert.ToBoolean(dtBuyer.Rows[0]["100%advance"].ToString()) == true)
            {
                rb100Adv.Checked = true;
                lblPTValue.Text = "3";
                lblPayment.Text = "100% with PO";
                //btnLotSample.Visible = true;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["50%adv+50%againstDocs"].ToString()) == true)
            {
                rb50Adv50AgnDevi.Checked = true;
                lblPTValue.Text = "4";
                lblPayment.Text = "50% with PO + 50% Against Delivery";
                //btnLotSample.Visible = true;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["100%againstDocs"].ToString()) == true)
            {
                rb100AgnDelivery.Checked = true;
                lblPTValue.Text = "5";
                lblPayment.Text = "100% against Delivery";
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["NoofDaysfromInvoice"].ToString()) == true)
            {
                rb100PaySelectDays.Checked = true;
                ddlDays.Visible = true;
                lblDays.Text = dtBuyer.Rows[0]["No_of_Days_Count_fromInvoice"].ToString();
                if (lblDays.Text == "15")
                {
                    ddlDays.SelectedValue = lblDays.Text;
                    lblPTValue.Text = "6";
                    lblPayment.Text = "100% - 15 Days from the date of Invoice";
                }
                if (lblDays.Text == "30")
                {
                    ddlDays.SelectedValue = lblDays.Text;
                    lblPTValue.Text = "7";
                    lblPayment.Text = "100% - 30 Days from the date of Invoice";
                }
                if (lblDays.Text == "45")
                {
                    ddlDays.SelectedValue = lblDays.Text;
                    lblPTValue.Text = "8";
                    lblPayment.Text = "100% - 45 Days from the date of Invoice";
                }
                if (lblDays.Text == "60")
                {
                    ddlDays.SelectedValue = lblDays.Text;
                    lblPTValue.Text = "9";
                    lblPayment.Text = "100% - 60 Days from the date of Invoice";
                }
            }
        }
    }
    public void Show(string message)
    {
        string cleanMessage = message.Replace("'", "\'");
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("fnShowMessage('{0}');", cleanMessage);
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", script, true /* addScriptTags */);
    }

    public void Confirm(string message)
    {
        string cleanMessage = message.Replace("'", "\'");
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("javascript:confirmMessage('{0}');", cleanMessage);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "confirmMessage", script, true /* addScriptTags */);
    }

    protected void rbFobIndia_CheckedChanged(object sender, EventArgs e)
    {
        //rbFob.Visible = true;
        divFob.Visible = true;
        txtAirPort.Visible = false;
        txtSeaPort.Visible = false;
        lblFTValue.Text = "1";
        //lblprice.Text = "FOB INDIA price shown in USA ($) Per KG";
        lblprice.Text = "FOB INDIA";
        lblCur.Text = "USD";
        rbFob_SelectedIndexChanged(sender, e);
        rbCIFSea.Checked = false;
        rbCIFAir.Checked = false;
    }
    protected void rbCIFSea_CheckedChanged(object sender, EventArgs e)
    {
        txtAirPort.Visible = false;
        txtSeaPort.Visible = true;
        //rbFob.Visible = false;
        divFob.Visible = false;
        lblFTValue.Text = "2";
        lblprice.Text = "CIF SEA";
        lblCur.Text = "USD";
        DataTable dtBuyer = BBL.BuyerDetails(defaultBuyerId);
        txtSeaPort.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
        rbFobIndia.Checked = false;
        rbCIFAir.Checked = false;
    }
    protected void rbCIFAir_CheckedChanged(object sender, EventArgs e)
    {
        txtAirPort.Visible = true;
        txtSeaPort.Visible = false;
        //rbFob.Visible = false;
        divFob.Visible = false;
        DataTable dtBuyer = BBL.BuyerDetails(defaultBuyerId);
        if (string.IsNullOrEmpty(dtBuyer.Rows[0]["CIF_Seaport"].ToString()))
        {
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true)
            {
                lblprice.Text = "CIF by AIR";
                lblFTValue.Text = "3";
                lblCur.Text = "USD";
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            }
            else
            {
                //lblprice.Text = "CIF AIR WestUSA price shown in USA ($) Per KG";
                lblprice.Text = "CIF by AIR";
                lblFTValue.Text = "5";
                lblCur.Text = "USD";
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            }
            rbFobIndia.Checked = false;
            rbCIFSea.Checked = false;
        }
        else
        {
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Seaport"].ToString()) == true)
            {
                lblprice.Text = "CIF by AIR";
                lblFTValue.Text = "3";
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            }
            else
            {
                lblprice.Text = "CIF by AIR";
                lblFTValue.Text = "5";
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            }
            rbFobIndia.Checked = false;
            rbCIFSea.Checked = false;
        }
    }
    protected void rbForDestination_CheckedChanged(object sender, EventArgs e)
    {
        lblFTValue.Text = "4";
        lblprice.Text = "FOR Destination ";
        lblCur.Text = "INR";
    }
    protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDays.SelectedValue == "15")
        {
            lblPTValue.Text = "6";
            lblPayment.Text = "100% - 15 Days from the date of Invoice";
        }
        if (ddlDays.SelectedValue == "30")
        {
            lblPTValue.Text = "7";
            lblPayment.Text = "100% - 30 Days from the date of Invoice";
        }
        if (ddlDays.SelectedValue == "45")
        {
            lblPTValue.Text = "8";
            lblPayment.Text = "100% - 45 Days from the date of Invoice";
        }
        if (ddlDays.SelectedValue == "60")
        {
            lblPTValue.Text = "9";
            lblPayment.Text = "100% - 60 Days from the date of Invoice";
        }
    }
    protected void rb100Adv_CheckedChanged(object sender, EventArgs e)
    {
        ddlDays.Visible = false;
        lblPTValue.Text = "3";
        lblPayment.Text = "100% with PO";
        rb50Adv50AgnDevi.Checked = false;
        rb100AgnDelivery.Checked = false;
        rb100PaySelectDays.Checked = false;
        //btnLotSample.Visible = true;

    }
    protected void rb50Adv50AgnDevi_CheckedChanged(object sender, EventArgs e)
    {
        ddlDays.Visible = false;
        lblPTValue.Text = "4";
        lblPayment.Text = "50% with PO + 50% against delivery";
        rb100Adv.Checked = false;
        rb100AgnDelivery.Checked = false;
        rb100PaySelectDays.Checked = false;
        //btnLotSample.Visible = true;
    }
    protected void rb100AgnDelivery_CheckedChanged(object sender, EventArgs e)
    {
        ddlDays.Visible = false;
        lblPTValue.Text = "5";
        lblPayment.Text = "100% against delivery";
        rb50Adv50AgnDevi.Checked = false;
        rb100Adv.Checked = false;
        rb100PaySelectDays.Checked = false;
    }
    protected void rb100PaySelectDays_CheckedChanged(object sender, EventArgs e)
    {
        ddlDays.Visible = true;
        ddlDays_SelectedIndexChanged(sender, e);
        rb50Adv50AgnDevi.Checked = false;
        rb100AgnDelivery.Checked = false;
        rb100Adv.Checked = false;
    }
    #region Private methods
    private void BindgvProductPricDetails(string name, string creditDay)
    {
        string[] value = name.Split('-');
        DataTable productlist = new DataTable();
        if (value.Length > 1)
        {
            productlist = objProduct.GetProductPricebyName(value[1].Trim(), creditDay);
        }
        else
        {
            productlist = BBL.GetBuyerProductDetails(defaultBuyerId);
            if (productlist.Rows.Count < 1)
            {
                lblmsg.Visible = true;
                btnAccept.Enabled = false;
            }
        }
        Session["dtPricedata"] = productlist;
        gvProductSpecification.DataSource = productlist;
        gvProductSpecification.DataBind();
        grdProdQuantity.DataSource = productlist;
        grdProdQuantity.DataBind();
    }
    private void TabbuttonControls()
    {
        btnTProducts.Enabled = false;
        btnTProducts.CssClass = "btnFarmer1 bactive";
        //btnTProducts.Font.Bold = true;
        //btnTProducts.BorderColor = System.Drawing.ColorTranslator.FromHtml("DodgerBlue");
        //btnTProducts.BorderStyle = BorderStyle.Solid;
        //btnTProducts.BorderWidth = Unit.Pixel(2);
        btnTQunatity.Enabled = false;
        btnTQunatity.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
        btnTOrganic.Enabled = false;
        btnTOrganic.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
        btnFT.Enabled = false;
        btnFT.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
        btnTPT.Enabled = false;
        btnTPT.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
        btnTOrderDetails.Enabled = false;
        btnTOrderDetails.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
        btnTPlaceorder.Enabled = false;
        btnTPlaceorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
    }
    private void BindOrder(int Ftvalue, int value)
    {
        DataTable dtpriceTerm = Session["sDtBuyers"] as DataTable;
        DataRow drPriceTerm = dtpriceTerm.Rows[0];
        string PaymentTerm = string.Empty;
        string PriceTerm = string.Empty;
        DataTable dtOrder = new DataTable();
        dtOrder.Columns.Add("ProductID");
        dtOrder.Columns.Add("ProductName");
        dtOrder.Columns.Add("FTerms");
        dtOrder.Columns.Add("PTerms");
        dtOrder.Columns.Add("payment");
        dtOrder.Columns.Add("Price");
        dtOrder.Columns.Add("Quantity");
        dtOrder.Columns.Add("BuyerId");
        dtOrder.Columns.Add("PackingDetails25");
        dtOrder.Columns.Add("PackingDetails180");
        dtOrder.Columns.Add("TotalPrices");
        dtOrder.Columns.Add("money");
        if (Ftvalue == 1)
        {
            PriceTerm = "FOB INDIA";
            Session["TransportMode"] = lblFoBPort.Text;
        }
        if (Ftvalue == 2)
        {
            PriceTerm = "CIF by SEA";
            Session["TransportMode"] = "Sea";
        }
        if (Ftvalue == 3)
        {
            PriceTerm = "CIF by AIR";
            Session["TransportMode"] = "Air";
        }
        if (Ftvalue == 5)
        {
            PriceTerm = "CIF by AIR";
            Session["TransportMode"] = "Air";
        }
        if (Ftvalue == 4)
        {
            PriceTerm = "FOR Destination";
            Session["TransportMode"] = "Road";
        }
        if (value == 3)
            PaymentTerm = "100% with PO";
        if (value == 4)
            PaymentTerm = "50% with PO + 50% against delivery";
        if (value == 5)
            PaymentTerm = "100% against delivery";
        if (value == 6)
            PaymentTerm = "100% - 15 Days from the date of Invoice";
        if (value == 7)
            PaymentTerm = "100% - 30 Days from the date of Invoice";
        if (value == 8)
            PaymentTerm = "100% - 45 Days from the date of Invoice";
        if (value == 9)
            PaymentTerm = "100% - 60 Days from the date of Invoice";
        foreach (GridViewRow gvr in gvProductPricDetails.Rows)
        {
            DataRow dr = dtOrder.NewRow();
            dr["ProductID"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["ProductId"].ToString();
            dr["ProductName"] = (gvr.Cells[1].Text).Replace("&#160;", "").Trim();
            dr["FTerms"] = PriceTerm;
            dr["PTerms"] = PaymentTerm;
            dr["payment"] = PaymentTerm;
            dr["Price"] = gvr.Cells[value].Text;
            dr["Quantity"] = gvr.Cells[2].Text;
            dr["BuyerId"] = defaultBuyerId;
            int output = 0;
            if (Convert.ToDecimal((gvr.Cells[10].FindControl("txtP25") as TextBox).Text) <= 0 && Convert.ToDecimal((gvr.Cells[11].FindControl("txtP180") as TextBox).Text) <= 0)
            {
                if (Convert.ToDecimal(dr["Quantity"].ToString()) >= 180)
                {
                    dr["PackingDetails180"] = Math.Floor(Convert.ToDecimal(dr["Quantity"].ToString()) / 180);
                    output = Convert.ToInt32(dr["Quantity"].ToString()) % 180; //Math.DivRem(Convert.ToInt32(dr["Quantity"].ToString()), 180, out output);
                }
                else
                {
                    dr["PackingDetails180"] = 0;
                    output = Convert.ToInt32(dr["Quantity"].ToString());
                }
                if (output >= 25)
                {
                    dr["PackingDetails25"] = Math.Ceiling(Convert.ToDouble(output) / 25).ToString();
                }
                else if (output > 0)
                    dr["PackingDetails25"] = 1;
                else
                    dr["PackingDetails25"] = 0;
            }
            else
            {
                dr["PackingDetails25"] = (gvr.Cells[10].FindControl("txtP25") as TextBox).Text;
                dr["PackingDetails180"] = (gvr.Cells[11].FindControl("txtP180") as TextBox).Text;
            }
            dr["TotalPrices"] = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["Quantity"].ToString());
            dr["money"] = lblmoneytype.Text;
            string Qty = gvr.Cells[2].Text;
            dr["Quantity"] = Qty + ".00";
            dtOrder.Rows.Add(dr);
        }
        Session["sDtOrder"] = dtOrder;
    }

    private void BindNewprices(int PayTerms, int value, decimal Fair, decimal FairP, string BuyerType)
    {
        decimal discount = Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["Discount"].ToString());
        decimal tt = Convert.ToDecimal(1 - (Convert.ToDouble(discount) / 100));
        //decimal Fair = Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTrade"].ToString());
        //decimal FairP = Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTradPremium"].ToString());
        //decimal Fair = Session["Organic_S"].ToString() == "2" ? Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTrade"].ToString()) : 0;
        //decimal FairP = Session["Organic_S"].ToString() == "2" ? Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTradPremium"].ToString()) : 0;
        DataTable dtNewprices = new DataTable();
        dtNewprices.Columns.Add("ProductId");
        dtNewprices.Columns.Add("ProductName");
        dtNewprices.Columns.Add("Quantity");
        dtNewprices.Columns.Add("100% Advance");
        dtNewprices.Columns.Add("50%Advance");
        dtNewprices.Columns.Add("100% against delivery");
        dtNewprices.Columns.Add("15_Days");
        dtNewprices.Columns.Add("30_Days");
        dtNewprices.Columns.Add("45_Days");
        dtNewprices.Columns.Add("60_Days");
        dtNewprices.Columns.Add("TotalPrice");
        DataTable dtSeletedProducts = new DataTable();
        DataTable dtQt = Session["dtPricedata"] as DataTable;
        int totalQuantity = 0;
        for (int c = 0; c < dtQt.Rows.Count; c++)
        {
            totalQuantity += Convert.ToInt32(dtQt.Rows[c]["Quantity"].ToString());
        }

        DataTable dtUpdateOrder = new DataTable();
        if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
        {
            dtUpdateOrder = (DataTable)Session["dtUpdateOrder"];
        }
        for (int c = 0; c < dtQt.Rows.Count; c++)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
            {
                dtSeletedProducts = objProduct.GetDetailsAllPrices(Convert.ToInt32(lblFTValue.Text), totalQuantity, Convert.ToInt32(dtQt.Rows[c]["ProductId"].ToString()), Convert.ToDateTime(dtUpdateOrder.Rows[c]["OrderDate"]));
            }
            else
            {
                if (Fair > 0 || FairP > 0)
                {
                    dtSeletedProducts = objProduct.GetDetailsAllPrices(Convert.ToInt32(lblFTValue.Text), 540, Convert.ToInt32(dtQt.Rows[c]["ProductId"].ToString()));
                }
                else
                {
                    dtSeletedProducts = objProduct.GetDetailsAllPrices(Convert.ToInt32(lblFTValue.Text), totalQuantity, Convert.ToInt32(dtQt.Rows[c]["ProductId"].ToString()));
                }
            }

            if (dtSeletedProducts.Rows.Count > 0)
            {
                DataRow dr = dtNewprices.NewRow();
                dr["ProductId"] = dtSeletedProducts.Rows[0]["ProductId"].ToString();
                dr["ProductName"] = dtSeletedProducts.Rows[0]["ProductName"];
                dr["Quantity"] = dtQt.Rows[c]["Quantity"].ToString();
                if (value == 3)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["100% Advance"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["100% Advance"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["100% Advance"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 4)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["50% Advance + 50% against delivery"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["50%Advance"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["50%Advance"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 5)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["100% against delivery"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["100% against delivery"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["100% against delivery"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 6)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["15_Days"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["15_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["15_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 7)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["30_Days"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["30_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["30_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 8)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["45_Days"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["45_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["45_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                if (value == 9)
                {
                    decimal Tprice = Convert.ToDecimal(dtSeletedProducts.Rows[0]["60_Days"].ToString());
                    decimal Fprice = ((Tprice * tt) + Fair + FairP);
                    if (BuyerType == "1")
                    {
                        decimal d = Math.Round(Fprice, 1);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["60_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                    else
                    {
                        decimal d = Math.Round(Fprice, 0);
                        string dStr = string.Format("{0:0.00}", d);
                        dr["60_Days"] = dStr;
                        decimal TotalPrice = d * Convert.ToDecimal(dr["Quantity"].ToString());
                        string TotPrice = string.Format("{0:0.00}", TotalPrice);
                        dr["TotalPrice"] = TotPrice;
                    }
                }
                dtNewprices.Rows.Add(dr);
            }
        }
        Session["new"] = dtNewprices;
        gvProductPricDetails.Columns[value].Visible = true;
        gvProductPricDetails.DataSource = dtNewprices.DefaultView;
        gvProductPricDetails.DataBind();
    }
    #endregion

    #region Navigation
    // product list
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 1;
        btnTProducts.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTProducts.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTQunatity.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTProducts.CssClass = "btnFarmer1";
        btnTQunatity.CssClass = "btnFarmer1 bactive";
    }
    //quantity
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int checkCount = 0;
            foreach (GridViewRow item in grdProdQuantity.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = item.Cells[3].FindControl("cbitem") as CheckBox;
                    if (chk.Checked)
                    {
                        string quantity = (item.Cells[2].FindControl("txtQuantity") as TextBox).Text.Trim();
                        //string quantity = (item.Cells[2].FindControl("ddlQuantity") as DropDownList).SelectedValue;
                        Regex regEx = new Regex(@"^\d{1,5}(\.\d{1,2})?$");
                        if (Session["dtUpdateOrder"] != null)
                        {
                            if ((Session["dtUpdateOrder"] as DataTable).Rows.Count > 0)
                            {
                                dtUpdateOrder = (Session["dtUpdateOrder"] as DataTable);
                                foreach (DataRow dr in dtUpdateOrder.Rows)
                                {
                                    if (dr["ProductID"].ToString() == item.Cells[0].Text)
                                    {
                                        if (Convert.ToDecimal(quantity) <= Convert.ToDecimal(dr["Quantity"].ToString()))
                                        {

                                        }
                                        else
                                        {
                                            Show("!!! Enter the Quantity not greater than the Lotsample Quantity as !!!");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
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
                        else if (Convert.ToDecimal(quantity) < 1)
                        {
                            Show("!!! Quantity must be atleast one KG !!!");
                            return;
                        }
                        else if (Convert.ToDecimal(quantity) < 1)
                        {
                            Show("!!! Quantity must be atleast one KG !!!");
                            return;
                        }
                        checkCount += 1;
                    }
                }
            }
            DataTable productList = Session["dtPricedata"] as DataTable;
            DataTable dtQty = new DataTable();
            dtQty.Columns.Add("Quantity");
            DataTable selectedProductlist = new DataTable();
            foreach (DataColumn dc in productList.Columns)
            {
                selectedProductlist.Columns.Add(dc.ColumnName);
            }
            selectedProductlist.Columns.Add("Quantity");
            foreach (GridViewRow item in grdProdQuantity.Rows)
            {
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = item.Cells[3].FindControl("cbitem") as CheckBox;
                    if (chk.Checked)
                    {
                        string s = item.Cells[1].Text.Replace("&#160;", "").Trim();
                        foreach (DataRow rd in productList.Rows)
                        {
                            string pName = Convert.ToString(rd["ProductName"]).Trim();
                            //string quantity = (item.Cells[2].FindControl("ddlQuantity") as DropDownList).SelectedValue;
                            string quantity = (item.Cells[2].FindControl("txtQuantity") as TextBox).Text.Trim();
                            if (string.Equals(s, pName, StringComparison.OrdinalIgnoreCase))
                            {
                                DataRow sRow = selectedProductlist.NewRow();
                                foreach (DataColumn dc in productList.Columns)
                                {
                                    sRow[dc.ColumnName] = rd[dc.ColumnName];
                                }
                                sRow["Quantity"] = quantity;
                                dtQty.Rows.Add(quantity);
                                selectedProductlist.Rows.Add(sRow);
                                break;
                            }
                        }
                    }
                }
            }
            Session["dtPricedata"] = selectedProductlist;
            //mvPrice.ActiveViewIndex = 2;
            //btnTQunatity.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnTQunatity.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
            ////btnTOrganic.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            //btnTQunatity.CssClass = "btnFarmer1";
            //btnTOrganic.CssClass = "btnFarmer1 bactive";
            SetNextTab("organic");
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx", false);
        }
    }
    //organic
    protected void btnOrganic_Click(object sender, EventArgs e)
    {
        //mvPrice.ActiveViewIndex = 3;
        //btnTOrganic.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTOrganic.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnFT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnTOrganic.CssClass = "btnFarmer1";
        //btnFT.CssClass = "btnFarmer1 bactive";

        lblOrgFair.Text = "0.00";
        lblOrgFTP.Text = "0.00";
        lblOrgType.Text = "0";
        SetNextTab("freightterms");
    }
    // organic FT
    protected void btnOrganicFair_Click(object sender, EventArgs e)
    {
        //mvPrice.ActiveViewIndex = 3;
        //btnTOrganic.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTOrganic.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnFT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnTOrganic.CssClass = "btnFarmer1";
        //btnFT.CssClass = "btnFarmer1 bactive";
        lblOrgFair.Text = (Session["sDtBuyers"] as DataTable).Rows[0]["FairTrade"].ToString();
        lblOrgFTP.Text = (Session["sDtBuyers"] as DataTable).Rows[0]["FairTradPremium"].ToString();
        lblOrgType.Text = "1";
        SetNextTab("freightterms");
    }
    // Fright Terms
    protected void btnFrightTermssubmit_Click(object sender, EventArgs e)
    {
        if (lblFTValue.Text == "1")
            rbFob_SelectedIndexChanged(sender, e);
        if (lblFTValue.Text == "2")
            lblPlacedelivey.Text = txtSeaPort.Text;
        if (lblFTValue.Text == "3" || lblFTValue.Text == "5")
            lblPlacedelivey.Text = txtAirPort.Text;
        //mvPrice.ActiveViewIndex = 4;
        //btnFT.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnFT.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        ////btnTPT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnFT.CssClass = "btnFarmer1";
        //btnTPT.CssClass = "btnFarmer1 bactive";
        SetNextTab("paymentterms");
    }
    //Payment Terms
    protected void btnPaymentTermsSubmit_Click(object sender, EventArgs e)
    {
        //mvPrice.ActiveViewIndex = 5;
        //if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
            //if (dtUpdateOrder != null)
            //btnLotSample.Visible = false;
        BindNewprices(Convert.ToInt32(lblFTValue.Text), Convert.ToInt32(lblPTValue.Text), Convert.ToDecimal(lblOrgFair.Text), Convert.ToDecimal(lblOrgFTP.Text), lblmoneytype.Text);
        //btnTPT.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTPT.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        ////btnTOrderDetails.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnTPT.CssClass = "btnFarmer1";
        //btnTOrderDetails.CssClass = "btnFarmer1 bactive";
        SetNextTab("orderdetails");
    }
    //place the order
    protected void btnorder_Click(object sender, EventArgs e)
    {
        divplaceorder.Visible = true;
        divLotsample.Visible = false;
        //mvPrice.ActiveViewIndex = 6;
        //btnTOrderDetails.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTOrderDetails.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        ////btnTPlaceorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //btnTOrderDetails.CssClass = "btnFarmer1";
        //btnTPlaceorder.CssClass = "btnFarmer1 bactive";
        SetNextTab("placeorder");
    }
    // lot sample
    protected void btnLotSample_Click(object sender, EventArgs e)
    {
        //mvPrice.ActiveViewIndex = 6;
        //divLotsample.Visible = true;
        //divplaceorder.Visible = false;
        //btnTOrderDetails.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTOrderDetails.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTPlaceorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        Session["OrderType"] = "LotSample";
        string buyer = defaultBuyerId;
        string strPO = string.Empty;
        BindOrder(Convert.ToInt32(lblFTValue.Text), Convert.ToInt32(lblPTValue.Text));
        strPO = txtPO.Text + "$" + txtPODate.Text + "$" + FileUpload1.FileName + "$Lot Sample";
        if (FileUpload1.FileName.Length > 0)
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
        //File.Move(Server.MapPath("~/Attachments/") + str, Server.MapPath("~/Attachments/Farmer/") + str);
        Session["s_PODetails"] = strPO;
        Session["Order_PO"] = "0";
        Session["placeofdelivery"] = lblPlacedelivey.Text;
        if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
            Response.Redirect("../Reports/PurchaseOrder.aspx");

    }
    /* po details submit */
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            Session["OrderType"] = "order";
            string buyer = defaultBuyerId;
            string strPO = string.Empty;
            BindOrder(Convert.ToInt32(lblFTValue.Text), Convert.ToInt32(lblPTValue.Text));
            strPO = txtPO.Text + "$" + txtPODate.Text + "$" + FileUpload1.FileName + "$" + txtcomments.Text;
            if (FileUpload1.FileName.Length > 0)
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
            Session["s_PODetails"] = strPO;
            if (!string.IsNullOrEmpty(Request.QueryString["lotid"]) && Session["dtUpdateOrder"] != null)
            //if(dtUpdateOrder!=null)
            {
                if (Session["Split"] == "3")
                {
                    Session["Order_PO"] = "3";
                    Session["SplitText"] = Session["Split"].ToString();
                }
                else
                {
                    Session["Order_PO"] = "2";
                }
            }
            else
            {
                Session["Order_PO"] = "1";
            }
            Session["placeofdelivery"] = lblPlacedelivey.Text;
            Session["OrganicType"] = lblOrgType.Text;

            if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
            {
                Response.Redirect("../Reports/PurchaseOrder.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('You clicked NO!')", true);
        }
    }
    /* comment by:Bhanu on 3/1/2014 lotsample submit */
    protected void btnLotSamples_Click(object sender, EventArgs e)
    {
        //string buyer = Session["BuyerId"].ToString();
        //string strPO = string.Empty;
        //BindOrder(Convert.ToInt32(lblFTValue.Text), Convert.ToInt32(lblPTValue.Text));
        //strPO = txtPO.Text + "$" + txtPODate.Text + "$" + FileUpload1.FileName + "$Lot Sample";
        //if (FileUpload1.FileName.Length > 0)
        //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
        ////File.Move(Server.MapPath("~/Attachments/") + str, Server.MapPath("~/Attachments/Farmer/") + str);
        //Session["s_PODetails"] = strPO;
        //Session["Order_PO"] = "0";
        //Session["placeofdelivery"] = lblPlacedelivey.Text;
        //Session["FOB"] = lblFoBPort.Text;
        //if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
        //    Response.Redirect("../Reports/PurchaseOrder.aspx");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtTotalDays.Text = string.Empty;
        divLotsample.Visible = false;
    }
    #endregion
    protected void rbFob_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBuyer = BBL.BuyerDetails(defaultBuyerId);
        //if (rbFob.SelectedValue == "0")
        if(rbtnFobAir.Checked)
        {
            lblPlacedelivey.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            lblFoBPort.Text = "Air";
        }
        else
        {
            lblFoBPort.Text = "Sea";
            lblPlacedelivey.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
        }
    }
    protected void gvProductPricDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //on you condition
            TextBox txt = (TextBox)e.Row.FindControl("txtP180");
            if (txt != null && e.Row.Cells[0].Text == "4")
            {
                txt.Attributes.Add("readonly", "readonly");
                // txt.Attributes.Remove("readonly"); To remove readonly attribute
            }
        }
    }

    private void SetNextTab(string tabname)
    {
        switch (tabname)
        {
            //productquantity
            case "organic":
                liproductquantity.Attributes["class"] = "done";
                tabProductQuantity.Attributes["class"] = "tab-pane";

                liorganic.Attributes["class"] = "active";
                tabOrganic.Attributes["class"] = "tab-pane active";

                lifreightterms.Attributes["class"] = "";
                tabFreightTerms.Attributes["class"] = "tab-pane";

                lipaymentterms.Attributes["class"] = "";
                tabPaymentTerms.Attributes["class"] = "tab-pane";

                liorderdetails.Attributes["class"] = "";
                tabOrderDetails.Attributes["class"] = "tab-pane";

                liplaceorder.Attributes["class"] = "";
                tabPlaceorder.Attributes["class"] = "tab-pane";

                divProgress.Attributes["style"] = "width:33.4%";
                spanCount.InnerHtml = "2";
                break;
            case "freightterms":
                liproductquantity.Attributes["class"] = "done";
                tabProductQuantity.Attributes["class"] = "tab-pane";

                liorganic.Attributes["class"] = "done";
                tabOrganic.Attributes["class"] = "tab-pane";

                lifreightterms.Attributes["class"] = "active";
                tabFreightTerms.Attributes["class"] = "tab-pane active";

                lipaymentterms.Attributes["class"] = "";
                tabPaymentTerms.Attributes["class"] = "tab-pane";

                liorderdetails.Attributes["class"] = "";
                tabOrderDetails.Attributes["class"] = "tab-pane";

                liplaceorder.Attributes["class"] = "";
                tabPlaceorder.Attributes["class"] = "tab-pane";

                divProgress.Attributes["style"] = "width:50.07%";
                spanCount.InnerHtml = "3";
                break;
            case "paymentterms":
                liproductquantity.Attributes["class"] = "done";
                tabProductQuantity.Attributes["class"] = "tab-pane";

                liorganic.Attributes["class"] = "done";
                tabOrganic.Attributes["class"] = "tab-pane";

                lifreightterms.Attributes["class"] = "done";
                tabFreightTerms.Attributes["class"] = "tab-pane";

                lipaymentterms.Attributes["class"] = "active";
                tabPaymentTerms.Attributes["class"] = "tab-pane active";

                liorderdetails.Attributes["class"] = "";
                tabOrderDetails.Attributes["class"] = "tab-pane";

                liplaceorder.Attributes["class"] = "";
                tabPlaceorder.Attributes["class"] = "tab-pane";

                divProgress.Attributes["style"] = "width:66.74%";
                spanCount.InnerHtml = "4";
                break;

            case "orderdetails":
                liproductquantity.Attributes["class"] = "done";
                tabProductQuantity.Attributes["class"] = "tab-pane";

                liorganic.Attributes["class"] = "done";
                tabOrganic.Attributes["class"] = "tab-pane";

                lifreightterms.Attributes["class"] = "done";
                tabFreightTerms.Attributes["class"] = "tab-pane";

                lipaymentterms.Attributes["class"] = "done";
                tabPaymentTerms.Attributes["class"] = "tab-pane";

                liorderdetails.Attributes["class"] = "active";
                tabOrderDetails.Attributes["class"] = "tab-pane active";

                liplaceorder.Attributes["class"] = "";
                tabPlaceorder.Attributes["class"] = "tab-pane";

                divProgress.Attributes["style"] = "width:83.41%";
                spanCount.InnerHtml = "5";
                break;
            case "placeorder":
                liproductquantity.Attributes["class"] = "done";
                tabProductQuantity.Attributes["class"] = "tab-pane";

                liorganic.Attributes["class"] = "done";
                tabOrganic.Attributes["class"] = "tab-pane";

                lifreightterms.Attributes["class"] = "done";
                tabFreightTerms.Attributes["class"] = "tab-pane";

                lipaymentterms.Attributes["class"] = "done";
                tabPaymentTerms.Attributes["class"] = "tab-pane";

                liorderdetails.Attributes["class"] = "done";
                tabOrderDetails.Attributes["class"] = "tab-pane";

                liplaceorder.Attributes["class"] = "active";
                tabPlaceorder.Attributes["class"] = "tab-pane active";

                divProgress.Attributes["style"] = "width:100%";
                spanCount.InnerHtml = "6";
                break;
        }
    }
}
