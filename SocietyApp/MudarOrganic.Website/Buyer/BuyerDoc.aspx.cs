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

public partial class BuyerDoc : System.Web.UI.Page
{
    Buyer_BL buyerObj = new Buyer_BL();
    BranchsRolesEmployees_BL BRE = new BranchsRolesEmployees_BL();
    Product_BL pr = new Product_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divIndBuyer.Visible = false;
            BindBuyerDetails();
        }
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            if (!string.IsNullOrEmpty(Request.QueryString["BackUrl"].ToString()))
                btnSubmit.PostBackUrl = Request.QueryString["BackUrl"].ToString();
        }
    }
    private void BindBuyerDetails()
    {
        try
        {
            lblBuyerID.Text = Session["BuyerId"].ToString();
            DataTable dtBuyer = buyerObj.BuyerDetails(lblBuyerID.Text);
            if (dtBuyer.Rows.Count > 0)
            {
                //company details
                lblCompanyname.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
                string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
                lblAddress1.Text = Address[0].ToString();
                lblAddress2.Text = Address[1].ToString();
                lblAddress3.Text = Address[2].ToString();
                lblCity.Text = dtBuyer.Rows[0]["CCity"].ToString();
                lblState.Text = dtBuyer.Rows[0]["CState"].ToString();
                lblZipCode.Text = dtBuyer.Rows[0]["CPincode"].ToString();
                lblCountry.Text = dtBuyer.Rows[0]["CCountry"].ToString();
                if (lblCountry.Text.ToUpper() == "INDIA")
                {
                    divIndBuyer.Visible = true;
                }

                //Contact Info
                lblContatperson.Text = dtBuyer.Rows[0]["CContactPerson"].ToString();
                lblContactPhone.Text = dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
                lblMobile.Text = dtBuyer.Rows[0]["MobileforTextingpurpose"].ToString();
                lblEmail.Text = dtBuyer.Rows[0]["email"].ToString();
                lblWebsite.Text = dtBuyer.Rows[0]["website"].ToString();

                //Notify
                lblNotifyName.Text = dtBuyer.Rows[0]["NContactPerson"].ToString();
                string[] nAddress = dtBuyer.Rows[0]["NAddress"].ToString().Split('@');
                lblNAddress1.Text = nAddress[0].ToString();
                if (nAddress.Length > 1)
                {
                    lblNAddress2.Text = nAddress[1].ToString();
                    lblNAddress3.Text = nAddress[2].ToString();
                }
                lblNCity.Text = dtBuyer.Rows[0]["NCity"].ToString();
                lblNState.Text = dtBuyer.Rows[0]["NState"].ToString();
                lblNZipCode.Text = dtBuyer.Rows[0]["NPincode"].ToString();
                lblNCountry.Text = dtBuyer.Rows[0]["NCountry"].ToString();

                //Bank Info
                lblBankname.Text = dtBuyer.Rows[0]["BankName"].ToString();
                string[] bAddress = dtBuyer.Rows[0]["BankAddress"].ToString().Split('@');
                lblBAddress1.Text = bAddress[0].ToString();
                if (bAddress.Length > 1)
                {
                    lblBAddress2.Text = bAddress[1].ToString();
                    lblBAddress3.Text = bAddress[2].ToString();
                }
                lblBCity.Text = dtBuyer.Rows[0]["BankCity"].ToString();
                lblBState.Text = dtBuyer.Rows[0]["BankState"].ToString();
                lblBCountry.Text = dtBuyer.Rows[0]["BankCountry"].ToString();
                lblBZipcode.Text = dtBuyer.Rows[0]["BankPincode"].ToString();

                //Port Info
                lblAir.Text = dtBuyer.Rows[0]["AirportName"].ToString();
                lblSea.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
                lblRoad.Text = dtBuyer.Rows[0]["RoadDestination"].ToString();
                lblRail.Text = dtBuyer.Rows[0]["RailStation"].ToString();
                //mode of transport
                if (dtBuyer.Rows[0]["ModeofTransport"].ToString() == "0")
                {
                    lblTransportmode.Text = "Air";
                }
                if (dtBuyer.Rows[0]["ModeofTransport"].ToString() == "1")
                {
                    lblTransportmode.Text = "Sea";

                }
                if (dtBuyer.Rows[0]["ModeofTransport"].ToString() == "2")
                {
                    lblTransportmode.Text = "Rail";

                }
                if (dtBuyer.Rows[0]["ModeofTransport"].ToString() == "3")
                {
                    lblTransportmode.Text = "Road";

                }
                //payment terms
                if (Convert.ToBoolean(dtBuyer.Rows[0]["100%advance"].ToString()) == true)
                {
                    lblPaymentTerms.Text = "100%advance";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["100%againstDocs"].ToString()) == true)
                {
                    lblPaymentTerms.Text = "100%againstDocs";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["50%adv+50%againstDocs"].ToString()) == true)
                {
                    lblPaymentTerms.Text = "50%adv+50%againstDocs";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["NoofDaysfromInvoice"].ToString()) == true)
                {
                    lblPaymentTerms.Text = "NoofDaysfromInvoice";
                    lblCreditDays.Text = dtBuyer.Rows[0]["No_of_Days_Count_fromInvoice"].ToString();
                }
                //price terms
                if (Convert.ToBoolean(dtBuyer.Rows[0]["FOB_India"].ToString()) == true)
                {
                    lblPriceTerms.Text = "FOB_India";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Sea_By"].ToString()) == true)
                {
                    lblPriceTerms.Text = "CIF By Sea";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true || Convert.ToBoolean(dtBuyer.Rows[0]["CIF_AIR_By_WEST_USA"].ToString()) == true)
                {
                    lblPriceTerms.Text = "CIF By AIR";
                }
                if (Convert.ToBoolean(dtBuyer.Rows[0]["FORDestination"].ToString()) == true)
                {
                    lblPriceTerms.Text = "FOR Destination";
                }
            }
            DataTable dtBuyerLogin = buyerObj.GetBuyerLoginDetails(lblBuyerID.Text);
            if (dtBuyerLogin.Rows.Count > 0)
            {
                lblusername.Text = dtBuyerLogin.Rows[0]["UserLoginID"].ToString();
                lblpassword.Text = dtBuyerLogin.Rows[0]["UserPassword"].ToString();
            }
            DataTable dtBuyerProducts = buyerObj.GetBuyerProductDetails(lblBuyerID.Text);
            if (dtBuyerProducts.Rows.Count > 0)
            {
                gvProductList.DataSource = dtBuyerProducts;
                gvProductList.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            Response.Redirect("~/Mudar/UpdateOrder.aspx");
        }
    }
}
