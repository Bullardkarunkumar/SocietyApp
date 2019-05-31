using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class CustomAgent_BL
    {
        public bool CustomDetails_INSandUPDandDEL(string CustomAgentId, string AgentCode, string ContactPerson, string AgentName, string Place, int Transportmode, string AddressforDelivery, string AgentAddress, string AgentCity, string AgentState, string AgentCountry, string Phone, string Mphone, string Email, string Zipcode, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return CustomAgent_DL.CustomDetails_INSandUPDandDEL(CustomAgentId, AgentCode, ContactPerson, AgentName, Place, Transportmode, AddressforDelivery, AgentAddress, AgentCity, AgentState, AgentCountry, Phone, Mphone, Email, Zipcode, CreatedBy, ModifiedBy, TypeOfOperation);
                
        }
        public DataTable GetAgentDetails()
        {
            return CustomAgent_DL.GetAgentDetails();
        }
        public DataTable GetAgentDetails(string CustomAgentId)
        {
            return CustomAgent_DL.GetAgentDetails(CustomAgentId);
        }
    }
}
