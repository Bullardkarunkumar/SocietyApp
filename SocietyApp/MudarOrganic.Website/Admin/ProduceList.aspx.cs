using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;

public partial class Admin_ProduceList : System.Web.UI.Page
{
    FarmPlantation_BL fp = new FarmPlantation_BL();
    Reports_BL report = new Reports_BL();
    public static string SortExpression_u = "PlantationId";
    Farmer_BL frmObj = new Farmer_BL();
    string chkSelectedVal = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindYears();
            BindIcsCodes();
            //chkICSList_SelectedIndexChanged(sender,e);
        }
    }
    private void BindYears()
    {
        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlyear.Items.Add(item);
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
    }
   
    protected void btnPP_Click(object sender, EventArgs e)
    {
        chkICSList_SelectedIndexChanged(sender, e);
        BindPlantationDetails(1, ddlyear.SelectedItem.Text, chkSelectedVal);
        btnPP.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnPP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnCM.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnCM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnSP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnSP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnBOs.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnBOs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnCM_Click(object sender, EventArgs e)
    {
        chkICSList_SelectedIndexChanged(sender, e);
        BindPlantationDetails(2, ddlyear.SelectedItem.Text, chkSelectedVal);
        btnCM.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnCM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnPP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnPP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnSP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnSP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnBOs.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnBOs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnSP_Click(object sender, EventArgs e)
    {
        chkICSList_SelectedIndexChanged(sender, e);
        BindPlantationDetails(3, ddlyear.SelectedItem.Text, chkSelectedVal);
        btnSP.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnSP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnCM.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnCM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnPP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnPP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnBOs.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnBOs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

    }
    protected void btnBOs_Click(object sender, EventArgs e)
    {
        BindPlantationDetails(8, ddlyear.SelectedItem.Text, chkSelectedVal);
        btnBOs.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnBOs.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnCM.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnCM.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnPP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnPP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnSP.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnSP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    public void BindIcsCodes()
    {
        DataTable dt = frmObj.GetAllICSCodes();
        chkICSList.DataTextField = "Branchcode";
        chkICSList.DataValueField = "Branchcode";
        chkICSList.DataSource = dt;
        chkICSList.DataBind();
    }
    public void BindPlantationDetails(int ID, string Year, string chkSelectedVal)
    {
        DataTable dt = report.GetAFLTotalProduction(chkSelectedVal,Year,ID);
        decimal Croparea = dt.AsEnumerable().Sum(m => m.Field<decimal>("CA"));
        lblCroparea.Text = string.Format("{0:0.0}", Croparea);

        decimal SolddQty = dt.AsEnumerable().Sum(m => m.Field<decimal>("EstimationFHerbaga"));
        lblEstiHerb.Text = string.Format("{0:0.0}", SolddQty);
        decimal FirstHerbaga = dt.AsEnumerable().Sum(m => m.Field<decimal>("FirstHerbaga"));
        lblHerbage.Text = string.Format("{0:0.0}", FirstHerbaga);
        decimal EstimationFProductQuantity = dt.AsEnumerable().Sum(m => m.Field<decimal>("EstimationFProductQuantity"));
        lblFEoil.Text = string.Format("{0:0.0}", EstimationFProductQuantity);
        decimal FirstProductQuantity = dt.AsEnumerable().Sum(m => m.Field<decimal>("FirstProductQuantity"));
        lblFAOil.Text = string.Format("{0:0.0}", FirstProductQuantity);


        decimal EstimationSHerbaga = dt.AsEnumerable().Sum(m => m.Field<decimal>("EstimationSHerbaga"));
        lblSEH.Text = string.Format("{0:0.0}", EstimationSHerbaga);
        decimal SecondHerbaga = dt.AsEnumerable().Sum(m => m.Field<decimal>("SecondHerbaga"));
        lblSAH.Text = string.Format("{0:0.0}", SecondHerbaga);
        decimal EstimationSProductQuantity = dt.AsEnumerable().Sum(m => m.Field<decimal>("EstimationSProductQuantity"));
        lblSEOil.Text = string.Format("{0:0.0}", EstimationSProductQuantity);
        decimal SecondProductQuantity = dt.AsEnumerable().Sum(m => m.Field<decimal>("SecondProductQuantity"));
        lblSAOil.Text = string.Format("{0:0.0}", SecondProductQuantity);

        decimal TotalQty = dt.AsEnumerable().Sum(m => m.Field<decimal>("TotalProductQuantity"));
        lblTOil.Text = string.Format("{0:0.0}", TotalQty);
      
        if (dt.Rows.Count > 0)
        {
            divResult.Visible = true;
            Session["ProductionInfo"] = null;
            Session["ProductionInfo"] = dt;
            gvPDetails.DataSource = dt;
            gvPDetails.DataBind();
        }
        else
        {
            divResult.Visible = false;
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! No Data Found !!!');</script>");
            return;
        }
    }
    protected void chkICSList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(chkICSList.SelectedValue))
        {
            List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
        .Where(li => li.Selected)
        .Select(li => "'" + li.Value + "'")
        .ToList();
            if (selectedValues.Count > 0)
                chkSelectedVal = string.Join(",", selectedValues.ToArray());
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
        DataTable dt = (DataTable)Session["ProductionInfo"];
        Session["ProductionInfo"] = dt;
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
        gvPDetails.DataSource = sortedView;
        gvPDetails.DataBind();
    }
    protected void gvPDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_u = e.SortExpression.ToString();
        SortingUnitInfo(SortExpression_u);
    }
}