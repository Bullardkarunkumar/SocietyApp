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
using MudarOrganic.BL;
using System.Text;

public partial class Admin_UnitInformation : System.Web.UI.Page
{
    UnitInformation_BL ui = new UnitInformation_BL();
    public static string SortExpression_u = "UnitId";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnUnitInfo();
            BindUnitDeatils();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result;
            StringBuilder strVillagesList = new StringBuilder();
            foreach (ListItem Village in lstAssignedVillages.Items)
            {
                strVillagesList.Append(Village.Text);
                strVillagesList.Append(";");
            }
            if (string.IsNullOrEmpty(lblUnitID.Text))
                result = ui.UnitInformationDetails_INSandUPDandDEL_new(string.Empty, txtUnitName.Text, txtUnitCode.Text, txtUnitOwner.Text, txtUAddress.Text, Convert.ToInt32(txtRaw.Text), txtOStaate.Text, txtOMaterial.Text, txtCapacity.Text, txtLotsof.Text, Convert.ToInt32(txtPLabour.Text), Convert.ToInt32(txtTLabour.Text), Convert.ToInt32(txtCLabour.Text), "Bhanu", string.Empty, MudarApp.Insert, strVillagesList.ToString());
            else
                result = ui.UnitInformationDetails_INSandUPDandDEL_new(lblUnitID.Text, txtUnitName.Text, txtUnitCode.Text, txtUnitOwner.Text, txtUAddress.Text, Convert.ToInt32(txtRaw.Text), txtOStaate.Text, txtOMaterial.Text, txtCapacity.Text, txtLotsof.Text, Convert.ToInt32(txtPLabour.Text), Convert.ToInt32(txtTLabour.Text), Convert.ToInt32(txtCLabour.Text), "Bhanu", string.Empty, MudarApp.Update, strVillagesList.ToString());
            divUnitInfoForm.Visible = false;
            BindUnitDeatils();
            ClearControls();
            divUnitBindDetails.Visible = true;
            btnAddUnit.Visible = true;
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/UnitInformation.aspx");
    }
    private void ClearControls()
    {
        txtCapacity.Text = string.Empty;
        txtLotsof.Text = string.Empty;
        txtOMaterial.Text = string.Empty;
        txtOStaate.Text = string.Empty;
        txtRaw.Text = string.Empty;
        txtUAddress.Text = string.Empty;
        txtUnitCode.Text = string.Empty;
        txtUnitName.Text = string.Empty;
        txtUnitOwner.Text = string.Empty;
        lstAssignedVillages.Items.Clear();
        BindVillagesList();
    }
    private void BindUnitDeatils()
    {
        DataTable dtUnitInfo = ui.UnitInformation();
        if (dtUnitInfo.Rows.Count > 0)
        {
            Session["UnitInfo"] = null;
            Session["UnitInfo"] = dtUnitInfo;
            gvUnitInfo.DataSource = (DataTable)Session["UnitInfo"];
            gvUnitInfo.DataBind();
            SortingUnitInfo(SortExpression_u);
           
        }
    }
    protected void gvUnitInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            BindUnitDetailsBasedOnUnit(gvUnitInfo.DataKeys[index].Value.ToString());
            lblUnitID.Text = gvUnitInfo.DataKeys[index].Value.ToString();
        }
    }
    private void BindUnitDetailsBasedOnUnit(string unitid)
    {
        divUnitBindDetails.Visible = false;
        btnAddUnit.Visible = false;
        BindVillagesList();
        lstAssignedVillages.Items.Clear();
        DataSet unitDetailsDS =   new DataSet();
        unitDetailsDS = ui.UnitInformation(unitid);
        if (unitDetailsDS.Tables.Count > 0)
        {
            if (unitDetailsDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = unitDetailsDS.Tables[0].Rows[0];
                divUnitInfoForm.Visible = true;
                txtUnitCode.Text = dr["Ucode"].ToString();
                txtUnitName.Text = dr["Name"].ToString();
                txtUnitOwner.Text = dr["Uowner"].ToString();
                txtUAddress.Text = dr["Address"].ToString();
                txtCapacity.Text = dr["CapacityOfPlant"].ToString();
                txtLotsof.Text = dr["LotsOfProducesSimultaneously"].ToString();
                txtOMaterial.Text = dr["OutputMaterial"].ToString();
                txtOStaate.Text = dr["OutputState"].ToString();
                txtRaw.Text = dr["RawRequired"].ToString();
                string[] Village = dr["Unit_Village"].ToString().Split(';');
                for (int temp = 0; temp <= Village.Length - 2; temp++)
                {
                    string text = Village[temp].ToString();
                    lstAssignedVillages.Items.Add(text);
                    if (lstAssignedVillages.Items.Count > 0)
                    {
                        ListItem item = lstAvailableVillages.Items.FindByText(text);
                        lstAvailableVillages.Items.Remove(item);    
                    }
                }
            }
        }
    }
    protected void btnPush_Click(object sender, EventArgs e)
    {
        for (int count = lstAvailableVillages.Items.Count - 1; count >= 0; count--)
        {
            if (lstAvailableVillages.Items[count].Selected)
            {
                lstAssignedVillages.Items.Add(lstAvailableVillages.Items[count]);
                ListItem temp = lstAvailableVillages.Items[count];
                lstAvailableVillages.Items.Remove(temp);
            }
        }
    }
    protected void btnPull_Click(object sender, EventArgs e)
    {
        for (int count = lstAssignedVillages.Items.Count - 1; count >= 0; count--)
        {
            if (lstAssignedVillages.Items[count].Selected)
            {
                lstAvailableVillages.Items.Add(lstAssignedVillages.Items[count]);
                ListItem temp = lstAssignedVillages.Items[count];
                lstAssignedVillages.Items.Remove(temp);
            }
        }
    }
    protected void btnAddUnit_Click(object sender, EventArgs e)
    {
        divUnitBindDetails.Visible = false;
        divUnitInfoForm.Visible = true;
        BindVillagesList();
        btnAddUnit.Visible = false;
    }
    private void BindVillagesList()
    {
        lstAvailableVillages.DataSource = ui.FarmersVillageList();
        lstAvailableVillages.DataTextField = "City_Village";
        lstAvailableVillages.DataValueField = "City_Village";
        lstAvailableVillages.DataBind();
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
        gvUnitInfo.DataSource = sortedView;
        gvUnitInfo.DataBind();
    }
    protected void gvUnitInfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_u = e.SortExpression.ToString();
        SortingUnitInfo(SortExpression_u);

    }
}
