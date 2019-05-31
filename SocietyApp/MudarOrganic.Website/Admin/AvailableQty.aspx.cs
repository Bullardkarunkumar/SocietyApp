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
using System.Xml.Linq;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;

public partial class Admin_AvailableQty : System.Web.UI.Page
{

    Farmer_BL farmerObj = new Farmer_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    Product_BL productObj = new Product_BL();
    Farming_BL farmingObj = new Farming_BL();
    FarmPlantation_BL farpln = new FarmPlantation_BL();
    public static int productID = 0;
    public static int seasonID = 0;
    DataTable dtnew = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnAvailableQuantity();
            BindYears();
            BindSeasonYear();
        }
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
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductList();
    }
    private void BindYears()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlYear.Items.Add(item);

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
    private void BindProductList()
    {
        if (ddlSeason.SelectedIndex > 0)
        {
            ddlProduct.DataSource = productObj.GetProductDetailsbySeasonNew(Convert.ToInt32(ddlSeason.SelectedValue));
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlProduct.Items.Clear();
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
            return;
        }
    }
    private void BindSeasonYear()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlSeason.Items.Add(item);
        ////for (int count = 0; count < Convert.ToInt32(WebConfigurationManager.AppSettings["SeasonYearCount"].ToString()); count++)
        ////    ddlSeasonYear.Items.Add((new ListItem()).Text = DateTime.Now.AddYears(count).Year.ToString());
        //ddlSeason.DataBind();
        //ddlSeason.SelectedValue = DateTime.Now.Year.ToString();

        ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
        ddlSeason.DataTextField = "SeasonName";
        ddlSeason.DataValueField = "SeasonID";
        ddlSeason.DataBind();
        ddlSeason.Items.Insert(0, MudarApp.AddListItem());
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        dtnew = farpln.GetAviableQtty(Convert.ToInt32(ddlProduct.SelectedValue));
        if (dtnew.Rows.Count > 0)
        {
            for (int i = 0; i < dtnew.Rows.Count; i++)
            {
                decimal X;
                X=Convert.ToDecimal(dtnew.Rows[i]["AvilQty"].ToString());
                if (Convert.ToDecimal(txtQty.Text) <= X)
                {
                    trAviQty.Visible = true;
                    txtDate.Text = dtnew.Rows[i]["Date"].ToString();
                }
            }
        }
        else
            trAviQty.Visible = false;
    }
}
