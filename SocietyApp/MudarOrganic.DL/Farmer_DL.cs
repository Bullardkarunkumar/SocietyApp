using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.Components;
using System.Data.SqlClient;
using System.Data;
namespace MudarOrganic.DL
{
    public static class Farmer_DL
    {

        public static bool Farmer_INSandUPTandDEL(string FarmerId, string FarmerCode, string FirstName, string FarmerMIECode, string FarmerAPEDACode, decimal TotalAreaInHectares, int NumberOfPlots, string FatherName, string Address, string City_Village, string Taluk, string District, string State, string Country, string PhoneNumber, string MobileNumber, string BankInfo, string BankAccNo, int ChildrenDependents, int ElderlyDependents, int NumberOfEarningPersons, string ChildrenStudies, int Cow, int Ox, int Sheep, int OtherAnimals, bool ProposedFieldOfficer, bool ProposedManager, bool InternalInspectorApproval, bool PRESIDNT, string CreatedBy, string ModifiedBy, string FarmerRegNumber, string PhotoPath, int TypeOfOPeration, DateTime ChemicalAppDate, string BankHolderName, bool Organic, bool OrganicFair,string icsCode)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            Params.Add(mdbh.AddParameter("FarmerCode", SqlDbType.NVarChar, FarmerCode));
            Params.Add(mdbh.AddParameter("FirstName", SqlDbType.NVarChar, FirstName));
            Params.Add(mdbh.AddParameter("FarmerMIECode", SqlDbType.NVarChar, FarmerMIECode));
            Params.Add(mdbh.AddParameter("FarmerAPEDACode", SqlDbType.NVarChar, FarmerAPEDACode));
            Params.Add(mdbh.AddParameter("TotalAreaInHectares", SqlDbType.Decimal, TotalAreaInHectares));
            Params.Add(mdbh.AddParameter("NumberOfPlots", SqlDbType.Int, NumberOfPlots));
            Params.Add(mdbh.AddParameter("FatherName", SqlDbType.NVarChar, FatherName));
            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, Address));
            Params.Add(mdbh.AddParameter("City_Village", SqlDbType.NVarChar, City_Village));
            Params.Add(mdbh.AddParameter("Taluk", SqlDbType.NVarChar, Taluk));
            Params.Add(mdbh.AddParameter("District", SqlDbType.NVarChar, District));
            Params.Add(mdbh.AddParameter("State", SqlDbType.NVarChar, State));
            Params.Add(mdbh.AddParameter("Country", SqlDbType.NVarChar, Country));
            Params.Add(mdbh.AddParameter("PhoneNumber", SqlDbType.NVarChar, PhoneNumber));
            Params.Add(mdbh.AddParameter("MobileNumber", SqlDbType.NVarChar, MobileNumber));
            Params.Add(mdbh.AddParameter("BankInfo", SqlDbType.NVarChar, BankInfo));
            Params.Add(mdbh.AddParameter("BankAccNo", SqlDbType.NVarChar, BankAccNo));
            Params.Add(mdbh.AddParameter("ChildrenDependents", SqlDbType.Int, ChildrenDependents));
            Params.Add(mdbh.AddParameter("ElderlyDependents", SqlDbType.Int, ElderlyDependents));
            Params.Add(mdbh.AddParameter("NumberOfEarningPersons", SqlDbType.Int, NumberOfEarningPersons));
            Params.Add(mdbh.AddParameter("ChildrenStudies", SqlDbType.NVarChar, ChildrenStudies));
            Params.Add(mdbh.AddParameter("Cow", SqlDbType.Int, Cow));
            Params.Add(mdbh.AddParameter("Ox", SqlDbType.Int, Ox));
            Params.Add(mdbh.AddParameter("Sheep", SqlDbType.Int, Sheep));
            Params.Add(mdbh.AddParameter("OtherAnimals", SqlDbType.Int, OtherAnimals));
            Params.Add(mdbh.AddParameter("ProposedFieldOfficer", SqlDbType.Bit, ProposedFieldOfficer));
            Params.Add(mdbh.AddParameter("ProposedManager", SqlDbType.Bit, ProposedManager));
            Params.Add(mdbh.AddParameter("InternalInspectorApproval", SqlDbType.Bit, InternalInspectorApproval));
            Params.Add(mdbh.AddParameter("PRESIDNT", SqlDbType.Bit, PRESIDNT));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("FarmerRegNumber", SqlDbType.NVarChar, FarmerRegNumber));
            Params.Add(mdbh.AddParameter("PhotoPath", SqlDbType.NVarChar, PhotoPath));
            Params.Add(mdbh.AddParameter("TypeOfOPeration", SqlDbType.Int, TypeOfOPeration));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ChemicalAppDate", SqlDbType.DateTime, ChemicalAppDate));
            Params.Add(mdbh.AddParameter("BankHolderName", SqlDbType.NVarChar, BankHolderName));
            Params.Add(mdbh.AddParameter("Organic", SqlDbType.Bit, Organic));
            Params.Add(mdbh.AddParameter("OrganicFair", SqlDbType.Bit, OrganicFair));
            Params.Add(mdbh.AddParameter("ICSType", SqlDbType.VarChar, icsCode));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmerDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public static bool FarmerFamilyDetails_INS(int FarmerFamilyId, string FarmerId, string ChildName, string Gender, DateTime DOB, int Age, bool SchoolGoing, bool Working, string CreatedBy, string ModifiedBy, int TypeOfOPeration, int NumberEarningPersons, int ElderDependents)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerFamilyId", SqlDbType.Int, FarmerFamilyId));
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            Params.Add(mdbh.AddParameter("Name", SqlDbType.NVarChar, ChildName));
            Params.Add(mdbh.AddParameter("Gender ", SqlDbType.NVarChar, Gender));
            Params.Add(mdbh.AddParameter("DOB ", SqlDbType.DateTime, DOB));
            Params.Add(mdbh.AddParameter("AGE ", SqlDbType.Int, Age));
            Params.Add(mdbh.AddParameter("SchoolGoing", SqlDbType.Bit, SchoolGoing));
            Params.Add(mdbh.AddParameter("Working", SqlDbType.Bit, Working));
            Params.Add(mdbh.AddParameter("TypeOfOPeration", SqlDbType.Int, TypeOfOPeration));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ElderDependents", SqlDbType.Int, ElderDependents));
            Params.Add(mdbh.AddParameter("NumberEarningPersons", SqlDbType.Int, NumberEarningPersons));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmerFamilyDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DataTable FarmerCount(string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT COUNT(*) AS 'FarmerCount' FROM tblFarmerDetails WHERE FarmerCode LIKE '" + code + "%'");
        }
        public static DataTable FamerNameCodeArea(string Value, int TypeOfOperation)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT ";
            if (TypeOfOperation == 1)
                sql += " [FirstName] ";
            else if (TypeOfOperation == 2)
                sql += " [FarmerCode] ";
            else if (TypeOfOperation == 3)
                sql += " [City_Village] ";
            sql += " FROM tblFarmerDetails WHERE ";
            if (TypeOfOperation == 1)
                sql += " [FirstName] LIKE '%" + Value + "%'";
            else if (TypeOfOperation == 2)
                sql += " [FarmerCode] LIKE '%" + Value + "%'";
            else if (TypeOfOperation == 3)
                sql += " [City_Village] LIKE '%" + Value + "%'";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable FarmerNameCodeArea(string Value)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT ";
            sql += " [FirstName] + ' - ' + ";
            sql += " [FarmerCode] + ' - ' +  ";
            sql += " [City_Village] AS FarmerNameCodeArea ";
            sql += " FROM tblFarmerDetails WHERE ";
            sql += "( [FirstName] LIKE '%" + Value + "%' OR ";
            sql += " [FarmerCode] LIKE '%" + Value + "%' OR ";
            sql += " [City_Village] LIKE '%" + Value + "%')";
            sql += " AND [Delete] = 0";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable FamerDetails(string farmerName, string farmerCode, string area)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT TOP 1 * FROM tblFarmerDetails WHERE ";
            if (!string.IsNullOrEmpty(farmerName))
                sql += " [FirstName] LIKE '%" + farmerName + "%'";
            if (!string.IsNullOrEmpty(farmerCode))
            {
                if (!string.IsNullOrEmpty(farmerName))
                    sql += " AND ";
                sql += " [FarmerCode] LIKE '%" + farmerCode + "%'";
            }
            if (!string.IsNullOrEmpty(area))
            {
                if (!string.IsNullOrEmpty(farmerName) || !string.IsNullOrEmpty(farmerCode))
                    sql += " AND ";
                sql += " [City_Village] LIKE '%" + area + "%'";
            }
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable FamerDetails(string farmerCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT TOP 1 * FROM tblFarmerDetails WHERE  [FarmerCode] = '" + farmerCode + "' and  [Delete] = 0 ";
            return mdbh.ExecuteDataTable(sql);
        }

        public static DataTable FamerDetailsByIcs(string icsCode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT * FROM tblFarmerDetails WHERE  [ICSType] = '" + icsCode + "' and  [Delete] = 0 ";
            //sql = "SELECT * FROM tblFarmerDetails WHERE  [ICSType] = '" + icsCode + "' and  [Delete] = 0 order by createddate asc ";
            return mdbh.ExecuteDataTable(sql);
        }

        public static string GetUserICSDetails(Guid userId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "select BranchCode from tblBranchDetails where BranchId=( select ted.BranchId from tblUserLogin tul join tblEmployeeDetails ted on tul.UserId=ted.EmployeeId where tul.UserId='" + userId.ToString() + "' and ted.[Delete]=0 and tul.[Delete]=0) and [Delete]=0";
            DataTable dt = mdbh.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0][0]);
            }
            else
            {
                return string.Empty;
            }
        }

        public static DataTable FamerDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT * FROM tblFarmerDetails WHERE  [Delete] = 0 ORDER BY CreatedDate DESC";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable FarmerDetails(string Village)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            if (Village != "All")
                sql = "SELECT * FROM tblFarmerDetails WHERE  City_Village = '" + Village + "' and [Delete] = 0 ORDER BY CreatedDate,FarmerCode asc";
            else
                sql = "SELECT * FROM tblFarmerDetails WHERE [Delete] = 0 ORDER BY CreatedDate,FarmerCode asc";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetFarmerDetailsonID(string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT * FROM tblFarmerDetails WHERE  FarmerId='" + farmerID + "' AND [Delete] = 0";
            return mdbh.ExecuteDataTable(sql);
        }
        //ravi code for detailed view on 24-marc-13
        public static DataTable FamerDetailDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "(select farmer.FirstName,farmer.City_Village,farmer.TotalAreaInHectares,farmer.Organic,farmer.FarmerCode,farmer.[Presidnt],farmer.FarmerId,farmer.CreatedDate from tblFarmerDetails farmer where farmer.[Delete]=0)union(select '' as FirstName,'' as City_Village,	farm.PlotArea as TotalAreaInHectares,farmer.Organic,farm.areacode,farmer.[Presidnt],farm.FarmerID,farmer.CreatedDate from 	 tblFarmerFarmDetails farm join tblFarmerDetails farmer on farm.FarmerID=farmer.FarmerId where farm.[Delete]=0 and farmer.[Delete]=0 and farm.ParentFarmID=0)order by createddate desc,farmerid,FirstName desc";
            return mdbh.ExecuteDataTable(sql);
        }
        //code ends
        public static DataTable ApprovedFamer_Inspection(bool Approved)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = string.Empty;
            sql = "SELECT * FROM tblFarmerDetails WHERE  [Delete] = 0 AND InternalInspectorApproval='" + Approved + "' ORDER BY CreatedDate DESC";
            return mdbh.ExecuteDataTable(sql);
        }
        public static bool FarmerExist(string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (mdbh.ExecuteDataTable("SELECT 1 FROM [dbo].[tblFarmerDetails] WHERE FarmerId='" + farmerID + "'").Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static bool FarmerSeasonProduct_INSandUPTandDEL(int SeasonID, string FarmerID, string SeasonName, string CropCultivating, bool Result, string CreatedBy, string ModifiedBy, DateTime StartDate, DateTime EndDate, int SeasonYear, int ProductId, int TypeOfOperation)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("SeasonName", SqlDbType.NVarChar, SeasonName));
            Params.Add(mdbh.AddParameter("CropCultivating", SqlDbType.NVarChar, CropCultivating));
            Params.Add(mdbh.AddParameter("Result", SqlDbType.Bit, Result));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("StartDate", SqlDbType.DateTime, StartDate));
            Params.Add(mdbh.AddParameter("EndDate", SqlDbType.DateTime, EndDate));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.Int, SeasonYear));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));

            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_SeasonDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public static bool FarmerSeasonProduct_INSandUPTandDELNEW(int SeasonID, string FarmerID, string SeasonName, string CropCultivating, bool Result, string CreatedBy, string ModifiedBy, DateTime StartDate, DateTime EndDate, int SeasonYear, int ProductId, int TypeOfOperation)
        {
            bool output = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("SeasonName", SqlDbType.NVarChar, SeasonName));
            Params.Add(mdbh.AddParameter("CropCultivating", SqlDbType.NVarChar, CropCultivating));
            Params.Add(mdbh.AddParameter("Result", SqlDbType.Bit, Result));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("StartDate", SqlDbType.DateTime, StartDate));
            Params.Add(mdbh.AddParameter("EndDate", SqlDbType.DateTime, EndDate));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.Int, SeasonYear));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, output, Param_Directions.Param_Out));

            try
            {
                output = (bool)mdbh.ExecuteNonQuery(sp.sp_SeasonDetails_INSandUPDandDELNew, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public static DataTable FarmerFarm(string FarmerUid)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblFarmerDetails WHERE FarmerId = '" + FarmerUid + "'");
        }
        #region Farm
        public static bool Farm_INSandUPTandDEL(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, int Latitude, int Longitude, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref  int ReturnFarmID, int ParentFarmID)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            List<string> output = new List<string>();

            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));
            Params.Add(mdbh.AddParameter("PlotArea", SqlDbType.Decimal, PlotArea));
            Params.Add(mdbh.AddParameter("AreaCode", SqlDbType.NVarChar, AreaCode));
            Params.Add(mdbh.AddParameter("Latitude", SqlDbType.Int, Latitude));
            Params.Add(mdbh.AddParameter("Longitude", SqlDbType.Int, Longitude));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.Int, FarmID));
            Params.Add(mdbh.AddParameter("ParentFarmID", SqlDbType.Int, ParentFarmID));
            Params.Add(mdbh.AddParameter("ReturnFarmID", SqlDbType.Int, ReturnFarmID, Param_Directions.Param_Out));

            try
            {
                mdbh.ExecuteNonQuery(sp.sp_FarmDetails_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    result = Convert.ToBoolean(output[0]);
                    ReturnFarmID = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable FarmDetails(string FarmerId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerId", SqlDbType.UniqueIdentifier, new Guid(FarmerId)));

            return mdbh.ExecuteDataTable(sp.sp_FarmDetails, Params, "FarmerDetails");
        }
        public static DataTable AvaliablePlotArea(string FarmerId, int SeasonID, int Year, int FarmID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT (SELECT plotarea FROM tblFarmerFarmDetails WHERE FarmerId = '" + FarmerId + "'  and [Delete]=0 and FarmID=" + FarmID + ") - ISNULL( SUM(plotarea) , 0) AS 'Avaliable Area' FROM tblFarmerFarmDetails ffb WHERE ffb.FarmerId = '" + FarmerId + "' and FYear=" + Year + " and [Delete]=0 and ParentFarmID=" + FarmID + " and ffb.seasonid=" + SeasonID + " and ffb.IsInterCrop=0");
        }
        public static DataTable FarmDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT fd.FarmerId, fd.FarmerCode, fd.FirstName, fd.FarmerRegNumber, fd.TotalAreaInHectares, ffd.FarmID, ffd.PlotArea, ffd.AreaCode  FROM tblFarmerDetails fd LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerID = ffd.FarmerID  WHERE ffd.[Delete] = 0 AND ffd.FarmID NOT IN (SELECT ParentFarmID FROM tblFarmerFarmDetails WHERE [Delete] = 0 AND ParentFarmID>0) ORDER BY fd.FarmerId");
        }
        #endregion

        public static DataTable GetFarmerFamilyDeatils(string FarmerId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT [FFamilyDetailsId],[FarmerId],[Name],[Gender],[DOB],DATEDIFF(yy,[DOB],GETDATE()) AS Age,[SchoolGoing],[Working] FROM [dbo].[tblFarmerFamilyDetails]WHERE [FarmerId]='" + FarmerId + "' and [Delete] =0");
        }
        public static DataTable GetSeasonYearByFarmer()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT DISTINCT([SeasonYear]) FROM [tblSeasonDetails]");
        }
        public static DataTable GetSeasonDetailsBasedFarmerID(string FarmerID, int SeasonYear)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("farmerid", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("seasonYear", SqlDbType.Int, SeasonYear));
            return mdbh.ExecuteDataTable(sp.sp_GetSeasonDetailsBasedFarmerID, Params, "FarmerSeasonDetailsName");
        }
        public static DataTable Getsubcheckpoints(string QuesID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblSubCheckPoints where QuestionID='" + QuesID + "'");
        }
        //Code by ravi to get Questions
        public static DataTable GetCheckpointQuestions(string FarmerId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;

            DataTable dtFarmer = mdbh.ExecuteDataTable("SELECT CP.Question,CP.QuestionID,CP.Qcategory,FCP.FarmerID,FCP.Answer,FCP.year,FCP.Note from tblCheckPoints CP" +
                            " inner join tblFarmerCheckPoints FCP" +
                            " on CP.QuestionID=FCP.QuestionID and CP.[delete] = 0 and FCP.FarmerID= '" + FarmerId + "' order by FCP.Year");
            if (dtFarmer.Rows.Count > 0)
            {
                return dtFarmer;
            }
            else
            {
                dtFarmer = mdbh.ExecuteDataTable("SELECT CP.Question,CP.QuestionID,CP.Qcategory,'3' as Answer," + DateTime.Now.Year + " as year,' ' as Note from tblCheckPoints CP where [delete]=0");
                return dtFarmer;
            }
        }
        //Code to insert/Update/Delete in tblFarmerCheckPoints
        public static bool FarmerCheckPoints_INSandUPDandDEL(string FarmerID, int QuestionID, int Answer, string Note, string CreatedBy, string ModifiedBy, int TypeOfOperation, int Year)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("QuestionID", SqlDbType.Int, QuestionID));
            Params.Add(mdbh.AddParameter("Answer", SqlDbType.Int, Answer));
            Params.Add(mdbh.AddParameter("Note", SqlDbType.NVarChar, Note));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("Year", SqlDbType.Int, Year));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));

            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmerCheckPoints_INSandUPDandDEL, Params);
                return result;
            }
            catch (Exception)
            {

                return result;
            }
        }

        public static bool FieldRisk_Exist(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dtFieldRisk = mdbh.ExecuteDataTable("SELECT * FROM tblFieldRisk_Farmer WHERE FarmerID = '" + FarmerID + "'");
            return dtFieldRisk.Rows.Count > 0 ? false : true;
        }

        public static DataTable FiledRisk(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblFarmerFarmDetails ffd, tblFieldRisk fr WHERE ffd.FarmerID = '" + FarmerID + "'	AND ffd.[Delete] = 0	AND ffd.ParentFarmID = 0 ORDER BY ffd.FarmID ");
        }
        public static DataTable FiledRiskResult(string FarmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ffd.FarmerID,ffd.FarmID, ffd.AreaCode,frf.FieldRiskFarmerID,frf.Result ,fr.FieldRiskID,fr.FieldRiskType  FROM tblFarmerFarmDetails ffd LEFT JOIN tblFieldRisk_Farmer frf ON ffd.FarmerID=frf.FarmerID AND ffd.FarmID = frf.FarmID LEFT JOIN tblFieldRisk fr ON frf.FieldRiskID = fr.FieldRiskID WHERE ffd.FarmerID = '" + FarmerID + "'	AND ffd.[Delete] = 0	AND ffd.ParentFarmID = 0");
        }
        public static bool FiledRiskt_Result_INSandUPDandDEL(int FieldRiskID, string FarmerID, int FarmID, bool Result, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool returnResult = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("FieldRiskID", SqlDbType.Int, FieldRiskID));
            Params.Add(mdbh.AddParameter("FarmID", SqlDbType.Int, FarmID));
            Params.Add(mdbh.AddParameter("Result", SqlDbType.Bit, Result));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, returnResult, Param_Directions.Param_Out));
            try
            {
                returnResult = (bool)mdbh.ExecuteNonQuery(sp.sp_FieldRisk_Farmer_INSandUPDandDEL, Params);
                return returnResult;
            }
            catch (Exception)
            {

                return returnResult;
            }
        }
        public static bool FarmerApproval(string FarmerID, string InspectorName, string InspectionComments, string ModifiedBy, int TypeOfOperation, ref bool ReturnValue)
        {
            bool returnResult = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("InspectorName", SqlDbType.NVarChar, InspectorName));
            Params.Add(mdbh.AddParameter("InspectionComments", SqlDbType.NVarChar, InspectionComments));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, returnResult, Param_Directions.Param_Out));
            try
            {
                returnResult = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmerApproval, Params);
                return returnResult;
            }
            catch (Exception)
            {
                return returnResult;
            }
        }
        public static DataTable GetFarmerProductDetailsBasedonYear(string FarmerID, int SeasonYear)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT  pd.ProductId,pd.ProductName,sd.SeasonName FROM tblSeasonDetails sd, tblProductDetails pd WHERE FarmerID = '" + FarmerID + "' AND SeasonYear = '" + SeasonYear + "' and sd.Result=1 AND pd.ProductId = sd.ProductId AND sd.[Delete] = 0");
        }
        public static DataTable GetFarmerProdcts(int SeasonYear, int seasonID, int productID, string Village, string ICStype)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (Village != "All")
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ISNULL(ffd.AreaCode,0) as PlotCode,ISNULL(ffd.PlotArea,0)as plotarea,ISNULL(ffdg.plotarea,0) as croparea,(ISNULL(ffd.plotarea,0)-((select ISNULL(SUM(PlotArea),0 )from tblFarmerFarmDetails ffdF,tblFarmerDetails fdF  where productID > 0 AND ffdf.[Delete]=0 AND ffdF.FarmerID = fdF.FarmerId and ffdF.ParentFarmID  = ffd.FarmID and ffdF.FYear ='" + SeasonYear + "'))) as Availablearea,fd.City_Village,fd.Organic,fd.InternalInspectorApproval,ffd.FarmID FROM tblFarmerDetails fd INNER JOIN tblSeasonDetails sd ON fd.FarmerId in (SELECT sd.FarmerID FROM tblSeasonDetails sd, tblProductDetails pd WHERE  sd.SeasonYear='" + SeasonYear + "' AND pd.ProductId='" + productID + "' AND sd.ProductId='" + productID + "' AND sd.Result=1 AND sd.m_SeasonID ='" + seasonID + "') LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0 LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='" + productID + "' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='" + SeasonYear + "' WHERE fd.InternalInspectorApproval= 1 and fd.ICSType='" + ICStype + "' and fd.[Delete]=0 and fd.City_Village = '" + Village + "' GROUP BY ffd.AreaCode,ffd.farmid,fd.FarmerCode,fd.FirstName,fd.FarmerId,fd.TotalAreaInHectares,fd.City_Village,fd.Organic,fd.InternalInspectorApproval,ffd.PlotArea,ffdg.PlotArea order by ffd.FarmID asc");
            else
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ISNULL(ffd.AreaCode,0) as PlotCode,ISNULL(ffd.PlotArea,0)as plotarea,ISNULL(ffdg.plotarea,0) as croparea,(ISNULL(ffd.plotarea,0)-((select ISNULL(SUM(PlotArea),0 )from tblFarmerFarmDetails ffdF,tblFarmerDetails fdF  where productID > 0 AND ffdf.[Delete]=0 AND ffdF.FarmerID = fdF.FarmerId and ffdF.ParentFarmID  = ffd.FarmID and ffdF.FYear ='" + SeasonYear + "'))) as Availablearea,fd.City_Village,fd.Organic,fd.InternalInspectorApproval,ffd.FarmID FROM tblFarmerDetails fd INNER JOIN tblSeasonDetails sd ON fd.FarmerId in (SELECT sd.FarmerID FROM tblSeasonDetails sd, tblProductDetails pd WHERE  sd.SeasonYear='" + SeasonYear + "' AND pd.ProductId='" + productID + "' AND sd.ProductId='" + productID + "' AND sd.Result=1 AND sd.m_SeasonID ='" + seasonID + "') LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID = 0 LEFT JOIN tblFarmerFarmDetails FFDG ON fd.FarmerId = FFDG.FarmerId and ffdg.productID='" + productID + "' and ffdg.ParentFarmID  = ffd.FarmID and ffdg.FYear='" + SeasonYear + "' WHERE fd.InternalInspectorApproval= 1 and fd.ICSType='" + ICStype + "' and fd.[Delete]=0 GROUP BY ffd.AreaCode,ffd.farmid,fd.FarmerCode,fd.FirstName,fd.FarmerId,fd.TotalAreaInHectares,fd.City_Village,fd.Organic,fd.InternalInspectorApproval,ffd.AreaCode,ffd.PlotArea,ffdg.PlotArea order by ffd.FarmID asc");
            //old ocde
            //return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ISNULL(ffd.AreaCode,0) as PlotCode,ISNULL(ffd.PlotArea,0)as cropareaISNULL((fd.TotalAreaInHectares - ffd.PlotArea),0) as Availablearea,fd.City_Village,fd.Organic,fd.InternalInspectorApproval FROM tblFarmerDetails fd INNER JOIN tblSeasonDetails sd ON fd.FarmerId in (SELECT sd.FarmerID FROM tblSeasonDetails sd, tblProductDetails pd WHERE  sd.SeasonYear='" + SeasonYear + "' AND pd.ProductId='" + productID + "' AND sd.ProductId='" + productID + "' AND sd.Result=1 AND sd.m_SeasonID ='" + seasonID + "') LEFT JOIN tblFarmerFarmDetails ffd ON fd.FarmerId = ffd.FarmerId and ffd.ParentFarmID! = 0 WHERE fd.InternalInspectorApproval= 1 and fd.[Delete]=0 GROUP BY fd.FarmerId,fd.FarmerCode,fd.FirstName,fd.TotalAreaInHectares,fd.City_Village,fd.Organic,fd.InternalInspectorApproval,ffd.PlotArea");
        }
        public static DataTable GetFarmerlistVillagewise(string Village)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblFarmerDetails where City_Village='" + Village + "'  AND InternalInspectorApproval = 1 AND [Delete] = 0 ");
        }
        public static DataTable GetFarmerFarmDetails(string FarmerID, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select FarmID,FarmerID,AreaCode,PlotArea,pd.ProductName as Maincrop,ffd.IsInterCrop,pd.ProductId from tblFarmerFarmDetails ffd, tblProductDetails pd where ffd.ParentFarmID > 0 and ffd.FarmerID='" + FarmerID + "' and FYear='" + Year + "' and ffd.productID=pd.ProductId order by areacode");
        }
        //Report for farmer Details
        public static DataTable GetFarmerDetails(string Village)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (Village != "All")
                return mdbh.ExecuteDataTable("(select farmer.FirstName,farmer.City_Village,farmer.TotalAreaInHectares,farmer.Organic,farmer.FarmerCode,farmer.[Presidnt],farmer.FarmerId,farmer.CreatedDate,farmer.NumberOfPlots from tblFarmerDetails farmer where farmer.[Delete]=0 and farmer.City_Village='" + Village + "') union (select '' as FirstName, '' as City_Village, farm.PlotArea as TotalAreaInHectares,farmer.Organic,farm.areacode,farmer.[Presidnt],farm.FarmerID,farmer.CreatedDate,farmer.NumberOfPlots from tblFarmerFarmDetails farm join tblFarmerDetails farmer on farm.FarmerID=farmer.FarmerId where farmer.City_Village='" + Village + "' and farm.[Delete]=0 and farmer.[Delete]=0 and farm.ParentFarmID=0) order by CreatedDate,FarmerCode asc");
            else
                return mdbh.ExecuteDataTable("(select farmer.FirstName,farmer.City_Village,farmer.TotalAreaInHectares,farmer.Organic,farmer.FarmerCode,farmer.[Presidnt],farmer.FarmerId,farmer.CreatedDate,farmer.NumberOfPlots from tblFarmerDetails farmer where farmer.[Delete]=0 ) union (select '' as FirstName,'' as City_Village, farm.PlotArea as TotalAreaInHectares,farmer.Organic,farm.areacode,farmer.[Presidnt],farm.FarmerID,farmer.CreatedDate,farmer.NumberOfPlots from tblFarmerFarmDetails farm join tblFarmerDetails farmer on farm.FarmerID=farmer.FarmerId where  farm.[Delete]=0 and farmer.[Delete]=0 and farm.ParentFarmID=0) order by CreatedDate,FarmerCode asc");
        }
        public static DataTable GetICSCodes()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select Branchcode from tblBranchDetails where [delete]=0 and sales<>1 and Export=0 and WareHousing=0 and Other=0 order by BranchCode");
        }
        public static DataTable GetNewICSCodes()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select Branchcode from tblBranchDetails where [delete]=0 and WareHousing=1 order by BranchCode");
        }
        public static DataTable GetAllICSCodes()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select Branchcode from tblBranchDetails where [delete]=0 and Other=0 order by BranchCode");
        }
        public static DataTable GetFarmerVillageDistinct(string Type)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT DISTINCT(City_Village) FROM tblFarmerDetails where ICSType='"+Type+"'");
        }
        public static DataTable GetICSVillagelist(string ICS)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT DISTINCT(City_Village) FROM tblFarmerDetails where ICSType in (" + ICS + ")");
        }
        public static DataTable GetEstimationDetails(string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT distinct tblFarmerDetails.FarmerId,tblFarmerFarmDetails.PlotArea, tblFarmerFarmDetails.AreaCode, tblPlantationDetails.FirstHerbaga, tblPlantationDetails.FirstProductQuantity,"
                      + "tblPlantationDetails.SecondHerbaga, tblPlantationDetails.SecondProductQuantity, tblFarmerDetails.FirstName, tblFarmerDetails.FarmerAPEDACode,tblPlantationDetails.ProductId"
                      + " FROM tblFarmerFarmDetails LEFT OUTER JOIN "
                      + "tblFarmerDetails ON tblFarmerFarmDetails.FarmerID = tblFarmerDetails.FarmerId AND tblFarmerFarmDetails.FarmerID = tblFarmerDetails.FarmerId AND "
                      + "tblFarmerFarmDetails.FarmerID = tblFarmerDetails.FarmerId AND tblFarmerFarmDetails.FarmerID = tblFarmerDetails.FarmerId LEFT OUTER JOIN "
                      + "tblPlantationDetails ON tblFarmerFarmDetails.FarmID = tblPlantationDetails.FarmID AND tblFarmerFarmDetails.FarmID = tblPlantationDetails.FarmID AND "
                      + "tblFarmerFarmDetails.FarmID = tblPlantationDetails.FarmID AND tblFarmerFarmDetails.FarmID = tblPlantationDetails.FarmID AND "
                      + "tblFarmerDetails.FarmerId = tblPlantationDetails.FarmerId AND tblFarmerDetails.FarmerId = tblPlantationDetails.FarmerId AND "
                      + "tblFarmerDetails.FarmerId = tblPlantationDetails.FarmerId AND tblFarmerDetails.FarmerId = tblPlantationDetails.FarmerId where tblFarmerFarmDetails.FYear=2015 order by tblFarmerFarmDetails.AreaCode");

        }
    }
}
