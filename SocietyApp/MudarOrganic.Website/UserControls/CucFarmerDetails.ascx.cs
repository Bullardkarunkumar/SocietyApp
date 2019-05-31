using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Text;

public partial class UserControls_UcFarmerDetails : System.Web.UI.UserControl
{
    Farmer_BL farmerobj = new Farmer_BL();
    bool result = false;
    MudarUser mu = new MudarUser();
    public string userPath = string.Empty;
    //public static string FarmerCode = string.Empty;
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL prod = new Product_BL();
    DataTable Seasond = new DataTable();
    DataTable SeasonYearByFarmer = new DataTable();
    DataTable farmTable;
    private string _code;
    public string code
    {
        get { return _code; }
        set { _code = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ClearControls();
            farmTable = new DataTable();
            farmTable.Columns.Add("FarmerID");
            farmTable.Columns.Add("FarmID");
            farmTable.Columns.Add("Exp_Col");
            farmTable.Columns.Add("Plot");
            farmTable.Columns.Add("PlotArea");
            farmTable.Columns.Add("Latitude");
            farmTable.Columns.Add("Longitude");
            farmTable.Columns.Add("AreaCode");
            farmTable.Columns.Add("IMECode");
            farmTable.Columns.Add("TracenetCode");
            farmTable.Columns.Add("C_P");
            farmTable.Columns.Add("ParentID");
            farmTable.Columns.Add("ChildCount");

            Session["session_Farm"] = farmTable;
            Seasond = cp.GetSeasonDetails();
            SeasonYearByFarmer = farmerobj.GetSeasonYearByFarmer();
            lblfarmerUid.Text = Guid.NewGuid().ToString();
            HiddenField1.Value = lblfarmerUid.Text;
            //bindSeasonYearandName();
            //BindSeasonYearByFarmer();
            BindFarmtoSession();
            BindFarm();
            FillYearDropDown();
            lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
            //gvSeasonDetailsBasedFarmerID.Visible = true;
            ddlSeasonYearByFarmer_SelectedIndexChanged(sender, e);
            imgFarmerP.ImageUrl = WebConfigurationManager.AppSettings["FarmerIcon"];
            TabbuttonControls();
            if (!string.IsNullOrEmpty(_code))
            {
                TabbuttonControlsEdit();
                bindFarmerDetails(_code);
            }
            //BindSeason_Farmer();
            BindFiledRisk();
        }
        BindSeason_Farmer();
        //ClearControls();

        if (cbBank.Checked == false)
        {
            txtBankName.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            txtAcccountHolder.Text = string.Empty;
        }
        //lblFarmerCode.Text = FarmerCode;
        txtBankName.Enabled = false;
        txtAccountNo.Enabled = false;
        txtAcccountHolder.Enabled = false;

        if (string.IsNullOrEmpty(txtAcccountHolder.Text))
        {
            txtAcccountHolder.Text = txtFarmerName.Text;
        }
    }

    protected void cbBank_CheckedChanged(object sender, EventArgs e)
    {
        if (cbBank.Checked)
        {
            txtBankName.Enabled = true;
            txtAccountNo.Enabled = true;
        }
        else
        {
            txtBankName.Enabled = false;
            txtAccountNo.Enabled = false;
            txtBankName.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
        }
    }

    private void BindSeasonProducts()
    {

    }

    protected void btnFarmerDetailsSubmit_Click(object sender, EventArgs e)
    {
        //MudarApp farmercode = new MudarApp();
        //string FarmerCode = farmercode.farmercode(txtCity.Text, txtState.Text);
        //string testpath = hfUserPhotoPath.Value;
        //if (farmerobj.FarmerExist(lblfarmerUid.Text))
        //    //update farmer
        //    result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, FarmerCode, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Update, DateTime.Now);
        //else
        //    //Insert farmer
        //    result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, FarmerCode, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Insert, DateTime.Now);
        //lblFarmerCode.Text = FarmerCode;
    }

