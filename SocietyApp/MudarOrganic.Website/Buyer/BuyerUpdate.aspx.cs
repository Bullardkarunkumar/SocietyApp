using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.EMails;
using MudarOrganic.Components;

public partial class Buyer_BuyerUpdate : System.Web.UI.Page
{
    Buyer_BL buyerObj = new Buyer_BL();
    Product_BL pr = new Product_BL();
    Emailtest email = new Emailtest();
    BranchsRolesEmployees_BL branchObj = new BranchsRolesEmployees_BL();
    int bankorconsignee = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnProfile();
            lblBuyerID.Text = Session["BuyerId"].ToString();
            BindBuyer();
            BindMudar();
            //EnableBindBankDetails();
            if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
                BindControls();
            if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Buyer.ToLower())
                btnFullview.PostBackUrl = "~/Buyer/BuyerDoc.aspx?BackUrl=~/Buyer/BuyerUpdate.aspx";
        }
        //if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        //{
        //    btnPortNext.Text = "Save";
        //}
    }
    private void BindControls()
    {
        btnUpdateCI.Visible = true;
        btnUpdateContact.Visible = true;
        btnUpdNotify.Visible = true;
        btnUpdBank.Visible = true;
        btnUpdPort.Visible = true;
        btnupdProduct.Visible = true;
        btnCompanyInfo.Visible = false;
        btnContactemail.Visible = false;
        btnBank.Visible = false;
        btnPortNext.Visible = false;
        btnNotify.Visible = false;
        btnProduct.Visible = false;
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
    private void BindMudar()
    {
        DataTable dtBranch = branchObj.GetDefaulBranchDetails();
        if (dtBranch.Rows.Count > 0)
        {
            lblMudarEmail.Text = dtBranch.Rows[0]["Email"].ToString();
        }
    }
    private void BindBuyer()
    {
        DataTable dtBuyer = buyerObj.BuyerDetails(lblBuyerID.Text);
        if (dtBuyer.Rows.Count > 0)
        {
            //company details
            txtCompanyname.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
            string[] Address = dtBuyer.Rows[0]["CAddress"].ToString().Split('@');
            txtAddress1.Text = Address[0].ToString();
            txtAddress2.Text = Address[1].ToString();
            txtAddress3.Text = Address[2].ToString();
            txtCity.Text = dtBuyer.Rows[0]["CCity"].ToString();
            txtState.Text = dtBuyer.Rows[0]["CState"].ToString();
            txtZipCode.Text = dtBuyer.Rows[0]["CPincode"].ToString();
            txtCountry.Text = dtBuyer.Rows[0]["CCountry"].ToString();

            //Contact Info
            txtContatperson.Text = dtBuyer.Rows[0]["CContactPerson"].ToString();
            txtContactPhone.Text = dtBuyer.Rows[0]["CContactPhoneNo"].ToString();
            txtMobile.Text = dtBuyer.Rows[0]["MobileforTextingpurpose"].ToString();
            txtEmail.Text = dtBuyer.Rows[0]["email"].ToString();
            txtWebsite.Text = dtBuyer.Rows[0]["website"].ToString();

            //Notify
            txtNotifyName.Text = dtBuyer.Rows[0]["NContactPerson"].ToString();
            string[] nAddress = dtBuyer.Rows[0]["NAddress"].ToString().Split('@');
            txtNAddress1.Text = nAddress[0].ToString();
            txtNAddress2.Text = nAddress[1].ToString();
            txtNAddress3.Text = nAddress[2].ToString();
            txtNCity.Text = dtBuyer.Rows[0]["NCity"].ToString();
            txtNState.Text = dtBuyer.Rows[0]["NState"].ToString();
            txtNZipCode.Text = dtBuyer.Rows[0]["NPincode"].ToString();
            txtNCountry.Text = dtBuyer.Rows[0]["NCountry"].ToString();

            //Bank Info
            txtBankname.Text = dtBuyer.Rows[0]["BankName"].ToString();
            string[] bAddress = dtBuyer.Rows[0]["BankAddress"].ToString().Split('@');
            txtBAddress1.Text = bAddress[0].ToString();
            txtBAddress2.Text = bAddress[1].ToString();
            txtBAddress3.Text = bAddress[2].ToString();
            txtBCity.Text = dtBuyer.Rows[0]["BankCity"].ToString();
            txtBState.Text = dtBuyer.Rows[0]["BankState"].ToString();
            txtBCountry.Text = dtBuyer.Rows[0]["BankCountry"].ToString();
            txtBZipcode.Text = dtBuyer.Rows[0]["BankPincode"].ToString();

            //Port Info
            txtAir.Text = dtBuyer.Rows[0]["AirportName"].ToString();
            txtSea.Text = dtBuyer.Rows[0]["SeaportName"].ToString();
            txtRoad.Text = dtBuyer.Rows[0]["RoadDestination"].ToString();
            txtRail.Text = dtBuyer.Rows[0]["RailStation"].ToString();
            ddlTransportMode.ClearSelection();
            ListItem list = ddlTransportMode.Items.FindByValue(dtBuyer.Rows[0]["ModeofTransport"].ToString());
            if (list != null)
                list.Selected = true;
            if (txtCountry.Text.ToUpper() == "INDIA")
            {
                txtSea.Enabled = false;
                txtAir.Enabled = false;
            }
            BindProductList();
        }
    }
    private void BindProductList()
    {
        DataTable dtProductList = new DataTable();
        gvProductList.DataSource = pr.GetProductDetails();
        gvProductList.DataBind();
        DataTable dtBuyerProdList = buyerObj.BuyerProductList(Session["BuyerId"].ToString());
        if (dtBuyerProdList.Rows.Count > 0)
        {
            foreach (DataRow dr in dtBuyerProdList.Rows)
            {
                for (int count = 0; count < gvProductList.Rows.Count; count++)
                {
                    if (gvProductList.DataKeys[count].Value.ToString().Trim() == dr["ProductID"].ToString())
                    {
                        (gvProductList.Rows[count].Cells[0].FindControl("cbitem") as CheckBox).Checked = true;
                    }
                }
            }
        }

    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        if (txtCountry.Text.ToUpper() == "INDIA")
        {
            divIndBuyer.Visible = true;
            txtTIN.Focus();
            txtRoad.Enabled = true;
            txtRail.Enabled = true;
        }
        else
        {
            txtRoad.Enabled = false;
            txtRail.Enabled = false;
        }
    }
    protected void btnCompanyInfoNext_Click(object sender, EventArgs e)
    {
        //company info
        if (txtCountry.Text.ToUpper() == "INDIA")
        {
            txtSea.Enabled = false;
            txtAir.Enabled = false;
        }
        MainBuyerView.ActiveViewIndex = 1;
    }
    protected void btnContactinfoBank_Click(object sender, EventArgs e)
    {
        //contact info back
        MainBuyerView.ActiveViewIndex = 0;
    }
    protected void btnContactinfo_Click(object sender, EventArgs e)
    {
        //contact info
        MainBuyerView.ActiveViewIndex = 5;


    }
    protected void btnNotifyBack_Click(object sender, EventArgs e)
    {
        //notify back
        MainBuyerView.ActiveViewIndex = 5;
    }
    protected void btnNotifyNext_Click(object sender, EventArgs e)
    {
        //notify info
        MainBuyerView.ActiveViewIndex = 3;
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            string notifyaddress = txtNAddress1.Text + "@" + txtNAddress2.Text + "@" + txtNAddress3.Text;
            //buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtNotifyName.Text, notifyaddress, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 3);
        }
    }
    protected void btnBankBack_Click(object sender, EventArgs e)
    {
        //bank info back
        MainBuyerView.ActiveViewIndex = 2;
    }
    protected void btnBankNext_Click(object sender, EventArgs e)
    {
        //bank info
        MainBuyerView.ActiveViewIndex = 4;
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            string bankaddress = txtBAddress1.Text + "@" + txtBAddress2.Text + "@" + txtBAddress3.Text;
            // buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 4);
        }
    }
    protected void btnPortBack_Click(object sender, EventArgs e)
    {
        //port info back
        MainBuyerView.ActiveViewIndex = 3;
    }
    protected void btnPortNext_Click(object sender, EventArgs e)
    {
        //port info
        bool result;
        result = email.SendBuyerPortInfo(txtCompanyname.Text, ddlTransportMode.SelectedItem.Text, txtAir.Text, txtSea.Text, txtRoad.Text, txtRail.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Port Info");
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            if (ddlTransportMode.SelectedValue == "--select--")
            {
                Response.Write("<script>alert('select the TransportMode');</script>");
            }
            else
            {
                buyerObj.BuyerTransPortDetails_INSandUPDandDEL(lblBuyerID.Text, Convert.ToInt32(ddlTransportMode.SelectedValue), txtSea.Text, txtAir.Text, txtRoad.Text, txtRail.Text, "Bhanu", string.Empty, MudarApp.Update);

            }
        }
    }
    protected void btnProductBack_Click(object sender, EventArgs e)
    {
        //product Back -- 5
        MainBuyerView.ActiveViewIndex = 1;
    }
    protected void btnProductNext_Click(object sender, EventArgs e)
    {
        //Product Next  -- 5
        MainBuyerView.ActiveViewIndex = 2;
        bool result = false;
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            result = buyerObj.BuyerProductInsertDetails(lblBuyerID.Text, 0, "bhanu", string.Empty, MudarApp.Delete);
            foreach (GridViewRow gvr in gvProductList.Rows)
            {
                if ((gvr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
                {
                    result = buyerObj.BuyerProductInsertDetails(lblBuyerID.Text, Convert.ToInt32(gvProductList.DataKeys[gvr.RowIndex].Values["ProductId"]), "bhanu", string.Empty, MudarApp.Insert);
                }
            }
        }
    }
    protected void btnCompanyInfo_Click(object sender, EventArgs e)
    {
        //company info
        bool result;
        result = email.SendBuyerCompanyInfo(txtCompanyname.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text, txtState.Text, txtZipCode.Text, txtCountry.Text, lblMudarEmail.Text, txtEmail.Text, "Update Buyer Company Info");
        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Edit request sent to Admin for Approval... !!! ');</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
            return;
        }
    }
    protected void btnContactemail_Click(object sender, EventArgs e)
    {
        //contact info
        bool result;
        result = email.SendBuyerContactInfo(txtCompanyname.Text, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, "sudheer@mudarindia.com", lblMudarEmail.Text, "Update Buyer Contact Info");
        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Edit request sent to Admin for Approval... !!! ');</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
            return;
        }
    }
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        //notify info
        bool result;
        result = email.SendBuyerNotifyInfo(txtCompanyname.Text, txtNotifyName.Text, txtNAddress1.Text, txtNAddress2.Text, txtNAddress3.Text, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Notify Info");
        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Edit request sent to Admin for Approval... !!! ');</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
            return;
        }
    }
    protected void btnBank_Click(object sender, EventArgs e)
    {
        //bank info
        bool result;
        result = email.SendBuyerBankInfo(txtCompanyname.Text, txtBankname.Text, txtBAddress1.Text, txtBAddress2.Text, txtBAddress3.Text, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Bank Info");
        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Edit request sent to Admin for Approval... !!! ');</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
            return;
        }
    }
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        //Product List
        string strpdf = "<html></body>";
        strpdf += "<table width='65%' border='1'><tr bgcolor='#CCCC99'><td align='center' width='30%'>Product ID</td><td align='center' width='70%'>Product Name</td></tr>";
        foreach (GridViewRow gvr in gvProductList.Rows)
        {
            if ((gvr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
                strpdf += "<tr bgcolor='#e2efd9'><td align='center' width='20%'>" + gvr.Cells[0].Text + "</td><td align='center' width='45%'>" + gvr.Cells[1].Text + "</td></tr>";
        }
        strpdf += "</table></body></html>";
        bool result;
        result = email.SendBuyerProductList(txtCompanyname.Text, strpdf, txtEmail.Text, lblMudarEmail.Text, "Update NEW Product List");
        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Edit request sent to Admin for Approval... !!! ');</script>");
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong !!! Tryagain later...');</script>");
            return;
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            Response.Redirect("~/SuperAdminHome.aspx");
        }
        else
            Response.Redirect("~/BuyerHome.aspx");
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
    protected void btnFullview_Click(object sender, EventArgs e)
    {

    }
    protected void rbBank_CheckedChanged(object sender, EventArgs e)
    {
        trCong.Visible = false;
        divForm.Visible = true;
        lblTex.Text = "Bank";
        bankorconsignee = 1;
    }
    protected void rbConsignee_CheckedChanged(object sender, EventArgs e)
    {
        trCong.Visible = true;
        lblTex.Text = "Consignee";
        divForm.Visible = true;
        bankorconsignee = 2;
    }
    #region Update Buyer Details
    protected void btnUpdateCI_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {

            bool result;
            string address = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;
            result = buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, address, txtCity.Text, txtState.Text, txtZipCode.Text, txtCountry.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 6, 0);
            if (result)
            {
                result = email.SendBuyerCompanyInfo(txtCompanyname.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text, txtState.Text, txtZipCode.Text, txtCountry.Text, lblMudarEmail.Text, txtEmail.Text, "Update Buyer Company Info");
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Data Updated Successfully !!! ');</script>");
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
                return;
            }
        }
    }
    protected void btnUpdateContact_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            bool result;
            result = buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtEmail.Text, txtWebsite.Text, txtNotifyName.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 2, 0);
            if (result)
            {
                result = email.UpdBuyerContactInfo(txtCompanyname.Text, txtContatperson.Text, txtContactPhone.Text, txtMobile.Text, txtWebsite.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Contact Info");
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Data Updated Successfully !!! ');</script>");
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
                return;
            }
        }
    }
    protected void btnUpdNotify_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            bool result;
            string notifyaddress = txtNAddress1.Text + "@" + txtNAddress2.Text + "@" + txtNAddress3.Text;
            result = buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtNotifyName.Text, notifyaddress, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtBankname.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 3, 0);
            if (result)
            {
                result = email.UpdBuyerNotifyInfo(txtCompanyname.Text, txtNotifyName.Text, txtNAddress1.Text, txtNAddress2.Text, txtNAddress3.Text, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Notify Info");
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Data Updated Successfully !!! ');</script>");
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
                return;
            }
        }
    }
    protected void btnUpdBank_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            bool result;
            string bankaddress = txtBAddress1.Text + "@" + txtBAddress2.Text + "@" + txtBAddress3.Text;
            result = buyerObj.BuyerDetails_INSandUPDandDEL(lblBuyerID.Text, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, txtBankname.Text, bankaddress, txtBCity.Text, txtBState.Text, txtBZipcode.Text, txtBCountry.Text, "Bhanu", string.Empty, string.Empty, string.Empty, string.Empty, 4, 1);
            if (result)
            {
                result = email.UpdBuyerBankInfo(txtCompanyname.Text, txtNotifyName.Text, txtNAddress1.Text, txtNAddress2.Text, txtNAddress3.Text, txtNCity.Text, txtNState.Text, txtNZipCode.Text, txtNCountry.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Notify Info");
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Data Updated Successfully !!! ');</script>");
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
                return;
            }
        }
    }
    protected void btnUpdPort_Click(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            if (ddlTransportMode.SelectedValue == "--select--")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! select the TransportMode !!! ');</script>");
            }
            else
            {
                bool result;
                result = buyerObj.BuyerTransPortDetails_INSandUPDandDEL(lblBuyerID.Text, Convert.ToInt32(ddlTransportMode.SelectedValue), txtSea.Text, txtAir.Text, txtRoad.Text, txtRail.Text, "Bhanu", string.Empty, MudarApp.Update);
                if (result)
                {
                    result = email.UpdBuyerPortInfo(txtCompanyname.Text, ddlTransportMode.SelectedItem.Text, txtAir.Text, txtSea.Text, txtRoad.Text, txtRail.Text, txtEmail.Text, lblMudarEmail.Text, "Update Buyer Port Info");
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Data Updated Successfully !!! ');</script>");
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong!!! Tryagain later... !!! ');</script>");
                    return;
                }
            }
        }
    }
    #endregion
    protected void cbconsignee_CheckedChanged(object sender, EventArgs e)
    {
        if (cbconsignee.Checked)
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
    protected void btnupdProduct_Click(object sender, EventArgs e)
    {
        bool result;
        int count = 0;
        DataTable dtBuyerProdList = buyerObj.BuyerProductList(Session["BuyerId"].ToString());
        foreach (GridViewRow gvr in gvProductList.Rows)
        {
            if ((gvr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
            {
                if (dtBuyerProdList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBuyerProdList.Rows)
                    {
                        count = 0;
                        string ss = dr["ProductID"].ToString();
                        if (gvr.Cells[0].Text == dr["ProductID"].ToString())
                        {
                            count = 1;
                            break;
                        }
                    }
                }
                if (count == 0)//count == dtBuyerProdList.Rows.Count
                {
                    result = buyerObj.BuyerProductInsertDetails(Session["BuyerId"].ToString(), Convert.ToInt32(gvr.Cells[0].Text),  "Bhanu",string.Empty, 1);
                    if (result)
                    {
                        string strpdf = "<html></body>";
                        strpdf += "<table width='65%' border='1'><tr bgcolor='#CCCC99'><td align='center' width='30%'>Product ID</td><td align='center' width='70%'>Product Name</td></tr>";
                        foreach (GridViewRow gevr in gvProductList.Rows)
                        {
                            if ((gevr.Cells[2].FindControl("cbitem") as CheckBox).Checked)
                                strpdf += "<tr bgcolor='#e2efd9'><td align='center' width='20%'>" + gevr.Cells[0].Text + "</td><td align='center' width='45%'>" + gevr.Cells[1].Text + "</td></tr>";
                        }
                        strpdf += "</table></body></html>";
                        result = email.UpdBuyerProductList(txtCompanyname.Text, strpdf, txtEmail.Text, lblMudarEmail.Text, "Update NEW Product List");
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!!Data Updated successfully !!! ');</script>");
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Something went wrong !!! Tryagain later...');</script>");
                        return;
                    }
                }
            }
        }
        
    }
}