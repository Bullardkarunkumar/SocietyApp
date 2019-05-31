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

public partial class Farmer_FarmerCheckPoints : System.Web.UI.Page
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
                BindPlotandPlantinfo();
                BindInputMaterialInfo();
                BindDiesaseInfo();
                BindInsectInfo();
                BindYiledsInfo();
                BindPestInfo();
                BindWeedInfo();
                BindTotalProductinfo();
                BindFarmerDetails();
            }
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
            lblPFromDate.Text = "01 Jan 2014";

        }
        DataTable dtnext = new DataTable();
        if (dtnext.Rows.Count > 0)
        {
            //if (string.IsNullOrEmpty(dtnext.Rows[1]["PlanDate"].ToString()))
            //    lblNDate.Text = dtnext.Rows[0]["PlanDate"].ToString();
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
        }
    }
    private void BindPlotandPlantinfo()
    {
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dt = inobj.GetPlotandPlantinfo(farmerID, DateTime.Now.Year);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dt.Rows[i][7]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dt.Rows[i].Delete();
                }
            }
        }
        gvMainPlotDetails.DataSource = dt;
        gvMainPlotDetails.DataBind();
    }
    private void BindInputMaterialInfo()
    {
        DataTable dtFinput = inobj.GetPlotandInputinfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("IDate");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (dtFinput.Rows[j]["IMPlanting"].ToString() == "Planting")
                {
                    if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["IMPlanting"].ToString() == "1st Harvest")
                {
                    if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["IMPlanting"].ToString() == "2nd Harvest")
                {
                    if (dtFinput.Rows[j]["IMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["IMDays"].ToString()));
                        dtFinput.Rows[j]["IDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
            }
        }
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtFinput.Rows[i][16]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtFinput.Rows[i].Delete();
                }
            }
            gvInput.DataSource = dtFinput;
            gvInput.DataBind();
        }
    }
    private void BindDiesaseInfo()
    {
        DataTable dtFinput = inobj.GetplotandDiesinfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("DMIDate");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "Planting")
                {
                    if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "1st Harvest")
                {
                    if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["DMIT_Planting"].ToString() == "2nd Harvest")
                {
                    if (dtFinput.Rows[j]["DMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["DMIT_Days"].ToString()));
                        dtFinput.Rows[j]["DMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
            }
        }
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtFinput.Rows[i][19]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtFinput.Rows[i].Delete();
                }
            }
        }
        gvDiseInfo.DataSource = dtFinput;
        gvDiseInfo.DataBind();
    }
    private void BindInsectInfo()
    {
        DataTable dtFinput = inobj.GetplotandInsectinfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("IMIDate");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "Planting")
                {
                    if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "1st Harvest")
                {
                    if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["InsectMPlanting"].ToString() == "2nd Harvest")
                {
                    if (dtFinput.Rows[j]["InsectMPeriod"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["InsectMDays"].ToString()));
                        dtFinput.Rows[j]["IMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
            }
        }
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtFinput.Rows[i][19]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtFinput.Rows[i].Delete();
                }
            }
        }
        gvInsect.DataSource = dtFinput;
        gvInsect.DataBind();
    }
    private void BindPestInfo()
    {
        DataTable dtFinput = inobj.GetplotandPestinfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("PMIDate");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (dtFinput.Rows[j]["PMIT_Planting"].ToString() == "Planting")
                {
                    if (dtFinput.Rows[j]["PMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["PMIT_Planting"].ToString() == "1st Harvest")
                {
                    if (dtFinput.Rows[j]["PMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["PMIT_Planting"].ToString() == "2nd Harvest")
                {
                    if (dtFinput.Rows[j]["PMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["PMIT_Days"].ToString()));
                        dtFinput.Rows[j]["PMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
            }
        }
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtFinput.Rows[i][19]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtFinput.Rows[i].Delete();
                }
            }
        }
        gvPest.DataSource = dtFinput;
        gvPest.DataBind();
    }
    private void BindWeedInfo()
    {
        DataTable dtFinput = inobj.GetplotandWeedInfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("WMIDate");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (dtFinput.Rows[j]["WMIT_Planting"].ToString() == "Planting")
                {
                    if (dtFinput.Rows[j]["WMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["WMIT_Planting"].ToString() == "1st Harvest")
                {
                    if (dtFinput.Rows[j]["WMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
                if (dtFinput.Rows[j]["WMIT_Planting"].ToString() == "2nd Harvest")
                {
                    if (dtFinput.Rows[j]["WMIT_Period"].ToString() == "AFTER")
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                    else
                    {
                        DateTime date = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString()).AddDays(-Convert.ToInt32(dtFinput.Rows[j]["WMIT_Days"].ToString()));
                        dtFinput.Rows[j]["WMIDate"] = string.Format("{0:dd MMM yyyy}", date);
                    }
                }
            }
        }
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtFinput.Rows[i][19]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtFinput.Rows[i].Delete();
                }
            }
        }
        gvWeed.DataSource = dtFinput;
        gvWeed.DataBind();
    }
    private void BindYiledsInfo()
    {
        DataTable dtYiled = new DataTable();
        dtYiled.Columns.Add("AreaCode");
        dtYiled.Columns.Add("PlotArea");
        dtYiled.Columns.Add("Maincrop");
        dtYiled.Columns.Add("IsInterCrop");
        dtYiled.Columns.Add("FHD11");
        dtYiled.Columns.Add("EFH");
        dtYiled.Columns.Add("FH");
        dtYiled.Columns.Add("FDD");
        dtYiled.Columns.Add("EFPQ");
        dtYiled.Columns.Add("FPQ");
        dtYiled.Columns.Add("SHD");
        dtYiled.Columns.Add("ESH");
        dtYiled.Columns.Add("SH");
        dtYiled.Columns.Add("SDD");
        dtYiled.Columns.Add("ESPQ");
        dtYiled.Columns.Add("SPQ");
        dtYiled.Columns.Add("FDU");
        dtYiled.Columns.Add("SDU");
        dtYiled.Columns.Add("FarmerLotnumber");
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dtFinput = inobj.GetYiledDetails(farmerID, DateTime.Now.Year);
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                DataRow drNew = dtYiled.NewRow();
                drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
                drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
                drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();
                drNew["IsInterCrop"] = dtFinput.Rows[j]["IsInterCrop"].ToString();
                DateTime FHD = Convert.ToDateTime(dtFinput.Rows[j]["FirstHarvestDate"].ToString());
                drNew["FHD11"] = string.Format("{0:dd MMM yyyy}", FHD);
                drNew["EFH"] = dtFinput.Rows[j]["EstimationFHerbaga"].ToString();
                drNew["FH"] = dtFinput.Rows[j]["FirstHerbaga"].ToString();
                DateTime FDD = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
                drNew["FDD"] = string.Format("{0:dd MMM yyyy}", FDD);
                drNew["EFPQ"] = dtFinput.Rows[j]["EstimationFProductQuantity"].ToString();
                drNew["FPQ"] = dtFinput.Rows[j]["FirstProductQuantity"].ToString();
                DateTime SHD = Convert.ToDateTime(dtFinput.Rows[j]["SecondHarvestDate"].ToString());
                drNew["SHD"] = string.Format("{0:dd MMM yyyy}", SHD);
                drNew["ESH"] = dtFinput.Rows[j]["EstimationSHerbaga"].ToString();
                drNew["SH"] = dtFinput.Rows[j]["SecondHerbaga"].ToString();
                DateTime SDD = Convert.ToDateTime(dtFinput.Rows[j]["SecondDistillationDate"].ToString());
                drNew["SDD"] = string.Format("{0:dd MMM yyyy}", SDD);
                drNew["ESPQ"] = dtFinput.Rows[j]["EstimationSProductQuantity"].ToString();
                drNew["SPQ"] = dtFinput.Rows[j]["SecondProductQuantity"].ToString();
                drNew["FDU"] = dtFinput.Rows[j]["Ucode"].ToString();
                drNew["SDU"] = dtFinput.Rows[j]["Ucode"].ToString();
                drNew["FarmerLotnumber"] = dtFinput.Rows[j]["FarmerLotnumber"].ToString();
                dtYiled.Rows.Add(drNew);
            }
        }
        if (dtYiled.Rows.Count > 0)
        {
            for (int i = 0; i < dtYiled.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtYiled.Rows[i][7]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtYiled.Rows[i][4] = DBNull.Value;
                    dtYiled.Rows[i][6] = DBNull.Value;
                    dtYiled.Rows[i][7] = DBNull.Value;
                    dtYiled.Rows[i][9] = DBNull.Value;
                    dtYiled.Rows[i][16] = DBNull.Value;

                }
                DateTime sd = Convert.ToDateTime(dtYiled.Rows[i][13]);
                if (sd > DateTime.Now.AddDays(-1))
                {
                    dtYiled.Rows[i][10] = DBNull.Value;
                    dtYiled.Rows[i][12] = DBNull.Value;
                    dtYiled.Rows[i][13] = DBNull.Value;
                    dtYiled.Rows[i][14] = DBNull.Value;
                    dtYiled.Rows[i][17] = DBNull.Value;
                    dtYiled.Rows[i][18] = DBNull.Value;
                }
            }
        }
        gvYields.DataSource = dtYiled;
        gvYields.DataBind();
    }
    private void BindTotalProductinfo()
    {
        DataTable dtYiled = new DataTable();
        dtYiled.Columns.Add("AreaCode");
        dtYiled.Columns.Add("PlotArea");
        dtYiled.Columns.Add("Maincrop");
        dtYiled.Columns.Add("IsInterCrop");
        dtYiled.Columns.Add("TotalProductOutput");
        dtYiled.Columns.Add("SoldTotalQty");
        dtYiled.Columns.Add("AvilQty");
        dtYiled.Columns.Add("FDD");
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dtFinput = inobj.GetYiledDetails(farmerID, DateTime.Now.Year);
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                DataRow drNew = dtYiled.NewRow();
                drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
                drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
                drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();
                drNew["IsInterCrop"] = dtFinput.Rows[j]["IsInterCrop"].ToString();
                drNew["TotalProductOutput"] = dtFinput.Rows[j]["TotalProductOutput"].ToString();
                drNew["SoldTotalQty"] = dtFinput.Rows[j]["SoldTotalQty"].ToString();
                drNew["AvilQty"] = dtFinput.Rows[j]["AvilQty"].ToString();
                DateTime FDD = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
                drNew["FDD"] = string.Format("{0:dd MMM yyyy}", FDD);
                dtYiled.Rows.Add(drNew);
            }
        }

        if (dtYiled.Rows.Count > 0)
        {
            for (int i = 0; i < dtYiled.Rows.Count; i++)
            {
                DateTime d = Convert.ToDateTime(dtYiled.Rows[i][7]);
                if (d > DateTime.Now.AddDays(-1))
                {
                    dtYiled.Rows[i][4] = DBNull.Value;
                    dtYiled.Rows[i][5] = DBNull.Value;
                    dtYiled.Rows[i][6] = DBNull.Value;
                }
            }
        }
        gvTotal.DataSource = dtYiled;
        gvTotal.DataBind();
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
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        GetPDF(lblMIE.Text);
    }
    private void GetPDF(string Farmername)
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

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='8'>Seeds &amp; Sowing / Planting - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvMainPlotDetails.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:8px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='19'>Yields - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Plot Details</td><td colspan='7'>First Harvest Details</td><td colspan='7'>Second Harvest Details</td><td>Batch</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot</td><td align='center'>Area<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>Uc</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>UC</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td>B.N</td></tr>";
        foreach (GridViewRow gv in gvYields.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + (gv.Cells[4].FindControl("txtFHD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[5].FindControl("txtEFH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[6].FindControl("txtFH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[7].FindControl("txtFDD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[8].FindControl("txtFDU") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[9].FindControl("txtEFPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[10].FindControl("txtFPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[11].FindControl("txtSHD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[12].FindControl("txtESH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[13].FindControl("txtSH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[14].FindControl("txtSDD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[15].FindControl("txtSDU") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[16].FindControl("txtESPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[17].FindControl("txtSPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[18].FindControl("txtBN") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        strpdf += "</table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder("Inspection", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + "Inspection" + "/" + Farmername.ToString() + "_InternalInspection" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + Farmername.ToString() + "_InternalInspection" + ".pdf";
            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();
            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();
            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);
            Paragraph mypara = new Paragraph();
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();
            //lbtnPdf.Visible = true;
            //lbtnPdf.NavigateUrl = Pdf_path;
        }
        catch (Exception exx)
        {
            Response.Write("<br>____________________________________<br>");
            Response.Write("<br>Error: " + exx + "<br>");
            Response.Write("<br>StackTrace: " + exx.StackTrace + "<br>");
            Response.Write("<br>strPDFDocument: " + strpdf.ToString() + "<br>");
            Response.Write("<br>strSelectUserListBuilder: " + strpdf.ToString() + "<br>");
        }
        finally
        {
            //document.Close();
        }
    }


}
