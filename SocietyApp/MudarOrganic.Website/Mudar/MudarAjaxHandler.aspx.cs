using MudarOrganic.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mudar_MudarAjaxHandler : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] == "assignbranch")
        {
            var branchId = Request.QueryString["bid"];
            var orderId = Request.QueryString["oid"];
            try
            {
                orderObj.UpdateOrderBranch(branchId, Convert.ToInt32(orderId));
                Response.Write("1");
            }
            catch (Exception ex)
            {
                Response.Write("0");
            }
            Response.End();

        }
    }
}