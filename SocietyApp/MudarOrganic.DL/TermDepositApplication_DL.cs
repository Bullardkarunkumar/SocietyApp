using Society.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MudarOrganic.DL
{
    public class TermDepositApplication_DL
    {
        public static MemberDetailsViewModels GetMemberDetails(string Input, bool IsName)
        {
            DataTable table = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string query = IsName == true ? @"SELECT admissionno,MemberName,nomineename,nomineerelation FROM CUS_PersonalDetails where MemberName like '%" + Input + "%' order by MemberName asc ; "
                                          : @"SELECT admissionno,MemberName,nomineename,nomineerelation FROM CUS_PersonalDetails where admissionno like '%" + Input + "%' order by MemberName asc ;";
            table = mdbh.ExecuteDataTable(query);
            if (table.Rows[0][0].ToString().Length > 0)
            {
                var item = new MemberDetailsViewModels
                {
                    MemberNo = Convert.ToInt32(table.Rows[0][0].ToString()),
                    MemberName = table.Rows[0][1].ToString(),
                    NomineeName = table.Rows[0][2].ToString(),
                    NomineeRelation = table.Rows[0][3].ToString()
                };
                return item;
            }
            else
            {
                return null;
            }
        }

        public static List<TenureDetailsViewModel> GetTenureDetails()
        {
            List<TenureDetailsViewModel> list = new List<TenureDetailsViewModel>();
            DataTable table = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            table = mdbh.ExecuteDataTable("SELECT id,tenure_name,start_duration,end_duration,rate_of_intrest FROM Trm_Deposit_RateofInterestConfig ; ");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var item = new TenureDetailsViewModel
                {
                    Id = Convert.ToInt32(table.Rows[i][0].ToString()),
                    TenureName = table.Rows[i][1].ToString(),
                    StartDuration = Convert.ToInt32(table.Rows[i][2].ToString()),
                    EndDuration = Convert.ToInt32(table.Rows[i][3].ToString()),
                    RateOfIntrest = Convert.ToDecimal(table.Rows[i][4].ToString())
                };
                list.Add(item);
            }
            return list;
        }
        public static int GetDepositeNumber()
        {
            DataTable table = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            table = mdbh.ExecuteDataTable("SELECT count(DepositNo) FROM Trm_DepositApplicationDtls; ");
            return table.Rows[0][0].ToString().Length > 0 ? Convert.ToInt32(table.Rows[0][0]) : 233;
        }
    }
}
