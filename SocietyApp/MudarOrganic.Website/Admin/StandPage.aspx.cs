using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MudarOrganic.BL;

public partial class Admin_StandPage : System.Web.UI.Page
{
    Settings_BL set = new Settings_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnStandInfo();
            BindYears();
            gvStandDetails.Visible = false;
            BindProductDetails();
            BindStandetailsList();
            BindMentholPerDetails();
        }
    }
    protected void btnSupplierPlaceorder_Click(object sender, EventArgs e)
    {
        bool result;
        if (!string.IsNullOrEmpty(lblStandID.Text))
        {
            result = set.StandDetails_INSandUPDandDEL(Convert.ToInt32(lblStandID.Text), Convert.ToInt32(ddlSeasonYear.Text), Convert.ToInt32(ddlProduct.Text), Convert.ToDateTime(txtPlantationFDate.Text), "", "bhanu", 2);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! Update Data Succefully !!!')", true);
        }
        else
        {
            result = set.StandDetails_INSandUPDandDEL(0, Convert.ToInt32(ddlSeasonYear.Text), Convert.ToInt32(ddlProduct.Text), Convert.ToDateTime(txtPlantationFDate.Text), "bhanu", "", 1);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! Saved Data Succefully !!!')", true);
        }
        txtPlantationFDate.Text = string.Empty;
        BindStandetailsList();
        divForm.Visible = false;
        divDetails.Visible = true;
        //divYear.Visible = true;
    }
    private void BindYears()
    {
        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlSeasonYear.Items.Add(item);
        ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();

        ListItemCollection items1 = MudarApp.BindYear();
        foreach (ListItem item11 in items1)
            ddlMyer.Items.Add(item11);
        ddlMyer.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void BindProductDetails()
    {
        ddlProduct.DataSource = set.GetProductDetails();
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductId";
        ddlProduct.DataBind();
        ddlProduct.Items.Insert(0, MudarApp.AddListItemWithDefaultValue());

        ddlMproduct.DataSource = set.GetProductDetails();
        ddlMproduct.DataTextField = "ProductName";
        ddlMproduct.DataValueField = "ProductId";
        ddlMproduct.DataBind();
        ddlMproduct.Items.Insert(0, MudarApp.AddListItemWithDefaultValue());
    }
    #region Production
    protected void gvStandDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "cmd_edit")
        {
            int StandID = Convert.ToInt32(gvStandDetails.DataKeys[index].Value.ToString());
            DataTable dsHolidayslist = set.GetStandDetails(StandID);
            lblStandID.Text = StandID.ToString();
            if (dsHolidayslist.Rows.Count > 0)
            {
                //divYear.Visible = false;
                divDetails.Visible = false;
                divForm.Visible = true;
                ddlSeasonYear.Text = dsHolidayslist.Rows[0]["Year"].ToString();
                DateTime Fdate = Convert.ToDateTime(dsHolidayslist.Rows[0]["Date"].ToString());
                txtPlantationFDate.Text = Fdate.ToShortDateString();
                ddlProduct.Text = dsHolidayslist.Rows[0]["ProductID"].ToString();
            }
        }
    }
    private void BindStandetailsList()
    {
        DataTable dsHolidayslist = set.GetStandDetails();
        if (dsHolidayslist.Rows.Count > 0)
        {
            gvStandDetails.Visible = true;
            gvStandDetails.DataSource = dsHolidayslist;
            gvStandDetails.DataBind();
        }
        else
        {
            gvStandDetails.Visible = false;
        }
    }
    protected void btnAddSDate_Click(object sender, EventArgs e)
    {
        divForm.Visible = true;
        divDetails.Visible = false;
        //divYear.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "plugins", "fnBindPlugins()", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/StandPage.aspx");
    }
    protected void btnTProductionYear_Click(object sender, EventArgs e)
    {
        //divProductionDetails.Visible = true;
        //divMenthol.Visible = false;
        //btnTProductionYear.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTProductionYear.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //btnTMenthol.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnTMenthol.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        htnSelectedTab.Value = "1";
    }
    #endregion

    protected void btnTMenthol_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "2";
        //divMenthol.Visible = true;
        //divProductionDetails.Visible = false;
        //btnTMenthol.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTMenthol.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //btnTProductionYear.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnTProductionYear.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void AddPercen_Click(object sender, EventArgs e)
    {
        divMForm.Visible = true;
        divMDetails.Visible = false;
        //divMPer.Visible = false;
    }
    private void BindMentholPerDetails()
    {
        DataTable dtMenthol = set.GetMentholPerDetails();
        if (dtMenthol.Rows.Count > 0)
        {
            gvMperdetails.Visible = true;
            gvMperdetails.DataSource = dtMenthol;
            gvMperdetails.DataBind();
        }
        else
        {
            gvMperdetails.Visible = false;
        }
    }
    protected void gvMperdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "cmd_edit")
        {
            int PerID = Convert.ToInt32(gvMperdetails.DataKeys[index].Value.ToString());
            DataTable dtMenthol = set.GetMentholPerDetails(PerID.ToString());
            lblPerID.Text = PerID.ToString();
            if (dtMenthol.Rows.Count > 0)
            {
                divMForm.Visible = true;
                divMDetails.Visible = false;
                //divMPer.Visible = false;
                ddlMyer.Text = dtMenthol.Rows[0]["SeasonYear"].ToString();
                txtPer.Text = dtMenthol.Rows[0]["percentage"].ToString();
                ddlMproduct.Text = dtMenthol.Rows[0]["ProductID"].ToString();
            }
        }
    }
    protected void btnMsubmit_Click(object sender, EventArgs e)
    {
        bool result;
        if (!string.IsNullOrEmpty(lblPerID.Text))
        {
            result = set.MentholPercentageDetailsINS(Convert.ToInt32(lblPerID.Text), Convert.ToInt32(ddlMyer.Text), Convert.ToInt32(ddlMproduct.Text), Convert.ToDecimal(txtPer.Text), "", "bhanu", 2);
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Update Data Succefully !!!');</script>");
        }
        else
        {
            result = set.MentholPercentageDetailsINS(0, Convert.ToInt32(ddlMyer.Text), Convert.ToInt32(ddlMproduct.Text), Convert.ToDecimal(txtPer.Text), "", "bhanu", 1);
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Saved Data Succefully !!!');</script>");
        }
        txtPlantationFDate.Text = string.Empty;
        BindMentholPerDetails();
        divMForm.Visible = false;
        divMDetails.Visible = true;
        //divMPer.Visible = true;
    }
    protected void btnMback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/StandPage.aspx");
    }

    protected override void OnPreRender(EventArgs e)
    {
        SetCurrentTab();
    }

    private void SetCurrentTab()
    {
        switch (htnSelectedTab.Value)
        {
            case "1":
                tabpanproductionyear.Attributes.Add("class", "tab-pane active");
                tabpaneTMenthol.Attributes.Add("class", "tab-pane");
                liCategory.Attributes.Add("class", "active");
                liProducts.Attributes.Remove("class");
                break;
            case "2":
                tabpaneTMenthol.Attributes.Add("class", "tab-pane active");
                tabpanproductionyear.Attributes.Add("class", "tab-pane");
                liProducts.Attributes.Add("class", "active");
                liCategory.Attributes.Remove("class");
                break;
        }
    }
}
