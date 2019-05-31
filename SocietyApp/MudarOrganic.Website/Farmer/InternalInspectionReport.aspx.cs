using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


using MudarOrganic.Components;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Web.Configuration;


public partial class Farmer_InternalInspectionReport : System.Web.UI.Page
{
    Farmer_BL farmerobj = new Farmer_BL();
    DataTable dtCheckpoint = new DataTable();
    MudarUser mu = new MudarUser();
    Reports_BL reportObj = new Reports_BL();
    InspectionPlan_BL inobj = new InspectionPlan_BL();
    Farming_BL farmingObj = new Farming_BL();
    public static string farmerID, Farmercode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            farmerID = Session["FarmerId"].ToString();
            if (!string.IsNullOrEmpty(farmerID))
            {
                YearAssaign();
                GridAssign();
                BindFarmerDetails();
                BindFarmingDetails();
                BindPlotandPlantinfo();
            }
        }
    }
    private void BindPlotandPlantinfo()
    {
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dt = inobj.GetPlotandPlantinfo(farmerID,DateTime.Now.Year);
        if (dt.Rows.Count > 0)
        {
            gvMainPlotDetails.DataSource = dt;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dt.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dt.Rows[i][4] = DBNull.Value;
                    dt.Rows[i][5] = DBNull.Value;
                    dt.Rows[i][6] = DBNull.Value;
                    dt.Rows[i][7] = DBNull.Value;
                }
            }
            gvMainPlotDetails.DataBind();
        }
    }
    private void BindFarmingDetails()
    {
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dt = inobj.GetPlantingInfo(farmerID);
        if (dt.Rows.Count > 0)
        {
            gvSeedDetails.DataSource = dt;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dt.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dt.Rows[i][0] = DBNull.Value;
                    dt.Rows[i][1] = DBNull.Value;
                    dt.Rows[i][2] = DBNull.Value;
                    dt.Rows[i][3] = DBNull.Value;
                }
            }
            gvSeedDetails.DataBind();
        }
        #region input information
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("IDate");
        dtNew.Columns.Add("IMMaterial");
        dtNew.Columns.Add("IMQuantity");
        dtNew.Columns.Add("IMSource");
        for (int i = 0; i < dtpDate.Rows.Count; i++)
        {
            DataTable dtFinput = farmingObj.GetInputInfoonProductID(dtpDate.Rows[i]["productID"].ToString());
            if (dtFinput.Rows.Count > 0)
            {
                for (int j = 0; j < dtFinput.Rows.Count; j++)
                {
                    DataRow drNew = dtNew.NewRow();
                    if (dtFinput.Rows[j]["IMPlanting"].ToString() == "Planting")
                    {
                        if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["IMPlanting"].ToString() == "1st Harvest")
                    {
                        if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["IMPlanting"].ToString() == "2nd Harvest")
                    {
                        if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                            drNew["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    drNew["IMMaterial"] = dtFinput.Rows[j]["IMMaterial"].ToString();
                    drNew["IMQuantity"] = dtFinput.Rows[j]["IM_MT_HC"].ToString();
                    drNew["IMSource"] = dtFinput.Rows[j]["IMSource"].ToString();
                    dtNew.Rows.Add(drNew);
                }
            }
        }
        if (dtNew.Rows.Count > 0)
        {
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtNew.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtNew.Rows[i][0] = DBNull.Value;
                    dtNew.Rows[i][1] = DBNull.Value;
                    dtNew.Rows[i][2] = DBNull.Value;
                    dtNew.Rows[i][3] = DBNull.Value;
                }
            }
            gvInput.DataSource = dtNew;
            gvInput.DataBind();
        }
        #endregion
        #region Diease Info
        DataTable dtDiese = new DataTable();
        dtDiese.Columns.Add("DMIDate");
        dtDiese.Columns.Add("DiseaseName");
        dtDiese.Columns.Add("DMIPreventionMaterial");
        dtDiese.Columns.Add("DMISource");
        dtDiese.Columns.Add("DMIBillNo");
        dtDiese.Columns.Add("DMIQuantity");
        for (int i = 0; i < dtpDate.Rows.Count; i++)
        {
            DataTable dtFinput = farmingObj.GetDiseInfoonProductID(dtpDate.Rows[i]["productID"].ToString());
            if (dtFinput.Rows.Count > 0)
            {
                for (int j = 0; j < dtFinput.Rows.Count; j++)
                {
                    DataRow drNew = dtDiese.NewRow();
                    if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "Planting")
                    {
                        if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "1st Harvest")
                    {
                        if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "2nd Harvest")
                    {
                        if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                            drNew["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    drNew["DiseaseName"] = dtFinput.Rows[j]["DiseaseName"].ToString();
                    drNew["DMIPreventionMaterial"] = dtFinput.Rows[j]["DMIPreventionMaterial"].ToString();
                    drNew["DMISource"] = dtFinput.Rows[j]["DMISource"].ToString();
                    drNew["DMIBillNo"] = dtFinput.Rows[j]["DMIBillNo"].ToString();
                    drNew["DMIQuantity"] = dtFinput.Rows[j]["DMIT_HC"].ToString();
                    dtDiese.Rows.Add(drNew);
                }
            }
        }
        if (dtDiese.Rows.Count > 0)
        {
            for (int i = 0; i < dtDiese.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtDiese.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtDiese.Rows[i][0] = DBNull.Value;
                    dtDiese.Rows[i][1] = DBNull.Value;
                    dtDiese.Rows[i][2] = DBNull.Value;
                    dtDiese.Rows[i][3] = DBNull.Value;
                    dtDiese.Rows[i][4] = DBNull.Value;
                    dtDiese.Rows[i][5] = DBNull.Value;
                }
            }
            gvDiese.DataSource = dtDiese;
            gvDiese.DataBind();
        }
        #endregion
        #region Insect info
        DataTable dtInsect = new DataTable();
        dtInsect.Columns.Add("IMIDate");
        dtInsect.Columns.Add("InsectName");
        dtInsect.Columns.Add("IMIPreventionMaterial");
        dtInsect.Columns.Add("IMISource");
        dtInsect.Columns.Add("IMIBillNo");
        dtInsect.Columns.Add("IMIQuantity");
        for (int i = 0; i < dtpDate.Rows.Count; i++)
        {
            DataTable dtFinput = farmingObj.GetInsectInfoonProduct(dtpDate.Rows[i]["productID"].ToString());
            if (dtFinput.Rows.Count > 0)
            {
                for (int j = 0; j < dtFinput.Rows.Count; j++)
                {
                    DataRow drNew = dtInsect.NewRow();
                    if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "Planting")
                    {
                        if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "1st Harvest")
                    {
                        if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "2nd Harvest")
                    {
                        if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(dtpDate.Rows[i]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                            drNew["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                        }
                    }
                    drNew["InsectName"] = dtFinput.Rows[j]["InsectName"].ToString();
                    drNew["IMIPreventionMaterial"] = dtFinput.Rows[j]["IMIPreventionMaterial"].ToString();
                    drNew["IMISource"] = dtFinput.Rows[j]["IMISource"].ToString();
                    drNew["IMIBillNo"] = dtFinput.Rows[j]["IMIBillNo"].ToString();
                    drNew["IMIQuantity"] = dtFinput.Rows[j]["InsectM_MT_HC"].ToString();
                    dtInsect.Rows.Add(drNew);
                }
            }
        }
        if (dtInsect.Rows.Count > 0)
        {
            for (int i = 0; i < dtInsect.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtInsect.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtInsect.Rows[i][0] = DBNull.Value;
                    dtInsect.Rows[i][1] = DBNull.Value;
                    dtInsect.Rows[i][2] = DBNull.Value;
                    dtInsect.Rows[i][3] = DBNull.Value;
                    dtInsect.Rows[i][4] = DBNull.Value;
                    dtInsect.Rows[i][5] = DBNull.Value;
                }
            }
            gvInsect.DataSource = dtInsect;
            gvInsect.DataBind();
        }
        #endregion
        DataTable dtplantation = new DataTable();
        dtplantation = inobj.GetPlantaionDetails(farmerID);
        if (dtplantation.Rows.Count > 0)
        {
            for (int i = 0; i < dtplantation.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtplantation.Rows[i][0]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtplantation.Rows[i][0] = DBNull.Value;
                    dtplantation.Rows[i][1] = DBNull.Value;
                    dtplantation.Rows[i][2] = DBNull.Value;
                    dtplantation.Rows[i][3] = DBNull.Value;
                    dtplantation.Rows[i][4] = DBNull.Value;
                    dtplantation.Rows[i][5] = DBNull.Value;
                    dtplantation.Rows[i][6] = DBNull.Value;
                    dtplantation.Rows[i][7] = DBNull.Value;
                }
            }
            gvYields.DataSource = dtplantation;
            gvYields.DataBind();
        }
    }
    private void BindFarmerDetails()
    {
        DataTable dt = new DataTable();
        dt = farmerobj.GetFarmerDetailsonID(farmerID);
        if (dt.Rows.Count > 0)
        {
            lblFarmername.Text = dt.Rows[0]["FirstName"].ToString();
            lblVillage.Text = dt.Rows[0]["City_Village"].ToString();
            lblMIE.Text = dt.Rows[0]["FarmerCode"].ToString();
            lblTotalArea.Text = dt.Rows[0]["TotalAreaInHectares"].ToString();
            lblNoofOrganicplots.Text = dt.Rows[0]["NumberOfPlots"].ToString();
            lblTotalArea0.Text = dt.Rows[0]["TotalAreaInHectares"].ToString();
        }
        DataTable dtInspection = new DataTable();
        dtInspection = inobj.GetInspectionByFarmerID(farmerID);
        if (dtInspection.Rows.Count > 0)
        {
            lblInspectname.Text = dtInspection.Rows[0]["InspectorName"].ToString();
            lblPToDate.Text = dtInspection.Rows[0]["VisitedDate"].ToString();
            lblIDate.Text = dtInspection.Rows[0]["VisitedDate"].ToString();
            lblInspectname0.Text = dtInspection.Rows[0]["InspectorName"].ToString();
            lblPFromDate.Text = "";
            lblPToDate0.Text = dtInspection.Rows[0]["VisitedDate"].ToString();
        }
        DataTable dtnext = new DataTable();
        if (dtnext.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dtnext.Rows[1]["PlanDate"].ToString()))
                lblNDate.Text = dtnext.Rows[0]["PlanDate"].ToString();
        }
        DataTable dtFarm = farmerobj.GetFarmerFarmDetails(farmerID, DateTime.Now.Year.ToString());
        if (dtFarm.Rows.Count > 0)
        {
            double TotalArea = 0.000;
            for (int i = 0; i < dtFarm.Rows.Count; i++)
            {
                TotalArea = Convert.ToDouble(dtFarm.Rows[i]["PlotArea"].ToString()) + TotalArea;
            }
            double Vacantarea = 0.000;
            Vacantarea = Convert.ToDouble(lblTotalArea.Text) - TotalArea;
            lblVacant.Text = Math.Round(Vacantarea, 3).ToString();
            //lblVacant.Text = Vacantarea.ToString();
            lblTotalOrganic.Text = TotalArea.ToString();
            gvFarmDeatils.DataSource = dtFarm;
            gvFarmDeatils.DataBind();
            gvFarmDeatils2.DataSource = dtFarm;
            gvFarmDeatils2.DataBind();
            gvFarmDeatils3.DataSource = dtFarm;
            gvFarmDeatils3.DataBind();
            gvFarmDeatils4.DataSource = dtFarm;
            gvFarmDeatils4.DataBind();
            gvFarmDeatils5.DataSource = dtFarm;
            gvFarmDeatils5.DataBind();
        }
    }
    private void YearAssaign()
    {
        DataTable dttemp = farmerobj.GetYearFarmer(farmerID);
        if (dttemp.Rows.Count > 0)
        {
        }
    }
    private void GridAssign()
    {
        DataTable dtAnimal = new DataTable();
        DataTable dtSubAnimal = new DataTable();
        DataTable dtFarm = new DataTable();
        DataTable dtSubFarm = new DataTable();
        DataTable dtOC = new DataTable();
        DataTable dtSubOC = new DataTable();
        DataTable dtRMC = new DataTable();
        DataTable dtsubRMC = new DataTable();
        DataTable dtFA = new DataTable();
        DataTable dtSubFA = new DataTable();
        DataTable dtRP = new DataTable();
        DataTable dtSubRP = new DataTable();
        DataTable dtSCF = new DataTable();
        DataTable dtsubSCF = new DataTable();
        DataTable dtEC = new DataTable();
        DataTable dtSubEC = new DataTable();
        DataTable dtStatComp = new DataTable();
        DataTable dtSubStatComp = new DataTable();
        DataTable dtLF = new DataTable();
        DataTable dtSubLF = new DataTable();
        farmerobj.GetNewCheckpointQuestions(ref dtAnimal, ref  dtFarm, ref  dtOC, ref  dtRMC, ref  dtFA, ref  dtRP, ref  dtSCF, ref  dtEC, ref  dtStatComp, ref dtLF, farmerID, 2014);
        #region gvAnimal
        gvAnimal.DataSource = dtAnimal.DefaultView;
        gvAnimal.DataBind();
        if (dtAnimal.Rows.Count > 0)
        {
            for (int i = 0; i < dtAnimal.Rows.Count; i++)
            {
                dtSubAnimal = farmerobj.Getsubcheckpoints(dtAnimal.Rows[i]["QuestionID"].ToString());
                if (dtSubAnimal.Rows.Count > 0)
                    for (int j = 0; j < dtSubAnimal.Rows.Count; j++)
                        (gvAnimal.Rows[i].Cells[0].FindControl("rblAL") as RadioButtonList).Items.Add(dtSubAnimal.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvAnimal.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblAL") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "Good")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "only organic feed")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "only with prescription")
                    rb.Items[r].Selected = true;
            }
        }
        #endregion
        #region gvFarmManagament
        gvFarmMangement.DataSource = dtFarm.DefaultView;
        gvFarmMangement.DataBind();
        if (dtFarm.Rows.Count > 0)
        {
            for (int i = 0; i < dtFarm.Rows.Count; i++)
            {
                dtSubFarm = farmerobj.Getsubcheckpoints(dtFarm.Rows[i]["QuestionID"].ToString());
                if (dtSubFarm.Rows.Count > 0)
                    for (int j = 0; j < dtSubFarm.Rows.Count; j++)
                        (gvFarmMangement.Rows[i].Cells[0].FindControl("rblFM") as RadioButtonList).Items.Add(dtSubFarm.Rows[j]["SubText"].ToString());

            }
        }
        foreach (GridViewRow gvr in gvFarmMangement.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblFM") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "organically managed")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "only organic crops grown")
                    rb.Items[r].Selected = true;
            }
        }
        #endregion
        #region gvOrganiCompliance
        gvOrannicComp.DataSource = dtOC.DefaultView;
        gvOrannicComp.DataBind();
        if (dtOC.Rows.Count > 0)
        {
            for (int i = 0; i < dtOC.Rows.Count; i++)
            {
                dtSubOC = farmerobj.Getsubcheckpoints(dtOC.Rows[i]["QuestionID"].ToString());
                if (dtSubOC.Rows.Count > 0)
                    for (int j = 0; j < dtSubOC.Rows.Count; j++)
                        (gvOrannicComp.Rows[i].Cells[0].FindControl("rblOC") as RadioButtonList).Items.Add(dtSubOC.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvOrannicComp.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblOC") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "complies")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "no risk of drift")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "manual")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "no burning at all")
                    rb.Items[r].Selected = true;
            }
        }
        #endregion
        #region gvRiskManagamentCompliance
        gvRMC.DataSource = dtRMC.DefaultView;
        gvRMC.DataBind();
        if (dtRMC.Rows.Count > 0)
        {
            for (int i = 0; i < dtRMC.Rows.Count; i++)
            {
                dtsubRMC = farmerobj.Getsubcheckpoints(dtRMC.Rows[i]["QuestionID"].ToString());
                if (dtsubRMC.Rows.Count > 0)
                    for (int j = 0; j < dtsubRMC.Rows.Count; j++)
                        (gvRMC.Rows[i].Cells[0].FindControl("rblRMC") as RadioButtonList).Items.Add(dtsubRMC.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvRMC.Rows)
        {

            RadioButtonList rb = gvr.FindControl("rblRMC") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "No risk")
                    rb.Items[r].Selected = true;
            }
        }
        foreach (GridViewRow Row in gvRMC.Rows)
        {
            if (gvRMC.Rows[Row.RowIndex].Cells[1].Text == "Neighboring Fields")
                gvRMC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
        }
        #endregion
        #region FarmerAwareness
        gvFA.DataSource = dtFA.DefaultView;
        gvFA.DataBind();
        if (dtFA.Rows.Count > 0)
        {
            for (int i = 0; i < dtFA.Rows.Count; i++)
            {
                dtSubFA = farmerobj.Getsubcheckpoints(dtFA.Rows[i]["QuestionID"].ToString());
                if (dtSubFA.Rows.Count > 0)
                    for (int j = 0; j < dtSubFA.Rows.Count; j++)
                        (gvFA.Rows[i].Cells[0].FindControl("rblFA") as RadioButtonList).Items.Add(dtSubFA.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvFA.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblFA") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "Trained")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "most of them are known")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Aware & maintain Sustainebility")
                    rb.Items[r].Selected = true;
            }
        }
        foreach (GridViewRow Row in gvFA.Rows)
        {
            if (gvFA.Rows[Row.RowIndex].Cells[1].Text == "Farmer Training")
                gvFA.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvFA.Rows[Row.RowIndex].Cells[1].Text == "Internal FT standards")
                gvFA.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
        }
        #endregion
        #region RiskProcessing
        gvRP.DataSource = dtRP.DefaultView;
        gvRP.DataBind();
        if (dtFA.Rows.Count > 0)
        {
            for (int i = 0; i < dtRP.Rows.Count; i++)
            {
                dtSubRP = farmerobj.Getsubcheckpoints(dtRP.Rows[i]["QuestionID"].ToString());
                if (dtSubRP.Rows.Count > 0)
                    for (int j = 0; j < dtSubRP.Rows.Count; j++)
                        (gvRP.Rows[i].Cells[0].FindControl("rblRP") as RadioButtonList).Items.Add(dtSubRP.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvRP.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblRP") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "No risk")
                    rb.Items[r].Selected = true;
            }
        }
        #endregion
        #region Safety Complianne Farms
        gvSafetyComp.DataSource = dtSCF.DefaultView;
        gvSafetyComp.DataBind();
        if (dtSCF.Rows.Count > 0)
        {
            for (int i = 0; i < dtSCF.Rows.Count; i++)
            {
                dtsubSCF = farmerobj.Getsubcheckpoints(dtSCF.Rows[i]["QuestionID"].ToString());
                if (dtsubSCF.Rows.Count > 0)
                    for (int j = 0; j < dtsubSCF.Rows.Count; j++)
                        (gvSafetyComp.Rows[i].Cells[0].FindControl("rblSafComp") as RadioButtonList).Items.Add(dtsubSCF.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvSafetyComp.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblSafComp") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "sufficiently available")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "No risk")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Clean water")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "family children after school hours allowed")
                    rb.Items[r].Selected = true;
            }
        }
        #endregion
        #region Enviromnet Compliance
        gvEC.DataSource = dtEC.DefaultView;
        gvEC.DataBind();
        if (dtEC.Rows.Count > 0)
        {
            for (int i = 0; i < dtEC.Rows.Count; i++)
            {
                dtSubEC = farmerobj.Getsubcheckpoints(dtEC.Rows[i]["QuestionID"].ToString());
                if (dtSubEC.Rows.Count > 0)
                    for (int j = 0; j < dtSubEC.Rows.Count; j++)
                        (gvEC.Rows[i].Cells[0].FindControl("rblEc") as RadioButtonList).Items.Add(dtSubEC.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvEC.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblEc") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "Electrity")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Flood irrigation")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "No risk")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "not applicable to this farmer")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Trained")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "very good")
                    rb.Items[r].Selected = true;
            }
        }
        foreach (GridViewRow Row in gvEC.Rows)
        {
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "energy used for pumping water")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "Water Conservation")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "amount spent in alternative fuels")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "Cleanliness of the Farm")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
        }
        #endregion
        #region Statutory Compliance
        gvStatComp.DataSource = dtStatComp.DefaultView;
        gvStatComp.DataBind();
        if (dtStatComp.Rows.Count > 0)
        {
            for (int i = 0; i < dtStatComp.Rows.Count; i++)
            {
                dtSubStatComp = farmerobj.Getsubcheckpoints(dtStatComp.Rows[i]["QuestionID"].ToString());
                if (dtSubEC.Rows.Count > 0)
                    for (int j = 0; j < dtSubStatComp.Rows.Count; j++)
                        (gvStatComp.Rows[i].Cells[0].FindControl("rblStatComp") as RadioButtonList).Items.Add(dtSubStatComp.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvStatComp.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblStatComp") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "all vouchers available")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "not applicable at all")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "stamped on all relevant docs")
                    rb.Items[r].Selected = true;
            }
        }
        foreach (GridViewRow Row in gvStatComp.Rows)
        {
            if (gvStatComp.Rows[Row.RowIndex].Cells[1].Text == "sale & payment vouchers")
                gvStatComp.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvStatComp.Rows[Row.RowIndex].Cells[1].Text == "transport documents")
                gvStatComp.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
        }
        #endregion
        #region LabourField
        gvLF.DataSource = dtLF.DefaultView;
        gvLF.DataBind();
        if (dtStatComp.Rows.Count > 0)
        {
            for (int i = 0; i < dtLF.Rows.Count; i++)
            {
                dtSubLF = farmerobj.Getsubcheckpoints(dtLF.Rows[i]["QuestionID"].ToString());
                if (dtSubLF.Rows.Count > 0)
                    for (int j = 0; j < dtSubLF.Rows.Count; j++)
                        (gvLF.Rows[i].Cells[0].FindControl("rblLabour") as RadioButtonList).Items.Add(dtSubLF.Rows[j]["SubText"].ToString());
            }
        }
        foreach (GridViewRow gvr in gvLF.Rows)
        {
            RadioButtonList rb = gvr.FindControl("rblLabour") as RadioButtonList;
            for (int r = 0; r < rb.Items.Count; r++)
            {
                if (rb.Items[r].Text == "No forced labor")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "not worked at all")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "no outside labor at all")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "holding no document")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "not observed")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Complies")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "observed stnds")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "all complies")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "not worked during night")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "good attendance in all seasons")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "paid timely")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "fully provided")
                    rb.Items[r].Selected = true;
                if (rb.Items[r].Text == "Not observed")
                    rb.Items[r].Selected = true;
            }
        }
        foreach (GridViewRow Row in gvLF.Rows)
        {
            if (gvLF.Rows[Row.RowIndex].Cells[1].Text == "outside labor ")
                gvLF.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
            if (gvLF.Rows[Row.RowIndex].Cells[1].Text == "number of outside workers during the PERIOD")
                gvLF.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF9966");
        }
        #endregion
    }
    private void GridEmpty()
    {

    }
    private void GeneratePDF(string Farmername)
    {
        string strpdf = "<table align='center' style='font-family:Verdana;font-size:7px;width:885px'>";
        strpdf += "<tr><td colspan='6' style='font-size:16px;' align='center'>Mudar India Exports<br />Internal Inspection Report</td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Inspection Details</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Name of the Inspector</td><td bgcolor='#FFFFCC'>" + lblInspectname.Text + "</td><td bgcolor='#CCFFCC'>Date of Inspection</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Period Details</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>From</td><td bgcolor='#FFFFCC'>" + lblPFromDate.Text + "</td><td bgcolor='#CCFFCC'>To&nbsp;&nbsp;</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1' ><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td width='60%' colspan='4'>Farmer Information</td><td width='40%' colspan='2'>Farm Details<br/>(all plots including non-organic plots)</td></tr>";
        strpdf += "<tr align='center'><td rowspan='2' bgcolor='#CCFFCC'>Farmer Name </td><td rowspan='2' bgcolor='#FFFFCC'>" + lblFarmername.Text + "</td><td bgcolor='#CCFFCC'>Farmer (mie) Code</td><td bgcolor='#FFFFCC'>" + lblMIE.Text + "</td><td bgcolor='#CCFFCC'>Totar Area in (Hc) </td><td bgcolor='#FFFFCC'>" + lblTotalArea.Text + "</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Farmer (tracenet) Ciode</td><td bgcolor='#FFFFCC'></td><td bgcolor='#CCFFCC'>Total Organic Area (Hc)</td><td bgcolor='#FFFFCC'>" + lblTotalArea.Text + "</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Village</td><td bgcolor='#FFFFCC'>" + lblVillage.Text + "</td><td bgcolor='#CCFFCC'>accompanied by</td><td bgcolor='#FFFFCC'>" + txtAccompanied.Text + "</td><td bgcolor='#CCFFCC'>Number of Organic Plots</td><td bgcolor='#FFFFCC'>" + lblNoofOrganicplots.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Seed Sowing start
        strpdf += "<tr align='center'><td colspan='6'>";
        strpdf += "<table width='100%' border='1'>";
        strpdf += "<tr  bgcolor='#CCCC99' style='font-size:9px;'><td colspan='4'>Farm Details - Plot-wise</td><td colspan='4'>Seeds & Sowing / Planting - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        strpdf += "<tr>";
        strpdf += "<td colspan='4'><table width='100%' style='font-size:7px;'>";
            foreach (GridViewRow gvr in gvFarmDeatils.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td></tr>";
            strpdf += "</table></td>";
            strpdf += "<td colspan='4'><table width='100%' style='font-size:7px;'>";
            foreach (GridViewRow gvr in gvSeedDetails.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td></tr>";
            strpdf += "</table></td>";
        strpdf +="</tr>";
        strpdf += "<tr align='center'><td colspan='1'></td><td colspan='1' bgcolor='#FFCC99'>" + lblTotalOrganic.Text + "</td><td colspan='6' bgcolor='#CCFFCC'>Total Farm in Area (Hc)</td></tr>";
        strpdf += "<tr align='center'><td colspan='6' bgcolor='#CCFFCC'>other crops / Vacant Area in Hc</td><td colspan='2' bgcolor='#FFCC99'>" + lblVacant.Text + "</td></tr>";
        strpdf += "</table>";
        strpdf += "</td></tr>";
        //Seed Sowing end
        strpdf += "<tr><td colspan='6'></td></tr>";
        // input start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1'><tr bgcolor='#CCCC99' style='font-size:9px;' align='center'>";
        strpdf += "<td colspan='4'>Farm Details - Plot-wise</td><td colspan='4'>Input Material - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Input<br/>Item</td><td>Qty in KG<br/>(Hc)</td><td>Source</td></tr>";
        foreach (GridViewRow gvr in gvFarmDeatils.Rows)
            foreach (GridViewRow gv in gvInput.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        // input end
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Diese Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Farm Details - Plot-wise</td><td colspan='6'>Plant Protection - Disese Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td>Date</td><td>Disease<br/>Expected</td><td>Protective/<br/>Preventive material used</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvFarmDeatils.Rows)
            foreach (GridViewRow gv in gvDiese.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + gv.Cells[4].Text + "</td><td align='center'>" + gv.Cells[5].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        // Diese End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Insecct start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Farm Details - Plot-wise</td><td colspan='6'>Plant Protection - Insect Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td>Date</td><td>Insect Expected/Observed</td><td>Protective/Preventivematerialused</td><td>Source</td><td>Bill details if purchased</td><td>Qty in KG / Hc</td></tr>";
        foreach (GridViewRow gvr in gvFarmDeatils.Rows)
            foreach (GridViewRow gv in gvInsect.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + gv.Cells[4].Text + "</td><td align='center'>" + gv.Cells[5].Text + "</td></tr>";
        strpdf += "</table></td></tr>";
        // Insect End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Yields Info
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Farm Details - Plot-wise</td><td colspan='8'>Yields - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td>Harvest<br/>Date</td><td>Estimated<br/>Herbage(MT)</td><td>Actual<br/>Yield(MT)</td><td>Distillation<br/>Date</td><td>Oil<br/>Yield(KG)</td><td>Batch<br/> No</td><td>SoldtoMIE<br/>(KG)</td><td>SoldOutSide<br/>(KG)</td></tr>";
        foreach (GridViewRow gvr in gvFarmDeatils.Rows)
            foreach (GridViewRow gv in gvYields.Rows)
                strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + gv.Cells[4].Text + "</td><td align='center'>" + gv.Cells[5].Text + "</td><td align='center'>" + gv.Cells[6].Text + "</td><td align='center'>" + gv.Cells[7].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        // Insect End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Yields End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Animal Husbandry Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Animal Husbandry</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvAnimal.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblAL") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtALRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Animal Husbandry End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Farm Management Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Farm Mangement</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvFarmMangement.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblFM") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtFMRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Farm Management End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Organic Compliance Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Organic Compliance</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvOrannicComp.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblOC") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtOCRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Organic Compliance End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Risk Management Compliance Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Risk Management Compliance</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvRMC.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblRMC") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtRMCRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Risk Management Compliance End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Farmer Awareness Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Farmer Awareness</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvFA.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblFA") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtFARemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Farmer Awareness End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Risk Processing Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Risk Processing</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvRP.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblRP") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtRPRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Risk Processing End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Safety Compliance Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Safety Compliance</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvSafetyComp.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblSafComp") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtSafCompRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        // Safety Compliance End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Environment Compliance Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Environment Compliance</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvEC.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblEc") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtECRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        //  Environment Compliance End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Statutory Compliance Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Statutory Compliance</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvStatComp.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblStatComp") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtStatCompRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        //  Statutory Compliance End
        strpdf += "<tr><td colspan='6'></td></tr>";
        // Labor in the Farms Start
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='3'>Labor in the Farms</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Question</td><td align='center'>Answer</td><td align='center'>Remarks</td></tr>";
        foreach (GridViewRow gvr in gvLF.Rows)
        {
            RadioButtonList rbl = gvr.FindControl("rblLabour") as RadioButtonList;
            string productlist = string.Empty;
            for (int count = 0; count < rbl.Items.Count; count++)
            {
                if (rbl.Items[count].Selected)
                    productlist += rbl.Items[count].Text + "  ";
            }
            strpdf += "<tr align='center' bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[1].Text + "<td>" + productlist + "</td><td>" + (gvr.FindControl("txtLabourRemarks") as TextBox).Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        //  Labor in the Farms End
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='3'><table width='100%' border='1' style='font-size:8px;'><tr align='center'><td colspan='2' bgcolor='#CCCC99' >Inspector Details</td></tr><tr><td bgcolor='FFFF66'>Name of the Inspector</td><td bgcolor='#FFFFCC'>" + lblInspectname.Text + "</td></tr><tr><td bgcolor='FFFF66'>Date of Inspection to Next Date</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + " to " + lblIDate.Text + "</td></tr></table></td><td colspan='3'><table width='100%' border='1' style='font-size:8px;'><tr align='center'><td colspan='2' bgcolor='#CCCC99'>Project Manger Details</td></tr><tr><td bgcolor='FFFF66'>Approved by</td><td bgcolor='#FFFFCC'>SukhDev singh</td></tr><tr><td bgcolor='FFFF66'>Designation</td><td bgcolor='#FFFFCC'>Project Manager</td></tr></table></td>";
        strpdf += "</td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr style='font-size:11px;'><td colspan='2' align='left' >Signature</td><td colspan='2'></td><td colspan='2' align='right'>Signature</td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr style='font-size:11px;'><td colspan='2' align='left' style='font-size:11px;'> Date</td><td colspan='2'></td><td colspan='2' align='right'>Date</td></tr>";
        strpdf += "</table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder("Inspection", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + "Inspection" + "/" + Farmername.ToString() + "_InternalInspection" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + "/FarmerInternalInspectionReport_" + 22 + ".pdf";
            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();
            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();


            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            ////add the collection to the document
            //for (int k = 0; k < htmlarraylist.Count; k++)
            //{
            //    document.Add((IElement)htmlarraylist[k]);
            //}

            //document.Add(new Paragraph("And the same with indentation...."));

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();

            //bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Cover_Letter);

            lbtnPdf.Visible = true;
            lbtnPdf.NavigateUrl = Pdf_path;
        }
        catch (Exception exx)
        {
            Response.Write("<br>____________________________________<br>");
            Response.Write("<br>Error: " + exx + "<br>");
            Response.Write("<br>StackTrace: " + exx.StackTrace + "<br>");
            Response.Write("<br>strPDFDocument: " + strpdf.ToString() + "<br>");
            Response.Write("<br>strSelectUserListBuilder: " + strpdf.ToString() + "<br>");

            //Console.Error.WriteLine(exx.StackTrace);
            //Console.Error.WriteLine(exx.StackTrace);
        }
        finally
        {
            //document.Close();
        }
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        GeneratePDF(lblMIE.Text);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Farmer/FarmerInspection.aspx");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GeneratePDF(lblMIE.Text);
    }
    protected void lbtnPdf_Click(object sender, EventArgs e)
    {
       
    }
    private void Getpath(string Path)
    {

    }
}
