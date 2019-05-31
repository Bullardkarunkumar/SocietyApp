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

public partial class BranchReports_CollectionRegister : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    Settings_BL settObj = new Settings_BL();
    Reports_BL reportObj = new Reports_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    Product_BL prodObj = new Product_BL();
    int count = 0;
    public static string SortExpression_u = "CreatedDate";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindYears();
            Bindproducts();
        }
    }
    protected void btncollReg_Click(object sender, EventArgs e)
    {
        trId.Visible = true;
        divcollectionDetails.Visible = true;
        divStockreg.Visible = false;
        BindNewCollectedInfo();
        //BindCollectedProductInfo();
        trtotal.Visible = true;
        btncollReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btncollReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnStockReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnStockReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        GetTotalProduction();
        //decimal avil = Convert.ToDecimal(lblTotalProd.Text) - Convert.ToDecimal(lblColleted.Text);
        //lblAvailQty.Text = avil.ToString();
        divBack.Visible = true;
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
       // ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        ddlProduct.Items.Insert(0, "All");
    }
    protected void btnStockReg_Click(object sender, EventArgs e)
    {
        Newstock();
        GetTotalProduction();
        trId.Visible = true;
        trtotal.Visible = true;
        divStockreg.Visible = true;
        divcollectionDetails.Visible = false;
        //GetnewStockRegister();
        btnStockReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnStockReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btncollReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btncollReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        //decimal avil = Convert.ToDecimal(lblTotalProd.Text) - Convert.ToDecimal(lblColleted.Text);
        //lblAvailQty.Text = Math.Round(avil,1).ToString();
        
    }
    private void BindNewCollectedInfo()
    {   
        string[] code = ddlSeasonYear.Text.Split(new string[] { "20" }, StringSplitOptions.None);
        DataTable dt = reportObj.GetNewallcollectionList(ddlProduct.SelectedValue, code[1].ToString());
        if (dt.Rows.Count > 0)
        {

            DataTable dtNewPre = new DataTable();
            dtNewPre.Columns.Add("Blending_BatchID");
            dtNewPre.Columns.Add("CollectionQty", typeof(decimal));
            dtNewPre.Columns.Add("CreatedDate", typeof(DateTime));
            dtNewPre.Columns.Add("ProductID", typeof(int));

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("FarmerID");
            dtNew.Columns.Add("FarmID");
            dtNew.Columns.Add("FarmerName");
            dtNew.Columns.Add("Farmercode");
            dtNew.Columns.Add("PlotCode");
            dtNew.Columns.Add("ProductName");
            dtNew.Columns.Add("Lotnumber");
            dtNew.Columns.Add("CollectionQty", typeof(decimal));
            dtNew.Columns.Add("CreatedDate", typeof(DateTime));
            dtNew.Columns.Add("ProductID", typeof(int));
            dtNew.Columns.Add("OrderID", typeof(int));
            dtNew.Columns.Add("Whether", typeof(string));
            dtNew.Columns.Add("ReceivedBy", typeof(string));
            dtNew.Columns.Add("Remarks", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                string[] FarmerID = item["FarmerID"].ToString().Split('@');
                string[] FarmID = item["FarmID"].ToString().Split('@');
                string[] Lotnumber = item["Lotnumber"].ToString().Split('@');
                string[] CollectionQty = item["CollectionQty"].ToString().Split('@');
                string blendingBatchId = item["Blending_BatchID"].ToString();
                if (!string.IsNullOrEmpty(blendingBatchId) && blendingBatchId.Contains("@"))
                {
                    string[] BlendingBID = item["Blending_BatchID"].ToString().Split('@');
                    if (BlendingBID[0].ToString().Trim() == string.Empty)
                    {
                        string[] FarmerID1 = FarmerID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] FarmID1 = FarmID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] Lotnumber1 = Lotnumber[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] CollectionQty1 = CollectionQty[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < FarmerID1.Length; i++)
                        {
                            DataRow drNew = dtNew.NewRow();
                            drNew["FarmerID"] = FarmerID1[i].ToString();
                            drNew["FarmID"] = FarmID1[i].ToString();
                            drNew["Lotnumber"] = Lotnumber1[i].ToString();
                            drNew["CollectionQty"] = CollectionQty1[i].ToString();
                            DataTable dtFarmer = orderObj.GetBlendFarmerDetails(FarmerID1[i].ToString(), FarmID1[i].ToString());
                            if (dtFarmer.Rows.Count > 0)
                            {
                                drNew["FarmerName"] = dtFarmer.Rows[0]["FirstName"].ToString();
                                drNew["Farmercode"] = dtFarmer.Rows[0]["FarmerCode"].ToString();
                                drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                                drNew["ProductID"] = Convert.ToInt32(item["ProductID"]);
                                if (Convert.ToInt32(item["ProductID"]) == 4)
                                    drNew["ProductName"] = "Organic Cornmint Oil";
                                else
                                    drNew["ProductName"] = item["ProductName"].ToString();
                                
                                drNew["OrderID"] = Convert.ToInt32(item["OrderID"].ToString());
                            }
                            dtNew.Rows.Add(drNew);
                        }
                    }
                }
            }
            decimal farmerCollTotal = dtNew.AsEnumerable().Sum(row => row.Field<decimal>("CollectionQty"));
            lblColleted.Text = Math.Round(farmerCollTotal,1).ToString();

            DataView dv = dtNew.DefaultView;
            dv.Sort = "CreatedDate asc";
            DataTable sortedDT = dv.ToTable();
            
            Session["UnitInfo"] = null;
            Session["UnitInfo"] = dtNew;

            gvNewCollect.DataSource = sortedDT;
            gvNewCollect.DataBind();
        }
    }
    private void BindAllNewCollectedInfo()
    {
        
        string[] code = ddlSeasonYear.Text.Split(new string[] { "20" }, StringSplitOptions.None);
        DataTable dt = reportObj.GetNewallcollectionList("All", code[1].ToString());
        if (dt.Rows.Count > 0)
        {
            divcollectionDetails.Visible = true;
            trtotal.Visible = true;
            DataTable dtNewPre = new DataTable();
            dtNewPre.Columns.Add("Blending_BatchID");
            dtNewPre.Columns.Add("CollectionQty", typeof(decimal));
            dtNewPre.Columns.Add("CreatedDate", typeof(DateTime));
            dtNewPre.Columns.Add("ProductID", typeof(int));

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("FarmerID");
            dtNew.Columns.Add("FarmID");
            dtNew.Columns.Add("FarmerName");
            dtNew.Columns.Add("Farmercode");
            dtNew.Columns.Add("PlotCode");
            dtNew.Columns.Add("ProductName");
            dtNew.Columns.Add("Lotnumber");
            dtNew.Columns.Add("CollectionQty", typeof(decimal));
            dtNew.Columns.Add("CreatedDate", typeof(DateTime));
            dtNew.Columns.Add("ProductID", typeof(int));
            dtNew.Columns.Add("OrderID", typeof(int));
            dtNew.Columns.Add("Whether", typeof(string));
            dtNew.Columns.Add("ReceivedBy", typeof(string));
            dtNew.Columns.Add("Remarks", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                string[] FarmerID = item["FarmerID"].ToString().Split('@');
                string[] FarmID = item["FarmID"].ToString().Split('@');
                string[] Lotnumber = item["Lotnumber"].ToString().Split('@');
                string[] CollectionQty = item["CollectionQty"].ToString().Split('@');
                string blendingBatchId = item["Blending_BatchID"].ToString();
                if (!string.IsNullOrEmpty(blendingBatchId) && blendingBatchId.Contains("@"))
                {
                    string[] BlendingBID = item["Blending_BatchID"].ToString().Split('@');
                    if (BlendingBID[0].ToString().Trim() == string.Empty)
                    {
                        string[] FarmerID1 = FarmerID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] FarmID1 = FarmID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] Lotnumber1 = Lotnumber[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] CollectionQty1 = CollectionQty[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < FarmerID1.Length; i++)
                        {
                            DataRow drNew = dtNew.NewRow();
                            drNew["FarmerID"] = FarmerID1[i].ToString();
                            drNew["FarmID"] = FarmID1[i].ToString();
                            drNew["Lotnumber"] = Lotnumber1[i].ToString();
                            drNew["CollectionQty"] = CollectionQty1[i].ToString();
                            DataTable dtFarmer = orderObj.GetBlendFarmerDetails(FarmerID1[i].ToString(), FarmID1[i].ToString());
                            if (dtFarmer.Rows.Count > 0)
                            {
                                drNew["FarmerName"] = dtFarmer.Rows[0]["FirstName"].ToString();
                                drNew["Farmercode"] = dtFarmer.Rows[0]["FarmerCode"].ToString();
                                drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                                drNew["ProductID"] = Convert.ToInt32(item["ProductID"]);
                                if (Convert.ToInt32(item["ProductID"]) == 4)
                                    drNew["ProductName"] = "Organic Cornmint Oil";
                                else
                                    drNew["ProductName"] = item["ProductName"].ToString();

                                drNew["OrderID"] = Convert.ToInt32(item["OrderID"].ToString());
                            }
                            dtNew.Rows.Add(drNew);
                        }
                    }
                }
            }
            decimal farmerCollTotal = dtNew.AsEnumerable().Sum(row => row.Field<decimal>("CollectionQty"));
            lblColleted.Text = Math.Round(farmerCollTotal, 1).ToString();

            DataView dv = dtNew.DefaultView;
            dv.Sort = "CreatedDate asc";
            DataTable sortedDT = dv.ToTable();

            Session["UnitInfo"] = null;
            Session["UnitInfo"] = dtNew;

            gvNewCollect.DataSource = sortedDT;
            gvNewCollect.DataBind();
        }
    }
    private void Newstock()
    {
        divBack.Visible = true;
        decimal Qty;
        DataTable dtCollect = new DataTable();
        dtCollect.Columns.Add("CreatedDate", typeof(DateTime));
        dtCollect.Columns.Add("Qty", typeof(decimal));
        dtCollect.Columns.Add("Productname");
        dtCollect.Columns.Add("BQty", typeof(decimal));
        dtCollect.Columns.Add("Balance", typeof(decimal));
        decimal BQty = 0;
        string[] code = ddlSeasonYear.Text.Split(new string[] { "20" }, StringSplitOptions.None);
        DataTable dtUniqeCollectionDates = reportObj.GetCollectionDates(Convert.ToInt32(ddlProduct.SelectedValue), code[1].ToString());
        DataTable dt = reportObj.GetNewallcollectionList(ddlProduct.SelectedValue, code[1].ToString());
        foreach (DataRow Date in dtUniqeCollectionDates.Rows)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (Date["CreatedDate"].ToString() == item["CreatedDate"].ToString())
                {
                    DataRow drNew = dtCollect.NewRow();
                    if (item["CollectionQty"].ToString().Contains("@"))
                    {
                        string[] CollectionQty = item["CollectionQty"].ToString().Split('@');
                    }
                    else
                    {
                        string[] CollectionQty1 = item["CollectionQty"].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        Qty = 0;
                        for (int i = 0; i < CollectionQty1.Length; i++)
                        {
                            Qty = Qty + Convert.ToDecimal(CollectionQty1[i].ToString());
                        }
                        drNew["Qty"] = Qty.ToString();
                    }
                    drNew["ProductName"] = item["ProductName"].ToString();
                    drNew["CreatedDate"] = Convert.ToDateTime(item["CreatedDate"]);
                    drNew["BQty"] = "0.00";
                    drNew["Balance"] = "0.00";
                    dtCollect.Rows.Add(drNew);
                }
            }
        }
        DataTable dtblend1 = reportObj.GetSelectedBlendQty(code[1].ToString(), Convert.ToInt32(ddlProduct.SelectedValue));
        for (int i = 0; i < dtblend1.Rows.Count; i++)
        {
            DataTable dtDate1 = reportObj.GetSelectedCollectionDate(dtblend1.Rows[i]["createddate"].ToString(), Convert.ToInt32(ddlProduct.SelectedValue));
            if (dtblend1.Rows.Count > 0)
            {
                DataRow drNew1 = dtCollect.NewRow();
                decimal BlendQty = 0;
                if (dtDate1.Rows.Count > 0)
                {
                    if (dtDate1.Rows[0]["createddate"].ToString() == dtblend1.Rows[i]["createddate"].ToString())
                    {
                        BlendQty = BlendQty + Convert.ToDecimal(dtblend1.Rows[i]["BQty"].ToString());
                        drNew1["Qty"] = "0.00";
                        drNew1["Productname"] = dtblend1.Rows[i]["ProductName"].ToString();
                        drNew1["CreatedDate"] = dtblend1.Rows[i]["createddate"].ToString();
                        drNew1["BQty"] = BlendQty.ToString();
                        dtCollect.Rows.Add(drNew1);
                    }
                }
                else
                {
                    drNew1["Qty"] = "0.00";
                    drNew1["Productname"] = dtblend1.Rows[i]["ProductName"].ToString();
                    drNew1["CreatedDate"] = dtblend1.Rows[i]["createddate"].ToString();
                    drNew1["BQty"] = dtblend1.Rows[i]["BQty"].ToString();
                    dtCollect.Rows.Add(drNew1);
                }
            }
        }
        DataView dv = dtCollect.DefaultView;
        dv.Sort = "CreatedDate asc";
        DataTable sortedDT = dv.ToTable();
        if (sortedDT.Rows.Count > 0)
        {
            for (int d = 0; d < sortedDT.Rows.Count; d++)
            {
                BQty = BQty + (Convert.ToDecimal(sortedDT.Rows[d]["Qty"].ToString()) - Convert.ToDecimal(sortedDT.Rows[d]["BQty"]));
                sortedDT.Rows[d]["Balance"] = BQty.ToString();
            }
        }
        gvgvStockregister.DataSource = sortedDT;
        gvgvStockregister.DataBind();
        decimal farmerCollTotal = dtCollect.AsEnumerable().Sum(m => m.Field<decimal>("Qty"));
        lblColleted.Text = Math.Round(farmerCollTotal,1).ToString();
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //trId.Visible = true;
        divcollectionDetails.Visible = true;
        divStockreg.Visible = false;
        BindNewCollectedInfo();
        //BindCollectedProductInfo();
        trtotal.Visible = true;
        btncollReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btncollReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnStockReg.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnStockReg.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        GetTotalProduction();
        //decimal avil = Convert.ToDecimal(lblTotalProd.Text) - Convert.ToDecimal(lblColleted.Text);
        //lblAvailQty.Text = avil.ToString();
        divBack.Visible = true;
    }
    private void GetnewStockRegister()
    {
        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("CreatedDate", typeof(DateTime));
        dtNew.Columns.Add("Qty", typeof(decimal));
        dtNew.Columns.Add("BQty", typeof(decimal));
        dtNew.Columns.Add("Productname");
        dtNew.Columns.Add("Balance", typeof(decimal));
        
        string[] code = ddlSeasonYear.Text.Split(new string[] { "20" }, StringSplitOptions.None);
        DataTable dt = reportObj.GetCollectionDates(Convert.ToInt32(ddlProduct.SelectedValue), code[1].ToString());
        decimal BQty = 0;
        decimal CheckBqty = 0;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtDate = reportObj.GetSelectedCollectionDate(dt.Rows[i]["createddate"].ToString(), Convert.ToInt32(ddlProduct.SelectedValue),code[1].ToString());
                decimal Qty = 0;
                if (dtDate.Rows.Count > 0)
                {
                    DataRow drNew = dtNew.NewRow();
                    foreach (DataRow item in dtDate.Rows)
                    {
                        string[] CollectionQty = item["CollectionQty"].ToString().Split(';');
                        for (int j = 0; j < CollectionQty.Length - 1; j++)
                        {
                            Qty = Qty + Convert.ToDecimal(CollectionQty[j].ToString());
                        }
                    }
                    drNew["Qty"] = Qty.ToString();
                    drNew["Productname"] = dtDate.Rows[0]["ProductName"].ToString();
                    drNew["CreatedDate"] = dtDate.Rows[0]["createddate"].ToString();
                    drNew["BQty"] = "0.00";
                    DataTable dtblend = reportObj.GetSelectedBlendQty(code[1].ToString(), Convert.ToInt32(ddlProduct.SelectedValue));
                    if (dtblend.Rows.Count > 0)
                    {
                        decimal BlendQty = 0;
                        foreach (DataRow blendQty in dtblend.Rows)
                        {
                            if (dtDate.Rows[0]["createddate"].ToString() == blendQty["createddate"].ToString())
                            {
                                BlendQty = BlendQty + Convert.ToDecimal(blendQty["BQty"].ToString());
                                drNew["BQty"] = BlendQty.ToString();
                                CheckBqty = Convert.ToDecimal(BlendQty.ToString());
                            }
                            else
                            {
                                drNew["BQty"] = "0.00";
                            }
                        }
                    }
                    else
                    {
                        drNew["BQty"] = "0.00";
                    }
                    dtNew.Rows.Add(drNew);
                }
            }
        }
        DataTable dtblend1 = reportObj.GetSelectedBlendQty(code[1].ToString(), Convert.ToInt32(ddlProduct.SelectedValue));
        for (int i = 0; i < dtblend1.Rows.Count; i++)
        {
            DataTable dtDate1 = reportObj.GetSelectedCollectionDate(dtblend1.Rows[i]["createddate"].ToString(), Convert.ToInt32(ddlProduct.SelectedValue));
            if (dtblend1.Rows.Count > 0)
            {
                DataRow drNew1 = dtNew.NewRow();
                if (dtDate1.Rows.Count > 0)
                {
                    if (dtDate1.Rows[0]["createddate"].ToString() == dtblend1.Rows[i]["createddate"].ToString())
                    {
                       
                    }
                }
                else
                {
                    drNew1["Qty"] = "0.00";
                    drNew1["Productname"] = dtblend1.Rows[i]["ProductName"].ToString();
                    drNew1["CreatedDate"] = dtblend1.Rows[i]["createddate"].ToString();
                    drNew1["BQty"] = dtblend1.Rows[i]["BQty"].ToString();
                    dtNew.Rows.Add(drNew1);
                }
            }
        }
        DataView dv = dtNew.DefaultView;
        dv.Sort = "CreatedDate asc";
        DataTable sortedDT = dv.ToTable();
        if (sortedDT.Rows.Count > 0)
        {
            for (int d = 0; d < sortedDT.Rows.Count; d++)
            {
                BQty = BQty + (Convert.ToDecimal(sortedDT.Rows[d]["Qty"].ToString()) - Convert.ToDecimal(sortedDT.Rows[d]["BQty"]));
                sortedDT.Rows[d]["Balance"] = BQty.ToString();
            }
        }
        decimal farmerCollTotal = sortedDT.AsEnumerable().Sum(m => m.Field<decimal>("Qty"));
        lblColleted.Text = farmerCollTotal.ToString();
        Session["UnitInfo"] = null;
        Session["UnitInfo"] = dtNew;
        gvgvStockregister.DataSource = sortedDT;
        gvgvStockregister.DataBind();
    }
    
    private void GetTotalProduction()
    {
        DataTable dtProduction = reportObj.GetProduction(ddlProduct.SelectedValue.ToString(), DateTime.Now.AddDays(-1), ddlSeasonYear.SelectedItem.Text);
        if (dtProduction.Rows.Count > 0)
        {
            decimal total = dtProduction.AsEnumerable().Sum(row => row.Field<decimal>("TotalProductQuantity"));
            //lblTotalProd.Text =Math.Round(total,1).ToString();
        }
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
    private void SortingUnitInfo(string SortExpression)
    {
        DataTable dt = (DataTable)Session["UnitInfo"];
        Session["UnitInfo"] = dt;
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
        gvgvStockregister.DataSource = sortedView;
        gvgvStockregister.DataBind();
    }
    private void SortingCollectionInfo(string SortExpression)
    {
        DataTable dt = (DataTable)Session["UnitInfo"];
        Session["UnitInfo"] = dt;
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
        gvNewCollect.DataSource = sortedView;
        gvNewCollect.DataBind();
    }
    protected void gvgvStockregister_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_u = e.SortExpression.ToString();
        SortingUnitInfo(SortExpression_u);

    }
    protected void gvNewCollect_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_u = e.SortExpression.ToString();
        SortingCollectionInfo(SortExpression_u);
    }
    protected void ddlSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllNewCollectedInfo();
        //GetTotalProduction();
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
        gvNewCollect.GridLines = GridLines.Both;
        gvNewCollect.HeaderStyle.Font.Bold = true;
        gvNewCollect.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}