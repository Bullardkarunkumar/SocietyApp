using MudarOrganic.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StockOrder : System.Web.UI.Page
{
    Order_BL objOrder = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var preOrderData = objOrder.GetPreOrderData();
            gvOrder.DataSource = preOrderData;
            gvOrder.DataBind();
        }
    }
}