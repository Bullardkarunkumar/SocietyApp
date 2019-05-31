using MudarOrganic.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mudar_BranchsRolesEmployees : System.Web.UI.Page
{
    BranchsRolesEmployees_BL bre = new BranchsRolesEmployees_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnempbran();
            BindParentBranch();
            BindBranchDetails();
            BindRoles();
            BindEmployess();
        }
    }


    #region Branch
    /// <summary>
    /// Create By :Aslam
    /// Create Date: 7/2/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Add new Branch
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        divAddBranch.Visible = true;
        divBranchList.Visible = false;
        BranchclearControls();
    }
    /// <summary>
    /// Create By :Aslam
    /// Create Date: 7/2/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: update the change done in "divAddBranch"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBranSubmit_Click(object sender, EventArgs e)
    {
        string bid = string.Empty;
        string pBranchID = string.Empty;
        //if (ddlParentList.Items.Count > 0)
        //    pBranchID = ddlParentList.SelectedValue;
        if (string.IsNullOrEmpty(txtBranchID.Text))
            bid = bre.BranchDetails(txtBranchID.Text, txtBranchCode.Text, txtBranchName.Text, cbSales.Checked, cbExports.Checked, cbWareHouse.Checked, cbOther.Checked, txtContactPerson.Text, txtPhoneorFax.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtBankName.Text, txtBankACno.Text, txtBankADCCode.Text, txtIECode.Text, txtFDA.Text, txtAPVAT.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, pBranchID, "Shaik Aslam", DateTime.UtcNow, "", DateTime.UtcNow, 1, Convert.ToInt32(txtOrganicPremium.Text), txtTin.Text);
        else
            bid = bre.BranchDetails(txtBranchID.Text, txtBranchCode.Text, txtBranchName.Text, cbSales.Checked, cbExports.Checked, cbWareHouse.Checked, cbOther.Checked, txtContactPerson.Text, txtPhoneorFax.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtBankName.Text, txtBankACno.Text, txtBankADCCode.Text, txtIECode.Text, txtFDA.Text, txtAPVAT.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, pBranchID, "Shaik Aslam", DateTime.UtcNow, "Shaik Aslam", DateTime.UtcNow, 2, Convert.ToInt32(txtOrganicPremium.Text), txtTin.Text);
        BindBranchDetails();
        BranchclearControls();
        divAddBranch.Visible = false;
        divBranchList.Visible = true;
    }
    /// <summary>
    /// Create By :Aslam
    /// Create Date: 7/2/2012
    /// Modified By :
    /// Modified Date:
    /// Describtion: Clear button will clear all control under Add divAddBranch
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBranClear_Click(object sender, EventArgs e)
    {
        divAddBranch.Visible = false;
        divBranchList.Visible = true;
        BranchclearControls();
    }
    protected void gvBranchList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            BindBranchDetails(gvBranchList.DataKeys[index].Value.ToString());
        }
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            bre.BranchDetails(gvBranchList.DataKeys[index].Value.ToString(), 3, "Shaik Aslam");
            BindBranchDetails();
        }
    }
    #endregion

    #region Employee
    protected void btnEmpSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (ddlBranchID.SelectedValue == "Select..." || ddlBranchID.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('select the Branch');", true);
        }
        else
        {
            List<string> roles = new List<string>();
            foreach (ListItem role in lstAssignedRoles.Items)
            {
                roles.Add(role.Value);
            }
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
                result = bre.CreateEmployee(txtEmployeeName.Text, ddlBranchID.SelectedIndex > 0 ? ddlBranchID.SelectedValue : null, txtPhoneNumber.Text, txtMobileNumber.Text, txtEmpAddress.Text, txtEmpCity.Text, txtEmpTaluk.Text, txtEmpDistrict.Text, txtEmpState.Text, txtEmpCountry.Text, "Shaik Aslam", "", MudarApp.Insert, roles);
            else
                result = bre.UpdateEmployee(txtEmployeeID.Text, txtEmployeeName.Text, ddlBranchID.SelectedIndex > 0 ? ddlBranchID.SelectedValue : null, txtPhoneNumber.Text, txtMobileNumber.Text, txtEmpAddress.Text, txtEmpCity.Text, txtEmpTaluk.Text, txtEmpDistrict.Text, txtEmpState.Text, txtEmpCountry.Text, "Shaik Aslam", "", MudarApp.Update, roles);

            BindEmployess();
            ClearEmployess();
            divAddEmployee.Visible = false;
            divEmployeeList.Visible = true;
        }
    }

    protected void btnEmpClear_Click(object sender, EventArgs e)
    {
        divAddEmployee.Visible = false;
        divEmployeeList.Visible = true;
    }

    protected void btnEmployee_Click(object sender, EventArgs e)
    {
        divAddEmployee.Visible = true;
        divEmployeeList.Visible = false;
        ClearEmployess();
        BindEmployess();
        BindRolestoList();
        BindBranchDetailsList();
    }

    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_Selected")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            BindEmployess(gvEmployee.DataKeys[index].Value.ToString());
            divAddEmployee.Visible = true;
            divEmployeeList.Visible = false;
        }
        if (e.CommandName == "Cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            bre.DeleteEmployee(gvEmployee.DataKeys[index].Value.ToString(), "Shaik Aslam", MudarApp.Delete);
            BindEmployess();
            //bre.BranchDetails(gvBranchList.DataKeys[index].Value.ToString(), 3, "Shaik Aslam");
            //BindBranchDetails();
        }
    }

    #endregion

    #region Roles
    protected void btnRoleSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;
        if (string.IsNullOrEmpty(txtRoleId.Text))
            result = bre.Role_INT_UPT_DEL(Guid.NewGuid().ToString(), txtRoleName.Text, "Shaik Aslam", "", MudarApp.Insert);
        else
            result = bre.Role_INT_UPT_DEL(txtRoleId.Text, txtRoleName.Text, "", "Shaik Aslam", MudarApp.Update);
        BindRoles();
        txtRoleName.Text = string.Empty;
        divAddRoles.Visible = false;
        divRoles.Visible = true;
    }
    protected void btnRole_Click(object sender, EventArgs e)
    {
        divAddRoles.Visible = true;
        divRoles.Visible = false;
    }
    protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            bre.Role_INT_UPT_DEL(gvRoles.DataKeys[index].Value.ToString(), "", "", "Shaik Aslam", MudarApp.Delete);
            //bre.BranchDetails(gvRoles.DataKeys[index].Value.ToString(), 3, "Shaik Aslam");
            BindRoles();
            divAddRoles.Visible = false;
            divRoles.Visible = true;
        }
    }
    protected void btnPush_Click(object sender, EventArgs e)
    {
        divAddEmployee.Visible = true;
        for (int count = lstAvailableRoles.Items.Count - 1; count >= 0; count--)
        {
            if (lstAvailableRoles.Items[count].Selected)
            {
                lstAssignedRoles.Items.Add(lstAvailableRoles.Items[count]);
                ListItem temp = lstAvailableRoles.Items[count];
                lstAvailableRoles.Items.Remove(temp);
            }
        }
    }
    protected void btnPull_Click(object sender, EventArgs e)
    {
        divAddEmployee.Visible = true;
        for (int count = lstAssignedRoles.Items.Count - 1; count >= 0; count--)
        {
            if (lstAssignedRoles.Items[count].Selected)
            {
                lstAvailableRoles.Items.Add(lstAssignedRoles.Items[count]);
                ListItem temp = lstAssignedRoles.Items[count];
                lstAssignedRoles.Items.Remove(temp);
            }
        }
    }
    protected void btnRoleClear_Click(object sender, EventArgs e)
    {
        txtRoleName.Text = string.Empty;
        divRoles.Visible = true;
        divAddRoles.Visible = false;
    }
    #endregion

    #region Private Methods

    #region Branch operations
    private void BranchclearControls()
    {
        txtBranchID.Text = string.Empty;
        txtBranchCode.Text = string.Empty;
        txtBranchName.Text = string.Empty;
        cbExports.Checked = false;
        cbOther.Checked = false;
        cbSales.Checked = false;
        cbWareHouse.Checked = false;
        txtAddress.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtDistrict.Text = string.Empty;
        txtState.Text = string.Empty;
        txtTaluk.Text = string.Empty;
        txtCountry.Text = string.Empty;
        //ddlParentList.SelectedIndex = 0;
        txtContactPerson.Text = string.Empty;
        txtPhoneorFax.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtBankACno.Text = string.Empty;
        txtTin.Text = string.Empty;
        txtBankADCCode.Text = string.Empty;
        txtIECode.Text = string.Empty;
        txtFDA.Text = string.Empty;
        txtAPVAT.Text = string.Empty;
        txtOrganicPremium.Text = string.Empty;
    }

    private void BindParentBranch()
    {
        //ddlParentList.DataSource = bre.GetParentBranchDetails();
        //ddlParentList.DataTextField = "BranchName/Code";
        //ddlParentList.DataValueField = "BranchID";
        //ddlParentList.DataBind();
        //ddlParentList.Items.Insert(0, MudarApp.AddListItem());
    }

    private void BindBranchDetails()
    {
        gvBranchList.DataSource = bre.BranchDetails();
        gvBranchList.DataBind();
    }
    private void BindBranchDetailsList()
    {
        ddlBranchID.DataSource = bre.BranchDetails();
        ddlBranchID.DataTextField = "BranchCode";
        ddlBranchID.DataValueField = "BranchId";
        ddlBranchID.DataBind();
        ddlBranchID.Items.Insert(0, MudarApp.AddListItemWithDefaultValue());
    }

    private void BindBranchDetails(string BranchId)
    {
        try
        {
            DataTable Branch = bre.BranchDetails(BranchId);
            if (Branch.Rows.Count > 0)
            {
                divAddBranch.Visible = true;
                divBranchList.Visible = false;
                DataRow Bdr = Branch.Rows[0];
                txtBranchID.Text = Bdr["BranchId"].ToString();
                txtBranchCode.Text = Bdr["BranchCode"].ToString();
                txtBranchName.Text = Bdr["Bname"].ToString();
                txtContactPerson.Text = Bdr["ContactPerson"].ToString();
                txtPhoneorFax.Text = Bdr["Phone_Fax"].ToString();
                txtMobile.Text = Bdr["Mobile"].ToString();
                txtEmail.Text = Bdr["Email"].ToString();
                txtWebsite.Text = Bdr["website"].ToString();
                txtBankName.Text = Bdr["BankName"].ToString();
                txtBankACno.Text = Bdr["BankAcct_no"].ToString();
                txtBankADCCode.Text = Bdr["Bank_ADC_Code"].ToString();
                txtIECode.Text = Bdr["IECode"].ToString();
                txtFDA.Text = Bdr["FDA"].ToString();
                txtAPVAT.Text = Bdr["AP_VAT"].ToString();
                txtOrganicPremium.Text = Bdr["Organic_Premium"].ToString();
                txtTin.Text = Bdr["Tin"].ToString();
                txtAddress.Text = Bdr["Address"].ToString();
                txtCity.Text = Bdr["City"].ToString();
                txtDistrict.Text = Bdr["District"].ToString();
                txtTaluk.Text = Bdr["Taluk"].ToString();
                txtState.Text = Bdr["State"].ToString();
                txtCountry.Text = Bdr["Country"].ToString();
                //ddlParentList.ClearSelection();
                //ddlParentList.Items.FindByValue(Bdr["BranchHeadCode"].ToString()).Selected = true;
                //ddlParentList.SelectedIndex = 0;
                //cbExports.Checked = (bool)Bdr["Export"];
                //cbOther.Checked = (bool)Bdr["Other"];
                //cbSales.Checked = (bool)Bdr["Sales"];
                //cbWareHouse.Checked = (bool)Bdr["WareHousing"];

            }
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx", false);
        }
    }
    #endregion

    #region Roles Operation
    private void BindRoles()
    {
        gvRoles.DataSource = bre.GetRoleDetails();
        gvRoles.DataBind();
    }
    private void BindRolestoList()
    {
        lstAvailableRoles.DataSource = bre.GetRoleDetails();
        lstAvailableRoles.DataTextField = "RoleName";
        lstAvailableRoles.DataValueField = "RoleId";
        lstAvailableRoles.DataBind();
    }
    #endregion

    #region Employee Operation
    private void BindEmployess()
    {
        gvEmployee.DataSource = bre.GetEmployeeDetails();
        gvEmployee.DataBind();
    }
    private void ClearEmployess()
    {
        txtEmployeeID.Text = string.Empty;
        txtEmployeeName.Text = string.Empty;
        ddlBranchID.ClearSelection();
        if (ddlBranchID.Items.Count > 0)
            ddlBranchID.Items[0].Selected = true;
        txtEmpAddress.Text = string.Empty;
        txtEmpCity.Text = string.Empty;
        txtEmpTaluk.Text = string.Empty;
        txtEmpDistrict.Text = string.Empty;
        txtEmpState.Text = string.Empty;
        txtEmpCountry.Text = string.Empty;
        txtMobileNumber.Text = string.Empty;
        txtPhoneNumber.Text = string.Empty;
        lstAssignedRoles.Items.Clear();
    }
    private void BindEmployess(string EmployeeId)
    {
        divAddEmployee.Visible = true;
        BindRolestoList();
        BindBranchDetailsList();
        DataSet empDS = new DataSet();
        empDS = bre.GetEmployeeDetails(EmployeeId);
        if (empDS.Tables.Count > 1)
        {
            if (empDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = empDS.Tables[0].Rows[0];
                txtEmployeeID.Text = dr["EmployeeID"].ToString();
                txtEmployeeName.Text = dr["EmployeeFristName"].ToString();
                ddlBranchID.ClearSelection();
                ddlBranchID.Items.FindByValue(dr["BranchId"].ToString()).Selected = true;
                txtEmpAddress.Text = dr["Address"].ToString();
                txtEmpCity.Text = dr["city"].ToString();
                txtEmpTaluk.Text = dr["taluk"].ToString();
                txtEmpDistrict.Text = dr["district"].ToString();
                txtEmpState.Text = dr["state"].ToString();
                txtEmpCountry.Text = dr["country"].ToString();
                txtMobileNumber.Text = dr["MPhone"].ToString();
                txtPhoneNumber.Text = dr["Phone"].ToString();
            }
            if (empDS.Tables[1].Rows.Count > 0)
            {
                lstAssignedRoles.DataSource = empDS.Tables[1];
                lstAssignedRoles.DataTextField = "RoleName";
                lstAssignedRoles.DataValueField = "RoleId";
                lstAssignedRoles.DataBind();

                for (int count = lstAssignedRoles.Items.Count - 1; count >= 0; count--)
                {
                    //lstAvailableRoles.Items.Add(lstAssignedRoles.Items[count]);
                    ListItem temp = lstAvailableRoles.Items.FindByValue(lstAssignedRoles.Items[count].Value);
                    lstAvailableRoles.Items.Remove(temp);
                }
            }
        }
    }
    #endregion

    #endregion

    protected void lnkbtnICS_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "1";
    }

    protected void lnkbtnRoles_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "2";
    }

    protected void lnkbtnEmployee_Click(object sender, EventArgs e)
    {
        htnSelectedTab.Value = "3";
    }

    protected void btnICS_Click(object sender, EventArgs e)
    {

    }

    protected void btnRoles_Click(object sender, EventArgs e)
    {

    }

    protected void btnEmployee_Click1(object sender, EventArgs e)
    {

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
                tabpaneICS.Attributes.Add("class", "tab-pane active");
                tabpaneRoles.Attributes.Add("class", "tab-pane");
                tabpaneEmployee.Attributes.Add("class", "tab-pane");
                liICS.Attributes.Add("class", "active");
                liRoles.Attributes.Remove("class");
                liEmployee.Attributes.Remove("class");
                break;
            case "2":
                tabpaneRoles.Attributes.Add("class", "tab-pane active");
                tabpaneICS.Attributes.Add("class", "tab-pane");
                tabpaneEmployee.Attributes.Add("class", "tab-pane");
                liRoles.Attributes.Add("class", "active");
                liICS.Attributes.Remove("class");
                liEmployee.Attributes.Remove("class");
                break;
            case "3":
                tabpaneEmployee.Attributes.Add("class", "tab-pane active");
                tabpaneICS.Attributes.Add("class", "tab-pane");
                tabpaneRoles.Attributes.Add("class", "tab-pane");
                liEmployee.Attributes.Add("class", "active");
                liICS.Attributes.Remove("class");
                liRoles.Attributes.Remove("class");
                break;

        }
    }
}