    protected void afuFarmerPhoto_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        if (afuFarmerPhoto.HasFile)
        {
            string path = mu.createfolder(lblfarmerUid.Text, MudarUser.MudarFamer) ? "~/Attachments/Farmer/" + lblfarmerUid.Text + "/" : "~/Attachments/Farmer/";
            string path1 = path;
            //if (!string.IsNullOrEmpty(path))
            //{
            System.Threading.Thread.Sleep(5000);
            if (afuFarmerPhoto.HasFile)
            {
                path = Server.MapPath(path) + Path.GetFileName(e.FileName);
                userPath = path1 + Path.GetFileName(e.FileName);
                afuFarmerPhoto.SaveAs(path);
                //HiddenField1.Value = path1 + Path.GetFileName(e.FileName);
                upFarmerPhoto.Update();
            }
            //}
        }
    }

    #region Public Methods
    public void bindFarmerDetails(string farmerCode)
    {
        ClearControls();
        DataTable farmerdata = farmerobj.FamerDetails(farmerCode); //farmerobj.FamerDetails(farmerName, farmerCode, area);
        if (farmerdata.Rows.Count > 0)
        {
            DataRow farmer = farmerdata.Rows[0];
            lblfarmerUid.Text = farmer["FarmerId"].ToString();
            txtFarmerName.Text = farmer["FirstName"].ToString();
            txtFatherName.Text = farmer["FatherName"].ToString();
            txtAddress.Text = farmer["Address"].ToString();
            txtCity.Text = farmer["City_Village"].ToString();
            txtDistrict.Text = farmer["District"].ToString();
            txtTaluk.Text = farmer["Taluk"].ToString();
            txtState.Text = farmer["State"].ToString();
            txtCountry.Text = farmer["Country"].ToString();
            txtPhone.Text = farmer["PhoneNumber"].ToString();
            txtMPhone.Text = farmer["MobileNumber"].ToString();
            lblFarmerCode.Text = farmer["FarmerCode"].ToString();
            txtFarmerRegNo.Text = farmer["FarmerRegNumber"].ToString();
            cbBank.Checked = string.IsNullOrEmpty(farmer["BankAccNo"].ToString()) ? false : true;
            txtAccountNo.Text = farmer["BankAccNo"].ToString();
            txtBankName.Text = farmer["BankInfo"].ToString();
            txtLastDate.Text = farmer["ChemicalAppDate"].ToString();
            //ddlSeason.ClearSelection();
            //ddlSeasonYear.ClearSelection();
            //lblSeasonStart.Text = farmer[""].ToString();
            //lblSeasonEnd.Text = farmer[""].ToString();
            txtEarningMember.Text = farmer["NumberOfEarningPersons"].ToString();
            txtDependentElder.Text = farmer["ElderlyDependents"].ToString();
            //ddlDepChild.ClearSelection();
            //ddlDepChild.Text = farmer["ChildrenDependents"].ToString();
            txtBuffalo.Text = farmer["OtherAnimals"].ToString();
            txtCows.Text = farmer["Cow"].ToString();
            txtOx.Text = farmer["Ox"].ToString();
            txtSheep.Text = farmer["Sheep"].ToString();
            //cbInternalInspector.Checked = (bool)farmer["PRESIDNT"];
            //cbPresident.Checked = (bool)farmer["InternalInspectorApproval"];
            //cbProposedFieldOfficer.Checked = (bool)farmer["ProposedFieldOfficer"];
            //cbProposedManager.Checked = (bool)farmer["ProposedManager"];
            imgFarmerP.ImageUrl = farmer["PhotoPath"].ToString();
            lblTotalArea.Text = farmer["TotalAreaInHectares"].ToString();
            lblTotalPlots.Text = farmer["NumberOfPlots"].ToString();
            txtAcccountHolder.Text = farmer["BankHolderName"].ToString();
            rbOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false);
            rbOrganicFairTrad.Checked = (bool)(!string.IsNullOrEmpty(farmer["OrganicFair"].ToString()) ? farmer["OrganicFair"] : false);
            txtCommentInternalInspector.Text = farmer["InspectionComments"].ToString();
            txtuploadedInternalInspector.Text = farmer["InspectorName"].ToString();
            BindChildDetails();
            BindFarmtoSession();
            BindFarm();
            DisableParentRow();
            GridBindSeasonDetailsBasedFarmerID();
            BindSeason_Farmer();
            BindFiledRisk();
        }
    }
    #endregion

    #region Private Methods
    private void TabbuttonControls()
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTaddPlots.Enabled = false;
        btnTaddPlots.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTSeasonInfo.Enabled = false;
        btnTSeasonInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTStandardRisk.Enabled = false;
        btnTStandardRisk.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTFamily.Enabled = false;
        btnTFamily.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTCattle.Enabled = false;
        btnTCattle.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTOffice.Enabled = false;
        btnTOffice.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
    }
    //edit farmerdetails 
    private void TabbuttonControlsEdit()
    {
        //btnTFarmerInfo.Enabled = true;
        //btnTBankInfo.Enabled = true;
        //btnTaddPlots.Enabled = true;
        //btnTSeasonInfo.Enabled = true;
        //btnTStandardRisk.Enabled = true;
        //btnTFamily.Enabled = true;
        //btnTCattle.Enabled = true;
        //btnTOffice.Enabled = true;
        btnNFarmBack.Visible = true;
    }
    private void ClearControls()
    {
        lblfarmerUid.Text = string.Empty;
        txtFarmerName.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtDistrict.Text = string.Empty;
        txtTaluk.Text = string.Empty;
        txtState.Text = string.Empty;
        txtCountry.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtMPhone.Text = string.Empty;
        lblFarmerCode.Text = string.Empty;
        txtFarmerRegNo.Text = string.Empty;
        cbBank.Checked = false;
        txtAccountNo.Text = string.Empty;
        txtBankName.Text = string.Empty;
        txtAcccountHolder.Text = string.Empty;
        txtLastDate.Text = string.Empty;
        //ddlSeason.ClearSelection();
        //ddlSeasonYear.ClearSelection();
        //lblSeasonStart.Text = string.Empty;
        //lblSeasonEnd.Text = string.Empty;
        txtEarningMember.Text = string.Empty;
        txtDependentElder.Text = string.Empty;
        //ddlDepChild.ClearSelection();
        txtBuffalo.Text = string.Empty;
        txtCows.Text = string.Empty;
        txtOx.Text = string.Empty;
        txtSheep.Text = string.Empty;
        //cbInternalInspector.Checked = false;
        //cbPresident.Checked = false;
        //cbProposedFieldOfficer.Checked = false;
        //cbProposedManager.Checked = false;
        imgFarmerP.ImageUrl = WebConfigurationManager.AppSettings["FarmerIcon"];
    }
    private void bindSeasonYearandName()
    {
        if (Seasond.Rows.Count > 0)
        {
            ddlSeasonYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlSeasonYear.DataTextField = "SeasonYear";
            ddlSeasonYear.DataValueField = "SeasonYear";
            ddlSeasonYear.DataBind();
            //ddlSeasonYear.Items.Insert(0, MudarApp.AddListItem());

            //ddlSeason.DataSource = Seasond.DefaultView.ToTable(true, "SeasonName");
            //ddlSeason.DataTextField = "SeasonName";
            //ddlSeason.DataValueField = "SeasonName";
            //ddlSeason.DataBind();
            //ddlSeason.Items.Insert(0, MudarApp.AddListItem());
        }
    }
    private void BindSeasonYearByFarmer()
    {
        //ddlSeasonYearByFarmer.DataSource = SeasonYearByFarmer.DefaultView.ToTable(true, "SeasonYear");
        //ddlSeasonYearByFarmer.DataTextField = "SeasonYear";
        //ddlSeasonYearByFarmer.DataValueField = "SeasonYear";
        //ddlSeasonYearByFarmer.DataBind();
    }
    private void BindChildDetails()
    {
        gvFamilyDet.DataSource = farmerobj.GetFarmerFamilyDeatils(lblfarmerUid.Text);
        gvFamilyDet.DataBind();
        lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
    }
    private void ClearChildDetails()
    {
        txtNameChild.Text = string.Empty;
        rbtnGender.ClearSelection();
        txtDOB.Text = string.Empty;
        lblAge.Text = string.Empty;
        cbSchool.Checked = false;
        cbWorking.Checked = false;
    }
    private void BindFarm()
    {
        dlFarmdetails.DataSource = (DataTable)Session["session_Farm"]; ; //farmerobj.FarmerFarmdetails(lblfarmerUid.Text);
        dlFarmdetails.DataBind();
    }
    private void BindFarmtoSession()
    {
        ((DataTable)Session["session_Farm"]).Clear();
        DataTable temp = farmerobj.FarmDetails(lblfarmerUid.Text);
        foreach (DataRow dr in temp.Rows)
        {
            DataRow newdr = ((DataTable)Session["session_Farm"]).NewRow();
            newdr["FarmerID"] = dr["FarmerID"];
            newdr["FarmID"] = dr["FarmID"];
            newdr["Exp_Col"] = Convert.ToInt16(dr["ChildCount"].ToString()) > 0 ? "~/images/arrow.JPG" : "~/images/arrow.JPG";
            newdr["Plot"] = dr["C_P"].ToString() == "P" ? "Plot" : "Child";
            newdr["PlotArea"] = dr["PlotArea"];
            newdr["Latitude"] = dr["Latitude"];
            newdr["Longitude"] = dr["Longitude"];
            newdr["AreaCode"] = dr["AreaCode"];
            newdr["IMECode"] = "0";
            newdr["TracenetCode"] = "0";
            newdr["C_P"] = dr["C_P"];
            newdr["ParentID"] = dr["ParentFarmID"];
            newdr["ChildCount"] = dr["ChildCount"];

            ((DataTable)Session["session_Farm"]).Rows.Add(newdr);
        }
    }
    private void BindDatalistViewState()
    {
        if (dlFarmdetails.Items.Count > 0 && ((DataTable)Session["session_Farm"]).Rows.Count > 0)
            for (int count = 0; count < dlFarmdetails.Items.Count; count++)
            {
                ((DataTable)Session["session_Farm"]).Rows[count]["PlotArea"] = ((TextBox)dlFarmdetails.Items[count].FindControl("txtPlotArea")).Text;
                ((DataTable)Session["session_Farm"]).Rows[count]["Latitude"] = ((TextBox)dlFarmdetails.Items[count].FindControl("txtLatitude")).Text;
                ((DataTable)Session["session_Farm"]).Rows[count]["Longitude"] = ((TextBox)dlFarmdetails.Items[count].FindControl("txtLongitude")).Text;
                ((DataTable)Session["session_Farm"]).Rows[count]["AreaCode"] = ((Label)dlFarmdetails.Items[count].FindControl("lblAreaCode")).Text;
                ((DataTable)Session["session_Farm"]).Rows[count]["IMECode"] = ((TextBox)dlFarmdetails.Items[count].FindControl("txtIMECode")).Text;
                ((DataTable)Session["session_Farm"]).Rows[count]["TracenetCode"] = ((TextBox)dlFarmdetails.Items[count].FindControl("txtTrancenet")).Text;
            }
    }
    private void DisableParentRow()
    {
        //Parent Row edit will be disable.
        if (dlFarmdetails.Items.Count > 0)
            for (int count = 0; count < dlFarmdetails.Items.Count; count++)
            {
                if (Convert.ToInt32(((Label)dlFarmdetails.Items[count].FindControl("lblChildCount")).Text) > 0)
                {
                    ((TextBox)dlFarmdetails.Items[count].FindControl("txtPlotArea")).Enabled = false;
                    ((TextBox)dlFarmdetails.Items[count].FindControl("txtLatitude")).Enabled = false;
                    ((TextBox)dlFarmdetails.Items[count].FindControl("txtLongitude")).Enabled = false;
                    ((TextBox)dlFarmdetails.Items[count].FindControl("txtIMECode")).Enabled = false;
                    ((TextBox)dlFarmdetails.Items[count].FindControl("txtTrancenet")).Enabled = false;
                }
                if (((Label)dlFarmdetails.Items[count].FindControl("lblC_P")).Text == "C")
                {
                    ((ImageButton)dlFarmdetails.Items[count].FindControl("imgbtnExpand_Collapse")).Visible = false;
                    ((Button)dlFarmdetails.Items[count].FindControl("btnAddChild")).Visible = false;
                }
            }
    }
    private void GridBindSeasonDetailsBasedFarmerID()
    {
        //string FarmerID = lblfarmerUid.Text;
        //int SeasonYear = Convert.ToInt32(ddlSeasonYearByFarmer.SelectedValue.ToString());
        //gvSeasonDetailsBasedFarmerID.DataSource = farmerobj.GetSeasonDetailsBasedFarmerID(FarmerID,SeasonYear);
        //gvSeasonDetailsBasedFarmerID.DataBind();
    }
    public void FillYearDropDown()
    {
        //DataTable temp = cp.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlSeasonYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlSeasonYear.DataTextField = "SeasonYear";
            ddlSeasonYear.DataValueField = "SeasonYear";
            ddlSeasonYear.DataBind();
            ListItem selectitem = new ListItem();
            selectitem.Text = "Select...";
            selectitem.Value = string.Empty;
            ddlSeasonYear.Items.Insert(0, selectitem);
            ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        }
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlSeasonYear.Items.Add(item);
        //ddlSeasonYear.DataBind();
        //ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
    }



    private void BindSeason_Farmer()
    {
        string seasonYr = ddlSeasonYear.SelectedValue;
        DataTable dtSeasonDetails = cp.GetSeasonDetails(seasonYr);


        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Product Id", typeof(int)));
        dt.Columns.Add(new DataColumn("Product Name", typeof(string)));
        foreach (DataRow item in dtSeasonDetails.Rows)
        {
            DataColumn dc = new DataColumn(Convert.ToString(item["SeasonName"]), typeof(string));
            dc.DefaultValue = Convert.ToString(item["SeasonName"]);
            dt.Columns.Add(dc);

            dc = new DataColumn(Convert.ToString(item["SeasonId"]), typeof(string));
            dc.DefaultValue = Convert.ToString(item["SeasonId"]);
            dt.Columns.Add(dc);

            dc = new DataColumn(Convert.ToString(item["StartDate"]), typeof(DateTime));
            dc.DefaultValue = Convert.ToDateTime(item["StartDate"]);
            dt.Columns.Add(dc);

            dc = new DataColumn(Convert.ToString(item["EndDate"]), typeof(DateTime));
            dc.DefaultValue = Convert.ToDateTime(item["EndDate"]);
            dt.Columns.Add(dc);
        }
        DataTable dtProds = prod.GetProductDetailsNew();
        foreach (DataRow item in dtProds.Rows)
        {
            DataRow newRow = dt.NewRow();
            newRow[0] = Convert.ToInt32(item["ProductId"]);
            newRow[1] = Convert.ToString(item["ProductName"]);
            for (int i = 2; i < dt.Columns.Count; i++)
            {
                newRow[i] = dt.Columns[i].DefaultValue;
            }
            dt.Rows.Add(newRow);
        }
        //dt = dt;
        gvFarmerSeasonDetails.Columns.Clear();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (i == 1)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = "Product Name";
                boundField.HeaderText = "";
                gvFarmerSeasonDetails.Columns.Add(boundField);
            }
            else if (i == 0)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = "Product Id";
                boundField.HeaderText = "";
                //boundField.Visible = false;
                gvFarmerSeasonDetails.Columns.Add(boundField);
            }
            else if (i % 4 == 2 && i >= 2)
            {
                TemplateField templateField = new TemplateField();
                templateField.HeaderText = dt.Columns[i].ColumnName;
                gvFarmerSeasonDetails.Columns.Add(templateField);
            }
            else if ((i % 4 == 3 || i % 4 == 0 || i % 4 == 1) && i >= 2)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = dt.Columns[i].ColumnName;
                boundField.HeaderText = dt.Columns[i].ColumnName;
                gvFarmerSeasonDetails.Columns.Add(boundField);
            }
        }

        gvFarmerSeasonDetails.DataSource = dt;
        gvFarmerSeasonDetails.DataBind();


        ////FillYearDropDown();
        //string seasonYr = ddlSeasonYear.SelectedValue;
        //DataTable dtSeasonDetails = cp.GetSeasonDetails(seasonYr);
        //dlFarmerSeasonDetails.DataSource = dtSeasonDetails;
        //dlFarmerSeasonDetails.DataBind();




        ////int SeasonYear = Convert.ToInt32(seasonYr);
        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

        //    cbl.DataSource = prod.GetProductDetailsbySeasonNew(seasonId);
        //    cbl.DataTextField = "ProductName";
        //    cbl.DataValueField = "ProductID";
        //    cbl.DataBind();
        //}
        //DataTable dt = prod.GetProductDetails(lblfarmerUid.Text, seasonYr);

        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    string sName = (dlFarmerSeasonDetails.Items[count].FindControl("lblseasonName") as Label).Text;
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

        //    if (cbl.Items.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            for (int icount = 0; icount < cbl.Items.Count; icount++)
        //                if ((sName.Trim().ToLower() == dr["SeasonName"].ToString().Trim().ToLower()) && (cbl.Items[icount].Value == dr["ProductID"].ToString()))
        //                {
        //                    cbl.Items[icount].Selected = (bool)dr["Result"];
        //                    //if (dr["SeasonName"].ToString().Trim().ToLower() == "1")
        //                    //    cbl.Items[icount].Selected = true;
        //                    //else
        //                    //    cbl.Items[icount].Selected = false;
        //                }
        //        }
        //    }
        //}
        //old code
        //dlFarmerSeasonDetails.DataSource = cp.GetSeasonDetails(ddlSeasonYear.SelectedValue.ToString());
        //dlFarmerSeasonDetails.DataBind();
        //int SeasonYear = Convert.ToInt32(ddlSeasonYear.SelectedValue.ToString());
        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

        //    cbl.DataSource = prod.GetProductDetailsbySeason(seasonId);
        //    cbl.DataTextField = "ProductName";
        //    cbl.DataValueField = "ProductID";
        //    cbl.DataBind();
        //}
        //DataTable dt = prod.GetProductDetails(lblfarmerUid.Text, ddlSeasonYear.SelectedValue.ToString());

        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    string sName = (dlFarmerSeasonDetails.Items[count].FindControl("lblseasonName") as Label).Text;
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

        //    if (cbl.Items.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            for (int icount = 0; icount < cbl.Items.Count; icount++)
        //                if ((sName.Trim().ToLower() == dr["SeasonName"].ToString().Trim().ToLower()) && (cbl.Items[icount].Value == dr["ProductID"].ToString()))
        //                {
        //                    cbl.Items[icount].Selected = (bool)dr["Result"];
        //                    //if (dr["SeasonName"].ToString().Trim().ToLower() == "1")
        //                    //    cbl.Items[icount].Selected = true;
        //                    //else
        //                    //    cbl.Items[icount].Selected = false;
        //                }
        //        }
        //    }
        //}
    }
    private void BindFiledRisk()
    {
        if (farmerobj.FieldRisk_Exist(lblfarmerUid.Text))
        {
            DataTable dt = farmerobj.FieldRisk(lblfarmerUid.Text);
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
            DataTable dt = farmerobj.FieldRiskResult(lblfarmerUid.Text);
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
    #endregion

    #region submit buttons

    #region FarmerdetailsSubmit
    //Farmerinfo submit code
    protected void btnFarmerNext_Click(object sender, EventArgs e)
    {
        try
        {
            
            string testpath = hfUserPhotoPath.Value;
            if (farmerobj.FarmerExist(lblfarmerUid.Text))
            {
                //update farmer
                btnTFarmerInfo.Enabled = true;
                btnTBankInfo.Enabled = true;
                result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, lblFarmerCode.Text, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Update, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked, string.Empty);
            }
            else
            {
                DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];

                string icsCode = farmerobj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
                //Insert farmer
                MudarApp farmercode = new MudarApp();
                string FarmerCode = farmercode.farmercode(txtCity.Text, txtState.Text);
                result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, FarmerCode, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Insert, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked, icsCode);
                lblFarmerCode.Text = FarmerCode;
            }
            divFarmerDetails.Visible = false;
            btnTFarmerInfo.Enabled = false;
            divBankDetails.Visible = true;
            btnTFarmerInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            btnTFarmerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
            btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=0" + ex.Message + "");
        }
    }
    #endregion

    #region FarmerBankinfo submit
    //farmerbank submitcode
    protected void btnBankNext_Click(object sender, EventArgs e)
    {
        result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, lblFarmerCode.Text, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, txtBankName.Text, txtAccountNo.Text, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Bhanu", txtFarmerRegNo.Text, string.Empty, 4, DateTime.Now, txtAcccountHolder.Text, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
        BindNewFarm();
        divBankDetails.Visible = false;
        btnTBankInfo.Enabled = false;
        divNewFarmDetails.Visible = true;
        btnTBankInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTaddPlots.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    #endregion

    #region FarmerFarminfo submit
    //write by Bhanu: farmerfarminfo save the farm data
    protected void btnNFarmNextt_Click(object sender, EventArgs e)
    {
        
        decimal TotalArea = 0;
        foreach (GridViewRow gvr in gvfarmdetails.Rows)
        {
            int nFarmID = (int)gvfarmdetails.DataKeys[gvr.RowIndex].Value;
            TotalArea = TotalArea + Convert.ToDecimal(!string.IsNullOrEmpty((gvr.FindControl("txtPSize") as TextBox).Text) ? (gvr.FindControl("txtPSize") as TextBox).Text : "0");
            if (nFarmID == 0)
            {
                farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, nFarmID, Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text), gvr.Cells[0].Text, Convert.ToInt32((gvr.FindControl("txtLatitude") as TextBox).Text), Convert.ToInt32((gvr.FindControl("txtLongtiude") as TextBox).Text), "Aslam", string.Empty, MudarApp.Insert, ref nFarmID, 0);
            }
            else
            {
                farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, nFarmID, Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text), gvr.Cells[0].Text, Convert.ToInt32((gvr.FindControl("txtLatitude") as TextBox).Text), Convert.ToInt32((gvr.FindControl("txtLongtiude") as TextBox).Text), string.Empty, "Aslam", MudarApp.Update, ref nFarmID, 0);
            }
        }
        result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, txtFarmerName.Text, string.Empty, string.Empty, TotalArea, gvfarmdetails.Rows.Count, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, 5, string.IsNullOrEmpty(txtLastDate.Text) ? DateTime.Now : Convert.ToDateTime(txtLastDate.Text), string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked, string.Empty);
        BindNewFarm();
        lblTotalArea.Text = TotalArea.ToString();
        btnNFarmBack.Visible = true;
        btnDisable.Visible = false;
        btnNFarmNext.Visible = false;
        btnDisableSubmit.Visible = true;
    }
    //write by Bhanu: farmerfarminfo submit for nextscreen 
    protected void btnNFarmNextBack_Click(object sender, EventArgs e)
    {
        divNewFarmDetails.Visible = false;
        btnTaddPlots.Enabled = false;
        divSeasonDetails.Visible = true;
        btnNFarmBack.Visible = true;
        btnTaddPlots.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTaddPlots.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTSeasonInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
       // BindSeason_Farmer();
    }
    #endregion

    #region farmerseason submit
    //farmer season submit code
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    string sName = (dlFarmerSeasonDetails.Items[count].FindControl("lblseasonName") as Label).Text;
        //    string sdate = (dlFarmerSeasonDetails.Items[count].FindControl("hfStartDate") as HiddenField).Value;
        //    string edate = (dlFarmerSeasonDetails.Items[count].FindControl("hfEndDate") as HiddenField).Value;
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);
        //    for (int icount = 0; icount < cbl.Items.Count; icount++)
        //    {
        //      farmerobj.FarmerSeasonProduct_INSandUPTandDEL(seasonId, lblfarmerUid.Text, sName, string.Empty, cbl.Items[icount].Selected, "Bhanu", "Bhanu", Convert.ToDateTime(sdate), Convert.ToDateTime(edate), Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), Convert.ToInt32(cbl.Items[icount].Value), 2);
        //    }
        //}

        //var dd=gvFarmerSeasonDetails.Columns;
        //List<SeasonProduct> seas = new List<SeasonProduct>();
        //for (int i = 2; i < gvFarmerSeasonDetails.Columns.Count; i++)
        //{
        //    if (i % 4 == 2)
        //    {
        //        SeasonProduct seaP = new SeasonProduct();
        //        seaP.SeasonName = gvFarmerSeasonDetails.Columns[i].HeaderText;
        //        seaP.SeasonId =Convert.ToInt32(gvFarmerSeasonDetails.Columns[i+1].HeaderText);
        //        seas.Add(seaP);
        //    }
        //}
        //foreach (GridViewRow item in gvFarmerSeasonDetails.Rows)
        //{
        //    if (item.RowType == DataControlRowType.DataRow)
        //    {
        //        for (int i = 0; i < item.Cells.Count; i++)
        //        {                  
        //            TableCell item1 = item.Cells[i];
        //            if (item1.Controls.OfType<CheckBox>().Count() > 0)
        //            {
        //                CheckBox chk = item1.Controls.OfType<CheckBox>().FirstOrDefault();
        //                if (chk != null && chk.Checked)
        //                {
        //                    int prodid = Convert.ToInt32(item.Cells[0].Text.Trim());
        //                    int seasonId = Convert.ToInt32(item.Cells[i + 1].Text.Trim());
        //                    DateTime startDate = Convert.ToDateTime(item.Cells[i + 2].Text.Trim());
        //                    DateTime endDate = Convert.ToDateTime(item.Cells[i + 3].Text.Trim());
        //                    farmerobj.FarmerSeasonProduct_INSandUPTandDEL(seasonId, lblfarmerUid.Text, seas.Where(m=>m.SeasonId==seasonId).FirstOrDefault().SeasonName, string.Empty, true, "Bhanu", "Bhanu", startDate, endDate, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), prodid, 2);
        //                }

        //            }
        //        }
        //    }
        //}

        var dd = gvFarmerSeasonDetails.Columns;
        List<SeasonProduct> seas = new List<SeasonProduct>();
        for (int i = 2; i < gvFarmerSeasonDetails.Columns.Count; i++)
        {
            if (i % 4 == 2)
            {
                SeasonProduct seaP = new SeasonProduct();
                seaP.SeasonName = gvFarmerSeasonDetails.Columns[i].HeaderText;
                seaP.SeasonId = Convert.ToInt32(gvFarmerSeasonDetails.Columns[i + 1].HeaderText);
                seas.Add(seaP);
            }
        }

        List<SeaProdModel> lstSeaProds = new List<SeaProdModel>();

        foreach (GridViewRow item in gvFarmerSeasonDetails.Rows)
        {
            if (item.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    TableCell item1 = item.Cells[i];
                    if (item1.Controls.OfType<CheckBox>().Count() > 0)
                    {
                        CheckBox chk = item1.Controls.OfType<CheckBox>().FirstOrDefault();
                        if (chk != null && chk.Checked)
                        {
                            SeaProdModel seaProd = new SeaProdModel();
                            seaProd.ProductId = Convert.ToInt32(item.Cells[0].Text.Trim());
                            seaProd.SeasonId = Convert.ToInt32(item.Cells[i + 1].Text.Trim());
                            seaProd.StartDate = Convert.ToDateTime(item.Cells[i + 2].Text.Trim());
                            seaProd.EndDate = Convert.ToDateTime(item.Cells[i + 3].Text.Trim());
                            seaProd.SeasonName = seas.Where(m => m.SeasonId == seaProd.SeasonId).FirstOrDefault().SeasonName;
                            lstSeaProds.Add(seaProd);
                            //int prodid = Convert.ToInt32(item.Cells[0].Text.Trim());
                            //int seasonId = Convert.ToInt32(item.Cells[i + 1].Text.Trim());
                            //DateTime startDate = Convert.ToDateTime(item.Cells[i + 2].Text.Trim());
                            //DateTime endDate = Convert.ToDateTime(item.Cells[i + 3].Text.Trim());




                            //farmerobj.FarmerSeasonProduct_INSandUPTandDEL(seasonId, lblfarmerUid.Text, seas.Where(m => m.SeasonId == seasonId).FirstOrDefault().SeasonName, string.Empty, true, "Bhanu", "Bhanu", startDate, endDate, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), prodid, 2);
                        }

                    }
                }
            }


        }

        

        DataTable dtDBData = cp.GetSeasonDetailsBasedonFarmerandYear(new Guid(lblfarmerUid.Text), ddlSeasonYear.SelectedValue);
        List<SeaProdModel> lstDBSeas = new List<SeaProdModel>();
        foreach (DataRow item in dtDBData.Rows)
        {
            SeaProdModel seaProd = new SeaProdModel();
            seaProd.SeasonId = Convert.ToInt32(item["m_SeasonID"]);
            seaProd.ProductId = Convert.ToInt32(item["ProductId"]);
            lstDBSeas.Add(seaProd);

            if (lstSeaProds.Where(m => m.ProductId == seaProd.ProductId && m.SeasonId == seaProd.SeasonId).Count() <= 0)
            {
                //lstDelProds.Add(seaProd);
                farmerobj.FarmerSeasonProduct_INSandUPTandDELNEW(seaProd.SeasonId, lblfarmerUid.Text, string.Empty, string.Empty, true, "Bhanu", "Bhanu", DateTime.Now, DateTime.Now, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), seaProd.ProductId, 3);
            }
        }

        foreach (SeaProdModel item in lstSeaProds)
        {
            if (lstDBSeas.Where(m => m.ProductId == item.ProductId && m.SeasonId == item.SeasonId).Count() <= 0)
            {
                farmerobj.FarmerSeasonProduct_INSandUPTandDELNEW(item.SeasonId, lblfarmerUid.Text, item.SeasonName, string.Empty, true, "Bhanu", "Bhanu", item.StartDate, item.EndDate, Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), item.ProductId, 1);
            }
        }

        BindFiledRisk();
        divSeasonDetails.Visible = false;
        btnTSeasonInfo.Enabled = false;
        divFieldRisk.Visible = true;
        btnTSeasonInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTSeasonInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTStandardRisk.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    #endregion

    #region Farmer FiledRiskinfo submit
    //farmer fieldrisk submit code
    protected void btnFieldRiskNext_Click(object sender, EventArgs e)
    {
        for (int count = 0; count < dlFieldRisk.Items.Count; count++)
        {
            string farmerID = lblfarmerUid.Text;
            int farmID = Convert.ToInt32((dlFieldRisk.Items[count].FindControl("hfFieldRisk") as HiddenField).Value);
            RadioButtonList rbl = (dlFieldRisk.Items[count].FindControl("rblFieldRisk") as RadioButtonList);
            for (int icount = 0; icount < rbl.Items.Count; icount++)
            {
                int fieldRiskID = Convert.ToInt32(rbl.Items[icount].Value);
                bool result = rbl.Items[icount].Selected;
                farmerobj.FiledRiskt_Result_INSandUPDandDEL(fieldRiskID, farmerID, farmID, result, "Aslam", "Aslam", MudarApp.Insert);
            }
        }
        divFieldRisk.Visible = false;
        btnTStandardRisk.Enabled = false;
        divFamilyDetails.Visible = true;
        btnTStandardRisk.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTStandardRisk.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTFamily.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

    }
    #endregion

    #region FarmerFamily submit
    protected void btnFamilyNext_Click(object sender, EventArgs e)
    {
        DateTime dob = DateTime.Now;
        farmerobj.FarmerFamilyDetails_INS(0, lblfarmerUid.Text, string.Empty, string.Empty, dob, 0, false, false, "bhanu", string.Empty, MudarApp.Update, Convert.ToInt32(txtEarningMember.Text), Convert.ToInt32(txtDependentElder.Text));
        lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
        BindChildDetails();
        ClearChildDetails();
        divFamilyDetails.Visible = false;
        btnTFamily.Enabled = false;
        divCattleDetails.Visible = true;
        btnTFamily.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTFamily.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTCattle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

    }
    #endregion

    #region FarmerFamily OK Button submit code
    //farmer family submit code
    protected void OkButton_Click(object sender, EventArgs e)
    {
        int age = DateTime.Now.Year - Convert.ToDateTime(txtDOB.Text).Year;
        farmerobj.FarmerFamilyDetails_INS(0, lblfarmerUid.Text, txtNameChild.Text, rbtnGender.SelectedItem.Value, Convert.ToDateTime(txtDOB.Text), age, cbSchool.Checked, cbWorking.Checked, "bhanu", string.Empty, MudarApp.Insert, Convert.ToInt32(txtEarningMember.Text), Convert.ToInt32(txtDependentElder.Text));
        lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
        BindChildDetails();
        ClearChildDetails();
    }
    #endregion

    #region Farmer Cattleinfo submit
    //farmer cattle submit
    protected void btbCattleNext_Click(object sender, EventArgs e)
    {

        string testpath = hfUserPhotoPath.Value;
        farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, Convert.ToInt32(txtCows.Text), Convert.ToInt32(txtOx.Text), Convert.ToInt32(txtSheep.Text), Convert.ToInt32(txtBuffalo.Text), false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, 7, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
        divCattleDetails.Visible = false;
        btnTCattle.Enabled = false;
        divForOfficeUse.Visible = true;
        btnTCattle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTCattle.ForeColor = System.Drawing.ColorTranslator.FromHtml("Gray");
        btnTOffice.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    #endregion

    #region FarmerOffice Info submit
    //farmeroffice submit code
    protected void officesubmit_Click(object sender, EventArgs e)
    {
        string farmerID = lblfarmerUid.Text;
        bool Result = false;
        Result = farmerobj.FarmerApproval(farmerID, txtuploadedInternalInspector.Text, txtCommentInternalInspector.Text, "Aslam", 2, ref Result);
        if (Result == true)
        {
            Response.Write("<script>alert('FARMER REGISTRATION SUCCESSFUL AND FORWARDED FOR APPROVAL');</script>");
            Response.Redirect("~/Farmer/Sample Farmer.aspx");
        }
        else
        {
            Response.Write("<script>alert('FARMER REGISTRATION SUCCESSFUL AND FORWARDED FOR APPROVAL');</script>");
            Response.Redirect("~/Farmer/Sample Farmer.aspx");
        }
    }
    #endregion

    #region Farmer Farm details
    protected void btnAddPlot_Click(object sender, EventArgs e)
    {
        BindSNewFarm();
        DataTable dt = (DataTable)Session["S_NewFarm"];
        DataRow dr = dt.NewRow();
        dr["FarmID"] = "0";
        dr["PlotArea"] = "0";
        dr["Latitude"] = "0";
        dr["Longitude"] = "0";
        dt.Rows.Add(dr);
        gvfarmdetails.DataSource = dt;
        gvfarmdetails.DataBind();
        lblTotalPlots.Text = gvfarmdetails.Rows.Count.ToString();
        btnNFarmBack.Visible = false;
        btnNFarmNext.Visible = true;
        btnDisable.Visible = true;
        btnNFarmNext.Visible = true;
        btnDisableSubmit.Visible = false;
    }

    protected void btnSaveFarmDetails_Click(object sender, EventArgs e)
    {
        decimal TotalArea = 0;
        foreach (GridViewRow gvr in gvfarmdetails.Rows)
        {
            int nFarmID = (int)gvfarmdetails.DataKeys[gvr.RowIndex].Value;
            TotalArea = TotalArea + Convert.ToDecimal(!string.IsNullOrEmpty((gvr.FindControl("txtPSize") as TextBox).Text) ? (gvr.FindControl("txtPSize") as TextBox).Text : "0");
            if (nFarmID == 0)
            {
                farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, nFarmID, Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text), gvr.Cells[0].Text, Convert.ToInt32((gvr.FindControl("txtLatitude") as TextBox).Text), Convert.ToInt32((gvr.FindControl("txtLongtiude") as TextBox).Text), "Aslam", string.Empty, MudarApp.Insert, ref nFarmID, 0);
            }
            else
            {
                farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, nFarmID, Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text), gvr.Cells[0].Text, Convert.ToInt32((gvr.FindControl("txtLatitude") as TextBox).Text), Convert.ToInt32((gvr.FindControl("txtLongtiude") as TextBox).Text), string.Empty, "Aslam", MudarApp.Update, ref nFarmID, 0);
            }
        }
        result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, txtFarmerName.Text, string.Empty, string.Empty, TotalArea, gvfarmdetails.Rows.Count, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, 5, string.IsNullOrEmpty(txtLastDate.Text) ? DateTime.Now : Convert.ToDateTime(txtLastDate.Text), string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
        BindNewFarm();
    }
    protected void gvfarmdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        BindSNewFarm();
        switch (e.CommandName)
        {
            case "Remove":
                {
                    GridViewRow gvr = gvfarmdetails.Rows[index];
                    int nFarmID = (int)gvfarmdetails.DataKeys[gvr.RowIndex].Value;
                    if (nFarmID == 0)
                    {
                        (Session["S_NewFarm"] as DataTable).Rows.RemoveAt(index);
                    }
                    else
                    {
                        farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, nFarmID, Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text), gvr.Cells[0].Text, Convert.ToInt32((gvr.FindControl("txtLatitude") as TextBox).Text), Convert.ToInt32((gvr.FindControl("txtLongtiude") as TextBox).Text), string.Empty, "Aslam", MudarApp.Delete, ref nFarmID, 0);
                        (Session["S_NewFarm"] as DataTable).Rows.RemoveAt(index);
                    }
                    gvfarmdetails.DataSource = (Session["S_NewFarm"] as DataTable);
                    gvfarmdetails.DataBind();
                }
                break;
        }

    }
    private void BindSNewFarm()
    {
        decimal totalarea = 0;
        for (int index = 0; index < gvfarmdetails.Rows.Count; index++)
        {
            GridViewRow gvr = gvfarmdetails.Rows[index];
            (Session["S_NewFarm"] as DataTable).Rows[index]["PlotArea"] = (gvr.FindControl("txtPSize") as TextBox).Text;
            (Session["S_NewFarm"] as DataTable).Rows[index]["Latitude"] = (gvr.FindControl("txtLatitude") as TextBox).Text;
            (Session["S_NewFarm"] as DataTable).Rows[index]["Longitude"] = (gvr.FindControl("txtLongtiude") as TextBox).Text;
            totalarea = totalarea + (!string.IsNullOrEmpty((gvr.FindControl("txtPSize") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtPSize") as TextBox).Text) : 0);
        }
        lblTotalArea.Text = totalarea.ToString();
    }
    private void BindNewFarm()
    {
        //DataTable dt = farmerobj.FarmDetails(lblfarmerUid.Text);
        DataRow[] drs = farmerobj.FarmDetails(lblfarmerUid.Text).Select("ParentFarmID  =0");
        DataTable dtPlot = farmerobj.FarmDetails(lblfarmerUid.Text).Clone();
        if (drs.Length > 0)
        {
            foreach (DataRow dr in drs)
            {
                dtPlot.ImportRow(dr);
            }
            gvfarmdetails.DataSource = dtPlot;
            gvfarmdetails.DataBind();
            Session["S_NewFarm"] = dtPlot;
        }
        else
        {
            Session["S_NewFarm"] = dtPlot;
        }
    }
    #endregion

    #endregion

    #region old Code not used

    protected void btnBankSubmit_Click(object sender, EventArgs e)
    {

        //update only bank details.
        //result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, lblFarmerCode.Text, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, txtBankName.Text, txtAccountNo.Text, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Bhanu", txtFarmerRegNo.Text, string.Empty, 4, DateTime.Now);

    }
    protected void btnFarmSubmit_Click(object sender, EventArgs e)
    {
        //bool result = false;
        //int farmid = 0, cfarmid = 0;
        //BindDatalistViewState();
        //DataTable farm = (DataTable)Session["session_Farm"];
        //for (int count = 0; count < farm.Rows.Count; count++)
        //{
        //    if (farm.Rows[count]["C_P"].ToString() == "P")
        //    {
        //        if (Convert.ToInt32(farm.Rows[count]["ChildCount"].ToString()) > 0)
        //            farm.Rows[count]["PlotArea"] = "0";
        //        for (int row = 0; row < Convert.ToInt32(farm.Rows[count]["ChildCount"].ToString()); row++)
        //        {
        //            farm.Rows[count]["PlotArea"] = Convert.ToDecimal(farm.Rows[count]["PlotArea"].ToString()) + Convert.ToDecimal(farm.Rows[count + row + 1]["PlotArea"].ToString());
        //        }
        //    }
        //}
        //decimal TotalArea = 0; int plottotalcount = 0;
        //if (farm.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in farm.Rows)
        //    {
        //        if (dr["C_P"].ToString() == "P")
        //        {
        //            result = farmerobj.Farm_INSandUPTandDEL(dr["FarmerID"].ToString(), Convert.ToInt16(dr["FarmID"].ToString()), string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString()), dr["AreaCode"].ToString(), string.IsNullOrEmpty(dr["Latitude"].ToString()) ? 0 : Convert.ToDecimal(dr["Latitude"].ToString()), string.IsNullOrEmpty(dr["Longitude"].ToString()) ? 0 : Convert.ToDecimal(dr["Longitude"].ToString()), "Shaik Aslam", "Shaik Aslam", MudarApp.Insert, ref farmid, 0);
        //            TotalArea += string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString());
        //            plottotalcount += 1;
        //        }
        //        else if (dr["C_P"].ToString() == "C")
        //            result = farmerobj.Farm_INSandUPTandDEL(dr["FarmerID"].ToString(), Convert.ToInt16(dr["FarmID"].ToString()), string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString()), dr["AreaCode"].ToString(), string.IsNullOrEmpty(dr["Latitude"].ToString()) ? 0 : Convert.ToDecimal(dr["Latitude"].ToString()), string.IsNullOrEmpty(dr["Longitude"].ToString()) ? 0 : Convert.ToDecimal(dr["Longitude"].ToString()), "Shaik Aslam", "Shaik Aslam", MudarApp.Insert, ref cfarmid, farmid);

        //    }
        //    result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, txtFarmerName.Text, string.Empty, string.Empty, TotalArea, plottotalcount, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, 5, string.IsNullOrEmpty(txtLastDate.Text) ? DateTime.Now : Convert.ToDateTime(txtLastDate.Text));
        //}
        //lblTotalArea.Text = TotalArea.ToString();
        //lblTotalPlots.Text = plottotalcount.ToString();
        //BindFarmtoSession();
        //BindFarm();
        //DisableParentRow();
    }
    protected void btnBankPrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 0;
    }
    protected void btnFarmPrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 1;
    }
    protected void btnFarmNext_Click(object sender, EventArgs e)
    {
        bool result = false;
        int farmid = 0, cfarmid = 0;
        BindDatalistViewState();
        DataTable farm = (DataTable)Session["session_Farm"];
        for (int count = 0; count < farm.Rows.Count; count++)
        {
            if (farm.Rows[count]["C_P"].ToString() == "P")
            {
                if (Convert.ToInt32(farm.Rows[count]["ChildCount"].ToString()) > 0)
                    farm.Rows[count]["PlotArea"] = "0";
                for (int row = 0; row < Convert.ToInt32(farm.Rows[count]["ChildCount"].ToString()); row++)
                {
                    farm.Rows[count]["PlotArea"] = Convert.ToDecimal(farm.Rows[count]["PlotArea"].ToString()) + Convert.ToDecimal(farm.Rows[count + row + 1]["PlotArea"].ToString());
                }
            }
        }
        decimal TotalArea = 0; int plottotalcount = 0;
        if (farm.Rows.Count > 0)
        {
            foreach (DataRow dr in farm.Rows)
            {
                if (dr["C_P"].ToString() == "P")
                {
                    result = farmerobj.Farm_INSandUPTandDEL(dr["FarmerID"].ToString(), Convert.ToInt16(dr["FarmID"].ToString()), string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString()), dr["AreaCode"].ToString(), string.IsNullOrEmpty(dr["Latitude"].ToString()) ? 0 : Convert.ToInt32(dr["Latitude"].ToString()), string.IsNullOrEmpty(dr["Longitude"].ToString()) ? 0 : Convert.ToInt32(dr["Longitude"].ToString()), "Shaik Aslam", "Shaik Aslam", MudarApp.Insert, ref farmid, 0);
                    TotalArea += string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString());
                    plottotalcount += 1;
                }
                else if (dr["C_P"].ToString() == "C")
                    result = farmerobj.Farm_INSandUPTandDEL(dr["FarmerID"].ToString(), Convert.ToInt16(dr["FarmID"].ToString()), string.IsNullOrEmpty(dr["PlotArea"].ToString()) ? 0 : Convert.ToDecimal(dr["PlotArea"].ToString()), dr["AreaCode"].ToString(), string.IsNullOrEmpty(dr["Latitude"].ToString()) ? 0 : Convert.ToInt32(dr["Latitude"].ToString()), string.IsNullOrEmpty(dr["Longitude"].ToString()) ? 0 : Convert.ToInt32(dr["Longitude"].ToString()), "Shaik Aslam", "Shaik Aslam", MudarApp.Insert, ref cfarmid, farmid);

            }
            result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, txtFarmerName.Text, string.Empty, string.Empty, TotalArea, plottotalcount, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, 5, string.IsNullOrEmpty(txtLastDate.Text) ? DateTime.Now : Convert.ToDateTime(txtLastDate.Text), string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
        }
        lblTotalArea.Text = TotalArea.ToString();
        lblTotalPlots.Text = plottotalcount.ToString();
        BindFarmtoSession();
        BindFarm();
        DisableParentRow();
        //MainFarmerView.ActiveViewIndex = 3;
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 9;
        ////MainFarmerView.ActiveViewIndex = 2;
    }
    protected void btnFamilyPrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 7;
    }
    protected void btnCattlePrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 4;
    }
    protected void btnOfficePrev_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 5;
    }
    protected void btnFieldRiskBack_Click(object sender, EventArgs e)
    {
        //MainFarmerView.ActiveViewIndex = 3;
    }
    protected void dlFarmdetails_ItemCommand(object source, DataListCommandEventArgs e)
    {
        BindDatalistViewState();

        string command = e.CommandName;
        DataTable NewTable = new DataTable();
        NewTable.Columns.Add("FarmerID");
        NewTable.Columns.Add("FarmID");
        NewTable.Columns.Add("Exp_Col");
        NewTable.Columns.Add("Plot");
        NewTable.Columns.Add("PlotArea");
        NewTable.Columns.Add("Latitude");
        NewTable.Columns.Add("Longitude");
        NewTable.Columns.Add("AreaCode");
        NewTable.Columns.Add("IMECode");
        NewTable.Columns.Add("TracenetCode");
        NewTable.Columns.Add("C_P");
        NewTable.Columns.Add("ParentID");
        NewTable.Columns.Add("ChildCount");

        switch (command)
        {
            case "addfarm":
                {
                    DataRow NewRow = NewTable.NewRow();
                    NewRow["FarmerID"] = lblfarmerUid.Text;
                    NewRow["FarmID"] = 0;
                    NewRow["Exp_Col"] = "~/images/arrow.jpg";
                    NewRow["Plot"] = "Plot" + (((DataTable)Session["session_Farm"]).Rows.Count + 1);
                    NewRow["PlotArea"] = 0;
                    NewRow["Latitude"] = 0;
                    NewRow["Longitude"] = 0;
                    NewRow["AreaCode"] = txtFarmerRegNo.Text + "0" + (((DataTable)Session["session_Farm"]).Rows.Count + 1);
                    NewRow["IMECode"] = 0;
                    NewRow["TracenetCode"] = 0;
                    NewRow["C_P"] = "P";
                    NewRow["ParentID"] = 0;
                    NewRow["ChildCount"] = 0;
                    NewTable.Rows.Add(NewRow);
                    if (NewTable.Rows.Count > 0)
                    {
                        DataRow FarmNewRow = ((DataTable)Session["session_Farm"]).NewRow();
                        for (int count = 0; count < FarmNewRow.ItemArray.Length; count++)
                        {
                            //if(count==1)
                            //    FarmNewRow[count] = ((DataTable)Session["session_Farm"]).Rows.Count;
                            //else 
                            FarmNewRow[count] = NewRow[count];
                        }
                        ((DataTable)Session["session_Farm"]).Rows.Add(FarmNewRow);
                        BindFarm();
                        for (int count = 0; count < dlFarmdetails.Items.Count; count++)
                        {
                            DataListItem item = dlFarmdetails.Items[count];
                            Label PlotCrop = item.FindControl("lblC_P") as Label;
                            if (PlotCrop.Text == "C")
                            {
                                ((ImageButton)dlFarmdetails.Items[count].FindControl("imgbtnExpand_Collapse")).Visible = false;
                                ((Button)dlFarmdetails.Items[count].FindControl("btnAddChild")).Visible = false;
                            }
                        }
                    }
                }
                break;
            case "addchildfarm":
                {
                    int Index = e.Item.ItemIndex;
                    DataRow NewRow = NewTable.NewRow();
                    NewRow["FarmerID"] = lblfarmerUid.Text;
                    NewRow["FarmID"] = 0;
                    NewRow["Exp_Col"] = "~/images/arrow.JPG";
                    NewRow["Plot"] = "Crop" + (((DataTable)Session["session_Farm"]).Rows.Count + 1);
                    NewRow["PlotArea"] = 0;
                    NewRow["Latitude"] = 0;
                    NewRow["Longitude"] = 0;
                    NewRow["AreaCode"] = txtFarmerRegNo.Text + (((DataTable)Session["session_Farm"]).Rows.Count + 1);
                    NewRow["IMECode"] = 0;
                    NewRow["TracenetCode"] = 0;
                    NewRow["C_P"] = "C";
                    NewRow["ParentID"] = Index.ToString();
                    NewRow["ChildCount"] = 0;

                    NewTable.Rows.Add(NewRow);
                    if (NewTable.Rows.Count > 0)
                    {
                        DataRow FarmNewRow = ((DataTable)Session["session_Farm"]).NewRow();
                        for (int count = 0; count < FarmNewRow.ItemArray.Length; count++)
                            FarmNewRow[count] = NewRow[count];
                        //FarmNewRow["Exp_Col"] = "~/images/collapse.JPG";
                        ((DataTable)Session["session_Farm"]).Rows[Index]["Exp_Col"] = "~/images/arrow.JPG";
                        ((DataTable)Session["session_Farm"]).Rows[Index]["ChildCount"] = Convert.ToInt32(((DataTable)Session["session_Farm"]).Rows[Index]["ChildCount"].ToString()) + 1;
                        ((DataTable)Session["session_Farm"]).Rows.InsertAt(FarmNewRow, Index + 1);
                        BindFarm();

                        for (int count = 0; count < dlFarmdetails.Items.Count; count++)
                        {
                            DataListItem item = dlFarmdetails.Items[count];
                            Label PlotCrop = item.FindControl("lblC_P") as Label;
                            if (PlotCrop.Text == "C")
                            {
                                ((ImageButton)dlFarmdetails.Items[count].FindControl("imgbtnExpand_Collapse")).Visible = false;
                                ((Button)dlFarmdetails.Items[count].FindControl("btnAddChild")).Visible = false;
                            }
                        }
                    }
                }
                break;
            case "Exp_Col":
                {
                    int Index = e.Item.ItemIndex;
                    DataListItem item = dlFarmdetails.Items[Index];
                    Label test = item.FindControl("lblC_P") as Label;
                    ImageButton img = item.FindControl("imgbtnExpand_Collapse") as ImageButton;
                    if (img.ImageUrl == "~/images/arrow.JPG")
                    {
                        ((ImageButton)dlFarmdetails.Items[Index].FindControl("imgbtnExpand_Collapse")).ImageUrl = "~/images/arrow.JPG";
                    }
                    else if (img.ImageUrl == "~/images/arrow.JPG")
                    {
                        ((ImageButton)dlFarmdetails.Items[Index].FindControl("imgbtnExpand_Collapse")).ImageUrl = "~/images/arrow.JPG";
                    }
                }
                break;
            case "delete":
                {
                    int Index = e.Item.ItemIndex;
                    DataListItem items = dlFarmdetails.Items[Index];
                    Label Plot = items.FindControl("lblC_P") as Label;
                    Label Dfarmid = items.FindControl("lblFarmId") as Label;
                    if (((DataTable)Session["session_Farm"]).Rows[Index]["FarmID"].ToString() == "0")
                    {
                        if (Plot.Text == "C")
                        {
                            int delete = 0;
                            for (int count = Index; count >= 0; count--)
                            {
                                if (delete == 0 && ((DataTable)Session["session_Farm"]).Rows[count]["C_P"].ToString() == "P")
                                {
                                    ((DataTable)Session["session_Farm"]).Rows[count]["ChildCount"] = (Convert.ToInt32((((DataTable)Session["session_Farm"]).Rows[count]["ChildCount"].ToString())) - 1).ToString();
                                    delete += 1;
                                }
                            }
                            ((DataTable)Session["session_Farm"]).Rows.RemoveAt(Index);
                        }
                        if (Plot.Text == "P")
                        {
                            int min = Index, max = 0, plotcount = 0;
                            for (int rowcount = min + 1; rowcount < ((DataTable)Session["session_Farm"]).Rows.Count; rowcount++)
                            {
                                if (plotcount == 0)
                                {
                                    if (((DataTable)Session["session_Farm"]).Rows[rowcount]["C_P"].ToString() == "C")
                                        max += 1;
                                    else if (((DataTable)Session["session_Farm"]).Rows[rowcount]["C_P"].ToString() == "P")
                                        plotcount += 1;
                                }
                            }
                            for (int row = max + min; row >= min; row--)
                                ((DataTable)Session["session_Farm"]).Rows.RemoveAt(row);
                        }
                    }
                    if (Convert.ToInt32(Dfarmid.Text) > 0)
                    {
                        int test = 0;
                        result = farmerobj.Farm_INSandUPTandDEL(lblfarmerUid.Text, Convert.ToInt32(Dfarmid.Text), 0, "0", 0, 0, "aslam", "Aslam Shaik", MudarApp.Delete, ref test, 0);
                        BindFarmtoSession();
                    }
                    BindFarm();
                    for (int count = 0; count < dlFarmdetails.Items.Count; count++)
                    {
                        DataListItem item = dlFarmdetails.Items[count];
                        Label PlotCrop = item.FindControl("lblC_P") as Label;
                        if (PlotCrop.Text == "C")
                        {
                            ((ImageButton)dlFarmdetails.Items[count].FindControl("imgbtnExpand_Collapse")).Visible = false;
                            ((Button)dlFarmdetails.Items[count].FindControl("btnAddChild")).Visible = false;
                        }
                    }
                }
                break;
        }
        DisableParentRow();
    }
    protected void dlFarmdetails_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //int Index = e.Item.ItemIndex;
        //if (Index > -1)
        //{
        //    if (dlFarmdetails.Items.Count > 0)
        //    {
        //        DataListItem item = dlFarmdetails.Items[Index];
        //        ImageButton img = item.FindControl("imgbtnExpand_Collapse") as ImageButton;
        //        Label test = item.FindControl("lblC_P") as Label;
        //    }
        //}
    }
    protected void btnSeasonDetailsSubmit_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow gvr in gvSeasonalProduct.Rows)
        //{
        //    CheckBox cb = gvr.Cells[3].FindControl("cbproduct") as CheckBox;
        //    if (cb.Checked)
        //    {
        //        //string str = gvr.Cells[0].Text + "/" + gvr.Cells[1].Text + "/" + gvr.Cells[2].Text;
        //        //string str = gvSeasonDetails.DataKeys[gvr.RowIndex].Value.ToString();
        //        farmerobj.FarmerSeasonProduct_INSandUPTandDEL(0, lblfarmerUid.Text, gvr.Cells[2].Text, string.Empty, true, "Bhanu", "Bhanu", Convert.ToDateTime(lblSeasonStart.Text), Convert.ToDateTime(lblSeasonEnd.Text), Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), Convert.ToInt32(gvr.Cells[0].Text), 2);
        //    }
        //}
    }
    protected void btnFamilySubmit_Click(object sender, EventArgs e)
    {

    }
    protected void btbCattleSubmit_Click(object sender, EventArgs e)
    {
        //string testpath = hfUserPhotoPath.Value;
        //farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, Convert.ToInt32(txtCows.Text), Convert.ToInt32(txtOx.Text), Convert.ToInt32(txtSheep.Text), Convert.ToInt32(txtBuffalo.Text), false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, 7, DateTime.Now);
    }
    protected void gvSeasonalProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvFamilyDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            farmerobj.FarmerFamilyDetails_INS(Convert.ToInt32(gvFamilyDet.DataKeys[index].Value.ToString()), lblfarmerUid.Text, string.Empty, string.Empty, DateTime.Now, 0, false, false, string.Empty, "bhanu", MudarApp.Delete, 0, 0);
            BindChildDetails();
        }
    }
    protected void ddlSeasonYearByFarmer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridBindSeasonDetailsBasedFarmerID();
    }
    protected void dlFarmdetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Seasond = cp.GetSeasonDetails();
        //if (Seasond.Rows.Count > 0)
        //{
        //    DataRow[] drcol = Seasond.Select("SeasonYear = " + ddlSeasonYear.SelectedValue.ToString() + " AND SeasonName LIKE '" + ddlSeason.Text + "'");
        //    if (drcol.Length > 0)
        //    {
        //        DataRow dr = drcol[0];
        //        lblSeasonStart.Text = dr["StartDate"].ToString();
        //        lblSeasonEnd.Text = dr["EndDate"].ToString();
        //        DataTable ptable = prod.GetProductDetails();
        //        DataRow[] proddr = ptable.Select(" SeasonName = '" + ddlSeason.Text + "'");
        //        DataTable temp = ptable.Clone();
        //        foreach (DataRow pdr in proddr)
        //        {
        //            DataRow newdr = temp.NewRow();
        //            for (int count = 0; count < pdr.ItemArray.Length; count++)
        //            {
        //                newdr[count] = pdr[count];
        //            }
        //            temp.Rows.Add(newdr);
        //        }
        //        gvSeasonalProduct.DataSource = temp;
        //        gvSeasonalProduct.DataBind();
        //    }
        //}
    }
    #endregion

    #region Tab design
    protected void btnTFarmerInfo_Click(object sender, EventArgs e)
    {
        divFarmerDetails.Visible = true;
        btnTFarmerInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTFarmerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTBankInfo_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        divBankDetails.Visible = true;
        divFarmerDetails.Visible = false;
        btnTBankInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTBankInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTaddPlots_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        divNewFarmDetails.Visible = true;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTaddPlots.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTaddPlots.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTSeasonInfo_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTaddPlots.Enabled = false;
        divSeasonDetails.Visible = true;
        divNewFarmDetails.Visible = false;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTSeasonInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTSeasonInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTStandardRisk_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTaddPlots.Enabled = false;
        btnTSeasonInfo.Enabled = false;
        divFieldRisk.Visible = true;
        divSeasonDetails.Visible = false;
        divNewFarmDetails.Visible = false;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTStandardRisk.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTStandardRisk.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTFamily_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTaddPlots.Enabled = false;
        btnTSeasonInfo.Enabled = false;
        btnTStandardRisk.Enabled = false;
        divFamilyDetails.Visible = true;
        divFieldRisk.Visible = false;
        divSeasonDetails.Visible = false;
        divNewFarmDetails.Visible = false;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTFamily.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTFamily.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTCattle_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTaddPlots.Enabled = false;
        btnTSeasonInfo.Enabled = false;
        btnTStandardRisk.Enabled = false;
        btnTFamily.Enabled = false;
        btnTCattle.Enabled = false;
        divCattleDetails.Visible = true;
        divFamilyDetails.Visible = false;
        divFieldRisk.Visible = false;
        divSeasonDetails.Visible = false;
        divNewFarmDetails.Visible = false;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTCattle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTCattle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnTOffice_Click(object sender, EventArgs e)
    {
        btnTFarmerInfo.Enabled = false;
        btnTBankInfo.Enabled = false;
        btnTaddPlots.Enabled = false;
        btnTSeasonInfo.Enabled = false;
        btnTStandardRisk.Enabled = false;
        btnTFamily.Enabled = false;
        btnTCattle.Enabled = false;
        divForOfficeUse.Visible = true;
        divCattleDetails.Visible = false;
        divFamilyDetails.Visible = false;
        divFieldRisk.Visible = false;
        divSeasonDetails.Visible = false;
        divNewFarmDetails.Visible = false;
        divBankDetails.Visible = false;
        divFarmerDetails.Visible = false;
        btnTOffice.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTOffice.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    #endregion
    protected void gvFarmerSeasonDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i >= 2 && i % 4 == 2)
                {
                    CheckBox chk = new CheckBox();                   
                    e.Row.Cells[i].Controls.Add(chk);
                }
                else if (i == 0 || (i >= 2 && (i % 4 == 1 || i % 4 == 3 || i % 4 == 0)))
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i == 0 || (i >= 2 && (i % 4 == 1 || i % 4 == 3 || i % 4 == 0)))
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
        }
    }
}
