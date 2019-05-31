using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MudarOrganic.Components;


namespace MudarOrganic.DL
{
    public static class Supplier_DL
    {
        public static bool SupplierDetails_INSandUPDandDEL(string SupplierId, string SupplierCompanyName, string CAddress, string CCity, string CState, string CPincode, string CCountry, string CContactPerson, string CContactPhoneNo, string Email, string Website, string MobileNumber, string BankName, string BankAddress, string BankCity, string BankState, string BankPincode, string BankCountry, string CreatedBy, string ModifiedBy, string TINNumber, string VAT, string CST, int TypeOfOperation, string icsCodes)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SupplierId", SqlDbType.UniqueIdentifier, new Guid(SupplierId)));
            Params.Add(mdbh.AddParameter("SupplierCompanyName", SqlDbType.NVarChar, SupplierCompanyName));
            Params.Add(mdbh.AddParameter("CAddress", SqlDbType.NVarChar, CAddress));
            Params.Add(mdbh.AddParameter("CCity", SqlDbType.NVarChar, CCity));
            Params.Add(mdbh.AddParameter("CState", SqlDbType.NVarChar, CState));
            Params.Add(mdbh.AddParameter("CPincode", SqlDbType.NVarChar, CPincode));
            Params.Add(mdbh.AddParameter("CCountry", SqlDbType.NVarChar, CCountry));
            Params.Add(mdbh.AddParameter("CContactPerson", SqlDbType.NVarChar, CContactPerson));
            Params.Add(mdbh.AddParameter("CContactPhoneNo", SqlDbType.NVarChar, CContactPhoneNo));
            Params.Add(mdbh.AddParameter("email", SqlDbType.NVarChar, Email));
            Params.Add(mdbh.AddParameter("website", SqlDbType.NVarChar, Website));
            Params.Add(mdbh.AddParameter("MobileNumber", SqlDbType.NVarChar, MobileNumber));
            Params.Add(mdbh.AddParameter("BankName", SqlDbType.NVarChar, BankName));
            Params.Add(mdbh.AddParameter("BankAddress", SqlDbType.NVarChar, BankAddress));
            Params.Add(mdbh.AddParameter("BankCity", SqlDbType.NVarChar, BankCity));
            Params.Add(mdbh.AddParameter("BankState", SqlDbType.NVarChar, BankState));
            Params.Add(mdbh.AddParameter("BankPincode", SqlDbType.NVarChar, BankPincode));
            Params.Add(mdbh.AddParameter("BankCountry", SqlDbType.NVarChar, BankCountry));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TINNumber", SqlDbType.NVarChar, TINNumber));
            Params.Add(mdbh.AddParameter("VAT", SqlDbType.NVarChar, VAT));
            Params.Add(mdbh.AddParameter("CST", SqlDbType.NVarChar, CST));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("SupplierType", SqlDbType.VarChar, icsCodes));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_SupplierDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DataTable GetIcsandNonSuppliers(bool ics)
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            if (ics)
                return mbdh.ExecuteDataTable("select SupplierId,SupplierCompanyName from tblSupplierDetails where suppliertype<>''");
            else
                return mbdh.ExecuteDataTable("select SupplierId,SupplierCompanyName from tblSupplierDetails where suppliertype=''");
        }


        public static DataTable GetBranches()
        {
            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("select BranchId,Bname from tblbranchdetails where other=1");
        }
        public static bool SupplierPriceandPaymentDetails_INSandUPDandDEL(string SupplierId, bool Exworks, bool ExSuppliersPlace, bool ForDestination, string PaymentTerm, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SupplierId", SqlDbType.UniqueIdentifier, new Guid(SupplierId)));
            Params.Add(mdbh.AddParameter("Exworks", SqlDbType.Bit, Exworks));
            Params.Add(mdbh.AddParameter("ExSuppliersPlace", SqlDbType.Bit, ExSuppliersPlace));
            Params.Add(mdbh.AddParameter("ForDestination", SqlDbType.Bit, ForDestination));
            Params.Add(mdbh.AddParameter("PaymentTerm", SqlDbType.NVarChar, PaymentTerm));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_SupplierPriceandPaymentTermsDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetSupplierDetails()
        {

            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT sd.*,sppt.* FROM dbo.tblSupplierDetails sd LEFT JOIN dbo.tblSupplierPriceandPaymentTermsDetails sppt ON sd.SupplierId = sppt.SupplierId WHERE sd.[Delete] = 0 ORDER BY sd.CreatedDate DESC");
        }
        public static DataTable GetSupplierDetails(string SupplierId)
        {

            MudarDBHelper mbdh = MudarDBHelper.Instance;
            return mbdh.ExecuteDataTable("SELECT sd.*,sppt.* FROM dbo.tblSupplierDetails sd LEFT JOIN dbo.tblSupplierPriceandPaymentTermsDetails sppt ON sd.SupplierId = sppt.SupplierId WHERE sd.[Delete] = 0 AND sd.SupplierId ='" + SupplierId + "'");
        }
    }
}
