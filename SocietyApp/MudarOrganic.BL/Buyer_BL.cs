using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;


namespace MudarOrganic.BL
{
    public class Buyer_BL
    {
        public bool BuyerDetails_INSandUPDandDEL(string BuyerID, string BuyerCompanyName, string CAddress, string CCity, string CState, string CPincode, string CCountry, string CContactPerson, string CContactPhoneNo, string MobileNoTesting, string Email, string Website, string NotifyName, string NAddress, string NCity, string NState, string NPincode, string NCountry, string BankName, string BankAddress, string BankCity, string BankState, string BankPincode, string BankCountry, string CreatedBy, string ModifiedBy, string TINNumber, string VAT, string CST, int TypeOfOperation, int bankorconsignee)
        {
            return Buyer_DL.BuyerDetails_INSandUPDandDEL(BuyerID, BuyerCompanyName, CAddress, CCity, CState, CPincode, CCountry, CContactPerson, CContactPhoneNo, MobileNoTesting, Email, Website, NotifyName, NAddress, NCity, NState, NPincode, NCountry, BankName, BankAddress, BankCity, BankState, BankPincode, BankCountry, CreatedBy, ModifiedBy, TINNumber, VAT, CST, TypeOfOperation, bankorconsignee);
        }
        public bool BuyerTransPortDetails_INSandUPDandDEL(string BuyerID, int Transportmode, string SeaportName, string AirportName, string RoadDestination, string RailStation, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Buyer_DL.BuyerTransPortDetails_INSandUPDandDEL(BuyerID, Transportmode, SeaportName, AirportName, RoadDestination, RailStation, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public bool BuyerPriceTermsDetails_INSandUPDandDEL(string BuyerID, bool FOB_India, bool CNF_Sea_By, bool CNF_AIR_By_EuropeandEastUSA, bool CNF_AIR_By_WEST_USA, bool CIF_Sea_By, bool CIF_Air_By_EuropeandEastUSA, bool CIF_AIR_By_WEST_USA, bool FORDestination, bool Exworks, bool advance100, bool Fiftyadv50againstDocs, bool HundredagainstDocs, bool NoofDaysfromInvoice, int No_of_Days_Count_fromInvoice, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Buyer_DL.BuyerPriceTermsDetails_INSandUPDandDEL(BuyerID, FOB_India, CNF_Sea_By, CNF_AIR_By_EuropeandEastUSA, CNF_AIR_By_WEST_USA, CIF_Sea_By, CIF_Air_By_EuropeandEastUSA, CIF_AIR_By_WEST_USA, FORDestination, Exworks, advance100, Fiftyadv50againstDocs, HundredagainstDocs, NoofDaysfromInvoice, No_of_Days_Count_fromInvoice, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable BuyerDetails(string BuyerId)
        {
            return Buyer_DL.BuyerDetails(BuyerId);
        }
        public DataTable BuyerDetails(int OrderId)
        {
            return Buyer_DL.BuyerDetails(OrderId);
        }
        public DataTable BuyerDetails(bool approval)
        {
            return Buyer_DL.BuyerDetails(approval);
        }
        public DataTable BuyerDetails()
        {
            return Buyer_DL.BuyerDetails();
        }
        public bool BuyerApproval(string BuyerID, string ModifiedBy, bool Approval, bool Lotsample, string BCode, decimal Discount, decimal FairTrade, decimal FairTradPremium, int valueType)
        {
            return Buyer_DL.BuyerApproval(BuyerID, ModifiedBy, Approval, Lotsample, BCode, Discount, FairTrade, FairTradPremium, valueType);
        }
        public bool BuyerPaymentandPriceUpdateDetails(string BuyerID, bool advance100, bool Fiftyadv50againstDocs, bool HundredagainstDocs, bool NoofDaysfromInvoice, int No_of_Days_Count_fromInvoice, int TypeOfOperation, string ModifiedBy)
        {
            return Buyer_DL.BuyerPaymentandPriceUpdateDetails(BuyerID, advance100, Fiftyadv50againstDocs, HundredagainstDocs, NoofDaysfromInvoice, No_of_Days_Count_fromInvoice, TypeOfOperation, ModifiedBy);
        }
        public bool BuyerProductInsertDetails(string BuyerID, int ProductID, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Buyer_DL.BuyerProductInsertDetails(BuyerID, ProductID, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public bool BuyerComplaintInsertDetails(string complaint, string ComplaintBy, string BuyerID, string InvoiceID, int InvoiceProductID, DateTime InvoiceDate, decimal ProductQty, string BatchNo, string Action, string CreatedBy, string ModifiedBy, int TypeOfOperation, int ComplaintType)
        {
            return Buyer_DL.BuyerComplaintInsertDetails(complaint, ComplaintBy, BuyerID, InvoiceID, InvoiceProductID, InvoiceDate, ProductQty, BatchNo, Action, CreatedBy, ModifiedBy, TypeOfOperation, ComplaintType);
        }
        public DataTable GetBuyerComplaintDetails()
        {
            return Buyer_DL.GetBuyerComplaintDetails();
        }
        public DataTable GetBuyerComplaintDetails(int ComplaintType, string userid)
        {
            return Buyer_DL.GetBuyerComplaintDetails(ComplaintType, userid);
        }

        public DataTable GetBuyerComplaintDetails(int ComplaintID)
        {
            return Buyer_DL.GetBuyerComplaintDetails(ComplaintID);
        }
        public DataTable GetBuyerLoginDetails(string BuyerID)
        {
            return Buyer_DL.GetBuyerLoginDetails(BuyerID);
        }
        public DataTable GetBuyerProductDetails(string BuyerID)
        {
            return Buyer_DL.GetBuyerProductDetails(BuyerID);
        }
        public DataTable BuyerProductList(string BuyerID)
        {
            return Buyer_DL.BuyerProductList(BuyerID);
        }
        public bool DeletetheBuyer(string BuyerID)
        {
            return Buyer_DL.DeletetheBuyer(BuyerID);
        }
        public bool UpdateBuyerPOandPathDetails(int OrderID, string PurchaseOrderID, string Buyer_path, string Comments)
        {
            return Buyer_DL.UpdateBuyerPOandPathDetails(OrderID, PurchaseOrderID, Buyer_path, Comments);
        }
    }
}
