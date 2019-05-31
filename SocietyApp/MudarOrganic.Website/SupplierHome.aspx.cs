using MudarOrganic.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SupplierHome : System.Web.UI.Page
{
    Order_BL BranchOrderObj = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
            var orders = BranchOrderObj.BranchOrderList("ALL", 1, Convert.ToString(dtLoginDetails.Rows[0]["UserId"]));             
            lblNew.Text = orders.Select("OrderStatus='NEW'").Length.ToString();
            lblOther.Text = orders.Select("OrderStatus<>'SAMPLE DISPATCH' AND OrderStatus<>'NEW'").Length.ToString();
            lblBranchOrderDispatch.Text = orders.Select("OrderStatus='SAMPLE DISPATCH'").Length.ToString();
            BindOrdersGrid(orders, "order", rptFiveOrders);
            BindOrdersGrid(orders, "LotSample", rptFiveLotSamples);
        }
    }

    private void BindOrdersGrid(DataTable orders, string type, Repeater dataControl)
    {
        var drows = orders.Rows.Cast<DataRow>().OrderByDescending(itm => itm["BranchOrderDate"]).Where(itm => itm["BOrderType"].ToString() == type).Take(5);
        if (drows.Count() > 0)
        {
            var result = drows.CopyToDataTable();
            dataControl.DataSource = result;
            dataControl.DataBind();
        }
    }
}