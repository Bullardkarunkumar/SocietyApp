using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.Components;
using System.Data.SqlClient;
using System.Data;

namespace MudarOrganic.DL
{
    public static class FarmPlantation_DL
    {
        public static DataTable BuildPlantation(int Year, int SeasonID, int ProdcutID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT * FROM tblPlantationDetails pd WHERE pd.SeasonID in (SELECT s.SeasonID  FROM tblSeason s WHERE s.SeasonYear = 2012 AND s.SeasonID = 15) AND pd.ProductId = 6");
            string sql = "SELECT pd.*, fd.FarmerCode, fd.FirstName, fd.TotalAreaInHectares,fd.FarmerRegNumber, ffd.AreaCode, ffd.PlotArea FROM tblPlantationDetails pd LEFT JOIN tblFarmerDetails fd ON pd.FarmerId = fd.FarmerId LEFT JOIN tblFarmerFarmDetails ffd ON pd.FarmID = ffd.FarmID WHERE pd.SeasonID in (SELECT s.SeasonID FROM tblSeason s WHERE s.SeasonYear = " + Year + " AND s.SeasonID = " + SeasonID + ")";
            if (ProdcutID > 0)
                sql = sql + " AND pd.ProductId = " + ProdcutID;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable BuildPlantation(int Year, int SeasonID, int ProdcutID, string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT * FROM tblPlantationDetails pd WHERE pd.SeasonID in (SELECT s.SeasonID  FROM tblSeason s WHERE s.SeasonYear = 2012 AND s.SeasonID = 15) AND pd.ProductId = 6");
            //string sql = "SELECT pd.*, fd.FarmerCode, fd.FirstName, fd.TotalAreaInHectares,fd.FarmerRegNumber, ffd.AreaCode, ffd.PlotArea, (SELECT PlotArea FROM  tblFarmerFarmDetails WHERE FarmID = ffd.ParentFarmID ) AS 'TotalPloatArea' FROM tblPlantationDetails pd LEFT JOIN tblFarmerDetails fd ON pd.FarmerId = fd.FarmerId LEFT JOIN tblFarmerFarmDetails ffd ON pd.FarmID = ffd.FarmID WHERE pd.[Delete] = 0 AND ffd.[Delete] = 0 AND pd.SeasonID in (SELECT s.SeasonID FROM tblSeason s WHERE s.SeasonYear = " + Year + " AND s.SeasonID = " + SeasonID + ")";
            string sql = "	SELECT fd.FarmerCode,fd.FirstName,fd.TotalAreaInHectares,ffd.AreaCode, (SELECT ffdP.PlotArea FROM tblFarmerFarmDetails ffdP WHERE ffdP.FarmID = ffd.ParentFarmID) AS 'PlotArea'	, ffd.PlotArea AS 'PlantationArea',  pd.*,(pd.TotalProductQuantity-pd.SecondProductQuantity) AS 'TotalProductOutput' "
    + ", ui.Ucode "
    + "FROM tblPlantationDetails pd "
    + "LEFT JOIN tblFarmerDetails fd ON fd.FarmerId = pd.FarmerId "
    + "LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmID = pd.FarmID AND ffd.FarmerID = pd.FarmerId  "
    + "LEFT JOIN tblFarmingInfo fi ON fi.ProductID = pd.ProductId AND fi.SeasonID = pd.SeasonID "
    + "LEFT JOIN tblUnitInformation ui ON ui.UnitId = pd.FirstUnitId "
    + "WHERE pd.FarmerId = '" + farmerID + "' "
        + " AND pd.ProductId = " + ProdcutID
        + " AND pd.SeasonID = " + SeasonID
        + " AND ffd.[Delete] = 0 "
        + " AND pd.FirstDistillationDate <= GetDate() - 1 "
        + " AND pd.SecondDistillationDate > GetDate() - 1 "
    + " UNION "
    + " SELECT fd.FarmerCode,fd.FirstName,fd.TotalAreaInHectares,ffd.AreaCode "
    + " , (SELECT ffdP.PlotArea FROM tblFarmerFarmDetails ffdP WHERE ffdP.FarmID = ffd.ParentFarmID) AS 'PlotArea' "
    + " , ffd.PlotArea AS 'PlantationArea',  pd.*,(pd.FirstProductQuantity+pd.SecondProductQuantity) AS 'TotalProductOutput' "
    + " ,ui.Ucode "
    + " FROM tblPlantationDetails pd "
    + " LEFT JOIN tblFarmerDetails fd ON fd.FarmerId = pd.FarmerId "
    + " LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmID = pd.FarmID AND ffd.FarmerID = pd.FarmerId "
    + " LEFT JOIN tblFarmingInfo fi ON fi.ProductID = pd.ProductId AND fi.SeasonID = pd.SeasonID "
    + " LEFT JOIN tblUnitInformation ui ON ui.UnitId = pd.FirstUnitId "
    + " WHERE pd.FarmerId = '" + farmerID + "' "
        + " AND pd.ProductId = " + ProdcutID
        + " AND pd.SeasonID = " + SeasonID
        + " AND ffd.[Delete] = 0 "
        + " AND pd.SecondDistillationDate <= GetDate() ";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetPlantation(int Year, int SeasonID, int ProdcutID, string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT * FROM tblPlantationDetails pd WHERE pd.SeasonID in (SELECT s.SeasonID  FROM tblSeason s WHERE s.SeasonYear = 2012 AND s.SeasonID = 15) AND pd.ProductId = 6");
            string sql = "SELECT fd.FarmerID, fd.FarmerRegNumber, ffd.AreaCode, ffd.PlotArea, ffd.Latitude,  ffd.Longitude, ffd.ParentFarmID, ffd.FYear, ffd.FarmID, ffd.IsInterCrop, ffd.SeasonId, pd.* FROM tblFarmerDetails fd  LEFT JOIN tblFarmerFarmDetails ffd ON ffd.FarmerID = fd.FarmerId  LEFT JOIN tblPlantationDetails pd ON pd.FarmerId = fd.FarmerId AND pd.FarmID = ffd.FarmID  WHERE fd.FarmerId = '" + farmerID + "' AND fd.[Delete] = 0 AND fd.InternalInspectorApproval = 1 AND ffd.[Delete] = 0 AND ffd.FYear = " + Year.ToString() + " AND pd.SeasonID in (SELECT s.SeasonID FROM tblSeason s WHERE s.SeasonYear = " + Year.ToString() + " AND s.SeasonID = " + SeasonID.ToString() + ") AND pd.ProductId = " + ProdcutID.ToString();
            return mdbh.ExecuteDataTable(sql);
        }
        public static bool PlantationDetails_INSandUPDandDEL(string FarmerId, int ProductId, string UnitId, int FarmID, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Int, PlantationId));
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.Int, FarmID));
            Params.Add(mdbh.AddParameter("PlantationDate", SqlDbType.DateTime, PlantationDate));
            Params.Add(mdbh.AddParameter("FirstHarvestDate", SqlDbType.DateTime, FirstHarvestDate));
            Params.Add(mdbh.AddParameter("FirstHerbaga", SqlDbType.Decimal, FirstHerbaga));
            Params.Add(mdbh.AddParameter("FirstDistillationDate", SqlDbType.DateTime, FirstDistillationDate));
            Params.Add(mdbh.AddParameter("FirstDistillationUnitNO", SqlDbType.Int, FirstDistillationUnitNO));
            Params.Add(mdbh.AddParameter("FirstProductQuantity", SqlDbType.Decimal, FirstProductQuantity));
            Params.Add(mdbh.AddParameter("SecondHarvestDate", SqlDbType.DateTime, SecondHarvestDate));
            Params.Add(mdbh.AddParameter("SecondHerbaga", SqlDbType.Decimal, SecondHerbaga));
            Params.Add(mdbh.AddParameter("SecondDistillationDate", SqlDbType.DateTime, SecondDistillationDate));
            Params.Add(mdbh.AddParameter("SecondDistillationUnitNO", SqlDbType.Int, SecondDistillationUnitNO));
            Params.Add(mdbh.AddParameter("SecondProductQuantity", SqlDbType.Decimal, SecondProductQuantity));
            Params.Add(mdbh.AddParameter("TotalProductQuantity", SqlDbType.Decimal, TotalProductQuantity));
            Params.Add(mdbh.AddParameter("ProposedFieldOfficer", SqlDbType.Bit, ProposedFieldOfficer));
            Params.Add(mdbh.AddParameter("ProposedManager", SqlDbType.Bit, ProposedManager));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("PlantationArea", SqlDbType.Decimal, PlantationArea));
            Params.Add(mdbh.AddParameter("FirstUnitId", SqlDbType.UniqueIdentifier, new Guid(FirstUnitId)));
            Params.Add(mdbh.AddParameter("SecondUnitId", SqlDbType.UniqueIdentifier, new Guid(SecondUnitId)));
            Params.Add(mdbh.AddParameter("FirstNoOfLots", SqlDbType.Int, FirstNoOfLots));
            Params.Add(mdbh.AddParameter("SecondNoOfLots", SqlDbType.Int, SecondNoOfLots));
            Params.Add(mdbh.AddParameter("FirstLotNos", SqlDbType.NVarChar, FirstLotNos));
            Params.Add(mdbh.AddParameter("SecondLotNos", SqlDbType.NVarChar, SecondLotNos));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));
            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_PlantationDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public static DataTable GetPlotFarmPlantationDetails(int Year, int SeasonID, int ProdcutID, int parentFarmID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select tpd.PlantationId,tfd.FarmID from tblFarmerFarmDetails tfd join tblPlantationDetails tpd on tfd.FarmID=tpd.FarmID where tfd.ParentFarmID='" + parentFarmID + "' and tfd.productID=" + ProdcutID + " and tfd.SeasonID=" + SeasonID + " and tfd.FYear=" + Year + " and tfd.[Delete] =0");
        }

        //public static bool ChekPlantation(int Year, int SeasonID, int ProdcutID)

        public static bool Farm_PlantationDetails_INSandUPDandDEL(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, decimal Latitude, decimal Longitude, int ParentFarmID, int ProductId, string UnitId, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos, int FYear, bool IsInterCrop, decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            //FarmID
            Params.Add(mdbh.AddParameter("PlotArea", SqlDbType.Decimal, PlotArea));
            Params.Add(mdbh.AddParameter("AreaCode", SqlDbType.NVarChar, AreaCode));
            Params.Add(mdbh.AddParameter("Latitude", SqlDbType.Decimal, Latitude));
            Params.Add(mdbh.AddParameter("Longitude", SqlDbType.Decimal, Longitude));
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.Int, FarmID));
            Params.Add(mdbh.AddParameter("ParentFarmID", SqlDbType.Int, ParentFarmID));
            Params.Add(mdbh.AddParameter("IsInterCrop", SqlDbType.Bit, IsInterCrop));
            //Plantation.
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Int, PlantationId));
            Params.Add(mdbh.AddParameter("PlantationDate", SqlDbType.DateTime, PlantationDate));
            Params.Add(mdbh.AddParameter("FirstHarvestDate", SqlDbType.DateTime, FirstHarvestDate));
            Params.Add(mdbh.AddParameter("FirstHerbaga", SqlDbType.Decimal, FirstHerbaga));
            Params.Add(mdbh.AddParameter("FirstDistillationDate", SqlDbType.DateTime, FirstDistillationDate));
            Params.Add(mdbh.AddParameter("FirstDistillationUnitNO", SqlDbType.Int, FirstDistillationUnitNO));
            Params.Add(mdbh.AddParameter("FirstProductQuantity", SqlDbType.Decimal, FirstProductQuantity));
            Params.Add(mdbh.AddParameter("SecondHarvestDate", SqlDbType.DateTime, SecondHarvestDate));
            Params.Add(mdbh.AddParameter("SecondHerbaga", SqlDbType.Decimal, SecondHerbaga));
            Params.Add(mdbh.AddParameter("SecondDistillationDate", SqlDbType.DateTime, SecondDistillationDate));
            Params.Add(mdbh.AddParameter("SecondDistillationUnitNO", SqlDbType.Int, SecondDistillationUnitNO));
            Params.Add(mdbh.AddParameter("SecondProductQuantity", SqlDbType.Decimal, SecondProductQuantity));
            Params.Add(mdbh.AddParameter("TotalProductQuantity", SqlDbType.Decimal, TotalProductQuantity));
            Params.Add(mdbh.AddParameter("ProposedFieldOfficer", SqlDbType.Bit, ProposedFieldOfficer));
            Params.Add(mdbh.AddParameter("ProposedManager", SqlDbType.Bit, ProposedManager));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("PlantationArea", SqlDbType.Decimal, PlantationArea));
            Params.Add(mdbh.AddParameter("FirstUnitId", SqlDbType.UniqueIdentifier, new Guid(FirstUnitId)));
            Params.Add(mdbh.AddParameter("SecondUnitId", SqlDbType.UniqueIdentifier, new Guid(SecondUnitId)));
            Params.Add(mdbh.AddParameter("FirstNoOfLots", SqlDbType.Int, FirstNoOfLots));
            Params.Add(mdbh.AddParameter("SecondNoOfLots", SqlDbType.Int, SecondNoOfLots));
            Params.Add(mdbh.AddParameter("FirstLotNos", SqlDbType.NVarChar, FirstLotNos));
            Params.Add(mdbh.AddParameter("SecondLotNos", SqlDbType.NVarChar, SecondLotNos));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("FYear", SqlDbType.Int, FYear));
            Params.Add(mdbh.AddParameter("EstimationFHerbaga", SqlDbType.Decimal, EstiFiHerbage));
            Params.Add(mdbh.AddParameter("EstimationFProductQuantity", SqlDbType.Decimal, EstiFOilqty));
            Params.Add(mdbh.AddParameter("EstimationSHerbaga", SqlDbType.Decimal, EstiSeHerbage));
            Params.Add(mdbh.AddParameter("EstimationSProductQuantity", SqlDbType.Decimal, EstiSeOilqty));

            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_Farm_Plantation_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public static bool sp_FarmerProduction_UPD(DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, decimal FirstProductQuantity
                                             , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate, decimal SecondProductQuantity, decimal TotalProductQuantity,
                             int PlantationId, decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PlantationDate", SqlDbType.DateTime, PlantationDate));
            Params.Add(mdbh.AddParameter("FirstHarvestDate", SqlDbType.DateTime, FirstHarvestDate));
            Params.Add(mdbh.AddParameter("FirstHerbaga", SqlDbType.Decimal, FirstHerbaga));
            Params.Add(mdbh.AddParameter("FirstDistillationDate", SqlDbType.DateTime, FirstDistillationDate));
            Params.Add(mdbh.AddParameter("FirstProductQuantity", SqlDbType.Decimal, FirstProductQuantity));
            Params.Add(mdbh.AddParameter("SecondHarvestDate", SqlDbType.DateTime, SecondHarvestDate));
            Params.Add(mdbh.AddParameter("SecondHerbaga", SqlDbType.Decimal, SecondHerbaga));
            Params.Add(mdbh.AddParameter("SecondDistillationDate", SqlDbType.DateTime, SecondDistillationDate));
            Params.Add(mdbh.AddParameter("SecondProductQuantity", SqlDbType.Decimal, SecondProductQuantity));
            Params.Add(mdbh.AddParameter("TotalProductQuantity", SqlDbType.Decimal, TotalProductQuantity));
            Params.Add(mdbh.AddParameter("EstimationFHerbaga", SqlDbType.Decimal, EstiFiHerbage));
            Params.Add(mdbh.AddParameter("EstimationFProductQuantity", SqlDbType.Decimal, EstiFOilqty));
            Params.Add(mdbh.AddParameter("EstimationSHerbaga", SqlDbType.Decimal, EstiSeHerbage));
            Params.Add(mdbh.AddParameter("EstimationSProductQuantity", SqlDbType.Decimal, EstiSeOilqty));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Decimal, PlantationId));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));
            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmerProduction_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        //Karun Added
        public static DataTable BindDropDownChild()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT * FROM tblPlantationDetails pd WHERE pd.SeasonID in (SELECT s.SeasonID  FROM tblSeason s WHERE s.SeasonYear = 2012 AND s.SeasonID = 15) AND pd.ProductId = 6");
            string sql = "SELECT * FROM tblUnitInformation";
            return mdbh.ExecuteDataTable(sql);
        }

        public static bool SoldQuantity_Update(int PlantationId, decimal SoldTotalQty, string ModifiedBy)
        {
            bool Result = true;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("SoldTotalQty", SqlDbType.Decimal, SoldTotalQty));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Int, PlantationId));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));

            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPlantationDetails_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable GetFarmerFarmdetails(string FarmerID, int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(" select * from  tblFarmerFarmDetails WHERE [FarmerId]='" + FarmerID + "' and [FYear] = '" + year + "' and [Delete] =0");
        }
        public static bool UpdatePlantationDetails(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, decimal Latitude, decimal Longitude, int ParentFarmID, int ProductId, string UnitId, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos, int FYear, bool IsInterCrop, decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            //FarmID
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.Int, FarmID));
            Params.Add(mdbh.AddParameter("ParentFarmID", SqlDbType.Int, ParentFarmID));
            Params.Add(mdbh.AddParameter("PlotArea", SqlDbType.Decimal, PlotArea));
            //Plantation.
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Int, PlantationId));
            Params.Add(mdbh.AddParameter("PlantationDate", SqlDbType.DateTime, PlantationDate));
            Params.Add(mdbh.AddParameter("FirstHarvestDate", SqlDbType.DateTime, FirstHarvestDate));
            Params.Add(mdbh.AddParameter("FirstHerbaga", SqlDbType.Decimal, FirstHerbaga));
            Params.Add(mdbh.AddParameter("FirstDistillationDate", SqlDbType.DateTime, FirstDistillationDate));
            Params.Add(mdbh.AddParameter("FirstDistillationUnitNO", SqlDbType.Int, FirstDistillationUnitNO));
            Params.Add(mdbh.AddParameter("FirstProductQuantity", SqlDbType.Decimal, FirstProductQuantity));
            Params.Add(mdbh.AddParameter("SecondHarvestDate", SqlDbType.DateTime, SecondHarvestDate));
            Params.Add(mdbh.AddParameter("SecondHerbaga", SqlDbType.Decimal, SecondHerbaga));
            Params.Add(mdbh.AddParameter("SecondDistillationDate", SqlDbType.DateTime, SecondDistillationDate));
            Params.Add(mdbh.AddParameter("SecondDistillationUnitNO", SqlDbType.Int, SecondDistillationUnitNO));
            Params.Add(mdbh.AddParameter("SecondProductQuantity", SqlDbType.Decimal, SecondProductQuantity));
            Params.Add(mdbh.AddParameter("TotalProductQuantity", SqlDbType.Decimal, TotalProductQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("PlantationArea", SqlDbType.Decimal, PlantationArea));
            Params.Add(mdbh.AddParameter("FirstUnitId", SqlDbType.UniqueIdentifier, new Guid(FirstUnitId)));
            Params.Add(mdbh.AddParameter("SecondUnitId", SqlDbType.UniqueIdentifier, new Guid(SecondUnitId)));
            Params.Add(mdbh.AddParameter("FirstNoOfLots", SqlDbType.Int, FirstNoOfLots));
            Params.Add(mdbh.AddParameter("SecondNoOfLots", SqlDbType.Int, SecondNoOfLots));
            Params.Add(mdbh.AddParameter("FirstLotNos", SqlDbType.NVarChar, FirstLotNos));
            Params.Add(mdbh.AddParameter("SecondLotNos", SqlDbType.NVarChar, SecondLotNos));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("FYear", SqlDbType.Int, FYear));
            Params.Add(mdbh.AddParameter("EstimationFHerbaga", SqlDbType.Decimal, EstiFiHerbage));
            Params.Add(mdbh.AddParameter("EstimationFProductQuantity", SqlDbType.Decimal, EstiFOilqty));
            Params.Add(mdbh.AddParameter("EstimationSHerbaga", SqlDbType.Decimal, EstiSeHerbage));
            Params.Add(mdbh.AddParameter("EstimationSProductQuantity", SqlDbType.Decimal, EstiSeOilqty));
            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_UPD_FarmPlot_PlantationDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        public static bool DeletePlantationDetails(int FarmID, string ModifiedBy)
        {
            bool Result = true;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.NVarChar, FarmID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_DEL_PlantationDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable GetAviableQtty(int ProductID)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            return mdbh.ExecuteDataTable(sp.SP_CheckAvailableQty, Params, "AvailQty");
        }

        public static DataTable GetParentFarmId( string areaCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select FarmID,FarmerID from  tblFarmerFarmDetails WHERE [AreaCode] = '" + areaCode + "' and ParentFarmID=0 and [Delete] =0");
       
        }
        public static DataTable GetProductionDataOnProductID(int ProductID,string Year,string ICStype)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select fd.farmercode,firstname,ffd.AreaCode,ffd.ParentFarmID,pd.PlantationId,pd.FarmID,PlantationDate,FirstDistillationDate as [DistillationDate],'I' AS Cut,PlantationArea,FirstHerbaga as Herbage,FirstProductQuantity as Qty,ISNULL(SoldTotalQty,0) AS SoldTotalQty,ISNULL((FirstProductQuantity-SoldTotalQty),0) as AvialQty from tblplantationdetails pd, tblFarmerFarmDetails ffd ,tblfarmerdetails fd where pd.ProductId='" + ProductID + "' and ffd.productID = '" + ProductID + "' and ffd.fyear='" + Year + "' and fd.ICSType in (" + ICStype + ") and pd.FarmID = ffd.FarmID and fd.farmerid= pd.farmerid union select fd.farmercode,firstname,ffd.AreaCode,ffd.ParentFarmID,pd.PlantationId,pd.FarmID,PlantationDate,SecondDistillationDate as [DistillationDate],'II' AS Cut,PlantationArea,SecondHerbaga as Herbage,(SecondProductQuantity),ISNULL(SoldTotalQty,0) AS SoldTotalQty,ISNULL((SecondProductQuantity-SoldTotalQty),0) as AvialQty from tblplantationdetails pd, tblFarmerFarmDetails ffd ,tblfarmerdetails fd where pd.ProductId='" + ProductID + "' and ffd.productID = '" + ProductID + "' and ffd.fyear='" + Year + "' and fd.ICSType in (" + ICStype + ") and pd.FarmID = ffd.FarmID and fd.farmerid= pd.farmerid");
            //return mdbh.ExecuteDataTable("select fd.farmercode,firstname,ffd.AreaCode,ffd.ParentFarmID,pd.PlantationId,pd.FarmID,PlantationDate,PlantationArea,TotalProductQuantity,ISNULL(SoldTotalQty,0) AS SoldTotalQty,ISNULL((TotalProductQuantity-SoldTotalQty),0) as AvialQty from tblplantationdetails pd, tblFarmerFarmDetails ffd ,tblfarmerdetails fd where pd.ProductId='" + ProductID + "' and ffd.productID = '" + ProductID + "' and ffd.fyear='"+Year+"' and pd.FarmID = ffd.FarmID and fd.farmerid= pd.farmerid order by ffd.FarmID asc");
        }
    }
}
