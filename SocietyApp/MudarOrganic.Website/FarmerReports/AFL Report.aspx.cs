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

public partial class FarmerReports_AFL_Report : System.Web.UI.Page
{
    Reports_BL report = new Reports_BL();
    DataTable dt = new DataTable();
    Farmer_BL farmerObj = new Farmer_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnReports();
            BindYears();
        }
    }
    private void BindYears()
    {
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
    private void BindGridDetails(string Year)
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        string icsCode = farmerObj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
        dt = report.GetAFLReportData(icsCode,Year);
        if (dt.Rows.Count > 0)
        {
            gvFarmerList.DataSource = dt;
            gvFarmerList.DataBind();
            bill.Visible = true;
            btnBack.Visible = true;
        }
    }
    private void BindFarmerList()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = farmerObj.FamerDetails();
            lblYear.Text = DateTime.Now.Year.ToString();
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
            }
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
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[7].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[10].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false; // Invisibiling Year Header Cell
            e.Row.Cells[13].Visible = false; // Invisibiling Period Header Cell
            e.Row.Cells[14].Visible = false; // Invisibiling Period Header Cell

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool check =Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Organic").ToString());
            if (check == false)
                e.Row.Cells[10].Text = "Org&FT";
            else
                e.Row.Cells[10].Text = "Org";
            bool check1 = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "firstRes").ToString());
            if (check1 == false)
                e.Row.Cells[21].Text = "";
            else
                e.Row.Cells[21].Text = "OK";
            bool check2 = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "secondRes").ToString());
            if (check2 == false)
                e.Row.Cells[22].Text = "";
            else
                e.Row.Cells[22].Text = "OK";
            bool check3 = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "thirddRes").ToString());
            if (check3 == false)
                e.Row.Cells[23].Text = "";
            else
                e.Row.Cells[23].Text = "OK";
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

            //Adding Period Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Father/Husband Name";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Farmer Reg. No";
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
            HeaderCell.Text = "Village";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Taluk";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "District";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "State";
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

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Organic Status";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Survey No.";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Latitude";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Longitude";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Audited By Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Last date of prohibited substances";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.RowSpan = 2;
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Internal Inspection Dates";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 3; // For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Inspector Name";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 3;// For merging three columns (Direct, Referral, Total)
            HeaderCell.CssClass = "HeaderStyle";
            HeaderRow.Cells.Add(HeaderCell);

            //Adding Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = "Internal Inspection Result";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.ColumnSpan = 3; // For merging three columns (Direct, Referral, Total)
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
            row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[1].Text == previousRow.Cells[1].Text)
            {
                if (previousRow.Cells[1].RowSpan < 2)
                {
                    row.Cells[1].RowSpan = 2;
                }
                else
                {
                    row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan + 1;
                }
                previousRow.Cells[1].Visible = false;
            }
            row.Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
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
            row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
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
            row.Cells[3].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        //for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow row = gvFarmerList.Rows[rowIndex];
        //    GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
        //    if (row.Cells[4].Text == previousRow.Cells[4].Text)
        //    {
        //        if (previousRow.Cells[4].RowSpan < 2)
        //        {
        //            row.Cells[4].RowSpan = 2;
        //        }
        //        else
        //        {
        //            row.Cells[4].RowSpan = previousRow.Cells[4].RowSpan + 1;
        //        }
        //        previousRow.Cells[4].Visible = false;
        //    }
        //    row.Cells[4].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        //}
        //for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow row = gvFarmerList.Rows[rowIndex];
        //    GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
        //    if (row.Cells[5].Text == previousRow.Cells[5].Text)
        //    {
        //        if (previousRow.Cells[5].RowSpan < 2)
        //        {
        //            row.Cells[5].RowSpan = 2;
        //        }
        //        else
        //        {
        //            row.Cells[5].RowSpan = previousRow.Cells[5].RowSpan + 1;
        //        }
        //        previousRow.Cells[5].Visible = false;
        //    }
        //    row.Cells[5].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        //}
        //for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow row = gvFarmerList.Rows[rowIndex];
        //    GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
        //    if (row.Cells[6].Text == previousRow.Cells[6].Text)
        //    {
        //        if (previousRow.Cells[6].RowSpan < 2)
        //        {
        //            row.Cells[6].RowSpan = 2;
        //        }
        //        else
        //        {
        //            row.Cells[6].RowSpan = previousRow.Cells[6].RowSpan + 1;
        //        }
        //        previousRow.Cells[6].Visible = false;
        //    }
        //    row.Cells[6].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        //}
        //for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow row = gvFarmerList.Rows[rowIndex];
        //    GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
        //    if (row.Cells[7].Text == previousRow.Cells[7].Text)
        //    {
        //        if (previousRow.Cells[7].RowSpan < 2)
        //        {
        //            row.Cells[7].RowSpan = 2;
        //        }
        //        else
        //        {
        //            row.Cells[7].RowSpan = previousRow.Cells[7].RowSpan + 1;
        //        }
        //        previousRow.Cells[7].Visible = false;
        //    }
        //    row.Cells[7].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        //}
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[8].Text == previousRow.Cells[8].Text)
            {
                if (previousRow.Cells[8].RowSpan < 2)
                {
                    row.Cells[8].RowSpan = 2;
                }
                else
                {
                    row.Cells[8].RowSpan = previousRow.Cells[8].RowSpan + 1;
                }
                previousRow.Cells[8].Visible = false;
            }
            row.Cells[8].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[12].Text == previousRow.Cells[12].Text)
            {
                if (previousRow.Cells[12].RowSpan < 2)
                {
                    row.Cells[12].RowSpan = 2;
                }
                else
                {
                    row.Cells[12].RowSpan = previousRow.Cells[12].RowSpan + 1;
                }
                previousRow.Cells[12].Visible = false;
            }
            row.Cells[12].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
        }
        for (int rowIndex = gvFarmerList.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvFarmerList.Rows[rowIndex];
            GridViewRow previousRow = gvFarmerList.Rows[rowIndex + 1];
            if (row.Cells[13].Text == previousRow.Cells[13].Text)
            {
                if (previousRow.Cells[13].RowSpan < 2)
                {
                    row.Cells[13].RowSpan = 2;
                }
                else
                {
                    row.Cells[13].RowSpan = previousRow.Cells[13].RowSpan + 1;
                }
                previousRow.Cells[13].Visible = false;
            }
            row.Cells[13].ForeColor = System.Drawing.ColorTranslator.FromHtml("Red"); // This is to just give header color, font style
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
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    string FileName = "AFL" + DateTime.Now + ".xls";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);
    //        gvFarmerList.HeaderRow.BackColor = Color.White;
    //        foreach (TableCell cell in gvFarmerList.HeaderRow.Cells)
    //        {
    //            cell.BackColor = gvFarmerList.HeaderStyle.BackColor;
    //        }
    //        foreach (GridViewRow row in gvFarmerList.Rows)
    //        {
    //            row.BackColor = Color.White;
    //            foreach (TableCell cell in row.Cells)
    //            {
    //                if (row.RowIndex % 2 == 0)
    //                {
    //                    cell.BackColor = gvFarmerList.AlternatingRowStyle.BackColor;
    //                }
    //                else
    //                {
    //                    cell.BackColor = gvFarmerList.RowStyle.BackColor;
    //                }
    //                cell.CssClass = "SomeCustomCssClass";
    //            }
    //        }

    //        gvFarmerList.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .SomeCustomCssClass { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
   
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridDetails(ddlYear.SelectedItem.Text);
    }
}
