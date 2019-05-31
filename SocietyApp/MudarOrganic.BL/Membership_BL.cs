using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using Society.Models;

namespace MudarOrganic.BL
{
    public class Membership_BL
    {        
        public bool AddMemmbershipDetails(MembershipViewModel membership)
        {
             return Membership_DL.Membership_INT_UPT_DEL(membership);
        }
        public int GetAdmissionNumber()
        {
            return Membership_DL.GetAdmissionNumber();
        }
    }
}
