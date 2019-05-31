using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MudarOrganic.DL;
using MudarOrganic.Components;

namespace MudarOrganic.BL
{
    public class Reports_Type
    {
        public int Invoice = 1;
        public int Packing = 2;
        public int Non_Haz_Sea = 3;
        public int Non_Haz_Air = 4;
        public int Cover_Letter = 5;
        public int Fir_Cover_Letter = 6;
        public int PP = 7;
        public int AR = 8;
        public int SP = 9;
        public int CRY = 10;
        public int CRY_P = 11;
        public int COA_BO = 12;
        public int LABEL = 13;
        public int BInvoice = 14;
        public int BGLCInfo = 15;
        public int BTruckBill = 16;
        public int BLR = 17;
        public int Other = 18;
    }
    public class Reports_BL
    {
        public DataTable GetReports()
        {
            return Reports_DL.GetReports();
        }
        public DataTable Trach_Lot(int typeSearch, string Input)
        {
            string Condition = string.Empty;
            if (typeSearch == 1)
                Condition = " ti.InvoiceId LIKE '" + Input + "'";
            else if (typeSearch == 2)
                Condition = " ct.Lotnumber LIKE '%" + Input + "%'";
            else if (typeSearch == 3)
                Condition = " ct.BatchID LIKE '" + Input + "'";
            else if (typeSearch == 4)
                Condition = " od.OrderID  LIKE '" + Input + "'";
            else if (typeSearch == 5)
                Condition = " od.PurchaseOrderID LIKE '" + Input + "'";
            else
                Condition = " ti.InvoiceId LIKE '" + Input + "'";
            return Reports_DL.Trach_Lot(Condition);
        }
        public DataTable GetInvoiceList_Farmer(string FarmerID, string ProductID)
        {
            return Reports_DL.GetInvoiceList_Farmer(FarmerID, ProductID);
        }
		public  bool OrderReportsPathInsertandUpdate(int OrderID, int BranchOrderID, string Path, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Reports_DL .OrderReportsPathInsertandUpdate(OrderID,BranchOrderID,Path,CreatedBy,ModifiedBy,TypeOfOperation);
        }
        public DataTable OrderReportsPathGetDetails(int OrderID, int BranchOrderID)
        {
            return Reports_DL.OrderReportsPathGetDetails(OrderID, BranchOrderID);
        }
        public DataTable OrderReportsPathGetDetails(int OrderID)
        {
            return Reports_DL.OrderReportsPathGetDetails(OrderID);
        }
        public DataTable GetAFLReportData(string ICSCode,string Year)
        {
            return Reports_DL.GetAFLReportData(ICSCode,Year);
        }
        public DataTable GetAFLReportProduction(string ICSCode)
        {
            return Reports_DL.GetAFLReportProduction(ICSCode);
        }
        public DataTable GetAFLTotalProduction(string ICSCode)
        {
            return Reports_DL.GetAFLTotalProduction(ICSCode);
        }
        public DataTable GetAFLTotalProduction(string ICSCode, string Year)
        {
            return Reports_DL.GetAFLTotalProduction(ICSCode,Year);
        }
        public DataTable GetAFLReportProductionEstimation(string ICSCode,string Year)
        {
            return Reports_DL.GetAFLReportProductionEstimation(ICSCode, Year);
        }
        public DataTable GetAFLTotalProduction(string ICSCode, string Year, int ProductID)
        {
            return Reports_DL.GetAFLTotalProduction(ICSCode, Year, ProductID);
        }
        public DataTable GetCollectionDates(int productID, string code)
        {
            return Reports_DL.GetCollectionDates(productID,code);
        }
        public DataTable GetAllProducDetails()
        {
            return Reports_DL.GetAllProducDetails();
        }
        public DataTable GetSelectedCollectionDate(string Date, int productID)
        {
            return Reports_DL.GetSelectedCollectionDate(Date, productID);
        }
        public DataTable GetSelectedCollectionDate(string Date, int productID, string Code)
        {
            return Reports_DL.GetSelectedCollectionDate(Date,productID, Code);
        }
        public  DataTable GetSelectedBlendnDate(string Date, int productID)
        {
            return Reports_DL.GetSelectedBlendnDate(Date, productID);
        }
        public DataTable GetSelectedBlendQty(string Code, int productID)
        {
            return Reports_DL.GetSelectedBlendQty(Code, productID);
        }
        public DataTable GetProduction(string ProductID, DateTime Date, string Year)
        {
            return Reports_DL.GetProduction( ProductID,  Date, Year);
        }
        public DataTable GetNewallcollectionList(string productID, string code)
        {
             return Reports_DL.GetNewallcollectionList( productID, code);
        }
        public DataTable GetNewallcollectionList(string code)
        {
            return Reports_DL.GetNewallcollectionList(code);
        }
        public DataTable GetBlendDetails(string ProductID)
        {
            return Reports_DL.GetBlendDetails(ProductID);
        }
        public DataTable GePackingDetails(string ProductID)
        {
            return Reports_DL.GePackingDetails(ProductID);
        }
        public DataTable GeDispatchDetails(string ProductID)
        {
            return Reports_DL.GeDispatchDetails(ProductID);
        }
        public DataTable GetFreezeDetails()
        {
            return Reports_DL.GetFreezeDetails();
        }
    }
}
