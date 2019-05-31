using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;
using Society.Models;

public partial class Masters_TermDepositApplication : System.Web.UI.Page
{
    TermDepositApplication_BL TermDepositApplicationBL = new TermDepositApplication_BL();
    ListItemCollection DDLitems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDepositDate.Text = DateTime.Now.ToShortDateString();
            txtByDate.Text = DateTime.Now.ToShortDateString();
            BindNomineeRelation();
            GetTenureDetails();
        }
    }

    private void GetTenureDetails()
    {
        var tenureList = TermDepositApplicationBL.GetTenureDetails();
        ListItemCollection ddlTenureList = new ListItemCollection();
        foreach (var item in tenureList)
        {
            ListItem _item = new ListItem() { Text = item.TenureName, Value = item.RateOfIntrest.ToString() };
            ddlTenureList.Add(_item);
        }
        ddlTenureList.Insert(0, new ListItem { Text = "--Select--", Value = string.Empty, Selected = false });
        foreach (ListItem item in ddlTenureList)
            ddlTenure.Items.Add(item);
        ddlTenure.DataBind();
        ddlTenure.SelectedValue = "--Select--";
        GetDepositeNumber();
    }

    private void GetDepositeNumber()
    {
        txtDepositNumber.Text = Convert.ToString(TermDepositApplicationBL.GetDepositeNumber() + 1);
    }

    private void BindNomineeRelation(bool isPostback = false)
    {
        DDLitems = new ListItemCollection() {
            new ListItem { Text = "Father", Value = "Father" },
            new ListItem { Text = "Mother", Value = "Mother" },
            new ListItem { Text = "Husband", Value = "Husband" },
            new ListItem { Text = "Wife", Value = "Wife" },
            new ListItem { Text = "Son", Value = "Son" },
            new ListItem { Text = "Daughter", Value = "Daughter" },
            new ListItem { Text = "Brother", Value = "Brother" },
            new ListItem { Text = "Sister", Value = "Sister" }
        };
        DDLitems.Insert(0, new ListItem { Text = "--Select--", Value = string.Empty, Selected = false });
        foreach (ListItem item in DDLitems)
            ddlNomineeRelationTDA.Items.Add(item);
        ddlNomineeRelationTDA.DataBind();
        ddlNomineeRelationTDA.SelectedValue = "--Select--";
    }

    private void BindMemberDetails(string input, bool isName)
    {

        var MemberDetails = TermDepositApplicationBL.GetMemberDetails(input, isName);
        if (MemberDetails != null)
        {
            txtAdmissionNumber.Text = Convert.ToString(MemberDetails.MemberNo);
            txtName.Text = MemberDetails.MemberName;
            txtNomineeNameTDA.Text = MemberDetails.NomineeName;
        }
    }

    protected void txtAdmissionNumber_TextChanged(object sender, EventArgs e)
    {
        if (txtAdmissionNumber.Text.Length > 2)
        {
            BindMemberDetails(txtAdmissionNumber.Text, false);
        }
    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        if (txtName.Text.Length > 2)
        {
            BindMemberDetails(txtName.Text, true);
        }
    }

    //protected void ddlTenure_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtRateOfIntrest.Text = ddlTenure.SelectedItem.Value;
    //}

    protected void ddlTenure_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRateOfIntrest.Text = ddlTenure.SelectedItem.Value;
        string chk = "3 Years to 5 Years".Trim().ToLower();
        if (ddlTenure.SelectedItem.Text.Trim().ToLower() == chk)
        {
            ddlFrequencyType.Enabled = true;
        }
        else
        {
            ddlFrequencyType.Enabled = false;
        }
    }
}