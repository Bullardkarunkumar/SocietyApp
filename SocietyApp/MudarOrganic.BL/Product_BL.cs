using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.DL;

namespace MudarOrganic.BL
{
    public class Product_BL
    {
        public bool Product_INS_UPT_DEL(int ProductID, string ProductCode, string ProductName, string Description, string ItcHsCode, int CategoryID, string CreatedBy, string ModifiedBy, int typeOperation, string Specification)
        {
            return Product_DL.Product_INT_UPT_DEL(ProductID, ProductCode, ProductName, Description, ItcHsCode, CategoryID, CreatedBy, ModifiedBy, typeOperation, Specification);
        }
        public DataTable GetProductDetails()
        {
            return Product_DL.GetProductDetails();
        }
        public DataTable GetAllProducDetails()
        {
            return Product_DL.GetAllProducDetails();
        }
        public DataTable GetProductDetailsNew()
        {
            return Product_DL.GetProductDetailsNew();
        }

        public string GetProductName(int productId)
        {
            return Product_DL.GetProductName(productId);
        }

        public List<int> GetSeasonProdIds(int seasonId)
        {
            return Product_DL.GetSeasonProdIds(seasonId);
        }
        
        public DataTable GetProductDetails(int seasonID, int CategoryID)
        {
            return Product_DL.GetProductDetails(seasonID, CategoryID);
        }
        public DataTable GetProductDetails(string startDate, string endDate, string ProductID)
        {
            return Product_DL.GetProductDetails(startDate, endDate, ProductID);
        }
        public DataTable GetProductDetails(int ProductID)
        {
            return Product_DL.GetProductDetails(ProductID);
        }

        public  DataTable GetProductDetails(string Productvalue)
        {
            return Product_DL.GetProductDetails(Productvalue);
        }
        public DataTable GetProductDetails(string FarmerID, string Year)
        {
            return Product_DL.GetProductDetails(FarmerID, Year);
        }
        public List<string> ProductName(string value)
        {
            List<string> item = new List<string>();
            if (value.ToLower() == "all")
            {
                DataTable dtproduct = GetProductDetails();
                foreach (DataRow dr in dtproduct.Rows)
                    item.Add(dr["ProductId"] + " - " + dr["ProductName"].ToString());
            }
            else
            {
                DataTable dtproduct = GetProductDetails(value);
                foreach (DataRow dr in dtproduct.Rows)
                    item.Add(dr["ProductId"] + " - " + dr["ProductName"].ToString());
            }
            return item;
        }

        public DataTable GetProductDetailsbySeason(int seasonid)
        {
            return Product_DL.GetProductDetailsbySeason(seasonid);
        }

        public DataTable GetProductDetailsbySeasonNew(int seasonid)
        {
            return Product_DL.GetProductDetailsbySeasonNew(seasonid);
        }

