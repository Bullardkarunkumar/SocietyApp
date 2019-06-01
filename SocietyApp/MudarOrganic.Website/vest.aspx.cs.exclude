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

public partial class vest : System.Web.UI.Page
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
    public void BindgvCollecingDetails(int productID, string type)
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
        DataTable dt2 = new DataTable();
        //Bind Data to GridView
       // GridView1.Caption = Path.GetFileName(FilePath);
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
                string farmerID = string.Empty;
                string pid = string.Empty;
                string farm = string.Empty;
                string qty = string.Empty;
                string lot = string.Empty;
                dt1.Columns.Add("sss");
                DataRow drdt1 = dt1.NewRow();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // for the scripts.........
                    
                    farmerID += dt.Rows[i]["farmerID"].ToString() + ";";
                    farm += dt.Rows[i]["farm"].ToString() + ";";
                    lot += dt.Rows[i]["lot"].ToString() + ";";
                    qty += dt.Rows[i]["qty"].ToString() + ";";
                    pid += dt.Rows[i]["PID"].ToString() + ";";
                }
                cmd = new SqlCommand("insert into tblCollectionTransaction values(9,'"+Convert.ToInt32(dt.Rows[0]["p"].ToString())+"','" + farmerID + "',0,0,0,'" + farm + "','" + lot + "','" + qty + "','bhanu','" + Convert.ToDateTime(dt.Rows[0]["dt"].ToString()) + "',null,'" + Convert.ToDateTime(dt.Rows[0]["dt"].ToString()) + "',0,'" + pid + "',null,'@')", con);
                s = cmd.CommandText;
                drdt1["sss"] = s;
                dt1.Rows.Add(drdt1);
                //cmd.ExecuteNonQuery();
                //GridView1.Caption = Path.GetFileName(FilePath);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                dt2.Columns.Add("ssss");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow drdt2 = dt2.NewRow();
                    CMD1 = new SqlCommand("UPDATE tblPlantationDetails SET [SoldTotalQty] ='" + dt.Rows[j]["upd"].ToString() + "' ,[ModifiedDate]='" + Convert.ToDateTime(dt.Rows[0]["dt"].ToString()) + "' WHERE PlantationId='" + dt.Rows[j]["pid"].ToString() + "'", con);
                    CMD1.ExecuteNonQuery();
                    s = CMD1.CommandText;
                    drdt2["ssss"] = s;
                    dt2.Rows.Add(drdt2);    
                }
                //GridView2.Caption = Path.GetFileName(FilePath);
                GridView2.DataSource = dt2;
                GridView2.DataBind();
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

        // cryRpt.Load("D:\\Mudar Organic(My Code)\\MudarOrganic.Website\\NewFolder1\\rptGetGeneralInspection.rpt");
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
