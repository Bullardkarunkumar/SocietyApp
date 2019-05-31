using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudarOrganic.Components
{
    public class MudarOrderConstants
    {
        public const string DefaultBuyerId = "19823D03-CEAB-48F0-AE0A-E01795580275";
    }
    public class MudarSPName
    {
        public string sp_GetUser_Login_Role = "dbo.sp_GetUser_Login_Role";
        public string sp_BranchDetails_INSandUPD = "dbo.sp_BranchDetails_INSandUPD";
        public string sp_BranchDetails = "dbo.sp_BranchDetails";
        public string sp_BranchDetails_DEL = "dbo.sp_BranchDetails_DEL";
        public string sp_Roles_INSandUPD = "dbo.sp_Roles_INSandUPD";
        public string sp_CategoryDetails_INSandUPD = "dbo.sp_CategoryDetails_INSandUPDandDEL";
        public string sp_EmployeeDetails_INSandUPDandDEL = "dbo.sp_EmployeeDetails_INSandUPDandDEL";
        public string sp_UserInRoles_INSandUPDandDEL = "dbo.sp_UserInRoles_INSandUPDandDEL";
        public string sp_UserLogin_INSandUPDandDEL = "dbo.sp_UserLogin_INSandUPDandDEL";
        public string sp_EmployeeDetails = "dbo.sp_EmployeeDetails";
        public string sp_season = "sp_Season_INSandUPDandDELNew";//"dbo.sp_Season_INSandUPDandDEL";
        public string sp_Product_INSandUPDandDEL = "dbo.sp_Products_INSandUPDandDEL";
        public string sp_PriceDetails = "dbo.sp_PriceDetails";
        public string sp_GetProduct_PriceDetails = "dbo.sp_GetProduct_PriceDetails";
        public string sp_FarmerDetails_INSandUPDandDEL = "dbo.sp_FarmerDetails_INSandUPDandDEL";
        public string sp_FarmerDetails_INSandUPDandDEL_Test = "sp_FarmerDetails_INSandUPDandDEL_Test";
        public string sp_SeasonDetails_INSandUPDandDEL = "dbo.sp_SeasonDetails_INSandUPDandDEL";
        public string sp_FarmDetails_INSandUPDandDEL = "dbo.sp_FarmDetails_INSandUPDandDEL";
        public string sp_FarmDetails = "dbo.sp_FarmDetails";
        public string sp_FarmerFamilyDetails = "dbo.sp_FarmerFamilyDetails_INSandUPDandDEL";
        public string sp_GetSeasonDetailsBasedFarmerID = "dbo.sp_GetSeasonDetailsBasedFarmerID";
        public string sp_InspectionPlan_INS = "dbo.sp_InspectionPlan_INS";
        public string sp_InspectionHistory_INSandUPDandDEL = "dbo.sp_InspectionHistory_INSandUPDandDEL";
        public string sp_PlantationDetails_INSandUPDandDEL = "dbo.sp_PlantationDetails_INSandUPDandDEL";
        public string sp_UnitInformationDetails_INSandUPDandDEL = "dbo.sp_UnitInformationDetails_INSandUPDandDEL";
        public string sp_BuyerPriceTermsDetails_INSandUPDandDEL = "dbo.sp_BuyerPriceTermsDetails_INSandUPDandDEL";
        public string sp_GetProductPrice = "dbo.sp_GetProductPrice";
        public string sp_tblProductPrice_INSandUPD = "dbo.sp_ProductPrice_INSandUPD";
        public string sp_tblPriceHistory_INS = "dbo.sp_PriceHistory_INS";
        public string sp_GetProductCalculationData = "dbo.sp_GetProductCalculationData";
        public string sp_FarmerCheckPoints_INSandUPDandDEL = "dbo.sp_FarmerCheckPoints_INSandUPDandDEL";
        public string sp_ProductPriceDetails_INSandUPDandDEL = "dbo.sp_ProductPriceDetails_INSandUPDandDEL";
        public string sp_BuyerDetails_INSandUPDandDEL = "dbo.sp_BuyerDetails_INSandUPDandDEL";
        public string sp_BuyerTransportDetails_INSandUPDandDEL = "dbo.sp_BuyerTransportDetails_INSandUPDandDEL";
        public string sp_vmUserLoginRoleDetails_Select = "sp_vmUserLoginRoleDetails_Select";
		public string sp_OrderDetails_INSandUPDandDEL = "dbo.sp_OrderDetails_INSandUPDandDEL";
        public string sp_LotSample_OrderDetails_UPDandDEL="sp_LotSample_OrderDetails_UPDandDEL";
        public string sp_OrderProducts_INSandUPDandDEL = "dbo.sp_OrderProducts_INSandUPDandDEL";
        public string sp_OrderDetails_UPD = "dbo.sp_OrderDetails_UPD";
        public string sp_BranchOrderDetails_UPD = "dbo.sp_BranchOrderDetails_UPD";
        public string sp_BranchOrder_Pdfpath_UPD = "dbo.sp_BranchOrder_Pdfpath_UPD";
        public string sp_BranchOrderDetails_INSandUPDandDEL = "dbo.sp_BranchOrderDetails_INSandUPDandDEL";
        public string sp_BranchOrderProduct_INSandUPDandDEL = "dbo.sp_BranchOrderProduct_INSandUPDandDEL";
        public string sp_InvoiceDetails_INSandUPDandDEL = "dbo.sp_InvoiceDetails_INSandUPDandDEL";
        public string sp_InvoiceProductDetails_INSandUPDandDEL = "dbo.sp_InvoiceProductDetails_INSandUPDandDEL";
        public string sp_sp_ProductsCollectionDetails_INSandUPDandDEL = "dbo.sp_ProductsCollectionDetails_INSandUPDandDEL";
        public string sp_Order_Pdfpath_UPD = "dbo.sp_Order_Pdfpath_UPD";
        public string sp_packingDetails_UPD = "dbo.sp_packingDetails_UPD";
        public string sp_CheckBuyerApporval = "dbo.sp_CheckBuyerApporval";
		public string sp_tblCollection_INSandUPDandDEL = "dbo.sp_tblCollection_INSandUPDandDEL";
        public string sp_tblCollectionTransaction_INSandUPDandDEL = "dbo.sp_tblCollectionTransaction_INSandUPDandDEL";
        public string sp_tblPreOrderCollectionTransaction_INSandUPDandDEL = "dbo.sp_tblPreOrderCollectionTransaction_INSandUPDandDEL";
        public string sp_tblPreOrderCollectionTransaction_INSandUPDandDEL_New = "dbo.sp_tblPreOrderCollectionTransaction_INSandUPDandDEL_new";
        public string sp_tblPreOrderCollectionTransaction_Update = "sp_tblPreOrderCollectionTransaction_Update";
        public string sp_tblPreOrderCollectionTransaction_Update_New = "sp_tblPreOrderCollectionTransaction_Update_New";
        public string sp_tblPlantationDetails_UPD = "dbo.sp_tblPlantationDetails_UPD";
        public string SP_udf_Num_ToWords = "dbo.SP_udf_Num_ToWords";
        public string sp_OrderReports_Path_INSandUPD = "dbo.sp_OrderReports_Path_INSandUPD";
        public string sp_UPD_Buyer_PaymentandPriceandTransport_Details="dbo.sp_UPD_Buyer_PaymentandPriceandTransport_Details";
        public string sp_OrderSampleDetails_INSandUPD = "dbo.sp_OrderSampleDetails_INSandUPD";
        public string sp_OrderSampleProductDetails_INSandUPD = "dbo.sp_OrderSampleProductDetails_INSandUPD";
        public string sp_FieldRisk_Farmer_INSandUPDandDEL = "dbo.sp_FieldRisk_Farmer_INSandUPDandDEL";
        public string sp_FarmerApproval = "dbo.sp_FarmerApproval";
        public string sp_tblPlantingInformation_INSandUPDandDEL = "dbo.sp_tblPlantingInformation_INSandUPDandDEL";
        public string sp_tblInputInformation_INSandUPDandDEL = "dbo.sp_tblInputInformation_INSandUPDandDEL";
        public string sp_tblInputTransaction_INSandUPDandDEL = "dbo.sp_tblInputTransaction_INSandUPDandDEL";
        public string sp_tblDiseaseManagementInfo_INSandUPDandDEL = "dbo.sp_tblDiseaseManagementInfo_INSandUPDandDEL";
        public string sp_tblDiseaseManagementInfoTransaction_INSandUPDandDEL = "dbo.sp_tblDiseaseManagementInfoTransaction_INSandUPDandDEL";
        public string sp_tblInsectsManagementInfo_INSandUPDandDEL = "dbo.sp_tblInsectsManagementInfo_INSandUPDandDEL";
        public string sp_tblInsectsManagementInfoTransaction_INSandUPDandDEL = "dbo.sp_tblInsectsManagementInfoTransaction_INSandUPDandDEL";
        public string sp_tblPestManagementInfo_INSandUPDandDEL="dbo.sp_tblPestManagementInfo_INSandUPDandDEL";
        public string sp_tblPestManagementInfoTransaction_INSandUPDandDEL="dbo.sp_tblPestManagementInfoTransaction_INSandUPDandDEL";
        public string sp_tblWeedManagementInfo_INSandUPDandDEL="dbo.sp_tblWeedManagementInfo_INSandUPDandDEL";
        public string sp_tblWeedManagementInfoTransaction_INSandUPDandDEL = "dbo.sp_tblWeedManagementInfoTransaction_INSandUPDandDEL";
        public string sp_tblWaterInfo_INSandUPDandDEL = "dbo.sp_tblWaterInfo_INSandUPDandDEL";
        public string sp_tblWaterInfoTransaction_INSandUPDandDEL = "dbo.sp_tblWaterInfoTransaction_INSandUPDandDEL";
        public string sp_FarmingInfo_INSandUPDandDEL = "dbo.sp_FarmingInfo_INSandUPDandDEL";
		public string sp_tblCollectionTransaction_UPD = "dbo.sp_tblCollectionTransaction_UPD";
        public string sp_tblFreeze_INSandUPDandDEL = "dbo.sp_tblFreeze_INSandUPDandDEL";
	    public string sp_SupplierDetails_INSandUPDandDEL="dbo.sp_SupplierDetails_INSandUPDandDEL";
        public string sp_tblFreezeTransaction_INSandUPDandDEL = "dbo.sp_tblFreezeTransaction_INSandUPDandDEL";
		public string sp_SupplierPriceandPaymentTermsDetails_INSandUPDandDEL="dbo.sp_SupplierPriceandPaymentTermsDetails_INSandUPDandDEL";
        public string sp_GetProductCalculationDatabyDate = "dbo.sp_GetProductCalculationDatabyDate";
		public string sp_CustomAgentDetails_INSandUPDandDEL = "dbo.sp_CustomAgentDetails_INSandUPDandDEL";
        public string sp_Farm_Plantation_INSandUPDandDEL = "dbo.sp_Farm_Plantation_INSandUPDandDEL";
        public string sp_BuyerProducts_INSandUPDandDEL = "dbo.sp_BuyerProducts_INSandUPDandDEL";
        public string sp_Order_PO_UPD = "dbo.sp_Order_PO_UPD";
        public string sp_BuyerComplaintDetails_INS_UPD_DEL = "dbo.sp_BuyerComplaintDetails_INS_UPD_DEL";
        public string sp_HolidayList_INSandUPDandDEL = "dbo.sp_HolidayList_INSandUPDandDEL";
        public string sp_userLogin_UPD = "dbo.sp_userLogin_UPD";
        public string sp_Get_Detailed_Pricedetails = "dbo.sp_Get_Detailed_Pricedetails";
        public string sp_Get_All_PriceDetails_With_Date = "dbo.sp_Get_All_PriceDetails_With_Date";
        public string sp_tblBlending_INSandUPDandDEL = "dbo.sp_tblBlending_INSandUPDandDEL";
        public string sp_tblBlendingTransaction_INSandUPDandDEL = "dbo.sp_tblBlendingTransaction_INSandUPDandDEL";
        public string sp_sp_Get_New_PriceDetails = "dbo.sp_Get_New_PriceDetails";
        public string sp_Get_New_PriceDetails_With_Date = "dbo.sp_Get_New_PriceDetails_With_Date";
        public string sp_Get_PackingDetails = "dbo.sp_Get_PackingDetails";
        public string sp_GetFinicalYear = "dbo.sp_GetFinicalYear";
        public string sp_Get_Lotnumber_Year = "dbo.sp_Get_Lotnumber_Year";
        public string sp_GetProductionYear = "dbo.sp_GetProductionYear";
        public string sp_Get_NewProduction_Year = "dbo.sp_Get_NewProduction_Year";
        public string sp_TestingResults_INS_UPD_DEL = "dbo.sp_TestingResults_INS_UPD_DEL";
        public string sp_StandDetails_INSandUPDandDEL = "dbo.sp_StandDetails_INSandUPDandDEL";
        public string sp_InspectionSubmitDetails = "dbo.sp_InspectionSubmitDetails";
        public string sp_getGeneralInspectionDetails = "sp_getGeneralInspectionDetails";
        public string sp_DEL_PlantationDetails = "sp_DEL_PlantationDetails";
        public string sp_UPD_FarmPlot_PlantationDetails = "sp_UPD_FarmPlot_PlantationDetails";
        public string SP_CheckAvailableQty = "SP_CheckAvailableQty";

        //new code changes
        public string sp_GetProductDetailsBySeason = "sp_GetProductDetailsBySeason";
        public string sp_SeasonProduct = "sp_SeasonProduct_INSandUPDandDEL";
        public string sp_SeasonDetails_FarmerSeasonProduct = "sp_SeasonDetails_FarmerSeasonProduct";
        public string sp_SeasonDetails_INSandUPDandDELNew = "sp_SeasonDetails_INSandUPDandDELNew";
        public string sp_ListBlendingLotNumbers = "sp_ListBlendingLotNumbers";
        public string sp_Insert_OrderPackingDetails = "sp_Insert_OrderPackingDetails";
        public string sp_tblTestResults_INSandUPDandDEL = "sp_tblTestResults_INSandUPDandDEL";
        public string sp_tblTestingResultsTrans_INSandUPDandDEL = "sp_tblTestingResultsTrans_INSandUPDandDEL";
        public string sp_OrderDetails_ETA_Update = "sp_OrderDetails_ETA_Update";
        public string sp_Lotsample_ReceivedDate_Update="sp_Lotsample_ReceivedDate_Update";
        public string sp_Get_TestingResults = "sp_Get_TestingResults";
        public string sp_GetFreezeBatchID = "sp_GetFreezeBatchID";
        public string sp_MentholPerDetails_INSandUPDandDEL ="sp_MentholPerDetails_INSandUPDandDEL";
        public string sp_upd_BuyerPOandPathDetails = "sp_upd_BuyerPOandPathDetails";
        public string sp_DeleteBuyer = "sp_DeleteBuyer";
        public string sp_GetBlendingLotNumbers = "sp_GetBlendingLotNumbers";
        public string sp_SampleQty_Msg_Purpose = "sp_SampleQty_Msg_Purpose";
        public string sp_BranchOrderDispatchDetails_UPD = "sp_BranchOrderDispatchDetails_UPD";
        public string sp_CanceltheOrder = "sp_CanceltheOrder";
        public string sp_UpdateSoldQtyforCancelOrder = "sp_UpdateSoldQtyforCancelOrder";
        public string sp_PreorderUpdateSoldQtyforCancelOrder = "sp_PreorderUpdateSoldQtyforCancelOrder";
        public string sp_UpdateOrderproductsforLotsample = "sp_UpdateOrderproductsforLotsample";
        public string sp_FarmerProduction_UPD = "sp_FarmerProduction_UPD";
        public string sp_CusPersonalDetails_INSandUPDandDEL = "sp_CusPersonalDetails_INSandUPDandDEL";

    }
    public static class Param_Directions
    {
        public static bool Param_In = false;
        public static bool Param_Out = true;
    }
}
