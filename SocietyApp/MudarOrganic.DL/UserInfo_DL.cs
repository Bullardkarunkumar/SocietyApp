using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{
    public static class UserInfo_DL
    {
        public static DataTable GetUserLoginDetailsBasedonRole(string roleID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ul.userid,bd.Bname,bd.BranchCode,ed.EmployeeFristName,ul.UserLoginID,ul.UserPassword from tblUserLogin ul,tblUsersInRoles uir,tblEmployeeDetails ed,tblBranchDetails bd where ul.UserId=uir.UserId AND ul.[Delete]=0 AND uir.RoleId='" + roleID + "' AND ul.UserId = ed.EmployeeId AND ED.BranchId = bd.BranchId ");
        }
        public static DataTable GetSupplierLoginDetails(string roleID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select SupplierId,SupplierCompanyName,ul.UserLoginID,ul.UserPassword from tblSupplierDetails sd,tblUserLogin ul,tblUsersInRoles uir where ul.UserId=uir.UserId  AND ul.[Delete]=0 AND sd.SupplierId=uir.UserId");
        }
        public static DataTable GetUserLoginDetailsBasedonRole()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ul.userid,bd.Bname,bd.BranchCode,ed.EmployeeFristName,ul.UserLoginID,ul.UserPassword from tblUserLogin ul,tblUsersInRoles uir,tblEmployeeDetails ed,tblBranchDetails bd where ul.UserId=uir.UserId AND ul.[Delete]=0 AND uir.RoleId!='82AD1024-6F5D-4B0D-9829-47E6BE304786' AND ul.UserId = ed.EmployeeId AND ED.BranchId = bd.BranchId ");
        }
        public static DataTable GetUserBuyerDetails(string roleID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT bud.BuyerCompanyName,ul.userid,ul.UserLoginID,ul.UserPassword from tblUserLogin ul,tblUsersInRoles uir,tblBuyerDetails bud where ul.UserId = uir.UserId AND ul.UserId = bud.BuyerId AND ul.[Delete]=0 AND uir.RoleId = '" + roleID + "' AND uir.UserId = bud.BuyerId");
        }
        public static DataTable GetBuyerDetailsBasedonBuyerID(string BuyerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT bud.BuyerCompanyName,ul.userid,ul.UserLoginID,ul.UserPassword from tblUserLogin ul,tblUsersInRoles uir,tblBuyerDetails bud where ul.UserId = uir.UserId AND ul.UserId='" + BuyerID + "' AND bud.BuyerId='" + BuyerID + "' AND ul.[Delete]=0 AND uir.UserId='" + BuyerID + "' AND bud.BuyerId='" + BuyerID + "'");
        }
        public static bool UpdateUserLoginDetails(string UserID, string UserLoginID, string UserPassword, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("UserID", SqlDbType.UniqueIdentifier, new Guid(UserID)));
            Params.Add(mdbh.AddParameter("UserLoginID", SqlDbType.NVarChar, UserLoginID));
            Params.Add(mdbh.AddParameter("UserPassword", SqlDbType.NVarChar, UserPassword));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_userLogin_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public static bool CheckUserExist(string UserLoginID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (mdbh.ExecuteDataTable("select * from tblUserLogin where UserLoginID = '" + UserLoginID + "'").Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
