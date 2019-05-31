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
using MudarOrganic.BL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Web.Configuration;


public partial class Test_page : System.Web.UI.Page
{
    Farmer_BL farmerobj = new Farmer_BL();
    bool result = false;
    MudarUser mu = new MudarUser();
    DataTable dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvFarmerDetails.DataSource = dt;
            //gvFarmerDetails.DataBind();
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

        //Bind Data to GridView
        GridView1.Caption = Path.GetFileName(FilePath);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        try
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count-1; i++)
                {


                    /////*  Farmer inserted code */
                    ////MudarApp farmercode = new MudarApp();
                    //////string FarmerCode = farmercode.farmercode(dt.Rows[i]["Village"].ToString(), dt.Rows[i]["State"].ToString());

                    ////string Farmerid = Guid.NewGuid().ToString();
                    ////result = farmerobj.Farmer_INSandUPTandDEL(Farmerid, dt.Rows[i]["CODE"].ToString(), dt.Rows[i]["FarmerName"].ToString(), string.Empty, string.Empty, Convert.ToDecimal(dt.Rows[i]["TotalArea"].ToString()), Convert.ToInt32(dt.Rows[i]["TotalPlots"].ToString()), dt.Rows[i]["FatherName"].ToString(), string.Empty, dt.Rows[i]["Village"].ToString(), dt.Rows[i]["Taulk"].ToString(), dt.Rows[i]["Dist"].ToString(), dt.Rows[i]["State"].ToString(), "INDIA", string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, true, true, "Raghu", string.Empty, string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Insert, DateTime.Now, string.Empty, false, false, "ICS04");
                    /////*  plot inserted inserted code */
                    ////int nFarmID = 0;
                    ////string[] plot = dt.Rows[i]["PlotArea"].ToString().Split(';');
                    ////for (int k = 0; k < plot.Length; k++)
                    ////{
                    ////    farmerobj.Farm_INSandUPTandDEL(Farmerid, 0, Convert.ToDecimal(plot[k].ToString()), string.Empty, 0, 0, "Aslam", string.Empty, MudarApp.Insert, ref nFarmID, 0);
                    ////}
                    /////* product details inserted*/
                    ////// After the all farmers inserted.insert the products of the Farmers
                    //////string[] product = dt.Rows[i]["product"].ToString().Split(';');
                    //////for (int j = 0; j < product.Length - 1; j++)
                    //////{
                    //////    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(9, dt.Rows[i]["FarmerID"].ToString(), "Zaid", string.Empty, true, "Bhanu", string.Empty, DateTime.Now, DateTime.Now, 2016, Convert.ToInt32(product[j].ToString()), 2);
                    //////}
                   

                    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(18, dt.Rows[i]["CODE"].ToString(), "Zaid", string.Empty, true, "Bhanu", string.Empty, DateTime.Now, DateTime.Now, 2019, 1, 2);
                    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(18, dt.Rows[i]["CODE"].ToString(), "Zaid", string.Empty, true, "Bhanu", string.Empty, DateTime.Now, DateTime.Now, 2019, 2, 2);
                    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(18, dt.Rows[i]["CODE"].ToString(), "Zaid", string.Empty, true, "Bhanu", string.Empty, DateTime.Now, DateTime.Now, 2019, 3, 2);
                    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(19, dt.Rows[i]["CODE"].ToString(), "Kharif", string.Empty, true, "Bhanu", string.Empty, DateTime.Now, DateTime.Now, 2019, 8, 2);
                }
            }
            Response.Write("<script>alert('Saved Successfully')</script>");
          
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Test page.aspx" + ex.Message);
        }
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
    }
}
