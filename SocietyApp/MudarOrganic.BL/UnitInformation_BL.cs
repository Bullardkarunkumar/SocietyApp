using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class UnitInformation_BL
    {
        public bool UnitInformationDetails_INSandUPDandDEL(string UnitId, string Name, string Ucode, string Uowner, string Address, int RawRequired, string OutputState, string OutputMaterial, string CapacityOfPlant, string LotsOfProducesSimultaneously, int PermanentLabour, int TemporaryLabour, int ChildLabour, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return UnitInformation_DL.UnitInformationDetails_INSandUPDandDEL(UnitId, Name, Ucode, Uowner , Address, RawRequired, OutputState, OutputMaterial, CapacityOfPlant, LotsOfProducesSimultaneously, PermanentLabour, TemporaryLabour, ChildLabour, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public bool UnitInformationDetails_INSandUPDandDEL_new(string UnitId, string Name, string Ucode, string Uowner, string Address, int RawRequired, string OutputState, string OutputMaterial, string CapacityOfPlant, string LotsOfProducesSimultaneously, int PermanentLabour, int TemporaryLabour, int ChildLabour, string CreatedBy, string ModifiedBy, int TypeOfOperation, string Unit_Village)
        {
            return UnitInformation_DL.UnitInformationDetails_INSandUPDandDEL_new(UnitId, Name, Ucode, Uowner, Address, RawRequired, OutputState, OutputMaterial, CapacityOfPlant, LotsOfProducesSimultaneously, PermanentLabour, TemporaryLabour, ChildLabour, CreatedBy, ModifiedBy, TypeOfOperation, Unit_Village);
        }
        public bool UnitInformation(string UnitID, ref DataTable dtUnitInfo)
        {
            return UnitInformation_DL.UnitInformation(UnitID, ref dtUnitInfo);
        }
        public DataTable UnitInformation()
        {
            return UnitInformation_DL.UnitInformation();
        }
        public DataSet UnitInformation(string UnitID)
        {
            return UnitInformation_DL.UnitInformation(UnitID);
        }
        public  DataTable GetUnitInofBasedonICS(string ICSVillage)
        {
            return UnitInformation_DL.GetUnitInofBasedonICS(ICSVillage);
        }
        public DataTable FarmersVillageList()
        {
            return UnitInformation_DL.FarmersVillageList();
        }
        public DataTable FarmersVillageListByIcs(string icsType)
        {
            return UnitInformation_DL.FarmersVillageListByIcs(icsType);
        }
        public DataTable FarmersVillageList(string FarmerID)
        {
            return UnitInformation_DL.FarmersVillageList(FarmerID);
        }
        public DataTable UnitInformationBasedOnVillage(string Village)
        {
            return UnitInformation_DL.UnitInformationBasedOnVillage(Village);
        }
        public DataTable GetDisitlationUnits(string UnitID, string ICSType, string Year)
        {
            return UnitInformation_DL.GetDisitlationUnits(UnitID, ICSType, Year);
        }
        public DataTable GetDisitlationUnits(string ICSType)
        {
            return UnitInformation_DL.GetDisitlationUnits(ICSType);
        }
        public DataTable GetDataP1(string ICSType, string Year)
        {
            return UnitInformation_DL.GetDataP1(ICSType, Year);
        }
        public DataTable GetDataP2(string ICSType, string Year)
        {
            return UnitInformation_DL.GetDataP2(ICSType, Year);
        }
        public DataTable GetDataP3(string ICSType, string Year)
        {
            return UnitInformation_DL.GetDataP3(ICSType, Year);
        }
    }
}