using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;


namespace MudarOrganic.DL
{
    public static class Reports_DL
    {
        public static DataTable GetReports()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblReports WHERE [Delete] = 0");
        }
        public static bool OrderReportsPathInsertandUpdate(int OrderID, int BranchOrderID, string Path, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("Path", SqlDbType.NVarChar, Path));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_OrderReports_Path_INSandUPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable OrderReportsPathGetDetails(int OrderID, int BranchOrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [tblOrderReports] WHERE [OrderID] = '" + OrderID + "' AND [BranchOrderID] ='" + BranchOrderID + "'");
        }
        public static DataTable OrderReportsPathGetDetails(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [tblOrderReports] WHERE [OrderID] = '" + OrderID + "'");
        }
        public static DataTable Trach_Lot(string Condition)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ti.InvoiceId,ti.OrderId, ti.BranchOrderID,od.BuyerID,od.OrderDate,od.BranchID ,od.PurchaseOrderID,op.ProductID,op.Quantity,ct.BatchID,REPLACE(SUBSTRING(ct.FarmerID,1,LEN(ct.FarmerID)-1) ,';',',') AS 'FarmerIDs'  ,ct.OtherFarmersName,ct.OtherFarmerQty,REPLACE(SUBSTRING(ct.FarmID,1,LEN(ct.FarmID)-1) ,';',',') AS 'FarmIDs'  ,REPLACE(SUBSTRING(ct.Lotnumber,1,LEN(ct.Lotnumber)-1) ,';',',') AS 'Lotnumber',REPLACE(SUBSTRING(ct.PlantationID,1,LEN(ct.PlantationID)-1) ,';',',') AS 'PlantationID', fd.FarmerId, fd.FarmerCode, fd.FirstName, pd.FirstHarvestDate, pd.FirstDistillationDate , pd.FirstUnitId, pd.FirstLotNos, pd.FirstProductQuantity, pd.SecondHarvestDate,pd.SecondDistillationDate, pd.SecondUnitId, pd.SecondLotNos, pd.SecondProductQuantity , pd.PlantationDate , ui.Ucode AS 'F_Ucode', ui_s.Ucode AS 'S_Ucode' , prod.ProductName ,pd.FirstHerbaga , pd.SecondHerbaga  FROM tblInVoices ti LEFT JOIN tblOrderDetails od ON ti.OrderId = od.OrderID LEFT JOIN tblOrderProducts op ON ti.OrderId = op.OrderID LEFT JOIN tblCollection c ON ti.OrderId = c.OrderID AND ti.BranchOrderID = c.BranchOrderID LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID AND op.ProductID = ct.ProductID LEFT JOIN tblFarmerDetails fd ON CAST(fd.FarmerId as nvarchar(50)) IN (REPLACE(SUBSTRING(ct.FarmerID,1,LEN(ct.FarmerID)-1) ,';',',') ) LEFT JOIN tblPlantationDetails pd ON CAST( pd.PlantationId AS nvarchar) IN (REPLACE(SUBSTRING(ct.PlantationID,1,LEN(ct.PlantationID)-1) ,';',''',''')) LEFT JOIN tblUnitInformation ui ON ui.UnitId in ( pd.FirstUnitId ) LEFT JOIN tblUnitInformation ui_s ON ui_s.UnitId in ( pd.SecondUnitId ) LEFT JOIN tblProductDetails prod ON op.ProductID = prod.ProductId WHERE  " + Condition + "  ORDER BY op.ProductID");
        }
        public static DataTable GetInvoiceList_Farmer(string FarmerID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ct.FarmerID, ct.CollectionTransactionID, ct.ProductID, c.CollectionID, c.OrderID, i.InvoiceId, i.OrderDate, ip.Netweight, ct.Lotnumber  , pd.ProductName, ip.Netweight   FROM tblCollectionTransaction ct LEFT JOIN tblCollection c ON ct.CollectionID = c.CollectionID  LEFT JOIN tblInVoices i ON c.OrderID = i.OrderId LEFT JOIN tblInvoiceProducts ip ON i.InvoiceId = ip.InvoiceId AND ct.ProductID = ip.ProductId  LEFT JOIN tblProductDetails pd ON ct.ProductID = pd.ProductId WHERE ct.FarmerID LIKE '%" + FarmerID + "%' AND ct.ProductID='" + ProductID + "' GROUP BY ct.FarmerID, ct.CollectionTransactionID, ct.ProductID, c.CollectionID, c.OrderID, i.InvoiceId, i.OrderDate, ip.Netweight, ct.Lotnumber  , pd.ProductName, ip.Netweight  ");
        }
        public static DataTable GetAFLReportData(string ICSCode,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            int i,j;
            dt =mdbh.ExecuteDataTable("select InspectioHistoryID from tblInspectionHistory where [Year] = '" + Year + "' and PlanName='" + ICSCode + "'");
            string sql = string.Empty;
            sql = "select fd.FarmerId,FirstName,FatherName,FarmerCode,FarmerAPEDACode,City_Village,fd.Taluk,fd.[State],fd.District,TotalAreaInHectares,NumberOfPlots,ffd.PlotArea,fd.Organic,ffd.AreaCode as Surveyno,TotalAreaInHectares as Latitude,TotalAreaInHectares as Longitude,ChemicalAppDate";
            for (i = 0; i < dt.Rows.Count+1; i++)
            {
                if (i == 0)
                {
                    if (i == 0)
                        sql += ",ins.PlanDate as [first],ed.EmployeeFristName as [firsttname],ISNULL(ins.Result,'') as [firstRes]";
                    else
                        sql += ",' ' as [first],''as [firsttname],' ' as [firstRes]";
                }
                if (i == 1)
                {
                    if (i == 1)
                        sql += ",ins1.PlanDate as [second],ed1.EmployeeFristName as [secondname],ISNULL(ins1.Result,'') as [secondRes]";
                    else
                        sql += ",' ' as [second],' ' as [secondname],' ' as [secondRes]";
                }
                if (i == 2)
                {
                    if (i < dt.Rows.Count)
                        sql += ",ins2.PlanDate as [third],ed2.EmployeeFristName as [thirdname],ISNULL(ins2.Result,'') as [thirddRes]";
                    else
                        sql += ",' ' as [third],' ' as [thirdname],'false' as [thirddRes]";
                }
            }
            sql += " from tblFarmerDetails fd";
            sql += " left join tblFarmerFarmDetails ffd on ffd.ParentFarmID = 0 and fd.FarmerId=ffd.FarmerID";
            for (i = 0; i < dt.Rows.Count+1; i++)
            {
                if (i == 0)
                {
                    j =Convert.ToInt32(dt.Rows[i]["InspectioHistoryID"].ToString());
                    sql += " left join tblInspection ins on fd.FarmerId = ins.FarmerID  and ins.InspectionHistoryID ='" + j + "'";
                    sql += " left join tblEmployeeDetails ed on ins.EmployeeID = ed.EmployeeId ";
                }
                if (i == 1)
                {
                    sql += " left join tblInspection ins1 on fd.FarmerId = ins1.FarmerID and ins1.InspectionHistoryID = '" + dt.Rows[i]["InspectioHistoryID"].ToString() + "'";
                    sql += " left join tblEmployeeDetails ed1 on ins1.EmployeeID = ed1.EmployeeId ";
                }
                if (i == 2)
                {
                    if (i < dt.Rows.Count)
                    {
                        sql += " left join tblInspection ins2 on fd.FarmerId = ins2.FarmerID and ins2.InspectionHistoryID = '" + dt.Rows[i]["InspectioHistoryID"].ToString() + "'";
                        sql += " left join tblEmployeeDetails ed2 on ins2.EmployeeID = ed2.EmployeeId ";
                    }
                    else
                    {
                        sql += " left join tblInspection ins2 on fd.FarmerId = ins2.FarmerID and ins2.InspectionHistoryID = 0";
                        sql += " left join tblEmployeeDetails ed2 on ins2.EmployeeID = ed2.EmployeeId ";
                    }
                }
            }
            sql += " where fd.[Delete] = 0 and fd.ICSType ='" + ICSCode + "' order by fd.CreatedDate asc";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetAFLReportProduction(string ICSCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select FirstName,City_Village,FarmerAPEDACode,TotalAreaInHectares,ffd.PlotArea as PA,";
            sql += "'Zaid' as Season,'Main' as Crop,isnull(FFDG.PlotArea,0) as CA,isnull(pd.EstimationFHerbaga+pd.EstimationSHerbaga,0) as Esti,fd.Organic,";
            sql += "'Zaid' as Season1,'Main' as Crop1,isnull(FFDG1.PlotArea,0) as CA1,isnull(pd1.EstimationFHerbaga+pd1.EstimationSHerbaga,0) as Esti1,fd.Organic,";
            sql += "'Zaid' as Season2,'Main' as Crop2,isnull(FFDG2.PlotArea,0) as CA2,isnull(pd2.EstimationFHerbaga+pd2.EstimationSHerbaga,0) as Esti2,fd.Organic,0.000 as VC";
            sql += " FROM tblFarmerDetails fd";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='1' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='2015'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG1 ON fd.FarmerId = FFDG1.FarmerId and ffdg1.productID='2' and ffdg1.ParentFarmID  = ffd.FarmID and ffdg1.FYear='2015'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG2 ON fd.FarmerId = FFDG2.FarmerId and ffdg2.productID='3' and ffdg2.ParentFarmID  = ffd.FarmID and ffdg2.FYear='2015'";
            sql += " LEFT JOIN tblPlantationDetails pd on fd.FarmerId = pd.FarmerId and ffdg.FarmID = pd.FarmID and ffdg.ParentFarmID >0 and pd.productID=1";
            sql += " LEFT JOIN tblPlantationDetails pd1 on fd.FarmerId = pd1.FarmerId and ffdg1.FarmID = pd1.FarmID and ffdg1.ParentFarmID >0 and pd1.productID=2";
            sql += " LEFT JOIN tblPlantationDetails pd2 on fd.FarmerId = pd2.FarmerId and ffdg2.FarmID = pd2.FarmID and ffdg2.ParentFarmID >0 and pd2.productID=3";
            sql += " WHERE fd.[Delete] = 0 and fd.ICSType ='" + ICSCode + "' order by fd.CreatedDate asc";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetAFLTotalProduction(string ICSCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select FirstName,City_Village,FarmerAPEDACode,TotalAreaInHectares,ffd.PlotArea as PA,";

            sql += "isnull(FFDG.PlotArea,0) as CA,pd.PlantationDate,pd.FirstHarvestDate,pd.EstimationFHerbaga,pd.FirstHerbaga,pd.FirstDistillationDate,ui.Ucode as code1,pd.EstimationFProductQuantity,pd.FirstProductQuantity,";
            sql += "pd.SecondHarvestDate,pd.EstimationSHerbaga,pd.SecondHerbaga,pd.SecondDistillationDate,ui.Ucode as code1,pd.EstimationSProductQuantity,pd.SecondProductQuantity,pd.TotalProductQuantity,";

            sql += "isnull(FFDG1.PlotArea,0) as CA1,pd1.PlantationDate as PD,pd1.FirstHarvestDate as FHD,pd1.EstimationFHerbaga as EFH,pd1.FirstHerbaga as FH,pd1.FirstDistillationDate as FDD,ui1.Ucode as code1,pd1.EstimationFProductQuantity as EFPQ,pd1.FirstProductQuantity as FPQ,";
            sql += "pd1.SecondHarvestDate as SHD,pd1.EstimationSHerbaga as ESH,pd1.SecondHerbaga as SH,pd1.SecondDistillationDate as SDD,ui1.Ucode as code1,pd1.EstimationSProductQuantity as ESPQ,pd1.SecondProductQuantity as SPQ,pd1.TotalProductQuantity as TPQ,";

            sql += "isnull(FFDG2.PlotArea,0) as CA2,pd2.PlantationDate as PD2,pd2.FirstHarvestDate as FHD2,pd2.EstimationFHerbaga as EFH2,pd2.FirstHerbaga as FH2,pd2.FirstDistillationDate as FDD2,ui2.Ucode as code2,pd2.EstimationFProductQuantity as EFPQ2,pd2.FirstProductQuantity as FPQ2,";
            sql += "pd2.SecondHarvestDate as SHD2,pd2.EstimationSHerbaga as ESH2,pd2.SecondHerbaga as SH2,pd2.SecondDistillationDate as SDD2,ui2.Ucode as code2,pd2.EstimationSProductQuantity as ESPQ2,pd2.SecondProductQuantity as SPQ2,pd2.TotalProductQuantity as TPQ2,";

            //sql += "isnull(FFDG1.PlotArea,0) as CA2,pd2.PlantationDate as PD2,pd2.FirstHarvestDate,pd2.EstimationFHerbaga,pd2.FirstHerbaga,pd2.FirstDistillationDate,ui2.Ucode as code2,pd2.EstimationFProductQuantity,pd2.FirstProductQuantity,";
            //sql += "pd2.SecondHarvestDate,pd2.EstimationSHerbaga,pd2.SecondHerbaga,pd2.SecondDistillationDate,ui2.Ucode as code2,pd2.EstimationSProductQuantity,pd2.SecondProductQuantity,pd2.TotalProductQuantity,";
            
            sql += "0.000 as VC";
            sql += " FROM tblFarmerDetails fd";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='1' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='2015'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG1 ON fd.FarmerId = FFDG1.FarmerId and ffdg1.productID='2' and ffdg1.ParentFarmID  = ffd.FarmID and ffdg1.FYear='2015'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG2 ON fd.FarmerId = FFDG2.FarmerId and ffdg2.productID='3' and ffdg2.ParentFarmID  = ffd.FarmID and ffdg2.FYear='2015'";
            sql += " LEFT JOIN tblPlantationDetails pd on fd.FarmerId = pd.FarmerId and ffdg.FarmID = pd.FarmID and ffdg.ParentFarmID >0 and pd.productID=1";
            sql += " LEFT JOIN tblPlantationDetails pd1 on fd.FarmerId = pd1.FarmerId and ffdg1.FarmID = pd1.FarmID and ffdg1.ParentFarmID >0 and pd1.productID=2";
            sql += " LEFT JOIN tblPlantationDetails pd2 on fd.FarmerId = pd2.FarmerId and ffdg2.FarmID = pd2.FarmID and ffdg2.ParentFarmID >0 and pd2.productID=3";
            sql += " LEFT JOIN tblUnitInformation ui on pd.FirstUnitId=ui.UnitId";
            sql += " LEFT JOIN tblUnitInformation ui1 on pd1.FirstUnitId=ui1.UnitId";
            sql += " LEFT JOIN tblUnitInformation ui2 on pd2.FirstUnitId=ui2.UnitId";
            sql += " WHERE fd.[Delete] = 0 and fd.ICSType ='" + ICSCode + "' order by fd.CreatedDate asc";

            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetAFLTotalProduction(string ICSCode,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select FirstName,City_Village,FarmerAPEDACode,TotalAreaInHectares,ffd.PlotArea as PA,";

            sql += "isnull(FFDG.PlotArea,0) as CA,pd.PlantationDate,pd.FirstHarvestDate,pd.EstimationFHerbaga,pd.FirstHerbaga,pd.FirstDistillationDate,ui.Ucode as code1,pd.EstimationFProductQuantity,pd.FirstProductQuantity,";
            sql += "pd.SecondHarvestDate,pd.EstimationSHerbaga,pd.SecondHerbaga,pd.SecondDistillationDate,ui.Ucode as code1,pd.EstimationSProductQuantity,pd.SecondProductQuantity,pd.TotalProductQuantity,";

            sql += "isnull(FFDG1.PlotArea,0) as CA1,pd1.PlantationDate as PD,pd1.FirstHarvestDate as FHD,pd1.EstimationFHerbaga as EFH,pd1.FirstHerbaga as FH,pd1.FirstDistillationDate as FDD,ui1.Ucode as code1,pd1.EstimationFProductQuantity as EFPQ,pd1.FirstProductQuantity as FPQ,";
            sql += "pd1.SecondHarvestDate as SHD,pd1.EstimationSHerbaga as ESH,pd1.SecondHerbaga as SH,pd1.SecondDistillationDate as SDD,ui1.Ucode as code1,pd1.EstimationSProductQuantity as ESPQ,pd1.SecondProductQuantity as SPQ,pd1.TotalProductQuantity as TPQ,";

            sql += "isnull(FFDG2.PlotArea,0) as CA2,pd2.PlantationDate as PD2,pd2.FirstHarvestDate as FHD2,pd2.EstimationFHerbaga as EFH2,pd2.FirstHerbaga as FH2,pd2.FirstDistillationDate as FDD2,ui2.Ucode as code2,pd2.EstimationFProductQuantity as EFPQ2,pd2.FirstProductQuantity as FPQ2,";
            sql += "pd2.SecondHarvestDate as SHD2,pd2.EstimationSHerbaga as ESH2,pd2.SecondHerbaga as SH2,pd2.SecondDistillationDate as SDD2,ui2.Ucode as code2,pd2.EstimationSProductQuantity as ESPQ2,pd2.SecondProductQuantity as SPQ2,pd2.TotalProductQuantity as TPQ2,";

            //sql += "isnull(FFDG1.PlotArea,0) as CA2,pd2.PlantationDate as PD2,pd2.FirstHarvestDate,pd2.EstimationFHerbaga,pd2.FirstHerbaga,pd2.FirstDistillationDate,ui2.Ucode as code2,pd2.EstimationFProductQuantity,pd2.FirstProductQuantity,";
            //sql += "pd2.SecondHarvestDate,pd2.EstimationSHerbaga,pd2.SecondHerbaga,pd2.SecondDistillationDate,ui2.Ucode as code2,pd2.EstimationSProductQuantity,pd2.SecondProductQuantity,pd2.TotalProductQuantity,";

            sql += "0.000 as VC";
            sql += " FROM tblFarmerDetails fd";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='1' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='"+Year+"'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG1 ON fd.FarmerId = FFDG1.FarmerId and ffdg1.productID='2' and ffdg1.ParentFarmID  = ffd.FarmID and ffdg1.FYear='" + Year + "'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG2 ON fd.FarmerId = FFDG2.FarmerId and ffdg2.productID='3' and ffdg2.ParentFarmID  = ffd.FarmID and ffdg2.FYear='" + Year + "'";
            sql += " LEFT JOIN tblPlantationDetails pd on fd.FarmerId = pd.FarmerId and ffdg.FarmID = pd.FarmID and ffdg.ParentFarmID >0 and pd.productID=1";
            sql += " LEFT JOIN tblPlantationDetails pd1 on fd.FarmerId = pd1.FarmerId and ffdg1.FarmID = pd1.FarmID and ffdg1.ParentFarmID >0 and pd1.productID=2";
            sql += " LEFT JOIN tblPlantationDetails pd2 on fd.FarmerId = pd2.FarmerId and ffdg2.FarmID = pd2.FarmID and ffdg2.ParentFarmID >0 and pd2.productID=3";
            sql += " LEFT JOIN tblUnitInformation ui on pd.FirstUnitId=ui.UnitId";
            sql += " LEFT JOIN tblUnitInformation ui1 on pd1.FirstUnitId=ui1.UnitId";
            sql += " LEFT JOIN tblUnitInformation ui2 on pd2.FirstUnitId=ui2.UnitId";
            sql += " WHERE fd.[Delete] = 0 and fd.ICSType ='" + ICSCode + "' order by fd.CreatedDate asc";

            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetAFLReportProductionEstimation(string ICSCode,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select FirstName,FarmerCode,FarmerAPEDACode,TotalAreaInHectares,ffd.AreaCode as PC,ffd.PlotArea as PA,";
            sql += "pd.EstimationFHerbaga as FEH1,pd.EstimationSHerbaga as SEH1,isnull(FFDG.PlotArea,0) as CA,isnull(pd.EstimationFHerbaga+pd.EstimationSHerbaga,0) as Esti,pd.EstimationFProductQuantity as FEO,pd.EstimationSProductQuantity as SEO ,isnull(pd.EstimationFProductQuantity+pd.EstimationSProductQuantity,0) as ETO,";
            sql += "pd1.EstimationFHerbaga as FEH2,pd1.EstimationSHerbaga as SEH2,isnull(FFDG1.PlotArea,0) as CA1,isnull(pd1.EstimationFHerbaga+pd1.EstimationSHerbaga,0) as Esti1,pd1.EstimationFProductQuantity as FEO1,pd1.EstimationSProductQuantity as SEO1,isnull(pd1.EstimationFProductQuantity+pd1.EstimationSProductQuantity,0) as ETO1,";
            sql += "pd2.EstimationFHerbaga as FEH3,pd2.EstimationSHerbaga as SEH3,isnull(FFDG2.PlotArea,0) as CA2,isnull(pd2.EstimationFHerbaga+pd2.EstimationSHerbaga,0) as Esti2,pd2.EstimationFProductQuantity as FEO2,pd2.EstimationSProductQuantity as SEO2,isnull(pd2.EstimationFProductQuantity+pd2.EstimationSProductQuantity,0) as ETO2,0.000 as VC";
            sql += " FROM tblFarmerDetails fd";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='1' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='" + Year + "'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG1 ON fd.FarmerId = FFDG1.FarmerId and ffdg1.productID='2' and ffdg1.ParentFarmID  = ffd.FarmID and ffdg1.FYear='" + Year + "'";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG2 ON fd.FarmerId = FFDG2.FarmerId and ffdg2.productID='3' and ffdg2.ParentFarmID  = ffd.FarmID and ffdg2.FYear='" + Year + "'";
            sql += " LEFT JOIN tblPlantationDetails pd on fd.FarmerId = pd.FarmerId and ffdg.FarmID = pd.FarmID and ffdg.ParentFarmID >0 and pd.productID=1";
            sql += " LEFT JOIN tblPlantationDetails pd1 on fd.FarmerId = pd1.FarmerId and ffdg1.FarmID = pd1.FarmID and ffdg1.ParentFarmID >0 and pd1.productID=2";
            sql += " LEFT JOIN tblPlantationDetails pd2 on fd.FarmerId = pd2.FarmerId and ffdg2.FarmID = pd2.FarmID and ffdg2.ParentFarmID >0 and pd2.productID=3";
            sql += " WHERE fd.[Delete] = 0 and fd.ICSType ='" + ICSCode + "' order by fd.CreatedDate asc";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetAFLTotalProduction(string ICSCode, string Year,int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select FirstName,City_Village,FarmerAPEDACode,TotalAreaInHectares,ffd.PlotArea as PA,";
            sql += "isnull(FFDG.PlotArea,0) as CA,isnull(pd.EstimationFHerbaga,0) as EstimationFHerbaga,isnull(pd.FirstHerbaga,0) as FirstHerbaga,isnull(pd.EstimationFProductQuantity,0) as EstimationFProductQuantity,isnull(pd.FirstProductQuantity,0) as FirstProductQuantity,";
            sql += "isnull(pd.EstimationSHerbaga,0) as EstimationSHerbaga,isnull(pd.SecondHerbaga,0) as SecondHerbaga,isnull(pd.EstimationSProductQuantity,0) as EstimationSProductQuantity,isnull(pd.SecondProductQuantity,0) as SecondProductQuantity,isnull(pd.TotalProductQuantity,0) as TotalProductQuantity";
            sql += " FROM tblFarmerDetails fd";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0";
            sql += " LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='" + ProductID + "' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='" + Year + "'";
            sql += " LEFT JOIN tblPlantationDetails pd on fd.FarmerId = pd.FarmerId and ffdg.FarmID = pd.FarmID and ffdg.ParentFarmID >0 and pd.productID='"+ProductID+"'";
            sql += " WHERE fd.[Delete] = 0 and fd.ICSType =" + ICSCode + " order by fd.CreatedDate asc";
            return mdbh.ExecuteDataTable(sql);
        }
        #region Branch Reports
        public static DataTable GetCollectionDates(int productID, string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            string cc = dtp.Rows[0]["ProductCode"].ToString() + code;
            if (productID == 2)
                return mdbh.ExecuteDataTable("select distinct(createddate) from tblCollectionTransaction where ProductId in (2,4) and Lotnumber like '%" + cc + "%' order by createddate asc");
            else
                return mdbh.ExecuteDataTable("select distinct(createddate) from tblCollectionTransaction where ProductId='" + productID + "' and Lotnumber like '%" + cc + "%' order by createddate asc");
        }
        public static DataTable GetAllProducDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select pd.* from tblProductDetails pd where pd.[delete]=0 and ProductType=1");
        }
        public static DataTable GetSelectedCollectionDate(string Date, int productID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;

            if (productID == 2)
                return mdbh.ExecuteDataTable("select ct.*,pd.ProductName from tblCollectionTransaction ct,tblProductDetails pd where ct.ProductId in (2,4) and pd.ProductID in (2,4) and ct.createddate='" + Date + "'");
            else
                return mdbh.ExecuteDataTable("select ct.*,pd.ProductName from tblCollectionTransaction ct,tblProductDetails pd where ct.ProductId='" + productID + "' and pd.ProductID='" + productID + "' and ct.createddate='" + Date + "'");
        }
        public static DataTable GetSelectedCollectionDate(string Date, int productID, string Code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            string cc = dtp.Rows[0]["ProductCode"].ToString() + Code;
            if (productID == 2)
                return mdbh.ExecuteDataTable("select ct.*,'Organic Cornmint Oil ' as ProductName from tblCollectionTransaction ct,tblProductDetails pd where ct.ProductId in (2,4) and pd.ProductID in (2,4) and Lotnumber like '%" + cc + "%' and ct.createddate='" + Date + "'");
            else
                return mdbh.ExecuteDataTable("select ct.*,pd.ProductName from tblCollectionTransaction ct,tblProductDetails pd where ct.ProductId='" + productID + "' and pd.ProductID='" + productID + "' and Lotnumber like '%" + cc + "%' and ct.createddate='" + Date + "'");
        }
        public static DataTable GetSelectedBlendnDate(string Date, int productID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (productID == 2)
                return mdbh.ExecuteDataTable("select * from tblBlendingTransaction where ProductId in (2,4) and createddate='" + Date + "'");

            else
                return mdbh.ExecuteDataTable("select * from tblBlendingTransaction where ProductId='" + productID + "' and createddate='" + Date + "'");
        }
        public static DataTable GetSelectedBlendQty(string Code, int productID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            string cc = dtp.Rows[0]["ProductCode"].ToString() + Code;
            if (productID == 2)
                return mdbh.ExecuteDataTable("select bt.*,'Organic Cornmint Oil 'as ProductName from tblBlendingTransaction bt where bt.ProductId in (2,4) and Lotnumber like '%" + cc + "%' ");
            else
                return mdbh.ExecuteDataTable("select bt.*,pd.ProductName from tblBlendingTransaction bt,tblProductDetails pd where bt.ProductId='" + productID + "' and pd.ProductId='" + productID + "' and Lotnumber like '%" + cc + "%' ");
        }
        public static DataTable GetProduction(string ProductID, DateTime Date, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT pld.farmid,pld.FirstProductQuantity as TotalProductQuantity,";
            sql += " ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable'";
            sql += " FROM tblProductDetails pd LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId";
            sql += " LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId and fd.ICSType in ('ICS01','ICS02')";
            sql += " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= '" + Date + "' AND pld.SecondDistillationDate > '" + Date + "' AND ffd.FYear = '" + Year + "'";
            sql += " UNION";
            sql += " SELECT pld.farmid,pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable'";
            sql += " FROM tblProductDetails pd LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId and fd.ICSType in ('ICS01','ICS02')";
            sql += " WHERE pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= '" + Date + "' AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0 AND ffd.FYear = '" + Year + "'";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetNewallcollectionList(string productID, string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (productID == "All")
            {
                DataTable dt = mdbh.ExecuteDataTable("select ct.*,c.OrderID, ProductName from tblCollectionTransaction ct left join tblCollection c on c.CollectionID = ct.CollectionID left join tblFarmerDetails fd on fd.FarmerId= ct.FarmerID left join tblProductDetails pd on pd.ProductId = ct.ProductID where ct.[Delete]=0 and Lotnumber like '%x15%' or  Lotnumber like '%Y15%' or Lotnumber like '%Z15%'");
                return dt;
            }
            else
            {
                DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
                string cc = dtp.Rows[0]["ProductCode"].ToString() + code;
                if (productID == "2")
                {
                    DataTable dt = mdbh.ExecuteDataTable("select ct.*,c.OrderID,'Organic Cornmint Oil ' as ProductName from tblCollectionTransaction ct left join tblCollection c on c.CollectionID = ct.CollectionID left join tblFarmerDetails fd on fd.FarmerId= ct.FarmerID left join tblProductDetails pd on pd.ProductId = ct.ProductID where pd.ProductId in ('2','4') and ct.ProductID in ('2','4') and ct.[Delete]=0 and Lotnumber like '%" + cc + "%'");
                    return dt;
                }
                else
                {
                    DataTable dt = mdbh.ExecuteDataTable("select ct.*,c.OrderID, ProductName from tblCollectionTransaction ct left join tblCollection c on c.CollectionID = ct.CollectionID left join tblFarmerDetails fd on fd.FarmerId= ct.FarmerID left join tblProductDetails pd on pd.ProductId = ct.ProductID where pd.ProductId='" + productID + "' and ct.ProductID='" + productID + "' and ct.[Delete]=0 and Lotnumber like '%" + cc + "%'");
                    return dt;
                }
            }
        }
        public static DataTable GetNewallcollectionList(string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            // DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            //string cc = dtp.Rows[0]["ProductCode"].ToString() + code;
            DataTable dt = mdbh.ExecuteDataTable("select ct.*,c.OrderID, ProductName,'Yes' as 'Whether',' ' as 'ReceivedBy',' ' as 'Remarks' from tblCollectionTransaction ct left join tblCollection c on c.CollectionID = ct.CollectionID left join tblFarmerDetails fd on fd.FarmerId= ct.FarmerID left join tblProductDetails pd on pd.ProductId = ct.ProductID where ct.[Delete]=0 order by CreatedDate");
            return dt;
        }
        public static DataTable GetBlendDetails(string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Code = string.Empty;
            if (ProductID == "All")
            {
                DataTable dt = mdbh.ExecuteDataTable("select bt.CreatedDate,cl.OrderID,prd.ProductName,bt.Lotnumber,bt.BlendQty,bqty,Blending_BatchID,bqty,' ' as 'BlendedBy',' ' as 'Remarks',prd.ProductId from tblBlendingTransaction bt left join tblBlending bl on bt.BlendingID= bl.BlendingID left join tblProductDetails pd on pd.ProductId = bt.ProductID left join tblCollection cl on cl.CollectionID = bl.CollectionID left join tblProductDetails prd on prd.ProductId=bt.ProductID where bt.[Delete]=0 and bt.Lotnumber like '%x15%' or  bt.Lotnumber like '%Y15%' or bt.Lotnumber like '%Z15%' order by bt.CreatedDate");
                return dt;
            }
            else
            {
                if (ProductID == "1")
                    Code = "X15";
                if (ProductID == "2")
                    Code = "Y15";
                if (ProductID == "3")
                    Code = "Z15";
                DataTable dt = mdbh.ExecuteDataTable("select bt.CreatedDate,cl.OrderID,prd.ProductName,bt.Lotnumber,bt.BlendQty,bqty,Blending_BatchID,bqty as 'LotQty(KG)',' ' as 'BlendedBy',' ' as 'Remarks',prd.ProductId from tblBlendingTransaction bt left join tblBlending bl on bt.BlendingID= bl.BlendingID left join tblProductDetails pd on pd.ProductId='" + ProductID + "'AND bt.ProductID='" + ProductID + "' left join tblCollection cl on cl.CollectionID = bl.CollectionID left join tblProductDetails prd on prd.ProductId='" + ProductID + "' and bt.ProductID='" + ProductID + "' where bt.[Delete]=0 and bt.Lotnumber like '%" + Code + "%' order by bt.CreatedDate");
                return dt;
            }
        }
        public static DataTable GePackingDetails(string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Code = string.Empty;
            if (ProductID == "All")
            {
                DataTable dt = mdbh.ExecuteDataTable("select opd.*,cl.OrderID,bt.BQty,pd.ProductName,'' as'Labelspasted?','' as 'PackedBy','' as 'Remarks' from tblOrderPackingDetails opd left join tblCollection cl on opd.CollectionID=cl.CollectionID left join tblProductDetails pd  on opd.ProductID=pd.ProductId left join tblBlendingTransaction bt on bt.Blending_BatchID=opd.LotNumber where opd.LotNumber like '%X15%' or opd.LotNumber like '%Y15%' OR opd.LotNumber like '%Z15%' order by opd.CreatedDate");
                return dt;
            }
            else
            {
                if (ProductID == "1")
                    Code = "X15";
                if (ProductID == "2")
                    Code = "Y15";
                if (ProductID == "3")
                    Code = "Z15";
                DataTable dt = mdbh.ExecuteDataTable("select opd.*,cl.OrderID,bt.BQty,pd.ProductName,'' as'Labelspasted?','' as 'PackedBy','' as 'Remarks' from tblOrderPackingDetails opd left join tblCollection cl on opd.CollectionID=cl.CollectionID left join tblProductDetails pd  on opd.ProductID='" + ProductID + "' and pd.ProductId='" + ProductID + "' left join tblBlendingTransaction bt on bt.Blending_BatchID=opd.LotNumber where opd.LotNumber like '%" + Code + "%' order by opd.CreatedDate");
                return dt;
            }
        }
        public static DataTable GeDispatchDetails(string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Code = string.Empty;
            if (ProductID == "All")
            {
                DataTable dt = mdbh.ExecuteDataTable("select bo.BOdispatchDate,opd.*,cl.OrderID,PD.ProductName,bt.BQty,'Mudar India Exports' as'Dispatchedto','' as 'Vehicleno','Good' as 'Vehiclecondition','' as 'Dispatchedby','' as 'Remarks' from tblBranchOrder bo left join tblCollection cl on bo.OrderID=cl.OrderID left join tblOrderPackingDetails opd on opd.CollectionID=cl.CollectionID left join tblProductDetails pd  on opd.ProductID=pd.ProductId left join tblBlendingTransaction bt on bt.Blending_BatchID=opd.LotNumber  where opd.LotNumber like '%X15%' or opd.LotNumber like '%Y15%' OR opd.LotNumber like '%Z15%' order by bo.BOdispatchDate");
                return dt;
            }
            else
            {
                if (ProductID == "1")
                    Code = "X15";
                if (ProductID == "2")
                    Code = "Y15";
                if (ProductID == "3")
                    Code = "Z15";
                DataTable dt = mdbh.ExecuteDataTable("select bo.BOdispatchDate,opd.*,cl.OrderID,PD.ProductName,bt.BQty,'Mudar India Exports' as'Dispatchedto','' as 'Vehicleno','Good' as 'Vehiclecondition','' as 'Dispatchedby','' as 'Remarks' from tblBranchOrder bo left join tblCollection cl on bo.OrderID=cl.OrderID left join tblOrderPackingDetails opd on opd.CollectionID=cl.CollectionID left join tblProductDetails pd  on opd.ProductID='" + ProductID + "' and pd.ProductId='" + ProductID + "' left join tblBlendingTransaction bt on bt.Blending_BatchID=opd.LotNumber where opd.LotNumber like '%" + Code + "%' order by bo.BOdispatchDate");
                return dt;
            }
        }
        public static DataTable GetFreezeDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select StartDate,EndDate,OrderID,'Organic Cornmint Oil' as Productname,bt.Blending_BatchID,fz.FBatchID,Quntatiy as 'Qty',ft.FreezeQuantity as 'CrystalReceived',ft.freezeProductBatchID as 'CrystalLotnumber',ft1.FreezeQuantity,ft1.freezeProductBatchID,'' as 'Operator',''as 'Remarks'  from tblFreeze fz LEFT JOIN tblFreezeTransaction ft ON fz.FreezeID=ft.FreezeID and ft.ProductID=4 LEFT JOIN tblFreezeTransaction ft1 ON fz.FreezeID=ft1.FreezeID and ft1.ProductID=6 LEFT JOIN tblBlendingTransaction bt on bt.BlendingTransactionID=fz.BlendingTransID where fz.[Delete] = 0 and ft.[Delete] =0 and ft.FreezeProductBatchID like '%C15%' OR ft.FreezeProductBatchID like '%D1815%' order by StartDate asc");
            return dt;
        }
        #endregion
    }
}
