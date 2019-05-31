using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class Product_DL
    {
        public static bool Product_INT_UPT_DEL(int ProductID, string ProductCode, string ProductName, string Description, string ItcHsCode, int CategoryID, string CreatedBy, string ModifiedBy,  int typeOperation, string Specification)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("ProductCode", SqlDbType.NVarChar, ProductCode));
            Params.Add(mdbh.AddParameter("ProductName", SqlDbType.NVarChar, ProductName));
            Params.Add(mdbh.AddParameter("Description", SqlDbType.NVarChar, Description));
            Params.Add(mdbh.AddParameter("ItcHsCode", SqlDbType.NVarChar, ItcHsCode));
            Params.Add(mdbh.AddParameter("CategoryID", SqlDbType.Int, CategoryID));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            //Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("Specification", SqlDbType.NVarChar, Specification));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Product_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        

        public static List<int> GetSeasonProdIds(int seasonId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt= mdbh.ExecuteDataTable("select ProductId  from tblSeasonProducts where SeasonId='"+seasonId+"'");
            return dt.AsEnumerable().Select(m => m.Field<int>("ProductId")).ToList();
        }

        public static string GetProductName(int productId)
        {
            string productName = string.Empty;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select ProductName from tblProductDetails where ProductId=" + productId);
            if (dt.Rows.Count > 0)
            {
                productName = Convert.ToString(dt.Rows[0][0]);
            }
            return productName;
        }

        public static DataTable GetAllProducDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select pd.* from tblProductDetails pd where pd.[delete]=0");
        }
        public static DataTable GetProductDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE s.SeasonID = p.SeasonID AND c.CategoryId = p.CategoryId and p.[Delete] =0");
            //return mdbh.ExecuteDataTable("select pd.*,sp.SeasonId,ts.SeasonName,tc.CategoryName  from dbo.tblSeasonProducts sp join tblProductDetails pd on sp.ProductId = pd.ProductId join tblSeason ts on ts.SeasonID=sp.SeasonId join tblCategory tc on pd.CategoryId=tc.CategoryId");
            return mdbh.ExecuteDataTable("select pd.* from tblProductDetails pd where pd.[delete]=0");
        }

        public static DataTable GetProductDetailsNew()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE s.SeasonID = p.SeasonID AND c.CategoryId = p.CategoryId and p.[Delete] =0");
            return mdbh.ExecuteDataTable("select pd.*,tc.CategoryName  from tblProductDetails pd join tblCategory tc on pd.CategoryId=tc.CategoryId");
        }

        public static DataTable GetProductDetails(int seasonID,int CategoryID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE s.SeasonID = " + seasonID + " AND c.CategoryId = " + CategoryID + " and p.[Delete] =0");
        }

        public static DataTable GetProductDetails(string startDate, string endDate, string ProductID)
        {
            //MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT tpd.ProductName,tpp.FOBPrice,tph.CreateDate FROM dbo.tblProductDetails tpd LEFT JOIN tblProductPrice tpp ON tpd.ProductId = tpp.ProductId LEFT JOIN tblPriceHistory tph ON tpp.PriceHistoryId = tph.PriceHistoryId where tpd.[Delete] = 0 AND tph.CreateDate BETWEEN '1/1/2012' AND '12/31/2012 23:59:59.978'");
            //return mdbh.ExecuteDataTable("SELECT convert(nvarchar(11), CreateDate,106) as 'CreateDate', [Demo Product]  ,  [Demo Product2]  ,  [Demo Product4] ,  [Demo Product5] FROM (SELECT tpd.ProductName, tph.CreateDate ,tpp.FOBPrice 	FROM dbo.tblProductDetails tpd 	LEFT JOIN tblProductPrice tpp ON tpd.ProductId = tpp.ProductId 	LEFT JOIN tblPriceHistory tph ON tpp.PriceHistoryId = tph.PriceHistoryId 	WHERE tpd.[Delete] = 0 		AND tph.CreateDate BETWEEN '1/1/2012' AND '12/31/2012 23:59:59.978') up PIVOT (sum(up.FOBPrice) for up.ProductName in (    [Demo Product]  ,  [Demo Product2]  ,  [Demo Product4]  ,  [Demo Product5]    )) AS pvt");
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("start", SqlDbType.NVarChar, startDate));
            Params.Add(mdbh.AddParameter("end", SqlDbType.NVarChar, endDate));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.NVarChar, ProductID));
            return mdbh.ExecuteDataTable(sp.sp_GetProduct_PriceDetails, Params, "Pricedetails");
        }
        public static DataTable GetProductDetails(int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE p.ProductId ="+ProductID+" and s.SeasonID = p.SeasonID	AND c.CategoryId = p.CategoryId and p.[Delete] =0");
            return mdbh.ExecuteDataTable("SELECT p.*, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE p.ProductId =" + ProductID + " AND c.CategoryId = p.CategoryId and p.[Delete] =0");
        }
        public static DataTable GetProductDetails(string Productvalue)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE (p.ProductName  LIKE '%" + Productvalue + "%' OR p.ProductId  LIKE '%" + Productvalue + "%') AND s.SeasonID = p.SeasonID	AND c.CategoryId = p.CategoryId AND p.[Delete] =0");
        }
        public static DataTable GetProductDetailsbySeason(int seasonid)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, s.StartDate, s.EndDate, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE s.SeasonID =" + seasonid + " and s.SeasonID = p.SeasonID	AND c.CategoryId = p.CategoryId and p.[Delete] =0");
        }

        public static DataTable GetProductDetailsbySeasonNew(int seasonid)
        {
            //MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT p.*, s.SeasonName, s.StartDate, s.EndDate, c.CategoryName From dbo.tblProductDetails p,dbo.tblSeason s, dbo.tblCategory c WHERE s.SeasonID =" + seasonid + " and s.SeasonID = p.SeasonID	AND c.CategoryId = p.CategoryId and p.[Delete] =0");
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("seasonid", SqlDbType.Int, seasonid));
            return mdbh.ExecuteDataTable(sp.sp_GetProductDetailsBySeason, Params, "GetProducts");
        }

        // test run 
        public static DataTable GetProductDetailsbySeason(int seasonid,int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select p.*, s.SeasonName, s.StartDate, s.EndDate, c.CategoryName  from tblProductDetails p, tblSeason s,tblCategory c where p.SeasonID=" + seasonid + " and s.SeasonID=" + seasonid + " and s.SeasonYear=" + year + " and p.Pyear=" + year + " and c.CategoryId = p.CategoryId");
        }
        public static DataTable GetProductDetails(string FarmerID, string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT sd.*, pd.* FROM tblSeason sd, tblProductDetails pd WHERE  SeasonYear = '" + Year + "'  AND pd.SeasonID = sd.SeasonID ");
            //Aslam commented below code to fetch only product details
            //MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT sd.*, pd.ProductName FROM tblSeasonDetails sd, tblProductDetails pd WHERE FarmerID = '" + FarmerID + "' AND SeasonYear = '" + Year + "'  AND pd.ProductId = sd.ProductId ");
        }
        public static DataTable GetProductPrice(int TypeOfOperation)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            return mdbh.ExecuteDataTable(sp.sp_GetProductPrice, Params, "Pricedetails");
        }
        public static DataTable GetProductPrice()
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sp.sp_GetProductPrice);
        }

        public static DataTable GetProductPricebyName(string name)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Sql ="SELECT pd.ProductId,pd.ProductName, pd.Specification, pd.ItcHsCode, pp.* , ( FOBPrice*1.015) AS 'FOB_50A_50D', (FOBPrice*1.03) AS 'FOB_100AD',(FOBPrice *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'FOB_Day',(USA_Sea * 1.015) AS 'USA_SEA_50A_50D', (USA_Sea*1.03) AS 'USA_SEA_100AD',(USA_Sea *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Sea_Day',(USA_Air * 1.015) AS 'USA_Air_50A_50D', (USA_Air*1.03) AS 'USA_Air_100AD',(USA_Air *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_Day',(USA_Air_West * 1.015) AS 'USA_Air_West_50A_50D', (USA_Air_West*1.03) AS 'USA_Air_West_100AD',(USA_Air_West *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_West_Day',(Europe_Sea * 1.015) AS 'Europe_Sea_50A_50D', (Europe_Sea*1.03) AS 'Europe_Sea_100AD',(Europe_Sea *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Sea_Day',(Europe_Air * 1.015) AS 'Europe_Air_50A_50D', (Europe_Air*1.03) AS 'Europe_Air_100AD',(Europe_Air *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_Day',(Europe_Air_West * 1.015) AS 'Europe_Air_West_50A_50D', (Europe_Air_West*1.03) AS 'Europe_Air_West_100AD',(Europe_Air_West *1.03)*(1+(CAST( CAST(15 AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_West_Day'  FROM tblProductPrice pp, tblPriceHistory ph, tblProductDetails pd	WHERE pp.PriceHistoryId = ph.PriceHistoryId AND pd.ProductId = pp.ProductId AND ph.PriceHistoryId = (SELECT top 1 PriceHistoryId FROM tblPriceHistory ORDER BY CreateDate DESC)";
            if (name.ToLower().Trim() != "all")
                Sql += " AND pd.ProductName = '" + name + "'";
            return mdbh.ExecuteDataTable(Sql);
        }
        public static DataTable GetProductPricebyName(string name, string creditDay)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Sql = "SELECT pd.ProductId,pd.ProductName, pd.Specification, pd.ItcHsCode, pp.* ,(India_Price) AS 'IndiaPrice', (India_Price*(1+(1.5/100))) AS 'India_50A_50D', (India_Price * (1+(3.0/100))) AS 'India_100AD', ((India_Price * (1+(3.0/100)))*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)))  AS 'India_Day', ( FOBPrice*1.015) AS 'FOB_50A_50D', (FOBPrice*1.03) AS 'FOB_100AD',(FOBPrice *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'FOB_Day',(USA_Sea * 1.015) AS 'USA_SEA_50A_50D', (USA_Sea*1.03) AS 'USA_SEA_100AD',(USA_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Sea_Day',(USA_Air * 1.015) AS 'USA_Air_50A_50D', (USA_Air*1.03) AS 'USA_Air_100AD',(USA_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_Day',(USA_Air_West * 1.015) AS 'USA_Air_West_50A_50D', (USA_Air_West*1.03) AS 'USA_Air_West_100AD',(USA_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_West_Day',(Europe_Sea * 1.015) AS 'Europe_Sea_50A_50D', (Europe_Sea*1.03) AS 'Europe_Sea_100AD',(Europe_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Sea_Day',(Europe_Air * 1.015) AS 'Europe_Air_50A_50D', (Europe_Air*1.03) AS 'Europe_Air_100AD',(Europe_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_Day',(Europe_Air_West * 1.015) AS 'Europe_Air_West_50A_50D', (Europe_Air_West*1.03) AS 'Europe_Air_West_100AD',(Europe_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_West_Day'  FROM tblProductPrice pp, tblPriceHistory ph, tblProductDetails pd	WHERE pp.PriceHistoryId = ph.PriceHistoryId AND pd.ProductId = pp.ProductId AND ph.PriceHistoryId = (SELECT top 1 PriceHistoryId FROM tblPriceHistory ORDER BY CreateDate DESC)";
            if (name.ToLower().Trim() != "all")
                Sql += " AND pd.ProductName = '" + name + "'";
            return mdbh.ExecuteDataTable(Sql);
        }
        public static DataTable GetProductPricebyBuyer(string creditDay,string BuyerID)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //comment for .00 value and round off by one value
            //string Sql = "SELECT pd.ProductId,pd.ProductName, pd.Specification, pd.ItcHsCode, pp.* , (India_Price) AS 'IndiaPrice', (India_Price*(1+(1.5/100))) AS 'India_50A_50D', (India_Price * (1+(3.0/100))) AS 'India_100AD', ((India_Price * (1+(3.0/100)))*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)))  AS 'India_Day', ( FOBPrice*1.015) AS 'FOB_50A_50D', (FOBPrice*1.03) AS 'FOB_100AD',(FOBPrice *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'FOB_Day',(USA_Sea * 1.015) AS 'USA_SEA_50A_50D', (USA_Sea*1.03) AS 'USA_SEA_100AD',(USA_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Sea_Day',(USA_Air * 1.015) AS 'USA_Air_50A_50D', (USA_Air*1.03) AS 'USA_Air_100AD',(USA_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_Day',(USA_Air_West * 1.015) AS 'USA_Air_West_50A_50D', (USA_Air_West*1.03) AS 'USA_Air_West_100AD',(USA_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'USA_Air_West_Day',(Europe_Sea * 1.015) AS 'Europe_Sea_50A_50D', (Europe_Sea*1.03) AS 'Europe_Sea_100AD',(Europe_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Sea_Day',(Europe_Air * 1.015) AS 'Europe_Air_50A_50D', (Europe_Air*1.03) AS 'Europe_Air_100AD',(Europe_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_Day',(Europe_Air_West * 1.015) AS 'Europe_Air_West_50A_50D', (Europe_Air_West*1.03) AS 'Europe_Air_West_100AD',(Europe_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)) AS 'Europe_Air_West_Day'  FROM tblProductPrice pp, tblPriceHistory ph, tblProductDetails pd	INNER JOIN tblbuyerProductDetails bpp ON bpp.ProductID = pd.ProductId WHERE pp.PriceHistoryId = ph.PriceHistoryId AND pd.ProductId = pp.ProductId AND  bpp.BuyerID = '" + BuyerID + "' AND ph.PriceHistoryId = (SELECT top 1 PriceHistoryId FROM tblPriceHistory ORDER BY CreateDate DESC)";
            string Sql = "SELECT pd.ProductId,pd.ProductName, pd.Specification, pd.ItcHsCode, pp.* , (India_Price) AS 'IndiaPrice', CAST(CAST((India_Price*(1+(1.5/100)))AS numeric(10,1)) AS numeric(10,2)) AS 'India_50A_50D', CAST(CAST((India_Price * (1+(3.0/100)))AS numeric(10,1)) AS numeric(10,2)) AS 'India_100AD', CAST(CAST(((India_Price * (1+(3.0/100)))*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03)))AS numeric(10,1)) AS numeric(10,2))  AS 'India_Day', CAST(CAST(( FOBPrice*1.015)AS numeric(10,1)) AS numeric(10,2)) AS 'FOB_50A_50D', CAST(CAST((FOBPrice*1.03)AS numeric(10,1)) AS numeric(10,2)) AS 'FOB_100AD', CAST(CAST((FOBPrice *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'FOB_Day', CAST(CAST((USA_Sea * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'USA_SEA_50A_50D', CAST(CAST((USA_Sea*1.03)AS numeric(10,1)) AS numeric(10,2)) AS 'USA_SEA_100AD' , CAST(CAST((USA_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Sea_Day', CAST(CAST((USA_Air * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_50A_50D', CAST(CAST((USA_Air*1.03)AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_100AD', CAST(CAST((USA_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_Day', CAST(CAST((USA_Air_West * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_West_50A_50D', CAST(CAST((USA_Air_West*1.03)AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_West_100AD', CAST(CAST((USA_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'USA_Air_West_Day',  CAST(CAST((Europe_Sea * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Sea_50A_50D', CAST(CAST((Europe_Sea*1.03)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Sea_100AD', CAST(CAST((Europe_Sea *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Sea_Day', CAST(CAST((Europe_Air * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_50A_50D', CAST(CAST((Europe_Air*1.03)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_100AD', CAST(CAST((Europe_Air *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_Day', CAST(CAST((Europe_Air_West * 1.015)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_West_50A_50D', CAST(CAST((Europe_Air_West*1.03)AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_West_100AD', CAST(CAST((Europe_Air_West *1.03)*(1+(CAST( CAST(" + creditDay + " AS FLOAT)/CAST(30 AS FLOAT) AS FLOAT))*(0.03))AS numeric(10,1)) AS numeric(10,2))  AS 'Europe_Air_West_Day'  FROM tblProductPrice pp, tblPriceHistory ph, tblProductDetails pd	INNER JOIN tblbuyerProductDetails bpp ON bpp.ProductID = pd.ProductId WHERE pp.PriceHistoryId = ph.PriceHistoryId AND pd.ProductId = pp.ProductId AND  bpp.BuyerID = '" + BuyerID + "' AND ph.PriceHistoryId = (SELECT top 1 PriceHistoryId FROM tblPriceHistory ORDER BY CreateDate DESC)";
            return mdbh.ExecuteDataTable(Sql);
        }
        public static bool tblProductPrice_INSandUPD(DataTable dt, DateTime dtDate, double USD, double Transport, double Others, string CreatedBy, 
            string ModifiedBy, int TypeOfOperation, decimal MandyTax, decimal BMar, decimal insurance, decimal Localtax, decimal AddUpPrice)
        {
            bool Result = false;
            int ReturnHistoryId=0;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("CreateDate", SqlDbType.DateTime, dtDate));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("return", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("HistoryId", SqlDbType.Int, ReturnHistoryId, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblPriceHistory_INS, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    ReturnHistoryId = Convert.ToInt32(output[1]);
                }
                if (Result == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Result = false;
                        Params.Clear();

                        Params.Add(mdbh.AddParameter("PriceHistoryId", SqlDbType.Int, ReturnHistoryId));
                        Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, dt.Rows[i]["ProductId"].ToString()));
                        Params.Add(mdbh.AddParameter("PriceMB", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["PriceMB"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("POPriceMB", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["POPriceMB"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("FOBPrice", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["FOBPrice"].ToString()), 2, MidpointRounding.AwayFromZero)));
                        Params.Add(mdbh.AddParameter("USA_Sea", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["USA_Sea"].ToString()), 2, MidpointRounding.AwayFromZero)));
                        Params.Add(mdbh.AddParameter("USA_Air", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["USA_Air"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("USA_Air_West", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["USA_Air_West"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("Europe_Sea", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["Europe_Sea"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("Europe_Air", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["Europe_Air"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("Europe_Air_West", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["Europe_Air_West"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("India_Price", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["India_Price"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("Non_organic_India", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["Non_organic_India"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("Non_organic_USA", SqlDbType.Money, Math.Round(Convert.ToDecimal(dt.Rows[i]["Non_organic_USA"].ToString()), 2)));
                        Params.Add(mdbh.AddParameter("USDollar", SqlDbType.Money, USD));
                        Params.Add(mdbh.AddParameter("OtherPrice", SqlDbType.Money, Others));
                        Params.Add(mdbh.AddParameter("Transport", SqlDbType.Money, Transport));
                        Params.Add(mdbh.AddParameter("MandyTax", SqlDbType.Decimal, MandyTax));
                        Params.Add(mdbh.AddParameter("BMar", SqlDbType.Decimal, BMar));
                        Params.Add(mdbh.AddParameter("insurance", SqlDbType.Decimal, insurance));
                        Params.Add(mdbh.AddParameter("Localtax", SqlDbType.Decimal, Localtax));
                        Params.Add(mdbh.AddParameter("createdBy", SqlDbType.VarChar, CreatedBy));
                        Params.Add(mdbh.AddParameter("modifiedBy", SqlDbType.VarChar, ModifiedBy));
                        Params.Add(mdbh.AddParameter("typeOfOperation", SqlDbType.Int, TypeOfOperation));
                        Params.Add(mdbh.AddParameter("AddUPPrice", SqlDbType.Money, AddUpPrice));
                        Params.Add(mdbh.AddParameter("retunValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
                        try
                        {
                            Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblProductPrice_INSandUPD, Params);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

            return Result;
        }

        public static DataTable GetUpdateProductPrice(int TypeOfOperation)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            return mdbh.ExecuteDataTable(sp.sp_GetProductCalculationData, Params, "GetProductCalculation");
        }
        public static DataTable GetUpdateProductPrice(string date)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("date", SqlDbType.DateTime, date));
            return mdbh.ExecuteDataTable(sp.sp_GetProductCalculationDatabyDate, Params, "GetProductCalculation");
        }
        public static DataTable GetDetailsAllPriceswithDate(int typeofoperation, int Qty, DateTime createdDate)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("typeofoperation", SqlDbType.Int, typeofoperation));
            Params.Add(mdbh.AddParameter("Qty", SqlDbType.Int, Qty));
            Params.Add(mdbh.AddParameter("createdDate", SqlDbType.DateTime, createdDate));
            return mdbh.ExecuteDataTable(sp.sp_Get_Detailed_Pricedetails, Params, "GetDetailedAllPrices");
        }
        public static DataTable GetProductCode(int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Sql = "select ProductCode from tblProductDetails where ProductId='" + ProductID + "' and [Delete] = 0";
            return mdbh.ExecuteDataTable(Sql);
        }
        public static DataTable GetDetailsAllPrices(int typeofoperation, int Qty,int productID)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("typeofoperation", SqlDbType.Int, typeofoperation));
            Params.Add(mdbh.AddParameter("Qty", SqlDbType.Int, Qty));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, productID));
            return mdbh.ExecuteDataTable(sp.sp_sp_Get_New_PriceDetails, Params, "GetNewPrices");
        }
        public static DataTable GetDetailsAllPrices(int typeofoperation, int Qty, int productID, DateTime createdDate)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("typeofoperation", SqlDbType.Int, typeofoperation));
            Params.Add(mdbh.AddParameter("Qty", SqlDbType.Int, Qty));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, productID));
            Params.Add(mdbh.AddParameter("createdDate", SqlDbType.DateTime, createdDate));
            return mdbh.ExecuteDataTable(sp.sp_Get_New_PriceDetails_With_Date, Params, "GetNewPrices");
        }
        public static DataTable GetMaxHistoryIDSupplierProducts(string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string Sql = "select ProductId,PriceMB,CreatedDate from tblProductPrice pp where ProductId='" + ProductID + "' and pricehistoryid =(select MAX(PriceHistoryId) from tblProductPrice where ProductId='" + ProductID + "')";
            return mdbh.ExecuteDataTable(Sql);
        }
    }
}
