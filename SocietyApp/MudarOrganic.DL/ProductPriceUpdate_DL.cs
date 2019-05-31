using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.Components;
using System.Data.SqlClient;
using System.Data;

namespace MudarOrganic.DL
{
    public static class ProductPriceUpdate_DL
    {
        public static bool ProductPriceDetailsINSandUPDandDEL(int PriceId, int ProductId, DateTime CreateDate, decimal PriceMB, decimal POPriceMB, decimal FOBPrice, decimal USA_Sea, decimal USA_Air, decimal Europe_Sea, decimal Europe_Air, decimal India_Price, decimal Non_organic_India, decimal Non_organic_USA, decimal USDollar, decimal OtherPrice, string createdBy, string modifiedBy, int TypeOfOperation, ref int ReturnPriceHistoryID)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PriceId", SqlDbType.Int,PriceId));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("CreateDate", SqlDbType.DateTime, CreateDate));
            Params.Add(mdbh.AddParameter("PriceMB", SqlDbType.Money, PriceMB));
            Params.Add(mdbh.AddParameter("POPriceMB", SqlDbType.Money, POPriceMB));
            Params.Add(mdbh.AddParameter("FOBPrice", SqlDbType.Money, FOBPrice));
            Params.Add(mdbh.AddParameter("USA_Sea", SqlDbType.Money, USA_Sea));
            Params.Add(mdbh.AddParameter("USA_Air", SqlDbType.Money, USA_Air));
            Params.Add(mdbh.AddParameter("Europe_Sea", SqlDbType.Money, Europe_Sea));
            Params.Add(mdbh.AddParameter("Europe_Air", SqlDbType.Money, Europe_Air));
            Params.Add(mdbh.AddParameter("India_Price", SqlDbType.Money, India_Price));
            Params.Add(mdbh.AddParameter("Non_organic_India", SqlDbType.Money, Non_organic_India));
            Params.Add(mdbh.AddParameter("Non_organic_USA", SqlDbType.Money, Non_organic_USA));
            Params.Add(mdbh.AddParameter("USDollar", SqlDbType.Money, USDollar));
            Params.Add(mdbh.AddParameter("OtherPrice", SqlDbType.Money, OtherPrice));
            Params.Add(mdbh.AddParameter("createdBy", SqlDbType.NVarChar, createdBy));
            Params.Add(mdbh.AddParameter("modifiedBy", SqlDbType.NVarChar, modifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ReturnPriceHistoryID", SqlDbType.Int, ReturnPriceHistoryID, Param_Directions.Param_Out));
            //try
            //{
            //    output = (bool)mdbh.ExecuteNonQuery(sp.sp_ProductPriceDetails_INSandUPDandDEL, Params);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return true;
        }
        
    }
}
