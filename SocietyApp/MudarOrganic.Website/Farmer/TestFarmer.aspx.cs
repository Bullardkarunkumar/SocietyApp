using MudarOrganic.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Farmer_TestFarmer : System.Web.UI.Page
{
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL prod = new Product_BL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        TestFarmerBinding();
        //}
    }

    private void TestFarmerBinding()
    {
        string seasonYr = "2014";
        DataTable dtSeasonDetails = cp.GetSeasonDetails(seasonYr);


        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Product Id", typeof(int)));
        dt.Columns.Add(new DataColumn("Product Name", typeof(string)));
        foreach (DataRow item in dtSeasonDetails.Rows)
        {
            DataColumn dc = new DataColumn(Convert.ToString(item["SeasonName"]), typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn(Convert.ToString(item["SeasonId"]), typeof(string));
            dt.Columns.Add(dc);
        }
        DataTable dtProds = prod.GetProductDetailsNew();
        foreach (DataRow item in dtProds.Rows)
        {
            DataRow newRow = dt.NewRow();
            newRow[0] = Convert.ToInt32(item["ProductId"]);
            newRow[1] = Convert.ToString(item["ProductName"]);
            for (int i = 2; i < dt.Columns.Count; i++)
            {
                if (i % 2 == 0)
                    newRow[i] = bool.FalseString;
                else
                    newRow[i] = Convert.ToString(dt.Columns[i].ColumnName);
            }
            dt.Rows.Add(newRow);
        }
        //dt = dt;
        gvfarmdetails.Columns.Clear();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (i == 1)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = "Product Name";
                boundField.HeaderText = "";
                gvfarmdetails.Columns.Add(boundField);
            }
            else if (i == 0)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = "Product Id";
                boundField.HeaderText = "";
                //boundField.Visible = false;
                gvfarmdetails.Columns.Add(boundField);
            }
            else if (i % 2 == 0 && i >= 2)
            {
                TemplateField templateField = new TemplateField();
                templateField.HeaderText = dt.Columns[i].ColumnName;
                gvfarmdetails.Columns.Add(templateField);
            }
            else if (i % 2 == 1 && i >= 2)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = dt.Columns[i].ColumnName;
                boundField.HeaderText = dt.Columns[i].ColumnName;
                //boundField.Visible = false;
                gvfarmdetails.Columns.Add(boundField);
            }
        }

        gvfarmdetails.DataSource = dt;
        gvfarmdetails.DataBind();

    }
    protected void gvfarmdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i >= 2 && i % 2 == 0)
                {
                    CheckBox chk = new CheckBox();
                    e.Row.Cells[i].Controls.Add(chk);
                }
                else if (i == 0 || (i >= 2 && i % 2 == 1))
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i == 0 || (i >= 2 && i % 2 == 1))
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow item in gvfarmdetails.Rows)
        {
            if (item.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    TableCell item1 = item.Cells[i];
                    if (item1.Controls.OfType<CheckBox>().Count() > 0)
                    {
                        CheckBox chk = item1.Controls.OfType<CheckBox>().FirstOrDefault();
                        if (chk != null && chk.Checked)
                        {
                            string prodid = item.Cells[0].Text.ToString();
                            string seasid = item.Cells[i + 1].Text.ToString();
                        }

                    }
                }
            }
        }
    }
}