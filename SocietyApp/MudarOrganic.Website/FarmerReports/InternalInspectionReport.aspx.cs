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

public partial class FarmerReports_InternalInspectionReport : System.Web.UI.Page
{
    Farmer_BL farmerobj = new Farmer_BL();
    DataTable dtCheckpoint = new DataTable();
    MudarUser mu = new MudarUser();
    Reports_BL reportObj = new Reports_BL();
    InspectionPlan_BL inobj = new InspectionPlan_BL();
    Farming_BL farmingObj = new Farming_BL();
    DataTable dtInspection = new DataTable();
    public static string farmerID, InspectionID, Farmercode, Insdate, InsName = "";
    DateTime PlantationDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            farmerID = Session["FarmerId"].ToString();
            InspectionID = Session["InspectionID"].ToString();
            Insdate = Session["InsDate"].ToString();
            InsName = Session["InsName"].ToString();
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
                GridAssign();
                BindPdflink();
            }
        }
    }
    private void BindPdflink()
    {
        DataTable dt = new DataTable();
        dt = inobj.GetReportPathDetails(InspectionID);
        if (dt.Rows.Count > 0)
        {
            trreport.Visible = true;
            lblSubmitDate.Text = dt.Rows[0]["Date"].ToString();
            lbtnPdf.NavigateUrl = dt.Rows[0]["Report_Path"].ToString();
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
            lblTrans.Text = dt.Rows[0]["FarmerAPEDACode"].ToString();
            lblTotalArea.Text = dt.Rows[0]["TotalAreaInHectares"].ToString();
            lblNoofOrganicplots.Text = dt.Rows[0]["NumberOfPlots"].ToString();
            lblTotalArea0.Text = dt.Rows[0]["TotalAreaInHectares"].ToString();
        }
        lblInspectname.Text = InsName;
        DateTime Date = Convert.ToDateTime(Insdate);
        lblPToDate.Text = string.Format("{0:dd MMM yyyy}", Date);
        lblIDate.Text = string.Format("{0:dd MMM yyyy}", Date);
        dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
        if (dtInspection.Rows.Count > 0)
        {
            if (dtInspection.Rows[0]["Result"].ToString() == string.Empty)
            {
                lblPFromDate.Text = "01" + " Jan" + " " + DateTime.Now.Year;
            }
            else
            {
                DateTime Plandate = Convert.ToDateTime(dtInspection.Rows[0]["PlanDate"]).AddDays(1);
                lblPFromDate.Text = string.Format("{0:dd MMM yyyy}", Plandate);
            }
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
        DateTime InsDate = Convert.ToDateTime(Insdate);
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
                string x = dt.Rows[i][7].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dt.Rows[i][7]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dt.Rows[i][7] = DBNull.Value;
                            dt.Rows[i][10] = DBNull.Value;
                            dt.Rows[i][11] = DBNull.Value;
                            dt.Rows[i][12] = DBNull.Value;
                        }
                    }
                    else
                    {
                        dt.Rows[i].Delete();
                        dt.AcceptChanges();
                        i = -1;
                    }
                }
            }
            // when inspection comes secod time
            dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
            if (dtInspection.Rows.Count > 0)
            {
                for (int i = 0; i < dtInspection.Rows.Count; i++)
                {
                    if (dtInspection.Rows[i]["Result"].ToString() == "True")
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (Convert.ToDateTime(dt.Rows[j]["PlantationDate"]) < Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                            {
                                dt.Rows[j].Delete();
                                dt.AcceptChanges();
                                j = -1;
                            }
                        }
                    }
                }
            }
            // end
            gvMainPlotDetails.DataSource = dt;
            gvMainPlotDetails.DataBind();
        }
        if (gvMainPlotDetails.Rows.Count > 0)
        {
            trTotFarm.Visible = true;
            trOthers.Visible = true;
        }
    }
    private void BindInputMaterialInfo()
    {
        DataTable dtFinput = inobj.GetPlotandInputinfo(farmerID, DateTime.Now.Year);
        DateTime InsDate = Convert.ToDateTime(Insdate);
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
                string x = dtFinput.Rows[i]["IDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dtFinput.Rows[i]["IDate"]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dtFinput.Rows[i][10] = DBNull.Value;
                            dtFinput.Rows[i][11] = DBNull.Value;
                            dtFinput.Rows[i][12] = DBNull.Value;
                            dtFinput.Rows[i][16] = DBNull.Value;
                        }
                    }
                    else
                    {
                        dtFinput.Rows[i].Delete();
                        dtFinput.AcceptChanges();
                        i = -1;
                    }
                }
            }
            //
            dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
            if (dtInspection.Rows.Count > 0)
            {
                for (int i = 0; i < dtInspection.Rows.Count; i++)
                {
                    if (dtInspection.Rows[i]["Result"].ToString() == "True")
                    {
                        for (int j = 0; j < dtFinput.Rows.Count; j++)
                        {
                            if (Convert.ToDateTime(dtFinput.Rows[j]["IDate"]) < Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                            {
                                dtFinput.Rows[j].Delete();
                                dtFinput.AcceptChanges();
                                j = -1;
                            }
                        }
                    }
                }
            }
            //
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (Convert.ToDateTime(dtFinput.Rows[j]["IDate"]) > InsDate)
                {
                    dtFinput.Rows[j].Delete();
                    dtFinput.AcceptChanges();
                    j = -1;
                }
            }
            gvInput.DataSource = dtFinput;
            gvInput.DataBind();
        }
    }
    private void BindDiesaseInfo()
    {
        DataTable dtFinput = inobj.GetplotandDiesinfo(farmerID, DateTime.Now.Year);
        DateTime InsDate = Convert.ToDateTime(Insdate);
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
                string x = dtFinput.Rows[i]["DMIDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dtFinput.Rows[i]["DMIDate"]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dtFinput.Rows[i][10] = DBNull.Value;
                            dtFinput.Rows[i][12] = DBNull.Value;
                            dtFinput.Rows[i][13] = DBNull.Value;
                            dtFinput.Rows[i][14] = DBNull.Value;
                            dtFinput.Rows[i][15] = DBNull.Value;
                            dtFinput.Rows[i][19] = DBNull.Value;

                        }
                    }
                    else
                    {
                        dtFinput.Rows[i].Delete();
                        dtFinput.AcceptChanges();
                        i = -1;
                    }
                }
            }
            //
            dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
            if (dtInspection.Rows.Count > 0)
            {
                for (int i = 0; i < dtInspection.Rows.Count; i++)
                {
                    if (dtInspection.Rows[i]["Result"].ToString() == "True")
                    {
                        for (int j = 0; j < dtFinput.Rows.Count; j++)
                        {
                            if (Convert.ToDateTime(dtFinput.Rows[j]["DMIDate"]) < Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                            {
                                dtFinput.Rows[j].Delete();
                                dtFinput.AcceptChanges();
                                j = -1;
                            }
                        }
                    }
                }
            }
            //
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (Convert.ToDateTime(dtFinput.Rows[j]["DMIDate"]) > InsDate)
                {
                    dtFinput.Rows[j].Delete();
                    dtFinput.AcceptChanges();
                    j = -1;
                }
            }
            gvDiseInfo.DataSource = dtFinput;
            gvDiseInfo.DataBind();
        }
    }
    private void BindInsectInfo()
    {
        DataTable dtFinput = inobj.GetplotandInsectinfo(farmerID, DateTime.Now.Year);
        DateTime InsDate = Convert.ToDateTime(Insdate);
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
        //
        dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
        if (dtInspection.Rows.Count > 0)
        {
            for (int i = 0; i < dtInspection.Rows.Count; i++)
            {
                if (dtInspection.Rows[i]["Result"].ToString() == "True")
                {
                    for (int j = 0; j < dtFinput.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(dtFinput.Rows[j]["IMIDate"]) > Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                        {
                            dtFinput.Rows[j].Delete();
                            dtFinput.AcceptChanges();
                            j = -1;
                        }
                    }
                }
            }
        }
        //
        if (dtFinput.Rows.Count > 0)
        {
            for (int i = 0; i < dtFinput.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtFinput.Rows[i][5].ToString()) == false)
                {
                    dtFinput.Rows[i]["IsInterCrop"] = "No";
                }
                string x = dtFinput.Rows[i]["IMIDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dtFinput.Rows[i]["IMIDate"]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dtFinput.Rows[i][10] = DBNull.Value;
                            dtFinput.Rows[i][12] = DBNull.Value;
                            dtFinput.Rows[i][13] = DBNull.Value;
                            dtFinput.Rows[i][14] = DBNull.Value;
                            dtFinput.Rows[i][15] = DBNull.Value;
                            dtFinput.Rows[i][19] = DBNull.Value;
                        }
                    }
                    else
                    {
                        dtFinput.Rows[i].Delete();
                        dtFinput.AcceptChanges();
                        i = -1;
                    }
                }
            }
            //
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (Convert.ToDateTime(dtFinput.Rows[j]["IMIDate"]) > InsDate)
                {
                    dtFinput.Rows[j].Delete();
                    dtFinput.AcceptChanges();
                    j = -1;
                }
            }

            gvInsect.DataSource = dtFinput;
            gvInsect.DataBind();
        }
    }
    private void BindPestInfo()
    {
        DataTable dtFinput = inobj.GetplotandPestinfo(farmerID, DateTime.Now.Year);
        DateTime InsDate = Convert.ToDateTime(Insdate);
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
                string x = dtFinput.Rows[i]["PMIDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dtFinput.Rows[i]["PMIDate"]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dtFinput.Rows[i][10] = DBNull.Value;
                            dtFinput.Rows[i][12] = DBNull.Value;
                            dtFinput.Rows[i][13] = DBNull.Value;
                            dtFinput.Rows[i][14] = DBNull.Value;
                            dtFinput.Rows[i][15] = DBNull.Value;
                            dtFinput.Rows[i][19] = DBNull.Value;
                        }
                    }
                    else
                    {
                        dtFinput.Rows[i].Delete();
                        dtFinput.AcceptChanges();
                    }
                }
            }
            //
            dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
            if (dtInspection.Rows.Count > 0)
            {
                for (int i = 0; i < dtInspection.Rows.Count; i++)
                {
                    if (dtInspection.Rows[i]["Result"].ToString() == "True")
                    {
                        for (int j = 0; j < dtFinput.Rows.Count; j++)
                        {
                            if (Convert.ToDateTime(dtFinput.Rows[j]["PMIDate"]) > Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                            {
                                dtFinput.Rows[j].Delete();
                                dtFinput.AcceptChanges();
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                if (Convert.ToDateTime(dtFinput.Rows[j]["PMIDate"]) > InsDate)
                {
                    dtFinput.Rows[j].Delete();
                    dtFinput.AcceptChanges();
                    j = -1;
                }
            }
            //
            gvPest.DataSource = dtFinput;
            gvPest.DataBind();
        }
    }
    private void BindWeedInfo()
    {
        DataTable dtFinput = inobj.GetplotandWeedInfo(farmerID, DateTime.Now.Year);
        DateTime InsDate = Convert.ToDateTime(Insdate);
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

                string x = dtFinput.Rows[i]["WMIDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    DateTime d = Convert.ToDateTime(dtFinput.Rows[i]["WMIDate"]);
                    if (d <= InsDate)
                    {
                        if (d > DateTime.Now.AddDays(-1))
                        {
                            dtFinput.Rows[i][10] = DBNull.Value;
                            dtFinput.Rows[i][12] = DBNull.Value;
                            dtFinput.Rows[i][13] = DBNull.Value;
                            dtFinput.Rows[i][14] = DBNull.Value;
                            dtFinput.Rows[i][15] = DBNull.Value;
                            dtFinput.Rows[i][19] = DBNull.Value;
                        }
                    }
                    else
                    {
                        dtFinput.Rows[i].Delete();
                        dtFinput.AcceptChanges();
                        i = -1;
                    }
                }
            }
            //
            dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
            if (dtInspection.Rows.Count > 0)
            {
                for (int i = 0; i < dtInspection.Rows.Count; i++)
                {
                    if (dtInspection.Rows[i]["Result"].ToString() == "True")
                    {
                        for (int j = 0; j < dtFinput.Rows.Count; j++)
                        {
                            if (Convert.ToDateTime(dtFinput.Rows[j]["WMIDate"]) > Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
                            {
                                dtFinput.Rows[j].Delete();
                                dtFinput.AcceptChanges();
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                string x = dtFinput.Rows[j]["WMIDate"].ToString();
                if (!string.IsNullOrEmpty(x))
                {
                    if (Convert.ToDateTime(dtFinput.Rows[j]["WMIDate"]) > InsDate)
                    {
                        dtFinput.Rows[j].Delete();
                        dtFinput.AcceptChanges();
                        j = -1;
                    }
                }
            }
            //
            gvWeed.DataSource = dtFinput;
            gvWeed.DataBind();
        }
    }
    private void BindYiledsInfo()
    {
        DateTime InsDate = Convert.ToDateTime(Insdate);
        DataTable dtYiled = new DataTable();
        DateTime d, sd;
        dtYiled.Columns.Add("AreaCode");
        dtYiled.Columns.Add("PlotArea");
        dtYiled.Columns.Add("Maincrop");
        dtYiled.Columns.Add("IsInterCrop");
        dtYiled.Columns.Add("FHD11");
        dtYiled.Columns.Add("EFH");//5
        dtYiled.Columns.Add("FH");
        dtYiled.Columns.Add("FDD");
        dtYiled.Columns.Add("FDU");
        dtYiled.Columns.Add("EFPQ");//9
        dtYiled.Columns.Add("FPQ");
        dtYiled.Columns.Add("SHD");
        dtYiled.Columns.Add("ESH");//12
        dtYiled.Columns.Add("SH");
        dtYiled.Columns.Add("SDD");
        dtYiled.Columns.Add("SDU");
        dtYiled.Columns.Add("ESPQ");//16
        dtYiled.Columns.Add("SPQ");
        dtYiled.Columns.Add("FarmerLotnumber");
        dtYiled.Columns.Add("PlantationDate");
        DataTable dtpDate = inobj.GetPlantationDate(farmerID);
        DataTable dtFinput = inobj.GetYiledDetails(farmerID, DateTime.Now.Year);
        DataTable dt = inobj.GetPlotandPlantinfo(farmerID, DateTime.Now.Year);
        dtFinput.Columns.Add("IsInterCrop");
        if (dtFinput.Rows.Count > 0)
        {
            for (int j = 0; j < dtFinput.Rows.Count; j++)
            {
                DataRow drNew = dtYiled.NewRow();
                drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
                drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
                drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();
                //drNew["IsInterCrop"] = dtFinput.Rows[j]["IsInterCrop"].ToString();
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
                PlantationDate = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString());
                drNew["PlantationDate"] = string.Format("{0:dd MMM yyyy}", PlantationDate);
                if (Convert.ToBoolean(dtFinput.Rows[j]["IIC"].ToString()) == false)
                {
                    //dtFinput.Rows[j]["IIC"] = "No";
                    drNew["IsInterCrop"] = "No";
                }
                dtYiled.Rows.Add(drNew);
            }
        }
        //dtInspection = inobj.GetInspectionByFarmerID(farmerID, DateTime.Now.Year.ToString());
        //if (dtInspection.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dtInspection.Rows.Count; i++)
        //    {
        //        if (dtInspection.Rows[i]["Result"].ToString() == "True")
        //        {
        //            for (int j = 0; j < dtYiled.Rows.Count; j++)
        //            {
        //                if (Convert.ToDateTime(dtYiled.Rows[j]["PlantationDate"]) < Convert.ToDateTime(dtInspection.Rows[i]["PlanDate"]))
        //                {
        //                    //dtYiled.Rows[j].Delete();
        //                    //dtYiled.AcceptChanges();
        //                    //j = -1;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}
        if (dtYiled.Rows.Count > 0)
        {
            for (int j = 0; j < dtYiled.Rows.Count; j++)
            {
                d = Convert.ToDateTime(dtYiled.Rows[j][7]);
                if (d > InsDate)
                {
                    dtYiled.Rows[j][4] = DBNull.Value;
                    dtYiled.Rows[j][6] = DBNull.Value;
                    dtYiled.Rows[j][7] = DBNull.Value;
                    dtYiled.Rows[j][8] = DBNull.Value;
                    dtYiled.Rows[j][10] = DBNull.Value;

                }
                sd = Convert.ToDateTime(dtYiled.Rows[j][14]);
                if (sd > InsDate)
                {
                    dtYiled.Rows[j][11] = DBNull.Value;
                    dtYiled.Rows[j][13] = DBNull.Value;
                    dtYiled.Rows[j][14] = DBNull.Value;
                    dtYiled.Rows[j][15] = DBNull.Value;
                    dtYiled.Rows[j][17] = DBNull.Value;
                    dtYiled.Rows[j][18] = DBNull.Value;
                }
            }

            if (dtInspection.Rows.Count > 0)
            {
                for (int j = 0; j < dtYiled.Rows.Count; j++)
                {
                    if (Convert.ToDateTime(dtYiled.Rows[j]["PlantationDate"]) <= InsDate)
                    {

                    }
                    else
                    {
                        dtYiled.Rows[j].Delete();
                        dtYiled.AcceptChanges();

                        j = -1;
                    }
                }
            }
            gvYields.DataSource = dtYiled;
            gvYields.DataBind();
        }
    }
    private void BindTotalProductinfo()
    {
        DateTime InsDate = Convert.ToDateTime(Insdate);
        DataTable dtYiled = new DataTable();
        dtYiled.Columns.Add("AreaCode");
        dtYiled.Columns.Add("PlotArea");
        dtYiled.Columns.Add("Maincrop");
        dtYiled.Columns.Add("FarmerLotnumber");
        dtYiled.Columns.Add("TotalProductOutput");
        dtYiled.Columns.Add("SoldTotalQty");
        dtYiled.Columns.Add("AvilQty");
        dtYiled.Columns.Add("FDD");
        dtYiled.Columns.Add("PlantationDate");
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
                if (FDD1 < InsDate)
                    drNew["TotalProductOutput"] = dtFinput.Rows[j]["FirstProductQuantity"].ToString();
                DateTime SDD1 = Convert.ToDateTime(dtFinput.Rows[j]["SecondDistillationDate"].ToString());
                if (SDD1 < InsDate)
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
                PlantationDate = Convert.ToDateTime(dtFinput.Rows[j]["PlantationDate"].ToString());
                drNew["PlantationDate"] = string.Format("{0:dd MMM yyyy}", PlantationDate);
                dtYiled.Rows.Add(drNew);
            }
            for (int j = 0; j < dtYiled.Rows.Count; j++)
            {
                if (Convert.ToDateTime(dtYiled.Rows[j]["PlantationDate"]) <= InsDate)
                {

                }
                else
                {
                    dtYiled.Rows[j].Delete();
                    j = -1;
                }
            }
        }
        gvTotal.DataSource = dtYiled;
        gvTotal.DataBind();
    }
    //private void BindTotalProductinfo()
    //{
    //    DateTime InsDate = Convert.ToDateTime(Insdate);
    //    DataTable dtYiled = new DataTable();
    //    dtYiled.Columns.Add("AreaCode");
    //    dtYiled.Columns.Add("PlotArea");
    //    dtYiled.Columns.Add("Maincrop");
    //    dtYiled.Columns.Add("FarmerLotnumber");
    //    dtYiled.Columns.Add("TotalProductOutput");
    //    dtYiled.Columns.Add("SoldTotalQty");
    //    dtYiled.Columns.Add("AvilQty");
    //    dtYiled.Columns.Add("FDD");
    //    DataTable dtpDate = inobj.GetPlantationDate(farmerID);
    //    DataTable dtFinput = inobj.GetYiledDetails(farmerID, DateTime.Now.Year);
    //    //dtFinput.Columns.Add("IsInterCrop");
    //    if (dtFinput.Rows.Count > 0)
    //    {
    //        for (int j = 0; j < dtFinput.Rows.Count; j++)
    //        {
    //            Decimal tot, sold;
    //            DataRow drNew = dtYiled.NewRow();
    //            drNew["AreaCode"] = dtFinput.Rows[j]["AreaCode"].ToString();
    //            drNew["PlotArea"] = dtFinput.Rows[j]["PlotArea"].ToString();
    //            drNew["Maincrop"] = dtFinput.Rows[j]["Maincrop"].ToString();
    //            DateTime FDD1 = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
    //            if (FDD1 < DateTime.Now.AddDays(-1))
    //                drNew["TotalProductOutput"] = dtFinput.Rows[j]["FirstProductQuantity"].ToString();
    //            DateTime SDD1 = Convert.ToDateTime(dtFinput.Rows[j]["SecondDistillationDate"].ToString());
    //            if (SDD1 < DateTime.Now.AddDays(-1))
    //                drNew["TotalProductOutput"] = (Convert.ToDecimal(dtFinput.Rows[j]["FirstProductQuantity"].ToString()) + Convert.ToDecimal(dtFinput.Rows[j]["SecondProductQuantity"].ToString())).ToString();
    //            drNew["SoldTotalQty"] = dtFinput.Rows[j]["SoldTotalQty"].ToString();
    //            if (!string.IsNullOrEmpty(drNew["TotalProductOutput"].ToString()))
    //            {
    //                tot = Convert.ToDecimal(drNew["TotalProductOutput"].ToString());
    //                drNew["FarmerLotnumber"] = dtFinput.Rows[j]["FarmerLotnumber"].ToString();
    //            }
    //            else
    //            {
    //                tot = 0;
    //                drNew["FarmerLotnumber"] = string.Empty;
    //            }
    //            if (!string.IsNullOrEmpty(drNew["SoldTotalQty"].ToString()))
    //                sold = Convert.ToDecimal(drNew["SoldTotalQty"].ToString());
    //            else
    //                sold = 0;
    //            if (!string.IsNullOrEmpty(drNew["TotalProductOutput"].ToString()))
    //                drNew["AvilQty"] = (tot - sold).ToString();
    //            //drNew["FarmerLotnumber"] = dtFinput.Rows[j]["FarmerLotnumber"].ToString();
    //            //drNew["TotalProductOutput"] = dtFinput.Rows[j]["TotalProductOutput"].ToString();
    //            //drNew["SoldTotalQty"] = dtFinput.Rows[j]["SoldTotalQty"].ToString();
    //            //drNew["AvilQty"] = dtFinput.Rows[j]["AvilQty"].ToString();
    //            DateTime FDD = Convert.ToDateTime(dtFinput.Rows[j]["FirstDistillationDate"].ToString());
    //            drNew["FDD"] = string.Format("{0:dd MMM yyyy}", FDD);
    //            //if (Convert.ToBoolean(dtFinput.Rows[j][2].ToString()) == false)
    //            //{
    //            //    dtFinput.Rows[j]["IsInterCrop"] = "No";
    //            //}
    //            dtYiled.Rows.Add(drNew);
    //        }
    //    }
    //    if (dtYiled.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dtYiled.Rows.Count; i++)
    //        {
    //            DateTime d = Convert.ToDateTime(dtYiled.Rows[i][7]);
    //            if (d <= InsDate)
    //            {
    //                if (d < DateTime.Now.AddDays(-1))
    //                {
    //                    dtYiled.Rows[i][3] = DBNull.Value;
    //                    dtYiled.Rows[i][4] = DBNull.Value;
    //                    dtYiled.Rows[i][5] = DBNull.Value;
    //                    dtYiled.Rows[i][6] = DBNull.Value;
    //                }
    //            }
    //            else
    //            {
    //                dtYiled.Rows[i][3] = DBNull.Value;
    //                dtYiled.Rows[i][4] = DBNull.Value;
    //                dtYiled.Rows[i][5] = DBNull.Value;
    //                dtYiled.Rows[i][6] = DBNull.Value;
    //                //dtYiled.Clear();
    //            }
    //        }
    //        gvTotal.DataSource = dtYiled;
    //        gvTotal.DataBind();
    //    }
    //}
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
        farmerobj.GetNewCheckpointQuestions(ref dtAnimal, ref  dtFarm, ref  dtOC, ref  dtRMC, ref  dtFA, ref  dtRP, ref  dtSCF, ref  dtEC, ref  dtStatComp, ref dtLF, farmerID, DateTime.Now.Year);
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
                gvRMC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
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
                gvFA.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvFA.Rows[Row.RowIndex].Cells[1].Text == "Internal FT standards")
                gvFA.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
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
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "Water Conservation")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "amount spent in alternative fuels")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvEC.Rows[Row.RowIndex].Cells[1].Text == "Cleanliness of the Farm")
                gvEC.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
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
                gvStatComp.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvStatComp.Rows[Row.RowIndex].Cells[1].Text == "transport documents")
                gvStatComp.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
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
                gvLF.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
            if (gvLF.Rows[Row.RowIndex].Cells[1].Text == "number of outside workers during the PERIOD")
                gvLF.Rows[Row.RowIndex].Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
        }
        #endregion
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        GetPDF(lblMIE.Text);
    }
    private void GetPDF(string Farmername)
    {
        DateTime Date = Convert.ToDateTime(DateTime.Now.AddHours(12).AddMinutes(30).ToShortDateString());
        lblSubmitDate.Text = string.Format("{0:dd MMM yyyy}", Date);
        string strpdf = "<table align='center' style='font-family:Verdana;font-size:7px;width:885px'>";
        strpdf += "<tr><td colspan='4'></td><td colspan='2' style='font-size:10px;' align='right'>Submit Date : " + lblSubmitDate.Text + "</td></tr>";
        strpdf += "<tr><td colspan='6' style='font-size:16px;' align='right'></td></tr>";
        strpdf += "<tr><td colspan='6' style='font-size:16px;' align='center'>Mudar India Exports<br />Internal Inspection Report</td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Inspection Details</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Name of the Inspector</td><td bgcolor='#FFFFCC'>" + lblInspectname.Text + "</td><td bgcolor='#CCFFCC'>Date of Inspection</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Period Details</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>From</td><td bgcolor='#FFFFCC'>" + lblPFromDate.Text + "</td><td bgcolor='#CCFFCC'>To&nbsp;&nbsp;</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1' ><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td width='60%' colspan='4'>Farmer Information</td><td width='40%' colspan='2'>Farm Details<br/>(all plots including non-organic plots)</td></tr>";
        strpdf += "<tr align='center'><td rowspan='2' bgcolor='#CCFFCC'>Farmer Name </td><td rowspan='2' bgcolor='#FFFFCC'>" + lblFarmername.Text + "</td><td bgcolor='#CCFFCC'>Farmer (mie) Code</td><td bgcolor='#FFFFCC'>" + lblMIE.Text + "</td><td bgcolor='#CCFFCC'>Totar Area in (HC) </td><td bgcolor='#FFFFCC'>" + lblTotalArea.Text + "</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Farmer (tracenet) Ciode</td><td bgcolor='#FFFFCC'>" + lblTrans.Text + "</td><td bgcolor='#CCFFCC'>Total Organic Area (Hc)</td><td bgcolor='#FFFFCC'>" + lblTotalArea.Text + "</td></tr>";
        strpdf += "<tr align='center'><td bgcolor='#CCFFCC'>Village</td><td bgcolor='#FFFFCC'>" + lblVillage.Text + "</td><td bgcolor='#CCFFCC'>accompanied by</td><td bgcolor='#FFFFCC'>" + txtAccompanied.Text + "</td><td bgcolor='#CCFFCC'>Number of Organic Plots</td><td bgcolor='#FFFFCC'>" + lblNoofOrganicplots.Text + "</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='8'>Seeds &amp; Sowing / Planting - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in MT <br/>(HC)</td></tr>";
        foreach (GridViewRow gvr in gvMainPlotDetails.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtPDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtPSource") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtPBillDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtPQty") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        if (gvMainPlotDetails.Rows.Count > 0)
        {
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td colspan='4'>" + lblTotalOrganic.Text + "</td><td colspan='4'>Total Farm Area in Hc </td></tr>";
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td colspan='4'>other crops / Vacant Area in Hc </td><td colspan='4'>" + lblVacant.Text + "</td></tr>";
        }
        strpdf += "</table></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='8'>Input- Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Input Item</td><td>source</td><td>Qty in MT <br/>(Hc)</td></tr>";
        foreach (GridViewRow gvr in gvInput.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtIdate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtIMMaterial") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtIMSource") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtIMQuantity") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Disese Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in MT <br/>(HC)</td></tr>";
        foreach (GridViewRow gvr in gvDiseInfo.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtDMIDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtDiseaseName") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtDMIPreventionMaterial") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtDMISource") as TextBox).Text + "</td><td>" + (gvr.Cells[8].FindControl("txtDMIBillNo") as TextBox).Text + "</td><td>" + (gvr.Cells[9].FindControl("txtDMIQuantity") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Insect Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in MT <br/>(HC)</td></tr>";
        foreach (GridViewRow gvr in gvInsect.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtIMIDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtInsectName") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtIMIPreventionMaterial") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtIMISource") as TextBox).Text + "</td><td>" + (gvr.Cells[8].FindControl("txtIMIBillNo") as TextBox).Text + "</td><td>" + (gvr.Cells[9].FindControl("txtIMIQuantity") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Pest Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in MT <br/>(HC)</td></tr>";
        foreach (GridViewRow gvr in gvPest.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtPMIDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtPestName") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtPMIPreventionMaterial") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtPMISource") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[8].FindControl("txtPMIBillNo") as TextBox).Text + "</td><td> " + (gvr.Cells[9].FindControl("txtPMIT_HC") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='10'>Plant Protection - Weed Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Date</td><td>Expected/Observed</td><td>Protective/Preventive materia</td><td>Source</td><td>Bill details <br/>if purchased</td><td>Qty in MT <br/>(HC)</td></tr>";
        foreach (GridViewRow gvr in gvWeed.Rows)
            strpdf += "<tr bgcolor='#FFFFCC' align='center'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtWMIDate") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtWeedName") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtWMIPreventionMaterial") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[7].FindControl("txtWMISource") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[8].FindControl("txtWMIBillNo") as TextBox).Text + "</td><td> " + (gvr.Cells[9].FindControl("txtWMIT_HC") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:8px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='19'>Yields - Information</td></tr>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='4'>Plot Details</td><td colspan='7'>First Harvest Details</td><td colspan='7'>Second Harvest Details</td><td>Batch</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot</td><td align='center'>Area<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>UC</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td align='center'>Har<br/>Dt</td><td>Est<br/>Herb</td><td>Ac<br/>Herb</td><td>Dist<br/>Dt</td><td>Dist<br/>UC</td><td>Est<br/>Oil</td><td>Ac<br/>Oil</td><td>Batch<br/>No</td></tr>";
        foreach (GridViewRow gv in gvYields.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td><td align='center'>" + (gv.Cells[4].FindControl("txtFHD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[5].FindControl("txtEFH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[6].FindControl("txtFH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[7].FindControl("txtFDD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[8].FindControl("txtFDU") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[9].FindControl("txtEFPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[10].FindControl("txtFPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[11].FindControl("txtSHD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[12].FindControl("txtESH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[13].FindControl("txtSH") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[14].FindControl("txtSDD") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[15].FindControl("txtSDU") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[16].FindControl("txtESPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[17].FindControl("txtSPQ") as TextBox).Text + "</td><td align='center'>" + (gv.Cells[18].FindControl("txtBN") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%' border='1' style='font-size:7px;'>";
        strpdf += "<tr bgcolor='#CCCC99' align='center' style='font-size:9px;'><td colspan='7'>Total Produce Quantity Information</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC' align='center'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Batch No</td><td align='center'>Produce <br/>Quantity</td><td>Sold to <br/>MIE</td><td>Available <br/> Qty</td></tr>";
        foreach (GridViewRow gvr in gvTotal.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + (gvr.Cells[4].FindControl("txtTotalProductOutput") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[5].FindControl("txtsoldtoMIE") as TextBox).Text + "</td><td align='center'>" + (gvr.Cells[6].FindControl("txtAvilQty") as TextBox).Text + "</td>";
        strpdf += "</tr>";
        strpdf += "</table></td></tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";

        strpdf += "<tr><td colspan='6'></td></tr>";
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
        strpdf += "</tr>";
        // start
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr><td colspan='3'><table width='100%' border='1' style='font-size:8px;'><tr align='center'><td colspan='2' bgcolor='#CCCC99' >Inspector Details</td></tr><tr><td bgcolor='#CCFFCC'>Name of the Inspector</td><td bgcolor='#FFFFCC'>" + lblInspectname.Text + "</td></tr><tr><td bgcolor='#CCFFCC'>Date of Inspection</td><td bgcolor='#FFFFCC'>" + lblIDate.Text + "   </td></tr></table></td><td colspan='3'><table width='100%' border='1' style='font-size:8px;'><tr align='center'><td colspan='2' bgcolor='#CCCC99'>Project Manger Details</td></tr><tr><td bgcolor='#CCFFCC'>Approved by</td><td bgcolor='#FFFFCC'>SukhDev singh</td></tr><tr><td bgcolor='#CCFFCC'>Designation</td><td bgcolor='#FFFFCC'>Project Manager</td></tr></table></td>";
        strpdf += "</td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='6'></td></tr>";
        strpdf += "<tr style='font-size:11px;'><td colspan='2' align='left' >Signature</td><td colspan='2'></td><td colspan='2' align='right'>Signature</td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr><td colspan='2' align='left'></td><td colspan='2'></td><td colspan='2' align='right'></td></tr>";
        strpdf += "<tr style='font-size:11px;'><td colspan='2' align='left' style='font-size:11px;'> Date</td><td colspan='2'></td><td colspan='2' align='right'>Date</td></tr>";
        //end
        strpdf += "</table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = "~/Attachments" + "/Farmer" + "/Inspection/" + Farmername.ToString() + "_IIR" + "_" + DateTime.Now.ToString("ddMMyy_hhmmss") + ".pdf";
            //string Pdf_path = System.IO.Path.Combine(Server.MapPath("~/Attachments/Farmer/Inspection"), fileName);
            //Pdf_path = mu.createfolder("Inspection", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + "Inspection" + "/" + Farmername.ToString() + "_InternalInspection" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + Farmername.ToString() + "_InternalInspection" + DateTime.Now.AddHours(12).AddMinutes(30).ToString() + ".pdf";
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
            trreport.Visible = true;
            //lbtnPdf.Visible = true;
            lbtnPdf.NavigateUrl = Pdf_path;
            //lblSubmitDate.Visible = true;
            inobj.InspectionSubmitDetails(Convert.ToInt32(InspectionID), "Bhanu", Pdf_path, 1);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmerInspection.aspx");
    }
}


