using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MudarOrganic.DL;

namespace MudarOrganic.BL
{
    public class Login_BL
    {
        public DataTable ValidateUserGetData(string ULoginID, string UPassword)
        {
            return Login_DL.ValidateUserGetData(ULoginID, UPassword);
        }
        public DataTable GetSupplierName(string SupplierID)
        {
            return Login_DL.GetSupplierName(SupplierID);
        }
    }
}
