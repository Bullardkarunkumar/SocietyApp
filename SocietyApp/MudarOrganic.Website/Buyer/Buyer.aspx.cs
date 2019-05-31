using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Buyer_Buyer : System.Web.UI.Page
{
    bool result = false;
    Buyer_BL BBL = new Buyer_BL();
    BranchsRolesEmployees_BL BRE = new BranchsRolesEmployees_BL();
    Product_BL pr = new Product_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MasterControlbtnAddBuyer();
            BindProductList();
            divIndBuyer.Visible = false;
        }
    }
    protected void btnCompanyInfoSubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnContactSubmit_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnNotifySubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnBankSubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnPortSubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnPriceTermsSubmit_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnPaymentTermsSumit_Click(object sender, EventArgs e)
    {
        
    }
    #region Navigator
    protected void btnCompanyInfoNext_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lblBuyerID.Text))
            lblBuyerID.Text = Guid.NewGuid().ToString();
        string NewLoginId = MudarAutoGenerate.GenerateULogin(txtCompanyname.Text);
        string NewPassword = MudarAutoGenerate.GeneratePassword(txtCompanyname.Text);
        string Country = txtCountry.Text.ToUpper();
        string companyaddress = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;

        result = BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, txtCompanyname.Text, companyaddress, txtCity.Text, txtState.Text, txtZipCode.Text, Country, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty,txtTIN.Text,txtVAT.Text,txtCST.Text ,MudarApp.Insert,0);
        if (result)
        {
            
            result = BRE.UserLogin_INS_UPD_DEL(lblBuyerID.Text, NewLoginId, NewPassword, "Bhanu", "bhanu", MudarApp.Insert);
            if (result)
            {
                result = BRE.UserInRoles_INS_UPD_DEL(WebConfigurationManager.AppSettings["BuyerRole"].ToString(), lblBuyerID.Text, "Bhanu", "bhanu", MudarApp.Insert);
            }
        }
        if (txtCountry.Text.ToUpper() == "INDIA")
        {
            txtSea.Enabled = false;
            txtAir.Enabled = false;

            //price terms
            rbtnFOB.Enabled = false;
            rbtnCIFbySea.Enabled = false;
            rbtnCIFAirEandEUSA.Enabled = false;
            rbtnForDestination.Enabled = true;
            rbtnForDestination.Checked = true;
        }
        else
        {
            //price terms
            rbtnFOB.Enabled = true;
            rbtnFOB.Checked = true;
            rbtnCIFbySea.Enabled = true;
            rbtnCIFAirEandEUSA.Enabled = true;
            rbtnForDestination.Enabled = false;
        }
        MainBuyerView.ActiveViewIndex = 1;
        
    }
    protected void btnContactBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 0;
    }
    protected void btnContactNext_Click(object sender, EventArgs e)
    {
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty,string.Empty,string.Empty,string.Empty, 2,0);
        MainBuyerView.ActiveViewIndex = 8;
    }
    protected void btnNotifySkip_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 3;
    }
    protected void btnNotifyBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 8;
    }
    protected void btnNotifyNext_Click(object sender, EventArgs e)
    {
        string notifyaddress = txtNAddress1.Text + "@" + txtNAddress2.Text + "@" + txtNAddress3.Text;
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtNotifyName.Text, notifyaddress, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty,string.Empty,string.Empty, 3,0);
        MainBuyerView.ActiveViewIndex = 3;
    }
    protected void btnBankSkip_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 4;
    }
    protected void btnBankBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 2;
    }
    protected void btnBankNext_Click(object sender, EventArgs e)
    {
        string bankaddress = txtBAddress1.Text + "@" + txtBAddress2.Text + "@" + txtBAddress3.Text;
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 4,0);
        MainBuyerView.ActiveViewIndex = 4;
    }
    protected void btnPortBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 3;
    }
    protected void btnPortNext_Click(object sender, EventArgs e)
    {
        if (ddlTransportMode.SelectedValue == "--select--")
        {
            Response.Write("<script>alert('select the TransportMode');</script>");
        }
        else
        {
            result = BBL.BuyerTransPortDetails_INSandUPDandDEL(lblBuyerID.Text, Convert.ToInt32(ddlTransportMode.SelectedValue), txtSea.Text, txtAir.Text, txtRoad.Text, txtRail.Text, "Bhanu", string.Empty, MudarApp.Insert);
            MainBuyerView.ActiveViewIndex = 5;
        }
    }
    protected void btnPriceTermsBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 4;
    }
    protected void btnPriceTermsNext_Click(object sender, EventArgs e)
    {

        result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text, rbtnFOB.Checked, false, false, false, rbtnCIFbySea.Checked, rbtnCIFAirEandEUSA.Checked,false, rbtnForDestination.Checked, false, false, false, false, false, 0, "Bhanu", string.Empty, MudarApp.Insert);
        MainBuyerView.ActiveViewIndex = 6;
    }
    protected void btnPaymentTermsBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 5;
    }
    protected void btnPaymentTermsNext_Click(object sender, EventArgs e)
    {
        result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text, false, false, false, false, false, false, false, false, false, rbtnHundperAdvance.Checked, rbtnFiftyfityAgnistDocs.Checked, rbtnHundAgnistDocs.Checked, rbtnNoofInvDate.Checked, Convert.ToInt32(ddlInvNoofDays.SelectedValue), "Bhanu", string.Empty, MudarApp.Update);
        MainBuyerView.ActiveViewIndex = 7;
    }
    #endregion
    protected void rbtnFOB_CheckedChanged(object sender, EventArgs e)
    {

    }
    private void BindTransportMode()
    {
       
    }
    private void BindProductList()
    {
        DataTable dtProductList = new DataTable();
        gvProductList.DataSource = pr.GetProductDetails();
        gvProductList.DataBind();
    }
    
    protected void btnProductNext_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 2;
        result=BBL.BuyerProductInsertDetails(lblBuyerID.Text, 0, "bhanu", string.Empty, MudarApp.Delete);
        foreach (GridViewRow gvr in gvProductList.Rows)
        {
            if ((gvr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
            {
                result = BBL.BuyerProductInsertDetails(lblBuyerID.Text, Convert.ToInt32(gvProductList.DataKeys[gvr.RowIndex].Values["ProductId"]), "bhanu", string.Empty, MudarApp.Insert);
                    
            } 
        }
    }
    protected void btnProductBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 1;
    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        if (txtCountry.Text.ToUpper() == "INDIA")
        {
            divIndBuyer.Visible = true;
            txtTIN.Focus();
        }
    }
    protected void ddlInvNoofDays_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
