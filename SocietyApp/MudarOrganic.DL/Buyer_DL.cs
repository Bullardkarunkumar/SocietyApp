using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class Buyer_DL
    {
        public static bool BuyerDetails_INSandUPDandDEL(string BuyerID, string BuyerCompanyName, string CAddress, string CCity, string CState, string CPincode, string CCountry, string CContactPerson, string CContactPhoneNo, string MobileNoTesting, string Email, string Website, string NotifyName, string NAddress, string NCity, string NState, string NPincode, string NCountry, string BankName, string BankAddress, string BankCity, string BankState, string BankPincode, string BankCountry, string CreatedBy, string ModifiedBy, string TINNumber, string VAT, string CST, int TypeOfOperation, int bankorconsignee)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("BuyerCompanyName", SqlDbType.NVarChar,BuyerCompanyName) );
            Params.Add(mdbh.AddParameter("CAddress", SqlDbType.NVarChar,CAddress));
            Params.Add(mdbh.AddParameter("CCity", SqlDbType.NVarChar,CCity));
            Params.Add(mdbh.AddParameter("CState", SqlDbType.NVarChar, CState));
            Params.Add(mdbh.AddParameter("CPincode", SqlDbType.NVarChar, CPincode));
            Params.Add(mdbh.AddParameter("CCountry", SqlDbType.NVarChar, CCountry));
            Params.Add(mdbh.AddParameter("ContactPerson", SqlDbType.NVarChar, CContactPerson));
            Params.Add(mdbh.AddParameter("ContactPhoneNo", SqlDbType.NVarChar, CContactPhoneNo));
            Params.Add(mdbh.AddParameter("MobileNoTestingpurpose", SqlDbType.NVarChar, MobileNoTesting));
            Params.Add(mdbh.AddParameter("email", SqlDbType.NVarChar, Email));
            Params.Add(mdbh.AddParameter("website", SqlDbType.NVarChar, Website));
            Params.Add(mdbh.AddParameter("NotifyName", SqlDbType.NVarChar, NotifyName));
            Params.Add(mdbh.AddParameter("NAddress", SqlDbType.NVarChar,NAddress));
            Params.Add(mdbh.AddParameter("NCity", SqlDbType.NVarChar,NCity));
            Params.Add(mdbh.AddParameter("NCountry", SqlDbType.NVarChar,NCountry));
            Params.Add(mdbh.AddParameter("NPincode", SqlDbType.NVarChar,NPincode));
            Params.Add(mdbh.AddParameter("NState", SqlDbType.NVarChar, NState));
            Params.Add(mdbh.AddParameter("BankName", SqlDbType.NVarChar,BankName));
            Params.Add(mdbh.AddParameter("BankAddress", SqlDbType.NVarChar,BankAddress));
            Params.Add(mdbh.AddParameter("BankCity", SqlDbType.NVarChar, BankCity));
            Params.Add(mdbh.AddParameter("BankState", SqlDbType.NVarChar, BankState));
            Params.Add(mdbh.AddParameter("BankPincode", SqlDbType.NVarChar,BankPincode));
            Params.Add(mdbh.AddParameter("BankCountry", SqlDbType.NVarChar, BankCountry));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TINNumber", SqlDbType.NVarChar, TINNumber));
            Params.Add(mdbh.AddParameter("VAT", SqlDbType.NVarChar, VAT));
            Params.Add(mdbh.AddParameter("CST", SqlDbType.NVarChar, CST));
            Params.Add(mdbh.AddParameter("bankorconsignee", SqlDbType.Int, bankorconsignee));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));

            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_BuyerDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool BuyerTransPortDetails_INSandUPDandDEL(string BuyerID, int Transportmode, string SeaportName, string AirportName, string RoadDestination, string RailStation, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("ModeofTransport", SqlDbType.Int, Transportmode));
            Params.Add(mdbh.AddParameter("SeaportName", SqlDbType.NVarChar, SeaportName));
            Params.Add(mdbh.AddParameter("AirportName", SqlDbType.NVarChar, AirportName));
            Params.Add(mdbh.AddParameter("RoadDestination", SqlDbType.NVarChar, RoadDestination));
            Params.Add(mdbh.AddParameter("RailStation", SqlDbType.NVarChar, RailStation));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_BuyerTransportDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool BuyerPriceTermsDetails_INSandUPDandDEL(string BuyerID, bool FOB_India, bool CNF_Sea_By, bool CNF_AIR_By_EuropeandEastUSA, bool CNF_AIR_By_WEST_USA, bool CIF_Sea_By, bool CIF_Air_By_EuropeandEastUSA, bool CIF_AIR_By_WEST_USA, bool FORDestination, bool Exworks, bool advance100, bool Fiftyadv50againstDocs, bool HundredagainstDocs, bool NoofDaysfromInvoice, int No_of_Days_Count_fromInvoice, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("FOB_India", SqlDbType.Bit, FOB_India));
            Params.Add(mdbh.AddParameter("CNF_Sea_By", SqlDbType.Bit, CNF_Sea_By));
            Params.Add(mdbh.AddParameter("CNF_AIR_By_EuropeandEastUSA", SqlDbType.Bit, CNF_AIR_By_EuropeandEastUSA));
            Params.Add(mdbh.AddParameter("CNF_AIR_By_WEST_USA", SqlDbType.Bit, CNF_AIR_By_WEST_USA));
            Params.Add(mdbh.AddParameter("CIF_Sea_By", SqlDbType.Bit, CIF_Sea_By));
            Params.Add(mdbh.AddParameter("CIF_Air_By_EuropeandEastUSA", SqlDbType.Bit, CIF_Air_By_EuropeandEastUSA));
            Params.Add(mdbh.AddParameter("CIF_AIR_By_WEST_USA", SqlDbType.Bit, CIF_AIR_By_WEST_USA));
            Params.Add(mdbh.AddParameter("FORDestination", SqlDbType.Bit, FORDestination));
            Params.Add(mdbh.AddParameter("Exworks", SqlDbType.Bit, Exworks));
            Params.Add(mdbh.AddParameter("100advance", SqlDbType.Bit, advance100));
            Params.Add(mdbh.AddParameter("50adv50againstDocs", SqlDbType.Bit, Fiftyadv50againstDocs));
            Params.Add(mdbh.AddParameter("100againstDocs", SqlDbType.Bit, HundredagainstDocs));
            Params.Add(mdbh.AddParameter("NoofDaysfromInvoice", SqlDbType.Bit, NoofDaysfromInvoice));
            Params.Add(mdbh.AddParameter("No_of_Days_Count_fromInvoice", SqlDbType.Int, No_of_Days_Count_fromInvoice));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_BuyerPriceTermsDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public static DataTable BuyerDetails(string BuyerId)
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT bd.*, bp.*, bt.* FROM dbo.tblBuyerDetails bd LEFT JOIN dbo.tblBuyerPriceTermsDetails bp ON bd.BuyerId = bp.BuyerId LEFT JOIN dbo.tblBuyerTransportDetails bt ON bd.BuyerId = bt.BuyerId WHERE bd.[Delete] = 0 	AND bd.BuyerId = '" + BuyerId + "'");
        }
        public static DataTable BuyerDetails(int OrderId)
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT bd.*, bp.*, bt.* FROM dbo.tblBuyerDetails bd LEFT JOIN dbo.tblBuyerPriceTermsDetails bp ON bd.BuyerId = bp.BuyerId LEFT JOIN dbo.tblBuyerTransportDetails bt ON bd.BuyerId = bt.BuyerId LEFT JOIN dbo.tblOrderDetails od ON od.BuyerID = bd.BuyerId  WHERE bd.[Delete] = 0 	AND od.OrderID = " + OrderId);
        }
        public static DataTable BuyerDetails()
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT bd.*, ul.UserLoginID, ul.UserPassword FROM dbo.tblBuyerDetails bd LEFT JOIN dbo.tblUserLogin ul ON bd.BuyerId = ul.UserId WHERE bd.[Delete] = 0");
        }
        public static DataTable BuyerDetails(bool Approval)
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            string value = Approval.ToString().ToLower() == "true" ? "1 ORDER BY BD.ModifiedDate   DESC " : "0 ORDER BY BD.CREATEDDATE  DESC ";
            return mbdh.ExecuteDataTable("SELECT bd.*, ul.UserLoginID, ul.UserPassword, ISNULL( bpt.FOB_India,0) AS 'FOB_India', ISNULL( bpt.CIF_Sea_By,0) AS 'CIF_Sea_By', ISNULL( bpt.CIF_Air_By_EuropeandEastUSA,0) AS 'CIF_Air_By_EuropeandEastUSA', ISNULL( bpt.CIF_AIR_By_WEST_USA, 0) AS 'CIF_AIR_By_WEST_USA',ISNULL( bpt.CIF_Seaport, 0) AS 'CIF_Seaport'   FROM dbo.tblBuyerDetails bd LEFT JOIN dbo.tblUserLogin ul ON bd.BuyerId = ul.UserId LEFT JOIN dbo.tblBuyerPriceTermsDetails bpt ON bd.BuyerId = bpt.BuyerId  WHERE bd.[Delete] = 0 AND Apporval = " + value);
        }
        public static bool BuyerApproval(string BuyerID, string ModifiedBy, bool Approval, bool Lotsample,string BCode,decimal Discount, decimal FairTrade, decimal FairTradPremium, int valueType)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("Approval", SqlDbType.Bit, Approval));
            Params.Add(mdbh.AddParameter("Lotsample", SqlDbType.Bit, Lotsample));
            Params.Add(mdbh.AddParameter("BCode", SqlDbType.NVarChar, BCode));
            Params.Add(mdbh.AddParameter("Discount", SqlDbType.Decimal, Discount));
            Params.Add(mdbh.AddParameter("FairTrade", SqlDbType.Decimal, FairTrade));
            Params.Add(mdbh.AddParameter("FairTradPremium", SqlDbType.Decimal, FairTradPremium));
            Params.Add(mdbh.AddParameter("valueType", SqlDbType.Int, valueType));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_CheckBuyerApporval, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool BuyerPaymentandPriceUpdateDetails(string BuyerID, bool advance100, bool Fiftyadv50againstDocs, bool HundredagainstDocs, bool NoofDaysfromInvoice, int No_of_Days_Count_fromInvoice, int TypeOfOperation, string ModifiedBy)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("100advance", SqlDbType.Bit, advance100));
            Params.Add(mdbh.AddParameter("50adv50againstDocs", SqlDbType.Bit, Fiftyadv50againstDocs));
            Params.Add(mdbh.AddParameter("100againstDocs", SqlDbType.Bit, HundredagainstDocs));
            Params.Add(mdbh.AddParameter("NoofDaysfromInvoice", SqlDbType.Bit, NoofDaysfromInvoice));
            Params.Add(mdbh.AddParameter("No_of_Days_Count_fromInvoice", SqlDbType.Int, No_of_Days_Count_fromInvoice));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_UPD_Buyer_PaymentandPriceandTransport_Details, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool BuyerProductInsertDetails(string BuyerID, int ProductID, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_BuyerProducts_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool BuyerComplaintInsertDetails(string complaint, string ComplaintBy, string BuyerID, string InvoiceID, int InvoiceProductID, DateTime InvoiceDate, decimal ProductQty, string BatchNo, string Action, string CreatedBy, string ModifiedBy, int TypeOfOperation, int ComplaintType)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Complaint", SqlDbType.NVarChar, complaint));
            Params.Add(mdbh.AddParameter("ComplaintBy", SqlDbType.NVarChar, ComplaintBy));
            Params.Add(mdbh.AddParameter("BuyerId", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("InvoiceId", SqlDbType.NVarChar, InvoiceID));
            Params.Add(mdbh.AddParameter("InvoiceProductID", SqlDbType.Int, InvoiceProductID));
            Params.Add(mdbh.AddParameter("InvoiceDate", SqlDbType.DateTime, InvoiceDate));
            Params.Add(mdbh.AddParameter("ProductQuantity", SqlDbType.Decimal, ProductQty));
            Params.Add(mdbh.AddParameter("BatchNo", SqlDbType.NVarChar, BatchNo));
            Params.Add(mdbh.AddParameter("Action", SqlDbType.NVarChar, Action));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ComplaintType", SqlDbType.Int, ComplaintType));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_BuyerComplaintDetails_INS_UPD_DEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetBuyerComplaintDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bd.* ,pd.ProductName from tblBuyerComplaints bd,tblProductDetails pd where bd.[Delete]=0 AND bd.InvoiceProductID = pd.ProductId");
        }
        public static DataTable GetBuyerComplaintDetails(int ComplaintType, string userid)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bd.* ,pd.ProductName from tblBuyerComplaints bd,tblProductDetails pd where bd.[Delete]=0 AND bd.InvoiceProductID = pd.ProductId and bd.BuyerId='" + userid + "' AND bd.ComplaintType=" + ComplaintType);
        
        }
        public static DataTable GetBuyerComplaintDetails(int ComplaintID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bd.* ,pd.ProductName from tblBuyerComplaints bd,tblProductDetails pd where bd.ComplaintID='" + ComplaintID + "' AND bd.[Delete]=0 AND bd.InvoiceProductID = pd.ProductId");
        }
        public static DataTable GetBuyerLoginDetails(string BuyerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblUserLogin where UserId='"+BuyerID+"' AND [Delete]=0");
        }
        public static DataTable GetBuyerProductDetails(string BuyerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bpd.*,pd.ProductName,pd.Specification from tblBuyerProductDetails bpd,tblProductDetails pd where bpd.BuyerId='" + BuyerID + "' AND bpd.ProductId = pd.ProductId AND bpd.[Delete]=0");
        }
		public static DataTable BuyerProductList(string BuyerID)
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT * FROM tblbuyerProductDetails bp, tblProductDetails pd WHERE bp.ProductID = pd.ProductId AND bp.BuyerID = '" + BuyerID + "'");
        }
        public static bool DeletetheBuyer(string BuyerID)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_DeleteBuyer, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        public static bool UpdateBuyerPOandPathDetails(int OrderID, string PurchaseOrderID, string Buyer_path, string Comments)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("PurchaseOrderID", SqlDbType.NVarChar, PurchaseOrderID));
            Params.Add(mdbh.AddParameter("Buyer_path", SqlDbType.NVarChar, Buyer_path));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_upd_BuyerPOandPathDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
