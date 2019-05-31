using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.Drawing;
using System.IO;
using MudarOrganic.Components;

public partial class BranchReports_PackingRegister : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    Settings_BL settObj = new Settings_BL();
    Reports_BL reportObj = new Reports_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindYears();
            Bindproducts();
        }
    }
    private void BindYears()
    {
        DataTable Seasond = cpObj.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlSeasonYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlSeasonYear.DataTextField = "SeasonYear";
            ddlSeasonYear.DataValueField = "SeasonYear";
            ddlSeasonYear.DataBind();
            ddlSeasonYear.Items.Insert(0, MudarApp.AddListItem());
            ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    private void Bindproducts()
    {
        DataTable dt = reportObj.GetAllProducDetails();
        ddlProduct.DataSource = dt;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductId";
        ddlProduct.DataBind();
        //ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        ddlProduct.Items.Insert(0, "All");
    }
    private void GetBlendDetails()
    {
        DataTable dt = reportObj.GePackingDetails(ddlProduct.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            gvBlendreg.DataSource = dt;
            gvBlendreg.DataBind();
            decimal farmerCollTotal = dt.AsEnumerable().Sum(m => m.Field<decimal>("BQty"));
            lblColleted.Text = farmerCollTotal.ToString();
            divBack.Visible = true;
        }
    }
    protected void btncollReg_Click(object sender, EventArgs e)
    {
        GetBlendDetails();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(ddlProduct.SelectedValue);
    }
    protected void ddlSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData("All");
    }
    private void BindData(string ProductID)
    {
        DataTable dt = reportObj.GePackingDetails(ProductID);
        if (dt.Rows.Count > 0)
        {
            gvBlendreg.DataSource = dt;
            gvBlendreg.DataBind();
            decimal farmerCollTotal = dt.AsEnumerable().Sum(m => m.Field<decimal>("BQty"));
            lblColleted.Text = farmerCollTotal.ToString();
            divBack.Visible = true;
            trId.Visible = true;
        }
    }
    protected void btnPF_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "ReceptionRegister" + "-" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvBlendreg.GridLines = GridLines.Both;
        gvBlendreg.HeaderStyle.Font.Bold = true;
        gvBlendreg.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
   
}