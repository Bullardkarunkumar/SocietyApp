using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;
using System.Configuration;
using System.Data.OleDb;

public partial class Farmer_FarmerProductionInfo : System.Web.UI.Page
{
    public static string SortExpression_p = "FarmerCode";
    Farmer_BL farmerobj = new Farmer_BL();
    Farming_BL farmingObj = new Farming_BL();
    FarmPlantation_BL fp = new FarmPlantation_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    FarmPlantation_BL fpobj = new FarmPlantation_BL();
    Product_BL objProd = new Product_BL();
    MudarApp objMudarApp = new MudarApp();
    UnitInformation_BL UI = new UnitInformation_BL();
    MudarMaster mm = new MudarMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblTAvaiArea.Text = string.Empty;
            lblTcropArea.Text = string.Empty;
            lblTplotArea.Text = string.Empty;
            gvFarmerList.AllowSorting = true;
            BindSeasonYear();
            BindSeasonDetails();
            BindProductList();
            BindVillageslist();
            ddlUnitDetails.DataSource = fp.BindDropDownChild();
            ddlUnitDetails.DataTextField = "Name";
            ddlUnitDetails.DataValueField = "UnitId";
            ddlUnitDetails.DataBind();
            BindddlCultivation(1);
            //lnkImportFarmerProdInfo.Visible = true;
            //divSeasonDetails.Visible = true;
        }
        Master.MasterControlbtnFarmerPlantation();
    }
    private void BindSeasonYear()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlYear.Items.Add(item);
        //ddlYear.DataBind();
        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        //ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
        //ddlSeason.DataTextField = "SeasonYear";
        //ddlYear.DataValueField = "SeasonYear";
        //ddlYear.DataBind();

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
    private void BindSeasonDetails()
    {
        ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
        ddlSeason.DataTextField = "SeasonName";
        ddlSeason.DataValueField = "SeasonID";
        ddlSeason.DataBind();
        ddlSeason.Items.Insert(0, MudarApp.AddListItem());

    }
    private void BindVillageslist()
    {
        DataTable dtLoginDetails = Session["dtLoginDetails"] as DataTable;
        if (dtLoginDetails.Rows.Count>0)
        {
            ddlVillage.DataSource = UI.FarmersVillageListByIcs(Convert.ToString(dtLoginDetails.Rows[0]["UserLoginID"]));
        }
        else
        {
            ddlVillage.DataSource = UI.FarmersVillageList();
        }
        
        ddlVillage.DataTextField = "City_Village";
        ddlVillage.DataValueField = "City_Village";
        ddlVillage.DataBind();
        ddlVillage.Items.Insert(0, MudarApp.AddListItem());
        ddlVillage.Items.Add("All");
    }
    protected void gvFarmerList_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_p = e.SortExpression.ToString();
        SortingFarmerCode(SortExpression_p);
    }
    public string dir
    {
        get
        {
            if (ViewState["dirState"].ToString() == "desc")
            {
                ViewState["dirState"] = "asc";
            }
            else
            {
                ViewState["dirState"] = "desc";
            }
            return ViewState["dirState"].ToString();
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingFarmerCode(string SortExpression)
    {
        DataTable dt = (DataTable)Session["FarmerDetails"];
        Session["FarmerDetails"] = dt;
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvFarmerList.DataSource = sortedView;
        gvFarmerList.DataBind();
    }
    private void BindProductList()
    {
        if (ddlSeason.SelectedIndex > 0)
        {
            ddlProduct.DataSource = objProd.GetProductDetailsbySeasonNew(Convert.ToInt32(ddlSeason.SelectedValue));
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlProduct.Items.Clear();
        }
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProduct.SelectedIndex > 0)
        {
            ddlVillage.Enabled = true;
            BindVillageslist();
        }
        else
            ddlVillage.Enabled = false;
    }
    private void BindFarmerProductWiseDetails()
    {
        double TAvaiArea = 0.000;
        double TcropArea = 0.000;
        double TplotArea = 0.000;
        DataTable dtLoginDetails = Session["dtLoginDetails"] as DataTable;
        lnkImportFarmerProdInfo.Visible = true;
        DataTable dtFarmerDetails = farmerobj.GetFarmerProdcts(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), ddlVillage.SelectedItem.Text,Convert.ToString(dtLoginDetails.Rows[0]["UserLoginID"]));
        if (dtFarmerDetails.Rows.Count > 0)
        {
            lblSyear.Text = ddlYear.SelectedValue;
            lblSname.Text = ddlSeason.SelectedItem.Text;
            lblProduct.Text = ddlProduct.SelectedItem.Text;
            divfarmerlist.Visible = true;

            divSeasonDetails.Visible = true;
            trTotalplots.Visible = true;
            lnkImportFarmerProdInfo.Visible = true;
            divFindDetails.Visible = false;
            divGetDetails.Visible = false;
            Session["FarmerDetails"] = null;
            Session["FarmerDetails"] = dtFarmerDetails;
            for (int i = 0; i < dtFarmerDetails.Rows.Count; i++)
            {
                TAvaiArea = Convert.ToDouble(dtFarmerDetails.Rows[i]["Availablearea"].ToString()) + TAvaiArea;
                TcropArea = Convert.ToDouble(dtFarmerDetails.Rows[i]["croparea"].ToString()) + TcropArea;
                TplotArea = Convert.ToDouble(dtFarmerDetails.Rows[i]["plotarea"].ToString()) + TplotArea;
            }
            lblTAvaiArea.Text = TAvaiArea.ToString();
            lblTcropArea.Text = TcropArea.ToString();
            lblTplotArea.Text = Math.Round(TplotArea, 3).ToString();
            gvFarmerList.DataSource = (DataTable)Session["FarmerDetails"];
            gvFarmerList.DataBind();
            //SortingFarmerCode(SortExpression_p);
        }
        else
        {
            //lnkImportFarmerProdInfo.Visible = true;
            divSeasonDetails.Visible = true;
        }
    }
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductList();
    }
    protected void gvFarmerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "FarmerCode" && e.CommandArgument.ToString() != "FirstName" && e.CommandArgument.ToString() != "City_Village" && e.CommandArgument.ToString() != "plotarea" && e.CommandArgument.ToString() != "PlotCode" && e.CommandArgument.ToString() != "Organic" && e.CommandArgument.ToString() != "croparea" && e.CommandArgument.ToString() != "Availablearea")
            index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "Production":
                {

                    trTotalplots.Visible = false;
                    divfarmerlist.Visible = false;
                    divSeasonDetails.Visible = false;
                    lnkImportFarmerProdInfo.Visible = false;
                    hfFarmerID.Value = gvFarmerList.DataKeys[index].Values[0].ToString();
                    hfFarmerCode.Value = gvFarmerList.DataKeys[index].Values[1].ToString();
                    DataTable dtSelectFarmerDetails = farmerobj.FamerDetails(gvFarmerList.DataKeys[index].Values[1].ToString());
                    if (dtSelectFarmerDetails.Rows.Count > 0)
                    {
                        //Bindgvfarmer();
                        NewBindgvfarmer();
                        lblFarmerCode.Text = dtSelectFarmerDetails.Rows[0]["FarmerCode"].ToString();
                        lblFarmername.Text = dtSelectFarmerDetails.Rows[0]["FirstName"].ToString();
                        lblVillage.Text = dtSelectFarmerDetails.Rows[0]["City_Village"].ToString();
                    }
                }
                break;
            case "AddCrop":
                {
                    btnFarmerBack.Visible = true;
                    divFindDetails.Visible = true;
                    divfarmerlist.Visible = false;
                    trTotalplots.Visible = false;
                    lnkImportFarmerProdInfo.Visible = false;
                    hfFarmerID.Value = gvFarmerList.DataKeys[index].Values[0].ToString();
                    DataTable dtSelectFarmerDetails = farmerobj.FamerDetails(gvFarmerList.DataKeys[index].Values[1].ToString());
                    if (dtSelectFarmerDetails.Rows.Count > 0)
                    {
                        BindDlMainFP();
                        btnFarmerSave.Visible = true;
                        lblFarmerCode.Text = dtSelectFarmerDetails.Rows[0]["FarmerCode"].ToString();
                        lblFarmername.Text = dtSelectFarmerDetails.Rows[0]["FirstName"].ToString();
                        lblVillage.Text = dtSelectFarmerDetails.Rows[0]["City_Village"].ToString();
                    }
                }
                break;

        }
    }
    protected void gvFarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[7].Text.ToUpper().Trim() == "FALSE")
            {
                e.Row.Cells[7].Text = "Org & FT";
            }
            else
                e.Row.Cells[7].Text = "Organic";
        }
    }
    private void BindddlCultivation(int Case)
    {
        //if (Case == 1)
        //{
        //    ddlCultivation.Items.Add("ALL");
        //    ddlCultivation.Items.Add("I Cut");
        //    ddlCultivation.Items.Add("II Cut");
        //    ddlCultivation.DataBind();
        //    //ddlCultivation.Items[0].Selected = true;
        //}
    }

    #region Farmer BindPlot and AddCrop Details
    protected void dlMainFP_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        int index = e.Item.ItemIndex;
        switch (command)
        {
            case "addcrop":
                {
                    DataTable dtFarm = (DataTable)Session["dtFarm_s"];
                    DataRow drNew = dtFarm.NewRow();
                    drNew["FarmID"] = 0;
                    drNew["PlantationId"] = 0;
                    drNew["PlotArea"] = 0;
                    drNew["ParentFarmID"] = dlMainFP.DataKeys[index];
                    drNew["IsInterCrop"] = false.ToString();
                    dtFarm.Rows.Add(drNew);
                    foreach (DataListItem dli in dlMainFP.Items)
                    {
                        string farmid = dlMainFP.DataKeys[dli.ItemIndex].ToString();
                        DataTable dt = new DataTable();
                        dt = dtFarm.Clone();
                        DataRow[] drs = dtFarm.Select("ParentFarmID = " + farmid);
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        GridView gvCrop = (GridView)dli.FindControl("gvCrops");
                        gvCrop.DataSource = dt;
                        gvCrop.DataBind();
                        if (dt.Rows.Count >= 1)
                        {
                            (dlMainFP.Items[dli.ItemIndex].FindControl("btnAddFarm") as Button).Visible = false;
                            (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Visible = true;
                            (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Enabled = false;
                            (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
                        }
                    }
                }
                break;
            case "Edit":
                {
                    bool result;
                    DataTable dtunit = new DataTable();
                    DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue));
                    if (dtBasicFInfo.Rows.Count > 0)
                    {
                        for (int count = 0; count < dlMainFP.Items.Count; count++)
                        {
                            int PFarmID = Convert.ToInt32(dlMainFP.DataKeys[count].ToString());
                            GridView gv = (dlMainFP.Items[count].FindControl("gvCrops") as GridView);
                            if (gv.Rows.Count > 0)
                            {
                                decimal PTotalArea = Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblAvaliableArea") as Label).Text);
                                decimal TotalArea = 0;
                                decimal totalInterCropArea = 0;
                                for (int gvCount = 0; gvCount < gv.Rows.Count; gvCount++)
                                {
                                    DataKey dk = gv.DataKeys[gvCount];
                                    int FarmId = !string.IsNullOrEmpty(dk.Values["FarmId"].ToString()) ? Convert.ToInt32(dk.Values["FarmId"].ToString()) : 0;
                                    int PlantationId = !string.IsNullOrEmpty(dk.Values["PlantationId"].ToString()) ? Convert.ToInt32(dk.Values["PlantationId"].ToString()) : 0;
                                    string PA = (gv.Rows[gvCount].FindControl("txtPlotArea") as TextBox).Text;
                                    bool IsInterCrop = (gv.Rows[gvCount].FindControl("cbIsInterCrop") as CheckBox).Checked;
                                    decimal PlotArea = !string.IsNullOrEmpty(PA) ? Convert.ToDecimal(PA) : 0;
                                    if (!IsInterCrop)
                                        TotalArea += PlotArea;
                                    if (IsInterCrop)
                                        totalInterCropArea += PlotArea;
                                    DateTime PlantationDate = objMudarApp.GenerateRandomDate(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString(), dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
                                    int FHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutTo"].ToString()));
                                    int SHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutTo"].ToString()));

                                    // First Cut Estimation Herbage Qty
                                    int FEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCTo"].ToString()));
                                    // Total Herbage firstcut (estimated)
                                    decimal FHerQty = Math.Round((PlotArea * FEHQty), 1);
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
                                    decimal SHerQty = Math.Round((PlotArea * SEHQty), 1);
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
                                    dtunit = UI.UnitInformationBasedOnVillage(lblVillage.Text);
                                    string unitid = string.Empty;
                                    if (dtunit.Rows.Count > 0)
                                    {
                                        int getunitID = MudarApp.RandomNumber(1, dtunit.Rows.Count);
                                        if (dtunit.Rows.Count > 2)
                                            unitid = dtunit.Rows[getunitID]["UnitId"].ToString();
                                        else
                                            unitid = dtunit.Rows[0]["UnitId"].ToString();
                                    }
                                    else
                                    {
                                        unitid = new Guid().ToString();
                                    }
                                    bool resutl = false;
                                    if (!IsInterCrop)
                                    {
                                        if (PlotArea > 0)
                                        {
                                            resutl = fp.UpdatePlantationDetails(hfFarmerID.Value.ToString(), FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                            unitid, PlantationDate, PlantationDate.AddDays(FHCount), FTHqty, PlantationDate.AddDays(FHCount + 1), 0, FTOil, PlantationDate.AddDays(FHCount + SHCount), SHerQty,
                                            PlantationDate.AddDays(FHCount + SHCount + 1), 0, STOil, FTOil + STOil, true, true, "Aslam", string.Empty,
                                            Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                             Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                        }
                                        else
                                        {
                                            Response.Write("<script>alert('Crop Area should not exceed the total Avaliable Area !!!!!!!!');</script>");
                                        }
                                    }
                                    else
                                    {
                                        if (totalInterCropArea <= Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblPlotArea") as Label).Text) && totalInterCropArea > 0)
                                        {
                                            //if (FarmId == 0 && PlantationId == 0)
                                            {
                                                resutl = fp.UpdatePlantationDetails(hfFarmerID.Value.ToString(), FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                                unitid, PlantationDate, PlantationDate.AddDays(FHCount), 0, PlantationDate.AddDays(FHCount + 2), 0, 0, PlantationDate.AddDays(FHCount + SHCount), 0,
                                                PlantationDate.AddDays(FHCount + SHCount + 2), 0, 0, 0, false, false, "Aslam", string.Empty,
                                                Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                                 Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                            }
                                        }
                                        else
                                        {
                                            if (FarmId == 0 && PlantationId == 0)
                                                Response.Write("<script>alert(' Inter Crop Area should not exceed the total PlotArea !!!!!!!!');</script>");
                                        }
                                    }
                                }
                            }
                        }
                        if (ddlProduct.SelectedIndex > 0)
                        {
                            BindDlMainFP();
                            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Update Data Successfully');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('plz check the basicfarming information');</script>");
                        return;
                    }
                }
                break;
        }
    }
    protected void btnFarmerGo_Click(object sender, EventArgs e)
    {
        //if (ddlProduct.SelectedIndex > 0)
        //{
        //    BindDlMainFP();
        //}
    }
    private void BindDlMainFP()
    {
        DataTable dtFarmDetails = farmerobj.FarmDetails(hfFarmerID.Value.ToString(), true);
        if (dtFarmDetails.Rows.Count > 0)
        {
            divFarmerPlotDetails.Visible = true;
            dtFarmDetails.Columns.Add("AvaliablePlotArea", typeof(decimal));
            foreach (DataRow dr in dtFarmDetails.Rows)
                dr["AvaliablePlotArea"] = farmerobj.AvaliablePlotArea(hfFarmerID.Value.ToString(), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(dr["FarmID"].ToString()));
            dlMainFP.DataSource = dtFarmDetails;
            dlMainFP.DataBind();
            double totAPA = 0.0000;
            foreach (DataListItem dli in dlMainFP.Items)
            {
                if (Convert.ToDouble(dtFarmDetails.Rows[dli.ItemIndex]["AvaliablePlotArea"].ToString()) == 0.0000)
                {
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnAddFarm") as Button).Visible = false;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Visible = true;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Enabled = false;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
                }
                else
                {
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnAddFarm") as Button).Visible = true;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Visible = false;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Enabled = false;
                    (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
                }
            }
            DataTable dtFarm = fpobj.GetPlantation(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), hfFarmerID.Value.ToString());
            Session["dtFarm_s"] = dtFarm;
            if (dtFarm.Rows.Count > 0)
            {
                foreach (DataListItem dli in dlMainFP.Items)
                {
                    string farmid = dlMainFP.DataKeys[dli.ItemIndex].ToString();
                    DataTable dt = new DataTable();
                    dt = dtFarm.Clone();
                    DataRow[] drs = dtFarm.Select("ParentFarmID = " + farmid);
                    foreach (DataRow dr in drs)
                        dt.ImportRow(dr);
                    GridView gvCrop = (GridView)dli.FindControl("gvCrops");
                    gvCrop.DataSource = dt;
                    gvCrop.DataBind();
                    if (dt.Rows.Count >= 1)
                    {
                        (dlMainFP.Items[dli.ItemIndex].FindControl("btnAddFarm") as Button).Visible = false;
                        (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Visible = true;
                        (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).Enabled = false;
                        (dlMainFP.Items[dli.ItemIndex].FindControl("btnEdit") as Button).Visible = true;
                        (dlMainFP.Items[dli.ItemIndex].FindControl("btnDisable") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
                    }
                }
            }
        }
        else
        {

        }
    }
    protected void btnFarmerSave_Click(object sender, EventArgs e)
    {
        DataTable dtunit = new DataTable();
        DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue));
        if (dtBasicFInfo.Rows.Count > 0)
        {
            for (int count = 0; count < dlMainFP.Items.Count; count++)
            {
                int PFarmID = Convert.ToInt32(dlMainFP.DataKeys[count].ToString());
                GridView gv = (dlMainFP.Items[count].FindControl("gvCrops") as GridView);
                if (gv.Rows.Count > 0)
                {
                    decimal PTotalArea = Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblAvaliableArea") as Label).Text);
                    decimal TotalArea = 0;
                    decimal totalInterCropArea = 0;
                    for (int gvCount = 0; gvCount < gv.Rows.Count; gvCount++)
                    {
                        DataKey dk = gv.DataKeys[gvCount];
                        int FarmId = !string.IsNullOrEmpty(dk.Values["FarmId"].ToString()) ? Convert.ToInt32(dk.Values["FarmId"].ToString()) : 0;
                        int PlantationId = !string.IsNullOrEmpty(dk.Values["PlantationId"].ToString()) ? Convert.ToInt32(dk.Values["PlantationId"].ToString()) : 0;
                        string PA = (gv.Rows[gvCount].FindControl("txtPlotArea") as TextBox).Text;

                        bool IsInterCrop = (gv.Rows[gvCount].FindControl("cbIsInterCrop") as CheckBox).Checked;
                        decimal PlotArea = !string.IsNullOrEmpty(PA) ? Convert.ToDecimal(PA) : 0;
                        if (!IsInterCrop)
                            TotalArea += PlotArea;
                        if (IsInterCrop)
                            totalInterCropArea += PlotArea;
                        DateTime PlantationDate = objMudarApp.GenerateRandomDate(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString(), dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
                        int FHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutTo"].ToString()));
                        int SHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutTo"].ToString()));

                        // First Cut Estimation Herbage Qty
                        int FEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCTo"].ToString()));
                        // Total Herbage firstcut (estimated)
                        decimal FHerQty = Math.Round((PlotArea * FEHQty), 1);
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
                        decimal SHerQty = Math.Round((PlotArea * SEHQty), 1);
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

                        #region commentcode(old code)
                        //Actual Herbage Percentage
                        //decimal HQty = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                        // First Actual Herbage Qty
                        //int FHQty = Convert.ToInt32(FEHQty * HQty * PlotArea);
                        // Second Estimation Herbage Qty
                        //int SEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCTo"].ToString()));
                        // Second Actual Herbage Qty
                        //int SHQty = Convert.ToInt32(SEHQty * HQty * PlotArea);
                        //decimal FROil = FHQty * MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilTo"].ToString()));
                        //decimal SROil = SHQty * MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilTo"].ToString()));

                        #endregion
                        dtunit = UI.UnitInformationBasedOnVillage(lblVillage.Text);
                        string unitid = string.Empty;
                        if (dtunit.Rows.Count > 0)
                        {
                            int getunitID = MudarApp.RandomNumber(1, dtunit.Rows.Count);
                            if (dtunit.Rows.Count > 2)
                                unitid = dtunit.Rows[getunitID]["UnitId"].ToString();
                            else
                                unitid = dtunit.Rows[0]["UnitId"].ToString();
                        }
                        else
                        {
                            unitid = new Guid().ToString();
                        }
                        bool resutl = false;
                        if (!IsInterCrop)
                        {
                            if (TotalArea <= PTotalArea && TotalArea > 0)
                            {
                                if (FarmId == 0 && PlantationId == 0)
                                {

                                    resutl = fp.Farm_PlantationDetails_INSandUPDandDEL(hfFarmerID.Value.ToString(), FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                       unitid, PlantationDate, PlantationDate.AddDays(FHCount), FTHqty, PlantationDate.AddDays(FHCount + 1), 0, FTOil, PlantationDate.AddDays(FHCount + SHCount), SHerQty,
                                       PlantationDate.AddDays(FHCount + SHCount + 1), 0, STOil, FTOil + STOil, true, true, "Aslam", string.Empty,
                                       Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                        Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                }
                            }
                            else
                            {
                                if (FarmId == 0 && PlantationId == 0)
                                    Response.Write("<script>alert('Crop Area should not exceed the total Avaliable Area !!!!!!!!');</script>");
                            }
                        }
                        else
                        {
                            if (totalInterCropArea <= Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblPlotArea") as Label).Text) && totalInterCropArea > 0)
                            {
                                //if (FarmId == 0 && PlantationId == 0)
                                {

                                    resutl = fp.Farm_PlantationDetails_INSandUPDandDEL(hfFarmerID.Value.ToString(), FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                       unitid, PlantationDate, PlantationDate.AddDays(FHCount), 0, PlantationDate.AddDays(FHCount + 2), 0, 0, PlantationDate.AddDays(FHCount + SHCount), 0,
                                       PlantationDate.AddDays(FHCount + SHCount + 2), 0, 0, 0, false, false, "Aslam", string.Empty,
                                       Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                        Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                }
                            }
                            else
                            {
                                if (FarmId == 0 && PlantationId == 0)
                                    Response.Write("<script>alert(' Inter Crop Area should not exceed the total PlotArea !!!!!!!!');</script>");
                            }
                        }
                    }
                }
            }
            if (ddlProduct.SelectedIndex > 0)
            {
                BindDlMainFP();
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Saved Successfully');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('plz check the basicfarming information');</script>");
            return;
        }
    }
    protected void btnFarmerBack_Click(object sender, EventArgs e)
    {
        BindFarmerProductWiseDetails();
        lblSyear.Text = ddlYear.SelectedItem.Text;
        lblSname.Text = ddlSeason.SelectedItem.Text;
        lblProduct.Text = ddlProduct.SelectedItem.Text;
        divfarmerlist.Visible = true;
        lnkImportFarmerProdInfo.Visible = true;
        divSeasonDetails.Visible = true;
        divFindDetails.Visible = false;
        divGetDetails.Visible = false;
        divFarmerPlotDetails.Visible = false;
        trTotalplots.Visible = true;
    }

    protected void dlMainFP_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
             e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gv = (GridView)e.Item.FindControl("gvCrops");
            if (gv.Rows.Count >= 1)
            {
                //(e.Item.FindControl("btnAddFarm") as Button).Enabled = false;
                (e.Item.FindControl("btnDisable") as Button).Visible = true;
                (e.Item.FindControl("btnDisable") as Button).Enabled = false;
                (e.Item.FindControl("btnDisable") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
            }
        }
    }
    #endregion
    #region Farmer Production
    protected void btnFPGo_Click(object sender, EventArgs e)
    {
        //Bindgvfarmer();
        NewBindgvfarmer();
    }
    private void NewBindgvfarmer()
    {
        int year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
        int seasonid = Convert.ToInt32(ddlSeason.SelectedIndex > 0 ? ddlSeason.SelectedValue.ToString() : "0");
        int productid = Convert.ToInt32(ddlProduct.SelectedIndex > 0 ? ddlProduct.SelectedValue.ToString() : "0");
        DataTable dtPlantation = fp.BuildPlantation(year, seasonid, productid, hfFarmerID.Value);
        if (dtPlantation.Rows.Count > 0)
        {
            divNewFarmerPlantation.Visible = true;
            gvNewFarmerPlantation.DataSource = dtPlantation;
            for (int i = 0; i < dtPlantation.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtPlantation.Rows[i][17]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtPlantation.Rows[i][17] = DBNull.Value;
                    dtPlantation.Rows[i][18] = DBNull.Value;
                    dtPlantation.Rows[i][19] = DBNull.Value;
                    dtPlantation.Rows[i][20] = DBNull.Value;
                    dtPlantation.Rows[i][21] = DBNull.Value;
                }
            }
            gvNewFarmerPlantation.DataBind();
            btnFPBack.Visible = true;
        }
        else
        {
            Response.Write("<script>alert('!!!! No Production for this !!!!! Farmer');</script>");
            divNewFarmerPlantation.Visible = false;
            divfarmerlist.Visible = true;
            lnkImportFarmerProdInfo.Visible = true;
        }
    }
    private void Bindgvfarmer()
    {
        string farmercode = hfFarmerCode.Value;
        int year = Convert.ToInt32(ddlYear.SelectedValue.ToString());
        int seasonid = Convert.ToInt32(ddlSeason.SelectedIndex > 0 ? ddlSeason.SelectedValue.ToString() : "0");
        int productid = Convert.ToInt32(ddlProduct.SelectedIndex > 0 ? ddlProduct.SelectedValue.ToString() : "0");
        DataTable dtTemp = fp.BuildPlantation(year, seasonid, productid, farmercode);
        if (dtTemp.Rows.Count > 0)
        {
            //divFarmerPlantationDetails.Visible = true;
            divNewFarmerPlantation.Visible = true;
            divFindDetails.Visible = true;
            divSeasonDetails.Visible = true;
            btnFPSave.Visible = true;
            btnFPBack.Visible = true;
            gvFarmer.DataSource = dtTemp.DefaultView;
            gvFarmer.DataBind();
            foreach (GridViewRow gvr in gvFarmer.Rows)
            {
                if (!string.IsNullOrEmpty(dtTemp.Rows[gvr.RowIndex]["FirstUnitId"].ToString()))
                {
                    DropDownList ddFirst = gvr.FindControl("ddlUnit") as DropDownList;
                    ddFirst.ClearSelection();
                    (gvr.FindControl("ddlUnit") as DropDownList).SelectedValue = dtTemp.Rows[gvr.RowIndex]["FirstUnitId"].ToString();
                }
                if (!string.IsNullOrEmpty(dtTemp.Rows[gvr.RowIndex]["SecondUnitId"].ToString()))
                {
                    DropDownList ddSec = gvr.FindControl("ddlSecUnit") as DropDownList;
                    ddSec.ClearSelection();
                    (gvr.FindControl("ddlSecUnit") as DropDownList).SelectedValue = dtTemp.Rows[gvr.RowIndex]["SecondUnitId"].ToString();
                }
            }
        }
        else
        {
            gvFarmer.DataBind();
            Response.Write("<script>alert('*no data found* !!!plz add the crops!!!!');</script>");
            DataTable dtLoginDetails = Session["dtLoginDetails"] as DataTable;
            DataTable dtFarmerDetails = farmerobj.GetFarmerProdcts(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), ddlVillage.SelectedItem.Text, Convert.ToString(dtLoginDetails.Rows[0]["UserLoginID"]));
            if (dtFarmerDetails.Rows.Count > 0)
            {
                lblSyear.Text = ddlYear.SelectedItem.Text;
                lblSname.Text = ddlSeason.SelectedItem.Text;
                lblProduct.Text = ddlProduct.SelectedItem.Text;
                divfarmerlist.Visible = true;
                lnkImportFarmerProdInfo.Visible = true;
                divSeasonDetails.Visible = true;
                divFindDetails.Visible = false;
                divGetDetails.Visible = false;
                gvFarmerList.DataSource = dtFarmerDetails;
                gvFarmerList.DataBind();
            }
        }
    }
    private void gvFirstCutVisible()
    {
        for (int i = 13; i < 22; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }
        for (int i = 22; i < 31; i++)
        {
            gvFarmer.Columns[i].Visible = false;
        }
    }
    private void gvSecCutVisible()
    {
        for (int i = 13; i < 22; i++)
        {
            gvFarmer.Columns[i].Visible = false;
        }
        for (int i = 22; i < 31; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }
    }
    private void gvAllCutVisible()
    {
        for (int i = 11; i < 32; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }

    }
    protected void btnFPSave_Click(object sender, EventArgs e)
    {
        bool result = false;
        foreach (GridViewRow gvr in gvFarmer.Rows)
        {
            int plantationid = Convert.ToInt32(gvFarmer.DataKeys[gvr.RowIndex].Value);
            decimal PlantationArea = !string.IsNullOrEmpty((gvr.FindControl("txtPlantationArea") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtPlantationArea") as TextBox).Text) : 0;
            DateTime PlantationDate = !string.IsNullOrEmpty((gvr.FindControl("txtPlantationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtPlantationDate") as TextBox).Text) : DateTime.Now;
            DateTime FirstHarvestDate = !string.IsNullOrEmpty((gvr.FindControl("txtFirstHarvestDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtFirstHarvestDate") as TextBox).Text) : DateTime.Now;
            decimal FirstHerbaga = !string.IsNullOrEmpty((gvr.FindControl("txtFirstHerbaga") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtFirstHerbaga") as TextBox).Text) : 0;
            DateTime FirstDistillationDate = !string.IsNullOrEmpty((gvr.FindControl("txtFirstDistillationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtFirstDistillationDate") as TextBox).Text) : DateTime.Now;
            int FirstDistillationUnitNO = !string.IsNullOrEmpty((gvr.FindControl("txtFirstDistillationUnitNO") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtFirstDistillationUnitNO") as TextBox).Text) : 0;
            decimal FirstProductQuantity = !string.IsNullOrEmpty((gvr.FindControl("txtFirstProductQuantity") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtFirstProductQuantity") as TextBox).Text) : 0;
            bool FirstProductCompletion = (gvr.FindControl("txtFirstProductCompletion") as CheckBox).Checked;

            DateTime SecFirstHarvestDate = !string.IsNullOrEmpty((gvr.FindControl("txtSecondHarvestDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtSecondHarvestDate") as TextBox).Text) : DateTime.Now;
            decimal SecFirstHerbaga = !string.IsNullOrEmpty((gvr.FindControl("txtSecondHerbaga") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtSecondHerbaga") as TextBox).Text) : 0;
            DateTime SecFirstDistillationDate = !string.IsNullOrEmpty((gvr.FindControl("txtSecondDistillationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtSecondDistillationDate") as TextBox).Text) : DateTime.Now;
            int SecFirstDistillationUnitNO = !string.IsNullOrEmpty((gvr.FindControl("txtSecondDistillationUnitNO") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtSecondDistillationUnitNO") as TextBox).Text) : 0;
            decimal SecFirstProductQuantity = !string.IsNullOrEmpty((gvr.FindControl("txtSecondProductQuantity") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtSecondProductQuantity") as TextBox).Text) : 0;
            bool SecFirstProductCompletion = (gvr.FindControl("txtSecondProductCompletion") as CheckBox).Checked;
            string FirstUnit = ((gvr.FindControl("ddlUnit") as DropDownList).SelectedItem.Value);
            string SecUnit = ((gvr.FindControl("ddlSecUnit") as DropDownList).SelectedItem.Value);
            int FirstNoLots = !string.IsNullOrEmpty((gvr.FindControl("txtNoOfLots") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtNoOfLots") as TextBox).Text) : 0;
            int SecNoLots = !string.IsNullOrEmpty((gvr.FindControl("txtSecNoOfLots") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtSecNoOfLots") as TextBox).Text) : 0;
            string FirstLotNo = (gvr.FindControl("hfLotNos") as HiddenField).Value;

            string SecLotNo = (gvr.FindControl("hfSecLotNos") as HiddenField).Value;

            string temp = Guid.NewGuid().ToString();
            result = fp.PlantationDetails_INSandUPDandDEL(temp, 0, temp, 0, PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate, FirstDistillationUnitNO,
                FirstProductQuantity, SecFirstHarvestDate, SecFirstHerbaga, SecFirstDistillationDate, SecFirstDistillationUnitNO, SecFirstProductQuantity,
                (FirstProductQuantity + SecFirstProductQuantity), false, false, "shaik Aslam", "shaik aslam", 0, PlantationArea, MudarApp.Update,
                plantationid, FirstUnit, SecUnit, FirstNoLots, SecNoLots, FirstLotNo, SecLotNo);
        }

        //Bindgvfarmer();
        NewBindgvfarmer();
    }
    protected void DropDownList1_Load1(object sender, EventArgs e)
    {
        DataTable dt = fp.BindDropDownChild();
        if (((DropDownList)sender).Items.Count <= 0)
        {
            ((DropDownList)sender).DataSource = dt.DefaultView;
            ((DropDownList)sender).DataTextField = "Ucode";
            ((DropDownList)sender).DataValueField = "UnitId";
            ((DropDownList)sender).DataBind();
        }
    }
    protected void btnFPBack_Click(object sender, EventArgs e)
    {
        lblSyear.Text = ddlYear.SelectedItem.Text;
        lblSname.Text = ddlSeason.SelectedItem.Text;
        lblProduct.Text = ddlProduct.SelectedItem.Text;
        divNewFarmerPlantation.Visible = false;
        divfarmerlist.Visible = true;
        lnkImportFarmerProdInfo.Visible = true;
        divSeasonDetails.Visible = true;
        divFindDetails.Visible = false;
        divGetDetails.Visible = false;
        divFarmerPlotDetails.Visible = false;
        btnFPBack.Visible = false;
        btnFPSave.Visible = false;
    }
    #endregion
    //protected void btnCrops_Click(object sender, EventArgs e)
    //{
    //    divProduction.Visible = false;
    //    divSelect.Visible = false;
    //    divGetDetails.Visible = true;
    //}
    //protected void btnProduction_Click(object sender, EventArgs e)
    //{
    //    divProduction.Visible = true;
    //    divSelect.Visible = false;
    //    divGetDetails.Visible = true;
    //}
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divGetDetails.Visible = true;
        divSeasonDetails.Visible = false;
        divfarmerlist.Visible = false;
        lnkImportFarmerProdInfo.Visible = true;
        ddlSeason.Items.Clear();
        BindSeasonDetails();
        ddlProduct.Items.Clear();
        BindProductList();
        ddlVillage.Enabled = false;
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divSeasonDetails.Visible = true;
        BindFarmerProductWiseDetails();
    }
    protected void btnImportSubmit_Click(object sender, EventArgs e)
    {
        if (fileUpload1.HasFile)
        {
            string fileName = DateTime.Now.ToString("ddMMyyyy_hhmmss_") + System.IO.Path.GetFileName(fileUpload1.FileName);
            string filePath = System.IO.Path.Combine(Server.MapPath("~/Uploads"), fileName);
            fileUpload1.SaveAs(filePath);
            fileUpload1.Attributes.Clear();
            fileUpload1.Dispose();
            string ext = System.IO.Path.GetExtension(fileUpload1.FileName);
            string conStr = string.Empty;
            if (ext == ".xls" || ext == ".xlsx")
            {
                switch (ext)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, filePath, false);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

               
                DataTable dtunit = new DataTable();
                DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue));
                if (dtBasicFInfo.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        int PFarmID = 0;
                        string farmerId = string.Empty;
                        
                        DataTable pDT = fp.GetParentFarmId(Convert.ToString(dt.Rows[count]["Plot Area Code"]));//Convert.ToInt32(dlMainFP.DataKeys[count].ToString());
                        
                        if (pDT != null && pDT.Rows.Count > 0)
                        {
                            PFarmID = Convert.ToInt32(pDT.Rows[0]["FarmID"]);
                            farmerId = Convert.ToString(pDT.Rows[0]["FarmerID"]);
                        }
                        decimal PTotalArea = Convert.ToDecimal(dt.Rows[count]["Total Area"]);
                        //GridView gv = (dlMainFP.Items[count].FindControl("gvCrops") as GridView);
                        //if (gv.Rows.Count > 0)
                        //{
                        decimal TotalArea = 0;
                        decimal totalInterCropArea = 0;
                        //for (int gvCount = 0; gvCount < gv.Rows.Count; gvCount++)
                        //{
                        int FarmId = 0;
                        int PlantationId = 0;
                        DataTable dataKeys = fp.GetPlotFarmPlantationDetails(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSeason.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue), PFarmID);
                        if (dataKeys.Rows.Count > 0)
                        {
                            FarmId = !string.IsNullOrEmpty(dataKeys.Rows[0]["FarmId"].ToString()) ? Convert.ToInt32(dataKeys.Rows[0]["FarmId"].ToString()) : 0;
                            PlantationId = !string.IsNullOrEmpty(dataKeys.Rows[0]["PlantationId"].ToString()) ? Convert.ToInt32(dataKeys.Rows[0]["PlantationId"].ToString()) : 0;
                        }
                        else
                        {
                            FarmId = 0;
                            PlantationId = 0;
                        }
                        //DataKey dk = gv.DataKeys[gvCount];
                        //int FarmId = !string.IsNullOrEmpty(dk.Values["FarmId"].ToString()) ? Convert.ToInt32(dk.Values["FarmId"].ToString()) : 0;
                        //int PlantationId = !string.IsNullOrEmpty(dk.Values["PlantationId"].ToString()) ? Convert.ToInt32(dk.Values["PlantationId"].ToString()) : 0;
                        string PA = Convert.ToString(dt.Rows[count]["Crop Area"]);

                        bool IsInterCrop = false;
                        decimal PlotArea = !string.IsNullOrEmpty(PA) ? Convert.ToDecimal(PA) : 0;
                        if (!IsInterCrop)
                            TotalArea += PlotArea;
                        if (IsInterCrop)
                            totalInterCropArea += PlotArea;
                        DateTime PlantationDate = objMudarApp.GenerateRandomDate(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString(), dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
                        int FHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutTo"].ToString()));
                        
                        int SHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutTo"].ToString()));

                        // First Cut Estimation Herbage Qty
                        int FEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty1stCutHCTo"].ToString()));
                        // Total Herbage firstcut (estimated)
                        decimal FHerQty = Math.Round((PlotArea * FEHQty), 1);
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
                        decimal SHerQty = Math.Round((PlotArea * SEHQty), 1);
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

                        #region commentcode(old code)
                        //Actual Herbage Percentage
                        //decimal HQty = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                        // First Actual Herbage Qty
                        //int FHQty = Convert.ToInt32(FEHQty * HQty * PlotArea);
                        // Second Estimation Herbage Qty
                        //int SEHQty = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["Qty2ndCutHCTo"].ToString()));
                        // Second Actual Herbage Qty
                        //int SHQty = Convert.ToInt32(SEHQty * HQty * PlotArea);
                        //decimal FROil = FHQty * MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["1stRecoveryOilTo"].ToString()));
                        //decimal SROil = SHQty * MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilFrom"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["2ndRecoveryOilTo"].ToString()));

                        #endregion

                        dtunit = UI.UnitInformationBasedOnVillage(lblVillage.Text);
                        string unitid = string.Empty;
                        if (dtunit.Rows.Count > 0)
                        {
                            int getunitID = MudarApp.RandomNumber(1, dtunit.Rows.Count);
                            if (dtunit.Rows.Count > 2)
                                unitid = dtunit.Rows[getunitID]["UnitId"].ToString();
                            else
                                unitid = dtunit.Rows[0]["UnitId"].ToString();
                        }
                        else
                        {
                            unitid = new Guid().ToString();
                        }
                        bool resutl = false;
                        if (!IsInterCrop)
                        {
                            if (TotalArea <= PTotalArea && TotalArea > 0)
                            {
                                //if (FarmId == 0 && PlantationId == 0)
                                //{
                                resutl = fp.Farm_PlantationDetails_INSandUPDandDEL(farmerId, FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                   unitid, PlantationDate, PlantationDate.AddDays(FHCount), FTHqty, PlantationDate.AddDays(FHCount + 1), 0, FTOil, PlantationDate.AddDays(FHCount + SHCount), SHerQty,
                                   PlantationDate.AddDays(FHCount + SHCount + 1), 0, STOil, FTOil + STOil, true, true, "Aslam", string.Empty,
                                   Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                    Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                //}
                            }
                            else
                            {
                                if (FarmId == 0 && PlantationId == 0)
                                    Response.Write("<script>alert('Crop Area should not exceed the total Avaliable Area !!!!!!!!');</script>");
                            }
                        }
                        else
                        {
                            if (totalInterCropArea <= Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblPlotArea") as Label).Text) && totalInterCropArea > 0)
                            {
                                //if (FarmId == 0 && PlantationId == 0)
                                {

                                    resutl = fp.Farm_PlantationDetails_INSandUPDandDEL(hfFarmerID.Value.ToString(), FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlProduct.SelectedValue),
                                       unitid, PlantationDate, PlantationDate.AddDays(FHCount), 0, PlantationDate.AddDays(FHCount + 2), 0, 0, PlantationDate.AddDays(FHCount + SHCount), 0,
                                       PlantationDate.AddDays(FHCount + SHCount + 2), 0, 0, 0, false, false, "Aslam", string.Empty,
                                       Convert.ToInt32(ddlSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                        Convert.ToInt32(ddlYear.SelectedValue), IsInterCrop, FHerQty, FOilEsti, SHerQty, SOilEsti);
                                }
                            }
                            else
                            {
                                if (FarmId == 0 && PlantationId == 0)
                                    Response.Write("<script>alert(' Inter Crop Area should not exceed the total PlotArea !!!!!!!!');</script>");
                            }
                        }
                        //}
                        //}
                    }
                    //if (ddlProduct.SelectedIndex > 0)
                    //{
                    //    BindDlMainFP();
                    //    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Saved Successfully');</script>");
                    //}
                }
                else
                {
                    Response.Write("<script>alert('plz check the basicfarming information');</script>");
                    return;
                }
                popupExtender.Hide();
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Imported Successfully');</script>");
                BindFarmerProductWiseDetails();
                //popupExtender.Show();
            }
        }
        else
        {
            lblImportErrorMsg.Text = "Please choose an excel file to import farnmer production info";
            popupExtender.Show();
        }
    }
    protected void btnImportClear_Click(object sender, EventArgs e)
    {
        fileUpload1.Attributes.Clear();
        lblImportErrorMsg.Text = string.Empty;
        popupExtender.Hide();
    }
}
