using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;

public partial class Supplier_SupplierView : System.Web.UI.Page
{
    Supplier_BL SBL = new Supplier_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["sid"].ToString()))
        {
            string sbi = Request.QueryString["sid"].ToString();
            BindSupplierDetails(sbi);
        }
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
}
