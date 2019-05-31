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

public partial class FarmerReports_FarmerDiary : System.Web.UI.Page
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
            lblMIE0.Text = dt.Rows[0]["FarmerCode"].ToString();
            lblFarmername0.Text = dt.Rows[0]["FirstName"].ToString();
            lblTrans.Text = dt.Rows[0]["FarmerAPEDACode"].ToString();
            lblPeriodFrom.Text = "01" + " Jan" + " " + DateTime.Now.Year;
            DateTime date = DateTime.Now.AddHours(12).AddMinutes(30);
            lblPeriodTo.Text = string.Format("{0:dd MMM yyyy}", date.AddDays(-1));
        }
    }
    private void BindPlotandPlantinfo()
    {
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dt = inobj.GetPlotandPlantinfo(farmerID, DateTime.Now.Year);
        dt.Columns.Add("IsInterCrop");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i][5].ToString()) == false)
                {
                    dt.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
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
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
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
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
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
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
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
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
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
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
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
        dtFinput.Columns.Add("IsInterCrop");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                DataRow drNew = dtYiled.NewRow();
                drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
                drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
                drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();
                
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
                if (Convert.ToBoolean(dtFinput.Rows[j]["IIC"].ToString()) == false)
                {
                    //dtFinput.Rows[j]["IIC"] = "No";
                    drNew["IsInterCrop"] = "No";
                }
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
                    //dtYiled.Rows[i][14] = DBNull.Value;
                    dtYiled.Rows[i][15] = DBNull.Value;
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
        dtYiled.Columns.Add("FarmerLotnumber");
        dtYiled.Columns.Add("TotalProductOutput");
        dtYiled.Columns.Add("SoldTotalQty");
        dtYiled.Columns.Add("AvilQty");
        dtYiled.Columns.Add("FDD");
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dtFinput = inobj.GetYiledDetails(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("IsInterCrop");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                Decimal tot, sold;
                if (Convert.ToBoolean(dtFinput.Rows[j][2].ToString()) == false)
                {
                    dtFinput.Rows[j]["IsInterCrop"] = "No";
                }
                DataRow drNew = dtYiled.NewRow();
                drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
                drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
                drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();

                DateTime FDD1 = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
                if (FDD1 < DateTime.Now.AddDays(-1))
                    drNew["TotalProductOutput"] = dtFinput.Rows[j]["FirstProductQuantity"].ToString();
                DateTime SDD1 = Convert.ToDateTime(dtFinput.Rows[j]["SecondDistillationDate"].ToString());
                if (SDD1 < DateTime.Now.AddDays(-1))
                    drNew["TotalProductOutput"] = (Convert.ToDecimal(dtFinput.Rows[j]["FirstProductQuantity"].ToString()) + Convert.ToDecimal(dtFinput.Rows[j]["SecondProductQuantity"].ToString())).ToString();
                drNew["SoldTotalQty"] = dtFinput.Rows[j]["SoldTotalQty"].ToString();
                if (!string.IsNullOrEmpty(drNew["TotalProductOutput"].ToString()))
                {
                    tot = Convert.ToDecimal(drNew["TotalProductOutput"].ToString());
                    drNew["FarmerLotnumber"] = dtFinput.Rows[j]["FarmerLotnumber"].ToString();
                }
                else
                {
                    tot = 0;
                    drNew["FarmerLotnumber"] = string.Empty;
                }
                if (!string.IsNullOrEmpty(drNew["SoldTotalQty"].ToString()))
                    sold = Convert.ToDecimal(drNew["SoldTotalQty"].ToString());
                else
                    sold = 0;
                if (!string.IsNullOrEmpty(drNew["TotalProductOutput"].ToString()))
                    drNew["AvilQty"] = (tot - sold).ToString();
                DateTime FDD = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
                drNew["FDD"] = string.Format("{0:dd MMM yyyy}", FDD);
                dtYiled.Rows.Add(drNew);
            }
        }
        gvTotal.DataSource = dtYiled;
        gvTotal.DataBind();
    }
    
    private void PrintPDF(string FarmerCode)
    {
        string strpdf = "<table align='center' style='font-family:Verdana;font-size:7px;width:885px'>";
        strpdf += "<tr><td colspan='6' style='font-size:xx-large' align='center'>VGpal</td></tr>";
        strpdf += "<tr><td colspan='6' style='font-size:xx-large' align='center'>MUDAR INDIA EXPORTS</td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6' style='font-size:xx-large;'align='center'>Farmer Diary</td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr align='center' style='font-size:xx-large'><td bgcolor='#CCFFCC'>Farmer Code</td><td bgcolor='#FFFFCC'>" + lblMIE.Text + "</td></tr>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='2'>Farmer Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center' style='font-size:9px;'><td>Name of the Farmer</td><td bgcolor='#FFFFCC'>" + lblFarmername.Text + "</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center' style='font-size:9px;'><td>Tracenet Code</td><td bgcolor='#FFFFCC'>" + lblTrans.Text + "</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center' style='font-size:9px;'><td>Village</td><td bgcolor='#FFFFCC'>" + lblVillage.Text + "</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center' style='font-size:9px;'><td>Organic Area in Hc</td><td bgcolor='#FFFFCC'>" + lblTotalArea.Text + "</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center' style='font-size:9px;'><td>No of Plots</td><td bgcolor='#FFFFCC'>" + lblNoofOrganicplots.Text + "</td></tr>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='8'>Seeds &amp; Sowing / Planting - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvMainPlotDetails.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='8'>Input- Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvInput.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Disese Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvDiseInfo.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td><td align='center'>" + gvr.Cells[8].Text + "</td><td align='center'> " + gvr.Cells[9].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Insect Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvInsect.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td><td align='center'>" + gvr.Cells[8].Text + "</td><td> " + gvr.Cells[9].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Pest Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvPest.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td><td align='center'>" + gvr.Cells[8].Text + "</td><td> " + gvr.Cells[9].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Weed Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvWeed.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td><td align='center'>" + gvr.Cells[7].Text + "</td><td align='center'>" + gvr.Cells[8].Text + "</td><td> " + gvr.Cells[9].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:8px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='19'>Yields - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Plot Details</td><td colspan='7'>First Harvest Details</td><td colspan='7'>Second Harvest Details</td><td>Batch</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot</td><td align='center'>Area<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>Uc</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>UC</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td>B.N</td></tr>";
        foreach (GridViewRow gv in gvYields.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + gv.Cells[4].Text + "</td><td align='center'>" + gv.Cells[5].Text + "</td><td align='center'>" + gv.Cells[6].Text + "</td><td align='center'>" + gv.Cells[7].Text + "</td><td align='center'>" + gv.Cells[8].Text + "</td><td align='center'>" + gv.Cells[9].Text + "</td><td align='center'>" + gv.Cells[10].Text + "</td><td align='center'>" + gv.Cells[11].Text + "</td><td align='center'>" + gv.Cells[12].Text + "</td><td align='center'>" + gv.Cells[13].Text + "</td><td align='center'>" + gv.Cells[14].Text + "</td><td align='center'>" + gv.Cells[15].Text + "</td><td align='center'>" + gv.Cells[16].Text + "</td><td align='center'>" + gv.Cells[17].Text + "</td><td align='center'>" + gv.Cells[18].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='7'>Total Produce Quantity Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Batch No</td><td align='center'>Produce Quantity</td><td>Sold to MIE</td><td>Available Qty</td></tr>";
        foreach (GridViewRow gvr in gvTotal.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td><td align='center'>" + gvr.Cells[6].Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";
        strpdf += "</table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder("FarmerDiary", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + "FarmerDiary" + "/" + FarmerCode.ToString() + "_Diary" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + FarmerCode.ToString() + "_FarmerDiary" + ".pdf";
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
        }
        finally
        {
            //document.Close();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PrintPDF(lblMIE.Text);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmerInspection.aspx");
    }
}
