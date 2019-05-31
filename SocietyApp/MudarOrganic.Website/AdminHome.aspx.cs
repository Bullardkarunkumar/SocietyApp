using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;

public partial class AdminHome : System.Web.UI.Page
{
    Order_BL objOrder = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var orders = objOrder.OrderList("ALL", MudarLogin.GetBranchId());
            lblNew.Text = orders.Select("OrderStatus='NEW'").Length.ToString();
            lblOther.Text = orders.Select("bOrderStatus<>'DISPATCH' AND bOrderStatus<>'NEW'").Length.ToString();
            lblBranchOrderDispatch.Text = orders.Select("bOrderStatus='DISPATCH'").Length.ToString();
            BindOrdersGrid(orders, "order", rptFiveOrders);
            BindOrdersGrid(orders, "LotSample", rptFiveLotSamples);
        }
    }

    private void BindOrdersGrid(DataTable orders, string type, Repeater dataControl)
    {
        var drows = orders.Rows.Cast<DataRow>().OrderByDescending(itm => itm["OrderDate"]).Where(itm => itm["OrderType"].ToString() == type).Take(5);
        if (drows.Count() > 0)
        {
            var result = drows.CopyToDataTable();
            dataControl.DataSource = result;
            dataControl.DataBind();
        }
    }


    protected void rptFiveOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Session["sOrderID"] = Encrypt_Decrypt.Encrypt(e.CommandArgument.ToString(), true);
        Response.Redirect("~/Orders/BranchOrder.aspx");
    }

    protected void rptFiveLotSamples_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Session["sOrderID"] = Encrypt_Decrypt.Encrypt(e.CommandArgument.ToString(), true);
        Response.Redirect("~/Orders/BranchOrder.aspx");
    }
}
