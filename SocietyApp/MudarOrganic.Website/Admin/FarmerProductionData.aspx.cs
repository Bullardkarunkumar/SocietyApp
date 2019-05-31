using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;
public partial class Admin_FarmerProductionData : System.Web.UI.Page
{
    Farmer_BL farmerobj = new Farmer_BL();
    Farming_BL farmingObj = new Farming_BL();
    FarmPlantation_BL fp = new FarmPlantation_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    FarmPlantation_BL fpobj = new FarmPlantation_BL();
    Product_BL objProd = new Product_BL();
    MudarApp objMudarApp = new MudarApp();
    UnitInformation_BL UI = new UnitInformation_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridDetails();
            BindSeasonYear();
            BindSeasonDetails();
            BindProductList();
        }
    }
    private void BindProductList()
    {
        if (ddlSeason.SelectedIndex > 0)
        {
            ddlProduct.DataSource = objProd.GetProductDetailsbySeason(Convert.ToInt32(ddlSeason.SelectedValue));
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlProduct.Items.Clear();
        }
    }
    private void BindGridDetails()
    {
        DataTable dtnew = new DataTable();
        //dtnew = 
    }
    private void BindSeasonYear()
    {
        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlYear.Items.Add(item);
        ddlYear.DataBind();
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
            ddlSeason.DataTextField = "SeasonName";
            ddlSeason.DataValueField = "SeasonID";
            ddlSeason.DataBind();
            ddlSeason.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlSeason.Items.Clear();
        }
    }
    private void BindSeasonDetails()
    {
        ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
        ddlSeason.DataTextField = "SeasonName";
        ddlSeason.DataValueField = "SeasonID";
        ddlSeason.DataBind();
        ddlSeason.Items.Insert(0, MudarApp.AddListItem());

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnFarmerGo_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
}
