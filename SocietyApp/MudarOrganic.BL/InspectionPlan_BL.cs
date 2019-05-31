using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;
using System.Configuration;

namespace MudarOrganic.BL
{
    public class InspectionPlan_BL
    {
        //public bool InspectionPlan_INS(string FarmerId, string FarmerName, string FarmerCode, string FarmerArea, string InspectorID, string InspectorName, DateTime PlanDate, DateTime VisitedDate, int Year, string PlanName, int SeasonID, int InspectionHistoryID, string CreatedBy, int TypeOfOperation)
        //{
        //    return InspectionPlan_DL.InspectionPlan_INS(FarmerId, FarmerName, FarmerCode, FarmerArea, InspectorID, InspectorName, PlanDate, VisitedDate, Year, PlanName, SeasonID, InspectionHistoryID, CreatedBy, TypeOfOperation);
        //} 
        public int HolidaysCount(int year, DateTime start, DateTime end)
        {
            int count = 0;
            DataTable temp = InspectionPlan_DL.Holidays(year, start, end);
            if (temp.Rows.Count > 0)
                count = temp.Rows.Count;
            return count;
        }
        public DataTable Holidays(int year)
        {
            return InspectionPlan_DL.Holidays(year);
        }
        public DataTable Holidays(int year, DateTime start, DateTime end)
        {
            return InspectionPlan_DL.Holidays(year, start, end);
        }
        public bool HolidayExist(DateTime HolidayDate)
        {
            return InspectionPlan_DL.HolidayExist(HolidayDate);
        }
        public DataTable FarmerCount()
        {
            return InspectionPlan_DL.FarmerCount();
        }
        public DataTable EmployeesCount()
        {
            return InspectionPlan_DL.EmployeesCount();
        }
        public int GetEmployeeWithRoles(string role,string ICSID)
        {
            DataRow[] drEmployees = (BranchsRolesEmployees_DL.GetEmployesListOnICS(ICSID)).Select("Roleid ='" + role + "'");
            return drEmployees.Length;
        }
        public DataTable Newplan(int year, decimal DaysPerFarmer, DateTime startDate, DateTime endDate, string role)
        {
            int count = 0;
            DataTable dtNewPlan = new DataTable();
            DataTable dtHolidays = Holidays(year, startDate, endDate);
            DateTime planDate;
            DateTime planDate1 = startDate;
            DataTable dtdate = new DataTable();
            dtdate.Columns.Add("Village");
            dtdate.Columns.Add("PlanDate");
            dtdate.Columns.Add("InspectorID");
            dtdate.Columns.Add("InspectorName");
            DataTable farmervillages = UnitInformation_DL.FarmersVillageList();
            Random randomdate = new Random();
            for (int v = 0; v < farmervillages.Rows.Count; v++)
            {
                for (int d = 0; d < 5; d++)
                {
                    DataRow newrow = dtdate.NewRow();
                    newrow["Village"] = farmervillages.Rows[v]["City_Village"].ToString();
                    if (planDate1 >= startDate && planDate1 <= endDate)
                    {
                        int range = ((TimeSpan)(Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate))).Days;
                        planDate = Convert.ToDateTime(startDate).AddDays(randomdate.Next(range));
                        DataTable EmployeeTable = BranchsRolesEmployees_DL.GetEmployeBasedonRoleID(role);
                        if (EmployeeTable.Rows.Count > 0)
                        {
                            int getunitID = new Random().Next(0, EmployeeTable.Rows.Count);
                            if (count == 0)
                            {
                                newrow["InspectorID"] = EmployeeTable.Rows[getunitID]["EmployeeId"].ToString();
                                newrow["InspectorName"] = EmployeeTable.Rows[getunitID]["EmployeeFristName"].ToString();
                                count++;
                            }
                            else
                            {
                                newrow["InspectorID"] = EmployeeTable.Rows[count]["EmployeeId"].ToString();
                                newrow["InspectorName"] = EmployeeTable.Rows[count]["EmployeeFristName"].ToString();
                            }
                        }
                        if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
                        {
                            planDate = planDate.AddDays(1);
                        }
                        if (planDate.DayOfWeek.ToString().ToLower() != "sunday")
                        {
                            for (int hcount = 0; hcount < dtHolidays.Rows.Count; hcount++)
                            {
                                if (DateTime.Compare(planDate, Convert.ToDateTime(dtHolidays.Rows[hcount]["HolidayDate"].ToString().Trim())) == 0)
                                {
                                    planDate = planDate.AddDays(1);
                                    hcount = -1;
                                }
                            }
                            if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
                            {
                                planDate = planDate.AddDays(1);
                            }
                            newrow["PlanDate"] = planDate.ToShortDateString();
                        }
                    }
                    dtdate.Rows.Add(newrow);
                }
            }
            return dtdate;
        }
        // new code testing purpose
        public DataTable Newoneplan(int year, decimal DaysPerFarmer, DateTime startDate, DateTime endDate, string role)
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
            List<DateTime> dates = GetDates(year, startDate, endDate);
            DataTable dtHolidays = InspectionPlan_DL.Holidays(year, startDate, endDate);
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
            DataTable farmervillages = UnitInformation_DL.FarmersVillageList();
            int c = farmervillages.Rows.Count;
            int mod = Convert.ToInt32(dates.Count / c);
            int l = Convert.ToInt32(dates.Count % c);
            List<MudarVillage> villages = new List<MudarVillage>();
            foreach (DataRow item in farmervillages.Rows)
            {
                MudarVillage village = new MudarVillage();
                village.VillageName = Convert.ToString(item["City_Village"]);
                village.loopCount = mod;
                villages.Add(village);
            }
            for (int i = 0; i < l; i++)
            {
                villages[i].loopCount += 1;
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
            bool first = false;
            int h = 0;
            int village2 = 0;
            Random random = new Random();
            for (int wd = 0; wd < dates.Count; wd++)
            {
                for (int vil = h; vil < 2; vil++)
                {
                    string village = string.Empty;
                    int getID = random.Next(0, farmervillages.Rows.Count);
                    village = farmervillages.Rows[getID]["City_Village"].ToString();
                    DataTable dtFarmer = Farmer_DL.GetFarmerlistVillagewise(village);
                    for (int r = 0; r < dtNewPlan.Rows.Count; r++)
                    {
                        string farmersid= string.Empty;
                        DataTable newdt = new DataTable();
                        farmersid = dtNewPlan.Rows[r]["FarmerID"].ToString();
                        for (int fv = 0; fv < dtFarmer.Rows.Count; fv++)
                        {
                            if (dtNewPlan.Rows[r]["FarmerID"].ToString() == dtFarmer.Rows[fv]["FarmerID"].ToString())
                            {

                            }
                        }
                    }
                    int p = (Convert.ToInt32(dtFarmer.Rows.Count.ToString()) / mod);
                    for (int f = 0; f < p; f++)
                    {
                        DataRow newrow = dtNewPlan.NewRow();
                        newrow["InspectionID"] = "0";
                        newrow["FarmerID"] = dtFarmer.Rows[f]["FarmerID"];
                        newrow["FarmerName"] = dtFarmer.Rows[f]["FirstName"];
                        newrow["FarmerCode"] = dtFarmer.Rows[f]["FarmerCode"];
                        newrow["FarmerArea"] = dtFarmer.Rows[f]["City_Village"];
                        if (first == false)
                        {
                            newrow["InspectorID"] = emps[0].EmployeeID;
                            newrow["InspectorName"] = emps[0].EmployeeName;
                            //first = true;
                        }
                        else
                        {
                            newrow["InspectorID"] = emps[1].EmployeeID;
                            newrow["InspectorName"] = emps[1].EmployeeName;
                            //first = false;
                        }
                        newrow["PlanDate"] = dates[wd].ToShortDateString();
                        newrow["VisitedDate"] = dates[wd].ToShortDateString();
                        dtNewPlan.Rows.Add(newrow);
                    }
                    
                    first = true;
                    village2 = village2 + 1;
                    h++;
                    if (village2 == 2)
                    {
                        //wd = wd + 1;
                        village2 = 0;
                        first = false;
                        h = 0;
                        break;
                    }
                }
            }
            return dtNewPlan;
        }
        public class MudarEmployee
        {
            public string EmployeeName { get; set; }
            public Guid EmployeeID { get; set; }
        }
        public class MudarVillage
        {
            public string VillageName { get; set; }
            public int VillageID { get; set; }
            public int loopCount { get; set; }
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
        public DataTable NewPlanDetails(int year, decimal DaysPerFarmer, DateTime startDate, DateTime endDate, string role)
        {
            DataTable FarmerTable = Farmer_DL.FamerDetails();
            DataRow[] drEmployees = (BranchsRolesEmployees_DL.GetEmployeeWithRoles()).Select("Roleid ='" + role + "'");
            DataTable dtNewPlan = new DataTable();
            DataTable dtHolidays = Holidays(year, startDate, endDate);
            dtNewPlan.Columns.Add("FarmerID");
            dtNewPlan.Columns.Add("FarmerName");
            dtNewPlan.Columns.Add("FarmerCode");
            dtNewPlan.Columns.Add("FarmerArea");
            dtNewPlan.Columns.Add("InspectorID");
            dtNewPlan.Columns.Add("InspectorName");
            dtNewPlan.Columns.Add("PlanDate");
            dtNewPlan.Columns.Add("VisitedDate");
            dtNewPlan.Columns.Add("InspectionID");

            int employeecount = 0;
            DateTime planDate = startDate;
            foreach (DataRow drFarmer in FarmerTable.Rows)
            {
                DataRow newrow = dtNewPlan.NewRow();

                newrow["InspectionID"] = "0";
                newrow["FarmerID"] = drFarmer["FarmerID"];
                newrow["FarmerName"] = drFarmer["FirstName"];
                newrow["FarmerCode"] = drFarmer["FarmerCode"];
                newrow["FarmerArea"] = drFarmer["City_Village"];
                if (employeecount < drEmployees.Length)
                {
                    DataRow dremp = drEmployees[employeecount];
                    newrow["InspectorID"] = dremp["EmployeeID"];
                    newrow["InspectorName"] = dremp["EmployeeFristName"];
                    employeecount += 1;
                    if (employeecount == drEmployees.Length)
                        employeecount = 0;
                }
                if (planDate >= startDate && planDate <= endDate)
                {
                    if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
                    {
                        planDate = planDate.AddDays(1);
                    }
                    if (planDate.DayOfWeek.ToString().ToLower() != "sunday")
                    {
                        for (int hcount = 0; hcount < dtHolidays.Rows.Count; hcount++)
                        {
                            if (DateTime.Compare(planDate, Convert.ToDateTime(dtHolidays.Rows[hcount]["HolidayDate"].ToString().Trim())) == 0)
                            {
                                planDate = planDate.AddDays(1);
                                hcount = -1;
                            }
                        }
                        if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
                        {
                            planDate = planDate.AddDays(1);
                        }
                        newrow["PlanDate"] = planDate.ToShortDateString();
                        newrow["VisitedDate"] = planDate.ToShortDateString();
                        //newrow["PlanDate"] = string.Format("{0:dd MMM yyyy}",planDate.ToShortDateString());
                        //newrow["VisitedDate"] = string.Format("{0:dd MMM yyyy}",planDate.ToShortDateString());
                        planDate = planDate.AddDays(1);
                        if (planDate > endDate)
                        {
                            planDate = startDate;
                        }
                    }

                }
                dtNewPlan.Rows.Add(newrow);
            }
            planDate = planDate.AddDays(1);
            if (planDate > endDate)
            {
                planDate = startDate;
            }
            //old code
            //DataTable FarmerTable = Farmer_DL.FamerDetails();
            //DataTable dtHolidays = Holidays(year, startDate, endDate);
            //DateTime planDate = startDate;
            //foreach (DataRow drFarmer in FarmerTable.Rows)
            //{
            //    DataRow newrow = dtNewPlan.NewRow();
            //    newrow["InspectionID"] = "0";
            //    newrow["FarmerID"] = drFarmer["FarmerID"];
            //    newrow["FarmerName"] = drFarmer["FirstName"];
            //    newrow["FarmerCode"] = drFarmer["FarmerCode"];
            //    newrow["FarmerArea"] = drFarmer["City_Village"];
            //    DataTable farmervillages = UnitInformation_DL.FarmersVillageList();
            //    string village = string.Empty;
            //    for (int i = 0; i < farmervillages.Rows.Count; i++)
            //    {
            //        Random random = new Random();
            //        int getID = random.Next(0, farmervillages.Rows.Count);
            //        village = farmervillages.Rows[getID]["City_Village"].ToString();
            //        DataTable EmployeeTable = BranchsRolesEmployees_DL.GetEmployeBasedonRoleID(role);
            //        string empID = string.Empty;
            //        string empname = string.Empty;
            //        if (EmployeeTable.Rows.Count > 0)
            //        {
            //            Random random1 = new Random();
            //            int getunitID = random1.Next(0, EmployeeTable.Rows.Count);
            //            if (count == 0)
            //            {
            //                newrow["InspectorID"] = EmployeeTable.Rows[getunitID]["EmployeeId"].ToString();
            //                newrow["InspectorName"] = EmployeeTable.Rows[getunitID]["EmployeeFristName"].ToString();
            //                count++;
            //            }
            //            else
            //            {
            //                newrow["InspectorID"] = EmployeeTable.Rows[getunitID]["EmployeeId"].ToString();
            //                newrow["InspectorName"] = EmployeeTable.Rows[getunitID]["EmployeeFristName"].ToString();
            //                count = getID;
            //            }
            //        }
            //        if (planDate >= startDate && planDate <= endDate)
            //        {
            //            if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
            //            {
            //               planDate = planDate.AddDays(1);
            //            }
            //            if (planDate.DayOfWeek.ToString().ToLower() != "sunday")
            //            {
            //                for (int hcount = 0; hcount < dtHolidays.Rows.Count; hcount++)
            //                {
            //                    if (DateTime.Compare(planDate, Convert.ToDateTime(dtHolidays.Rows[hcount]["HolidayDate"].ToString().Trim())) == 0)
            //                    {
            //                        planDate = planDate.AddDays(1);
            //                        hcount = -1;
            //                    }
            //                }
            //                if (planDate.DayOfWeek.ToString().ToLower() == "sunday")
            //                {
            //                    planDate = planDate.AddDays(1);
            //                }
            //                newrow["PlanDate"] = planDate.ToShortDateString();
            //                newrow["VisitedDate"] = planDate.ToShortDateString();
            //                planDate = planDate.AddDays(1);
            //                if (planDate > endDate)
            //                {
            //                    planDate = startDate;
            //                }
            //            }
            //        }
            //    }
            //    dtNewPlan.Rows.Add(newrow);
            //}
            return dtNewPlan;
        }
        public bool InspectionHistory_INSandUPDandDEL(int InspectioHistoryID, int Year, string PlanName, int SeasonID, string CreatedBY, string ModifiedBy, ref  int NewHistoryId)
        {
            return InspectionPlan_DL.InspectionHistory_INSandUPDandDEL(InspectioHistoryID, Year, PlanName, SeasonID, CreatedBY, ModifiedBy, ref NewHistoryId);
        }
        public bool InspectionPlan_INS(int InspectionID, int InspectionHistoryID, string EmployeeID, string FarmerID, DateTime PlanDate, DateTime VisitDate, string CreatedBy, string ModifiedBy)
        {
            return InspectionPlan_DL.InspectionPlan_INS(InspectionID, InspectionHistoryID, EmployeeID, FarmerID, PlanDate, VisitDate, CreatedBy, ModifiedBy);
        }
        public DataTable GetInspectionPlan(int HistoryID)
        {
            return InspectionPlan_DL.GetInspectionPlan(HistoryID);
        }
        public DataTable GetInspectionPlan(string EmployeeID)
        {
            return InspectionPlan_DL.GetInspectionPlan(EmployeeID);
        }
        public DataTable GetInspectionPlan(string EmployeeID, string From, string To)
        {
            return InspectionPlan_DL.GetInspectionPlan(EmployeeID, From, To);
        }
        public DataTable GetInspectionPlan(string EmployeeID, string From, string To, string ICStype)
        {
            return InspectionPlan_DL.GetInspectionPlan(EmployeeID, From, To, ICStype);
        }
        public DataTable GetInspectionPlanHistory(int Year, int SeasonID, string ICStype)
        {
            return InspectionPlan_DL.GetInspectionPlanHistory(Year, SeasonID,ICStype);
        }
        public DataTable GetInspectionByFarmerID(string FarmerID)
        {
            return InspectionPlan_DL.GetInspectionByFarmerID(FarmerID);
        }
        public DataTable GetInspectionByFarmerID(string FarmerID, string Year)
        {
            return InspectionPlan_DL.GetInspectionByFarmerID(FarmerID, Year);
        }
        public DataTable GetInspectionBasedonFarmerName(string Farmername)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return InspectionPlan_DL.GetInspectionBasedonFarmerName(Farmername);
        }
        public bool HolidayList_INSandUPDandDEL(int HolidayID, int year, DateTime date, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return InspectionPlan_DL.HolidayList_INSandUPDandDEL(HolidayID, year, date, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable GetHolidaysList()
        {
            return InspectionPlan_DL.GetHolidaysList();
        }
        public DataTable GetHolidaysListByYear(int year)
        {
            return InspectionPlan_DL.GetHolidaysListByYear(year);
        }
        public DataTable GetHolidaysListBasedonHolidayID(int HolidayID)
        {
            return InspectionPlan_DL.GetHolidaysListBasedonHolidayID(HolidayID);
        }
        public  DataTable GetPlantingInfo(string farmerID)
        {
            return InspectionPlan_DL.GetPlantingInfo(farmerID);
        }
        public  DataTable GetPlantationDate(string farmerID)
        {
            return InspectionPlan_DL.GetPlantationDate(farmerID);
        }
        public  DataTable GetInspectiononFarmerID(string FarmerID)
        {
            return InspectionPlan_DL.GetInspectiononFarmerID(FarmerID);
        }
        public DataTable GetPlantaionDetails(string FarmerID)
        {
            return InspectionPlan_DL.GetPlantaionDetails(FarmerID);
        }
        public DataTable GetPlotandPlantinfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetPlotandPlantinfo(farmerID, Year);
        }
        public DataTable GetPlotandInputinfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetPlotandInputinfo(farmerID,Year);
        }
        public DataTable GetplotandDiesinfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetplotandDiesinfo(farmerID, Year);
        }
        public DataTable GetplotandInsectinfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetplotandInsectinfo(farmerID, Year);
        }
        public DataTable GetYiledDetails(string farmerID, int Year, string ProductID)
        {
            return InspectionPlan_DL.GetYiledDetails(farmerID, Year, ProductID);
        }
        public DataTable GetYiledDetails(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetYiledDetails(farmerID, Year);
        }
        public DataTable GetplotandPestinfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetplotandPestinfo(farmerID, Year);
        }
        public DataTable GetplotandWeedInfo(string farmerID, int Year)
        {
            return InspectionPlan_DL.GetplotandWeedInfo(farmerID, Year);
        }
        public bool InspectionSubmitDetails(int InspectionID, string ModifiedBy, string Report_Path, int Result)
        {
            return InspectionPlan_DL.InspectionSubmitDetails(InspectionID, ModifiedBy, Report_Path, Result);
        }
        public DataTable GetInspaectionSubmitDetails(string FarmerID)
        {
           return InspectionPlan_DL.GetInspaectionSubmitDetails(FarmerID);
        }
        public DataSet GetGeneralInspectionDetails()
        {
            return InspectionPlan_DL.GetGeneralInspectionDetails();
        }
        public DataTable GetReportPathDetails(string InspectionID)
        {
            return InspectionPlan_DL.GetReportPathDetails(InspectionID);
        }
        public DataTable GetICSID()
        {
            return InspectionPlan_DL.GetICSID();
        }
        public DataTable GetICSID(string ICSCode)
        {
            return InspectionPlan_DL.GetICSID(ICSCode);
        }
        public DataTable GetEmployeeWithRolesICsid(string RoleName, string ICSid)
        {
            return BranchsRolesEmployees_DL.GetEmployeeWithRoles(RoleName, ICSid);
        }
    }
}
