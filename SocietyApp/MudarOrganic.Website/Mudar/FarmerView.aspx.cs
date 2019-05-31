using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MudarOrganic.BL;

public partial class Farmer_FarmerView : System.Web.UI.Page
{
    Farmer_BL farmerobj = new Farmer_BL();
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL prod = new Product_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FarmerCode"].ToString()))
                lblFarmerCode.Text = Request.QueryString["FarmerCode"].ToString().Trim();
            else
                lblFarmerCode.Text = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["BackUrl"].ToString()))
                btnBack.PostBackUrl = Request.QueryString["BackUrl"].ToString().Trim();             
                
            ListItemCollection items = MudarApp.BindYear();
            foreach (ListItem item in items)
            {
                ddlSeasonYear.Items.Add(item);
            }
            ddlSeasonYear.Items.RemoveAt(0);
            BindFarmer();
        }
    }

    private void BindFarmer()
    {
        DataTable farmerdata = farmerobj.FamerDetails(lblFarmerCode.Text); //farmerobj.FamerDetails(farmerName, farmerCode, area);
        if (farmerdata.Rows.Count > 0)
        {
            DataRow farmer = farmerdata.Rows[0];
            hdfarmerUid.Value = farmer["FarmerId"].ToString();
            lblFarmerName.Text = farmer["FirstName"].ToString();
            lblTotalArea.Text = farmer["TotalAreaInHectares"].ToString();
            lblPlots.Text = farmer["NumberOfPlots"].ToString();
            lblFatherName.Text = farmer["FatherName"].ToString();
            lblAddress.Text = farmer["Address"].ToString()
                            + "<br/>" + farmer["City_Village"].ToString()
                            + "<br/>" + farmer["District"].ToString()
                            + "<br/>" + farmer["Taluk"].ToString()
                            + "<br/>" + farmer["State"].ToString()
                            + "<br/>" + farmer["Country"].ToString();
            lblPhone.Text = farmer["PhoneNumber"].ToString();
            lblMobile.Text = farmer["MobileNumber"].ToString();
            lblFarmerCode.Text = farmer["FarmerCode"].ToString();
            lblFarmerRegistration.Text = farmer["FarmerRegNumber"].ToString();
            lblBankName.Text = farmer["BankInfo"].ToString();
            lblHolderName.Text = farmer["BankHolderName"].ToString();
            lblAcctNo.Text = farmer["BankAccNo"].ToString();
            lblChemicalDate.Text = string.Format("{0:dd - MMM - yyyy}", Convert.ToDateTime(farmer["ChemicalAppDate"].ToString()));
            rbOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false);
            rbNonOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["OrganicFair"].ToString()) ? farmer["OrganicFair"] : false);

            //Farm Info
            DataRow[] drs = farmerobj.FarmDetails(hdfarmerUid.Value).Select("ParentFarmID  =0");
            DataTable dtPlot = farmerobj.FarmDetails(hdfarmerUid.Value).Clone();
            if (drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    dtPlot.ImportRow(dr);
                }
                gvPlot.DataSource = dtPlot;
                gvPlot.DataBind();
            }

            //Season Info 
            BindSeason_Farmer();

            //Field Risk
            BindFiledRisk();

            //Family Info
            lblEarningMember.Text = farmer["NumberOfEarningPersons"].ToString();
            lblDElder.Text = farmer["ElderlyDependents"].ToString();

            //child Info
            gvFamilyDet.DataSource = farmerobj.GetFarmerFamilyDeatils(hdfarmerUid.Value);
            gvFamilyDet.DataBind();
            lblDChildren.Text = gvFamilyDet.Rows.Count.ToString();

            //Animal Info
            lblBuffalos.Text = farmer["OtherAnimals"].ToString();
            lblCows.Text = farmer["Cow"].ToString();
            lblOx.Text = farmer["Ox"].ToString();
            lblSheep.Text = farmer["Sheep"].ToString();

            lblComments.Text = farmer["InspectionComments"].ToString();
            lblUploadedBy.Text = farmer["InspectorName"].ToString();
        }
    }
    private void BindSeason_Farmer()
    {
        string seasonYr = ddlSeasonYear.SelectedValue; //DateTime.Now.Year.ToString();
        dlFarmerSeasonDetails.DataSource = cp.GetSeasonDetails(seasonYr);
        dlFarmerSeasonDetails.DataBind();
        int SeasonYear = Convert.ToInt32(seasonYr);
        for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        {
            int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
            CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

            cbl.DataSource = prod.GetProductDetailsbySeason(seasonId);
            cbl.DataTextField = "ProductName";
            cbl.DataValueField = "ProductID";
            cbl.DataBind();
        }
        DataTable dt = prod.GetProductDetails(hdfarmerUid.Value, seasonYr);

        for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        {
            int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
            string sName = (dlFarmerSeasonDetails.Items[count].FindControl("lblseasonName") as Label).Text;
            CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

            if (cbl.Items.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    for (int icount = 0; icount < cbl.Items.Count; icount++)
                        if ((sName.Trim().ToLower() == dr["SeasonName"].ToString().Trim().ToLower()) && (cbl.Items[icount].Value == dr["ProductID"].ToString()))
                        {
                            cbl.Items[icount].Selected = (bool)dr["Result"];
                            //if (dr["SeasonName"].ToString().Trim().ToLower() == "1")
                            //    cbl.Items[icount].Selected = true;
                            //else
                            //    cbl.Items[icount].Selected = false;
                        }
                }
            }
        }
    }
    private void BindFiledRisk()
    {
        if (farmerobj.FieldRisk_Exist(hdfarmerUid.Value))
        {
            DataTable dt = farmerobj.FieldRisk(hdfarmerUid.Value);
            dlFieldRisk.DataSource = dt.DefaultView.ToTable(true, "FarmID", "AreaCode");
            dlFieldRisk.DataBind();

            for (int count = 0; count < dlFieldRisk.Items.Count; count++)
            {
                RadioButtonList rbl = (dlFieldRisk.Items[count].FindControl("rblFieldRisk") as RadioButtonList);
                DataRow[] drField = dt.Select(" FarmID = " + (dlFieldRisk.Items[count].FindControl("hfFieldRisk") as HiddenField).Value);
                rbl.Items.Clear();
                foreach (DataRow dr in drField)
                {
                    ListItem item = new ListItem();
                    item.Text = dr["FieldRiskType"].ToString();
                    item.Value = dr["FieldRiskID"].ToString();
                    rbl.Items.Add(item);
                }
            }
            if (dlFieldRisk.Items.Count == 0)
            {

            }
        }
        else
        {
            DataTable dt = farmerobj.FieldRiskResult(hdfarmerUid.Value);
            dlFieldRisk.DataSource = dt.DefaultView.ToTable(true, "FarmID", "AreaCode");
            dlFieldRisk.DataBind();
            for (int count = 0; count < dlFieldRisk.Items.Count; count++)
            {
                RadioButtonList rbl = (dlFieldRisk.Items[count].FindControl("rblFieldRisk") as RadioButtonList);
                DataRow[] drField = dt.Select(" FarmID = " + (dlFieldRisk.Items[count].FindControl("hfFieldRisk") as HiddenField).Value);
                rbl.Items.Clear();
                foreach (DataRow dr in drField)
                {
                    ListItem item = new ListItem();
                    item.Text = dr["FieldRiskType"].ToString();
                    item.Value = dr["FieldRiskID"].ToString();
                    item.Selected = (bool)(!string.IsNullOrEmpty(dr["Result"].ToString()) ? dr["Result"] : false);
                    rbl.Items.Add(item);
                }
            }
        }
    }

    protected void ddlSeasonYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSeason_Farmer();
    }
}
