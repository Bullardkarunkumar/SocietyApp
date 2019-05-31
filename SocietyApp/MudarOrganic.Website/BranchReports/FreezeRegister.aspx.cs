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

public partial class BranchReports_FreezeRegister : System.Web.UI.Page
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
    protected void ddlSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    private void BindData()
    {
        int Crystaltot=0;
        int DMOtot=0;
        DataTable dt = reportObj.GetFreezeDetails();
        if (dt.Rows.Count > 0)
        {

            decimal Qunta = dt.AsEnumerable().Sum(m => m.Field<int>("Qty"));
            lblLotQty.Text = Qunta.ToString()+".00";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Crystaltot = Crystaltot + Convert.ToInt32(dt.Rows[i]["CrystalReceived"].ToString());
                DMOtot = DMOtot + Convert.ToInt32(dt.Rows[i]["FreezeQuantity"].ToString());
                dt.Rows[i]["CrystalReceived"] = dt.Rows[i]["CrystalReceived"].ToString() + ".00";
                dt.Rows[i]["FreezeQuantity"] = dt.Rows[i]["FreezeQuantity"].ToString() + ".00";
            }
            lblCrystal.Text = Crystaltot.ToString()+".00";
            
            lblDMO.Text = DMOtot.ToString()+".00";

            gvBlendreg.DataSource = dt;
            gvBlendreg.DataBind();
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