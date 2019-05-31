using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MudarOrganic.BL;
public partial class Orders_OrderForm : System.Web.UI.Page
{
    Product_BL objProduct = new Product_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["dtPricedata"] = new object();
            Session["sDtOrder"] = new object();
            BindgvProductPricDetails("ALL");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindgvProductPricDetails(txtSproductName.Text);
    }
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        BindOrder();
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
        dtOrder.Columns.Add("FOBPrice");
        dtOrder.Columns.Add("USA_Sea");
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
            if ((gvr.Cells[12].FindControl("cbitem") as CheckBox).Checked)
            {
                DataRow dr = dtOrder.NewRow();
                dr["ProductID"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["ProductId"].ToString();
                dr["ProductName"] = gvr.Cells[1].Text;
                dr["Specification"] = gvr.Cells[2].Text;
                dr["itchscode"] = gvr.Cells[3].Text;
                dr["PriceID"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["PriceId"].ToString();
                dr["FOBPrice"] = gvr.Cells[5].Text;
                dr["USA_Sea"] = gvr.Cells[6].Text;
                dr["USA_Air"] = gvr.Cells[7].Text;
                dr["Europe_Sea"] = gvr.Cells[8].Text;
                dr["Europe_Air"] = gvr.Cells[9].Text;
                dr["Price"] = gvr.Cells[10].Text;
                dr["Quantity"] = (gvr.Cells[11].FindControl("txtQuantity") as TextBox).Text;
                dr["BuyerId"] = Session["BuyerId"].ToString();
                dr["PriceHistoryId"] = gvProductPricDetails.DataKeys[gvr.RowIndex].Values["PriceHistoryId"].ToString();
                int output = 0;
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
                dr["TotalPrices"] = Convert.ToDecimal(dr["Price"].ToString()) * Convert.ToDecimal(dr["Quantity"].ToString());
                dr["money"] = 1;
                dtOrder.Rows.Add(dr);
            }
        }
        Session["sDtOrder"] = dtOrder;
    }
    private void BindgvProductPricDetails(string name)
    {
        string[] value = name.Split('-');
        DataTable productlist = new DataTable();
        if (value.Length > 1)
        {
            productlist = objProduct.GetProductPricebyName(value[1].Trim());
        }
        else
        {
            productlist = objProduct.GetProductPricebyName(name);
        }
        gvProductPricDetails.DataSource = productlist;
        gvProductPricDetails.DataBind();
    }
}
