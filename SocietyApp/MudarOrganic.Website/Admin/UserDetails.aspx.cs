using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;

public partial class Admin_UserDetails : System.Web.UI.Page
{
    UserInfo_BL UI = new UserInfo_BL();
    string userid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnUserEdit();
            divgvUserDetails.Visible = false;
            divUserEdit.Visible = false;
        }
    }
    protected void btnTAdmin_Click(object sender, EventArgs e)
    {
        divgvUserDetails.Visible = true;
        divICSsupplierdetails.Visible = false;
        divBueyrDetails.Visible = false;
        lblRoleID.Text = WebConfigurationManager.AppSettings["AdminRole"].ToString();
        BindUserDetails(lblRoleID.Text);
        btnTAdmin.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTAdmin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnTBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBranch.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTOthers.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTOthers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnTBranch_Click(object sender, EventArgs e)
    {
        btnTBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTBranch.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnTAdmin.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTAdmin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTOthers.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTOthers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        divgvUserDetails.Visible = false;
        divBueyrDetails.Visible = false;
        divICSsupplierdetails.Visible = false;
        lblRoleID.Text = WebConfigurationManager.AppSettings["BranchRole"].ToString();
        BindUserDetails(lblRoleID.Text);
        divgvUserDetails.Visible = true;
    }
    protected void btnTBuyer_Click(object sender, EventArgs e)
    {
        btnTBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnTBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBranch.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTAdmin.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTAdmin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBranch.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTOthers.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTOthers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        divgvUserDetails.Visible = false;
        divBueyrDetails.Visible = true;
        divICSsupplierdetails.Visible = false;
        lblRoleID.Text = WebConfigurationManager.AppSettings["BuyerRole"].ToString();
        DataTable dt = UI.GetUserBuyerDetails(lblRoleID.Text);
        if (dt.Rows.Count > 0)
        {
            gvBuyerDetails.DataSource = dt;
            gvBuyerDetails.DataBind();
        }
    }
    protected void btnTOthers_Click(object sender, EventArgs e)
    {
        divgvUserDetails.Visible = false;
        divBueyrDetails.Visible = false;
        divICSsupplierdetails.Visible = true;
        lblRoleID.Text = WebConfigurationManager.AppSettings["SupplierRole"].ToString();
        BindSupplierDetails(lblRoleID.Text);
        btnTOthers.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTOthers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //normal view
        btnTBranch.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBranch.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        btnTAdmin.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        btnTAdmin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_edit")
        {
            divUserEdit.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            lblUserID.Text = gvUserDetails.DataKeys[index].Value.ToString();
            txtUsername.Focus();
            txtUsername.Text = gvUserDetails.Rows[index].Cells[4].Text.ToString();
            txtPassword.Text = gvUserDetails.Rows[index].Cells[5].Text.ToString();
        }
        BindUserDetails(lblRoleID.Text);
    }
    protected void gvBuyerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_edit_Buyer")
        {
            divUserEdit.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            lblUserID.Text = gvBuyerDetails.DataKeys[index].Value.ToString();
            txtUsername.Focus();
            txtUsername.Text = gvBuyerDetails.Rows[index].Cells[2].Text.ToString();
            txtPassword.Text = gvBuyerDetails.Rows[index].Cells[3].Text.ToString();
        }
        BindUserDetails(lblRoleID.Text);
    }
    protected void gvICSsupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_edit_Supplier")
        {
            divUserEdit.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            lblUserID.Text = gvICSsupplier.DataKeys[index].Value.ToString();
            txtUsername.Focus();
            txtUsername.Text = gvICSsupplier.Rows[index].Cells[2].Text.ToString();
            txtPassword.Text = gvICSsupplier.Rows[index].Cells[3].Text.ToString();
        }
        BindUserDetails(lblRoleID.Text);
    }
    protected void btnUserClear_Click(object sender, EventArgs e)
    {
        divUserEdit.Visible = false;
    }
    protected void btnUserSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = false;
            if (UI.CheckUserExist(txtUsername.Text))
            {
                txtUsername.Focus();
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('user name already exists !!! try new one');</script>");
            }
            else
            {
                result = UI.UpdateUserLoginDetails(lblUserID.Text, txtUsername.Text, txtPassword.Text, "Bhanu", MudarApp.Update);
                if (result == true)
                {
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    if (lblRoleID.Text == "82AD1024-6F5D-4B0D-9829-47E6BE304786")
                        BindUserDetails(lblRoleID.Text);
                    if (lblRoleID.Text == "604F2B45-D580-4E63-BD1F-9324DE1E2560")
                        BindBuyerDetails(lblRoleID.Text);
                    if (lblRoleID.Text == "A7112120-B136-4407-A5F9-FA9A6CB2254F")
                        BindSupplierDetails(lblRoleID.Text);
                    divUserEdit.Visible = false;
                    lblUserID.Text = string.Empty;
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Updated userdetails Successfully');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Saved failed');</script>");
                }
            }
        }
        catch (Exception ex)
        {
           Response.Write(ex.Message);
        }
    }
    public void BindUserDetails(string RoleID)
    {
        DataTable dt = UI.GetUserLoginDetailsBasedonRole(RoleID);
        if (dt.Rows.Count > 0)
        {
            gvUserDetails.DataSource = dt;
            gvUserDetails.DataBind();
        }
    }
    public void BindBuyerDetails(string RoleID)
    {
        DataTable dt = UI.GetUserBuyerDetails(lblRoleID.Text);
        if (dt.Rows.Count > 0)
        {
            gvBuyerDetails.DataSource = dt;
            gvBuyerDetails.DataBind();
        }
    }
    public void BindSupplierDetails(string RoleID)
    {
        DataTable dt = UI.GetSupplierLoginDetails(RoleID);
        if (dt.Rows.Count > 0)
        {
            gvICSsupplier.DataSource = dt;
            gvICSsupplier.DataBind();
        }
    }
}
