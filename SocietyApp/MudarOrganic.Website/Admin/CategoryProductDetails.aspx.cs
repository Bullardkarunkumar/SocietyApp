using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;


public partial class Admin_ProductDetails : System.Web.UI.Page
{
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL pr = new Product_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnCatProSea();
            BindselSeasonYear();
            CatBindDetails();
            SeaBindDetails();
            BindCatDetailsList();
            //BindSeaDetailsList();
            ProBindDetails();
            BindSeasonYear();
            //ddlSelSeasonYear_SelectedIndexChanged(sender, e);
        }
        divAddCategory.Visible = false;
        divAddProduct.Visible = false;
        divAddSeason.Visible = false;
    }

    protected override void OnPreRender(EventArgs e)
    {
        SetCurrentTab();
    }

    private void SetCurrentTab()
    {
        switch (htnSelectedTab.Value)
        {
            case "1":
                tabpaneCategory.Attributes.Add("class", "tab-pane active");
                tabpaneSeaon.Attributes.Add("class", "tab-pane");
                tabpaneProducts.Attributes.Add("class", "tab-pane");
                liCategory.Attributes.Add("class", "active");
                liProducts.Attributes.Remove("class");
                liSeason.Attributes.Remove("class");
                break;
            case "2":
                tabpaneProducts.Attributes.Add("class", "tab-pane active");
                tabpaneCategory.Attributes.Add("class", "tab-pane");
                tabpaneSeaon.Attributes.Add("class", "tab-pane");
                liProducts.Attributes.Add("class", "active");
                liCategory.Attributes.Remove("class");
                liSeason.Attributes.Remove("class");
                break;
            case "3":
                tabpaneSeaon.Attributes.Add("class", "tab-pane active");
                tabpaneCategory.Attributes.Add("class", "tab-pane");
                tabpaneProducts.Attributes.Add("class", "tab-pane");
                liSeason.Attributes.Add("class", "active");
                liCategory.Attributes.Remove("class");
                liProducts.Attributes.Remove("class");
                break;

        }
    }

    //protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    MainView.ActiveViewIndex = Convert.ToInt32(MainMenu.SelectedValue);
    //}
    #region Category
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Add new Category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        divAddCategory.Visible = true;
        CatBindDetails();
    }

    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: update the change done in "divAddCategory"
    protected void btnCatSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (string.IsNullOrEmpty(txtCategoryID.Text))
            result = cp.Category_INT_UPT(0, txtCategoryName.Text, "BHANU", "", MudarApp.Insert);
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Category Saved Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Category Save failed');", true);
        }
        CatClearControls();
        CatBindDetails();
    }
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Clear button will clear all control under Add divAddCategory
    /// </summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCatClear_Click(object sender, EventArgs e)
    {
        CatClearControls();
    }
    protected void gvCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            cp.Category_INT_UPT(index, "", "", "Bhanu", MudarApp.Delete);
            CatBindDetails();
        }
    }
    #endregion

    #region Product
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Add new Product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        divAddProduct.Visible = true;
        //BindSeaDetailsList();
        //cp.GetSeasonDetails();
        BindCatDetailsList();
        ProBindDetails();
    }
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: update the change done in "divAddProduct"
    protected void btnProductSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (string.IsNullOrEmpty(txtProductID.Text))
        {
            result = pr.Product_INS_UPT_DEL(0, txtProductcode.Text.ToUpper(), txtProductName.Text, txtDescription.Text, txtITCHSCode.Text, ddlSelectCategory.SelectedIndex > 0 ? Convert.ToInt32(ddlSelectCategory.SelectedValue) : 0, "Bhanu", "", MudarApp.Insert, txtSpecification.Text);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Product saved successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Product save failed');", true);
            }
            ProductClearControls();
            ProBindDetails();
        }
        else
        {
            result = pr.Product_INS_UPT_DEL(Convert.ToInt32(txtProductID.Text), txtProductcode.Text.ToUpper(), txtProductName.Text, txtDescription.Text, txtITCHSCode.Text, ddlSelectCategory.SelectedIndex > 0 ? Convert.ToInt32(ddlSelectCategory.SelectedValue) : 0, "Bhanu", "", MudarApp.Update, txtSpecification.Text);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Product updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Product update failed');", true);

            }
            ProductClearControls();
            ProBindDetails();
        }
    }
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Clear button will clear all control under Add divAddProduct
    /// </summary>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProductClear_Click(object sender, EventArgs e)
    {
        ProductClearControls();
    }
    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            ProBindDetails(Convert.ToInt32(gvProduct.DataKeys[index].Value.ToString()));
            ProBindDetails();

        }
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            pr.Product_INS_UPT_DEL(Convert.ToInt32(gvProduct.DataKeys[index].Value.ToString()), string.Empty, string.Empty, string.Empty, string.Empty, 0, "", "Bhanu", MudarApp.Delete, string.Empty);
            ProBindDetails();
        }
    }
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: clear the controls of Product
    /// </summary>

    #endregion

    #region Season
    protected void btnSeason_Click(object sender, EventArgs e)
    {
        divAddSeason.Visible = true;
        divSeasonDetails.Visible = false;
        ddlSelSeasonYear_SelectedIndexChanged(sender, e);
        divSeasonButton.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "fnBindPlugins", "fnBindPlugins()", true);
    }
    //protected void lbtnSeason_Click(object sender, EventArgs e)
    //{
    //    divAddSeason.Visible = true;
    //    //BindSeasonYear();
    //}
    protected void gvSeason_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            cp.Season_INT_UPT(Convert.ToInt32(gvSeason.DataKeys[index].Value.ToString()), string.Empty, DateTime.Now, DateTime.Now, string.Empty, "Bhanu", MudarApp.Delete, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text));
            ddlSelSeasonYear_SelectedIndexChanged(sender, e);
        }
        if (e.CommandName == "cmd_select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            SeaBindDetails(Convert.ToInt32(gvSeason.DataKeys[index].Value.ToString()));
            SeasonProdBindDetails(Convert.ToInt32(gvSeason.DataKeys[index].Value.ToString()));
            divSeasonDetails.Visible = false;
        }
    }
    protected void btnSeasonSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            List<int> selProds = new List<int>();
            foreach (GridViewRow item in grdSeasonProduct.Rows)
            {
                CheckBox cbItem = (CheckBox)item.Cells[1].FindControl("chkItemCheck");
                if (cbItem.Checked)
                {
                    selProds.Add(Convert.ToInt32(grdSeasonProduct.DataKeys[item.RowIndex].Value));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Wrong info provided');", true);
                }
            }

            int result = 0;
            DateTime SDate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EDate = Convert.ToDateTime(txtEndDate.Text);
            if (string.IsNullOrEmpty(txtSeasonID.Text))
            {
                result = cp.Season_INT_UPT(0, txtSeasonNmae.Text, SDate, EDate, "BHANU", "", MudarApp.Insert, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text));
                if (result > 0)
                {
                    foreach (int item in selProds)
                    {
                        cp.SeasonProduct_INSandUPDandDEL(item, result, MudarApp.Insert);
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Season saved Successfully');", true);
                }
            }
            else
            {
                List<int> dbProds = pr.GetSeasonProdIds(Convert.ToInt32(txtSeasonID.Text));
                List<int> delProds = dbProds.Except(selProds).ToList();
                List<int> addProds = selProds.Except(dbProds).ToList();

                result = cp.Season_INT_UPT(Convert.ToInt32(txtSeasonID.Text), txtSeasonNmae.Text, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), "", "BHANU", MudarApp.Update, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text));
                foreach (int item in delProds)
                {
                    cp.SeasonProduct_INSandUPDandDEL(item, result, MudarApp.Delete);
                }
                foreach (int item in addProds)
                {
                    cp.SeasonProduct_INSandUPDandDEL(item, result, MudarApp.Insert);
                }
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Season update Successfully');", true);
            }
            SeaClearControls();
            SeaBindDetails();
            BindselSeasonYear();
            ddlSelSeasonYear_SelectedIndexChanged(sender, e);
            divSeasonDetails.Visible = true;
            divSeasonButton.Visible = true;
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx", false);
        }
    }
    protected void btnSeasonClear_Click(object sender, EventArgs e)
    {
        txtSeasonID.Text = string.Empty;
        txtSeasonNmae.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        divSeasonDetails.Visible = true;
        divSeasonButton.Visible = true;
    }
    protected void gvSeason_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable Sproduct = pr.GetProductDetailsbySeasonNew(Convert.ToInt32(gvSeason.DataKeys[e.Row.RowIndex].Value));
            if (Sproduct.Rows.Count > 0)
            {
                ImageButton btn = (ImageButton)e.Row.Cells[4].Controls[0];
                btn.Enabled = false;
            }
        }
    }
    #endregion

    #region Private Methods
    #region Category Operations
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: clear the controls of Category
    /// </summary>
    private void CatClearControls()
    {
        txtCategoryID.Text = String.Empty;
        txtCategoryName.Text = String.Empty;
    }
    /// <summary>
    /// Create By :Bhanu
    /// Create Date: 7/3/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Bind the Category Details for Grid
    /// </summary>
    private void CatBindDetails()
    {
        gvCategory.DataSource = cp.GetCategoryDetails();
        gvCategory.DataBind();
    }
    private void BindCatDetailsList()
    {
        ddlSelectCategory.DataSource = cp.GetCategoryDetails();
        ddlSelectCategory.DataTextField = "CategoryName";
        ddlSelectCategory.DataValueField = "CategoryID";
        ddlSelectCategory.DataBind();

        ddlSelectCategory.Items.Insert(0,"Select Category");
    }

    #endregion
    #region Season Operations
    private void SeaBindDetails()
    {
        gvSeason.DataSource = cp.GetSeasonDetails();
        gvSeason.DataBind();
        //if (gvSeason.Rows.Count > 0)
        //{
        //    for (int count = 0; count < gvSeason.Rows.Count; count++)
        //    {
        //        DataTable Sproduct = pr.GetProductDetailsbySeason(Convert.ToInt32(gvSeason.DataKeys[count].Value));
        //        //if(Sproduct.Rows.Count>0)

        //    }
        //}
    }
    private void SeaBindDetails(int SeasonID)
    {
        DataTable seasondt = cp.GetSeasonDetails(SeasonID);
        if (seasondt.Rows.Count > 0)
        {
            divAddSeason.Visible = true;
            DataRow dr = seasondt.Rows[0];
            txtSeasonID.Text = dr["SeasonId"].ToString();
            txtSeasonNmae.Text = dr["SeasonName"].ToString();
            DateTime Sdate = Convert.ToDateTime(dr["startDate"].ToString());
            txtStartDate.Text = Sdate.ToShortDateString();
            ddlSeasonYear.SelectedItem.Text = dr["SeasonYear"].ToString();
            DateTime Edate = Convert.ToDateTime(dr["endDate"].ToString());
            txtEndDate.Text = Edate.ToShortDateString();
        }
        //txtSeasonID.Text = 
    }
    private void SeaClearControls()
    {
        txtSeasonNmae.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtSeasonID.Text = string.Empty;
        ddlSeasonYear.ClearSelection();
    }
    //private void BindSeaDetailsList()
    //{
    //    ddlSelectSeason.DataSource = cp.GetSeasonDetails();
    //    ddlSelectSeason.DataTextField = "SeasonName";
    //    ddlSelectSeason.DataValueField = "SeasonID";
    //    ddlSelectSeason.DataBind();

    //    ddlSelectSeason.Items.Insert(0, MudarApp.AddListItem());
    //}
    private void BindSeasonYear()
    {

        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlSeasonYear.Items.Add(item);
        ddlSeasonYear.DataBind();
        ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();

        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlSeasonYear.Items.Add(item);
        //for (int count = 0; count < Convert.ToInt32(WebConfigurationManager.AppSettings["SeasonYearCount"].ToString()); count++)
        //    ddlSeasonYear.Items.Add((new ListItem()).Text = DateTime.Now.AddYears(count).Year.ToString());
        //ddlSeasonYear.DataBind();
        //ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        //DataTable temp = cp.GetSeasonDetails();
        //if (temp.Rows.Count > 0)
        //{
        //    ddlSeasonYear.DataSource = temp.DefaultView.ToTable(true, "SeasonYear");
        //    ddlSeasonYear.DataTextField = "SeasonYear";
        //    ddlSeasonYear.DataValueField = "SeasonYear";
        //    ddlSeasonYear.DataBind();
        //    string currYear = Convert.ToString(DateTime.Now.Year);
        //    IEnumerable<DataRow> query = temp.AsEnumerable().Where(m => m.Field<int>("SeasonYear") == DateTime.Now.Year);
        //    if (query.Count() <= 0)
        //    {
        //        ListItem lstItem = new ListItem(currYear, currYear);
        //        ddlSeasonYear.Items.Add(lstItem);
        //        ddlSeasonYear.DataBind();
        //    }

        //    ddlSeasonYear.SelectedValue = currYear;
        //}
    }
    private void BindselSeasonYear()
    {
        DataTable temp = cp.GetSeasonDetails();
        if (temp.Rows.Count > 0)
        {
            ddlSelSeasonYear.DataSource = temp.DefaultView.ToTable(true, "SeasonYear");
            ddlSelSeasonYear.DataTextField = "SeasonYear";
            ddlSelSeasonYear.DataValueField = "SeasonYear";
            ddlSelSeasonYear.DataBind();
            ListItem selectitem = new ListItem();
            selectitem.Text = "Select Season Year";
            selectitem.Value = string.Empty;
            ddlSelSeasonYear.Items.Insert(0, selectitem);
            ddlSelSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
    #endregion
    #region Product Operations
    private void ProBindDetails()
    {
        gvProduct.DataSource = pr.GetProductDetailsNew();
        gvProduct.DataBind();
    }

    private void SeasonProdBindDetails(int seasonId)
    {
        DataTable dt = pr.GetProductDetailsNew();
        dt.Columns.Add(new DataColumn("Selected", typeof(bool)));
        List<int> seasProd = pr.GetSeasonProdIds(seasonId);
        foreach (DataRow item in dt.Rows)
        {
            int pid = Convert.ToInt32(item[0]);
            int count = seasProd.Where(m => m == pid).Count();
            if (count > 0)
            {
                item["Selected"] = true;
            }
            else
            {
                item["Selected"] = false;
            }
        }
        grdSeasonProduct.DataSource = dt;
        grdSeasonProduct.DataBind();
    }
    private void ProBindDetails(int ProductID)
    {
        divAddProduct.Visible = true;
        CatBindDetails();
        SeaBindDetails();
        DataTable Productdt = pr.GetProductDetails(ProductID);
        if (Productdt.Rows.Count > 0)
        {
            DataRow dr = Productdt.Rows[0];
            txtProductID.Text = dr["ProductID"].ToString();
            txtProductcode.Text = dr["ProductCode"].ToString();
            txtProductName.Text = dr["ProductName"].ToString();
            ddlSelectCategory.ClearSelection();
            ddlSelectCategory.Items.FindByValue(dr["Categoryid"].ToString()).Selected = true;
            //ddlSelectCategory.Text = dr["CategoryName"].ToString();
            txtDescription.Text = dr["Description"].ToString();
            //ddlSelectSeason.ClearSelection();
            //ddlSelectSeason.Items.FindByValue(dr["SeasonID"].ToString()).Selected = true;
            //ddlSelectSeason.Text = dr["SessonName"].ToString();
            txtITCHSCode.Text = dr["ItcHsCode"].ToString();
            txtSpecification.Text = dr["Specification"].ToString();
        }
    }
    private void ProductClearControls()
    {
        txtProductID.Text = string.Empty;
        txtProductcode.Text = string.Empty;
        txtProductName.Text = string.Empty;
        ddlSelectCategory.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        //ddlSelectSeason.SelectedIndex = 0;
        txtITCHSCode.Text = string.Empty;
        txtSpecification.Text = string.Empty;

    }
    #endregion
    #endregion

    protected void ddlSelSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedYear = ddlSelSeasonYear.SelectedValue.ToString();
        if (!string.IsNullOrEmpty(selectedYear))
        {
            string sql = "SeasonYear = " + selectedYear;
            DataTable temp = cp.GetSeasonDetails();
            if (temp.Rows.Count > 0)
            {
                DataRow[] seasonrow = temp.Select(sql);
                DataTable seasondt = temp.Clone();
                foreach (DataRow dr in seasonrow)
                {
                    DataRow newdr = seasondt.NewRow();
                    newdr[0] = dr[0];
                    newdr[1] = dr[1];
                    newdr[2] = dr[2];
                    newdr[3] = dr[3];
                    newdr[4] = dr[4];
                    newdr[5] = dr[5];
                    newdr[6] = dr[6];
                    newdr[7] = dr[7];
                    newdr[8] = dr[8];
                    newdr[9] = dr[9];
                    //newdr.AcceptChanges();
                    seasondt.Rows.Add(newdr);
                }
                gvSeason.DataSource = seasondt;
                gvSeason.DataBind();
            }
        }
        else
        {
            SeaBindDetails();
        }
    }
    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes.Add("onClick", "return confirm('Are you sure delete this record?');");
        }
    }

    protected void lnkbtnCategory_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "1";
    }

    protected void lnkbtnProducts_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "2";
    }

    protected void lnkbtnSeason_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "3";
    }
}
