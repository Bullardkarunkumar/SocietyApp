using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class Login_DL
    {
        public static DataTable ValidateUserGetData(string ULoginID, string UPassword)
        {
            //bool Result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM [vmUserLoginRoleDetails] WHERE UserLoginID COLLATE Latin1_General_CS_AS = '" + ULoginID + "' AND UserPassword COLLATE Latin1_General_CS_AS = '" + UPassword + "' ");
        }
        public static DataTable GetSupplierName(string SupplierID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblSupplierDetails where SupplierId='" + SupplierID + "'");
        }
    }
}
