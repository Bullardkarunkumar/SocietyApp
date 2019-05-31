using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;

public partial class Admin_TrancationsPage : System.Web.UI.Page
{
    Order_BL order = new Order_BL();
    string TodayDate = DateTime.Now.ToShortDateString();
    protected void Page_Load(object sender, EventArgs e)
    {
        string todaydate = DateTime.Now.ToShortDateString();
        if (!Page.IsPostBack)
        {
            BindTodayOrdersData();
            //BindTodayBranchOrdersData();
        }
    }
    public void BindTodayOrdersData()
    {
        DataTable dt = new DataTable();
        dt = order.GetTransactionsBuyerOrderDetails(TodayDate);
        if (dt.Rows.Count > 0)
        {
            gvTransactions.DataSource = dt;
            gvTransactions.DataBind();
        }
    }
    public void BindTodayBranchOrdersData()
    {
        DataTable dtBranch =  new DataTable();
        dtBranch = order.GetTransactionsBranchOrderDetails(TodayDate);
        if (dtBranch.Rows.Count > 0)
        {
            gvTransactions.DataSource = dtBranch;
            gvTransactions.DataBind();
        }
    }
    protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtnew =  new DataTable();
        if (e.CommandName == "cmd_view")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string date = gvTransactions.DataKeys[index].Value.ToString();
            dtnew = order.GetTransactionsBasedOnDate(date);
            if (dtnew.Rows.Count > 0)
            {
                divDetailsView.Visible = true;
                gvDetaildList.DataSource = dtnew;
                gvDetaildList.DataBind();
            }
        }
    }
}

