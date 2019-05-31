using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.DL;

namespace MudarOrganic.BL
{
    public class FarmPlantation_BL
    {
        public  DataTable BuildNEWPlantation(int Year, int SeasonID, int ProdcutID)
        {
            DataTable dtFarmer = Farmer_DL.FarmDetails();
            DataTable dtProduct = Product_DL.GetProductDetailsbySeason(SeasonID);
            DataTable dtSeason = CategoryProduct_DL.GetSeasonDetails(SeasonID);

            DataTable NewPlantation = new DataTable();
            NewPlantation.Columns.Add("FarmerId");
            NewPlantation.Columns.Add("FarmerCode");
            NewPlantation.Columns.Add("FirstName");
            NewPlantation.Columns.Add("FarmerRegNumber");
            NewPlantation.Columns.Add("TotalAreaInHectares");
            NewPlantation.Columns.Add("FarmID");
            NewPlantation.Columns.Add("AreaCode");
            NewPlantation.Columns.Add("PlotArea");
            NewPlantation.Columns.Add("SeasonID");

            if (ProdcutID == 0)
            {
                foreach (DataRow dr in dtProduct.Rows)
                {
                    NewPlantation.Columns.Add("ProductId" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationId" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationArea" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstHarvestDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstHerbaga" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstDistillationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstDistillationUnitNO" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstProductQuantity" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondHarvestDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondHerbaga" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondDistillationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondDistillationUnitNO" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondProductQuantity" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("TotalProductQuantity" + "_" + dr["ProductId"]);
                }
            }
            else
            {
                NewPlantation.Columns.Add("ProductId");
                NewPlantation.Columns.Add("PlantationId");
                NewPlantation.Columns.Add("PlantationArea");
                NewPlantation.Columns.Add("PlantationDate");
                NewPlantation.Columns.Add("FirstHarvestDate");
                NewPlantation.Columns.Add("FirstHerbaga");
                NewPlantation.Columns.Add("FirstDistillationDate");
                NewPlantation.Columns.Add("FirstDistillationUnitNO");
                NewPlantation.Columns.Add("FirstProductQuantity");
                NewPlantation.Columns.Add("SecondHarvestDate");
                NewPlantation.Columns.Add("SecondHerbaga");
                NewPlantation.Columns.Add("SecondDistillationDate");
                NewPlantation.Columns.Add("SecondDistillationUnitNO");
                NewPlantation.Columns.Add("SecondProductQuantity");
                NewPlantation.Columns.Add("TotalProductQuantity");
            }
            int DateCount = 0;
            foreach (DataRow drFarmer in dtFarmer.Rows)
            {
                DataRow newdr = NewPlantation.NewRow();
                DataRow drseason = dtSeason.Rows[0];
                newdr["FarmerId"] = drFarmer["FarmerId"];
                newdr["FarmerCode"] = drFarmer["FarmerCode"];
                newdr["FirstName"] = drFarmer["FirstName"];
                newdr["FarmerRegNumber"] = drFarmer["FarmerRegNumber"];
                newdr["TotalAreaInHectares"] = drFarmer["TotalAreaInHectares"];
                newdr["FarmID"] = drFarmer["FarmID"];
                newdr["AreaCode"] = drFarmer["AreaCode"];
                newdr["PlotArea"] = drFarmer["PlotArea"];
                newdr["SeasonID"] = SeasonID;
                if (ProdcutID > 0)
                {
                    newdr["ProductId"] = ProdcutID;
                    newdr["PlantationId"] = "0";
                    newdr["PlantationArea"] = "0";
                    newdr["PlantationDate"] = Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount).ToShortDateString();
                    newdr["FirstHarvestDate"] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 100)).ToShortDateString();
                    newdr["FirstHerbaga"] = "0";
                    newdr["FirstDistillationDate"] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 101)).ToShortDateString();
                    newdr["FirstDistillationUnitNO"] = "0";
                    newdr["FirstProductQuantity"] = "0";
                    newdr["SecondHarvestDate"] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 145)).ToShortDateString(); ;
                    newdr["SecondHerbaga"] = "0";
                    newdr["SecondDistillationDate"] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 146)).ToShortDateString(); ;
                    newdr["SecondDistillationUnitNO"] = "0";
                    newdr["SecondProductQuantity"] = "0";
                    newdr["TotalProductQuantity"] = "0";
                }
                else
                {
                    foreach (DataRow dr in dtProduct.Rows)
                    {
                        newdr["ProductId" + "_" + dr["ProductId"]] = dr["ProductId"];
                        newdr["PlantationId" + "_" + dr["ProductId"]] = "0";
                        newdr["PlantationArea" + "_" + dr["ProductId"]] = "0";
                        newdr["PlantationDate" + "_" + dr["ProductId"]] = Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount).ToShortDateString();
                        newdr["FirstHarvestDate" + "_" + dr["ProductId"]] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 100)).ToShortDateString();
                        newdr["FirstHerbaga" + "_" + dr["ProductId"]] = "0";
                        newdr["FirstDistillationDate" + "_" + dr["ProductId"]] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 101)).ToShortDateString();
                        newdr["FirstDistillationUnitNO" + "_" + dr["ProductId"]] = "0";
                        newdr["FirstProductQuantity" + "_" + dr["ProductId"]] = "0";
                        newdr["SecondHarvestDate" + "_" + dr["ProductId"]] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 145)).ToShortDateString(); ;
                        newdr["SecondHerbaga" + "_" + dr["ProductId"]] = "0";
                        newdr["SecondDistillationDate" + "_" + dr["ProductId"]] = (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount + 146)).ToShortDateString(); ;
                        newdr["SecondDistillationUnitNO" + "_" + dr["ProductId"]] = "0";
                        newdr["SecondProductQuantity" + "_" + dr["ProductId"]] = "0";
                        newdr["TotalProductQuantity" + "_" + dr["ProductId"]] = "0";
                    }
                }
                if (DateCount < dtFarmer.Rows.Count)
                {
                    DateCount += 1;
                    if (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(DateCount) > Convert.ToDateTime(drseason["EndDate"].ToString()))
                        DateCount = 0;
                }
                NewPlantation.Rows.Add(newdr);
            }
            

            return NewPlantation;
        }
        public DataTable BuildPlantation(int Year, int SeasonID, int ProdcutID)
        {
            DataTable dtProduct = new DataTable();
            if (ProdcutID > 0)
                dtProduct = Product_DL.GetProductDetails(ProdcutID);
            else
                dtProduct = Product_DL.GetProductDetailsbySeason(SeasonID);
            DataTable dtSeason = CategoryProduct_DL.GetSeasonDetails(SeasonID);
            
            DataTable NewPlantation = new DataTable();
            NewPlantation.Columns.Add("FarmerId");
            NewPlantation.Columns.Add("FarmerCode");
            NewPlantation.Columns.Add("FirstName");
            NewPlantation.Columns.Add("FarmerRegNumber");
            NewPlantation.Columns.Add("TotalAreaInHectares");
            NewPlantation.Columns.Add("FarmID");
            NewPlantation.Columns.Add("AreaCode");
            NewPlantation.Columns.Add("PlotArea");
            NewPlantation.Columns.Add("SeasonID");

            if (ProdcutID == 0)
            {
                foreach (DataRow dr in dtProduct.Rows)
                {
                    NewPlantation.Columns.Add("ProductId" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationId" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationArea" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("PlantationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstHarvestDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstHerbaga" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstDistillationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstDistillationUnitNO" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("FirstProductQuantity" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondHarvestDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondHerbaga" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondDistillationDate" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondDistillationUnitNO" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("SecondProductQuantity" + "_" + dr["ProductId"]);
                    NewPlantation.Columns.Add("TotalProductQuantity" + "_" + dr["ProductId"]);
                }
            }
            else
            {
                NewPlantation.Columns.Add("ProductId");
                NewPlantation.Columns.Add("PlantationId");
                NewPlantation.Columns.Add("PlantationArea");
                NewPlantation.Columns.Add("PlantationDate");
                NewPlantation.Columns.Add("FirstHarvestDate");
                NewPlantation.Columns.Add("FirstHerbaga");
                NewPlantation.Columns.Add("FirstDistillationDate");
                NewPlantation.Columns.Add("FirstDistillationUnitNO");
                NewPlantation.Columns.Add("FirstProductQuantity");
                NewPlantation.Columns.Add("SecondHarvestDate");
                NewPlantation.Columns.Add("SecondHerbaga");
                NewPlantation.Columns.Add("SecondDistillationDate");
                NewPlantation.Columns.Add("SecondDistillationUnitNO");
                NewPlantation.Columns.Add("SecondProductQuantity");
                NewPlantation.Columns.Add("TotalProductQuantity");
            }
            int count = 0, rowcount = 0;
            foreach (DataRow drproduct in dtProduct.Rows)
            {
                DataTable dtplantation = FarmPlantation_DL.BuildPlantation(Year, SeasonID, Convert.ToInt32(drproduct["ProductId"].ToString()));
                rowcount = 0;
                foreach (DataRow drplantation in dtplantation.Rows)
                {
                    DataRow newdr = NewPlantation.NewRow();
                    DataRow drseason = dtSeason.Rows[0];
                    newdr["FarmerId"] = drplantation["FarmerId"];
                    newdr["FarmerCode"] = drplantation["FarmerCode"];
                    newdr["FirstName"] = drplantation["FirstName"];
                    newdr["FarmerRegNumber"] = drplantation["FarmerRegNumber"];
                    newdr["TotalAreaInHectares"] = drplantation["TotalAreaInHectares"];
                    newdr["FarmID"] = drplantation["FarmID"];
                    newdr["AreaCode"] = drplantation["AreaCode"];
                    newdr["PlotArea"] = drplantation["PlotArea"];
                    newdr["SeasonID"] = SeasonID;
                    if (ProdcutID > 0)
                    {
                        newdr["ProductId"] = ProdcutID;
                        newdr["PlantationId"] = drplantation["PlantationId"];
                        newdr["PlantationArea"] = drplantation["PlantationArea"];
                        newdr["PlantationDate"] = drplantation["PlantationDate"];//Convert.ToDateTime(drseason["StartDate"].ToString()).ToShortDateString();
                        newdr["FirstHarvestDate"] = drplantation["FirstHarvestDate"];//(Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(100)).ToShortDateString();
                        newdr["FirstHerbaga"] = drplantation["FirstHerbaga"];//"0";
                        newdr["FirstDistillationDate"] = drplantation["FirstDistillationDate"];//(Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(101)).ToShortDateString();
                        newdr["FirstDistillationUnitNO"] = drplantation["FirstDistillationUnitNO"];//"0";
                        newdr["FirstProductQuantity"] = drplantation["FirstProductQuantity"];//"0";
                        newdr["SecondHarvestDate"] = drplantation["SecondHarvestDate"];//(Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(145)).ToShortDateString(); ;
                        newdr["SecondHerbaga"] = drplantation["SecondHerbaga"];//"0";
                        newdr["SecondDistillationDate"] = drplantation["SecondDistillationDate"];//(Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(146)).ToShortDateString(); ;
                        newdr["SecondDistillationUnitNO"] = drplantation["SecondDistillationUnitNO"];//"0";
                        newdr["SecondProductQuantity"] = drplantation["SecondProductQuantity"];//"0";
                        newdr["TotalProductQuantity"] = drplantation["TotalProductQuantity"];//"0";
                    }
                    else
                    {
                        if (count == 0)
                        {
                            newdr["ProductId" + "_" + drproduct["ProductId"]] = drproduct["ProductId"];
                            newdr["PlantationId" + "_" + drproduct["ProductId"]] = drplantation["PlantationId"]; //"0";
                            newdr["PlantationArea" + "_" + drproduct["ProductId"]] = drplantation["PlantationArea"]; //"0";
                            newdr["PlantationDate" + "_" + drproduct["ProductId"]] = drplantation["PlantationDate"];//Convert.ToDateTime(drseason["StartDate"].ToString()).ToShortDateString();
                            newdr["FirstHarvestDate" + "_" + drproduct["ProductId"]] = drplantation["FirstHarvestDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(100)).ToShortDateString();
                            newdr["FirstHerbaga" + "_" + drproduct["ProductId"]] = drplantation["FirstHerbaga"];// "0";
                            newdr["FirstDistillationDate" + "_" + drproduct["ProductId"]] = drplantation["FirstDistillationDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(101)).ToShortDateString();
                            newdr["FirstDistillationUnitNO" + "_" + drproduct["ProductId"]] = drplantation["FirstDistillationUnitNO"];// "0";
                            newdr["FirstProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["FirstProductQuantity"];// "0";
                            newdr["SecondHarvestDate" + "_" + drproduct["ProductId"]] = drplantation["SecondHarvestDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(145)).ToShortDateString(); ;
                            newdr["SecondHerbaga" + "_" + drproduct["ProductId"]] = drplantation["SecondHerbaga"];// "0";
                            newdr["SecondDistillationDate" + "_" + drproduct["ProductId"]] = drplantation["SecondDistillationDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(146)).ToShortDateString(); ;
                            newdr["SecondDistillationUnitNO" + "_" + drproduct["ProductId"]] = drplantation["SecondDistillationUnitNO"];// "0";
                            newdr["SecondProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["SecondProductQuantity"];// "0";
                            newdr["TotalProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["TotalProductQuantity"];// "0";
                        }
                        if (count > 0)
                        {
                            if (rowcount < NewPlantation.Rows.Count)
                            {
                                NewPlantation.Rows[rowcount]["ProductId" + "_" + drproduct["ProductId"]] = drproduct["ProductId"];
                                NewPlantation.Rows[rowcount]["PlantationId" + "_" + drproduct["ProductId"]] = drplantation["PlantationId"]; //"0";
                                NewPlantation.Rows[rowcount]["PlantationArea" + "_" + drproduct["ProductId"]] = drplantation["PlantationArea"]; //"0";
                                NewPlantation.Rows[rowcount]["PlantationDate" + "_" + drproduct["ProductId"]] = drplantation["PlantationDate"];//Convert.ToDateTime(drseason["StartDate"].ToString()).ToShortDateString();
                                NewPlantation.Rows[rowcount]["FirstHarvestDate" + "_" + drproduct["ProductId"]] = drplantation["FirstHarvestDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(100)).ToShortDateString();
                                NewPlantation.Rows[rowcount]["FirstHerbaga" + "_" + drproduct["ProductId"]] = drplantation["FirstHerbaga"];// "0";
                                NewPlantation.Rows[rowcount]["FirstDistillationDate" + "_" + drproduct["ProductId"]] = drplantation["FirstDistillationDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(101)).ToShortDateString();
                                NewPlantation.Rows[rowcount]["FirstDistillationUnitNO" + "_" + drproduct["ProductId"]] = drplantation["FirstDistillationUnitNO"];// "0";
                                NewPlantation.Rows[rowcount]["FirstProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["FirstProductQuantity"];// "0";
                                NewPlantation.Rows[rowcount]["SecondHarvestDate" + "_" + drproduct["ProductId"]] = drplantation["SecondHarvestDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(145)).ToShortDateString(); ;
                                NewPlantation.Rows[rowcount]["SecondHerbaga" + "_" + drproduct["ProductId"]] = drplantation["SecondHerbaga"];// "0";
                                NewPlantation.Rows[rowcount]["SecondDistillationDate" + "_" + drproduct["ProductId"]] = drplantation["SecondDistillationDate"];// (Convert.ToDateTime(drseason["StartDate"].ToString()).AddDays(146)).ToShortDateString(); ;
                                NewPlantation.Rows[rowcount]["SecondDistillationUnitNO" + "_" + drproduct["ProductId"]] = drplantation["SecondDistillationUnitNO"];// "0";
                                NewPlantation.Rows[rowcount]["SecondProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["SecondProductQuantity"];// "0";
                                NewPlantation.Rows[rowcount]["TotalProductQuantity" + "_" + drproduct["ProductId"]] = drplantation["TotalProductQuantity"];// "0";
                            }
                        }
                    }
                    if (count == 0)
                        NewPlantation.Rows.Add(newdr);
                    rowcount += 1;
                }
                count += 1;
            }

            return NewPlantation;
        }

        public DataTable BuildPlantation(int Year, int SeasonID, int ProdcutID, string farmerID)
        {
            return FarmPlantation_DL.BuildPlantation(Year, SeasonID, ProdcutID, farmerID);
        }

        public DataTable GetPlotFarmPlantationDetails(int Year, int SeasonID, int ProdcutID, int parentFarmID)
        {
            return FarmPlantation_DL.GetPlotFarmPlantationDetails(Year, SeasonID, ProdcutID, parentFarmID);
        }

        public DataTable GetParentFarmId( string areaCode)
        {
            return FarmPlantation_DL.GetParentFarmId( areaCode);
        }
        
        public bool PlantationDetails_INSandUPDandDEL(string FarmerId, int ProductId, string UnitId, int FarmID, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos)
        {
            return FarmPlantation_DL.PlantationDetails_INSandUPDandDEL(FarmerId, ProductId, UnitId, FarmID,
                PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate, 
                FirstDistillationUnitNO, FirstProductQuantity, SecondHarvestDate, 
                SecondHerbaga, SecondDistillationDate, SecondDistillationUnitNO, 
                SecondProductQuantity, TotalProductQuantity, ProposedFieldOfficer, 
                ProposedManager, "Shaik Aslam", "Shaik Aslam", SeasonID, PlantationArea, 
                TypeOfOperation, PlantationId,FirstUnitId,SecondUnitId,FirstNoOfLots,SecondNoOfLots,FirstLotNos,SecondLotNos);
        }
        public bool Farm_PlantationDetails_INSandUPDandDEL(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, decimal Latitude, decimal Longitude, int ParentFarmID, int ProductId, string UnitId, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos, int FYear, bool IsInterCrop, decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)
        {
            return FarmPlantation_DL.Farm_PlantationDetails_INSandUPDandDEL(FarmerId, FarmID, PlotArea, AreaCode, Latitude, Longitude, ParentFarmID, ProductId, UnitId, 
                PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate,
                FirstDistillationUnitNO, FirstProductQuantity, SecondHarvestDate,
                SecondHerbaga, SecondDistillationDate, SecondDistillationUnitNO,
                SecondProductQuantity, TotalProductQuantity, ProposedFieldOfficer,
                ProposedManager, "Shaik Aslam", "Shaik Aslam", SeasonID, PlantationArea,
                TypeOfOperation, PlantationId, FirstUnitId, SecondUnitId, FirstNoOfLots, SecondNoOfLots, FirstLotNos, SecondLotNos, FYear, IsInterCrop, EstiFiHerbage, EstiFOilqty, EstiSeHerbage, EstiSeOilqty);
        }

        public bool sp_FarmerProduction_UPD(DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate,decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
         , decimal SecondProductQuantity, decimal TotalProductQuantity,
           int PlantationId,decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)

        {
            return FarmPlantation_DL.sp_FarmerProduction_UPD(PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate, FirstProductQuantity
            , SecondHarvestDate, SecondHerbaga, SecondDistillationDate, SecondProductQuantity, TotalProductQuantity,
            PlantationId, EstiFiHerbage, EstiFOilqty, EstiSeHerbage, EstiSeOilqty);
        }



        //Karun Added
        public DataTable BindDropDownChild()
        {
            return FarmPlantation_DL.BindDropDownChild();
        }
        public bool SoldQuantity_Update(int PlantationId, decimal SoldTotalQty, string ModifiedBy)
        {
            return FarmPlantation_DL.SoldQuantity_Update(PlantationId, SoldTotalQty, ModifiedBy);
        }
        public DataTable GetFarmerFarmdetails(string FarmerID, int year)
        {
            return FarmPlantation_DL.GetFarmerFarmdetails(FarmerID, year);
        }
        public DataTable GetPlantation(int Year, int SeasonID, int ProdcutID, string farmerID)
        {
            return FarmPlantation_DL.GetPlantation(Year, SeasonID, ProdcutID, farmerID);
        }
        public bool UpdatePlantationDetails(string FarmerId, int FarmID, decimal PlotArea, string AreaCode, decimal Latitude, decimal Longitude, int ParentFarmID, int ProductId, string UnitId, DateTime PlantationDate, DateTime FirstHarvestDate, decimal FirstHerbaga, DateTime FirstDistillationDate, int FirstDistillationUnitNO, decimal FirstProductQuantity
            , DateTime SecondHarvestDate, decimal SecondHerbaga, DateTime SecondDistillationDate
           , int SecondDistillationUnitNO, decimal SecondProductQuantity, decimal TotalProductQuantity, bool ProposedFieldOfficer, bool ProposedManager
           , string CreatedBy, string ModifiedBy, int SeasonID, decimal PlantationArea, int TypeOfOperation, int PlantationId, string FirstUnitId, string SecondUnitId,
            int FirstNoOfLots, int SecondNoOfLots, string FirstLotNos, string SecondLotNos, int FYear, bool IsInterCrop, decimal EstiFiHerbage, decimal EstiFOilqty, decimal EstiSeHerbage, decimal EstiSeOilqty)
        {
            return FarmPlantation_DL.UpdatePlantationDetails(FarmerId, FarmID, PlotArea, AreaCode, Latitude, Longitude, ParentFarmID, ProductId, UnitId,
                PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate,
                FirstDistillationUnitNO, FirstProductQuantity, SecondHarvestDate,
                SecondHerbaga, SecondDistillationDate, SecondDistillationUnitNO,
                SecondProductQuantity, TotalProductQuantity, ProposedFieldOfficer,
                ProposedManager, "Shaik Aslam", "Shaik Aslam", SeasonID, PlantationArea,
                TypeOfOperation, PlantationId, FirstUnitId, SecondUnitId, FirstNoOfLots, SecondNoOfLots, FirstLotNos, SecondLotNos, FYear, IsInterCrop, EstiFiHerbage, EstiFOilqty, EstiSeHerbage, EstiSeOilqty);
        }
        public bool DeletePlantationDetails(int FarmID, string ModifiedBy)
        {
            return FarmPlantation_DL.DeletePlantationDetails(FarmID,ModifiedBy);
        }
        public DataTable GetAviableQtty(int ProductID)
        {
             return FarmPlantation_DL.GetAviableQtty(ProductID);
        }
        public DataTable GetProductionDataOnProductID(int ProductID, string Year, string ICStype)
        {
            return FarmPlantation_DL.GetProductionDataOnProductID(ProductID, Year, ICStype);
        }
    }
}
