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

public partial class FarmerReports_UnitInfo : System.Web.UI.Page
{
    Farmer_BL frmObj = new Farmer_BL();
    UnitInformation_BL unitObj = new UnitInformation_BL();
    Reports_BL reportObj = new Reports_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindIcsCodes();
            BindYears();
            BindProdYears();
        }
    }
    private void BindYears()
    {
        DataTable Seasond = cpObj.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlYear.DataTextField = "SeasonYear";
            ddlYear.DataValueField = "SeasonYear";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, MudarApp.AddListItem());
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    private void BindProdYears()
    {
        DataTable Seasond = cpObj.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlProdYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlProdYear.DataTextField = "SeasonYear";
            ddlProdYear.DataValueField = "SeasonYear";
            ddlProdYear.DataBind();
            ddlProdYear.Items.Insert(0, MudarApp.AddListItem());
            ddlProdYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    public void BindIcsCodes()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        DataTable dtvill = unitObj.FarmersVillageListByIcs(dtLoginDetails.Rows[0]["UserLoginID"].ToString());
        List<string> listofvalues = dtvill.AsEnumerable()
                           .Select(r => "'" + r.Field<string>("City_Village") + ";" + "'")
                           .ToList();
        string ICSVillage = string.Empty;
        if (listofvalues.Count > 0)
            ICSVillage = string.Join(",", listofvalues.ToArray());
        //DataTable dt = unitObj.UnitInformation();
        DataTable dt = unitObj.GetUnitInofBasedonICS(ICSVillage);
        chkICSList.DataTextField = "Ucode";
        chkICSList.DataValueField = "UnitId";
        chkICSList.DataSource = dt;
        chkICSList.DataBind();
        chkICSList.Items.Add("All");
        //chkICSList.Items.FindByText("All").Selected = true;

    }
    protected void chkICSList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = unitObj.UnitInformation();
        BindUnitINfoData(ddlYear.SelectedItem.Text);
    }
    private void BindUnitINfoData(string Year)
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        if (chkICSList.SelectedValue.ToString() != string.Empty)
        {

            DataTable dtUI = unitObj.GetDisitlationUnits(chkICSList.SelectedValue, dtLoginDetails.Rows[0]["UserLoginID"].ToString(),Year);
            if (dtUI.Rows.Count > 0)
            {
                divUnitDetails.Visible = true;
                gvUnitInfo.Visible = true;
                gvProduct.Visible = false;
                gvUnitInfo.DataSource = dtUI;
                gvUnitInfo.DataBind();
                btnBackprod.Visible = false;
                btnProdEx.Visible = false;
                btnBack.Visible = true;
                btnPF.Visible = true;
            }
            lblHerb.Text = dtUI.AsEnumerable().Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
            lblOil.Text = dtUI.AsEnumerable().Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();

        }
        else
        {
            divUnitDetails.Visible = false;
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! plz select Unit code !!!');</script>");
        }
    }
    protected void btnPF_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "UnitInfo" + "-" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvUnitInfo.GridLines = GridLines.Both;
        gvUnitInfo.HeaderStyle.Font.Bold = true;
        gvUnitInfo.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnUnitwise_Click(object sender, EventArgs e)
    {
        trIcslist.Visible = true;
        trYear.Visible = true;
        btnVisible.Visible = false;
    }
    protected void btnProductwise_Click(object sender, EventArgs e)
    {
        tdProdYear.Visible = true;
        btnVisible.Visible = false;
        divUnitDetails.Visible = false;
        //divProdwiseDetails.Visible = true;
        btnsubmit.Visible = false;
        BindProductWiseData(ddlProdYear.SelectedItem.Text);
    }
    private void BindProductWiseData(string Year)
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        DataTable dtUI = unitObj.GetDisitlationUnits("All", dtLoginDetails.Rows[0]["UserLoginID"].ToString(), Year);
        DataTable dtvill = unitObj.FarmersVillageListByIcs(dtLoginDetails.Rows[0]["UserLoginID"].ToString());
        List<string> listofvalues = dtvill.AsEnumerable()
                           .Select(r => "'" + r.Field<string>("City_Village") + ";" + "'")
                           .ToList();
        string ICSVillage = string.Empty;
        if (listofvalues.Count > 0)
            ICSVillage = string.Join(",", listofvalues.ToArray());

        DataTable dtUnit = unitObj.GetUnitInofBasedonICS(ICSVillage);

        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("Ucode", typeof(string));
        dtNew.Columns.Add("PPH", typeof(decimal));
        dtNew.Columns.Add("CMH", typeof(decimal));
        dtNew.Columns.Add("SPH", typeof(decimal));
        dtNew.Columns.Add("BOH", typeof(decimal));
        dtNew.Columns.Add("PPO", typeof(decimal));
        dtNew.Columns.Add("CMO", typeof(decimal));
        dtNew.Columns.Add("SPO", typeof(decimal));
        dtNew.Columns.Add("BOO", typeof(decimal));

        for (int i = 0; i < dtUnit.Rows.Count; i++)
        {
            divProdwiseDetails.Visible = false;
            DataRow drNew = dtNew.NewRow();
            drNew["Ucode"] = dtUnit.Rows[i]["ucode"].ToString();
            drNew["PPH"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 1).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
            drNew["PPO"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 1).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();
            drNew["CMH"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 2).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
            drNew["CMO"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 2).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();
            drNew["SPH"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 3).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
            drNew["SPO"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 3).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();
            drNew["BOH"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 4).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
            drNew["BOO"] = dtUI.AsEnumerable().Where(y => y.Field<int>("ProductId") == 4).Where(y => y.Field<string>("ucode") == dtUnit.Rows[i]["ucode"].ToString()).Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();
            dtNew.Rows.Add(drNew);
        }
        DataRow drNew0 = dtNew.NewRow();
        drNew0["Ucode"] = "Total";
        drNew0["PPH"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("PPH")).ToString();
        drNew0["PPO"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("PPO")).ToString();
        drNew0["CMH"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("CMH")).ToString();
        drNew0["CMO"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("CMO")).ToString();
        drNew0["SPH"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("SPH")).ToString();
        drNew0["SPO"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("SPO")).ToString();
        drNew0["BOH"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("BOH")).ToString();
        drNew0["BOO"] = dtNew.AsEnumerable().Sum(x => x.Field<decimal>("BOO")).ToString();
        dtNew.Rows.Add(drNew0);
        DataRow drNew1 = dtNew.NewRow();
        DataTable dt1 = unitObj.GetDataP1(dtLoginDetails.Rows[0]["UserLoginID"].ToString(), Year);
        DataTable dt2 = unitObj.GetDataP2(dtLoginDetails.Rows[0]["UserLoginID"].ToString(), Year);
        DataTable dt3 = unitObj.GetDataP3(dtLoginDetails.Rows[0]["UserLoginID"].ToString(), Year);
        drNew1["Ucode"] = "Collected by Vidhya Aromatics";
        drNew1["PPH"] = 0.0;
        if(dt1.Rows[0]["collected"].ToString() == string.Empty)
            drNew1["PPO"] = 0.0;
        else
            drNew1["PPO"] = dt1.Rows[0]["collected"].ToString();
        drNew1["CMH"] = 0.0;
        if (dt2.Rows[0]["collected"].ToString() == string.Empty)
            drNew1["CMO"] = 0.0;
        else
            drNew1["CMO"] = dt2.Rows[0]["collected"].ToString();
        drNew1["SPH"] = 0.0;
        if (dt3.Rows[0]["collected"].ToString() == string.Empty)
            drNew1["SPO"] = 0.0;
        else
            drNew1["SPO"] = dt3.Rows[0]["collected"].ToString();
        drNew1["BOH"] = 0.0;
        drNew1["BOO"] = 0.0;
        dtNew.Rows.Add(drNew1);
        DataRow drNew2 = dtNew.NewRow();
        drNew2["Ucode"] = "Oil available with ICS Farmers";
        drNew2["PPH"] = 0.0;
        if (dt1.Rows[0]["Avil"].ToString() == string.Empty)
           drNew2["PPO"]  = 0.0;
        else
            drNew2["PPO"] = dt1.Rows[0]["Avil"].ToString();
  
        drNew2["CMH"] = 0.0;
        if (dt2.Rows[0]["Avil"].ToString() == string.Empty)
            drNew2["CMO"] = 0.0;
        else
        drNew2["CMO"] = dt2.Rows[0]["Avil"].ToString();
        drNew2["SPH"] = 0.0;
        if (dt3.Rows[0]["Avil"].ToString() == string.Empty)
            drNew2["SPO"] = 0.0;
        else
             drNew2["SPO"] = dt3.Rows[0]["Avil"].ToString();
        drNew2["BOH"] = 0.0;
        drNew2["BOO"] = 0.0;
        dtNew.Rows.Add(drNew2);

        divUnitDetails.Visible = false;
        divProdwiseDetails.Visible = true;
        gvUnitInfo.Visible = false;
        gvProduct.Visible = true;
        

        gvProduct.DataSource = dtNew;
        gvProduct.DataBind();

        tdProdwise.Visible = false;
        tdProdwise1.Visible = false;

        //lblHerb.Text = dtUI.AsEnumerable().Sum(x => x.Field<decimal>("FirstHerbaga")).ToString();
        //lblOil.Text = dtUI.AsEnumerable().Sum(x => x.Field<decimal>("FirstProductQuantity")).ToString();

        btnBack.Visible = false;
        btnPF.Visible = false;
        btnProdEx.Visible = true;
        btnBackprod.Visible = true;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        btnVisible.Visible = true;
        divUnitDetails.Visible = false;
        trIcslist.Visible = false;
        btnProdEx.Visible = false;
        btnBackprod.Visible = false;
        trYear.Visible = false;
    }
    protected void btnProdEx_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Unit" + "-" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvProduct.GridLines = GridLines.Both;
        gvProduct.HeaderStyle.Font.Bold = true;
        gvProduct.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    protected void btnBackprod_Click(object sender, EventArgs e)
    {
        btnVisible.Visible = true;
        divUnitDetails.Visible = false;
        trIcslist.Visible = false;
        btnPF.Visible = false;
        btnBack.Visible = false;
        trYear.Visible = false;
        divProdwiseDetails.Visible = false;
        tdProdYear.Visible = false;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkICSList.SelectedItem.Selected = false;
        divUnitDetails.Visible = false;
        gvUnitInfo.Visible = false;
        gvProduct.Visible = false;
        btnBackprod.Visible = false;
        btnProdEx.Visible = false;
        btnBack.Visible = false;
        btnPF.Visible = false;
    }
    protected void ddlProdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductWiseData(ddlProdYear.SelectedItem.Text);
    }
}