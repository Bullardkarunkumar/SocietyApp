using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.DL;

public partial class Admin_InspectionHisoy : System.Web.UI.Page
{
    CategoryProduct_BL cp = new CategoryProduct_BL();
    InspectionPlan_BL ip = new InspectionPlan_BL();
    UnitInformation_BL ui = new UnitInformation_BL();
    Farmer_BL fr = new Farmer_BL();
    BranchsRolesEmployees_BL be = new BranchsRolesEmployees_BL();
    public static string SortExpression_i = "FarmerID";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MasterControlbtnInspectionSchedules();
            HiddenHistoryID.Value = "0";
            BindYear();
            BindSeasonDetailsList();
            BindICS();
            divShowPlanDetails.Visible = false;
            btnSubmit.Visible = false;
        }
    }
    protected void ddlHolidayYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlHolidayYear.SelectedIndex > 0)
        {
            ddlSeason.DataSource = cp.GetSeasonDetails(ddlHolidayYear.SelectedValue);
            ddlSeason.DataTextField = "SeasonName";
            ddlSeason.DataValueField = "SeasonID";
            ddlSeason.DataBind();
            ddlSeason.Items.Insert(0, MudarApp.AddListItem());
        }
        else
        {
            ddlSeason.Items.Clear();
        }
    }
    private void BindICS()
    {
        ddlICS.DataSource = ip.GetICSID();
        ddlICS.DataTextField = "Branchcode";
        ddlICS.DataValueField = "Branchcode";
        ddlICS.DataBind();
        ddlICS.Items.Insert(0, MudarApp.AddListItem());
    }
    
    private void BindYear()
    {
        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlHolidayYear.Items.Add(item);
        ddlHolidayYear.DataBind();
        ddlHolidayYear.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void BindSeasonDetailsList()
    {
        ddlSeason.DataSource = cp.GetSeasonDetails(ddlHolidayYear.SelectedValue);
        ddlSeason.DataTextField = "SeasonName";
        ddlSeason.DataValueField = "SeasonID";
        ddlSeason.DataBind();
        ddlSeason.Items.Insert(0, MudarApp.AddListItem());
    }
    private string GetDaysInAYear(int year, DateTime start, DateTime end)
    {
        int days = 0;
        //for (int i = startMonth ; i <= endMonth ; i++)
        //{
        //    days += DateTime.DaysInMonth(year, i);
        //}
        TimeSpan ts = end - start;
        days = ts.Days + 1;
        return days.ToString();
    }
    private string GetSaturdaysInAYear(int year, DateTime start, DateTime end)
    {
        DayOfWeek day = DayOfWeek.Saturday;
        //DateTime start = Convert.ToDateTime("1/1/" + year);
        //DateTime end = Convert.ToDateTime("12/31/" + year);
        TimeSpan ts = end - start;
        int count = (int)Math.Floor(ts.TotalDays / 7);
        int remainder = (int)(ts.TotalDays % 7);
        int sinceLastDay = (int)(end.DayOfWeek - day);
        if (sinceLastDay < 0) sinceLastDay += 7;
        if (remainder >= sinceLastDay) count++;
        int numberOfSaturdays = count;
        return numberOfSaturdays.ToString();
    }
    public static List<DateTime> getSaturdays(int year, DateTime StartDate, DateTime EndDate)
    {
        List<DateTime> lstSaturdays = new List<DateTime>();
        int m = 0;
        int Sm = StartDate.Month;
        int Em = EndDate.Month;
        for (m = Sm; m <= Em; m++)
        {
            int intDaysThisMonth = DateTime.DaysInMonth(year, m);
            StartDate = new DateTime(year, m, 1);
            for (int i = 1; i < intDaysThisMonth+1 ; i++)
            {
                if (StartDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                {
                   lstSaturdays.Add(StartDate.AddDays(i));
                }
            }
            Sm = Sm + 1;
        }
        return lstSaturdays;
    }
    public static List<DateTime> getSundays(int year, DateTime StartDate, DateTime EndDate)
    {
        List<DateTime> lstSundays = new List<DateTime>();
        int m= 0;
        int Sm = StartDate.Month;
        int Em = EndDate.Month;
        for(m = Sm; m <= Em; m++)
        {
            int intDaysThisMonth = DateTime.DaysInMonth(year, m);
            StartDate = new DateTime(year, m, 1);
            for (int i = 1; i < intDaysThisMonth + 1; i++)
            {
                if (StartDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    lstSundays.Add(StartDate.AddDays(i));
                }
            }
            Sm = Sm + 1;
        }
        return lstSundays;
    }
    private string GetSundaysInAYear(int year, DateTime start, DateTime end)
    {
        DayOfWeek day = DayOfWeek.Sunday;
        //DateTime start = Convert.ToDateTime("1/1/" + year);
        //DateTime end = Convert.ToDateTime("12/31/" + year);
        TimeSpan ts = end - start;
        int count = (int)Math.Floor(ts.TotalDays / 7);
        int remainder = (int)(ts.TotalDays % 7);
        int sinceLastDay = (int)(end.DayOfWeek - day);
        if (sinceLastDay < 0) sinceLastDay += 7;
        if (remainder >= sinceLastDay) count++;
        int numberOfSundays = count;
        return numberOfSundays.ToString();
    }

    public static List<DateTime> GetDates(int year, int month)
    {
        return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
            .Select(m => new DateTime(year, month, m))
            .Where(m => m.DayOfWeek.ToString() != "Sunday")
            .ToList();
    }
    public static List<DateTime> GetDates(int year, DateTime start, DateTime end)
    {
        List<DateTime> dates = new List<DateTime>();
        for (DateTime i = start; i <= end; i = i.AddDays(1))
        {
            if (i.DayOfWeek != DayOfWeek.Sunday)
                dates.Add(i);
        }
        return dates;
    }

    public static int[] ShareEqual(int total, int divide)
    {
        List<int> shares = new List<int>();
        int value = Convert.ToInt32(total / divide);
        for (int i = 0; i < divide; i++)
        {
            shares.Add(value);
        }
        int mod = total % divide;
        for (int i = 0; i < mod; i++)
        {
            shares[i] += 1;
        }
        return shares.ToArray();
    }
    private void BindgvPlan(int year, decimal DaysPerFarmer, DateTime start, DateTime end, string role)
    {
        try
        {
            DataTable dtNewPlan = new DataTable();
            dtNewPlan.Columns.Add("FarmerID");
            dtNewPlan.Columns.Add("FarmerName");
            dtNewPlan.Columns.Add("FarmerCode");
            dtNewPlan.Columns.Add("FarmerArea");
            dtNewPlan.Columns.Add("InspectorID");
            dtNewPlan.Columns.Add("InspectorName");
            dtNewPlan.Columns.Add("PlanDate");
            dtNewPlan.Columns.Add("VisitedDate");
            dtNewPlan.Columns.Add("InspectionID");
            List<DateTime> dates = GetDates(year, start, end);
            DataTable dtHolidays = InspectionPlan_DL.Holidays(year, start, end);
            foreach (DataRow item in dtHolidays.Rows)
            {
                string stDate = Convert.ToString(item["HolidayDate"]);
                if (stDate.Contains("/"))
                {
                    string[] keys = stDate.Split('/');
                    DateTime hdate = new DateTime(Convert.ToInt32(keys[2]), Convert.ToInt32(keys[0]), Convert.ToInt32(keys[1]));
                    if (hdate.DayOfWeek != DayOfWeek.Sunday && dates.Contains(hdate))
                        dates.Remove(new DateTime(Convert.ToInt32(keys[2]), Convert.ToInt32(keys[0]), Convert.ToInt32(keys[1])));
                }
            }
            dates = dates.OrderBy(m => m).ToList();
            List<int> months = dates.Select(m => m.Month).Distinct().ToList();

            DataTable farmervillages = UnitInformation_DL.FarmersVillageListByIcs("ICS02");
            int c = farmervillages.Rows.Count;

            List<MudarVillage> villages = new List<MudarVillage>();
            foreach (DataRow item in farmervillages.Rows)
            {
                MudarVillage village = new MudarVillage();
                village.VillageName = Convert.ToString(item["City_Village"]);
                DataTable dtFarmer = Farmer_DL.GetFarmerlistVillagewise(village.VillageName);

                List<MudarFarmer> farmers = new List<MudarFarmer>();
                for (int j = 0; j < dtFarmer.Rows.Count; j++)
                {
                    MudarFarmer farmer = new MudarFarmer();
                    farmer.FarmerID = Convert.ToString(dtFarmer.Rows[j]["FarmerID"]);
                    farmer.FirstName = Convert.ToString(dtFarmer.Rows[j]["FirstName"]);
                    farmer.FarmerCode = Convert.ToString(dtFarmer.Rows[j]["FarmerCode"]);
                    farmer.IsVisited = false;
                    farmers.Add(farmer);
                }
                int[] visitMonths = ShareEqual(farmers.Count, months.Count);
                int mn = 0;
                int skip = 0;
                foreach (var vm in months)
                {
                    farmers.Skip(skip).Take(visitMonths[mn]).ToList().ForEach(m => m.VisitedMonth = vm);
                    skip = skip + visitMonths[mn];
                    mn = mn + 1;
                }
                village.Farmers = farmers;
                villages.Add(village);
            }
            DataTable EmployeeTable = BranchsRolesEmployees_DL.GetEmployeBasedonRoleID(role);
            List<MudarEmployee> emps = new List<MudarEmployee>();
            foreach (DataRow item in EmployeeTable.Rows)
            {
                MudarEmployee emp = new MudarEmployee();
                emp.EmployeeID = new Guid(Convert.ToString(item["EmployeeId"]));
                emp.EmployeeName = Convert.ToString(item["EmployeeFristName"]);
                emps.Add(emp);
            }
            List<MudarPlanModel> newPlans = new List<MudarPlanModel>();
            foreach (var mnth in months)
            {
                List<DateTime> monthDates = dates.Where(m => m.Month == mnth && m.Year == year).ToList();
                int mod = Convert.ToInt32((monthDates.Count) / c);
                int l = Convert.ToInt32((monthDates.Count) % c);
                villages.ForEach(m => m.loopCount = mod);
                for (int i = 0; i < l; i++)
                {
                    villages[i].loopCount += 1;
                }
                int h = 0;
                foreach (var item in villages)
                {
                    List<MudarFarmer> listFarmers = item.Farmers.Where(m => m.VisitedMonth == mnth).ToList();
                    int[] farmerVisitedPerInsp = ShareEqual(listFarmers.Count, emps.Count);
                    int f = 0;
                    foreach (var item1 in emps)
                    {
                        int[] timesVisit = ShareEqual(farmerVisitedPerInsp[f], item.loopCount);
                        int k = h;
                        foreach (var visit in timesVisit)
                        {
                            List<MudarFarmer> visitFarmers = listFarmers.Where(m => m.IsVisited == false).Take(visit).ToList();
                            foreach (var farm in visitFarmers)
                            {
                                MudarPlanModel plan = new MudarPlanModel();
                                plan.FarmerID = farm.FarmerID;
                                plan.FarmerName = farm.FirstName;
                                plan.FarmerCode = farm.FarmerCode;
                                plan.VillageName = item.VillageName;
                                plan.InspectorCode = item1.EmployeeID;
                                plan.InspectorName = item1.EmployeeName;
                                plan.PlanDate = monthDates[k];
                                plan.VisitedDate = monthDates[k];
                                var empCheck = newPlans.Where(m => m.PlanDate.ToShortDateString() == plan.PlanDate.ToShortDateString() && m.VillageName == plan.VillageName && m.InspectorName != plan.InspectorName).ToList();
                                if (empCheck.Count <= 0)
                                    newPlans.Add(plan);
                            }
                            listFarmers.Where(m => m.IsVisited == false).Take(visit).ToList().ForEach(m => m.IsVisited = true);
                            k = k + villages.Count;
                            if (k >= monthDates.Count)
                                k = 0;
                        }
                        f = f + 1;
                        h = h + 1;
                    }
                    h = h - 1;
                    item.Farmers.Where(m => m.VisitedMonth == mnth).ToList().ForEach(m => m.IsVisited = true);
                }
            }
            newPlans = newPlans.OrderBy(m => m.PlanDate).ToList();
            foreach (var item in newPlans)
            {
                DataRow newrow = dtNewPlan.NewRow();
                newrow["FarmerID"] = item.FarmerID;
                newrow["FarmerName"] = item.FarmerName;
                newrow["FarmerCode"] = item.FarmerCode;
                newrow["FarmerArea"] = item.VillageName;
                newrow["InspectorID"] = item.InspectorCode;
                newrow["InspectorName"] = item.InspectorName;
                newrow["PlanDate"] = item.PlanDate.ToShortDateString();
                newrow["VisitedDate"] = item.VisitedDate.ToShortDateString();
                dtNewPlan.Rows.Add(newrow);
            }
            dt = dtNewPlan;
            if (dt.Rows.Count > 0)
            {
                Session["Inspec"] = string.Empty;
                Session["Inspec"] = dt;
                lblFarmers.Text = dt.Rows.Count.ToString();
                divShowPlanDetails.Visible = true;
                btnSubmit.Visible = true;
                gvPlan.DataSource = dt;
                gvPlan.DataBind();
            }
            else
                Response.Write("<script>alert('No Data Found for InspectionPlan !!!!'</script>");
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx", false);
        }
    }
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void Submit_Click1(object sender, EventArgs e)
    //{
    //    bool result = false;
    //    //for (int i = 0; i < gvPlan.Rows.Count; i++)
    //    //{
    //    //    string FarmerID = gvPlan.Rows[i].Cells[0].Text;
    //    //    string FarmerName=gvPlan.Rows[i].Cells[1].Text;
    //    //    string FarmerCode=gvPlan.Rows[i].Cells[2].Text;
    //    //    string FarmerArea=gvPlan.Rows[i].Cells[3].Text; 
    //    //    string InspectorID=gvPlan.Rows[i].Cells[4].Text;
    //    //    string InspectorName=gvPlan.Rows[i].Cells[5].Text;
    //    //    DateTime  PlanDae=Convert.ToDateTime(gvPlan.Rows[i].Cells[6].Text);
    //    //    DateTime  VisitedDate=Convert.ToDateTime(gvPlan.Rows[i].Cells[7].Text);
    //    //    int SeasonID = ddlSeason.SelectedIndex;
    //    //    int InspectionHistoryID = 0;
    //    //    string PlanName = txtPlanName.Text;
    //    //    int Year = Convert.ToInt32(ddlHolidayYear.SelectedItem.Value);
    //    //    result = ip.InspectionPlan_INS(FarmerID, FarmerName, FarmerCode, FarmerArea, InspectorID, InspectorName, PlanDae, VisitedDate, Year, PlanName, SeasonID, InspectionHistoryID, "", 1);
    //    //}

    //}
    private void BindgvPlan(int HistoryId)
    {
        DataTable dt = ip.GetInspectionPlan(HistoryId);
        //Session["Inspec"] = string.Empty;
        //Session["Inspec"] = dt;
        lblFarmers.Text = dt.Rows.Count.ToString();
        gvPlan.DataSource = dt; // (DataTable)Session["Inspec"];
        gvPlan.DataBind();
        SortingFarmerCode(SortExpression_i);
        HistoryId = 0;
        //BindgvPlan(Convert.ToInt32(HiddenHistoryID.Value));
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        bool result = false;
        int Newhistoryid = 0;
        result = ip.InspectionHistory_INSandUPDandDEL(0, ddlHolidayYear.SelectedIndex > 0 ? Convert.ToInt32((ddlHolidayYear.SelectedItem.Text)) : 0, ddlICS.SelectedItem.Text, ddlSeason.SelectedIndex > 0 ? Convert.ToInt32((ddlSeason.SelectedValue)) : 0, "Shaik Aslam", "Shaik Aslam", ref Newhistoryid);
        if (result)
            foreach (GridViewRow gvr in gvPlan.Rows)
            {
                DataKey dkey = gvPlan.DataKeys[gvr.RowIndex];
                string farmerid = dkey[0].ToString();
                string InspectorID = dkey[1].ToString();
                string InspectionID = dkey[2].ToString();
                //result = ip.InspectionPlan_INS(Convert.ToInt32(gvr.Cells[8].Text), Newhistoryid, gvr.Cells[4].Text, gvr.Cells[0].Text, Convert.ToDateTime(gvr.Cells[6].Text), Convert.ToDateTime(gvr.Cells[7].Text), "shaik Aslam", "shaik Aslam");
                result = ip.InspectionPlan_INS(0, Newhistoryid, InspectorID, farmerid, Convert.ToDateTime(gvr.Cells[6].Text), Convert.ToDateTime(gvr.Cells[7].Text), "shaik Aslam", "shaik Aslam");
            }

        if (result)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Saved Data Succefully !!!');</script>");
            btnDisable.Visible = true;
            btnSubmit.Visible = false;
            btnBack.Visible = true;
            HiddenHistoryID.Value = Newhistoryid.ToString();
            BindgvPlan(Newhistoryid);
        }
    }
    protected void lbViewList_Click(object sender, EventArgs e)
    {

    }
    protected void btnPlanOk_Click(object sender, EventArgs e)
    {
        divShowPlanDetails.Visible = true;
        divPlanDetails.Visible = false;
        btnSubmit.Visible = true;
        string yer = " ";
        if (ddlHolidayYear.SelectedIndex > 0)
        {
            yer = ddlHolidayYear.SelectedItem.Text;
        }
        if (ddlSeason.SelectedIndex > 0)
        {
            DataRow[] dr = cp.GetSeasonDetails().Select(" SeasonID = '" + ddlSeason.SelectedValue.ToString() + "'");
            DataRow dr1 = dr[0];
            lblSeas.Text = ddlSeason.SelectedItem.Text;
            lblICStype.Text = ddlICS.SelectedItem.Text;
            DateTime Sdate = Convert.ToDateTime(dr[0]["StartDate"].ToString());
            lblStartDate.Text = string.Format("{0:dd MMM yyyy}", Sdate);
            DateTime Edate = Convert.ToDateTime(dr[0]["EndDate"].ToString());
            lblEndDate.Text = string.Format("{0:dd MMM yyyy}", Edate);
            DataTable dtInspectionPlan = ip.GetInspectionPlanHistory(ddlHolidayYear.SelectedIndex > 0 ? Convert.ToInt32((ddlHolidayYear.SelectedItem.Text)) : 0, ddlSeason.SelectedIndex > 0 ? Convert.ToInt32((ddlSeason.SelectedValue)) : 0,lblICStype.Text);
            if (dtInspectionPlan.Rows.Count > 0)
            {
                Session["InspectionPlan"] = dtInspectionPlan;
                txtPlanName.Text = dtInspectionPlan.Rows[0]["PlanName"].ToString();
                HiddenHistoryID.Value = dtInspectionPlan.Rows[0]["InspectioHistoryID"].ToString();
                btnDisable.Visible = false;
                btnSubmit.Visible = false;
                btnBack.Visible = true;
                DataTable dt = ip.GetInspectionPlan(Convert.ToInt32(HiddenHistoryID.Value));
                DateTime StartDate = Convert.ToDateTime(lblStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(lblEndDate.Text);
                lblDays.Text = GetDaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
                lblSundays.Text = GetSundaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
                string Saturdays = GetSaturdaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
                lblHoliday.Text = ip.HolidaysCount(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate).ToString();
                lblPlanDays.Text = (Convert.ToUInt32(lblDays.Text) - Convert.ToUInt32(lblHoliday.Text) - Convert.ToInt32(Saturdays) - Convert.ToUInt32(lblSundays.Text)).ToString();
                DataTable dtICSID = ip.GetICSID(ddlICS.SelectedValue);
                DataTable dtInspectorList = ip.GetEmployeeWithRolesICsid(WebConfigurationManager.AppSettings["BranchRole"].ToString(), dtICSID.Rows[0]["BranchId"].ToString());
                lblFarmers.Text = dt.Rows.Count.ToString();
                lblEmployees.Text = dtInspectorList.Rows.Count.ToString();
                gvPlan.DataSource = dt;
                gvPlan.DataBind();
               
            }
            else
            {
                HiddenHistoryID.Value = "0";
                txtPlanName.Text = string.Empty;
                DataAssign(yer);
            }
        }
    }
    protected void gvPlan_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_i = e.SortExpression.ToString();
        SortingFarmerCode(SortExpression_i);
    }
    public string dir
    {
        get
        {
            if (ViewState["dirState"].ToString() == "desc")
            {
                ViewState["dirState"] = "asc";
            }
            else
            {
                ViewState["dirState"] = "desc";
            }
            return ViewState["dirState"].ToString();
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingFarmerCode(string SortExpression)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = ip.GetInspectionPlan(Convert.ToInt32(HiddenHistoryID.Value));
            if (dt.Rows.Count > 0)
            {
                DataView sortedView = new DataView(dt);
                sortedView.Sort = SortExpression + " " + ViewState["dirState"];
                gvPlan.DataSource = sortedView;
                gvPlan.DataBind();
            }
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx", false);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divShowPlanDetails.Visible = false;
        divPlanDetails.Visible = true;
    }
    public DataTable GetAllHolidaysList()
    {
        DataTable dtAllHolidays = new DataTable();
        dtAllHolidays.Columns.Add("Date");
        DataTable dtHolidays = InspectionPlan_DL.Holidays(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), Convert.ToDateTime(lblStartDate.Text), Convert.ToDateTime(lblEndDate.Text));
        for (int h = 0; h < dtHolidays.Rows.Count; h++)
        {
            DataRow drV = dtAllHolidays.NewRow();
            drV["Date"] = dtHolidays.Rows[h]["HolidayDate"].ToString();
            dtAllHolidays.Rows.Add(drV);
        }
        List<DateTime> lstSaturdays = getSaturdays(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), Convert.ToDateTime(lblStartDate.Text), Convert.ToDateTime(lblEndDate.Text));
        foreach (DateTime date in lstSaturdays)
        {
            DataRow drV = dtAllHolidays.NewRow();
            drV["Date"] = date.ToShortDateString().ToString();
            dtAllHolidays.Rows.Add(drV);
        }
        List<DateTime> lstsundays = getSundays(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), Convert.ToDateTime(lblStartDate.Text), Convert.ToDateTime(lblEndDate.Text));
        foreach (DateTime date in lstsundays)
        {
            DataRow drV = dtAllHolidays.NewRow();
            drV["Date"] = date.ToShortDateString().ToString();
            dtAllHolidays.Rows.Add(drV);
        }
        return dtAllHolidays;
    }
    
    /* new code */
    private void DataAssign(string yer)
    {
        btnDisable.Visible = false;
        DateTime StartDate = Convert.ToDateTime(lblStartDate.Text);
        DateTime EndDate = Convert.ToDateTime(lblEndDate.Text);
        lblDays.Text = GetDaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
        lblSundays.Text = GetSundaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
        string Saturdays = GetSaturdaysInAYear(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate);
        lblHoliday.Text = ip.HolidaysCount(Convert.ToInt32(ddlHolidayYear.SelectedItem.Text), StartDate, EndDate).ToString();
        lblPlanDays.Text = (Convert.ToUInt32(lblDays.Text) - Convert.ToUInt32(lblHoliday.Text) - Convert.ToInt32(Saturdays) - Convert.ToUInt32(lblSundays.Text)).ToString();
        DataTable dtNewPlan = new DataTable();
        dtNewPlan.Columns.Add("FarmerID");
        dtNewPlan.Columns.Add("FarmerName");
        dtNewPlan.Columns.Add("FarmerCode");
        dtNewPlan.Columns.Add("FarmerArea");
        //dtNewPlan.Columns.Add("InspectorID");
        //dtNewPlan.Columns.Add("InspectorName");
        //dtNewPlan.Columns.Add("PlanDate");
        //dtNewPlan.Columns.Add("VisitedDate");
        //dtNewPlan.Columns.Add("InspectionID");
        DataTable dtfarmer = fr.FamerDetailsByIcs(ddlICS.SelectedValue);
        DataTable dtICSID = ip.GetICSID(ddlICS.SelectedValue);
        DataTable dtInspectorList = ip.GetEmployeeWithRolesICsid(WebConfigurationManager.AppSettings["BranchRole"].ToString(), dtICSID.Rows[0]["BranchId"].ToString());
        lblEmployees.Text = dtInspectorList.Rows.Count.ToString();
        DataTable dtvillages = ui.FarmersVillageListByIcs(ddlICS.SelectedValue);
        //DataTable dtVillageCheck = new DataTable();
        //dtVillageCheck.Columns.Add("Vill");
        //dtVillageCheck.Columns.Add("count");
        //for (int v = 0; v < dtvillages.Rows.Count; v++)
        //{
        //    DataRow drv = dtVillageCheck.NewRow();
        //    drv["vill"] = dtvillages.Rows[v]["city_village"].ToString();
        //    drv["count"] = 0;
        //    dtVillageCheck.Rows.Add(drv);
        //}
        lblvillage.Text = dtvillages.Rows.Count.ToString();
        DataTable dtHolidays = GetAllHolidaysList();
        int frmcount = dtfarmer.Rows.Count;
        int inspcount = dtInspectorList.Rows.Count;
        decimal frr = dtfarmer.Rows.Count / dtInspectorList.Rows.Count;
        //decimal err = dtInspectorList.Rows.Count / dtVillageCheck.Rows.Count;
        //int lc = Convert.ToInt32(Math.Round(err,2));
        int shdays = Convert.ToInt32(Math.Round(frr,2));
        int f,set,check =  0;
        DateTime newdtm = StartDate;
        int Farmerscount = dtfarmer.Rows.Count;
        DataTable dtVillageCheck = new DataTable();
        dtVillageCheck.Columns.Add("Vill");
        dtVillageCheck.Columns.Add("count");
        string village = string.Empty;
        string cou = string.Empty;
        DataTable dtvill = fr.GetFarmerVillageDistinct(ddlICS.SelectedItem.Text);
        int d = 0;
        while (d < Farmerscount)
        {
            for (int ins = 0; ins < dtInspectorList.Rows.Count; ins++)
            {
                DataRow dr = dtNewPlan.NewRow();
                for (int v = check; v < dtvill.Rows.Count; v++)
                {
                    DataTable dt = fr.FarmerDetails(dtvill.Rows[v]["City_Village"].ToString());
                    dr["FarmerID"] = dt.Rows[0]["FarmerId"].ToString();
                    dr["FarmerCode"] = dt.Rows[0]["FarmerCode"].ToString();
                    dr["FarmerName"] = dt.Rows[0]["FirstName"].ToString();
                    dr["FarmerArea"] = dt.Rows[0]["City_Village"].ToString();
                    dt.Rows[v].Delete();
                    dt.AcceptChanges();
                    check = check + 1;
                    break;
                }
                if (check == 5)
                {
                    check = 0;
                }
                if (ins == 8)
                {
                    ins = 0;
                    break;
                }
                dtNewPlan.Rows.Add(dr);
            }
            d = d + 1;
        }
        gvPlan.DataSource = dtNewPlan;
        gvPlan.DataBind();
        lblFarmers.Text = dtNewPlan.Rows.Count.ToString();
    }
}
                    