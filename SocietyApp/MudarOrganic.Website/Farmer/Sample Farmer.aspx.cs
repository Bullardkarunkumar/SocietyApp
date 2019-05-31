using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Collections;
using System.Net;
using System.Data;
using MudarOrganic.BL;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Web.Configuration;

public partial class Farmer_Sample_Farmer : System.Web.UI.Page
{
    Farmer_BL farmerObj = new Farmer_BL();
    UnitInformation_BL UI = new UnitInformation_BL();
    MudarUser mu = new MudarUser();
    public static string code = string.Empty;
    public static string SortExpression_p = "FarmerCode";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            divSearch.Visible = false;
            Master.MasterControlbtnFarmerInfo();
            gvFarmer.AllowPaging = true;
            gvFarmer.AllowSorting = true;
            BindFarmerList();
        }
    }
    private void BindFarmerList()
    {
        try
        {
            DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];

            string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
            DataTable dt = new DataTable();
            dt = farmerObj.FamerDetailsByIcs(icsCode);
            lblFarmers.Text = dt.Rows.Count.ToString();
            if (dt.Rows.Count > 0)
            {
                double TotalArea = 0.000;
                int TotalPlots = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TotalArea = Convert.ToDouble(dt.Rows[i]["TotalAreaInHectares"].ToString()) + TotalArea;
                    TotalPlots = Convert.ToInt32(dt.Rows[i]["NumberOfPlots"].ToString()) + TotalPlots;
                }
                lblTotalArea.Text = TotalArea.ToString();
                lblNoofPlots.Text = TotalPlots.ToString();
            }
            Session["FarmerDetails"] = null;
            Session["FarmerDetails"] = dt;
            gvFarmer.DataSource = (DataTable)Session["FarmerDetails"];
            gvFarmer.DataBind();
            SortingFarmerCode(SortExpression_p);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void gvFarmer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "FarmerCode" && e.CommandArgument.ToString() != "FirstName" && e.CommandArgument.ToString() != "City_Village" && e.CommandArgument.ToString() != "TotalAreaInHectares" && e.CommandArgument.ToString() != "Organic")
            index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "Farmer":
                {
                    gvFarmer.DataKeys[index].Values[1].ToString();
                    code = gvFarmer.DataKeys[index].Values[1].ToString();//gvFarmer.Rows[index].Cells[0].Text;
                    Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=2&FarmerCode=" + code);
                }
                break;
            case "FarmerCode":
                {
                    code = gvFarmer.DataKeys[index].Values[1].ToString(); //gvFarmer.Rows[index].Cells[0].Text;
                    Response.Redirect("~/Farmer/Farmerview.aspx?FarmerCode=" + code + "&BackUrl=~/Farmer/Sample Farmer.aspx");
                }
                break;
        }
    }
    protected void gvFarmer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[4].Text.ToUpper().Trim() == "FALSE")
            {
                e.Row.Cells[4].Text = "Org & FT";
            }
            else
                e.Row.Cells[4].Text = "Org";
        }
    }
    protected void gvFarpdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[2].Text == "Bastara" || e.Row.Cells[2].Text == "shahbazpur" || e.Row.Cells[2].Text == "Kurau")
            {
                if (e.Row.Cells[4].Text.ToUpper().Trim() == "FALSE")
                {
                    e.Row.Cells[4].Text = "Org & FT";
                }
                else
                    e.Row.Cells[4].Text = "Org";
            }
            else
                e.Row.Cells[4].Text = "Org";

            if (e.Row.Cells[5].Text.ToUpper().Trim() == "TRUE")
            {
                e.Row.Cells[5].Text = "Approved";
            }
            else
                e.Row.Cells[5].Text = " ";
        }
    }
    protected void btnCondensed_Click(object sender, EventArgs e)
    {
        BindFarmerList();
    }
    protected void btnDetailed_Click(object sender, EventArgs e)
    {
        BindDetailedFarmerList();
    }
    private void BindDetailedFarmerList()
    {
        DataTable dt = farmerObj.FamerDetailDetails();
        Session["FarmerDetails"] = null;
        Session["FarmerDetails"] = dt;
        gvFarmer.DataSource = (DataTable)Session["FarmerDetails"];
        gvFarmer.DataBind();
        SortingFarmerCode(SortExpression_p);
    }
    protected void btnSearchFarmer_Click(object sender, EventArgs e)
    {
        divSearch.Visible = true;
        divgvFarmerDetails.Visible = false;
        divPDF.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string[] str = txtSFarmerName.Text.Split('-');
        if (str.Length >= 2)
        {
            code = str[1].ToString();
            Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=2&FarmerCode=" + code);
        }
        else
            Response.Write("<script>alert('!!!! plz select the farmer from genearted list above textbox !!!! ');</script>");
    }
    protected void btnAddFarmer_Click(object sender, EventArgs e)
    {

    }
    protected void gvFarmer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFarmer.PageIndex = e.NewPageIndex;
        SortingFarmerCode(SortExpression_p);
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
    protected void gvFarmer_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_p = e.SortExpression.ToString();
        SortingFarmerCode(SortExpression_p);
    }
    private void SortingFarmerCode(string SortExpression)
    {
        DataTable dt = (DataTable)Session["FarmerDetails"];
        Session["FarmerDetails"] = dt;
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        else
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + sortingDirection;
        gvFarmer.DataSource = sortedView;
        gvFarmer.DataBind();
    }

    private void BindVillageslist()
    {
        ddlVillage.DataSource = UI.FarmersVillageList();
        ddlVillage.DataTextField = "City_Village";
        ddlVillage.DataValueField = "City_Village";
        ddlVillage.DataBind();
        ddlVillage.Items.Insert(0, MudarApp.AddListItem());
        ddlVillage.Items.Add("All");
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        double TotArea = 0.000;
        int TotPlots = 0;
        DataTable dt = farmerObj.FarmerDetails(ddlVillage.SelectedItem.Text);
        string vill = ddlVillage.SelectedItem.Text;
        if (dt.Rows.Count > 0)
        {
            lbtnCon.Visible = true;
            lbtnDet.Visible = true;
            trFarmer.Visible = true;
            gvFarpdf.DataSource = dt;
            gvFarpdf.DataBind();
            btnpd.Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TotArea = Convert.ToDouble(dt.Rows[i]["TotalAreaInHectares"].ToString()) + TotArea;
                TotPlots = Convert.ToInt32(dt.Rows[i]["NumberOfPlots"].ToString()) + TotPlots;
            }
            lblTotArea.Text = TotArea.ToString();
            lblTotArea.Text = Math.Round(TotArea, 3).ToString();
            lblTotFarmer.Text = dt.Rows.Count.ToString();
            lbtnPdf.Visible = false;
            lbtnCon.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
            lbtnDet.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
        }
    }
    private void GeneratePDF(string Village)
    {
        string strpdf = "<table align='center' style='font-family:Verdana;font-size:9px;width:885px'><tr>";
        strpdf += "<td colspan='6'><table border='1' width='100%' align='center'><tr align='center' bgcolor='#CCCC99' style='font-size:12px;'><td colspan='6'>Mudar India Exports</td></tr><tr align='center' bgcolor='#CCCC99'><td colspan='6'>Farmer Information Village Wise</td></tr>";
        strpdf += "<tr bgcolor='#FFCC99'><td colspan='3' align='center'>Total Farmers<br/>" + lblTotFarmer.Text + "</td><td colspan='3' align='center'>Total Organic Area<br/>" + lblTotArea.Text + "</td></tr>";
        strpdf += "<tr bgcolor='#CCFFCC'><td align='center'>Farmer Code</td><td align='center'>Farmer Name</td><td align='center'>Village</td><td align='center'>Total Area in (Hc)</td><td align='center'>Plot Status</td><td align='center'>Status</td></tr>";
        foreach (GridViewRow gvr in gvFarpdf.Rows)
            strpdf += "<tr bgcolor='#FFFFCC'><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td><td align='center'>" + gvr.Cells[4].Text + "</td><td align='center'>" + gvr.Cells[5].Text + "</td></tr>";
        strpdf += "</table></td></tr>";
        strpdf += "</table>";
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder("village", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + "Village" + "/" + "Farmer List of " + Village.ToString() + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + "/" + Village.ToString() + "_" + ".pdf";
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();
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

        }
    }
    protected void btnpd_Click(object sender, EventArgs e)
    {
        GeneratePDF(ddlVillage.SelectedItem.Text);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/Sample Farmer.aspx");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        divPDF.Visible = true;
        divgvFarmerDetails.Visible = false;
        divSearch.Visible = false;
        SelectDiv.Visible = false;
        btnpd.Visible = false;
        BindVillageslist();
    }
    protected void lbtnCon_Click(object sender, EventArgs e)
    {
        DataTable dt = farmerObj.FarmerDetails(ddlVillage.SelectedItem.Text);
        string vill = ddlVillage.SelectedItem.Text;
        if (dt.Rows.Count > 0)
        {
            lbtnCon.ForeColor = System.Drawing.ColorTranslator.FromHtml("Orange");
            lbtnDet.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
            trFarmer.Visible = true;
            gvFarpdf.DataSource = dt;
            gvFarpdf.DataBind();
            btnpd.Visible = true;
            lbtnPdf.Visible = false;
        }
    }
    protected void lbtnDet_Click(object sender, EventArgs e)
    {
        DataTable dt = farmerObj.GetFarmerDetails(ddlVillage.SelectedItem.Text);
        string vill = ddlVillage.SelectedItem.Text;
        if (dt.Rows.Count > 0)
        {
            trFarmer.Visible = true;
            gvFarpdf.DataSource = dt;
            gvFarpdf.DataBind();
            lbtnDet.ForeColor = System.Drawing.ColorTranslator.FromHtml("Orange");
            lbtnCon.ForeColor = System.Drawing.ColorTranslator.FromHtml("Blue");
            for (int i = 0; i < gvFarpdf.Rows.Count; i++)
            {
                if (vill == "Bastara" || vill == "shahbazpur" || vill == "Kurau")
                {
                    if (gvFarpdf.Rows[i].Cells[4].Text == "Org")
                    {
                        gvFarpdf.Rows[i].Cells[4].Text = "Org & FT";
                    }
                }
            }
            btnpd.Visible = true;
            lbtnPdf.Visible = false;
        }
    }
}
