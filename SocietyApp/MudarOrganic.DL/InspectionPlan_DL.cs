using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.Components;
using System.Data.SqlClient;

namespace MudarOrganic.DL
{
    public static class InspectionPlan_DL
    {
        //public static DataTable HolidaysCount(int year)
        //{
        //    MudarDBHelper mdbh = MudarDBHelper.Instance;
        //    return mdbh.ExecuteDataTable("select count(*) from tblHolidayList where HolidayYear=" + year);

        //}
        public static DataTable Holidays(int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select HolidayID, CONVERT(varchar(10), HolidayDate,101) AS 'HolidayDate', HolidayYear from tblHolidayList where HolidayYear=year and [delete]=0");
        }
        public static DataTable Holidays(int year, DateTime start, DateTime end)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select HolidayID, CONVERT(varchar(10), HolidayDate,101) AS 'HolidayDate', HolidayYear from tblHolidayList where HolidayYear=" + year + " AND HolidayDate BETWEEN '" + start.ToShortDateString() + "' AND '" + end.ToShortDateString() + " 23:59:59:970' and [delete]=0 ");
        }
        public static bool HolidayExist(DateTime HolidayDate)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (mdbh.ExecuteDataTable("SELECT 1 FROM [dbo].[tblHolidayList] WHERE HolidayDate='"+HolidayDate+"' and [delete]=0").Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static DataTable FarmerCount()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select count(*) from tblFarmerDetails where [Delete]=0");
        }
        public static DataTable EmployeesCount()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select count(*) from tblEmployeeDetails");

        }
        public static bool InspectionHistory_INSandUPDandDEL(int InspectioHistoryID, int Year, string PlanName, int SeasonID, string CreatedBY, string ModifiedBy, ref  int NewHistoryId)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            List<string> output = new List<string>();

            Params.Add(mdbh.AddParameter("InspectioHistoryID", SqlDbType.Int, InspectioHistoryID));
            Params.Add(mdbh.AddParameter("Year", SqlDbType.Int, Year));
            Params.Add(mdbh.AddParameter("PlanName", SqlDbType.NVarChar, PlanName));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("CreatedBY", SqlDbType.NVarChar, CreatedBY));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ReturnHistoryID", SqlDbType.Int, NewHistoryId, Param_Directions.Param_Out));
            try
            {
                mdbh.ExecuteNonQuery(sp.sp_InspectionHistory_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    result = Convert.ToBoolean(output[0]);
                    NewHistoryId = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool InspectionPlan_INS(int InspectionID, int InspectionHistoryID, string EmployeeID, string FarmerID, DateTime PlanDate, DateTime VisitDate, string CreatedBy, string ModifiedBy)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("InspectionID", SqlDbType.Int, InspectionID));
            Params.Add(mdbh.AddParameter("InspectionHistoryID", SqlDbType.Int, InspectionHistoryID));
            Params.Add(mdbh.AddParameter("EmployeeID", SqlDbType.UniqueIdentifier, new Guid(EmployeeID)));
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("PlanDate", SqlDbType.DateTime, PlanDate));
            Params.Add(mdbh.AddParameter("VisitDate", SqlDbType.DateTime, VisitDate));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_InspectionPlan_INS, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetInspectionPlan(int HistoryID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerArea',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', ins.PlanDate,ins.VisitDate AS 'VisitedDate',ins.InspectionID  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID = far.FarmerId WHERE ins.InspectionHistoryID = " + HistoryID + " and ins.[delete]=0 order by ins.PlanDate asc");
        }
        public static DataTable GetInspectionPlan(string EmployeeID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerArea',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', ins.PlanDate,ins.VisitDate AS 'VisitedDate',ins.InspectionID,SUM( ffd.PlotArea ) AS 'Total_Area'  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID = far.FarmerId LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID = ffd.FarmerID WHERE emp.EmployeeId =  '" + EmployeeID + "' AND ffd.ParentFarmID = 0	AND ffd.[Delete] = 0 GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID ");
        }
        public static DataTable GetInspectionPlan(string EmployeeID, string From, string To)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerVillage',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', ins.PlanDate,ins.VisitDate AS 'VisitedDate',ins.InspectionID,ins.Result,SUM( ffd.PlotArea ) AS 'Total_Area'  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID = far.FarmerId LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID = ffd.FarmerID WHERE ffd.ParentFarmID = 0	AND ffd.[Delete] = 0 AND ins.PlanDate BETWEEN '" + From + " 00:00:00.000' AND '" + To + " 23:59:59.978'   ";
            if (EmployeeID != "ALL")
                sql +=" AND emp.EmployeeId =  '" + EmployeeID + "'";
            sql += "  GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID,ins.Result ";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetInspectionPlan(string EmployeeID, string From, string To,string ICStype)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerVillage',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', ins.PlanDate,ins.VisitDate AS 'VisitedDate',ins.InspectionID,ins.Result,SUM( ffd.PlotArea ) AS 'Total_Area'  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID = far.FarmerId LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID = ffd.FarmerID WHERE far.ICSType='" + ICStype + "' AND ffd.ParentFarmID = 0	AND ffd.[Delete] = 0 AND ins.PlanDate BETWEEN '" + From + " 00:00:00.000' AND '" + To + " 23:59:59.978'   ";
            if (EmployeeID != "ALL")
                sql += " AND emp.EmployeeId =  '" + EmployeeID + "'";
            sql += "  GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID,ins.Result ";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetInspectionPlanHistory(int Year, int SeasonID,string ICStype)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [tblInspectionHistory] WHERE [Year] =" + Year + " AND [SeasonID] =" + SeasonID + " AND planname='"+ICStype+"' AND [Delete]=0 ");
        }
        public static DataTable GetInspectionBasedonFarmerName(string Farmername)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerVillage',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', ins.PlanDate,ins.VisitDate AS 'VisitedDate',ins.InspectionID,SUM( ffd.PlotArea ) AS 'Total_Area'  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID = far.FarmerId LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID = ffd.FarmerID WHERE far.FirstName='" + Farmername + "'GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID ");
        }
        public static DataTable GetInspectionByFarmerID(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerVillage',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', convert(char(11),ins.PlanDate,106) AS PlanDate,convert(char(11),ins.VisitDate,106) AS 'VisitedDate',ins.InspectionID,SUM( ffd.PlotArea ) AS 'Total_Area',ins.Result  FROM dbo.tblInspection ins INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID='" + FarmerID + "' and far.FarmerId='" + FarmerID + "' LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID='" + FarmerID + "' and ffd.FarmerID='" + FarmerID + "' WHERE ffd.ParentFarmID = 0 AND ffd.[Delete] = 0 GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID,ins.Result");
        }
        public static DataTable GetInspectionByFarmerID(string FarmerID,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT far.FarmerId,far.FirstName as 'FarmerName',far.FarmerCode,far.City_Village AS 'FarmerVillage',emp.EmployeeId AS 'InspectorID',emp.EmployeeFristName AS 'InspectorName', convert(char(11),ins.PlanDate,106) AS PlanDate,convert(char(11),ins.VisitDate,106) AS 'VisitedDate',ins.InspectionID,SUM( ffd.PlotArea ) AS 'Total_Area',ins.Result  FROM dbo.tblInspection ins INNER JOIN dbo.tblInspectionHistory inh ON ins.InspectionHistoryID = inh.InspectioHistoryID and inh.[Year] ='"+Year+"' INNER JOIN dbo.tblEmployeeDetails emp ON ins.EmployeeID = emp.EmployeeId INNER JOIN dbo.tblFarmerDetails far ON ins.FarmerID='" + FarmerID + "' and far.FarmerId='" + FarmerID + "' LEFT JOIN dbo.tblFarmerFarmDetails ffd ON ins.FarmerID='" + FarmerID + "' and ffd.FarmerID='" + FarmerID + "' WHERE ffd.ParentFarmID = 0 AND ffd.[Delete] = 0 GROUP BY far.FarmerId,far.FirstName ,far.FarmerCode,far.City_Village ,emp.EmployeeId ,emp.EmployeeFristName, ins.PlanDate,ins.VisitDate ,ins.InspectionID,ins.Result");
        }
        public static bool HolidayList_INSandUPDandDEL(int HolidayID, int year, DateTime date, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("HolidayID", SqlDbType.Int, HolidayID));
            Params.Add(mdbh.AddParameter("HolidayYear", SqlDbType.Int, year));
            Params.Add(mdbh.AddParameter("HolidayDate", SqlDbType.DateTime, date));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_HolidayList_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DataTable GetHolidaysList()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT HolidayID,HolidayDate,HolidayYear FROM tblHolidayList WHERE [Delete]= 0  ORDER BY HolidayDate ASC");
        }
        public static DataTable GetHolidaysListByYear(int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT HolidayID,HolidayDate,HolidayYear FROM tblHolidayList WHERE HolidayYear='" + year + "' AND [Delete]= 0");
        }
        public static DataTable GetHolidaysListBasedonHolidayID(int HolidayID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT HolidayID,HolidayDate,HolidayYear FROM tblHolidayList WHERE HolidayID='" + HolidayID + "' AND [Delete]= 0");
        }
        // Internal Inspection Report
        public static DataTable GetPlantingInfo(string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select pd.PlantationDate,p.PlantingSource,PlantingBill_Date,PlantingQuantity from tblPlantationDetails pd,tblPlantingInformation p where pd.FarmerId='"+farmerID+"' and pd.ProductId= p.ProductID ");
        }
        public static DataTable GetPlantationDate(string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select pd.PlantationDate,productID,FirstHarvestDate,SecondHarvestDate from tblPlantationDetails pd  where pd.FarmerId='" + farmerID + "'");
        }
        public static DataTable GetInspectiononFarmerID(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select PlanDate from tblInspection where FarmerID='" + FarmerID + "' and [Delete] = 0 [Delete] = 0 order by PlanDate asc");
        }
        public static DataTable GetPlantaionDetails(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select FirstHarvestDate,(EstimationFHerbaga+EstimationSHerbaga) as Estimation,FirstHerbaga,FirstDistillationDate,FirstProductQuantity,FarmerLotnumber,ISNULL(SoldTotalQty,0) as soldMIE,ISNULL(SoldTotalQty,0) as SoldOut  from tblPlantationDetails where FarmerId='"+FarmerID+"'");
        }
        // new code
        public static DataTable GetPlotandPlantinfo(string farmerID,int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,p.PlantingSource,PlantingBill_Date,PlantingQuantity from tblFarmerFarmDetails ffd, tblProductDetails pd ,tblPlantationDetails pld,tblPlantingInformation p where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and pld.ProductId= p.ProductID order by areacode");
        }
        public static DataTable GetPlotandInputinfo(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,ii.IMMaterial,ii.IMSource,it.IM_MT_HC,it.IMDays,it.IMPeriod,it.IMPlanting from tblFarmerFarmDetails ffd, tblProductDetails pd,tblPlantationDetails pld,tblInputTransaction it,tblInputInformation ii where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and ii.InputMID =it.InputMID and ii.ProductID=ffd.productID and pd.ProductId= ii.ProductID and ii.[delete]=0 and it.[delete]=0 order by areacode");
        }
        public static DataTable GetplotandDiesinfo(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,di.DiseaseName,DMIExpected,DMIPreventionMaterial,DMISource,DMIBillNo,dt.DMIT_HC,DMIT_Days,DMIT_Period,DMIT_Planting from tblFarmerFarmDetails ffd, tblProductDetails pd,tblPlantationDetails pld,tblDiseaseManagementInfo di,tblDiseaseManagementInfoTransaction dt where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and di.DiseaseMID =dt.DiseaseMID AND di.ProductID=ffd.productID and pd.ProductId= di.ProductID and dt.[delete]=0 and di.[delete]=0 order by areacode");
        }
        public static DataTable GetplotandInsectinfo(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,imi.InsectName,IMIExpected,IMIPreventionMaterial,IMISource,IMIBillNo,imt.InsectM_MT_HC,InsectMDays,InsectMPeriod,InsectMPlanting from tblFarmerFarmDetails ffd, tblProductDetails pd,tblPlantationDetails pld,tblInsectsManagementInfo imi,tblInsectsManagementInfoTransaction imt where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and imi.InsectMIID= imt.InsectMIID AND imi.ProductID=ffd.productID and pd.ProductId= imi.ProductID and imt.[delete]=0 and imi.[delete]=0 order by areacode");
        }
        public static DataTable GetplotandPestinfo(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,pmi.PestName,PMIExpected,PMIPreventionMaterial,PMISource,PMIBillNo,pmt.PMIT_HC,PMIT_Days,PMIT_Period,PMIT_Planting from tblFarmerFarmDetails ffd, tblProductDetails pd,tblPlantationDetails pld,tblPestManagementInfo pmi,tblPestManagementInfoTransaction pmt where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and pmi.PestMIID = pmt.PestMIID AND pmi.ProductID=ffd.productID and pd.ProductId= pmi.ProductID and pmt.[delete]=0 and pmi.[delete]=0 order by areacode");
        }
        public static DataTable GetplotandWeedInfo(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop as IIC,pd.ProductId,pld.PlantationDate,FirstHarvestDate,SecondHarvestDate,wmi.WeedName,WMIExpected,WMIPreventionMaterial,WMISource,WMIBillNo,WMT.WMIT_HC,WMIT_Days,WMIT_Period,WMIT_Planting from tblFarmerFarmDetails ffd, tblProductDetails pd,tblPlantationDetails pld,tblWeedManagementInfo wmi,tblWeedManagementInfoTransaction wmt where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId and pld.ProductId=pd.ProductId and ffd.FarmID=pld.FarmID and wmi.WeedMIID = wmt.WeedMIID AND wmi.ProductID=ffd.productID and pd.ProductId= wmi.ProductID and wmt.[delete]=0 and wmi.[delete]=0 order by areacode");
        }
        public static DataTable GetYiledDetails(string farmerID, int Year, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT ffd.AreaCode,(SELECT ffdP.PlotArea FROM tblFarmerFarmDetails ffdP WHERE ffdP.FarmID = ffd.ParentFarmID) AS 'PlotArea',ffd.IsInterCrop,"
                + " pd.*,(pd.TotalProductQuantity-pd.SecondProductQuantity) AS 'TotalProductOutput',(isnull((pd.TotalProductQuantity-pd.SecondProductQuantity),0)-isnull(SoldTotalQty,0))as AvilQty,ui.Ucode,prd.ProductName as Maincrop FROM tblPlantationDetails pd"
                + " LEFT JOIN tblFarmerDetails fd ON fd.FarmerId = pd.FarmerId"
    + " LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmID = pd.FarmID AND ffd.FarmerID = pd.FarmerId"
    + " LEFT JOIN tblFarmingInfo fi ON fi.ProductID = pd.ProductId AND fi.SeasonID = pd.SeasonID"
    + " LEFT JOIN tblUnitInformation ui ON ui.UnitId = pd.FirstUnitId"
    + " LEFT JOIN tblProductDetails prd ON prd.ProductId=pd.ProductId"
    + " WHERE pd.FarmerId = '" + farmerID + "' AND pd.ProductId = '" + ProductID + "' AND fyear = '" + Year + "' AND pd.SeasonID =  1 AND ffd.[Delete] = 0 AND pd.FirstDistillationDate <= GetDate() - 1 AND pd.SecondDistillationDate > GetDate()-1"
    + " UNION "
     + " SELECT ffd.AreaCode,(SELECT ffdP.PlotArea FROM tblFarmerFarmDetails ffdP WHERE ffdP.FarmID = ffd.ParentFarmID) AS 'PlotArea',ffd.IsInterCrop,"
     + " pd.*,(pd.FirstProductQuantity+pd.SecondProductQuantity) AS 'TotalProductOutput',(isnull((pd.TotalProductQuantity+pd.SecondProductQuantity),0)-isnull(SoldTotalQty,0))as AvilQty,ui.Ucode,prd.ProductName as Maincrop FROM tblPlantationDetails pd"
     + " LEFT JOIN tblFarmerDetails fd ON fd.FarmerId = pd.FarmerId"
     + " LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmID = pd.FarmID AND ffd.FarmerID = pd.FarmerId"
     + " LEFT JOIN tblFarmingInfo fi ON fi.ProductID = pd.ProductId AND fi.SeasonID = pd.SeasonID"
     + " LEFT JOIN tblUnitInformation ui ON ui.UnitId = pd.SecondUnitId"
     + " LEFT JOIN tblProductDetails prd ON prd.ProductId=pd.ProductId"
     + " WHERE pd.FarmerId = '" + farmerID + "' AND pd.ProductId = '" + ProductID + "' AND fyear = '" + Year + "'  AND pd.SeasonID =  1 AND ffd.[Delete] = 0  AND pd.SecondDistillationDate > GetDate()-1";
            return mdbh.ExecuteDataTable(sql);
             //return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop,pd.ProductId,pld.*,((pld.TotalProductQuantity-(select isnull(SecondProductQuantity,0.000) as SQty from tblPlantationDetails  where FarmerId='" + farmerID + "' and productID='" + ProductID + "'  and SecondDistillationDate <= GetDate()-1))-pld.SoldTotalQty) as soldoutside from tblFarmerFarmDetails ffd, tblProductDetails pd ,tblPlantationDetails pld where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID='" + ProductID + "' and pd.ProductId='" + ProductID + "' and pld.ProductId='" + ProductID + "' and ffd.FarmID=pld.FarmID order by areacode");
        }
        public static DataTable GetYiledDetails(string farmerID, int Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT ffd.AreaCode,ffd.PlotArea,ffd.IsInterCrop as IIC, pd.*,(pd.FirstProductQuantity+pd.SecondProductQuantity) AS 'TotalProductOutput',"
            + "(isnull((pd.TotalProductQuantity+pd.SecondProductQuantity),0)-isnull(SoldTotalQty,0))as AvilQty,ui.Ucode,prd.ProductName as Maincrop"
            + " FROM tblPlantationDetails pd LEFT JOIN tblFarmerDetails fd ON fd.FarmerId = pd.FarmerId"
            + " LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmID = pd.FarmID AND ffd.FarmerID = pd.FarmerId"
            + " LEFT JOIN tblFarmingInfo fi ON fi.ProductID = pd.ProductId AND fi.SeasonID = pd.SeasonID"
            + " LEFT JOIN tblUnitInformation ui ON ui.UnitId = pd.SecondUnitId"
            + " LEFT JOIN tblProductDetails prd ON prd.ProductId=pd.ProductId"
            + " WHERE pd.FarmerId = '" + farmerID + "' AND pd.ProductId = prd.ProductId AND fyear = '" + Year + "'  AND ffd.[Delete] = 0 ORDER BY ffd.AreaCode ASC";
            return mdbh.ExecuteDataTable(sql);
            //return mdbh.ExecuteDataTable("select ffd.FarmID,ffd.FarmerID,ffd.AreaCode,ffd.PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop,pd.ProductId,pld.*,((pld.TotalProductQuantity-(select isnull(SecondProductQuantity,0.000) as SQty from tblPlantationDetails  where FarmerId='" + farmerID + "' and productID='" + ProductID + "'  and SecondDistillationDate <= GetDate()-1))-pld.SoldTotalQty) as soldoutside from tblFarmerFarmDetails ffd, tblProductDetails pd ,tblPlantationDetails pld where ffd.ParentFarmID > 0 and ffd.FarmerID='" + farmerID + "' and FYear='" + Year + "' and ffd.productID='" + ProductID + "' and pd.ProductId='" + ProductID + "' and pld.ProductId='" + ProductID + "' and ffd.FarmID=pld.FarmID order by areacode");
        }
        public static bool InspectionSubmitDetails(int InspectionID, string ModifiedBy, string Report_Path, int Result)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("InspectionID",SqlDbType.Int,InspectionID));
            Params.Add(mdbh.AddParameter("Report_Path", SqlDbType.NVarChar, Report_Path));
            Params.Add(mdbh.AddParameter("Result", SqlDbType.NVarChar, Result));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_InspectionSubmitDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static  DataSet GetGeneralInspectionDetails()
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataSet(sp.sp_getGeneralInspectionDetails);
        }
        public static DataTable GetInspaectionSubmitDetails(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ins.*,fd.FarmerCode from tblInspection ihttp://localhost:4021/MudarOrganic.Website/ChartImages/ns,tblFarmerDetails fd where ins.FarmerID='" + FarmerID + "' and fd.FarmerId='" + FarmerID + "'");
        }
        public static DataTable GetReportPathDetails(string InspectionID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select CONVERT(char(11),ModifiedDate,106) as [Date],Report_Path  from tblInspection where InspectionID='" + InspectionID + "' and Result=1");
        }
        public static DataTable GetICSID()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select BranchId,Branchcode from tblBranchDetails where [delete]=0 and Other=0 order by BranchCode");
        }
        public static DataTable GetICSID(string ICSCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select BranchId,Branchcode from tblBranchDetails where [delete]=0 and Branchcode='" + ICSCode + "' order by BranchCode");
        }

    }
}
