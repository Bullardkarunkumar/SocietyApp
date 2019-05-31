using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class Invoice_DL
    {
        public static bool InvoiceInsert(int OrderId, int BranchOrderID, string PriceTerms, string Transport, string OriginCountry, string LoadingPort, string PaymentTerms, string FreightTerms, string DestinationCountry, string DestinationPort,DateTime OrderDate, string CreatedBy, string ModifiedBy, int TypeOfOperation, string InvoiceID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderId));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("PriceTerms", SqlDbType.NVarChar, PriceTerms));
            Params.Add(mdbh.AddParameter("Transport", SqlDbType.NVarChar, Transport));
            Params.Add(mdbh.AddParameter("OriginCountry", SqlDbType.NVarChar, OriginCountry));
            Params.Add(mdbh.AddParameter("LoadingPort", SqlDbType.NVarChar, LoadingPort));
            Params.Add(mdbh.AddParameter("PaymentTerms", SqlDbType.NVarChar, PaymentTerms));
            Params.Add(mdbh.AddParameter("FreightTerms", SqlDbType.NVarChar, FreightTerms));
            Params.Add(mdbh.AddParameter("DestinationCountry", SqlDbType.NVarChar, DestinationCountry));
            Params.Add(mdbh.AddParameter("DestinationPort", SqlDbType.NVarChar, DestinationPort));
            Params.Add(mdbh.AddParameter("OrderDate", SqlDbType.Date, OrderDate));
            Params.Add(mdbh.AddParameter("OrderDispachDate", SqlDbType.Date, DateTime.Now));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("InvoiceId", SqlDbType.NVarChar, InvoiceID));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_InvoiceDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public static bool InvoiceProductDetailsINSandUPDandDEL(string InvoiceId,int ProductId,decimal Netweight,decimal Grossweight,decimal PriceforKG,int TotalDrums,decimal TotalAmount,string CreatedBy, string ModifiedBy,int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("InvoiceId", SqlDbType.NVarChar, InvoiceId));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("Netweight", SqlDbType.Decimal, Netweight));
            Params.Add(mdbh.AddParameter("Grossweight", SqlDbType.Decimal, Grossweight));
            Params.Add(mdbh.AddParameter("PriceforKG", SqlDbType.Decimal, PriceforKG));
            Params.Add(mdbh.AddParameter("TotalDrums", SqlDbType.Int, TotalDrums));
            Params.Add(mdbh.AddParameter("TotalAmount", SqlDbType.Decimal, TotalAmount));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
             try
            {
                Result =(bool) mdbh.ExecuteNonQuery(sp.sp_InvoiceProductDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public static DataTable ReturnInvoiceList(string InvoiceID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblInVoices WHERE InvoiceId LIKE '" + InvoiceID + "%'");
        }
        public static DataTable InvoiceDetails(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblInVoices where OrderId='"+OrderID+"'");
        }
        public static DataTable GetProductsBasedonInvoice(string InvoiceID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT  ip.InvoiceId,ip.ProductId,convert(char(11),ip.CreatedDate,106) as InvDate,pd.ProductName FROM tblInvoiceProducts ip,tblProductDetails pd WHERE ip.InvoiceId = '" + InvoiceID + "' AND ip.ProductId = pd.ProductId");
        }
    }
}
