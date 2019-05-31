using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public class Settings_DL
    {
        public static DataTable GetFinicalYear()
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sp.sp_GetFinicalYear, "FYear");
        }
        public static DataTable GetLotYear(DateTime Date)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Date", SqlDbType.DateTime, Date));
            return mdbh.ExecuteDataTable(sp.sp_Get_Lotnumber_Year, Params, "FYear");
        }
        public static DataTable Get_NewProduction_Year(DateTime Date, string Year)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Date", SqlDbType.DateTime, Date));
            Params.Add(mdbh.AddParameter("year", SqlDbType.VarChar, Year));
            return mdbh.ExecuteDataTable(sp.sp_Get_NewProduction_Year, Params, "FYear");
        }
       
        public static DataTable GetProductionYear(DateTime date)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Date", SqlDbType.SmallDateTime, date));
            return mdbh.ExecuteDataTable(sp.sp_GetProductionYear, Params, "PYear");
        }
        public static bool StandDetails_INSandUPDandDEL(int StandID, int Year, int ProductID, DateTime Date, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("StandID", SqlDbType.Int, StandID));
            Params.Add(mdbh.AddParameter("Year", SqlDbType.Int, Year));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("Date", SqlDbType.DateTime, Date));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_StandDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool MentholPercentageDetailsINS(int PerID, int Year, int ProductID, decimal Percentage, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PerID", SqlDbType.Int, PerID));
            Params.Add(mdbh.AddParameter("Year", SqlDbType.Int, Year));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("Percentage", SqlDbType.Decimal, Percentage));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_MentholPerDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetStandDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT pd.ProductCode,pd.ProductName,sd.* FROM tblStandDetails sd,tblProductDetails pd WHERE sd.ProductID = pd.ProductId and sd.[Delete]= 0");
        }
        public static DataTable GetMentholPerDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select mp.*,pd.ProductName from tblMentholPer mp,tblProductDetails pd where pd.ProductId=mp.ProductId and pd.[Delete] = 0 and mp.[Delete] = 0");
        }
        public static DataTable GetMentholPerDetails(string PerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select mp.*,pd.ProductName from tblMentholPer mp,tblProductDetails pd where PerID='"+PerID+"' and pd.ProductId=mp.ProductId and pd.[Delete] = 0 and mp.[Delete] = 0");
        }
        public static DataTable GetMentholPerDetails(int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select mp.*,pd.ProductName from tblMentholPer mp,tblProductDetails pd where pd.ProductId='" + ProductID + "'and mp.ProductId='" + ProductID + "' and pd.[Delete] = 0 and mp.[Delete] = 0");
        }
        public static DataTable GetStandDetails(string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT pd.ProductCode,pd.ProductName,sd.* FROM tblStandDetails sd,tblProductDetails pd WHERE sd.ProductID='"+ProductID+"' AND pd.ProductId='"+ProductID+"' and sd.[Delete]= 0");
        }
        public static DataTable GetStandDetails(string ProductID,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT pd.ProductCode,pd.ProductName,sd.* FROM tblStandDetails sd,tblProductDetails pd WHERE sd.ProductID='" + ProductID + "' AND pd.ProductId='" + ProductID + "' and sd.[Year]='"+Year+"' and sd.[Delete]= 0");
        }
        public static DataTable GetStandDetails(int StandID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblStandDetails WHERE StandID='" + StandID + "' AND [Delete]= 0");
        }
        public static DataTable GetProductDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblProductDetails WHERE [Delete]= 0");
        }
        public static DataTable GetStandardProductDetails(string productID,string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT pd.ProductCode,pd.ProductName,sd.* FROM tblStandDetails sd,tblProductDetails pd WHERE sd.ProductID='" + productID + "' and sd.[Year]='" + Year + "' and pd.ProductId='" + productID + "' and sd.[Delete]= 0");
        }
    }
}
