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


public partial class Masters_Membershipform : System.Web.UI.Page
{
    Membership_BL mebmershipDl = new Membership_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindNomineeRelation();
        }
    }

    private void BindNomineeRelation()
    {
        ListItemCollection items = new ListItemCollection() {
            new ListItem { Text = "Father", Value = "Father" },
            new ListItem { Text = "Mother", Value = "Mother" },
            new ListItem { Text = "Husband", Value = "Husband" },
            new ListItem { Text = "Wife", Value = "Wife" },
            new ListItem { Text = "Son", Value = "Son" },
            new ListItem { Text = "Daughter", Value = "Daughter" },
            new ListItem { Text = "Brother", Value = "Brother" },
            new ListItem { Text = "Sister", Value = "Sister" }
        };
        items.Insert(0, new ListItem { Text = "--Select--", Value = string.Empty, Selected = false });
        foreach (ListItem item in items)
            ddlNomineeDetails.Items.Add(item);
        ddlNomineeDetails.DataBind();
        ddlNomineeDetails.SelectedValue = "--Select--";
        GetAdmissionNumber();
    }

    private void GetAdmissionNumber()
    {
        txtAdmissionNumber.Text = Convert.ToString(mebmershipDl.GetAdmissionNumber() + 1);
    }

    protected void btnDetailed_Click(object sender, EventArgs e)
    {      
        MembershipViewModel membership = new MembershipViewModel()
        {
            AadhaarNumber = txtAadhaarNumber.Text.Length>0? Convert.ToInt64(txtAadhaarNumber.Text):0,
            Address = txtAddress.Text,
            AdmissionNumber = txtAdmissionNumber.Text.Length>0? Convert.ToInt64(txtAdmissionNumber.Text):0,
            Area = txtArea.Text,
            DoB = Convert.ToDateTime(txtDoB.Text),
            EntranceFee = txtEntranceFee.Text,
            FatherName = txtFatherName.Text,
            MemberName = txtMemberName.Text,
            MobileNumber = txtMemberName.Text,
            NomineeName = txtNomineeName.Text,
            NomineeRelation = ddlNomineeDetails.SelectedIndex == 0 ? "" : ddlNomineeDetails.SelectedValue,
            NoofShares = txtNoofShares.Text.Length>0? Convert.ToInt32(txtNoofShares.Text):0,
            PanNumber = txtPanNumber.Text.Length>0? Convert.ToInt32(txtPanNumber.Text):0,
            ShareValue = txtShareValue.Text.Length>0? Convert.ToInt32(txtShareValue.Text):0,
            SpouseName = txtSpouseName.Text,
            TotalShareAmt = txtNoofShares.Text.Length>0? Convert.ToInt32(txtNoofShares.Text) * Convert.ToInt32(txtShareValue.Text):0 //Convert.ToInt32(txtTotalShareAmt.Text)
        };
        bool result = mebmershipDl.AddMemmbershipDetails(membership);
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('New member Saved Successfully');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "fnShowMessage('Membership Save failed');", true);
        }
        // ClearMemberShipForm();
    }

    private void ClearMemberShipForm()
    {
        txtAadhaarNumber.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtAdmissionNumber.Text = string.Empty;
        txtArea.Text = string.Empty;
        txtDoB.Text = string.Empty;
        txtEntranceFee.Text = string.Empty;
        txtMemberName.Text = string.Empty;
        txtNomineeName.Text = string.Empty;
        txtNoofShares.Text = string.Empty;
        txtPanNumber.Text = string.Empty;
        txtShareValue.Text = string.Empty;
        txtSpouseName.Text = string.Empty;
        txtTotalShareAmt.Text = string.Empty;
        ddlNomineeDetails.ClearSelection();
        BindNomineeRelation();

    }    

    protected void txtNoofShares_TextChanged(object sender, EventArgs e)
    {
        txtTotalShareAmt.Text = txtNoofShares.Text.Length > 0 ? Convert.ToString(Convert.ToInt32(txtNoofShares.Text) * Convert.ToInt32(txtShareValue.Text)) : "0";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Masters/Membershipform.aspx");
        //ClearTextBoxes(Page);
    }

    private void ClearTextBoxes(Control p1)
    {
        foreach (Control ctrl in p1.Controls)
        {
            if (ctrl is TextBox)
            {
                TextBox t = ctrl as TextBox;

                if (t != null)
                {
                    t.Text = String.Empty;
                }
            }
            else
            {
                if (ctrl.Controls.Count > 0)
                {
                    ClearTextBoxes(ctrl);
                }
            }
        }
    }
}

