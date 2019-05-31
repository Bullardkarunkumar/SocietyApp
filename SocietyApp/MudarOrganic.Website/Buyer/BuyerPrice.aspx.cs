using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using AjaxControlToolkit;
using System.IO;

public partial class Buyer_BuyerPrice : System.Web.UI.Page
{
    Product_BL objProduct = new Product_BL();
    Buyer_BL BBL = new Buyer_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["dtPricedata"] = new object();
            Session["sDtOrder"] = new object();
            BindBuyerDetails();
            Session["sDtBuyers"] = BBL.BuyerDetails(Session["BuyerId"].ToString());
            Session["Organic_S"] = "1";
            Session["Order_PO"] = "0";
            BindgvProductPricDetails("ALL", ddlDays.Text);
            txtPODate.Text = DateTime.Now.ToShortDateString();
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
                rbFobIndia.Checked = true;
                rbForDestination.Enabled = false;
                rbFobIndia.Enabled = true;
                rbCIFAir.Enabled = true;
                rbCIFSea.Enabled = true;
            }
        }
        
    }
    private void BindBuyerDetails()
    {
        DataTable dtBuyer = BBL.BuyerDetails(Session["BuyerId"].ToString());
        if (dtBuyer.Rows.Count > 0)
        {
            //buyer country  CCountry
            if (dtBuyer.Rows[0]["CCountry"].ToString() == "INDIA")
                lblmoneytype.Text = "0";
            else
                lblmoneytype.Text = "1";
            //price terms
            if (Convert.ToBoolean(dtBuyer.Rows[0]["FOB_India"].ToString()) == true)
            {
                rbFobIndia.Checked = true;
                rbFob.Visible = true;
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Sea_By"].ToString()) == true)
            {
                rbCIFSea.Checked = true;
                txtSeaPort.Visible = true;
                txtSeaPort.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true)
            {
                rbCIFAir.Checked = true;
                txtAirPort.Visible = true;
                txtAirPort.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                
            }
            if (Convert.ToBoolean(dtBuyer.Rows[0]["FORDestination"].ToString()) == true)
            {
                rbForDestination.Checked = true;
                lblPlacedelivey.Text = dtBuyer.Rows[0]["RoadDestination"].ToString();
            }
            //payment terms
            foreach(ListItem item in rbfreight.Items)
            {
                if (Convert.ToBoolean(dtBuyer.Rows[0]["100%advance"].ToString()) == true && item.Text == "100% Advance")
                {
                    item.Selected = true;
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["100%againstDocs"].ToString()) == true &&  item.Text == "100% against delivery")
                {
                    item.Selected = true;
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["50%adv+50%againstDocs"].ToString()) == true && item.Text == "50% Advance + 50% against delivery")
                {
                    item.Selected = true;
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["NoofDaysfromInvoice"].ToString()) == true && item.Text == "100% payment against number of SELECTED DAYS")
                {
                    item.Selected = true;
                    //lblCreditDays.Text = dtBuyer.Rows[0]["No_of_Days_Count_fromInvoice"].ToString();
                    CreditDays.Visible = true;
                }
            }
        }
    }
    private void BindgvProductPricDetails(string name,string creditDay)
    {
        string[] value = name.Split('-');
        DataTable productlist = new DataTable();
        if (value.Length > 1)
        {
            productlist = objProduct.GetProductPricebyName(value[1].Trim(), creditDay);
        }
        else
        {
            productlist = objProduct.GetProductPricebyBuyer(creditDay, Session["BuyerId"].ToString());
            if (productlist.Rows.Count < 1)
            {
                //print Message zero product register.
                lblmsg.Visible = true;
                btnAccept.Enabled = false;
            }
            //productlist = objProduct.GetProductPricebyName(name, creditDay);
        }
        //if (productlist.Rows.Count > 0)
        //{
        //    productlist.Columns.Add("Fair");
        //    productlist.Columns.Add("Discount");
        //}

        Session["dtPricedata"] = productlist;
        gvProductPricDetails.DataSource = productlist;
        gvProductPricDetails.DataBind();
        gvProductSpecification.DataSource = productlist;
        gvProductSpecification.DataBind();
    }
    protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgvProductPricDetails("ALL", ddlDays.Text);
        Bindgvproduct_Price();
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    //mvPrice.ActiveViewIndex = 2;
    //    //Bindgvproduct_Price();
    //}
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 5;
    }
    protected void btnPTBack_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 5;
    }
    protected void btnPTNext_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 2;
    }
    protected void btnFTBack_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 1;
    }
    protected void btnFTNext_Click(object sender, EventArgs e)
    {
        mvPrice.ActiveViewIndex = 3;
        if (rbfreight.SelectedValue == "4")
            ddlDays.Enabled = true;
        else
            ddlDays.Enabled = false;
        Bindgvproduct_Price();
    }
    protected void btnorder_Click(object sender, EventArgs e)
    {
        int orderIteam = 0;
        foreach (GridViewRow gvr in gvProductPricDetails.Rows)
        {
            if ((gvr.Cells[0].FindControl("cbitem") as CheckBox).Checked)
            {
                orderIteam += 1;
            }
        }
        if (orderIteam <= 0)
        {
            Response.Write("<Script>alert('Please select Product to process.')</script>");
            return;
        }
        mvPrice.ActiveViewIndex = 4;
    }
    protected void btnOrganic_Click(object sender, EventArgs e)
    {
        Session["Organic_S"] = "1";
        mvPrice.ActiveViewIndex = 1;
    }
    protected void btnOrganicFair_Click(object sender, EventArgs e)
    {
        Session["Organic_S"] = "2";
        mvPrice.ActiveViewIndex = 1;
    }
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        bool result;
        string buyer = Session["BuyerId"].ToString();
        string strPO = string.Empty;
        //if (rbFob.SelectedItem.Text == "Sea")
        //    lblPlacedelivey.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
        //else
        //    lblPlacedelivey.Text = dtBuyer.Rows[0]["AirportName"].ToString();
        result = BBL.BuyerPaymentandPriceUpdateDetails(buyer, Convert.ToBoolean(rbfreight.Items[0].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[1].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[2].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[3].Selected ? 1 : 0), Convert.ToInt32(ddlDays.SelectedValue), 0, "Bhanu");
        BindOrder();

        strPO = txtPO.Text + "$" + txtPODate.Text + "$" + FileUpload1.FileName + "$" + txtcomments.Text;
        if (FileUpload1.FileName.Length > 0)
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
        //File.Move(Server.MapPath("~/Attachments/") + str, Server.MapPath("~/Attachments/Farmer/") + str);
        Session["s_PODetails"] = strPO;
        Session["Order_PO"] = "1";
        Session["FOB"] = lblFoBPort.Text;
        Session["placeofdelivery"] = lblPlacedelivey.Text;
        if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
            Response.Redirect("../Reports/PurchaseOrder.aspx");
    }
    protected void btnorderWithoutPO_Click(object sender, EventArgs e)
    {
        //place order with out po
        bool result;
        string buyer = Session["BuyerId"].ToString();
        string strPO = string.Empty;
        result = BBL.BuyerPaymentandPriceUpdateDetails(buyer, Convert.ToBoolean(rbfreight.Items[0].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[1].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[2].Selected ? 1 : 0), Convert.ToBoolean(rbfreight.Items[3].Selected ? 1 : 0), Convert.ToInt32(ddlDays.SelectedValue), 0, "Bhanu");
        int orderIteam = 0;
        foreach (GridViewRow gvr in gvProductPricDetails.Rows)
        {
            if ((gvr.Cells[0].FindControl("cbitem") as CheckBox).Checked)
            {
                orderIteam += 1;
            }
        }
        if (orderIteam <= 0)
        {
            Response.Write("<Script>alert('!!!! Please select Product to !!!!')</script>");
            return;
        }
        BindOrder();
        strPO = txtPO.Text + "$" + txtPODate.Text + "$" + FileUpload1.FileName + "$Order Without PO.";
        if (FileUpload1.FileName.Length > 0)
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
        //File.Move(Server.MapPath("~/Attachments/") + str, Server.MapPath("~/Attachments/Farmer/") + str);
        Session["s_PODetails"] = strPO;
        Session["Order_PO"] = "0";
        Session["placeofdelivery"] = lblPlacedelivey.Text;
        Session["FOB"] = lblFoBPort.Text;
        if ((Session["sDtOrder"] as DataTable).Rows.Count > 0)
            Response.Redirect("../Reports/PurchaseOrder.aspx");
    }

    private void BindOrder()
    {
        DataTable dtOrder = new DataTable();
        dtOrder.Columns.Add("ProductID");
        dtOrder.Columns.Add("ProductName");
        dtOrder.Columns.Add("Specification");
        dtOrder.Columns.Add("itchscode");
        dtOrder.Columns.Add("PriceID");
        dtOrder.Columns.Add("payment");
        dtOrder.Columns.Add("freight");
        dtOrder.Columns.Add("USA_Air");
        dtOrder.Columns.Add("Europe_Sea");
        dtOrder.Columns.Add("Europe_Air");
        dtOrder.Columns.Add("Price");
        dtOrder.Columns.Add("Quantity");
        dtOrder.Columns.Add("BuyerId");
        dtOrder.Columns.Add("PriceHistoryId");
        dtOrder.Columns.Add("PackingDetails25");
        dtOrder.Columns.Add("PackingDetails180");
        dtOrder.Columns.Add("TotalPrices");
        dtOrder.Columns.Add("money");

        foreach (GridViewRow gvr in gvProductPricDetails.Rows)
        {
            if ((gvr.Cells[33].FindControl("cbitem") as CheckBox).Checked)
            {
                if (Convert.ToInt32((gvr.Cells[30].FindControl("txtQuantity") as TextBox).Text) == 0)
                {
                    Response.Write("<script>alert('!!! Please Enter the Quantity !!!');</script>");
                    return;
                }
                else
                {
                    DataRow dr = dtOrder.NewRow();
                    dr["ProductID"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["ProductId"].ToString();
                    dr["ProductName"] = gvr.Cells[1].Text;
                    dr["Specification"] = gvr.Cells[2].Text;
                    dr["itchscode"] = gvr.Cells[3].Text;
                    dr["PriceID"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["PriceId"].ToString();
                    dr["payment"] = rbfreight.SelectedItem.Text; //gvr.Cells[5].Text;
                    dr["freight"] = gvr.Cells[6].Text;
                    dr["USA_Air"] = gvr.Cells[7].Text;
                    dr["Europe_Sea"] = gvr.Cells[8].Text;
                    dr["Europe_Air"] = gvr.Cells[9].Text;
                    int cell = Convert.ToInt16(hfFreight.Value) + (Convert.ToInt16(rbfreight.SelectedValue) - 1);
                    dr["Price"] = gvr.Cells[cell].Text;
                    dr["Quantity"] = (gvr.Cells[30].FindControl("txtQuantity") as TextBox).Text;
                    dr["BuyerId"] = Session["BuyerId"].ToString();
                    dr["PriceHistoryId"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["PriceHistoryId"].ToString();
                    int output = 0;
                    if (Convert.ToDecimal((gvr.Cells[30].FindControl("txtP25") as TextBox).Text) <= 0 && Convert.ToDecimal((gvr.Cells[30].FindControl("txtP180") as TextBox).Text) <= 0)
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
                            //dr["PackingDetails25"] = Math.Ceiling(Convert.ToDecimal(dr["Quantity"].ToString()) / 25);
                            dr["PackingDetails25"] = Math.Ceiling(Convert.ToDouble(output) / 25).ToString();
                        }
                        else if (output > 0)
                            dr["PackingDetails25"] = 1;
                        else
                            dr["PackingDetails25"] = 0;
                    }
                    else
                    {
                        dr["PackingDetails25"] = (gvr.Cells[30].FindControl("txtP25") as TextBox).Text;
                        dr["PackingDetails180"] = (gvr.Cells[30].FindControl("txtP180") as TextBox).Text;
                    }
                    dr["TotalPrices"] = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["Quantity"].ToString());
                    dr["money"] = lblmoneytype.Text;
                    dtOrder.Rows.Add(dr);
                }
            }
        }
        Response.Write("<script>alert('!!! Please Enter the Quantity !!!');</script>");
        Session["sDtOrder"] = dtOrder;
    }
    private void Bindgvproduct_Price()
    {
        gvProductPricDetails.Columns[2].Visible = false;
        gvProductPricDetails.Columns[3].Visible = false;
        gvProductPricDetails.Columns[4].Visible = false;
        gvProductPricDetails.Columns[5].Visible = false;
        gvProductPricDetails.Columns[6].Visible = false;
        gvProductPricDetails.Columns[7].Visible = false;
        gvProductPricDetails.Columns[8].Visible = false;
        gvProductPricDetails.Columns[9].Visible = false;
        gvProductPricDetails.Columns[10].Visible = false;
        gvProductPricDetails.Columns[11].Visible = false;
        gvProductPricDetails.Columns[12].Visible = false;
        gvProductPricDetails.Columns[13].Visible = false;
        gvProductPricDetails.Columns[14].Visible = false;
        gvProductPricDetails.Columns[15].Visible = false;
        gvProductPricDetails.Columns[16].Visible = false;
        gvProductPricDetails.Columns[17].Visible = false;
        gvProductPricDetails.Columns[18].Visible = false;
        gvProductPricDetails.Columns[19].Visible = false;
        gvProductPricDetails.Columns[20].Visible = false;
        gvProductPricDetails.Columns[21].Visible = false;
        gvProductPricDetails.Columns[22].Visible = false;
        gvProductPricDetails.Columns[23].Visible = false;
        gvProductPricDetails.Columns[24].Visible = false;
        gvProductPricDetails.Columns[25].Visible = false;
        gvProductPricDetails.Columns[26].Visible = false;
        gvProductPricDetails.Columns[27].Visible = false;
        gvProductPricDetails.Columns[28].Visible = false;
        gvProductPricDetails.Columns[29].Visible = false;
        gvProductPricDetails.Columns[34].Visible = false;
        gvProductPricDetails.Columns[35].Visible = false;
        gvProductPricDetails.Columns[36].Visible = false;
        gvProductPricDetails.Columns[37].Visible = false;

        gvProductPricDetails.DataBind();
        lblprice.Text = string.Empty;
        int pt = Convert.ToInt16(rbfreight.SelectedValue) - 1;

        decimal discount = Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["Discount"].ToString());
        decimal Fair = Session["Organic_S"].ToString() == "2" ? Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTrade"].ToString()) : 0;
        decimal FairP = Session["Organic_S"].ToString() == "2" ? Convert.ToDecimal((Session["sDtBuyers"] as DataTable).Rows[0]["FairTradPremium"].ToString()) : 0;

        if (rbFobIndia.Checked)
        {
            hfFreight.Value = "2";
            lblprice.Text = "FOB INDIA price shown in USA ($) ";
            DataTable dtBuyer = BBL.BuyerDetails(Session["BuyerId"].ToString());
            if (dtBuyer.Rows.Count > 0)
            {
                string transportmode = string.Empty;
                if (rbFob.SelectedValue == "0")
                {
                    lblPlacedelivey.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                    lblFoBPort.Text = "Air";
                }
                else
                {
                    lblFoBPort.Text = "Sea";
                    lblPlacedelivey.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                }
                gvProductPricDetails.Columns[2 + pt].Visible = true;
                //gvProductPricDetails.Columns[3].Visible = true;
                //gvProductPricDetails.Columns[4].Visible = true;
                //gvProductPricDetails.Columns[5].Visible = true;

                gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
                gvProductPricDetails.DataBind();
                for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
                {
                    decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[2 + pt].Text);
                    gvProductPricDetails.Rows[count].Cells[2 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                    //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                    //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                    //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                    //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
                }
            }
        }
        //else if (rbCNFSea.Checked)
        //{
        //    hfFreight.Value = "6";
        //    lblprice.Text = "CNF SEA";
        //    gvProductPricDetails.Columns[6 + pt].Visible = true;
        //    //gvProductPricDetails.Columns[7].Visible = true;
        //    //gvProductPricDetails.Columns[8].Visible = true;
        //    //gvProductPricDetails.Columns[9].Visible = true;
        //    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
        //    gvProductPricDetails.DataBind();
        //}
        //else if (rbCNFAir.Checked)
        //{
        //    hfFreight.Value ="10";
        //    lblprice.Text = "CNF AIR (Europe & East USA)";
        //    gvProductPricDetails.Columns[10 + pt].Visible = true;
        //    //gvProductPricDetails.Columns[11].Visible = true;
        //    //gvProductPricDetails.Columns[12].Visible = true;
        //    //gvProductPricDetails.Columns[13].Visible = true;
        //    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
        //    gvProductPricDetails.DataBind();
        //}
        //else if (rbCNFAir_West.Checked)
        //{
        //    hfFreight.Value = "14";
        //    lblprice.Text = "CNF AIR West";
        //    gvProductPricDetails.Columns[14 + pt].Visible = true;
        //    //gvProductPricDetails.Columns[15].Visible = true;
        //    //gvProductPricDetails.Columns[16].Visible = true;
        //    //gvProductPricDetails.Columns[17].Visible = true;
        //    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
        //    gvProductPricDetails.DataBind();
        //}
        else if (rbCIFSea.Checked)
        {
            hfFreight.Value = "18";
            txtSeaPort.Visible = true;
            lblPlacedelivey.Text = txtSeaPort.Text;
            lblprice.Text = "CIF SEA price shown in USA ($) ";
            gvProductPricDetails.Columns[18 + pt].Visible = true;
            //gvProductPricDetails.Columns[19].Visible = true;
            //gvProductPricDetails.Columns[20].Visible = true;
            //gvProductPricDetails.Columns[21].Visible = true;
            gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
            gvProductPricDetails.DataBind();
            for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
            {
                decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[18 + pt].Text);
                gvProductPricDetails.Rows[count].Cells[18 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
            }
        }
        else if (rbCIFAir.Checked)
        {
            DataTable dtBuyer = BBL.BuyerDetails(Session["BuyerId"].ToString());
            if (dtBuyer.Rows.Count > 0)
            {
                if (dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString().ToLower().Trim() == "true")
                {
                    hfFreight.Value = "22";
                    lblPlacedelivey.Text = txtAirPort.Text;
                    lblprice.Text = "CIF AIR price shown in USA ($) ";
                    gvProductPricDetails.Columns[22 + pt].Visible = true;
                    //gvProductPricDetails.Columns[23].Visible = true;
                    //gvProductPricDetails.Columns[24].Visible = true;
                    //gvProductPricDetails.Columns[25].Visible = true;
                    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
                    gvProductPricDetails.DataBind();
                    for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
                    {
                        decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[22 + pt].Text);
                        gvProductPricDetails.Rows[count].Cells[22 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                        //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                        //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
                    }
                }
                else if (dtBuyer.Rows[0]["CIF_AIR_By_WEST_USA"].ToString().ToLower().Trim() == "true")
                {
                    hfFreight.Value = "26";
                    lblprice.Text = "CIF AIR West price shown in USA ($) ";
                    gvProductPricDetails.Columns[26 + pt].Visible = true;
                    //gvProductPricDetails.Columns[27].Visible = true;
                    //gvProductPricDetails.Columns[28].Visible = true;
                    //gvProductPricDetails.Columns[29].Visible = true;
                    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
                    gvProductPricDetails.DataBind();
                    for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
                    {
                        decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[26 + pt].Text);
                        gvProductPricDetails.Rows[count].Cells[26 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                        //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                        //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
                    }
                }
                else // if data in not avaible 
                {
                    hfFreight.Value = "22";
                    lblprice.Text = "CIF AIR price shown in USA ($) ";
                    gvProductPricDetails.Columns[22 + pt].Visible = true;
                    //gvProductPricDetails.Columns[23].Visible = true;
                    //gvProductPricDetails.Columns[24].Visible = true;
                    //gvProductPricDetails.Columns[25].Visible = true;
                    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
                    gvProductPricDetails.DataBind();
                    for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
                    {
                        decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[22 + pt].Text);
                        gvProductPricDetails.Rows[count].Cells[22 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                        //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                        //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                        //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
                    }
                }
            }
        }
        else if (rbForDestination.Checked)
        {
            hfFreight.Value = "34";
            lblprice.Text = "INDIA price shown in IND (Rs) ";
            gvProductPricDetails.Columns[34 + pt].Visible = true;
            //gvProductPricDetails.Columns[19].Visible = true;
            //gvProductPricDetails.Columns[20].Visible = true;
            //gvProductPricDetails.Columns[21].Visible = true;
            gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
            gvProductPricDetails.DataBind();
            for (int count = 0; count < (Session["dtPricedata"] as DataTable).Rows.Count; count++)
            {
                decimal Tprice = Convert.ToDecimal(gvProductPricDetails.Rows[count].Cells[34 + pt].Text);
                gvProductPricDetails.Rows[count].Cells[34 + pt].Text = string.Format("{0:0.00}", ((Tprice - (Tprice * (discount / 100))) + Fair + FairP));
                //(Session["dtPricedata"] as DataTable).Rows[count]["Discount"] = (Tprice - (Tprice * (discount / 100))).ToString();
                //(Session["dtPricedata"] as DataTable).Rows[count]["Fair"] = ((Tprice - (Tprice * (discount / 100))) + Fair + FairP).ToString();
                //gvProductPricDetails.Rows[count].Cells[34].Text = string.Format("{0:0.00}", Tprice - (Tprice * (discount / 100)));
                //gvProductPricDetails.Rows[count].Cells[35].Text = string.Format("{0:0.00}", (Tprice - (Tprice * (discount / 100))) + Fair + FairP);
            }
        }
        //else if (rbCIFAir_West.Checked)
        //{
        //    hfFreight.Value = "26";
        //    lblprice.Text = "CIF AIR West";
        //    gvProductPricDetails.Columns[26 + pt].Visible = true;
        //    //gvProductPricDetails.Columns[27].Visible = true;
        //    //gvProductPricDetails.Columns[28].Visible = true;
        //    //gvProductPricDetails.Columns[29].Visible = true;
        //    gvProductPricDetails.DataSource = (Session["dtPricedata"] as DataTable);
        //    gvProductPricDetails.DataBind();
        //}
    }
    protected void rbFob_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
