using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class Settings_BL
    {
        public DataTable GetFinicalYear()
        {
            return Settings_DL.GetFinicalYear();
        }
        public DataTable GetLotYear(DateTime Date)
        {
            return Settings_DL.GetLotYear(Date);
        }
        public DataTable GetProductionYear(DateTime Date)
        {
            return Settings_DL.GetProductionYear(Date);
        }

        public bool StandDetails_INSandUPDandDEL(int StandID, int Year, int ProductID, DateTime Date, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Settings_DL.StandDetails_INSandUPDandDEL(StandID, Year, ProductID, Date, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public  bool MentholPercentageDetailsINS(int PerID, int Year, int ProductID, decimal Percentage, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Settings_DL.MentholPercentageDetailsINS(PerID, Year, ProductID, Percentage, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable GetStandDetails()
        {
            return Settings_DL.GetStandDetails();
        }
        public DataTable GetMentholPerDetails()
        {
            return Settings_DL.GetMentholPerDetails();
        }
        public DataTable GetMentholPerDetails(string PerID)
        {
            return Settings_DL.GetMentholPerDetails(PerID);
        }
        public DataTable GetMentholPerDetails(int ProductID)
        {
            return Settings_DL.GetMentholPerDetails(ProductID);
        }
        public DataTable GetStandDetails(string ProductID)
        {
            return Settings_DL.GetStandDetails(ProductID);
        }
        public DataTable GetStandDetails(string ProductID, string Year)
        {
            return Settings_DL.GetStandDetails(ProductID, Year);
        }
        public DataTable GetStandDetails(int StandID)
        {
            return Settings_DL.GetStandDetails(StandID);
        }
        public DataTable GetProductDetails()
        {
            return Settings_DL.GetProductDetails();
        }
        public DataTable GetStandardProductDetails(string productID, string Year)
        {
            return Settings_DL.GetStandardProductDetails(productID,Year);
        }
    }
}
