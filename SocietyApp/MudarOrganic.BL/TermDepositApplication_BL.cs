using MudarOrganic.DL;
using Society.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudarOrganic.BL
{
    public class TermDepositApplication_BL
    {
        public MemberDetailsViewModels GetMemberDetails(string input, bool status)
        {
            return TermDepositApplication_DL.GetMemberDetails(input, status);
        }
        public List<TenureDetailsViewModel> GetTenureDetails()
        {
            return TermDepositApplication_DL.GetTenureDetails();
        }
        public int GetDepositeNumber()
        {
            return TermDepositApplication_DL.GetDepositeNumber();
        }
    }
}
