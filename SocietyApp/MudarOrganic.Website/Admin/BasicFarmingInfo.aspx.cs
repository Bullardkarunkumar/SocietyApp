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
using System.Data.OleDb;
using System.IO;
using System.Text;
using MudarOrganic.DL;
using MudarOrganic.BL;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_BasicFarmingInfo : System.Web.UI.Page
{

    Farmer_BL farmerObj = new Farmer_BL();
    FarmPlantation_BL fp = new FarmPlantation_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    Product_BL productObj = new Product_BL();
    Farming_BL farmingObj = new Farming_BL();
    MudarApp objMudarApp = new MudarApp();
    public static int productID = 0;
    public static int seasonID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnFarmingInfo();
            BindYears();
            BindSeasonYear();
            ddlYear_SelectedIndexChanged(sender, e);
            //BindBasicFarmingInfo(DateTime.Now.Year);
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
            ddlSeason.DataTextField = "SeasonName";
            ddlSeason.DataValueField = "SeasonID";
            ddlSeason.DataBind();
            ddlSeason.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlSeason.Items.Clear();
        }
    }
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductList();
    }
    protected void btnFarmerInfosumbit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = false;
            int FarmingInfoID = 0;
            FarmingInfoID = Convert.ToInt32(hfBFID.Value);
            //DateTime FDate = System.DateTime.Parse(txtPlantationFDate.Text);
            //DateTime TDate = System.DateTime.Parse(txtPlantationTDate.Text);
            bool resutl = false;
            if (FarmingInfoID > 0)
            {
                result = farmingObj.FarmingInfo_INSandUPDandDEL(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToDateTime(txtPlantationFDate.Text), Convert.ToDateTime(txtPlantationTDate.Text), Convert.ToInt32(txt1stCutF.Text), Convert.ToInt32(txt1stCutT.Text), Convert.ToDecimal(txt1stHCF.Text), Convert.ToDecimal(txt1stHCT.Text), Convert.ToDecimal(txt1stOilF.Text), Convert.ToDecimal(txt1stOilT.Text), Convert.ToInt32(txt2ndCutF.Text), Convert.ToInt32(txt2ndCutT.Text), Convert.ToDecimal(txt2ndHCF.Text), Convert.ToDecimal(txt2ndHCT.Text), Convert.ToDecimal(txt2ndOilF.Text), Convert.ToDecimal(txt2ndOilT.Text), Convert.ToInt32(txtActualF.Text), Convert.ToInt32(txtActualTo.Text), "", "Aslam", MudarApp.Update, FarmingInfoID);
                if (result)
                {
                   
                    DataTable dt = new DataTable();
                    dt = farmingObj.GetProductionInfo(Convert.ToInt32(ddlProduct.SelectedValue),Convert.ToInt32(ddlSeason.SelectedValue));
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DateTime PlantationDate = objMudarApp.GenerateRandomDate(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString(), dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
                            int FHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutTo"].ToString()));
                            int SHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutTo"].ToString()));
                            // First Cut Estimation Herbage Qty
                            int FEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCTo"].ToString()));
                            // Total Herbage firstcut (estimated)
                            decimal FHerQty = Math.Round((Convert.ToDecimal(dt.Rows[i]["PlantationArea"].ToString()) * FEHQty), 1);
                            // firstcut oil Estimation kgs
                            decimal FEoil = MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilTo"].ToString()));
                            // Total Oil firstcut(estimated)
                            decimal FOilEsti = Math.Round((FHerQty * FEoil), 1);
                            //  firstcut Estimation vs Actual Percentage 
                            decimal FEsvsAt = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                            // firstcut Actual Herbage 
                            decimal FTHqty = Math.Round((FHerQty * FEsvsAt), 1);
                            // firstcut oil kgs
                            decimal FTOil = Math.Round((FOilEsti * FEsvsAt), 1);
                            // Second Cut Estimation Herbage Qty
                            int SEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCTo"].ToString()));
                            // Total Herbage second cut (estimated)
                            decimal SHerQty = Math.Round((Convert.ToDecimal(dt.Rows[i]["PlantationArea"].ToString()) * SEHQty), 1);
                            // Second Cut oil Estimation kgs
                            decimal SEoil = MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilTo"].ToString()));
                            // Second Cut Total Oil (estimated) 
                            decimal SOilEsti = Math.Round((SHerQty * SEoil), 1);
                            // Second Cut Estimation vs Actual  Percentage
                            decimal SEsvsAt = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                            // Second Cut Actual Herbage Percentage
                            decimal STHqty = Math.Round((SHerQty * SEsvsAt), 1);
                            // Second Cut oil kgs
                            decimal STOil = Math.Round((SOilEsti * SEsvsAt), 1);
                            resutl = fp.sp_FarmerProduction_UPD(PlantationDate, PlantationDate.AddDays(FHCount), FTHqty, PlantationDate.AddDays(FHCount + 1), FTOil, PlantationDate.AddDays(FHCount + SHCount), SHerQty,
                                       PlantationDate.AddDays(FHCount + SHCount + 1), STOil, FTOil + STOil, Convert.ToInt32(dt.Rows[i]["PlantationId"].ToString()), FHerQty, FOilEsti, SHerQty, SOilEsti);
                        }
                        if (resutl)
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Update Data Successfully !!!');</script>");
                        }
                        
                    }
                }
            }
            else
            {
                result = farmingObj.FarmingInfo_INSandUPDandDEL(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToDateTime(txtPlantationFDate.Text), Convert.ToDateTime(txtPlantationTDate.Text), Convert.ToInt32(txt1stCutF.Text), Convert.ToInt32(txt1stCutT.Text), Convert.ToDecimal(txt1stHCF.Text), Convert.ToDecimal(txt1stHCT.Text), Convert.ToDecimal(txt1stOilF.Text), Convert.ToDecimal(txt1stOilT.Text), Convert.ToInt32(txt2ndCutF.Text), Convert.ToInt32(txt2ndCutT.Text), Convert.ToDecimal(txt2ndHCF.Text), Convert.ToDecimal(txt2ndHCT.Text), Convert.ToDecimal(txt2ndOilF.Text), Convert.ToDecimal(txt2ndOilT.Text), Convert.ToInt32(txtActualF.Text), Convert.ToInt32(txtActualTo.Text), "Aslam", "", MudarApp.Insert, 0);
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Saved Data Successfully !!!');</script>");
            }
            divBasicForm.Visible = false;
            btnAddBasic.Visible = false;
            divAllSeasonNames.Visible = true;
            btnAddBasic.Visible = true;
            ClearControls();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void BindYears()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlYear.Items.Add(item);

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
    private void BindProductList()
    {
        if (ddlSeason.SelectedIndex > 0)
        {
            ddlProduct.DataSource = productObj.GetProductDetailsbySeasonNew(Convert.ToInt32(ddlSeason.SelectedValue));
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlProduct.Items.Clear();
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
            return;
        }
    }
    private void BindBasicFarmingonID(int FarminginfoID)
    {
        try
        {
            DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfoBasedonID(FarminginfoID);
            if (dtBasicFInfo.Rows.Count > 0)
            {
                DataRow dr = dtBasicFInfo.Rows[0];
                divBasicForm.Visible = true;
                hfBFID.Value = dr["FarmingInfoID"].ToString();
                ddlYear.ClearSelection();
                ddlYear.Items.FindByText(dr["Year"].ToString()).Selected = true;
                // bind the details for season
                ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
                ddlSeason.DataTextField = "SeasonName";
                ddlSeason.DataValueField = "SeasonID";
                ddlSeason.DataBind();
                //if(ddlSeason.Items.FindByText(dr["SeasonID"].ToString()).Selected)
                ddlSeason.Items.FindByValue(dr["SeasonID"].ToString()).Selected = true;
                // bind the details for product
                //ddlProduct.DataSource = productObj.GetProductDetailsbySeason(Convert.ToInt32(dr["SeasonID"].ToString()));
                ddlProduct.DataSource = productObj.GetProductDetailsbySeasonNew(Convert.ToInt32(ddlSeason.SelectedValue));
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();
                ddlProduct.Items.FindByValue(dr["ProductID"].ToString()).Selected = true;
                DateTime fdate =Convert.ToDateTime(dr["PlantationFrom"].ToString());
                txtPlantationFDate.Text = fdate.ToShortDateString();
                DateTime tdate = Convert.ToDateTime(dr["PlantationTo"].ToString());
                txtPlantationTDate.Text = tdate.ToShortDateString();
                txt1stCutF.Text = dr["1stCutFrom"].ToString();
                txt1stCutT.Text = dr["1stCutTo"].ToString();
                txt1stHCF.Text = dr["Qty1stCutHCFrom"].ToString();
                txt1stHCT.Text = dr["Qty1stCutHCTo"].ToString();
                txt1stOilF.Text = dr["1stRecoveryOilFrom"].ToString();
                txt1stOilT.Text = dr["1stRecoveryOilTo"].ToString();
                txt2ndCutF.Text = dr["2ndCutFrom"].ToString();
                txt2ndCutT.Text = dr["2ndCutTo"].ToString();
                txt2ndHCF.Text = dr["Qty2ndCutHCFrom"].ToString();
                txt2ndHCT.Text = dr["Qty2ndCutHCTo"].ToString();
                txt2ndOilF.Text = dr["2ndRecoveryOilFrom"].ToString();
                txt2ndOilT.Text = dr["2ndRecoveryOilTo"].ToString();
                txtActualF.Text = dr["EsvsAc_From"].ToString();
                txtActualTo.Text = dr["EsvsAc_To"].ToString();

            }
            else
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
                
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! wrong info provided !!!');</script>");
            return;
        }
    }
    private void BindBasicFarmingInfo()
    { 
        DataTable dtBasicFInfo =  farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue));
        if (dtBasicFInfo.Rows.Count > 0)
        {
            hfBFID.Value =dtBasicFInfo.Rows[0]["FarmingInfoID"].ToString();
            DateTime fdate = Convert.ToDateTime(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString());
            txtPlantationFDate.Text = fdate.ToShortDateString();
            DateTime tdate = Convert.ToDateTime(dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
            txtPlantationTDate.Text = tdate.ToShortDateString();
            //txtPlantationFDate.Text = dtBasicFInfo.Rows[0]["PlantationFrom"].ToString();
            //txtPlantationFDate.Text = System.DateTime.Now.ToShortDateString();
            //txtPlantationTDate.Text = dtBasicFInfo.Rows[0]["PlantationTo"].ToString();
            //txtPlantationTDate.Text = System.DateTime.Now.ToShortDateString();
            txt1stCutF.Text = dtBasicFInfo.Rows[0]["1stCutFrom"].ToString();
            txt1stCutT.Text = dtBasicFInfo.Rows[0]["1stCutTo"].ToString();
            txt1stHCF.Text = dtBasicFInfo.Rows[0]["Qty1stCutHCFrom"].ToString();
            txt1stHCT.Text = dtBasicFInfo.Rows[0]["Qty1stCutHCTo"].ToString();
            txt1stOilF.Text = dtBasicFInfo.Rows[0]["1stRecoveryOilFrom"].ToString();
            txt1stOilT.Text = dtBasicFInfo.Rows[0]["1stRecoveryOilTo"].ToString();
            txt2ndCutF.Text = dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString();
            txt2ndCutT.Text = dtBasicFInfo.Rows[0]["2ndCutTo"].ToString();
            txt2ndHCF.Text = dtBasicFInfo.Rows[0]["Qty2ndCutHCFrom"].ToString();
            txt2ndHCT.Text = dtBasicFInfo.Rows[0]["Qty2ndCutHCTo"].ToString();
            txt2ndOilF.Text = dtBasicFInfo.Rows[0]["2ndRecoveryOilFrom"].ToString();
            txt2ndOilT.Text = dtBasicFInfo.Rows[0]["2ndRecoveryOilTo"].ToString();
            txtActualF.Text = dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString();
            txtActualTo.Text = dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString();
        }
        else
        {
            hfBFID.Value = "0";
            txtPlantationFDate.Text = System.DateTime.Now.ToShortDateString();
            txtPlantationTDate.Text = System.DateTime.Now.ToShortDateString();
            txt1stCutF.Text = "0";
            txt1stCutT.Text = "0";
            txt1stHCF.Text = "0";
            txt1stHCT.Text = "0";
            txt1stOilF.Text = "0";
            txt1stOilT.Text = "0";
            txt2ndCutF.Text = "0";
            txt2ndCutT.Text = "0";
            txt2ndHCF.Text = "0";
            txt2ndHCT.Text = "0";
            txt2ndOilF.Text = "0";
            txt2ndOilT.Text = "0";
            txtActualF.Text = "0";
            txtActualTo.Text = "0";
        }
    }
    private void ClearControls()
    {
        ddlYear.Items.Clear();
        ddlSeason.Items.Clear();
        ddlProduct.Items.Clear();
        txtPlantationFDate.Text = System.DateTime.Now.ToShortDateString();
        txtPlantationTDate.Text = System.DateTime.Now.ToShortDateString();
        txt1stCutF.Text = "0";
        txt1stCutT.Text = "0";
        txt1stHCF.Text = "0";
        txt1stHCT.Text = "0";
        txt1stOilF.Text = "0";
        txt1stOilT.Text = "0";
        txt2ndCutF.Text = "0";
        txt2ndCutT.Text = "0";
        txt2ndHCF.Text = "0";
        txt2ndHCT.Text = "0";
        txt2ndOilF.Text = "0";
        txt2ndOilT.Text = "0";
        txtActualF.Text = "0";
        txtActualTo.Text ="0";
        BindYears();
        hfBFID.Value = string.Empty;
        btnTZaid.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTZaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTKharif.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTKharif.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTRabi.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTRabi.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTAnnual.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTAnnual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBasicFarmingInfo();
    }
    protected void btnFarmerInfoClear_Click(object sender, EventArgs e)
    {
        ClearControls();
        divBasicForm.Visible = false;
        btnAddBasic.Visible = true;
        divBasicFarmDetails.Visible = false;
        divAllSeasonNames.Visible = true;
    }
    protected void btnAddBasic_Click(object sender, EventArgs e)
    {
        divBasicForm.Visible = true;
        btnAddBasic.Visible = false;
        divAllSeasonNames.Visible = false;
        divBasicFarmDetails.Visible = false;
    }
    private void BindBasicFarmingInfo(int year)
    {
        DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(year);
        if (dtBasicFInfo.Rows.Count > 0)
        {
            gvBasicFarm.DataSource = dtBasicFInfo;
            gvBasicFarm.DataBind();
        }
    }
    protected void gvBasicFarm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            BindBasicFarmingonID(Convert.ToInt32(gvBasicFarm.DataKeys[index].Value.ToString()));
        }
    }
    private void BindSeasonYear()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlSeasonYear.Items.Add(item);
        ////for (int count = 0; count < Convert.ToInt32(WebConfigurationManager.AppSettings["SeasonYearCount"].ToString()); count++)
        ////    ddlSeasonYear.Items.Add((new ListItem()).Text = DateTime.Now.AddYears(count).Year.ToString());
        //ddlSeasonYear.DataBind();
        //ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();

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
    #region TabSeasonDetails
    private void BindBasicFarmingDetails(int year, int seasonID)
    {
        DataTable dt = farmingObj.GetBasicFarmingInfoBasedon(year, seasonID);
        if (dt.Rows.Count > 0)
        {
            if (seasonID == 1)
            {
                btnTZaid.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
                btnTZaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                btnTKharif.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTKharif.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                btnTRabi.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTRabi.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
            if (seasonID == 2)
            {
                btnTKharif.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
                btnTKharif.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                btnTZaid.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTZaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                btnTRabi.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTRabi.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
            if (seasonID == 3)
            {
                btnTRabi.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
                btnTRabi.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                btnTZaid.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTZaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                btnTKharif.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
                btnTKharif.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
            divBasicFarmDetails.Visible = true;
            gvBasicFarm.DataSource = dt;
            gvBasicFarm.DataBind();
        }
        else
        {
            divBasicFarmDetails.Visible = false;
            divBasicForm.Visible = false;
            btnAddBasic.Visible = true;
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
            ClearTabControls();
        }
    }
    private void ClearTabControls()
    {
        btnTZaid.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTZaid.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTKharif.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTKharif.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTRabi.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTRabi.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTAnnual.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTAnnual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnTZaid_Click(object sender, EventArgs e)
    {
        if (ddlSeasonYear.SelectedValue == "Select...")
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Plz select the SeasonYear !!!');</script>");
        else
        {
            btnAddBasic.Visible = true;
            DataTable dtSeasondt = cpObj.GetSeasonDetailsBasedonYear(ddlSeasonYear.SelectedValue);
            if (dtSeasondt.Rows.Count > 0)
            {
                int year = Convert.ToInt32(ddlSeasonYear.SelectedValue);
                int season = Convert.ToInt32(dtSeasondt.Rows[0][0].ToString());
                BindBasicFarmingDetails(year, season);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
                gvBasicFarm.DataSource = "";
                gvBasicFarm.DataBind();
                ClearTabControls();
            }
        }
    }
    protected void btnTKharif_Click(object sender, EventArgs e)
    {
        if (ddlSeasonYear.SelectedValue == "Select...")
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Plz select the SeasonYear !!!');</script>");
        else
        {
            btnAddBasic.Visible = true;
            DataTable dtSeasondt = cpObj.GetSeasonDetailsBasedonYear(ddlSeasonYear.SelectedValue);
            if (dtSeasondt.Rows.Count > 0)
            {
                int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                int season = Convert.ToInt32(dtSeasondt.Rows[1][0].ToString());
                BindBasicFarmingDetails(year, season);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
                gvBasicFarm.DataSource = "";
                gvBasicFarm.DataBind();
                ClearTabControls();
            }
        }
    }
    protected void btnTRabi_Click(object sender, EventArgs e)
    {
        if (ddlSeasonYear.SelectedValue == "Select...")
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Plz select the SeasonYear !!!');</script>");
        else
        {
            btnAddBasic.Visible = true;
            DataTable dtSeasondt = cpObj.GetSeasonDetailsBasedonYear(ddlSeasonYear.SelectedValue);
            if (dtSeasondt.Rows.Count > 0)
            {
                int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                int season = Convert.ToInt32(dtSeasondt.Rows[2][0].ToString());
                BindBasicFarmingDetails(year, season);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
                gvBasicFarm.DataSource = "";
                gvBasicFarm.DataBind();
                ClearTabControls();
            }
        }
    }
    protected void btnTAnnual_Click(object sender, EventArgs e)
    {
       
    } 
    #endregion
}
