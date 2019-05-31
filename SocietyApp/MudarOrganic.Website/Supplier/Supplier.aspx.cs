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
    Supplier_BL SBL = new Supplier_BL();
    Farmer_BL fmObj = new Farmer_BL();
    BranchsRolesEmployees_BL BRE = new BranchsRolesEmployees_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MasterControlbtnAddSupplier();
            lblSupplierID.Text = string.Empty;
            BindSupplierDetails();
            btnAddSupplier.Visible = true;
            gvSupplier.Visible = true;
            divSupplierForm.Visible = false;
            BindIcsCodes();
        }
    }
    protected void btnCompanyInfoSubmit_Click(object sender, EventArgs e)
    {

    }

    public void BindIcsCodes()
    {
        DataTable dt = fmObj.GetICSCodes();
        chkICSList.DataTextField = "Branchcode";
        chkICSList.DataValueField = "Branchcode";
        chkICSList.DataSource = dt;
        chkICSList.DataBind();
    }
    protected void btnContactSubmit_Click(object sender, EventArgs e)
    {
        //BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, 2);
    }
    protected void btnNotifySubmit_Click(object sender, EventArgs e)
    {
        //string notifyaddress = txtNAddress1.Text + "@" + txtNAddress2.Text + "@" +txtNAddress3.Text;
        //BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtNotifyName.Text, notifyaddress , txtNCity.Text, txtNState.Text,txtNZipCode.Text,txtNCountry.Text ,txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, 3);
    }
    protected void btnBankSubmit_Click(object sender, EventArgs e)
    {
        //string bankaddress = txtBAddress1.Text + "@" + txtBAddress2.Text + "@" + txtBAddress3.Text;
        //BBL.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress,txtBCity.Text,txtBState.Text,txtBZipcode.Text,txtBCountry.Text, "Bhanu", string.Empty,4);
    }
    protected void btnPortSubmit_Click(object sender, EventArgs e)
    {
        //result = BBL.BuyerTransPortDetails_INSandUPDandDEL(lblBuyerID.Text,txtSea.Text,txtAir.Text,txtRoad.Text,txtRail.Text,"Bhanu", string.Empty, MudarApp.Insert);
    }
    protected void btnPriceTermsSubmit_Click(object sender, EventArgs e)
    {
        //result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text,rbtnFOB.Checked,rbtnCNFDestbySea.Checked, rbtnCIFDestbyAir.Checked, rbtnCIFDestbySea.Checked, rbtnCIFDestbyAir.Checked, rbtnForDestination.Checked, rbtnExworks.Checked, false, false, false, false, false, "Bhanu", string.Empty, MudarApp.Insert);
    }
    protected void btnPaymentTermsSumit_Click(object sender, EventArgs e)
    {
        //result = BBL.BuyerPriceTermsDetails_INSandUPDandDEL(lblBuyerID.Text, false, false, false, false, false, false, false, rbtnHundperAdvance.Checked, rbtnFiftyfityAgnistDocs.Checked, rbtnHundAgnistDocs.Checked, rbtnThirtyDateofinvoice.Checked, rbtnThirtyDateofDelivery.Checked, "Bhanu", string.Empty, MudarApp.Update);
    }
    #region Navigator
    protected void btnCompanyInfoNext_Click(object sender, EventArgs e)
    {

        string NewLoginId = MudarAutoGenerate.GenerateULogin(txtCompanyname.Text);
        string NewPassword = MudarAutoGenerate.GeneratePassword(txtCompanyname.Text);
        string Country = txtCountry.Text.ToUpper();
        string companyaddress = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;
        List<string> selectedValues = chkICSList.Items.Cast<ListItem>()
   .Where(li => li.Selected)
   .Select(li => li.Value)
   .ToList();
        string chkSelectedVal = string.Empty;
        if (selectedValues.Count > 0)
            chkSelectedVal = string.Join(",", selectedValues.ToArray());
        if (string.IsNullOrEmpty(lblSupplierID.Text))
        {
            lblSupplierID.Text = Guid.NewGuid().ToString();
            result = SBL.SupplierDetails_INSandUPDandDEL(lblSupplierID.Text, txtCompanyname.Text, companyaddress, txtCity.Text, txtState.Text, txtZipCode.Text, Country, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, txtTIN.Text, txtVAT.Text, txtCST.Text, MudarApp.Insert, chkSelectedVal);
            if (result)
            {
                result = BRE.UserLogin_INS_UPD_DEL(lblSupplierID.Text, NewLoginId, NewPassword, "Bhanu", "bhanu", MudarApp.Insert);
                if (result)
                {
                    result = BRE.UserInRoles_INS_UPD_DEL(WebConfigurationManager.AppSettings["SupplierRole"].ToString(), lblSupplierID.Text, "Bhanu", "bhanu", MudarApp.Insert);
                }
            }
        }
        else
        {
            result = SBL.SupplierDetails_INSandUPDandDEL(lblSupplierID.Text, txtCompanyname.Text, companyaddress, txtCity.Text, txtState.Text, txtZipCode.Text, Country, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", txtTIN.Text, txtVAT.Text, txtCST.Text, 6, chkSelectedVal);
        }
        MainSupplierView.ActiveViewIndex = 1;

    }
    protected void btnContactBack_Click(object sender, EventArgs e)
    {
        MainSupplierView.ActiveViewIndex = 0;
    }
    protected void btnContactNext_Click(object sender, EventArgs e)
    {
        SBL.SupplierDetails_INSandUPDandDEL(lblSupplierID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtContatperson.Text, txtContactPhone.Text, txtEmail.Text, txtWebsite.Text, txtMobile.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 2, string.Empty);
        MainSupplierView.ActiveViewIndex = 2;
    }
    protected void btnBankBack_Click(object sender, EventArgs e)
    {
        MainSupplierView.ActiveViewIndex = 1;
    }
    protected void btnBankNext_Click(object sender, EventArgs e)
    {
        string bankaddress = txtBAddress1.Text + "@" + txtBAddress2.Text + "@" + txtBAddress3.Text;
        SBL.SupplierDetails_INSandUPDandDEL(lblSupplierID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 4, string.Empty);
        MainSupplierView.ActiveViewIndex = 3;
    }
    protected void btnPriceTermsBack_Click(object sender, EventArgs e)
    {
        MainSupplierView.ActiveViewIndex = 2;
    }
    protected void btnPriceTermsNext_Click(object sender, EventArgs e)
    {
        SBL.SupplierPriceandPaymentDetails_INSandUPDandDEL(lblSupplierID.Text, rbtnExworks.Checked, rbtnExSupplierPlace.Checked, rbtnForDestination.Checked, string.Empty, "Bhanu", string.Empty, 2);
        MainSupplierView.ActiveViewIndex = 4;
    }
    protected void btnPaymentTermsBack_Click(object sender, EventArgs e)
    {

        MainSupplierView.ActiveViewIndex = 3;
    }
    protected void btnPaymentTermsNext_Click(object sender, EventArgs e)
    {
        SBL.SupplierPriceandPaymentDetails_INSandUPDandDEL(lblSupplierID.Text, false, false, false, txtPaymentTerms.Text, "Bhanu", string.Empty, 3);
        MainSupplierView.ActiveViewIndex = 5;
    }
    #endregion
    protected void rbtnFOB_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnAddSupplier_Click(object sender, EventArgs e)
    {
        lblSupplierID.Text = string.Empty;
        divSupplierForm.Visible = true;
        gvSupplier.Visible = false;
        btnAddSupplier.Visible = false;
    }
    private void BindSupplierDetails()
    {
        gvSupplier.DataSource = SBL.GetSupplierDetails();
        gvSupplier.DataBind();
    }
    private void BindSupplierDetails(string SupplierID)
    {
        DataTable dtSuppliers = SBL.GetSupplierDetails(SupplierID);
        if (dtSuppliers.Rows.Count > 0)
        {
            DataRow dr = dtSuppliers.Rows[0];
            lblSupplierID.Text = dr["SupplierId"].ToString();
            txtCompanyname.Text = dr["SupplierCompanyName"].ToString();
            txtTIN.Text = dr["TINNumber"].ToString();
            txtVAT.Text = dr["VAT"].ToString();
            txtCST.Text = dr["CST"].ToString();
            string[] address = dr["CAddress"].ToString().Split('@');
            txtAddress1.Text = address[0];
            txtAddress2.Text = address[1];
            txtAddress3.Text = address[2];

            string supplierTypes = dr["SupplierType"].ToString();
            List<string> lstSupplierTypes = new List<string>();
            if (!string.IsNullOrEmpty(supplierTypes))
            {
                lstSupplierTypes = supplierTypes.Split(',').ToList();
            }

            DataTable dtICSCode = fmObj.GetICSCodes();
            chkICSList.Items.Clear();
            foreach (DataRow item in dtICSCode.Rows)
            {
                ListItem ls = new ListItem();
                ls.Text = item["Branchcode"].ToString();
                ls.Value = item["Branchcode"].ToString();
                if (lstSupplierTypes.Where(m => m == ls.Value).Count() > 0)
                {
                    ls.Selected = true;
                }
                chkICSList.Items.Add(ls);
            }
            
            

            txtCity.Text = dr["CCity"].ToString();
            txtState.Text = dr["CState"].ToString();
            txtZipCode.Text = dr["CPincode"].ToString();
            txtCountry.Text = dr["CCountry"].ToString();

            txtContatperson.Text = dr["CContactPerson"].ToString();
            txtContactPhone.Text = dr["CContactPhoneNo"].ToString();
            txtMobile.Text = dr["MobileforTextingpurpose"].ToString();
            txtEmail.Text = dr["email"].ToString();
            txtWebsite.Text = dr["website"].ToString();

            txtBankname.Text = dr["BankName"].ToString();
            string[] baddress = dr["BankAddress"].ToString().Split('@');
            txtBAddress1.Text = baddress[0];
            txtBAddress2.Text = baddress[1];
            txtBAddress3.Text = baddress[2];

            txtBCity.Text = dr["BankCity"].ToString();
            txtBState.Text = dr["BankState"].ToString();
            txtBZipcode.Text = dr["BankPincode"].ToString();
            txtBCountry.Text = dr["BankCountry"].ToString();

            rbtnExworks.Checked = !string.IsNullOrEmpty(dr["Exworks"].ToString()) ? Convert.ToBoolean(dr["Exworks"].ToString()) : false;
            rbtnExSupplierPlace.Checked = !string.IsNullOrEmpty(dr["Ex-Suppliers Place"].ToString()) ? Convert.ToBoolean(dr["Ex-Suppliers Place"].ToString()) : false;
            rbtnForDestination.Checked = !string.IsNullOrEmpty(dr["ForDestination"].ToString()) ? Convert.ToBoolean(dr["ForDestination"].ToString()) : false;

            txtPaymentTerms.Text = dr["PaymentTerm"].ToString();
        }
    }
    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Supplier":
                {
                    divSupplierForm.Visible = true;
                    gvSupplier.Visible = false;
                    btnAddSupplier.Visible = false;

                    BindSupplierDetails(gvSupplier.DataKeys[index].Value.ToString());
                }
                break;
            case "SupplierView":
                {
                    Response.Redirect("~/Supplier/SupplierView.aspx?sid={" + gvSupplier.DataKeys[index].Value.ToString() + "}");
                }
                break;
        }
    }
}
