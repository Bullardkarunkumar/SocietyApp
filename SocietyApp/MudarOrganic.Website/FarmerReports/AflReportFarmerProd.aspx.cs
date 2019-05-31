using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.Drawing;
using System.IO;
using MudarOrganic.Components;

public partial class FarmerReports_AflReportFarmerProd : System.Web.UI.Page
{
    Reports_BL report = new Reports_BL();
    DataTable dt = new DataTable();
    Farmer_BL farmerObj = new Farmer_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnReports();
            BindGridDetails();
        }
    }
    private void BindGridDetails()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
        dt = report.GetAFLReportProduction(icsCode);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["VC"] = (Convert.ToDecimal(dt.Rows[i]["PA"].ToString()) - (Convert.ToDecimal(dt.Rows[i]["CA"].ToString()) + Convert.ToDecimal(dt.Rows[i]["CA1"].ToString()) + Convert.ToDecimal(dt.Rows[i]["CA2"].ToString()))).ToString();
            }
            gvFarmerList.DataSource = dt;
            gvFarmerList.DataBind();
        }
    }
    private void BindFarmerList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = farmerObj.FamerDetails();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void gvFarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[1].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[4].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[20].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool check = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Organic").ToString());
            if (check == false)
                e.Row.Cells[8].Text = "Org&FT";
            else
                e.Row.Cells[8].Text = "Org";
            if (check == false)
                e.Row.Cells[13].Text = "Org&FT";
            else
                e.Row.Cells[13].Text = "Org";
            if (check == false)
                e.Row.Cells[18].Text = "Org&FT";
            else
                e.Row.Cells[18].Text = "Org";
        }
    }
    protected void gvFarmerList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        // Adding a column manually once the header created
        if (e.Row.RowType == DataControlRowType.Header) // If header created
        {
            GridView ProductGrid = (GridView)sender;
            // Creating a Row
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Adding Year Column
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Farmer Name";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2; // For merging first, second row cells to one
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Village";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Farmer Tracenet No";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Organic Area(Ha)";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Organic Plot Area(Ha)";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);


            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Crop: Mentha Piperita";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 5; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Crop: Mentha Arvensis";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 5;// For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Crop: Mentha Spicata";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 5; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Vacant land/Fallow";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding the Row at the 0th position (first row) in the Grid
            ProductGrid.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void gvFarmerList_DataBound(object sender, EventArgs e)
    {
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[0].Text == previousRow.Cells[0].Text)
            {
                if (previousRow.Cells[0].RowSpan < 2)
                {
                    row.Cells[0].RowSpan = 2;
                }
                else
                {
                    row.Cells[0].RowSpan = previousRow.Cells[0].RowSpan + 1;
                }
                previousRow.Cells[0].Visible = false;
            }
            //row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[2].Text == previousRow.Cells[2].Text)
            {
                if (previousRow.Cells[2].RowSpan < 2)
                {
                    row.Cells[2].RowSpan = 2;
                }
                else
                {
                    row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan + 1;
                }
                previousRow.Cells[2].Visible = false;
            }
            //row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[2].Text == previousRow.Cells[2].Text)
            {
                if (previousRow.Cells[2].RowSpan < 2)
                {
                    row.Cells[2].RowSpan = 2;
                }
                else
                {
                    row.Cells[2].RowSpan = previousRow.Cells[2].RowSpan + 1;
                }
                previousRow.Cells[2].Visible = false;
            }
            //row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[3].Text == previousRow.Cells[3].Text)
            {
                if (previousRow.Cells[3].RowSpan < 2)
                {
                    row.Cells[3].RowSpan = 2;
                }
                else
                {
                    row.Cells[3].RowSpan = previousRow.Cells[3].RowSpan + 1;
                }
                previousRow.Cells[3].Visible = false;
            }
            //row.Cells[3].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btnPF_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AFL" + "-" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvFarmerList.GridLines = GridLines.Both;
        gvFarmerList.HeaderStyle.Font.Bold = true;
        gvFarmerList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}