        // test run 
        public  DataTable GetProductDetailsbySeason(int seasonid, int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select p.*, s.SeasonName, s.StartDate, s.EndDate, c.CategoryName  from tblProductDetails p, tblSeason s,tblCategory c where p.SeasonID=" + seasonid + " and s.SeasonID=" + seasonid + " and s.SeasonYear=" + year + " and p.Pyear=" + year + " and c.CategoryId = p.CategoryId");
        }
        public DataTable GetProductPrice()
        {
            return Product_DL.GetProductPrice();
        }
        public DataTable GetProductPricebyName(string name)
        {
            return Product_DL.GetProductPricebyName(name);
        }
        public DataTable GetProductPricebyName(string name, string creditDay)
        {
            return Product_DL.GetProductPricebyName(name, creditDay);
        }
        public DataTable GetProductPricebyBuyer(string creditDay, string BuyerID)
        {
            return Product_DL.GetProductPricebyBuyer(creditDay, BuyerID);
        }
        public bool GetUpdatePriceCalculation(DataTable dtPrice, double USD, double Transport, double Others, DateTime dtDate, string CreatedBy, 
            string ModifiedBy, int TypeOfOperation, decimal MandyTax, decimal BMar, decimal insurance,decimal localtax, decimal addUpPrice)
        {
            DataTable dtTemp = new DataTable();
            dtTemp = dtPrice.Copy();
            dtTemp.Columns.Add("POPriceMB");
            dtTemp.Columns.Add("FOBPrice");
            dtTemp.Columns.Add("USA_Sea");
            dtTemp.Columns.Add("USA_Air");
            dtTemp.Columns.Add("USA_Air_West");
            dtTemp.Columns.Add("Europe_Sea");
            dtTemp.Columns.Add("Europe_Air");
            dtTemp.Columns.Add("Europe_Air_West");
            dtTemp.Columns.Add("India_Price");
            dtTemp.Columns.Add("Non_organic_India");
            dtTemp.Columns.Add("Non_organic_USA");
            for (int i = 0; i < dtPrice.Rows.Count; i++)
            {
                double price = 0;
                if (!string.IsNullOrEmpty(dtTemp.Rows[i]["PriceMB"].ToString()))
                    price = Convert.ToDouble(dtTemp.Rows[i]["PriceMB"].ToString().Trim());
                else
                    price = 0;
                dtTemp.Rows[i]["POPriceMB"] = price + Others;
                double pric=price + Others;
                double FOB = ((((pric * (1 + (Convert.ToDouble(MandyTax) / 100))) + Transport) / (1 - (Convert.ToDouble(BMar) / 100))) / USD);

                dtTemp.Rows[i]["FOBPrice"] = Math.Round(FOB, 1);
                dtTemp.Rows[i]["USA_Sea"] = Math.Round(FOB + 2, 1);
                dtTemp.Rows[i]["USA_Air"] = Math.Round(FOB + 7, 1);
                dtTemp.Rows[i]["USA_Air_West"] = Math.Round(FOB + 10, 1);
                dtTemp.Rows[i]["Europe_Sea"] = Math.Round((FOB + 2) * (1 + (Convert.ToDouble(insurance) / 100)), 1);
                dtTemp.Rows[i]["Europe_Air"] = Math.Round((FOB + 7) * (1 + (Convert.ToDouble(insurance) / 100)), 1);
                dtTemp.Rows[i]["Europe_Air_West"] = Math.Round((FOB + 10) * (1 + (Convert.ToDouble(insurance) / 100)), 1);
                double lt =  (1 - (Convert.ToDouble(localtax) / 100));
                double iPrice = (((pric * (1 + (Convert.ToDouble(MandyTax) / 100))) + Transport) / (1 - (Convert.ToDouble(BMar) / 100))) / lt;
                dtTemp.Rows[i]["India_Price"] = Math.Round(iPrice, 1);
                double NonOrgInd = ((((price + 25) * 1.025) + 35) + 50);
                dtTemp.Rows[i]["Non_organic_India"] = Math.Round(NonOrgInd, 1);
                dtTemp.Rows[i]["Non_organic_USA"] = Math.Round(NonOrgInd / USD, 1);
            }
            return Product_DL.tblProductPrice_INSandUPD(dtTemp, dtDate, USD, Transport, Others, CreatedBy, ModifiedBy, TypeOfOperation, MandyTax, BMar, insurance,localtax, addUpPrice);
        }
        public DataTable GetUpdateProductPrice(int TypeOfOperation)
        {
            return Product_DL.GetUpdateProductPrice(TypeOfOperation);
        }
        public DataTable GetUpdateProductPrice(string date)
        {
            return Product_DL.GetUpdateProductPrice(date);
        }
        public DataTable GetProductPrice(int TypeOfOperation)
        {
            return Product_DL.GetProductPrice(TypeOfOperation);
        }
        public DataTable GetDetailsAllPriceswithDate(int typeofoperation,int Qty,DateTime createdDate)
        {
            return Product_DL.GetDetailsAllPriceswithDate(typeofoperation, Qty, createdDate);
        }
        public  DataTable GetProductCode(int ProductID)
        {
            return Product_DL.GetProductCode(ProductID);
        }
        public DataTable GetDetailsAllPrices(int typeofoperation, int Qty, int productID)
        {
            return Product_DL.GetDetailsAllPrices(typeofoperation, Qty,productID);
        }
        public DataTable GetDetailsAllPrices(int typeofoperation, int Qty, int productID, DateTime createDate)
        {
            return Product_DL.GetDetailsAllPrices(typeofoperation, Qty, productID, createDate);
        }
        public DataTable GetMaxHistoryIDSupplierProducts(string ProductID)
        {
            return Product_DL.GetMaxHistoryIDSupplierProducts(ProductID);
        }
    }
}
