using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class UserInfo_BL
    {
        public DataTable GetUserLoginDetailsBasedonRole(string roleID)
        {
            return UserInfo_DL.GetUserLoginDetailsBasedonRole(roleID);
        }
        public DataTable GetSupplierLoginDetails(string roleID)
        {
            return UserInfo_DL.GetSupplierLoginDetails(roleID);
        }
        public DataTable GetUserLoginDetailsBasedonRole()
        {
            return UserInfo_DL.GetUserLoginDetailsBasedonRole();
        }
        public bool UpdateUserLoginDetails(string UserID, string UserLoginID, string UserPassword, string ModifiedBy, int TypeOfOperation)
        {
            return UserInfo_DL.UpdateUserLoginDetails(UserID, UserLoginID, UserPassword, ModifiedBy, TypeOfOperation);
        }
        public DataTable GetUserBuyerDetails(string roleID)
        {
            return UserInfo_DL.GetUserBuyerDetails(roleID);
        }
        public DataTable GetBuyerDetailsBasedonBuyerID(string BuyerID)
        {
            return UserInfo_DL.GetBuyerDetailsBasedonBuyerID(BuyerID);
        }
        public bool CheckUserExist(string UserLoginID)
        {
            return UserInfo_DL.CheckUserExist(UserLoginID);
        }
    }
}
