using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;
namespace MudarOrganic.BL
{
    public class Farmer_BL
    {
        public bool Farmer_INSandUPTandDEL(string FarmerId, string FarmerCode, string FirstName, string FarmerMIECode, string FarmerAPEDACode, decimal TotalAreaInHectares, int NumberOfPlots, string FatherName, string Address, string City_Village, string Taluk, string District, string State, string Country, string PhoneNumber, string MobileNumber, string BankInfo, string BankAccNo, int ChildrenDependents, int ElderlyDependents, int NumberOfEarningPersons, string ChildrenStudies, int Cow, int Ox, int Sheep, int OtherAnimals, bool ProposedFieldOfficer, bool ProposedManager, bool InternalInspectorApproval, bool PRESIDNT, string CreatedBy, string ModifiedBy, string FarmerRegNumber, string PhotoPath, int TypeOfOPeration, DateTime ChemicalAppDate, string BankHolderName, bool Organic, bool OrganicFair, string icsCode)
        {
            return Farmer_DL.Farmer_INSandUPTandDEL(FarmerId, FarmerCode, FirstName, FarmerMIECode, FarmerAPEDACode, TotalAreaInHectares, NumberOfPlots, FatherName, Address, City_Village, Taluk, District, State, Country, PhoneNumber, MobileNumber, BankInfo, BankAccNo, ChildrenDependents, ElderlyDependents, NumberOfEarningPersons, ChildrenStudies, Cow, Ox, Sheep, OtherAnimals, ProposedFieldOfficer, ProposedManager, InternalInspectorApproval, PRESIDNT, CreatedBy, ModifiedBy, FarmerRegNumber, PhotoPath, TypeOfOPeration, ChemicalAppDate, BankHolderName, Organic, OrganicFair,icsCode);
        }
        public  int FarmerCount(string code)
        {
            DataTable dt = Farmer_DL.FarmerCount(code);
            DataRow dr = dt.Rows[0];
            return Convert.ToInt32(dr[0]);
        }
        public List<string> FamerNameCodeArea(string Value, int TypeOfOperation)
        {
            List<string> list = new List<string>();
            DataTable dt = Farmer_DL.FamerNameCodeArea(Value, TypeOfOperation);
            foreach (DataRow dr in dt.Rows)
                list.Add(dr[0].ToString());
            return list;
        }
        public List<string> FarmerNameCodeArea(string Value)
        {
            List<string> list = new List<string>();
            DataTable dt = Farmer_DL.FarmerNameCodeArea(Value);
            foreach (DataRow dr in dt.Rows)
                list.Add(dr[0].ToString());
            return list;
        }
        public DataTable FamerDetails(string farmerName, string farmerCode, string area)
        {
            return Farmer_DL.FamerDetails(farmerName, farmerCode, area);
        }
        public DataTable FamerDetails(string farmerCode)
        {
            return Farmer_DL.FamerDetails(farmerCode);
        }
        public DataTable FamerDetailsByIcs(string icsCode)
        {
            return Farmer_DL.FamerDetailsByIcs(icsCode);
        }
        public DataTable FamerDetails()
        {
            return Farmer_DL.FamerDetails();
        }
        public DataTable GetICSCodes()
        {
            return Farmer_DL.GetICSCodes();
        }
        public DataTable GetNewICSCodes()
        {
            return Farmer_DL.GetNewICSCodes();
        }
        public  DataTable GetAllICSCodes()
        {
            return Farmer_DL.GetAllICSCodes();
        }
        public DataTable FarmerDetails(string Village)
        {
            return Farmer_DL.FarmerDetails(Village);
        }
        public DataTable GetFarmerDetailsonID(string farmerID)
        {
            return Farmer_DL.GetFarmerDetailsonID(farmerID);
        }
        //ravi code for detailed view on 24-marc-13
        public DataTable FamerDetailDetails()
        {
            return Farmer_DL.FamerDetailDetails();
        }
        //code ends
        public DataTable ApprovedFamer_Inspection(bool Approved)
        {
            return Farmer_DL.ApprovedFamer_Inspection(Approved);
        }
        public bool FarmerExist(string farmerID)
        {
            return Farmer_DL.FarmerExist(farmerID);
        }

