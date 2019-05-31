using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.DL;

namespace MudarOrganic.BL
{
    
    public class CategoryProduct_BL
    {
        #region Category
        public bool Category_INT_UPT(int categoryID, string categoryname, string createdby, string modifiedby, int typeOperation)
        {
            return CategoryProduct_DL.Category_INT_UPT(categoryID, categoryname, createdby, modifiedby, typeOperation);
        }
        public DataTable GetCategoryDetails()
        {
            return CategoryProduct_DL.GetCategoryDetails();
        } 
        #endregion
        #region Season
        public int Season_INT_UPT(int SeasonID, string Seasonname,DateTime StartDate,DateTime EndDate ,string createdby, string modifiedby, int typeOperation,int SeasonYear)
        {
            return CategoryProduct_DL.Season_INT_UPT(SeasonID, Seasonname, StartDate, EndDate, createdby, modifiedby, typeOperation,SeasonYear);
        }
        public DataTable GetSeasonDetails()
        {
            return CategoryProduct_DL.GetSeasonDetails();
        }
        public DataTable GetSeasonDetails(int seasonId)
        {
            return CategoryProduct_DL.GetSeasonDetails(seasonId);
        }
        public bool SeasonProduct_INSandUPDandDEL(int ProductID, int SeasonId, int typeOperation)
        {
            return CategoryProduct_DL.SeasonProduct_INSandUPDandDEL(ProductID, SeasonId, typeOperation);
        }
        public DataTable GetSeasonDetailsBasedonYear(string Year)
        {
            return CategoryProduct_DL.GetSeasonDetailsBasedonYear(Year);
        }
        public DataTable GetSeasonDetails(string Year)
        {
            return CategoryProduct_DL.GetSeasonDetails(Year);
        }

        public DataTable SeasonDetails_FarmerSeasonProduct(Guid farmerId, int ProductID, int SeasonId, int seasonYear)
        {
            return CategoryProduct_DL.SeasonDetails_FarmerSeasonProduct(farmerId, ProductID, SeasonId, seasonYear);
        }

        public DataTable GetSeasonDetailsBasedonFarmerandYear(Guid farmerId, string Year)
        {
            return CategoryProduct_DL.GetSeasonDetailsBasedonFarmerandYear(farmerId, Year);
        }

        public DataTable GetProductNameByFarmerandSeason(int seasonId, Guid farmerId, int seasonYear)
        {
            return CategoryProduct_DL.GetProductNameByFarmerandSeason(seasonId, farmerId, seasonYear);
        }

        public int GetProductByName(string productName)
        {
            return CategoryProduct_DL.GetProductByName(productName);
        }

        public int GetSeasonByName(string seasonName)
        {
            return CategoryProduct_DL.GetSeasonByName(seasonName);
        }
        #endregion
    } 
}
