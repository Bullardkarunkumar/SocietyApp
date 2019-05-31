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

using MudarOrganic.Components;
using System.Net;
using HtmlAgilityPack;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;

public partial class Test : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Order_BL orderObj = new Order_BL();
    Farmer_BL farobj = new Farmer_BL();
    Farming_BL farmingObj = new Farming_BL();
    Settings_BL settObj = new Settings_BL();
    MudarUser mu = new MudarUser();
    Reports_BL reportObj = new Reports_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        }
    }
    public  void BindgvCollecingDetails(int productID, string type)
    {
       
    }
    protected void cbCollecting_CheckedChanged1(object sender, EventArgs e)
    {
        
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }

        set
        {
            ViewState["dirState"] = value;
        }
    }
    protected void gvCollecingDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
       
    }
    public void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
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
        DataTable dt1 = new DataTable();
        
        //Bind Data to GridView
        GridView1.Caption = Path.GetFileName(FilePath);
        GridView1.DataSource = dt;
        GridView1.DataBind(); 
        try
        {
            if (dt.Rows.Count > 0)
            {

                string s = "";
                DateTime d;
                string sss = string.Empty;
                MudarDBHelper mdbh = MudarDBHelper.Instance;
                s = ConfigurationManager.ConnectionStrings["MudarConnectoinString"].ConnectionString;
                SqlConnection con;
                con = new SqlConnection(s);
                SqlCommand cmd, CMD1;
                con.Open();

                /* update farmer name details */
                dt1.Columns.Add("sss");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow drdt1 = dt1.NewRow();
                    cmd = new SqlCommand("UPDATE tblFarmerDetails SET [firstname] = '" + dt.Rows[i]["FarmerName"].ToString() + "',[fathername] = '" + dt.Rows[i]["FatherName"].ToString() + "',[farmerapedacode]='" + dt.Rows[i]["TracenetCode"].ToString() + "' WHERE farmerid='" + dt.Rows[i]["FarmerID"].ToString() + "'", con);
                    s = cmd.CommandText;
                    drdt1["sss"] = s;
                    dt1.Rows.Add(drdt1);
                }
                GridView1.Caption = Path.GetFileName(FilePath);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                //string LotsOfProducesSimultaneously = string.Empty;
                ////dt1.Columns.Add("pid");
                ////dt1.Columns.Add("sold");
                ////dt1.Columns.Add("sss");
                //string farmerID = string.Empty;
                //string pid = string.Empty;
                //string farm = string.Empty;
                //string qty = string.Empty;
                //string lot = string.Empty;
                //dt1.Columns.Add("sss");
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    // for the scripts.........
                //    DataRow drdt1 = dt1.NewRow();

                //    //string s1 = dt.Rows[i]["tot"].ToString();
                //    string s2 = dt.Rows[i]["Date"].ToString();
                //    ////cmd = new SqlCommand("select PlantationId,FarmerId,FarmID,FarmerLotnumber,TotalProductQuantity,SoldTotalQty,ProductId from tblPlantationDetails where plantationid='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    cmd = new SqlCommand("UPDATE tblPlantationDetails set [TotalProductQuantity] = TotalProductQuantity +'" + s2 + "' WHERE FarmID='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    //cmd = new SqlCommand("update tblBranchOrder set [BranchOrderDate] = '" + s2 + "' WHERE OrderID='" + dt.Rows[i]["ID"].ToString() + "'", con);
                //    //s = cmd.CommandText;
                //    //farmerID = dt.Rows[i]["farmerID"].ToString() + ";";
                //    //farm = dt.Rows[i]["farm"].ToString() + ";";
                //    //lot = dt.Rows[i]["lot"].ToString() + ";";
                //    //qty = dt.Rows[i]["qty"].ToString() + ";";
                //    //pid = dt.Rows[i]["PID"].ToString() + ";";
                //    //cmd = new SqlCommand("insert into tblCollectionTransaction values(17,4,'" + farmerID + "',0,0,0,'" + farm + "','" + lot + "','" + qty + "','bhanu','" + Convert.ToDateTime(dt.Rows[0]["dt"].ToString()) + "',null,'" + Convert.ToDateTime(dt.Rows[0]["dt"].ToString()) + "',0,'" + pid + "',null,'@')", con);
                //    //cmd = new SqlCommand("UPDATE tblOrderDetails [PurchaseOrderPath]='" + dt.Rows[i]["PO"].ToString() + "',[LSpath] = '" + dt.Rows[i]["LS"].ToString() + "' WHERE OrderID='" + dt.Rows[i]["OrderID"].ToString() + "'", con);
                //    s = cmd.CommandText;
                //    drdt1["sss"] = s;
                //    dt1.Rows.Add(drdt1);
                //    //cmd.ExecuteNonQuery();


                //    //cmd = new SqlCommand("insert into tblFarmerFarmDetails values('" + dt.Rows[i]["FarmerID"].ToString() + "','" + dt.Rows[i]["PlotArea"].ToString() + "','" + dt.Rows[i]["AreaCode"].ToString() + "',0,0,'bhanu','12/21/2014',null,'',0,0,0,'" + dt.Rows[i]["RowcountID"].ToString() + "',0,0,null)", con);
                //    //drdt1["pid"] = Aqty;
                //    //drdt1["sold"] = sold;

                //     //test end

                //   // string s1 = dt.Rows[i]["tot"].ToString();
                //    //d = Convert.ToDateTime(dt.Rows[i]["pd"].ToString());
                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [PlantationDate]='" + d.ToShortDateString() + "',[TotalProductQuantity]='" +s1 + "',[SoldTotalQty] = '" + s2 + "' WHERE FarmID='" + dt.Rows[i]["pid"].ToString() + "'", con);

                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [FirstLotNos] = '" + dt.Rows[i]["cut1"].ToString() + "',[SecondLotNos] = '" + dt.Rows[i]["cut2"].ToString() + "',[FarmerLotnumber]='" + dt.Rows[i]["FFL"].ToString() + "' WHERE PlantationId='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    //s = cmd.CommandText;
                //    //cmd = new SqlCommand("UPDATE tblFarmerFarmDetails SET [AreaCode] = '" + s2 + "' where FarmID='" + dt.Rows[i]["FarmID"].ToString() + "'", con);
                //    //update tblplantation for sold qty code start
                //    /* 
                //    string sold =  dt.Rows[i]["sold"].ToString();
                //    string Aqty =  dt.Rows[i]["pid"].ToString();
                //    cmd = new SqlCommand("UPDATE tblPlantationDetails SET [SoldTotalQty] = '" + dt.Rows[i]["sold"].ToString() + "' WHERE PlantationId='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    DataRow drdt1 = dt1.NewRow();
                //    s = cmd.CommandText;
                //    //drdt1["pid"] = Aqty;
                //    //drdt1["sold"] = sold;
                //    drdt1["sss"] = s;
                //    dt1.Rows.Add(drdt1); */
                //    //update tblplantation for sold qty code end
                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [FirstLotNos] = '" + dt.Rows[i]["cut1"].ToString() + "',[SecondLotNos] = '" + dt.Rows[i]["cut2"].ToString() + "' WHERE PlantationId='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [FirstLotNos] = '" + dt.Rows[i]["cut1"].ToString() + "',[SecondLotNos] = '" + dt.Rows[i]["cut2"].ToString() + "',[FarmerLotnumber] = '" + dt.Rows[i]["TOTAL"].ToString() + "' WHERE PlantationId='" + dt.Rows[i]["pid"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [PlantationDate] = '" + dt.Rows[i]["PlantationDate"].ToString() + "',[FirstHarvestDate]='" + dt.Rows[i]["FirstHarvestDate"].ToString() + "',[FirstHerbaga] = '" + dt.Rows[i]["FirstHerbaga"].ToString() + "',[FirstDistillationDate]='" + dt.Rows[i]["FirstDistillationDate"].ToString() + "',[FirstProductQuantity] = '" + dt.Rows[i]["FirstProductQuantity"].ToString() + "',[SecondHarvestDate]='" + dt.Rows[i]["SecondHarvestDate"].ToString() + "',[SecondHerbaga] = '" + dt.Rows[i]["SecondHerbaga"].ToString() + "',[SecondDistillationDate]='" + dt.Rows[i]["SecondDistillationDate"].ToString() + "',[SecondProductQuantity]='" + dt.Rows[i]["SecondProductQuantity"].ToString() + "',[TotalProductQuantity] = '" + dt.Rows[i]["TotalProductQuantity"].ToString() + "',[SecondLotNos]='" + dt.Rows[i]["SecondLotNos"].ToString() + "',[FirstLotNos] = '" + dt.Rows[i]["FirstLotNos"].ToString() + "',[FarmerLotnumber]='" + dt.Rows[i]["FarmerLotnumber"].ToString() + "',[EstimationFHerbaga] = '" + dt.Rows[i]["EstimationFHerbaga"].ToString() + "',[EstimationFProductQuantity]='" + dt.Rows[i]["EstimationFProductQuantity"].ToString() + "',[EstimationSHerbaga]='" + dt.Rows[i]["EstimationSHerbaga"].ToString() + "',[EstimationSProductQuantity]='" + dt.Rows[i]["EstimationSProductQuantity"].ToString() + "' WHERE PlantationId='" + dt.Rows[i]["PlantationId"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblBranchOrder SET [BranchOrderDate] = '" + dt.Rows[i]["BranchOrderDate"].ToString() + "',[CreatedDate]='" + dt.Rows[i]["CreatedDate"] + "',[FreightTerms]='" + dt.Rows[i]["FreightTerms"] + "',[Transport]='" + dt.Rows[i]["Transport"] + "',[DestinationCountry]='" + dt.Rows[i]["DestinationCountry"] + "',[DestinationPort]='" + dt.Rows[i]["DestinationPort"] + "',[PurchaseOrderID]='" + dt.Rows[i]["PurchaseOrderID"] + "' where OrderID='" + dt.Rows[i]["OrderID"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblBranchOrder SET [BranchOrderDate] = '" + dt.Rows[i]["BranchOrderDate"].ToString() + "',[CreatedDate]='" + dt.Rows[i]["CreatedDate"] + "' where OrderID='" + dt.Rows[i]["OrderID"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblFarmerFarmDetails SET [AreaCode] = '" + dt.Rows[i]["AC"].ToString() + "' where FarmID='" + dt.Rows[i]["FarmID"].ToString() + "'", con);
                //    //cmd = new SqlCommand("UPDATE tblFarmerDetails SET [NumberOfPlots] = '" + dt.Rows[i]["Noofplots"].ToString() + "',[TotalAreaInHectares]='" + dt.Rows[i]["Totalarea"].ToString() + "' WHERE FarmerId='" + dt.Rows[i]["RowcountID"].ToString() + "'", con);
                //    // cmd = new SqlCommand("insert into tblFarmerFarmDetails values('" + dt.Rows[i]["FarmerID"].ToString() + "','" + dt.Rows[i]["PlotArea"].ToString() + "','" + dt.Rows[i]["AreaCode"].ToString() + "',0,0,'bhanu','12/21/2014',null,'',0,0,0,'" + dt.Rows[i]["RowcountID"].ToString() + "',0,0,null)", con);
                //    //DataTable dtunit = new DataTable();
                //    //DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(2014,3, 1);
                //    //// First Cut Estimation Herbage Qty
                //    //decimal area = Convert.ToDecimal(dt.Rows[i]["PA"].ToString());
                //    //double  ss = 7.25;
                //    //int FEHQty = MudarApp.RandomNumber(14, 14);
                //    //// Total Herbage firstcut (estimated)
                //    //decimal FHerQty = Math.Round((area*Convert.ToDecimal(ss)),1);
                //    //// firstcut oil Estimation kgs
                //    //decimal FEoil = MudarApp.RandomNumber(5, 6);
                //    //// Total Oil firstcut(estimated)
                //    //decimal FOilEsti = Math.Round((FHerQty * FEoil), 1);
                //    ////  firstcut Estimation vs Actual Percentage 
                //    //decimal FEsvsAt = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                //    //// firstcut Actual Herbage 
                //    //decimal FTHqty = Math.Round((FHerQty * FEsvsAt), 1);
                //    //// firstcut oil kgs
                //    //decimal FTOil = Math.Round((FOilEsti * FEsvsAt), 1);

                //    //// Second Cut Estimation Herbage Qty
                //    //int SEHQty = MudarApp.RandomNumber(9, 9);
                //    ////decimal qq = 7.25;
                //    //// Total Herbage second cut (estimated)
                //    //decimal SHerQty = Math.Round((area * Convert.ToDecimal(ss)), 1);
                //    //// Second Cut oil Estimation kgs
                //    //decimal SEoil = MudarApp.RandomNumber(5, 6);
                //    //// Second Cut Total Oil (estimated) 
                //    //decimal SOilEsti = Math.Round((SHerQty * SEoil), 1);
                //    //// Second Cut Estimation vs Actual  Percentage
                //    //decimal SEsvsAt = (MudarApp.RandomNumber(Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_From"].ToString()), Convert.ToDecimal(dtBasicFInfo.Rows[0]["EsvsAc_To"].ToString()))) / 100;
                //    //// Second Cut Actual Herbage Percentage
                //    //decimal STHqty = Math.Round((SHerQty * SEsvsAt), 1);
                //    //// Second Cut oil kgs
                //    //decimal STOil = Math.Round((SOilEsti * SEsvsAt), 1);
                //    //cmd = new SqlCommand("UPDATE tblPlantationDetails SET [FirstHerbaga] = '" + FTHqty + "',[SecondHerbaga] = '" + STHqty + "',[EstimationFHerbaga] = '" + FHerQty + "',[EstimationSHerbaga] = '" + SHerQty + "',[EstimationFProductQuantity] = '" + FOilEsti + "',[EstimationSProductQuantity] = '" + SOilEsti + "',[FirstProductQuantity] = '" + FTOil + "',[SecondProductQuantity] = '" + STOil + "',[PlantationArea]= '" + dt.Rows[i]["PA"].ToString() + "',[ModifiedBy] ='Bhanu' WHERE FarmID='" + dt.Rows[i]["FarmID"].ToString() + "'", con);
                //    //cmd.ExecuteNonQuery();
                //    //Label1.Text = "Information submitted successfully";
                //    //#region plantaion
                //    ////DataTable dt1 = new DataTable();
                //    ////DataTable dt3 = new DataTable();
                //    ////DataTable dt4 = new DataTable();
                //    ////dt1 = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductId='" + dt.Rows[i]["ProductID"].ToString() + "'");
                //    ////dt3 = mdbh.ExecuteDataTable("select AreaCode from tblFarmerFarmDetails where FarmID='" + dt.Rows[i]["frid"].ToString() + "' and farmerid ='" + dt.Rows[i]["FarmerID"].ToString() + "' and [delete]=0");
                //    ////string lot = dt1.Rows[0]["ProductCode"].ToString() + "13" + dt3.Rows[0]["AreaCode"].ToString();
                //    ////int FID = Convert.ToInt32(dt.Rows[i]["PID"].ToString());
                //    ////int dd = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                //    ////decimal ps = Convert.ToDecimal(dt.Rows[i]["pa"].ToString());
                //    ////int FDNO = 0;
                //    ////int fpco = 1;
                //    ////string sss = null;
                //    ////bool c = false;
                //    ////DateTime date = DateTime.Now;
                //    ////dt4 = mdbh.ExecuteDataTable("select FarmerCode from tblFarmerDetails where FarmerId='" + dt.Rows[i]["FarmerID"].ToString() + "'");
                //    ////string FirstLotNos = dt1.Rows[0]["ProductCode"].ToString() + "C1" + dt4.Rows[0]["FarmerCode"].ToString();
                //    ////string SecondLotNos = dt1.Rows[0]["ProductCode"].ToString() + "C2" + dt4.Rows[0]["FarmerCode"].ToString();
                //    ////cmd = new SqlCommand("insert into tblPlantationDetails values('" + dt.Rows[i]["FarmerID"].ToString() + "','" + dd + "','" + FID + "','" + Convert.ToDateTime(dt.Rows[i]["FPD"].ToString()) + "','" + Convert.ToDateTime(dt.Rows[i]["FHD"].ToString()) + "','" + Convert.ToDecimal(dt.Rows[i]["FAH"].ToString()) + "','" + Convert.ToDateTime(dt.Rows[i]["FDD"].ToString()) + "','" + FDNO + "','" + Convert.ToDecimal(dt.Rows[i]["FPQ"].ToString()) + "','" + fpco + "','" + Convert.ToDateTime(dt.Rows[i]["SHD"].ToString()) + "','" + Convert.ToDecimal(dt.Rows[i]["SAH"].ToString()) + "','" + Convert.ToDateTime(dt.Rows[i]["SDD"].ToString()) + "','" + FDNO + "','" + Convert.ToDecimal(dt.Rows[i]["SPQ"].ToString()) + "','" + fpco + "','" + Convert.ToDecimal(dt.Rows[i]["TPQ"].ToString()) + "','" + sss + "','" + sss + "','" + sss + "','" + date + "','" + sss + "','" + date + "','" + c + "','" + fpco + "','" + ps + "','" + dt.Rows[i]["FU"].ToString() + "','" + FDNO + "','" + dt.Rows[i]["SU"].ToString() + "','" + SecondLotNos + "','" + FDNO + "','" + FirstLotNos + "','" + FDNO + "','" + lot + "','" + Convert.ToDecimal(dt.Rows[i]["EFH"].ToString()) + "','" + Convert.ToDecimal(dt.Rows[i]["EFP"].ToString()) + "','" + Convert.ToDecimal(dt.Rows[i]["ESH"].ToString()) + "','" + Convert.ToDecimal(dt.Rows[i]["ESP"].ToString()) + "')", con);
                //    //#endregion
                //    //drdt1["sss"] = s;
                //    //dt1.Rows.Add(drdt1);
                //    //cmd.ExecuteNonQuery();
                //}
                //GridView1.Caption = Path.GetFileName(FilePath);
                //GridView1.DataSource = dt1;
                //GridView1.DataBind();
                Label1.Text = "Information submitted successfully"; 
            }
            Response.Write("<script>alert('Saved')</script>");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Test.aspx" + ex.Message);
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
         //DataTable dtOrderCollect = orderObj.CollectedProductDetailsBasedonProduct(1,"2013",txtFN.Text);
         //if (dtOrderCollect.Rows.Count > 0)
         //{
       
         //}

        InspectionPlan_BL bl = new InspectionPlan_BL();

       
            ReportDocument cryRpt = new ReportDocument();
            //SetConnectionInfo(cryRpt);
            cryRpt.Load(MapPath(ResolveUrl("~/NewFolder1/rptGetGeneralInspection.rpt")));
            //cryRpt.Load("D:\\Mudar Organic(My Code)\\MudarOrganic.Website\\NewFolder1\\rptGetGeneralInspection.rpt");

            ds = bl.GetGeneralInspectionDetails();
         

            SetConnectionInfo(cryRpt);
            cryRpt.SetDataSource(ds);
            MemoryStream ostream = (MemoryStream)(cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=Report.pdf");
            Response.BinaryWrite(ostream.ToArray());
            ostream.Close();
            ostream.Dispose();
            cryRpt.Close();
            cryRpt.Dispose();
            Response.End();

            //cryRpt.Load("D:\\Mudar Organic(My Code)\\MudarOrganic.Website\\NewFolder1\\rptGetGeneralInspection.rpt");
            //CrystalReportViewer1.ReportSource = cryRpt;
            //CrystalReportViewer1.RefreshReport();
        
    }
    protected void gvCollecingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void gvCollecingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    private void SetConnectionInfo(ReportDocument ReportDoc)
    {

        CrystalDecisions.Shared.TableLogOnInfo crLogOnInfo;
        crLogOnInfo = ReportDoc.Database.Tables[0].LogOnInfo;
        crLogOnInfo.ConnectionInfo.ServerName = "BHANU-PC";
        crLogOnInfo.ConnectionInfo.UserID = "sa";
        crLogOnInfo.ConnectionInfo.Password = "sql";
        crLogOnInfo.ConnectionInfo.DatabaseName = "Mudar";
        ReportDoc.Database.Tables[0].ApplyLogOnInfo(crLogOnInfo);
    }
}