        public  bool FarmerSeasonProduct_INSandUPTandDEL(int SeasonID, string FarmerID, string SeasonName, string CropCultivating, bool Result, string CreatedBy, string ModifiedBy, DateTime StartDate, DateTime EndDate, int SeasonYear, int ProductId, int TypeOfOperation)
        {
            return Farmer_DL.FarmerSeasonProduct_INSandUPTandDEL(SeasonID, FarmerID, SeasonName, CropCultivating, Result, CreatedBy, ModifiedBy, StartDate, EndDate, SeasonYear, ProductId, TypeOfOperation);
        }
        public bool FarmerSeasonProduct_INSandUPTandDELNEW(int SeasonID, string FarmerID, string SeasonName, string CropCultivating, bool Result, string CreatedBy, string ModifiedBy, DateTime StartDate, DateTime EndDate, int SeasonYear, int ProductId, int TypeOfOperation)
        {
            return Farmer_DL.FarmerSeasonProduct_INSandUPTandDELNEW(SeasonID, FarmerID, SeasonName, CropCultivating, Result, CreatedBy, ModifiedBy, StartDate, EndDate, SeasonYear, ProductId, TypeOfOperation);
        }
        public DataTable FarmerFarmdetails(string FarmerUid)
        {
            return Farmer_DL.FarmerFarm(FarmerUid);
        }
        #region Farm
		public bool Farm_INSandUPTandDEL(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, int Latitude, int Longitude, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref  int ReturnFarmID, int ParentFarmID)
        {
            return Farmer_DL.Farm_INSandUPTandDEL(FarmerId, FarmID, PlotArea, AreaCode, Latitude, Longitude, CreatedBy, ModifiedBy, TypeOfOperation, ref ReturnFarmID, ParentFarmID);
        }
        public DataTable FarmDetails(string FarmerId)
        {
            return Farmer_DL.FarmDetails(FarmerId);
        }
        public DataTable FarmDetails(string FarmerId, bool IsParent)
        {
            DataTable dtFarm = Farmer_DL.FarmDetails(FarmerId);
            DataTable dtFarmResult = dtFarm.Clone();
            if (dtFarm.Rows.Count > 0)
            {
                string condtion = string.Empty;
                if (IsParent)
                    condtion = " ParentFarmID = 0";
                else
                    condtion = " ParentFarmID > 0";
                DataRow[] drs = dtFarm.Select(condtion);
                foreach (DataRow dr in drs)
                    dtFarmResult.ImportRow(dr);
            }
            return dtFarmResult;
        }
        public decimal AvaliablePlotArea(string FarmerId, int SeasonID, int Year, int FarmID)
        {
            DataTable dtFarm = Farmer_DL.AvaliablePlotArea(FarmerId, SeasonID, Year, FarmID);
            decimal APA=0;
            if (dtFarm.Rows.Count > 0)
                APA = Convert.ToDecimal(dtFarm.Rows[0][0].ToString());
            return APA;
        }
        #endregion
        public bool FarmerFamilyDetails_INS(int FarmerFamilyId,string FarmerId, string ChildName, string Gender, DateTime DOB, int Age, bool SchoolGoing, bool Working, string CreatedBy, string ModifiedBy, int TypeOfOPeration ,int NumberEarningPersons,int ElderDependents)
        {
            return Farmer_DL.FarmerFamilyDetails_INS(FarmerFamilyId, FarmerId, ChildName, Gender, DOB, Age, SchoolGoing, Working, CreatedBy, ModifiedBy, TypeOfOPeration, NumberEarningPersons, ElderDependents);
        }
        public DataTable GetFarmerFamilyDeatils(string FarmerId)
        {
            return Farmer_DL.GetFarmerFamilyDeatils(FarmerId);
        }
        public DataTable GetSeasonYearByFarmer()
        {
            return Farmer_DL.GetSeasonYearByFarmer();
        }
        #region New code Internal Inspection Report
        public DataTable GetSeasonDetailsBasedFarmerID(string FarmerID, int SeasonYear)
        {
            return Farmer_DL.GetSeasonDetailsBasedFarmerID(FarmerID, SeasonYear);
        }
        public void GetNewCheckpointQuestions(ref DataTable Animaldt, ref DataTable Farmdt, ref DataTable OCdt, ref DataTable RMCdt, ref DataTable FAdt, ref DataTable RPdt, ref DataTable SCFdt, ref DataTable ECdt, ref DataTable StatCompdt, ref DataTable LFdt, string FarmerID, int year)
        {
            Animaldt = Questiondata("Animal Husbandry", FarmerID, year);
            Farmdt = Questiondata("Farm and FarmManagement", FarmerID, year);
            OCdt = Questiondata("Organic Compliance", FarmerID, year);
            RMCdt = Questiondata("Risk Management Compliance", FarmerID, year);
            FAdt = Questiondata("Farmer Awareness", FarmerID, year);
            RPdt = Questiondata("Risk Processing", FarmerID, year);
            SCFdt = Questiondata("Safety Compliance Farms", FarmerID, year);
            ECdt = Questiondata("Environment Compliance", FarmerID, year);
            StatCompdt = Questiondata("Statutory Compliance", FarmerID, year);
            LFdt = Questiondata("Labor in the Farms", FarmerID, year);
        } 
        #endregion
        //Code by ravi to get Questions
        public DataTable GetYearFarmer(string FarmerID)
        {
            return Farmer_DL.GetCheckpointQuestions(FarmerID).DefaultView.ToTable(true, "Year");
        }
        public void GetCheckpointQuestions(ref DataTable Animaldt, ref DataTable Farmdt, ref DataTable riskDt, ref DataTable Harvest, string FarmerID, ref DataTable DtYear, int year)
        {
            // int year = Convert.ToInt32(DtYear.Rows[0][0].ToString());
            Animaldt = Questiondata("Animal Husbandry", FarmerID, year);
            Farmdt = Questiondata("Farm and FarmManagement", FarmerID, year);
            riskDt = Questiondata("Risk Management", FarmerID, year);
            Harvest = Questiondata("Post Harvest Measures and Processing", FarmerID, year);
        }
        public DataTable Getsubcheckpoints(string QuesID)
        {
            return Farmer_DL.Getsubcheckpoints(QuesID);
        }
        private DataTable Questiondata(string Category, string FarmerID, int year)
        {
            DataTable dtTempp = new DataTable();
            DataRow[] drRow = Farmer_DL.GetCheckpointQuestions(FarmerID).Select("Qcategory='" + Category + "' and Year='" + year + "'");
            if (drRow.Length > 0)
            {
                dtTempp = Farmer_DL.GetCheckpointQuestions(FarmerID).Clone();
                foreach (DataRow pdr in drRow)
                {
                    DataRow newdr = dtTempp.NewRow();
                    for (int count = 0; count < pdr.ItemArray.Length; count++)
                    {
                        newdr[count] = pdr[count];
                    }
                    dtTempp.Rows.Add(newdr);
                }
            }
            return dtTempp;
        }
        public bool FarmerCheckPoints_INSandUPDandDEL(string FarmerID, int QuestionID, int Answer, string Note, string CreatedBy, string ModifiedBy, int TypeOfOperation, int Year)
        {
            return Farmer_DL.FarmerCheckPoints_INSandUPDandDEL(FarmerID, QuestionID, Answer, Note, CreatedBy, ModifiedBy, TypeOfOperation, Year);
        }
        public bool FieldRisk_Exist(string FarmerID)
        {
            return Farmer_DL.FieldRisk_Exist(FarmerID);
        }
        public DataTable FieldRisk(string FarmerID)
        {
            return Farmer_DL.FiledRisk(FarmerID);
        }
        public DataTable FieldRiskResult(string FarmerID)
        {
            return Farmer_DL.FiledRiskResult(FarmerID);
        }
        public bool FiledRiskt_Result_INSandUPDandDEL(int FieldRiskID, string FarmerID, int FarmID, bool Result, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Farmer_DL.FiledRiskt_Result_INSandUPDandDEL(FieldRiskID, FarmerID, FarmID, Result, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public bool FarmerApproval(string FarmerID, string InspectorName, string InspectionComments, string ModifiedBy, int TypeOfOperation, ref bool ReturnValue)
        {
            return Farmer_DL.FarmerApproval(FarmerID, InspectorName, InspectionComments, ModifiedBy, TypeOfOperation, ref ReturnValue);
        }
        public  DataTable GetFarmerProductDetailsBasedonYear(string FarmerID, int SeasonYear)
        {
            return Farmer_DL.GetFarmerProductDetailsBasedonYear(FarmerID, SeasonYear);
        }
        public DataTable GetFarmerProdcts(int SeasonYear, int seasonID, int productID, string Village, string ICStype)
        {
            return Farmer_DL.GetFarmerProdcts(SeasonYear, seasonID, productID, Village, ICStype);
        }
        public DataTable GetFarmerlistVillagewise(string Village)
        {
            return Farmer_DL.GetFarmerlistVillagewise(Village);
        }
        public DataTable GetFarmerFarmDetails(string FarmerID, string Year)
        {
            return Farmer_DL.GetFarmerFarmDetails(FarmerID, Year);
        }
        //Report
        public DataTable GetFarmerDetails(string Village)
        {
            return Farmer_DL.GetFarmerDetails(Village);
        }

        public string GetUserICSDetails(Guid userId)
        {
            return Farmer_DL.GetUserICSDetails(userId);
        }
        public DataTable GetFarmerVillageDistinct(string Type)
        {
            return Farmer_DL.GetFarmerVillageDistinct(Type);
        }
        public DataTable GetICSVillagelist(string ICS)
        {
            return Farmer_DL.GetICSVillagelist(ICS);
        }
        public DataTable GetEstimationDetails(string Year)
        {
            return Farmer_DL.GetEstimationDetails(Year);
        }
    }
}
