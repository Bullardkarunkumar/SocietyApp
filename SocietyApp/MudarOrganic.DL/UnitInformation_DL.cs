using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class UnitInformation_DL
    {
        public static bool UnitInformationDetails_INSandUPDandDEL(string UnitId, string Name, string Ucode, string Uowner, string Address, int RawRequired, string OutputState, string OutputMaterial, string CapacityOfPlant, string LotsOfProducesSimultaneously, int PermanentLabour, int TemporaryLabour, int ChildLabour, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            if (string.IsNullOrEmpty(UnitId))
            {
                Params.Add(mdbh.AddParameter("UnitId", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
            }
            else
            {
                Params.Add(mdbh.AddParameter("UnitId", SqlDbType.UniqueIdentifier, new Guid(UnitId)));
            }
            Params.Add(mdbh.AddParameter("Name", SqlDbType.NVarChar, Name));
            Params.Add(mdbh.AddParameter("Ucode", SqlDbType.NVarChar, Ucode));
            Params.Add(mdbh.AddParameter("Uowner", SqlDbType.NVarChar, Uowner));

            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, Address));
            Params.Add(mdbh.AddParameter("RawRequired", SqlDbType.Int, RawRequired));
            Params.Add(mdbh.AddParameter("OutputState", SqlDbType.NVarChar, OutputState));
            Params.Add(mdbh.AddParameter("OutputMaterial", SqlDbType.NVarChar, OutputMaterial));
            Params.Add(mdbh.AddParameter("CapacityOfPlant", SqlDbType.NVarChar, CapacityOfPlant));
            Params.Add(mdbh.AddParameter("LotsOfProducesSimultaneously", SqlDbType.NVarChar, LotsOfProducesSimultaneously));
            Params.Add(mdbh.AddParameter("PermanentLabour", SqlDbType.Int, PermanentLabour));
            Params.Add(mdbh.AddParameter("TemporaryLabour", SqlDbType.Int, TemporaryLabour));
            Params.Add(mdbh.AddParameter("ChildLabour", SqlDbType.Int, ChildLabour));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_UnitInformationDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool UnitInformationDetails_INSandUPDandDEL_new(string UnitId, string Name, string Ucode, string Uowner, string Address, int RawRequired, string OutputState, string OutputMaterial, string CapacityOfPlant, string LotsOfProducesSimultaneously, int PermanentLabour, int TemporaryLabour, int ChildLabour, string CreatedBy, string ModifiedBy, int TypeOfOperation, string Unit_Village)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            if (string.IsNullOrEmpty(UnitId))
            {
                Params.Add(mdbh.AddParameter("UnitId", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
            }
            else
            {
                Params.Add(mdbh.AddParameter("UnitId", SqlDbType.UniqueIdentifier, new Guid(UnitId)));
            }
            Params.Add(mdbh.AddParameter("Name", SqlDbType.NVarChar, Name));
            Params.Add(mdbh.AddParameter("Ucode", SqlDbType.NVarChar, Ucode));
            Params.Add(mdbh.AddParameter("Uowner", SqlDbType.NVarChar, Uowner));
            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, Address));
            Params.Add(mdbh.AddParameter("RawRequired", SqlDbType.Int, RawRequired));
            Params.Add(mdbh.AddParameter("OutputState", SqlDbType.NVarChar, OutputState));
            Params.Add(mdbh.AddParameter("OutputMaterial", SqlDbType.NVarChar, OutputMaterial));
            Params.Add(mdbh.AddParameter("CapacityOfPlant", SqlDbType.NVarChar, CapacityOfPlant));
            Params.Add(mdbh.AddParameter("LotsOfProducesSimultaneously", SqlDbType.NVarChar, LotsOfProducesSimultaneously));
            Params.Add(mdbh.AddParameter("PermanentLabour", SqlDbType.Int, PermanentLabour));
            Params.Add(mdbh.AddParameter("TemporaryLabour", SqlDbType.Int, TemporaryLabour));
            Params.Add(mdbh.AddParameter("ChildLabour", SqlDbType.Int, ChildLabour));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("Unit_Village", SqlDbType.NVarChar, Unit_Village));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_UnitInformationDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool UnitInformation(string UnitID, ref DataTable dtUnitInfo)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            dtUnitInfo = mdbh.ExecuteDataTable("SELECT * FROM [dbo].[tblUnitInformation] WHERE UnitId='" + UnitID + "' AND [Delete] =0");
            if (dtUnitInfo.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static DataTable UnitInformation()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [dbo].[tblUnitInformation] WHERE [Delete] =0 order by Ucode asc");
        }
        public static DataSet UnitInformation(string UnitID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataSet("SELECT * FROM [dbo].[tblUnitInformation] WHERE UnitId='" + UnitID + "' AND [Delete] =0");
        }
        public static DataTable GetUnitInofBasedonICS(string ICSVillage)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [dbo].[tblUnitInformation] WHERE  Unit_Village IN (" + ICSVillage + ")  AND [Delete] =0 order by Ucode asc ");
        }
        public static DataTable FarmersVillageList()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT DISTINCT (City_Village) FROM tblFarmerDetails WHERE [Delete] = 0");
        }
        public static DataTable FarmersVillageListByIcs(string icsType)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT DISTINCT (City_Village) FROM tblFarmerDetails WHERE [Delete] = 0 and ICSType='" + icsType + "'");
        }
        public static DataTable FarmersVillageList(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT City_Village FROM tblFarmerDetails WHERE  FarmerId='" + FarmerID + "' and [Delete] = 0");
        }
        public static DataTable UnitInformationBasedOnVillage(string Village)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("Select * from tblUnitInformation where Unit_Village like '%" + Village + "%' AND [Delete] = 0");
        }
        public static DataTable GetDisitlationUnits(string UnitID, string ICSType,string Year)
        {
            DateTime Date = DateTime.Now.AddHours(12).AddMinutes(30).AddDays(-1);
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if(UnitID == "All")
                return mdbh.ExecuteDataTable("select pd.ProductId,ui.Ucode,FirstDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'1' as Cut,FirstHerbaga,FirstProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear='" + Year + "' and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and pd.FirstUnitId=ui.UnitId and fd.ICSType='" + ICSType + "' and pd.FirstDistillationDate < '" + Date + "' union select pd.ProductId,ui.Ucode,SecondDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'2' as Cut,SecondHerbaga,SecondProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear='" + Year + "' and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and pd.SecondUnitId =  ui.UnitId and fd.ICSType='" + ICSType + "' and pd.SecondDistillationDate < '" + Date + "'");
            else
                return mdbh.ExecuteDataTable("select ui.Ucode,FirstDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'1' as Cut,FirstHerbaga,FirstProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear='" + Year + "' and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and pd.FirstUnitId='" + UnitID + "' and  ui.UnitId='" + UnitID + "' and fd.ICSType='" + ICSType + "' and pd.FirstDistillationDate < '" + Date + "' union select ui.Ucode,SecondDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'2' as Cut,SecondHerbaga,SecondProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear='" + Year + "' and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and pd.SecondUnitId='" + UnitID + "' and ui.UnitId='" + UnitID + "' and fd.ICSType='" + ICSType + "' and pd.SecondDistillationDate < '" + Date + "'");
        }
      
        public static DataTable GetDisitlationUnits(string ICSType)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ui.Ucode,FirstDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'1' as Cut,FirstHerbaga,FirstProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear=2015 and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and fd.ICSType='" + ICSType + "' union select ui.Ucode,SecondDistillationDate as [Date],FirstName,AreaCode,prd.ProductName,'2' as Cut,SecondHerbaga,SecondProductQuantity,FarmerLotnumber,'yes' as whetherdrumsealed,' ' as inchargeperson from tblFarmerFarmDetails ffd,tblPlantationDetails pd,tblFarmerDetails fd,tblProductDetails prd,tblUnitInformation ui where ffd.FarmID=pd.FarmID and fd.FarmerId=pd.FarmerId and fd.FarmerId=ffd.FarmerID and ffd.FYear=2015 and pd.ProductId=prd.ProductId and ffd.productID=prd.ProductId and fd.ICSType='" + ICSType + "'");
        }
        public static DataTable GetData(string ICSType,string ProductID)
        {
             MudarDBHelper mdbh = MudarDBHelper.Instance;
             return mdbh.ExecuteDataTable("select sum(TotalProductQuantity) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(TotalProductQuantity) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID=4 and ProductId='"+ProductID+"' and pd.FarmerId=fd.FarmerId and fd.ICSType='"+ICSType+"'");
        }

        public static DataTable GetDataP1(string ICSType, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable DT = mdbh.ExecuteDataTable("Select * from tblSeason where SeasonYear='" + Year + "'");
            DateTime Date = DateTime.Now.AddHours(12).AddMinutes(30).AddDays(-1);
            return mdbh.ExecuteDataTable("select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='1' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.FirstDistillationDate < '" + Date + "' UNION select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='1' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.SecondDistillationDate < '" + Date + "'");
            //return mdbh.ExecuteDataTable("select sum(TotalProductQuantity) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(TotalProductQuantity) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID=4 and ProductId='1' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "'");
        }
        public static DataTable GetDataP2(string ICSType, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable DT = mdbh.ExecuteDataTable("Select * from tblSeason where SeasonYear='" + Year + "'");
            DateTime Date = DateTime.Now.AddHours(12).AddMinutes(30).AddDays(-1);
            return mdbh.ExecuteDataTable("select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='2' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.FirstDistillationDate < '" + Date + "' UNION select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='2' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.SecondDistillationDate < '" + Date + "'");
        }
        public static DataTable GetDataP3(string ICSType,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable DT = mdbh.ExecuteDataTable("Select * from tblSeason where SeasonYear='" + Year + "'");
            DateTime Date = DateTime.Now.AddHours(12).AddMinutes(30).AddDays(-1);
            return mdbh.ExecuteDataTable("select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='3' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.FirstDistillationDate < '" + Date + "' UNION select sum(ISNULL(TotalProductQuantity,0.0)) as Total,sum(ISNULL(SoldTotalQty,0.0))AS collected,sum(ISNULL(TotalProductQuantity,0.0)) - sum(ISNULL(SoldTotalQty,0.0))as Avil from tblPlantationDetails pd,tblFarmerDetails fd where SeasonID='" + DT.Rows[0]["SeasonID"] + "' and ProductId='3' and pd.FarmerId=fd.FarmerId and fd.ICSType='" + ICSType + "' AND pd.SecondDistillationDate < '" + Date + "'");
        }
    }
}
