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

public partial class BuyerReg : System.Web.UI.Page
{
    bool result = false;
    Buyer_BL BBL = new Buyer_BL();
    BranchsRolesEmployees_BL BRE = new BranchsRolesEmployees_BL();
    Product_BL pr = new Product_BL();
    int bankorconsignee = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        int Days = 0;

        if (!IsPostBack)
        {
            BindProductList();
            DisableButtons();
            //EnableBindBankDetails();
        }
    }
    private void EnableBindBankDetails()
    {
        txtBankname.Enabled = false;
        txtBAddress1.Enabled = false;
        txtBAddress2.Enabled = false;
        txtBAddress3.Enabled = false;
        txtBCity.Enabled = false;
        txtBCountry.Enabled = false;
        txtBState.Enabled = false;
        txtBZipcode.Enabled = false;
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
        if (txtCountry.Text.ToUpper() == "INDIA")
        {
            txtSea.Enabled = false;
            txtAir.Enabled = false;
            divIndBuyer.Visible = true;
            //price terms
            rbtnFOB.Enabled = false;
            rbtnCIFbySea.Enabled = false;
            rbtnCIFAirEandEUSA.Enabled = false;
            rbtnForDestination.Enabled = true;
            rbtnForDestination.Checked = true;
            //payment terms
            rbtnHundperAdvance.Checked = true;
            //if (!string.IsNullOrEmpty(txtTIN.Text))
            InsertData();
        }
        else
        {
            lblCustomBroker.Visible = true;
            //price terms
            rbtnFOB.Enabled = true;
            rbtnFOB.Checked = true;
            lblCustomBroker.Visible = true;
            rbtnCIFbySea.Enabled = true;
            rbtnCIFAirEandEUSA.Enabled = true;
            rbtnForDestination.Enabled = false;
            //payment terms
            rbtnHundperAdvance.Checked = true;
            txtRoad.Enabled = false;
            txtRail.Enabled = false;
            InsertData();
        }
        licompanydetails.Attributes["class"] = "done";
        tabCompanyDetails.Attributes["class"] = "tab-pane";

        licontactdetails.Attributes["class"] = "active";
        tabContactDetails.Attributes["class"] = "tab-pane active";

        liproductdetails.Attributes["class"] = "";
        tabProductDetails.Attributes["class"] = "tab-pane";

        liportdetails.Attributes["class"] = "";
        tabPortDetails.Attributes["class"] = "tab-pane";

        liFrightTerms.Attributes["class"] = "";
        tabFrightTerms.Attributes["class"] = "tab-pane";

        lipaymentterms.Attributes["class"] = "";
        tabPaymentTerms.Attributes["class"] = "tab-pane";

        divProgress.Attributes["style"] = "width:33.4%";
        spanCount.InnerHtml = "2";
    }
    private void InsertData()
    {
        if (string.IsNullOrEmpty(lblBuyerID.Text))
            lblBuyerID.Text = Guid.NewGuid().ToString();
        string NewLoginId = MudarAutoGenerate.GenerateULogin(txtCompanyname.Text);
        string NewPassword = MudarAutoGenerate.GeneratePassword(txtCompanyname.Text);
        string Country = txtCountry.Text.ToUpper();
        string companyaddress = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;
        result = BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, txtCompanyname.Text, companyaddress, txtCity.Text, txtState.Text, txtZipCode.Text, Country, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, txtTIN.Text, txtVAT.Text, txtCST.Text, MudarApp.Insert, 0);
        if (result)
        {
            result = BRE.UserLogin_INS_UPD_DEL(lblBuyerID.Text, NewLoginId, NewPassword, "Bhanu", "bhanu", MudarApp.Insert);
            if (result)
                result = BRE.UserInRoles_INS_UPD_DEL(WebConfigurationManager.AppSettings["BuyerRole"].ToString(), lblBuyerID.Text, "Bhanu", "bhanu", MudarApp.Insert);
        }
        //MainBuyerView.ActiveViewIndex = 1;
        //btnTBuyerInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTBuyerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTContactInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnContactBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 0;
    }
    protected void btnContactNext_Click(object sender, EventArgs e)
    {
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 2, 0);

        //MainBuyerView.ActiveViewIndex = 8;
        //btnTContactInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTContactInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTProductInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        licompanydetails.Attributes["class"] = "done";
        tabCompanyDetails.Attributes["class"] = "tab-pane";

        licontactdetails.Attributes["class"] = "done";
        tabContactDetails.Attributes["class"] = "tab-pane";

        liproductdetails.Attributes["class"] = "active";
        tabProductDetails.Attributes["class"] = "tab-pane active";

        liportdetails.Attributes["class"] = "";
        tabPortDetails.Attributes["class"] = "tab-pane";

        lipaymentterms.Attributes["class"] = "";
        tabPaymentTerms.Attributes["class"] = "tab-pane";

        liFrightTerms.Attributes["class"] = "";
        tabFrightTerms.Attributes["class"] = "tab-pane";

        divProgress.Attributes["style"] = "width:50.1%";
        spanCount.InnerHtml = "3";
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
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtNotifyName.Text, notifyaddress, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 3, 0);
        MainBuyerView.ActiveViewIndex = 3;
        btnTNotifyInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTNotifyInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
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
        BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 4, bankorconsignee);
        MainBuyerView.ActiveViewIndex = 4;
        btnTBankInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTPortInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
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
            //MainBuyerView.ActiveViewIndex = 5;
            licompanydetails.Attributes["class"] = "done";
            tabCompanyDetails.Attributes["class"] = "tab-pane";

            licontactdetails.Attributes["class"] = "done";
            tabContactDetails.Attributes["class"] = "tab-pane";

            liproductdetails.Attributes["class"] = "done";
            tabProductDetails.Attributes["class"] = "tab-pane";

            liportdetails.Attributes["class"] = "done";
            tabPortDetails.Attributes["class"] = "tab-pane";

            liFrightTerms.Attributes["class"] = "active";
            tabFrightTerms.Attributes["class"] = "tab-pane active";

            lipaymentterms.Attributes["class"] = "";
            tabPaymentTerms.Attributes["class"] = "tab-pane";

            divProgress.Attributes["style"] = "width:83.5%";
            spanCount.InnerHtml = "5";
        }
        //btnTPortInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTPortInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTPriceTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");


    }
    protected void btnPriceTermsBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 4;
    }
    protected void btnPriceTermsNext_Click(object sender, EventArgs e)
    {
        result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text, rbtnFOB.Checked, false, false, false, rbtnCIFbySea.Checked, rbtnCIFAirEandEUSA.Checked, false, rbtnForDestination.Checked, false, false, false, false, false, 0, "Bhanu", string.Empty, MudarApp.Insert);
        //MainBuyerView.ActiveViewIndex = 6;
        //btnTPriceTerms.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTPriceTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //btnTPaymentTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

        licompanydetails.Attributes["class"] = "done";
        tabCompanyDetails.Attributes["class"] = "tab-pane";

        licontactdetails.Attributes["class"] = "done";
        tabContactDetails.Attributes["class"] = "tab-pane";

        liproductdetails.Attributes["class"] = "done";
        tabProductDetails.Attributes["class"] = "tab-pane";

        liportdetails.Attributes["class"] = "done";
        tabPortDetails.Attributes["class"] = "tab-pane";

        liFrightTerms.Attributes["class"] = "done";
        tabFrightTerms.Attributes["class"] = "tab-pane";

        lipaymentterms.Attributes["class"] = "active";
        tabPaymentTerms.Attributes["class"] = "tab-pane active";

        divProgress.Attributes["style"] = "width:100%";
        spanCount.InnerHtml = "6";
    }
    protected void btnPaymentTermsBack_Click(object sender, EventArgs e)
    {
        MainBuyerView.ActiveViewIndex = 5;
    }
    protected void btnPaymentTermsNext_Click(object sender, EventArgs e)
    {
        result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text, false, false, false, false, false, false, false, false, false, rbtnHundperAdvance.Checked, rbtnFiftyfityAgnistDocs.Checked, rbtnHundAgnistDocs.Checked, rbtnNoofInvDate.Checked, Convert.ToInt32(ddlInvNoofDays.SelectedValue), "Bhanu", string.Empty, MudarApp.Update);

        if (result)
            ScriptManager.RegisterStartupScript(this, typeof(Page), "message", "fnShowMessage('Thank You We will contact you on your email within next 24 hours with username and Password')", true);
        //MainBuyerView.ActiveViewIndex = 7;
        //btnTPaymentTerms.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnTPaymentTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        //divTBuyerDetails.Visible = false;
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
        gvProductList.DataSource = pr.GetAllProducDetails();
        gvProductList.DataBind();
    }
    protected void btnProductNext_Click(object sender, EventArgs e)
    {
        int checkCount = 0;

        result = BBL.BuyerProductInsertDetails(lblBuyerID.Text, 0, "bhanu", string.Empty, MudarApp.Delete);
        foreach (GridViewRow gvr in gvProductList.Rows)
        {
            if ((gvr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
            {
                checkCount += 1;
                result = BBL.BuyerProductInsertDetails(lblBuyerID.Text, Convert.ToInt32(gvProductList.DataKeys[gvr.RowIndex].Values["ProductId"]), "bhanu", string.Empty, MudarApp.Insert);
                //btnTProductInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
                //btnTProductInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
                //btnTNotifyInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                //MainBuyerView.ActiveViewIndex = 2;
                licompanydetails.Attributes["class"] = "done";
                tabCompanyDetails.Attributes["class"] = "tab-pane";

                licontactdetails.Attributes["class"] = "done";
                tabContactDetails.Attributes["class"] = "tab-pane";

                liproductdetails.Attributes["class"] = "done";
                tabProductDetails.Attributes["class"] = "tab-pane";

                liportdetails.Attributes["class"] = "active";
                tabPortDetails.Attributes["class"] = "tab-pane active";

                lipaymentterms.Attributes["class"] = "";
                tabPaymentTerms.Attributes["class"] = "tab-pane";

                liFrightTerms.Attributes["class"] = "";
                tabFrightTerms.Attributes["class"] = "tab-pane";

                divProgress.Attributes["style"] = "width:66.8%";
                spanCount.InnerHtml = "4";
                break;

            }
        }
        if (checkCount <= 0)
        {
            Show("!!! Please select atleast one product !!!");
            return;
        }


    }
    public static void Show(string message)
    {
        string cleanMessage = message.Replace("'", "\'");
        Page page = HttpContext.Current.CurrentHandler as Page;
        string script = string.Format("alert('{0}');", cleanMessage);
        if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
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
            //divIndBuyer.Visible = true;
            //txtTIN.Focus();
            txtRoad.Enabled = true;
            txtRail.Enabled = true;
        }
        else
        {
            txtRoad.Enabled = false;
            txtRail.Enabled = false;
        }
    }
    private void DisableButtons()
    {
        btnTBuyerInfo.Enabled = false;
        btnTContactInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTContactInfo.Enabled = false;
        btnTProductInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTProductInfo.Enabled = false;
        btnTNotifyInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTNotifyInfo.Enabled = false;
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTBankInfo.Enabled = false;
        btnTPortInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTPortInfo.Enabled = false;
        btnTPriceTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTPriceTerms.Enabled = false;
        btnTPaymentTerms.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTPaymentTerms.Enabled = false;
    }
    #region Buyer Taborder Details
    protected void btnTBuyerInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTContactInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTProductInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTNotifyInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTBankInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTPortInfo_Click(object sender, EventArgs e)
    {

    }
    protected void btnTPriceTerms_Click(object sender, EventArgs e)
    {

    }
    protected void btnTPaymentTerms_Click(object sender, EventArgs e)
    {

    }
    #endregion
    protected void rbtnNoofInvDate_CheckedChanged(object sender, EventArgs e)
    {
        ddlInvNoofDays.Visible = true;
    }
    protected void ddlInvNoofDays_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cdNotify_CheckedChanged(object sender, EventArgs e)
    {
        if (cdNotify.Checked)
        {
            txtNotifyName.Text = txtCompanyname.Text;
            txtNAddress1.Text = txtAddress1.Text;
            txtNAddress2.Text = txtAddress2.Text;
            txtNAddress3.Text = txtAddress3.Text;
            txtNCity.Text = txtCity.Text;
            txtNCountry.Text = txtCountry.Text;
            txtNState.Text = txtState.Text;
            txtNZipCode.Text = txtZipCode.Text;
        }
        else
        {
            txtNotifyName.Text = string.Empty;
            txtNAddress1.Text = string.Empty;
            txtNAddress2.Text = string.Empty;
            txtNAddress3.Text = string.Empty;
            txtNCity.Text = string.Empty;
            txtNCountry.Text = string.Empty;
            txtNState.Text = string.Empty;
            txtNZipCode.Text = string.Empty;
        }
    }
    protected void cbconsignee_CheckedChanged(object sender, EventArgs e)
    {
        if (cdNotify.Checked)
        {
            txtBankname.Text = txtCompanyname.Text;
            txtBAddress1.Text = txtAddress1.Text;
            txtBAddress2.Text = txtAddress2.Text;
            txtBAddress3.Text = txtAddress3.Text;
            txtBCity.Text = txtCity.Text;
            txtBCountry.Text = txtCountry.Text;
            txtBState.Text = txtState.Text;
            txtBZipcode.Text = txtZipCode.Text;
        }
        else
        {
            txtBankname.Text = string.Empty;
            txtBAddress1.Text = string.Empty;
            txtBAddress2.Text = string.Empty;
            txtBAddress3.Text = string.Empty;
            txtBCity.Text = string.Empty;
            txtBCountry.Text = string.Empty;
            txtBState.Text = string.Empty;
            txtBZipcode.Text = string.Empty;
        }
    }
    protected void rbBank_CheckedChanged(object sender, EventArgs e)
    {
        trbank.Visible = true;
        trConsignee.Visible = false;
        lblTex.Text = "Bank";
        bankorconsignee = 1;
    }
    protected void rbConsignee_CheckedChanged(object sender, EventArgs e)
    {
        trConsignee.Visible = true;
        trbank.Visible = false;
        lblTex.Text = "Consignee";
        bankorconsignee = 2;
    }

}

