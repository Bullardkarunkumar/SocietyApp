using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace MudarOrganic.BL
{
    public class Supplier_BL
    {
        public bool SupplierDetails_INSandUPDandDEL(string SupplierId, string SupplierCompanyName, string CAddress, string CCity, string CState, string CPincode, string CCountry, string CContactPerson, string CContactPhoneNo, string Email, string Website, string MobileNumber, string BankName, string BankAddress, string BankCity, string BankState, string BankPincode, string BankCountry, string CreatedBy, string ModifiedBy, string TINNumber, string VAT, string CST, int TypeOfOperation, string icsCodes)
        {
            return Supplier_DL.SupplierDetails_INSandUPDandDEL(SupplierId, SupplierCompanyName, CAddress, CCity, CState, CPincode, CCountry, CContactPerson, CContactPhoneNo, Email, Website, MobileNumber, BankName, BankAddress, BankCity, BankState, BankPincode, BankCountry, CreatedBy, ModifiedBy, TINNumber, VAT, CST, TypeOfOperation, icsCodes);
        }
        public bool SupplierPriceandPaymentDetails_INSandUPDandDEL(string SupplierId, bool Exworks, bool ExSuppliersPlace, bool ForDestination, string PaymentTerm, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Supplier_DL.SupplierPriceandPaymentDetails_INSandUPDandDEL(SupplierId, Exworks, ExSuppliersPlace, ForDestination, PaymentTerm, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable GetSupplierDetails()
        {
            return Supplier_DL.GetSupplierDetails();
        }
        public DataTable GetSupplierDetails(string SupplierId)
        {
            return Supplier_DL.GetSupplierDetails(SupplierId);
        }
        public DataTable GetIcsandNonSuppliers(bool ics)
        {
            return Supplier_DL.GetIcsandNonSuppliers(ics);
        }
        public string GetBranches()
        {
            var dt = Supplier_DL.GetBranches();
            var strbld = new StringBuilder();
            var result = JsonConvert.SerializeObject(dt, Formatting.Indented);            
            return result;
        }
    }
}
