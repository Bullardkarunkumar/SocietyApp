using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using MudarOrganic.BL;
using System.Web.Configuration;

public partial class Farmer_FarmerInspection : System.Web.UI.Page
{
    BranchsRolesEmployees_BL breObj = new BranchsRolesEmployees_BL();
    InspectionPlan_BL inspObj = new InspectionPlan_BL();
    Farmer_BL farmerObj = new Farmer_BL();
    public static string SortExpression_p = "FarmerCode";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnInternalInspection();
            BindEmployeeList();
            lblTodayDate.Text = txtFromDate.Text = txtToDate.Text = DateTime.Now.AddHours(12).AddMinutes(30).ToShortDateString();
            AllInspectionDetailsBindDetails();
        }
    }
    private void AllInspectionDetailsBindDetails()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
        DataTable dtIns = inspObj.GetInspectionPlan(ddlEmployeeList.SelectedValue, lblTodayDate.Text, lblTodayDate.Text,icsCode);
        if (dtIns.Rows.Count > 0)
        {
            Session["FarmerListDetails"] = null;
            Session["FarmerListDetails"] = dtIns;
            divResult.Visible = true;
            gvFarmerList.DataSource = (DataTable)Session["FarmerListDetails"];
            gvFarmerList.DataBind();
            SortingFarmerCode(SortExpression_p);
        }
        else
        {
            Dataclear();
        }
    }
    private void Dataclear()
    {
        divResult.Visible = true;
        gvFarmerList.Visible = false;
        lblHoliday.Visible = true;
        lblHoliday.Text = " !!! Today is Holiday Inspection Not Scheduled !!! ";
    }
    private void SortingFarmerCode(string SortExpression)
    {
        DataTable dt = (DataTable)Session["FarmerListDetails"];
        Session["FarmerListDetails"] = dt;
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        else
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + sortingDirection;
        gvFarmerList.DataSource = sortedView;
        gvFarmerList.DataBind();
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    protected void gvFarmerList_RowCommand(object sender, CommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "PlanDate" && e.CommandArgument.ToString() != "FarmerCode" && e.CommandArgument.ToString() != "FarmerName" && e.CommandArgument.ToString() != "FarmerVillage" && e.CommandArgument.ToString() != "Total_Area" && e.CommandArgument.ToString() != "VisitedDate" && e.CommandArgument.ToString() != "InspectorName")
            index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "Report":
                {

                    //DateTime dt = Convert.ToDateTime(gvFarmerList.DataKeys[index].Values[2].ToString());
                    Session["FarmerId"] = gvFarmerList.DataKeys[index].Value;
                    Session["InsDate"] = gvFarmerList.DataKeys[index].Values[2].ToString();
                    Session["InspectionID"] = gvFarmerList.DataKeys[index].Values[3].ToString();
                    Session["InsName"] = gvFarmerList.DataKeys[index].Values[4].ToString();
                    Response.Redirect("../FarmerReports/InternalInspectionReport.aspx");

                }
                break;
            case "Diary":
                {
                    //DateTime dt = Convert.ToDateTime(gvFarmerList.DataKeys[index].Values[2].ToString());
                    Session["FarmerId"] = gvFarmerList.DataKeys[index].Value;
                    Response.Redirect("../FarmerReports/FarmerDiary.aspx");
                }
                break;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        divFarmer.Visible = false;
        //AllInspectionDetailsBindDetails();
        BindgvFarmerList();
    }
    private void BindEmployeeList()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
        DataTable dtICSID = inspObj.GetICSID(icsCode);
        DataTable EmployeList = inspObj.GetEmployeeWithRolesICsid(WebConfigurationManager.AppSettings["BranchRole"].ToString(), dtICSID.Rows[0]["BranchId"].ToString());
        //DataTable EmployeList = breObj.GetEmployeeWithRoles("BRANCH");
        if (EmployeList.Rows.Count > 0)
        {
            ddlEmployeeList.DataSource = EmployeList;
            ddlEmployeeList.DataTextField = "EmployeeFristName";
            ddlEmployeeList.DataValueField = "EmployeeId";
            ddlEmployeeList.DataBind();
            ListItem item = new ListItem();
            item.Text = "ALL";
            item.Value = "ALL";
            ddlEmployeeList.Items.Insert(0, item);
        }
    }
    private void BindgvFarmerList()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
        if (ddlEmployeeList.SelectedIndex > -1)
        {
            DataTable dt = inspObj.GetInspectionPlan(ddlEmployeeList.SelectedValue, txtFromDate.Text, txtToDate.Text,icsCode);
            if (dt.Rows.Count > 0)
            {
                lbtnPrev.Visible = false;
                lbtnNext.Visible = false;
                divResult.Visible = true;
                btnBack.Visible = true;
                lblHoliday.Visible = false;
                gvFarmerList.Visible = true;
                Session["FarmerListDetails"] = null;
                Session["FarmerListDetails"] = dt;
                gvFarmerList.DataSource = (DataTable)Session["FarmerListDetails"];
                gvFarmerList.DataBind();
                SortingFarmerCode(SortExpression_p);
            }
            else
                Response.Write("<script>alert('No Data found')</script>");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divInspection.Visible = false;
        string[] code = txtSFarmerName.Text.Split('-');
        if (code.Length >= 2)
        {
            string Farmername = code[0].ToString();
            string Farmercode = code[1].ToString();
            string Farmerarea = code[2].ToString();
            BindInspectionDetails(Farmername, Farmercode, Farmerarea);
        }
        else
            Response.Write("<script>alert('!!!! plz select the farmer from genearted list above textbox !!!! ');</script>");
    }
    private void BindInspectionDetails(string Farmername, string Farmercode, string Farmerarea)
    {
        if (!string.IsNullOrEmpty(Farmername))
        {
            DataTable dtInspection = inspObj.GetInspectionBasedonFarmerName(Farmername);
            if (dtInspection.Rows.Count > 0)
            {
                gvFarmerList.DataSource = dtInspection;
                gvFarmerList.DataBind();
            }
        }
        else
            Response.Write("<script>alert('No Data found')</script>");
    }
    protected void rbtnOne_CheckedChanged(object sender, EventArgs e)
    {
        txtToDate.Enabled = false;
        txtToDate.Text = null;
    }
    protected void rbtnMore_CheckedChanged(object sender, EventArgs e)
    {
        txtToDate.Enabled = true;
    }
    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        if (ddlEmployeeList.SelectedIndex > -1)
        {
            DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
            string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
            DateTime PrevDate = Convert.ToDateTime(lblTodayDate.Text).AddDays(-1);
            DataTable dtPrev = inspObj.GetInspectionPlan(ddlEmployeeList.SelectedValue, PrevDate.ToShortDateString().ToString(), PrevDate.ToShortDateString().ToString(), icsCode);
            if (dtPrev.Rows.Count > 0)
            {
                divResult.Visible = true;
                gvFarmerList.Visible = true;
                gvFarmerList.DataSource = dtPrev;
                gvFarmerList.DataBind();
                lbtnPrev.ForeColor = System.Drawing.ColorTranslator.FromHtml("Orange");
                lbtnNext.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
                lblHoliday.Visible = false;
            }
            else
            {
                Dataclear();
            }
            lblTodayDate.Text = PrevDate.ToString();
        }
    }
    protected void lbtnNext_Click(object sender, EventArgs e)
    {
        if (ddlEmployeeList.SelectedIndex > -1)
        {
            DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
            string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
            DateTime NextDate = Convert.ToDateTime(lblTodayDate.Text).AddDays(1);
            DataTable dtNex = inspObj.GetInspectionPlan(ddlEmployeeList.SelectedValue, NextDate.ToShortDateString().ToString(), NextDate.ToShortDateString().ToString(),icsCode);
            if (dtNex.Rows.Count > 0)
            {
                divResult.Visible = true;
                gvFarmerList.Visible = true;
                gvFarmerList.DataSource = dtNex;
                gvFarmerList.DataBind();
                lblHoliday.Visible = false;
                lbtnNext.ForeColor = System.Drawing.ColorTranslator.FromHtml("Orange");
                lbtnPrev.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
            }
            else
            {
                Dataclear();
            }
            lblTodayDate.Text = NextDate.ToString();
        }
    }
    protected void btnSInspection_Click(object sender, EventArgs e)
    {
        divResult.Visible = false;
        trgvFarmerList.Visible = true;
        divInspection.Visible = true;
        btnBack.Visible = true;
        divFarmer.Visible = false;
        btnSFarmer.Visible = false;
        btnSInspection.Visible = false;
        btnSInspection.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnSInspection.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnSFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnSFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnSFarmer_Click(object sender, EventArgs e)
    {
        divFarmer.Visible = true;
        btnBack.Visible = true;
        divInspection.Visible = false;
        btnSFarmer.Visible = false;
        btnSInspection.Visible = false;
        btnSFarmer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnSFarmer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnSInspection.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnSInspection.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmerInspection.aspx");
    }
    protected void gvFarmerList_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_p = e.SortExpression.ToString();
        SortingFarmerCode(SortExpression_p);
    }

    protected void gvFarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[9].Text.ToUpper().Trim() == "TRUE")
            {
                e.Row.Cells[9].Text = "Submit";
                e.Row.Cells[7].ForeColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
            }
        }
    }
}
