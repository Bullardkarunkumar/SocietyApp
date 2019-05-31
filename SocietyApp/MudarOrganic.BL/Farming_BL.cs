using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MudarOrganic.DL;

namespace MudarOrganic.BL
{
    public class Farming_BL
    {
        #region PlantingInfo
        public  DataTable GetPlantingInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetPlantingInfo(farmerID, productID, seasonID);
        }
        public DataTable GetPlantingInfo(string farmerID)
        {
            return Farming_DL.GetPlantingInfo(farmerID);
        }
        public bool PlantingInfo_INT_UPT_DEL(ref int rPlantingID, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string PlantingSource, string PlantingBill_Date, string PlantingSeedVariety, string PlantingSeedTreatMent, string PlantingQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, int PlantingID)
        {
            return Farming_DL.PlantingInfo_INT_UPT_DEL(ref rPlantingID, FarmerID, ProductID, SeasonID, SeasonYear, PlantingSource, PlantingBill_Date, PlantingSeedVariety, PlantingSeedTreatMent, PlantingQuantity, CreatedBy, ModifiedBy, TypeOfOperation, PlantingID);
        }
        #endregion

        #region Input Information
        public DataSet GetInputInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetInputInfo(farmerID, productID, seasonID);
        }
        public DataSet GetInputInfo()
        {
            return Farming_DL.GetInputInfo();
        }
        public DataTable GetInputInfoonProductID(string ProdutID)
        {
             return Farming_DL.GetInputInfoonProductID(ProdutID);
        }
        public bool InputInformation_INT_UPT_DEL(string FarmerID, int ProductID, int SeasonID, string SeasonYear, string IMMaterial, string IMSource, string IMBillNo, DateTime IMDate, string IMQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rInputMID, int InputMID)
        {
            return Farming_DL.InputInformation_INT_UPT_DEL(FarmerID, ProductID, SeasonID, SeasonYear, IMMaterial, IMSource, IMBillNo, IMDate, IMQuantity, CreatedBy, ModifiedBy, TypeOfOperation, ref rInputMID, InputMID);
        }
        public bool InputTransaction_INT_UPT_DEL(int InputMID, string IM_MT_HC, string IMDays, string IMPeriod, string IMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int InputMTId)
        {
            return Farming_DL.InputTransaction_INT_UPT_DEL(InputMID, IM_MT_HC, IMDays, IMPeriod, IMPlanting, CreatedBy, ModifiedBy, TypeOfOperation, InputMTId);
        }
        #endregion

        #region Disease Information
        public DataSet GetDisease(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetDisease(farmerID, productID, seasonID);
        }
        public  DataSet GetDisease()
        {
            return Farming_DL.GetDisease();
        }
        public  DataTable GetDiseInfoonProductID(string ProdutID)
        {
            return Farming_DL.GetDiseInfoonProductID(ProdutID);
        }
        public bool DiseaseInfo_INT_UPT_DEL(string DiseaseName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string DMIExpected, string DMIObserved, string DMIPreventionMaterial, string DMISource, string DMIBillNo, DateTime DMIDate, string DMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rDiseaseMID, int DiseaseMID)
        {
            return Farming_DL.DiseaseInfo_INT_UPT_DEL(DiseaseName, FarmerID, ProductID, SeasonID, SeasonYear, DMIExpected, DMIObserved, DMIPreventionMaterial, DMISource, DMIBillNo, DMIDate, DMIQuantity, CreatedBy, ModifiedBy, TypeOfOperation, ref rDiseaseMID, DiseaseMID);
        }
        public DataTable GetInsectInfoonProduct(string ProdutID)
        {
            return Farming_DL.GetInsectInfoonProduct(ProdutID);
        }
        public bool DiseaseTransaction_INT_UP_DEL(int DiseaseMID, string DM_MT_HC, string DMDays, string DMPeriod, string DMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int DMITId)
        {
            return Farming_DL.DiseaseTransaction_INT_UP_DEL(DiseaseMID, DM_MT_HC, DMDays, DMPeriod, DMPlanting, CreatedBy, ModifiedBy, TypeOfOperation, DMITId);
        }
        #endregion

        #region Insect Information
        public DataSet GetInsectInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetInsectInfo(farmerID, productID, seasonID);
        }
        public DataSet GetInsectInfo()
        {
            return Farming_DL.GetInsectInfo();
        }
        public  bool InsectInfo_INT_UPT_DEL(string InsectName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string IMIExpected, string IMIObserved, string IMIPreventionMaterial, string IMISource, string IMIBillNo, DateTime IMIDate, string IMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rInsectMIID, int InsectMIID)
        {
            return Farming_DL.InsectInfo_INT_UPT_DEL(InsectName, FarmerID, ProductID, SeasonID, SeasonYear, IMIExpected, IMIObserved, IMIPreventionMaterial, IMISource, IMIBillNo, IMIDate, IMIQuantity, CreatedBy, ModifiedBy, TypeOfOperation, ref rInsectMIID, InsectMIID);
        }
        public bool InsectTransaction_INT_UP_DEL(int InsectMIID, string InsectM_MT_HC, string InsectMDays, string InsectMPeriod, string InsectMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int InsectMTId)
        {
            return Farming_DL.InsectTransaction_INT_UP_DEL(InsectMIID, InsectM_MT_HC, InsectMDays, InsectMPeriod, InsectMPlanting, CreatedBy, ModifiedBy, TypeOfOperation, InsectMTId);
        }
	    #endregion

        #region Pest Information
        public DataSet GetPestInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetPestInfo(farmerID, productID, seasonID);
        }
        public bool PestInfo_INT_UPT_DEL(string PestName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string PMIExpected, string PMIObserved, string PMIPreventionMaterial, string PMISource, string PMIBillNo, DateTime PMIDate, string PMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rPestMIID, int PestMIID)
        {
            return Farming_DL.PestInfo_INT_UPT_DEL(PestName, FarmerID, ProductID, SeasonID, SeasonYear, PMIExpected, PMIObserved, PMIPreventionMaterial, PMISource, PMIBillNo, PMIDate, PMIQuantity, CreatedBy, ModifiedBy, TypeOfOperation, ref rPestMIID, PestMIID);
        }
        public bool PestTransaction_INT_UPT_DEL(int PestMIID, string PM_MT_HC, string PMDays, string PMPeriod, string PMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int PMITId)
        {
            return Farming_DL.PestTransaction_INT_UPT_DEL(PestMIID, PM_MT_HC, PMDays, PMPeriod, PMPlanting, CreatedBy, ModifiedBy, TypeOfOperation, PMITId);
        }
        #endregion

        #region Weed Information
        public DataSet GetWeedInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetWeedInfo(farmerID, productID, seasonID);
        }
        public bool WeedInfo_INT_UPT_DEL(string WeedName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string WMIExpected, string WMIObserved, string WMIPreventionMaterial, string WMISource, string WMIBillNo, DateTime WMIDate, string WMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rWeedMIID, int WeedMIID)
        {
            return Farming_DL.WeedInfo_INT_UPT_DEL(WeedName, FarmerID, ProductID, SeasonID,SeasonYear, WMIExpected,WMIObserved,WMIPreventionMaterial, WMISource,WMIBillNo,WMIDate,WMIQuantity,CreatedBy,ModifiedBy, TypeOfOperation, ref rWeedMIID, WeedMIID);
        }
        public bool WeedTransaction_INT_UPT_DEL(int WeedMIID, string WM_MT_HC, string WMDays, string WMPeriod, string WMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int WMITId)
        {
            return Farming_DL.WeedTransaction_INT_UPT_DEL(WeedMIID,WM_MT_HC,WMDays, WMPeriod, WMPlanting,CreatedBy, ModifiedBy,TypeOfOperation,WMITId);
        }
        #endregion

        #region Water Information
        public DataSet GetWaterInfo(string farmerID, string productID, string seasonID)
        {
            return Farming_DL.GetWaterInfo(farmerID, productID, seasonID);
        }
        public bool WaterInfo_INT_UPT_DEL(string FarmerID, int ProductID, int SeasonID, string SeasonYear, string WISource, string  WIOrganicF, string WIFCSource, string WIFCWaterFlow, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rWaterMID, int WaterMID)
        {
            return Farming_DL.WaterInfo_INT_UPT_DEL(FarmerID,ProductID,SeasonID,SeasonYear, WISource,  WIOrganicF,  WIFCSource,WIFCWaterFlow,CreatedBy,ModifiedBy,TypeOfOperation, ref rWaterMID, WaterMID);
        }
        public bool WaterTransaction_INT_UPT_DEL(int WaterMID, string WITT_Irrigation, string WITT_Days, string WITT_Period, string WITT_Planting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int WITTId)
        {
            return Farming_DL.WaterTransaction_INT_UPT_DEL(WaterMID, WITT_Irrigation, WITT_Days, WITT_Period, WITT_Planting, CreatedBy, ModifiedBy, TypeOfOperation, WITTId);
        }
        #endregion

        #region BasicFarmingInfo
        public bool FarmingInfo_INSandUPDandDEL(int Year, int ProductID, int SeasonID, DateTime PlantationFrom, DateTime PlantationTo, int FirstCutFrom, int FirstCutTo, decimal Qty1stCutHCFrom, decimal Qty1stCutHCTo, decimal FirstRecoveryOilFrom, decimal FirstRecoveryOilTo, int SecondCutFrom, int SecondCutTo, decimal Qty2ndCutHCFrom, decimal Qty2ndCutHCTo, decimal SecondRecoveryOilFrom, decimal SecondRecoveryOilTo, int EsvsAc_From, int EsvsAc_To, string CreatedBy, string ModifiedBy, int TypeOfOperation, int FarmingInfoID)
        {
            return Farming_DL.FarmingInfo_INSandUPDandDEL(Year, ProductID, SeasonID, PlantationFrom, PlantationTo, FirstCutFrom, FirstCutTo, Qty1stCutHCFrom, Qty1stCutHCTo, FirstRecoveryOilFrom, FirstRecoveryOilTo, SecondCutFrom, SecondCutTo, Qty2ndCutHCFrom, Qty2ndCutHCTo, SecondRecoveryOilFrom, SecondRecoveryOilTo, EsvsAc_From,  EsvsAc_To, CreatedBy, ModifiedBy, TypeOfOperation, FarmingInfoID);
        }
        public DataTable GetBasicFarmingInfo(int year, int productID, int seasonID)
        {
            return Farming_DL.GetBasicFarmingInfo(year, productID, seasonID);
        }
        public  DataTable GetProductionInfo(int productID, int seasonID)
        {
            return Farming_DL.GetProductionInfo(productID,seasonID);
        }
        public DataTable GetBasicFarmingInfo(int year)
        {
            return Farming_DL.GetBasicFarmingInfo(year);
        }
        public DataTable GetBasicFarmingInfoBasedon(int year, int seasonID)
        {
            return Farming_DL.GetBasicFarmingInfoBasedon(year, seasonID);
        }
        public DataTable GetBasicFarmingInfoBasedonID(int FarmingInfoID)
        {
            return Farming_DL.GetBasicFarmingInfoBasedonID(FarmingInfoID);
        }
        #endregion

        public DataTable GetProductDetailsByYear(string Year)
        {
            return Farming_DL.GetProductDetailsByYear(Year);
        }
    }
}

