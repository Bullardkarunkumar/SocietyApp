using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using MudarOrganic.BL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Web.Configuration;

public partial class Farmer_Farmerview : System.Web.UI.Page
{
    bool result = false;
    Farmer_BL farmerobj = new Farmer_BL();
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL prod = new Product_BL();
    MudarUser mu = new MudarUser();
    MudarApp mudarObj = new MudarApp();
    Reports_BL reportObj = new Reports_BL();
    InspectionPlan_BL inobj = new InspectionPlan_BL();
    public string userPath = string.Empty;
    private string _code;
    public string code
    {
        get { return _code; }
        set { _code = value; }
    }
    bool editmode = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["FarmerCode"].ToString()))
                lblFCode.Text = Request.QueryString["FarmerCode"].ToString().Trim();
            else
                lblFCode.Text = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["BackUrl"].ToString()))
                btnBack.PostBackUrl = Request.QueryString["BackUrl"].ToString().Trim();
            ddlSeasonYear.Enabled = false;
            //ddlSeasonYear.Items.Add(DateTime.Now.Year.ToString());
            BindFarmer();

            imgFarmerP.ImageUrl = WebConfigurationManager.AppSettings["FarmerIcon"];
            //for edit purpose
            _code = lblFCode.Text;
            Master.MasterControlbtnFarmerInfo();
            //bindFarmerDetails(_code);

        }
        BindSeason_Farmer();
    }
    private void BindFarmer()
    {
        try
        {
            DataTable farmerdata = farmerobj.FamerDetails(lblFCode.Text); //farmerobj.FamerDetails(farmerName, farmerCode, area);
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
                lblFCode.Text = farmer["FarmerCode"].ToString();
                lblFarmerRegistration.Text = farmer["FarmerRegNumber"].ToString();
                lblBankName.Text = farmer["BankInfo"].ToString();
                lblHolderName.Text = farmer["BankHolderName"].ToString();
                lblAcctNo.Text = farmer["BankAccNo"].ToString();
                //lblChemicalDate.Text =(string.Format("{0:dd - MMM - yyyy}", Convert.ToDateTime(farmer["ChemicalAppDate"].ToString()));
                lblChemicalDate.Text = farmer["ChemicalAppDate"].ToString();
                //rbOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false);
                //rbNonOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["OrganicFair"].ToString()) ? farmer["OrganicFair"] : false);
                if ((bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false == true))
                {
                    lblOrg.Text = "Yes";
                }
                else
                {
                    lblNonOrganic.Text = "Yes";
                }
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
                //BindSeason_Farmer();

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
        catch (Exception ex)
        {
            Response.Write("Error is" + ex.Message);
        }
    }
    public void FillYearDropDown()
    {
        //int Year;
        //int StartYear = 2013;
        //int EndYear = DateTime.Now.Year;
        //ddlSeasonYear.Items.Clear();
        //for (Year = StartYear; Year <= EndYear; Year++)
        //{
        //    ddlSeasonYear.Items.Add(Year.ToString());
        //}


        DataTable Seasond = cp.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlSeasonYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlSeasonYear.DataTextField = "SeasonYear";
            ddlSeasonYear.DataValueField = "SeasonYear";
            ddlSeasonYear.DataBind();
            ddlSeasonYear.Items.Insert(0, MudarApp.AddListItem());
            ddlSeasonYear.SelectedValue = DateTime.Now.Year.ToString();
        }
        //ddlSeasonYear.Items.Insert(0, "--Select Year--");
    }
    private void BindSeason_Farmer()
    {
        FillYearDropDown();
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


        //FillYearDropDown();
        //string seasonYr = DateTime.Now.Year.ToString(); //ddlSeasonYear.SelectedValue;
        //dlFarmerSeasonDetails.DataSource = cp.GetSeasonDetails(seasonYr);
        //dlFarmerSeasonDetails.DataBind();
        //int SeasonYear = Convert.ToInt32(seasonYr);
        //for (int count = 0; count < dlFarmerSeasonDetails.Items.Count; count++)
        //{
        //    int seasonId = Convert.ToInt32(dlFarmerSeasonDetails.DataKeys[count].ToString());
        //    CheckBoxList cbl = (dlFarmerSeasonDetails.Items[count].FindControl("cblProduct") as CheckBoxList);

        //    cbl.DataSource = prod.GetProductDetailsbySeasonNew(seasonId);
        //    cbl.DataTextField = "ProductName";
        //    cbl.DataValueField = "ProductID";
        //    cbl.DataBind();
        //}
        //DataTable dt = prod.GetProductDetails(hdfarmerUid.Value, seasonYr);

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
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
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
        //
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    private void generatePDF()
    {
        string strpdf = "<table  style='border:5px solid green;' border='1' width='100%' ><tr>";
        strpdf += "<td colspan='2' align='center' bgcolor='#CCCCCC'>Farmer Information</td></tr><tr>";
        strpdf += "<td align='center' width='50%'> Farmer name</td><td width='50%'>&nbsp;" + lblFarmerName.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Total Area in Hc<br />" + lblTotalArea.Text + " </td><td align='center'>Total number of Plots<br />" + lblPlots.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Father name</td><td width='50%'>&nbsp;" + lblFatherName.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Address</td><td width='50%'>" + lblAddress.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Fixed Phone</td><td width='50%'>&nbsp;" + lblPhone.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Mobile Phone</td><td width='50%'>&nbsp;" + lblMobile.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Farmer Code</td><td width='50%'>&nbsp;" + lblFCode.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Farmer Registration no</td><td width='50%'>&nbsp;" + lblFarmerRegistration.Text + "</td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Bank Information</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Name of the Bank</td><td width='50%'>&nbsp;" + lblBankName.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>A/C holder name</td><td width='50%'>&nbsp;" + lblHolderName.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>A/C Number</td><td width='50%'>&nbsp;" + lblAcctNo.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Last date of chemical application</td><td width='50%'>&nbsp;" + lblChemicalDate.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Organic</td><td width='50%'>&nbsp;" + lblOrg.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Org & Fair Trade</td><td width='50%'>&nbsp;" + lblNonOrganic.Text + "</td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Plot Information</td></tr><tr>";
        //grid Plot details
        strpdf += "<td colspan='2' align='center' width='50%'><table>";
        strpdf += "<tr align='center'><td>Plot Code</td><td>Area in Hc</td><td>Latitude</td><td>Longitude</td></tr>";
        foreach (GridViewRow gvr in gvPlot.Rows)
            strpdf += "<tr align='center'><td>" + gvr.Cells[0].Text + "</td><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td></tr>";
        //grid end
        strpdf += "</table></td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Season Information</td></tr><tr>  ";
        strpdf += "<td colspan='2' align='center' >need to do some changes</td></tr><tr>";
        strpdf += "<td colspan='2' align='center' width='50%'><table>";


        DataTable dtSeason = cp.GetSeasonDetails(ddlSeasonYear.SelectedValue);
        foreach (DataRow item in dtSeason.Rows)
        {
            DataTable dtProdNames = cp.GetProductNameByFarmerandSeason(Convert.ToInt32(item["SeasonID"]), new Guid(hdfarmerUid.Value), Convert.ToInt32(ddlSeasonYear.SelectedValue));
            string prodStr = string.Empty;
            foreach (DataRow item1 in dtProdNames.Rows)
            {
                prodStr += Convert.ToString(item1["ProductName"]) + ",";
            }
            if (!string.IsNullOrEmpty(prodStr))
            {
                prodStr = prodStr.Substring(0, prodStr.Length - 1);
                strpdf += "<tr align='center'><td>" + Convert.ToString(item["SeasonName"]) + "</td><td>" + prodStr + "</td></tr>";
            }
        }
        //foreach (DataListItem dl in dlFarmerSeasonDetails.Items)
        //{
        //    CheckBoxList cbl = (CheckBoxList)dl.FindControl("cblProduct");
        //    string seasonYr = ddlSeasonYear.SelectedValue;

        //    string productlist =string.Empty;
        //    for (int count = 0; count < cbl.Items.Count;count++ )
        //    {
        //        if (cbl.Items[count].Selected)
        //            productlist += cbl.Items[count].Text + "  ";
        //    }
        //    strpdf += "<tr align='center'><td>" + (dl.FindControl("lblseasonName") as Label).Text + "</td><td>" + productlist + "</td></tr>";
        //}
        strpdf += "</table></td></tr><tr>";
        strpdf += " <td align='center' colspan='2' bgcolor='#CCCCCC'>Risk Information</td></tr><tr>";
        strpdf += "<td colspan='2' align='center' >need to do some changes</td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Family Information</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Earning members</td><td width='50%'>&nbsp;" + lblEarningMember.Text + " </td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Dependent Elders</td><td width='50%'>&nbsp;" + lblDElder.Text + " </td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Dependent Children</td><td width='50%'>&nbsp;" + lblDChildren.Text + "</td></tr><tr>";
        //grid Family details
        strpdf += "<td colspan='2' align='center' width='50%'><table>";
        strpdf += "<tr align='center'><td>Child Name</td><td>Gender</td><td>DateofBirth</td><td>Age</td><td>School Going</td><td>Work Going</td></tr>";
        foreach (GridViewRow gvr in gvFamilyDet.Rows)
            strpdf += "<tr align='center'><td>" + gvr.Cells[0].Text + "</td><td>" + gvr.Cells[1].Text + "</td><td>" + gvr.Cells[2].Text + "</td><td>" + gvr.Cells[3].Text + "</td><td>" + gvr.Cells[4].Text + "</td><td>" + gvr.Cells[5].Text + "</td></tr>";
        //grid end
        strpdf += "</table></td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Animal Husbandry</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Buffalos</td><td width='50%'>&nbsp;" + lblBuffalos.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Cows</td><td width='50%'>&nbsp;" + lblCows.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Ox</td><td width='50%'>&nbsp;" + lblOx.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Sheep</td><td width='50%'>&nbsp;" + lblSheep.Text + "</td></tr><tr>";
        strpdf += "<td align='center' colspan='2' bgcolor='#CCCCCC'>Upload Details</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Comments from Branch</td><td width='50%'>&nbsp;" + lblComments.Text + "</td></tr><tr>";
        strpdf += "<td align='center' width='50%'>Uploaded by </td><td width='50%'>&nbsp;" + lblUploadedBy.Text + "</td></tr></table>";
        //report
        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder(hdfarmerUid.Value, MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + hdfarmerUid.Value + "/FarmerDoc_" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + "/FarmerDoc_" + hdfarmerUid.Value + ".pdf";
            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();




            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();


            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            ////add the collection to the document
            //for (int k = 0; k < htmlarraylist.Count; k++)
            //{
            //    document.Add((IElement)htmlarraylist[k]);
            //}

            //document.Add(new Paragraph("And the same with indentation...."));

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();


            //bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Invoice);


        }
        catch (Exception exx)
        {
            Response.Write("<br>____________________________________<br>");
            Response.Write("<br>Error: " + exx + "<br>");
            Response.Write("<br>StackTrace: " + exx.StackTrace + "<br>");
            Response.Write("<br>strPDFDocument: " + strpdf.ToString() + "<br>");
            Response.Write("<br>strSelectUserListBuilder: " + strpdf.ToString() + "<br>");

            //Console.Error.WriteLine(exx.StackTrace);
            //Console.Error.WriteLine(exx.StackTrace);
        }
        finally
        {
            //document.Close();
        }

    }

    #region Farmer Edit all information

    #region Farmer Edit
    protected void lbtnFarmerEdit_Click(object sender, EventArgs e)
    {
        divfarmerInfoView.Visible = false;
        divFarmerDetails.Visible = true;
        //edit option for bind method
        bindFarmerDetails();
    }

    protected void lbtnBankEdit_Click(object sender, EventArgs e)
    {
        txtAcccountHolder.Text = txtFarmerName.Text;
        divbankinfoView.Visible = false;
        divBankDetails.Visible = true;
        bindFarmerDetails();
    }
    protected void lbtnPlotEdit_Click(object sender, EventArgs e)
    {
        divplotinfoview.Visible = false;
        divNewFarmDetails.Visible = true;
        bindFarmerDetails();
    }
    protected void lbtnSeasonEdit_Click(object sender, EventArgs e)
    {
        ddlSeasonYear.Enabled = true;
        //System.Web.UI.WebControls.ListItemCollection items = MudarApp.BindYear();
        //foreach (System.Web.UI.WebControls.ListItem item in items)
        //{
        //    ddlSeasonYear.Items.Add(item);
        //}
        //ddlSeasonYear.Items.RemoveAt(0);
        //ddlSeasonYear.Items.Add(DateTime.Now.Year.ToString());
        divSeasonSubmit.Visible = true;
        btnSeasonSubmit.Visible = true;
        divseasoninfoview.Visible = true;
        lbtnSeasocClose.Visible = true;
        lbtnSeasonEdit.Visible = false;
        editmode = true;
        //BindSeason_Farmer();
        bindFarmerDetails();
    }
    protected void lbtnRiskEdit_Click(object sender, EventArgs e)
    {
        btnFieldRiskNext.Visible = true;
        divRiskinfoview.Visible = true;
        divRiskSubmit.Visible = true;
        lbtnRiskClose.Visible = true;
        lbtnRiskEdit.Visible = false;
        bindFarmerDetails();
    }
    protected void lbtnFamilyEdit_Click(object sender, EventArgs e)
    {
        divFamilyinfoview.Visible = false;
        divFamilyDetails.Visible = true;
        bindFarmerDetails();
    }
    protected void lbtnAnimalEdit_Click(object sender, EventArgs e)
    {
        divAnimalinfoview.Visible = false;
        divCattleDetails.Visible = true;
        bindFarmerDetails();
    }
    protected void lbtnOfficeEdit_Click(object sender, EventArgs e)
    {
        divOfficeinfoview.Visible = false;
        divForOfficeUse.Visible = true;
        bindFarmerDetails();
    }
    #endregion

    #region Farmer Close

    protected void lbtnFarmerClose_Click(object sender, EventArgs e)
    {
        divfarmerInfoView.Visible = true;
        divFarmerDetails.Visible = false;
        BindFarmer();
    }

    protected void lblBankClose_Click(object sender, EventArgs e)
    {
        divbankinfoView.Visible = true;
        divBankDetails.Visible = false;
        BindFarmer();
    }

    protected void lbtnPlotClose_Click(object sender, EventArgs e)
    {
        divplotinfoview.Visible = true;
        divNewFarmDetails.Visible = false;
        BindFarmer();
    }
    protected void lbtnSeasocClose_Click(object sender, EventArgs e)
    {
        divseasoninfoview.Visible = true;
        lbtnSeasonEdit.Visible = true;
        lbtnSeasocClose.Visible = false;
        divSeasonSubmit.Visible = false;
        btnSeasonSubmit.Visible = false;
        editmode = false;
        //BindSeason_Farmer();
        ddlSeasonYear.Enabled = false;
    }
    protected void lbtnRiskClose_Click(object sender, EventArgs e)
    {
        divRiskinfoview.Visible = true;
        lbtnRiskEdit.Visible = true;
        divRiskSubmit.Visible = false;
        lbtnRiskClose.Visible = false;
    }
    protected void lbtnFamilyClose_Click(object sender, EventArgs e)
    {
        divFamilyinfoview.Visible = true;
        divFamilyDetails.Visible = false;
        BindFarmer();
    }
    protected void lbtnAnimalClose_Click(object sender, EventArgs e)
    {
        divAnimalinfoview.Visible = true;
        divCattleDetails.Visible = false;
        BindFarmer();
    }
    protected void lbtnOfficeclose_Click(object sender, EventArgs e)
    {
        divOfficeinfoview.Visible = true;
        divForOfficeUse.Visible = false;
        BindFarmer();
    }
    #endregion

    public void bindFarmerDetails()
    {
        DataTable farmerdata = farmerobj.FamerDetails(lblFCode.Text); //farmerobj.FamerDetails(farmerName, farmerCode, area);
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
            ddlSeasonYear.ClearSelection();
            txtEarningMember.Text = farmer["NumberOfEarningPersons"].ToString();
            txtDependentElder.Text = farmer["ElderlyDependents"].ToString();
            txtBuffalo.Text = farmer["OtherAnimals"].ToString();
            txtCows.Text = farmer["Cow"].ToString();
            txtOx.Text = farmer["Ox"].ToString();
            txtSheep.Text = farmer["Sheep"].ToString();
            imgFarmerP.ImageUrl = farmer["PhotoPath"].ToString();
            lblTotArea.Text = farmer["TotalAreaInHectares"].ToString();
            lblTotalPlots.Text = farmer["NumberOfPlots"].ToString();
            txtAcccountHolder.Text = farmer["BankHolderName"].ToString();
            //rbOrganic.Checked = (bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false);
            //rbOrganicFairTrad.Checked = (bool)(!string.IsNullOrEmpty(farmer["OrganicFair"].ToString()) ? farmer["OrganicFair"] : false);
            if ((bool)(!string.IsNullOrEmpty(farmer["Organic"].ToString()) ? farmer["Organic"] : false == true))
            {
                lblOrg.Text = "Yes";
            }
            else
            {
                lblNonOrganic.Text = "Yes";
            }
            txtCommentInternalInspector.Text = farmer["InspectionComments"].ToString();
            txtuploadedInternalInspector.Text = farmer["InspectorName"].ToString();
            //plot info
            BindNewFarm();
            //Season Info 
            BindSeason_Farmer();
            //Field Risk
            BindFiledRisk();

        }
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

    protected void btnFarmerNext_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];

            string icsCode = farmerobj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));

            MudarApp farmercode = new MudarApp();
            string FarmerCode = farmercode.farmercode(txtCity.Text, txtState.Text);
            string testpath = hfUserPhotoPath.Value;
            if (farmerobj.FarmerExist(lblfarmerUid.Text))
            {
                //update farmer
                result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, FarmerCode, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Update, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
            }
            else
            //Insert farmer
            {
                result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, FarmerCode, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, MudarApp.Insert, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,icsCode);
                lblFarmerCode.Text = FarmerCode;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=0" + ex.Message + "");
        }
    }

    protected void btnBankNext_Click(object sender, EventArgs e)
    {
        result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, lblFarmerCode.Text, txtFarmerName.Text, string.Empty, string.Empty, 0, 0, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, txtBankName.Text, txtAccountNo.Text, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Bhanu", txtFarmerRegNo.Text, string.Empty, 4, DateTime.Now, txtAcccountHolder.Text, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
        //BindNewFarm();
    }

    protected void btnNFarmNextt_Click(object sender, EventArgs e)
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];

        string icsCode = farmerobj.GetUserICSDetails(new Guid(Convert.ToString(dtLoginDetails.Rows[0]["UserId"])));
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
        result = farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, txtFarmerName.Text, string.Empty, string.Empty, TotalArea, gvfarmdetails.Rows.Count, txtFatherName.Text, txtAddress.Text, txtCity.Text, txtTaluk.Text, txtDistrict.Text, txtState.Text, txtCountry.Text, txtPhone.Text, txtMPhone.Text, string.Empty, string.Empty, 0, 0, 0, string.Empty, 0, 0, 0, 0, false, false, false, false, string.Empty, "Raghu", string.Empty, string.Empty/*path imgFarmerP.ImageUrl string.Empty*/, 5, string.IsNullOrEmpty(txtLastDate.Text) ? DateTime.Now : Convert.ToDateTime(txtLastDate.Text), string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,icsCode);
        BindNewFarm();
        lblTotArea.Text = TotalArea.ToString();
    }

    private void BindChildDetails()
    {
        gvFarmerChildDetails.DataSource = farmerobj.GetFarmerFamilyDeatils(lblfarmerUid.Text);
        gvFarmerChildDetails.DataBind();
        lblChildCount.Text = gvFarmerChildDetails.Rows.Count.ToString();
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

    private void ClearControls()
    {
        //lblfarmerUid.Text = string.Empty;
        //txtFarmerName.Text = string.Empty;
        //txtFatherName.Text = string.Empty;
        //txtAddress.Text = string.Empty;
        //txtCity.Text = string.Empty;
        //txtDistrict.Text = string.Empty;
        //txtTaluk.Text = string.Empty;
        //txtState.Text = string.Empty;
        //txtCountry.Text = string.Empty;
        //txtPhone.Text = string.Empty;
        //txtMPhone.Text = string.Empty;
        //lblFarmerCode.Text = string.Empty;
        //txtFarmerRegNo.Text = string.Empty;
        //cbBank.Checked = false;
        //txtAccountNo.Text = string.Empty;
        //txtBankName.Text = string.Empty;
        //txtAcccountHolder.Text = string.Empty;
        //txtLastDate.Text = string.Empty;
        ////ddlSeason.ClearSelection();
        //ddlSeasonYear.ClearSelection();
        ////lblSeasonStart.Text = string.Empty;
        ////lblSeasonEnd.Text = string.Empty;
        //txtEarningMember.Text = string.Empty;
        //txtDependentElder.Text = string.Empty;
        ////ddlDepChild.ClearSelection();
        //txtBuffalo.Text = string.Empty;
        //txtCows.Text = string.Empty;
        //txtOx.Text = string.Empty;
        //txtSheep.Text = string.Empty;
        ////cbInternalInspector.Checked = false;
        ////cbPresident.Checked = false;
        ////cbProposedFieldOfficer.Checked = false;
        ////cbProposedManager.Checked = false;
        //imgFarmerP.ImageUrl = WebConfigurationManager.AppSettings["FarmerIcon"];
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
        lblTotalArea.Text = TotalArea.ToString();
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
                    decimal totArea = 0;
                    foreach (GridViewRow gvrFarmerdt in gvfarmdetails.Rows)
                    {
                        totArea = totArea + Convert.ToDecimal(!string.IsNullOrEmpty((gvrFarmerdt.FindControl("txtPSize") as TextBox).Text) ? (gvrFarmerdt.FindControl("txtPSize") as TextBox).Text : "0");
                    }
                    lblTotalPlots.Text = gvfarmdetails.Rows.Count.ToString();
                    lblTotArea.Text = totArea.ToString();
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

    protected void OkButton_Click(object sender, EventArgs e)
    {
        int age = DateTime.Now.Year - Convert.ToDateTime(txtDOB.Text).Year;
        farmerobj.FarmerFamilyDetails_INS(0, lblfarmerUid.Text, txtNameChild.Text, rbtnGender.SelectedItem.Value, Convert.ToDateTime(txtDOB.Text), age, cbSchool.Checked, cbWorking.Checked, "bhanu", string.Empty, MudarApp.Insert, Convert.ToInt32(txtEarningMember.Text), Convert.ToInt32(txtDependentElder.Text));
        lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
        BindChildDetails();
        ClearChildDetails();
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
    protected void btbCattleNext_Click(object sender, EventArgs e)
    {
        string testpath = hfUserPhotoPath.Value;
        farmerobj.Farmer_INSandUPTandDEL(lblfarmerUid.Text, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, Convert.ToInt32(txtCows.Text), Convert.ToInt32(txtOx.Text), Convert.ToInt32(txtSheep.Text), Convert.ToInt32(txtBuffalo.Text), false, false, false, false, "Raghu", string.Empty, string.Empty, testpath/*path imgFarmerP.ImageUrl string.Empty*/, 7, DateTime.Now, string.Empty, rbOrganic.Checked, rbOrganicFairTrad.Checked,string.Empty);
    }
    protected void officesubmit_Click(object sender, EventArgs e)
    {
        bool Result = false;
        Result = farmerobj.FarmerApproval(lblfarmerUid.Text, txtuploadedInternalInspector.Text, txtCommentInternalInspector.Text, "Aslam", 2, ref Result);
    }
    protected void gvFarmerChildDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_delete")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            farmerobj.FarmerFamilyDetails_INS(Convert.ToInt32(gvFamilyDet.DataKeys[index].Value.ToString()), lblfarmerUid.Text, string.Empty, string.Empty, DateTime.Now, 0, false, false, string.Empty, "bhanu", MudarApp.Delete, 0, 0);
            BindChildDetails();
        }
    }
    //seasoninfo submit code
    protected void btnSeasonSubmit_Click(object sender, EventArgs e)
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
        //        farmerobj.FarmerSeasonProduct_INSandUPTandDEL(seasonId, lblfarmerUid.Text, sName, string.Empty, cbl.Items[icount].Selected, "Bhanu", "Bhanu", Convert.ToDateTime(sdate), Convert.ToDateTime(edate), Convert.ToInt32(ddlSeasonYear.SelectedItem.Text), Convert.ToInt32(cbl.Items[icount].Value), 2);
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

        List<SeaProdModel> lstAddProds = new List<SeaProdModel>();
        List<SeaProdModel> lstDelProds = new List<SeaProdModel>();

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



        editmode = false;
        //BindSeason_Farmer();
        BindFarmer();
    }
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
        BindFarmer();
    }
    protected void gvFarmerChildDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Attributes.Add("onClick", "return confirm('Are you sure you want to delete the record?');");

        }
    }
    protected void gvFarmerChildDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(gvFamilyDet.Rows[e.RowIndex].Cells[0].Text);
        farmerobj.FarmerFamilyDetails_INS(Convert.ToInt32(gvFamilyDet.DataKeys[index].Value.ToString()), lblfarmerUid.Text, string.Empty, string.Empty, DateTime.Now, 0, false, false, string.Empty, "bhanu", MudarApp.Delete, 0, 0);
        BindChildDetails();
    }
    protected void btnFamilyNext_Click(object sender, EventArgs e)
    {
        DateTime dob = DateTime.Now;
        farmerobj.FarmerFamilyDetails_INS(0, lblfarmerUid.Text, string.Empty, string.Empty, dob, 0, false, false, "bhanu", string.Empty, MudarApp.Update, Convert.ToInt32(txtEarningMember.Text), Convert.ToInt32(txtDependentElder.Text));
        lblChildCount.Text = gvFamilyDet.Rows.Count.ToString();
        BindChildDetails();
        ClearChildDetails();
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        generatePDF();
        //GeneratePDF2();
    }
    private void GeneratePDF2()
    {
        string strpdf = "<table align='center' style='font-family:Verdana;font-size:7px;width:885px'>";
        strpdf += "<tr><td colspan='6' style='font-size:16px;' align='center'>Mudar India Exports<br />Internal Inspection Report</td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Inspection Details</td></tr>";
        strpdf += "<tr align='center'><td>Name of the Inspector</td><td >Inspector Name</td><td >Date of Inspection</td><td>12-mar-2014</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1'><tr align='center' style='font-size:9px;'><td colspan='4' bgcolor='#CCCC99'>Period Details</td></tr>";
        strpdf += "<tr align='center'><td>From</td><td >12-jan-2013</td><td >To&nbsp;&nbsp;</td><td>12-mar-2014</td></tr></table></td></tr>";
        strpdf += "<tr><td colspan='6'><table width='100%' border='1' ><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td width='60%' colspan='4'>Farmer Information</td><td width='40%' colspan='2'>Farm Details(all plots including non-organic plots)</td></tr>";
        strpdf += "<tr align='center'><td rowspan='2'>Farmer Name </td><td rowspan='2'>ghghghjhgjhghhgh<br />ghghgjhj</td><td>Farmer (mie) Code</td><td>hhhhhhhhhh</td><td>Totar Area in (Hc) </td><td>1263636</td></tr>";
        strpdf += "<tr align='center'><td>Farmer (tracenet) Ciode</td><td>hhhhhhhhhh</td><td>Total Organic Area (Hc)</td><td>123636636</td></tr>";
        strpdf += "<tr align='center'><td>Village</td><td>Hathinibhood</td><td>accompanied by</td><td>hhhhhhhhhhhh</td><td>Number of Organic Plots</td><td>16</td></tr></table></td></tr>";
        // grid view
        strpdf += "<tr><td colspan='6'>";
        strpdf += "<table width='100%'><tr>";
        strpdf += "<td width='50%'><table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='4'>Farm Details - Plot-wise</td></tr><tr bgcolor='#CCFFCC'><td align='center'>Plot (mie) <br/>Code</td><td align='center'>Area in<br/>(HC)</td><td align='center'>Main<br/>Crop</td><td align='center'>Inter<br/>Crop</td></tr>";
        foreach (GridViewRow gvr in gvFarmerChildDetails.Rows)
            strpdf += "<tr><td align='center'>" + gvr.Cells[0].Text + "</td><td align='center'>" + gvr.Cells[1].Text + "</td><td align='center'>" + gvr.Cells[2].Text + "</td><td align='center'>" + gvr.Cells[3].Text + "</td></tr>";
        strpdf += "<tr><td colspan='2' align='center' bgcolor='#FFCC99'>gg </td><td colspan='2' bgcolor='#CCCC99' align='center'>Total Farm Area in Hc</td></tr>";
        strpdf += "<tr><td colspan='2' align='left'>&nbsp;other crops / Vacant Area in Hc</td><td colspan='2' align='center' bgcolor='#FFCC99'>gg</td></tr>";
        strpdf += "</table></td>";
        strpdf += "<td width='50%'><table width='100%' border='1' font-size:6px><tr align='center' bgcolor='#CCCC99' style='font-size:9px;'><td colspan='4'>Seeds & Sowing / Planting - Information</td></tr><tr bgcolor='#CCFFCC' align='center'><td>Plantation <br/>Date</td><td>Seed <br/>Source</td><td>Bill details <br/>if purchased</td><td>Qty in KG <br/>(Hc)</td></tr>";
        foreach (GridViewRow gv in gvFarmerChildDetails.Rows)
            strpdf += "<tr><td align='center'>" + gv.Cells[0].Text + "</td><td align='center'>" + gv.Cells[1].Text + "</td><td align='center'>" + gv.Cells[2].Text + "</td><td align='center'>" + gv.Cells[3].Text + "</td></tr>";
        strpdf += "</table></td>";
        strpdf += "</tr></table>";
        strpdf += "</td></tr>";

        strpdf += "</table>";

        Document document = new Document();
        try
        {
            string Pdf_path = string.Empty;
            Pdf_path = mu.createfolder("14", MudarUser.MudarFamer) ? WebConfigurationManager.AppSettings["farmer"].ToString() + 14 + "/FarmerInternalInspectionReport_" + ".pdf" : WebConfigurationManager.AppSettings["farmer"].ToString() + "/FarmerInternalInspectionReport_" + 14 + ".pdf";
            //writer - have our own path!!!
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath(Pdf_path), FileMode.Create));
            document.Open();
            //Here is where your HTML source goes................
            String htmlText = strpdf.ToString();


            //make an arraylist ....with STRINGREADER since its no IO reading file...

            List<IElement> htmlarraylist = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(new StringReader(htmlText), null);

            ////add the collection to the document
            //for (int k = 0; k < htmlarraylist.Count; k++)
            //{
            //    document.Add((IElement)htmlarraylist[k]);
            //}

            //document.Add(new Paragraph("And the same with indentation...."));

            // or add the collection to an paragraph
            // if you add it to an existing non emtpy paragraph it will insert it from
            //the point youwrite -
            Paragraph mypara = new Paragraph();//make an emtphy paragraph as "holder"
            mypara.IndentationLeft = 20;
            mypara.InsertRange(0, htmlarraylist);
            document.Add(mypara);
            document.Close();


            //bool result = reportObj.OrderReportsPathInsertandUpdate(Convert.ToInt32(orderid), Convert.ToInt32(Session["BranchOrderID_S"].ToString()), Pdf_path, "Bhanu", string.Empty, rtypeObj.Cover_Letter);


        }
        catch (Exception exx)
        {
            Response.Write("<br>____________________________________<br>");
            Response.Write("<br>Error: " + exx + "<br>");
            Response.Write("<br>StackTrace: " + exx.StackTrace + "<br>");
            Response.Write("<br>strPDFDocument: " + strpdf.ToString() + "<br>");
            Response.Write("<br>strSelectUserListBuilder: " + strpdf.ToString() + "<br>");

            //Console.Error.WriteLine(exx.StackTrace);
            //Console.Error.WriteLine(exx.StackTrace);
        }
        finally
        {
            //document.Close();
        }
    }

    protected void gvFarmerSeasonDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i >= 2 && i % 4 == 2)
                {
                    int prodid = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
                    int seasonId = Convert.ToInt32(e.Row.Cells[i + 1].Text.Trim());
                    DateTime startDate = Convert.ToDateTime(e.Row.Cells[i + 2].Text.Trim());
                    DataTable dt = cp.SeasonDetails_FarmerSeasonProduct(new Guid(hdfarmerUid.Value), prodid, seasonId, startDate.Year);
                    CheckBox chk = new CheckBox();
                    if (dt.Rows.Count > 0)
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                    e.Row.Cells[i].Controls.Add(chk);
                    //if (dt.Rows.Count > 0)
                    //{

                    //    CheckBox chk = new CheckBox();
                    //    chk.Checked = true;
                    //    e.Row.Cells[i].Controls.Add(chk);
                    //    //if (editmode)
                    //    //{
                    //    //    CheckBox chk = new CheckBox();
                    //    //    chk.Checked = true;
                    //    //    e.Row.Cells[i].Controls.Add(chk);
                    //    //}
                    //    //else
                    //    //{
                    //    //    e.Row.Cells[i].Text = "&#10004;";
                    //    //}
                    //}
                    //else
                    //{
                    //    if (editmode)
                    //    {
                    //        CheckBox chk = new CheckBox();
                    //        chk.Checked = false;
                    //        e.Row.Cells[i].Controls.Add(chk);
                    //    }
                    //    else
                    //    {
                    //        e.Row.Cells[i].Text = "&#10006";
                    //    }
                    //}


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


