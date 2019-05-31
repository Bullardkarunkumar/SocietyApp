using MudarOrganic.Components;
using Society.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace MudarOrganic.DL
{
    public class Membership_DL
    {
        public static bool Membership_INT_UPT_DEL(MembershipViewModel membership)
        {
            object Return = string.Empty;
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("MemberName", SqlDbType.NVarChar, membership.MemberName));
            Params.Add(mdbh.AddParameter("FatherName", SqlDbType.NVarChar, membership.FatherName));
            Params.Add(mdbh.AddParameter("Aadhaar", SqlDbType.BigInt, membership.AadhaarNumber));
            Params.Add(mdbh.AddParameter("PANnumber", SqlDbType.BigInt, membership.PanNumber));
            Params.Add(mdbh.AddParameter("SpouseName", SqlDbType.NVarChar, membership.SpouseName));
            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, membership.Address));
            Params.Add(mdbh.AddParameter("Area", SqlDbType.NVarChar, membership.Area));
            // Params.Add(mdbh.AddParameter("Status", SqlDbType.NVarChar, ""));
            Params.Add(mdbh.AddParameter("Nomineename", SqlDbType.NVarChar, membership.NomineeName));
            Params.Add(mdbh.AddParameter("NomineeRelation", SqlDbType.NVarChar, membership.NomineeRelation));
            //Params.Add(mdbh.AddParameter("ClosureDate", SqlDbType.DateTime, null));
            Params.Add(mdbh.AddParameter("DateofBirth", SqlDbType.DateTime, membership.DoB));
            Params.Add(mdbh.AddParameter("Age", SqlDbType.Int, membership.Age));
            Params.Add(mdbh.AddParameter("noofshares", SqlDbType.Int, membership.NoofShares));
            Params.Add(mdbh.AddParameter("shareprice", SqlDbType.Int, membership.ShareValue));
            Params.Add(mdbh.AddParameter("totalshareamt", SqlDbType.Int, membership.TotalShareAmt));

            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, 1));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));

            try
            {
                Return = mdbh.ExecuteNonQuery(sp.sp_CusPersonalDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (bool)Return;
        }

        public static int GetAdmissionNumber()
        {
            DataTable table = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            table = mdbh.ExecuteDataTable("SELECT max(admissionno) FROM CUS_PersonalDetails;");
            return table.Rows[0][0].ToString().Length > 0 ? Convert.ToInt32(table.Rows[0][0]) : 233;
        }
    }
}
