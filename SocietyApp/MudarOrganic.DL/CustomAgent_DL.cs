using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class CustomAgent_DL
    {
        public static bool CustomDetails_INSandUPDandDEL(string CustomAgentId, string AgentCode,string ContactPerson ,string AgentName, string Place, int Transportmode, string AddressforDelivery, string AgentAddress, string AgentCity, string AgentState, string AgentCountry, string Phone, string Mphone, string Email, string Zipcode, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("CustomAgentId", SqlDbType.UniqueIdentifier, new Guid(CustomAgentId)));
            Params.Add(mdbh.AddParameter("AgentCode", SqlDbType.NVarChar, AgentCode));
            Params.Add(mdbh.AddParameter("ContactPerson", SqlDbType.NVarChar, ContactPerson));
            Params.Add(mdbh.AddParameter("AgentName", SqlDbType.NVarChar, AgentName));
            Params.Add(mdbh.AddParameter("Place", SqlDbType.NVarChar, Place));
            Params.Add(mdbh.AddParameter("ModeofTransport", SqlDbType.NVarChar, Transportmode));
            Params.Add(mdbh.AddParameter("AddressforDelivery", SqlDbType.NVarChar, AddressforDelivery));
            Params.Add(mdbh.AddParameter("AgentAddress", SqlDbType.NVarChar, AgentAddress));
            Params.Add(mdbh.AddParameter("AgentCity", SqlDbType.NVarChar, AgentCity));
            Params.Add(mdbh.AddParameter("AgentState", SqlDbType.NVarChar, AgentState));
            Params.Add(mdbh.AddParameter("AgentCountry", SqlDbType.NVarChar, AgentCountry));
            Params.Add(mdbh.AddParameter("Phone", SqlDbType.NVarChar, Phone));
            Params.Add(mdbh.AddParameter("Mphone ", SqlDbType.NVarChar, Mphone));
            Params.Add(mdbh.AddParameter("Email ", SqlDbType.NVarChar, Email));
            Params.Add(mdbh.AddParameter("Zipcode ", SqlDbType.NVarChar, Zipcode));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_CustomAgentDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetAgentDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblCustomAgentDetails WHERE [Delete]=0 ");
        }
        public static DataTable GetAgentDetails(string CustomAgentId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblCustomAgentDetails WHERE CustomAgentId = '" + CustomAgentId + "' and [Delete]=0 ");
        }


    }
}
