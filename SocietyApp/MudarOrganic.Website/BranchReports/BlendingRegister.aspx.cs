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

public partial class BranchReports_BlendingRegister : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    Settings_BL settObj = new Settings_BL();
    Reports_BL reportObj = new Reports_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindYears();
            Bindproducts();
        }
    }
    private void BindYears()
    {
        DataTable Seasond = cpObj.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlSeasonYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlSeasonYear.DataTextField = "SeasonYear";
            ddlSeasonYear.DataValueField = "SeasonYear";
            ddlSeasonYear.DataBind();
            ddlSeasonYear.Items.Insert(0, MudarApp.AddListItem());
            ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    private void Bindproducts()
    {
        DataTable dt = reportObj.GetAllProducDetails();
        ddlProduct.DataSource = dt;
        ddlProduct.DataTextField = "ProductName";
        ddlProduct.DataValueField = "ProductId";
        ddlProduct.DataBind();
        //ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        ddlProduct.Items.Insert(0, "All");
    }
    private void GetBlendDetails()
    {
        DataTable dt = reportObj.GetBlendDetails(ddlProduct.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            gvBlendreg.DataSource = dt;
            gvBlendreg.DataBind();
            decimal farmerCollTotal = dt.AsEnumerable().Sum(m => m.Field<decimal>("BQty"));
            lblColleted.Text = farmerCollTotal.ToString();
            divBack.Visible = true;
        }
    }
    protected void btncollReg_Click(object sender, EventArgs e)
    {
        GetBlendDetails();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(ddlProduct.SelectedValue);
    }
    protected void ddlSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData("All");
    }
    private void BindData(string ProductID)
    {
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("CreatedDate", typeof(DateTime));
        dtNew.Columns.Add("OrderID", typeof(int));
        dtNew.Columns.Add("Productname");
        dtNew.Columns.Add("Lotnumber");
        dtNew.Columns.Add("BlendQty", typeof(decimal));
        dtNew.Columns.Add("BQty", typeof(decimal));
        dtNew.Columns.Add("Blending_BatchID");
        dtNew.Columns.Add("BQty1", typeof(decimal));
        dtNew.Columns.Add("BlendedBy");
        dtNew.Columns.Add("Remarks");
        DataTable dt = reportObj.GetBlendDetails(ProductID);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow item in dt.Rows)
            {
                string[] CollectionQty = item["BlendQty"].ToString().Split(';');
                string[] Lotnumber = item["Lotnumber"].ToString().Split(';');

                for (int j = 0; j < CollectionQty.Length - 1; j++)
                {
                    DataRow drNew = dtNew.NewRow();
                    drNew["BlendQty"] = CollectionQty[j].ToString();
                    drNew["Lotnumber"] = Lotnumber[j].ToString();
                    if (item["ProductId"].ToString() == "4" || item["ProductId"].ToString() == "10" || item["ProductId"].ToString() == "11")
                    {
                        if (item["ProductId"].ToString() == "4")
                            drNew["ProductName"] = "Organic Cornmint Oil";
                        if (item["ProductId"].ToString() == "10")
                            drNew["ProductName"] = "Organic Spearmint Oil Rec";
                        if (item["ProductId"].ToString() == "11")
                            drNew["ProductName"] = "Organic Basil Oil 80";
                    }
                    else
                        drNew["ProductName"] = item["ProductName"].ToString();
                    drNew["CreatedDate"] = item["createddate"].ToString();
                    drNew["OrderID"] = item["OrderID"].ToString();
                    drNew["BQty"] = item["BQty"].ToString();
                    drNew["Blending_BatchID"] = item["Blending_BatchID"].ToString();
                    drNew["BQty1"] = item["BQty"].ToString();
                    drNew["BlendedBy"] = string.Empty;
                    drNew["Remarks"] = string.Empty;
                    dtNew.Rows.Add(drNew);
                }
            }
            gvBlendreg.DataSource = dtNew;
            gvBlendreg.DataBind();
            trId.Visible = true;
            decimal farmerCollTotal = dt.AsEnumerable().Sum(m => m.Field<decimal>("BQty"));
            lblColleted.Text = farmerCollTotal.ToString();
            divBack.Visible = true;
        }
    }
    protected void gvBlendreg_DataBound(object sender, EventArgs e)
    {
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
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
        }
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
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
        }
       
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
            if (row.Cells[5].Text == previousRow.Cells[5].Text)
            {
                if (previousRow.Cells[5].RowSpan < 2)
                {
                    row.Cells[5].RowSpan = 2;
                }
                else
                {
                    row.Cells[5].RowSpan = previousRow.Cells[5].RowSpan + 1;
                }
                previousRow.Cells[5].Visible = false;
            }
        }
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
            if (row.Cells[6].Text == previousRow.Cells[6].Text)
            {
                if (previousRow.Cells[6].RowSpan < 2)
                {
                    row.Cells[6].RowSpan = 2;
                }
                else
                {
                    row.Cells[6].RowSpan = previousRow.Cells[6].RowSpan + 1;
                }
                previousRow.Cells[6].Visible = false;
            }
        }
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
            if (row.Cells[7].Text == previousRow.Cells[7].Text)
            {
                if (previousRow.Cells[7].RowSpan < 2)
                {
                    row.Cells[7].RowSpan = 2;
                }
                else
                {
                    row.Cells[7].RowSpan = previousRow.Cells[7].RowSpan + 1;
                }
                previousRow.Cells[7].Visible = false;
            }
        }
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
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
        }
        for (int rowIndex = gvBlendreg.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gvBlendreg.Rows[rowIndex];
            GridViewRow previousRow = gvBlendreg.Rows[rowIndex + 1];
            if (row.Cells[9].Text == previousRow.Cells[9].Text)
            {
                if (previousRow.Cells[9].RowSpan < 2)
                {
                    row.Cells[9].RowSpan = 2;
                }
                else
                {
                    row.Cells[9].RowSpan = previousRow.Cells[9].RowSpan + 1;
                }
                previousRow.Cells[9].Visible = false;
            }
        }
    }
    protected void btnPF_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "ReceptionRegister" + "-" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvBlendreg.GridLines = GridLines.Both;
        gvBlendreg.HeaderStyle.Font.Bold = true;
        gvBlendreg.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}