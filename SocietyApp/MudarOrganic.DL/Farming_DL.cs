using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.Components;
using System.Data.SqlClient;

namespace MudarOrganic.DL
{
    public static class Farming_DL
    {
        #region PlantingInfo
        public static DataTable GetPlantingInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblplantingInformation WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID);
        }
        public static DataTable GetPlantingInfo(string farmerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblplantingInformation WHERE [delete] = 0 ");
        }
        public static bool PlantingInfo_INT_UPT_DEL(ref int rPlantingID, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string PlantingSource, string PlantingBill_Date, string PlantingSeedVariety, string PlantingSeedTreatMent, string PlantingQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, int PlantingID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("PlantingSource", SqlDbType.NVarChar, PlantingSource));
            Params.Add(mdbh.AddParameter("PlantingBill_Date", SqlDbType.NVarChar, PlantingBill_Date));
            Params.Add(mdbh.AddParameter("PlantingSeedVariety", SqlDbType.NVarChar, PlantingSeedVariety));
            Params.Add(mdbh.AddParameter("PlantingSeedTreatMent", SqlDbType.NVarChar, PlantingSeedTreatMent));
            Params.Add(mdbh.AddParameter("PlantingQuantity", SqlDbType.NVarChar, PlantingQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rPlantingID", SqlDbType.Int, rPlantingID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("PlantingID", SqlDbType.Int, PlantingID));
            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblPlantingInformation_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    PlantingID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Input Information
        public static DataSet GetInputInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(InputMID as Nvarchar(50)) AS 'S_InputMID'  FROM tblInputInformation WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " SELECT *, CAST(InputMID as Nvarchar(50)) AS 'S_InputMID' FROM tblInputTransaction WHERE InputMID in (SELECT InputMID FROM tblInputInformation WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + ")";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataSet GetInputInfo()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //string sql = "select IMMaterial,IMSource,it.IMDays from tblInputInformation ii,tblInputTransaction it where ii.InputMID = it.InputMTId and ii.ProductID > 0";
            string sql = "SELECT *, CAST(InputMID as Nvarchar(50)) AS 'S_InputMID'  FROM tblInputInformation WHERE [delete]=0 SELECT *, CAST(InputMID as Nvarchar(50)) AS 'S_InputMID' FROM tblInputTransaction WHERE InputMID in (SELECT InputMID FROM tblInputInformation WHERE [delete]=0)";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataTable GetInputInfoonProductID(string ProdutID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "select ii.IMMaterial,ii.IMSource,it.IM_MT_HC,it.IMDays,it.IMPeriod,it.IMPlanting from tblInputTransaction it,tblInputInformation ii where ii.InputMID =it.InputMID and ii.ProductID='" + ProdutID + "' and ii.[delete]=0 and it.[delete]=0";
            return mdbh.ExecuteDataTable(sql);
        }
        public static bool InputInformation_INT_UPT_DEL(string FarmerID, int ProductID, int SeasonID, string SeasonYear, string IMMaterial, string IMSource, string IMBillNo, DateTime IMDate, string IMQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rInputMID, int InputMID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("IMSource", SqlDbType.NVarChar, IMSource));
            Params.Add(mdbh.AddParameter("IMMaterial", SqlDbType.NVarChar, IMMaterial));
            Params.Add(mdbh.AddParameter("IMBillNo", SqlDbType.NVarChar, IMBillNo));
            Params.Add(mdbh.AddParameter("IMDate", SqlDbType.DateTime, IMDate));
            Params.Add(mdbh.AddParameter("IMQuantity", SqlDbType.NVarChar, IMQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rInputMID", SqlDbType.Int, rInputMID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("InputMID", SqlDbType.Int, InputMID));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblInputInformation_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    rInputMID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool InputTransaction_INT_UPT_DEL(int InputMID, string IM_MT_HC, string IMDays, string IMPeriod, string IMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int InputMTId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("InputMID", SqlDbType.Int, InputMID));
            Params.Add(mdbh.AddParameter("IM_MT_HC", SqlDbType.NVarChar, IM_MT_HC));
            Params.Add(mdbh.AddParameter("IMDays", SqlDbType.NVarChar, IMDays));
            Params.Add(mdbh.AddParameter("IMPeriod", SqlDbType.NVarChar, IMPeriod));
            Params.Add(mdbh.AddParameter("IMPlanting", SqlDbType.NVarChar, IMPlanting));
            Params.Add(mdbh.AddParameter("InputMTId", SqlDbType.Int, InputMTId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            //Params.Add(mdbh.AddParameter("rInputMTId", SqlDbType.Int, rInputMTId, Param_Directions.Param_Out)); 
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblInputTransaction_INSandUPDandDEL, Params);
                //if (output.Count >= 2)
                //{
                //    Result = Convert.ToBoolean(output[0]);
                //    rInputMTId = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Disease Management
        public static DataSet GetDisease(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(DiseaseMID as Nvarchar(50)) AS 'S_DiseaseMID'  FROM dbo.tblDiseaseManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " AND [Delete] = 0  SELECT *, CAST(DiseaseMID as Nvarchar(50)) AS 'S_DiseaseMID' FROM dbo.tblDiseaseManagementInfoTransaction WHERE DiseaseMID in (SELECT DiseaseMID FROM tblDiseaseManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " AND [Delete] = 0)";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataSet GetDisease()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(DiseaseMID as Nvarchar(50)) AS 'S_DiseaseMID'  FROM dbo.tblDiseaseManagementInfo WHERE  [Delete] = 0  SELECT *, CAST(DiseaseMID as Nvarchar(50)) AS 'S_DiseaseMID' FROM dbo.tblDiseaseManagementInfoTransaction WHERE DiseaseMID in (SELECT DiseaseMID FROM tblDiseaseManagementInfo WHERE [Delete] = 0)";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataTable GetDiseInfoonProductID(string ProdutID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "select di.DiseaseName,DMIExpected,DMIPreventionMaterial,DMISource,DMIBillNo,dt.DMIT_HC,DMIT_Days,DMIT_Period,DMIT_Planting from tblDiseaseManagementInfo di,tblDiseaseManagementInfoTransaction dt where di.DiseaseMID =dt.DiseaseMID AND di.ProductID ='" + ProdutID + "'";
            return mdbh.ExecuteDataTable(sql);
        }
        public static bool DiseaseInfo_INT_UPT_DEL(string DiseaseName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string DMIExpected, string DMIObserved, string DMIPreventionMaterial, string DMISource, string DMIBillNo, DateTime DMIDate, string DMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rDiseaseMID, int DiseaseMID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("DiseaseName", SqlDbType.NVarChar, DiseaseName));
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("DMIExpected", SqlDbType.NVarChar, DMIExpected));
            Params.Add(mdbh.AddParameter("DMIObserved", SqlDbType.NVarChar, DMIObserved));
            Params.Add(mdbh.AddParameter("DMIPreventionMaterial", SqlDbType.NVarChar, DMIPreventionMaterial));
            Params.Add(mdbh.AddParameter("DMISource", SqlDbType.NVarChar, DMISource));
            Params.Add(mdbh.AddParameter("DMIBillNo", SqlDbType.NVarChar, DMIBillNo));
            Params.Add(mdbh.AddParameter("DMIDate", SqlDbType.DateTime, DMIDate));
            Params.Add(mdbh.AddParameter("DMIQuantity", SqlDbType.NVarChar, DMIQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rDiseaseMID", SqlDbType.Int, rDiseaseMID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("DiseaseMID", SqlDbType.Int, DiseaseMID));
            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblDiseaseManagementInfo_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    @rDiseaseMID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool DiseaseTransaction_INT_UP_DEL(int DiseaseMID, string DM_MT_HC, string DMDays, string DMPeriod, string DMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int DMITId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("DiseaseMID", SqlDbType.Int, DiseaseMID));
            Params.Add(mdbh.AddParameter("DMIT_HC", SqlDbType.NVarChar, DM_MT_HC));
            Params.Add(mdbh.AddParameter("DMIT_Days", SqlDbType.NVarChar, DMDays));
            Params.Add(mdbh.AddParameter("DMIT_Period", SqlDbType.NVarChar, DMPeriod));
            Params.Add(mdbh.AddParameter("DMIT_Planting", SqlDbType.NVarChar, DMPlanting));
            Params.Add(mdbh.AddParameter("DMITId", SqlDbType.Int, DMITId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblDiseaseManagementInfoTransaction_INSandUPDandDEL, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Insect Management
        public static DataSet GetInsectInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(InsectMIID as Nvarchar(50)) AS 'S_InsectMIID'  FROM tblInsectsManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " SELECT *, CAST(InsectMIID as Nvarchar(50)) AS 'S_InsectMIID' FROM tblInsectsManagementInfoTransaction WHERE InsectMIID in (SELECT InsectMIID FROM tblInsectsManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + ")";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataSet GetInsectInfo()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(InsectMIID as Nvarchar(50)) AS 'S_InsectMIID'  FROM tblInsectsManagementInfo WHERE [Delete] = 0  SELECT *, CAST(InsectMIID as Nvarchar(50)) AS 'S_InsectMIID' FROM tblInsectsManagementInfoTransaction WHERE InsectMIID in (SELECT InsectMIID FROM tblInsectsManagementInfo WHERE [Delete] = 0)";
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataTable GetInsectInfoonProduct(string ProdutID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "select imi.InsectName,IMIExpected,IMIPreventionMaterial,IMISource,IMIBillNo,imt.InsectM_MT_HC,InsectMDays,InsectMPeriod,InsectMPlanting from tblInsectsManagementInfo imi,tblInsectsManagementInfoTransaction imt where imi.InsectMIID= imt.InsectMIID AND imi.ProductID='" + ProdutID + "'";
            return mdbh.ExecuteDataTable(sql);
        }
        public static bool InsectInfo_INT_UPT_DEL(string InsectName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string IMIExpected, string IMIObserved, string IMIPreventionMaterial, string IMISource, string IMIBillNo, DateTime IMIDate, string IMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rInsectMIID, int InsectMIID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("InsectName", SqlDbType.NVarChar, InsectName));
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("IMIExpected", SqlDbType.NVarChar, IMIExpected));
            Params.Add(mdbh.AddParameter("IMIObserved", SqlDbType.NVarChar, IMIObserved));
            Params.Add(mdbh.AddParameter("IMIPreventionMaterial", SqlDbType.NVarChar, IMIPreventionMaterial));
            Params.Add(mdbh.AddParameter("IMISource", SqlDbType.NVarChar, IMISource));
            Params.Add(mdbh.AddParameter("IMIBillNo", SqlDbType.NVarChar, IMIBillNo));
            Params.Add(mdbh.AddParameter("IMIDate", SqlDbType.DateTime, IMIDate));
            Params.Add(mdbh.AddParameter("IMIQuantity", SqlDbType.NVarChar, IMIQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rInsectMIID", SqlDbType.Int, rInsectMIID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("InsectMIID", SqlDbType.Int, InsectMIID));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblInsectsManagementInfo_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    rInsectMIID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool InsectTransaction_INT_UP_DEL(int InsectMIID, string InsectM_MT_HC, string InsectMDays, string InsectMPeriod, string InsectMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int InsectMTId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("InsectMIID", SqlDbType.Int, InsectMIID));
            Params.Add(mdbh.AddParameter("InsectM_MT_HC ", SqlDbType.NVarChar, InsectM_MT_HC));
            Params.Add(mdbh.AddParameter("InsectMDays", SqlDbType.NVarChar, InsectMDays));
            Params.Add(mdbh.AddParameter("InsectMPeriod", SqlDbType.NVarChar, InsectMPeriod));
            Params.Add(mdbh.AddParameter("InsectMPlanting", SqlDbType.NVarChar, InsectMPlanting));
            Params.Add(mdbh.AddParameter("InsectMTId", SqlDbType.Int, InsectMTId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblInsectsManagementInfoTransaction_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Pest Management
        public static DataSet GetPestInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(PestMIID as Nvarchar(50)) AS 'S_PestMIID'  FROM tblPestManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " SELECT *, CAST(PestMIID as Nvarchar(50)) AS 'S_PestMIID' FROM tblPestManagementInfoTransaction WHERE PestMIID in (SELECT PestMIID FROM tblPestManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + ")";
            return mdbh.ExecuteDataSet(sql);
        }
        public static bool PestInfo_INT_UPT_DEL(string PestName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string PMIExpected, string PMIObserved, string PMIPreventionMaterial, string PMISource, string PMIBillNo, DateTime PMIDate, string PMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rPestMIID, int PestMIID)
        {

            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PestName", SqlDbType.NVarChar, PestName));
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("PMIExpected", SqlDbType.NVarChar, PMIExpected));
            Params.Add(mdbh.AddParameter("PMIObserved", SqlDbType.NVarChar, PMIObserved));
            Params.Add(mdbh.AddParameter("PMIPreventionMaterial", SqlDbType.NVarChar, PMIPreventionMaterial));
            Params.Add(mdbh.AddParameter("PMISource", SqlDbType.NVarChar, PMISource));
            Params.Add(mdbh.AddParameter("PMIBillNo", SqlDbType.NVarChar, PMIBillNo));
            Params.Add(mdbh.AddParameter("PMIDate", SqlDbType.DateTime, PMIDate));
            Params.Add(mdbh.AddParameter("PMIQuantity", SqlDbType.NVarChar, PMIQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rPestMIID", SqlDbType.Int, rPestMIID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("PestMIID", SqlDbType.Int, PestMIID));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblPestManagementInfo_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    rPestMIID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool PestTransaction_INT_UPT_DEL(int PestMIID, string PM_MT_HC, string PMDays, string PMPeriod, string PMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int PMITId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PestMIID", SqlDbType.Int, PestMIID));
            Params.Add(mdbh.AddParameter("PMIT_HC", SqlDbType.NVarChar, PM_MT_HC));
            Params.Add(mdbh.AddParameter("PMIT_Days", SqlDbType.NVarChar, PMDays));
            Params.Add(mdbh.AddParameter("PMIT_Period", SqlDbType.NVarChar, PMPeriod));
            Params.Add(mdbh.AddParameter("PMIT_Planting", SqlDbType.NVarChar, PMPlanting));
            Params.Add(mdbh.AddParameter("PMITId", SqlDbType.Int, PMITId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPestManagementInfoTransaction_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Weed Management
        public static DataSet GetWeedInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(WeedMIID as Nvarchar(50)) AS 'S_WeedMIID'  FROM tblWeedManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " SELECT *, CAST(WeedMIID as Nvarchar(50)) AS 'S_WeedMIID' FROM tblWeedManagementInfoTransaction WHERE WeedMIID in (SELECT WeedMIID FROM tblWeedManagementInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + ")";
            return mdbh.ExecuteDataSet(sql);
        }
        public static bool WeedInfo_INT_UPT_DEL(string WeedName, string FarmerID, int ProductID, int SeasonID, string SeasonYear, string WMIExpected, string WMIObserved, string WMIPreventionMaterial, string WMISource, string WMIBillNo, DateTime WMIDate, string WMIQuantity, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rWeedMIID, int WeedMIID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("WeedName", SqlDbType.NVarChar, WeedName));
            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("WMIExpected", SqlDbType.NVarChar, WMIExpected));
            Params.Add(mdbh.AddParameter("WMIObserved", SqlDbType.NVarChar, WMIObserved));
            Params.Add(mdbh.AddParameter("WMIPreventionMaterial", SqlDbType.NVarChar, WMIPreventionMaterial));
            Params.Add(mdbh.AddParameter("WMISource", SqlDbType.NVarChar, WMISource));
            Params.Add(mdbh.AddParameter("WMIBillNo", SqlDbType.NVarChar, WMIBillNo));
            Params.Add(mdbh.AddParameter("WMIDate", SqlDbType.DateTime, WMIDate));
            Params.Add(mdbh.AddParameter("WMIQuantity", SqlDbType.NVarChar, WMIQuantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rWeedMIID", SqlDbType.Int, rWeedMIID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("WeedMIID", SqlDbType.Int, WeedMIID));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblWeedManagementInfo_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    rWeedMIID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public static bool WeedTransaction_INT_UPT_DEL(int WeedMIID, string WM_MT_HC, string WMDays, string WMPeriod, string WMPlanting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int WMITId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("WeedMIID", SqlDbType.Int, WeedMIID));
            Params.Add(mdbh.AddParameter("WMIT_HC", SqlDbType.NVarChar, WM_MT_HC));
            Params.Add(mdbh.AddParameter("WMIT_Days", SqlDbType.NVarChar, WMDays));
            Params.Add(mdbh.AddParameter("WMIT_Period", SqlDbType.NVarChar, WMPeriod));
            Params.Add(mdbh.AddParameter("WMIT_Planting", SqlDbType.NVarChar, WMPlanting));
            Params.Add(mdbh.AddParameter("WMITId", SqlDbType.Int, WMITId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblWeedManagementInfoTransaction_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region Water Management
        public static DataSet GetWaterInfo(string farmerID, string productID, string seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT *, CAST(WaterMID as Nvarchar(50)) AS 'S_WaterMID'  FROM tblWaterInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + " SELECT *, CAST(WaterMID as Nvarchar(50)) AS 'S_WaterMID' FROM tblWaterInfoTransaction WHERE WaterMID in (SELECT WaterMID FROM tblWaterInfo WHERE FarmerID = '" + farmerID + "' AND ProductID =" + productID + " AND SeasonID = " + seasonID + ")";
            return mdbh.ExecuteDataSet(sql);
        }
        public static bool WaterInfo_INT_UPT_DEL(string FarmerID, int ProductID, int SeasonID, string SeasonYear, string WISource, string WIOrganicF, string WIFCSource, string WIFCWaterFlow, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int rWaterMID, int WaterMID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("FarmerID", SqlDbType.UniqueIdentifier, new Guid(FarmerID)));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("SeasonYear", SqlDbType.NVarChar, SeasonYear));
            Params.Add(mdbh.AddParameter("WISource", SqlDbType.NVarChar, WISource));
            Params.Add(mdbh.AddParameter("WIOrganicF", SqlDbType.NVarChar, WIOrganicF));
            Params.Add(mdbh.AddParameter("WIFCSource", SqlDbType.NVarChar, WIFCSource));
            Params.Add(mdbh.AddParameter("WIFCWaterFlow", SqlDbType.NVarChar, WIFCWaterFlow));

            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("rWaterMID", SqlDbType.Int, rWaterMID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("WaterMID", SqlDbType.Int, WaterMID));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblWaterInfo_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    rWaterMID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public static bool WaterTransaction_INT_UPT_DEL(int WaterMID, string WITT_Irrigation, string WITT_Days, string WITT_Period, string WITT_Planting, string CreatedBy, string ModifiedBy, int TypeOfOperation, int WITTId)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("WaterMID", SqlDbType.Int, WaterMID));

            Params.Add(mdbh.AddParameter("WITT_Irrigation", SqlDbType.NVarChar, WITT_Irrigation));
            Params.Add(mdbh.AddParameter("WITT_Days", SqlDbType.NVarChar, WITT_Days));
            Params.Add(mdbh.AddParameter("WITT_Period", SqlDbType.NVarChar, WITT_Period));
            Params.Add(mdbh.AddParameter("WITT_Planting", SqlDbType.NVarChar, WITT_Planting));
            Params.Add(mdbh.AddParameter("WITTId", SqlDbType.Int, WITTId));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblWaterInfoTransaction_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion

        #region BasicFarmingInfo
        public static bool FarmingInfo_INSandUPDandDEL(int Year, int ProductID, int SeasonID, DateTime PlantationFrom, DateTime PlantationTo, int FirstCutFrom, int FirstCutTo, decimal Qty1stCutHCFrom, decimal Qty1stCutHCTo, decimal FirstRecoveryOilFrom, decimal FirstRecoveryOilTo, int SecondCutFrom, int SecondCutTo, decimal Qty2ndCutHCFrom, decimal Qty2ndCutHCTo, decimal SecondRecoveryOilFrom, decimal SecondRecoveryOilTo, int EsvsAc_From, int EsvsAc_To, string CreatedBy, string ModifiedBy, int TypeOfOperation, int FarmingInfoID)
        {

            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Year", SqlDbType.Int, Year));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SeasonID", SqlDbType.Int, SeasonID));
            Params.Add(mdbh.AddParameter("PlantationFrom", SqlDbType.DateTime, PlantationFrom));
            Params.Add(mdbh.AddParameter("PlantationTo", SqlDbType.DateTime, PlantationTo));
            Params.Add(mdbh.AddParameter("1stCutFrom", SqlDbType.Int, FirstCutFrom));
            Params.Add(mdbh.AddParameter("1stCutTo", SqlDbType.Int, FirstCutTo));
            Params.Add(mdbh.AddParameter("Qty1stCutHCFrom", SqlDbType.Decimal, Qty1stCutHCFrom));
            Params.Add(mdbh.AddParameter("Qty1stCutHCTo", SqlDbType.Decimal, Qty1stCutHCTo));
            Params.Add(mdbh.AddParameter("1stRecoveryOilFrom", SqlDbType.Decimal, FirstRecoveryOilFrom));
            Params.Add(mdbh.AddParameter("1stRecoveryOilTo", SqlDbType.Decimal, FirstRecoveryOilTo));
            Params.Add(mdbh.AddParameter("2ndCutFrom", SqlDbType.Int, SecondCutFrom));
            Params.Add(mdbh.AddParameter("2ndCutTo", SqlDbType.Int, SecondCutTo));
            Params.Add(mdbh.AddParameter("Qty2ndCutHCFrom", SqlDbType.Decimal, Qty2ndCutHCFrom));
            Params.Add(mdbh.AddParameter("Qty2ndCutHCTo", SqlDbType.Decimal, Qty2ndCutHCTo));
            Params.Add(mdbh.AddParameter("2ndRecoveryOilFrom", SqlDbType.Decimal, SecondRecoveryOilFrom));
            Params.Add(mdbh.AddParameter("2ndRecoveryOilTo", SqlDbType.Decimal, SecondRecoveryOilTo));
            Params.Add(mdbh.AddParameter("EsvsAc_From", SqlDbType.Int, EsvsAc_From));
            Params.Add(mdbh.AddParameter("EsvsAc_To", SqlDbType.Int, EsvsAc_To));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("FarmingInfoID", SqlDbType.Int, FarmingInfoID));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_FarmingInfo_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }
        public static DataTable GetBasicFarmingInfo(int year, int productID, int seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT  FI.*,convert(char(11),PlantationFrom,106) as PlantFDate,convert(char(11),PlantationTo,106) as PlantTDate FROM tblFarmingInfo fi WHERE Year = '" + year + "' AND ProductID ='" + productID + " 'AND SeasonID =' " + seasonID + " ' AND [Delete] = 0");
        }
        public static DataTable GetProductionInfo(int productID, int seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblPlantationDetails where ProductId='" + productID + "' and SeasonID='" + seasonID + "'");
        }
        public static DataTable GetBasicFarmingInfo(int year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT  fi.FarmingInfoID,fi.[YEAR],se.SeasonName,pd.ProductName,fi.PlantationFrom as PlantFDate,fi.PlantationTo as PlantTDate FROM tblFarmingInfo fi,tblSeason se,tblProductDetails pd WHERE Year = '" + year + "' AND fi.ProductID = pd.ProductId AND fi.SeasonID =se.SeasonID  AND fi.[Delete] = 0 order by se.SeasonName");
        }
        public static DataTable GetBasicFarmingInfoBasedon(int year, int seasonID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT  fi.FarmingInfoID,fi.[YEAR],se.SeasonName,pd.ProductName,fi.PlantationFrom as PlantFDate,fi.PlantationTo as PlantTDate FROM tblFarmingInfo fi,tblSeason se,tblProductDetails pd WHERE Year = '" + year + "' AND fi.ProductID = pd.ProductId AND fi.SeasonID = '" + seasonID + "'  AND  se.SeasonID= '" + seasonID + "'  AND fi.[Delete] = 0 order by se.SeasonName");
        }
        public static DataTable GetBasicFarmingInfoBasedonID(int FarmingInfoID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblFarmingInfo where FarmingInfoID='" + FarmingInfoID + "' AND [Delete] = 0");
        }
        #endregion

        //aslam by write by code 15-06-2013
        public static DataTable GetProductDetailsByYear(string Year)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            // return mdbh.ExecuteDataTable("SELECT sd.*, pd.* FROM tblSeason sd, tblProductDetails pd WHERE  SeasonYear = '" + Year + "'  AND pd.SeasonID = sd.SeasonID ");
            return mdbh.ExecuteDataTable("select ts.*,tpd.* from tblSeason ts join tblSeasonProducts tsp on ts.SeasonID=tsp.SeasonId join tblProductDetails tpd on tsp.ProductId=tpd.ProductId where ts.SeasonYear='" + Year + "'");
        }
    }
}
