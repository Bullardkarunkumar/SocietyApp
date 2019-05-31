using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;
namespace MudarOrganic.DL
{
    public static class CategoryProduct_DL
    {
        #region Category
        public static bool Category_INT_UPT(int categoryID, string categoryname, string createdby, string modifiedby, int typeOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("CategoryID", SqlDbType.Int, categoryID));
            Params.Add(mdbh.AddParameter("CategoryName", SqlDbType.NVarChar, categoryname));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdby));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));
            Params.Add(mdbh.AddParameter("Delete", SqlDbType.Bit, false));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_CategoryDetails_INSandUPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }
        public static DataTable GetCategoryDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblCategory WHERE [Delete] = 0 ORDER BY CategoryId");
        }
        #endregion
        #region Season
        public static int Season_INT_UPT(int SeasonID, string Seasonname, DateTime StartDate, DateTime EndDate, string createdby, string modifiedby, int typeOperation, int SeasonYear)
        {
            int Result = 0;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonName", SqlDbType.NVarChar, Seasonname));
            Params.Add(mdbh.AddParameter("StartDate", SqlDbType.DateTime, StartDate));
            Params.Add(mdbh.AddParameter("EndDate", SqlDbType.DateTime, EndDate));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdby));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Int, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.Int, SeasonYear));
            try
            {
                Result = (int)mdbh.ExecuteNonQuery(sp.sp_season, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        public static bool SeasonProduct_INSandUPDandDEL(int ProductID, int SeasonId, int typeOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonId));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_SeasonProduct, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        public static DataTable SeasonDetails_FarmerSeasonProduct(Guid farmerId, int ProductID, int SeasonId, int seasonYear)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, farmerId));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonId));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.Int, seasonYear));
            try
            {
                return mdbh.ExecuteDataTable(sp.sp_SeasonDetails_FarmerSeasonProduct, Params, "FarmerSeasonProduct");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetSeasonDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblSeason WHERE [Delete] = 0");
        }
        public static DataTable GetSeasonDetailsBasedonYear(string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblSeason WHERE [Delete] = 0 AND SeasonYear = '" + Year + "' ");
        }
        public static DataTable GetSeasonDetailsBasedonFarmerandYear(Guid farmerId, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ProductId,m_SeasonID FROM dbo.tblSeasonDetails WHERE [Delete] = 0 AND FarmerId='" + farmerId + "' and SeasonYear = '" + Year + "' ");
        }
        public static DataTable GetSeasonDetails(int SeasonId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblSeason WHERE [Delete] = 0 AND SeasonId = " + SeasonId);
        }

        public static DataTable GetProductNameByFarmerandSeason(int seasonId, Guid farmerId, int seasonYear)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select pd.ProductName from tblSeasonDetails sd join tblProductDetails pd on sd.ProductId=pd.ProductId where sd.FarmerID='" + farmerId + "' and sd.[Delete]=0 and sd.SeasonYear=" + seasonYear + " and sd.m_SeasonID=" + seasonId);
        }

        public static DataTable GetSeasonDetails(string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (!string.IsNullOrEmpty(Year))
                return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblSeason WHERE [Delete] = 0 AND SeasonYear = " + Year);
            else
                return null;
        }


        public static int GetProductByName(string productName)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("SELECT ProductId FROM dbo.tblProductDetails WHERE [Delete] = 0 AND ProductName = '" + productName + "'");
            if (dt.Rows.Count>0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }

        public static int GetSeasonByName(string seasonName)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("SELECT SeasonID FROM dbo.tblSeason WHERE [Delete] = 0 AND SeasonName = '" + seasonName + "'");
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
