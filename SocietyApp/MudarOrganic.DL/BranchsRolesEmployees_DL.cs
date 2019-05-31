using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;

namespace MudarOrganic.DL
{

    public static class BranchsRolesEmployees_DL
    {

        #region Branch Operation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="BranchCode"></param>
        /// <param name="Bname"></param>
        /// <param name="sales"></param>
        /// <param name="export"></param>
        /// <param name="warehose"></param>
        /// <param name="other"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="taluk"></param>
        /// <param name="district"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="parentBranchID"></param>
        /// <param name="createdby"></param>
        /// <param name="createdDate"></param>
        /// <param name="modifiedby"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="typeOperation"></param>
        /// <returns></returns>
        /// 
        public static string Branch_INT_UPT_DEL(string BranchID, string BranchCode, string Bname, bool sales, bool export, bool warehose, bool other, string ContactPerson, string PhoneorFax, string Mobile, string Email, string website, string BankName, string BankAcctno, string BankADCCode, string IECode, string FDA, string APVAT, string address, string city, string taluk, string district, string state, string country, string parentBranchID, string createdby, DateTime createdDate, string modifiedby, DateTime modifiedDate, int typeOperation, int OrganicPremium, string TIN)
        {
            object Return = string.Empty;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            if (string.IsNullOrEmpty(BranchID))
            {
                Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
                Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.UniqueIdentifier, Guid.NewGuid(), Param_Directions.Param_Out));
            }
            else
            {
                Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.UniqueIdentifier, new Guid(BranchID), Param_Directions.Param_Out));
                Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, new Guid(BranchID)));
            }
            Params.Add(mdbh.AddParameter("BranchCode", SqlDbType.NVarChar, BranchCode));
            Params.Add(mdbh.AddParameter("Bname", SqlDbType.NVarChar, Bname));
            Params.Add(mdbh.AddParameter("Sales", SqlDbType.Bit, sales));
            Params.Add(mdbh.AddParameter("Export", SqlDbType.Bit, export));
            Params.Add(mdbh.AddParameter("WareHousing", SqlDbType.Bit, warehose));
            Params.Add(mdbh.AddParameter("Other", SqlDbType.Bit, other));
            Params.Add(mdbh.AddParameter("ContactPerson", SqlDbType.NVarChar, ContactPerson));
            Params.Add(mdbh.AddParameter("PhoneFax", SqlDbType.NVarChar, PhoneorFax));
            Params.Add(mdbh.AddParameter("Mobile", SqlDbType.NVarChar, Mobile));
            Params.Add(mdbh.AddParameter("Email", SqlDbType.NVarChar, Email));
            Params.Add(mdbh.AddParameter("website", SqlDbType.NVarChar, website));
            Params.Add(mdbh.AddParameter("BankName", SqlDbType.NVarChar, BankName));
            Params.Add(mdbh.AddParameter("BankAcctno", SqlDbType.NVarChar, BankAcctno));
            Params.Add(mdbh.AddParameter("BankADCCode", SqlDbType.NVarChar, BankADCCode));
            Params.Add(mdbh.AddParameter("IECode", SqlDbType.NVarChar, IECode));
            Params.Add(mdbh.AddParameter("FDA", SqlDbType.NVarChar, FDA));
            Params.Add(mdbh.AddParameter("APVAT", SqlDbType.NVarChar, APVAT));
            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, address));
            Params.Add(mdbh.AddParameter("City", SqlDbType.NVarChar, city));
            Params.Add(mdbh.AddParameter("Taluk", SqlDbType.NVarChar, taluk));
            Params.Add(mdbh.AddParameter("District", SqlDbType.NVarChar, district));
            Params.Add(mdbh.AddParameter("State", SqlDbType.NVarChar, state));
            Params.Add(mdbh.AddParameter("Country", SqlDbType.NVarChar, country));
            Params.Add(mdbh.AddParameter("Designation", SqlDbType.NVarChar, ""));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdby));
            Params.Add(mdbh.AddParameter("CreatedDate", SqlDbType.DateTime, createdDate));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));
            Params.Add(mdbh.AddParameter("ModifiedDate", SqlDbType.DateTime, modifiedDate));
            Params.Add(mdbh.AddParameter("Delete", SqlDbType.Bit, false));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("OrganicPremium", SqlDbType.Int, OrganicPremium));
            Params.Add(mdbh.AddParameter("TIN", SqlDbType.NVarChar, TIN));
            if (string.IsNullOrEmpty(parentBranchID))
                Params.Add(mdbh.AddParameter("BranchHeadCode", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
            else
                Params.Add(mdbh.AddParameter("BranchHeadCode", SqlDbType.UniqueIdentifier, new Guid(parentBranchID)));

            try
            {
                Return = mdbh.ExecuteNonQuery(sp.sp_BranchDetails_INSandUPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Return.ToString();
        }
        //oldcode
        //public static string Branch_INT_UPT_DEL(string BranchID, string BranchCode, string Bname, bool sales, bool export, bool warehose, bool other, string address, string city, string taluk, string district, string state, string country, string parentBranchID, string createdby, DateTime createdDate, string modifiedby, DateTime modifiedDate, int typeOperation)
        //{
        //    object Return = string.Empty;
        //    MudarSPName sp = new MudarSPName();
        //    MudarDBHelper mdbh = MudarDBHelper.Instance;
        //    List<SqlParameter> Params = new List<SqlParameter>();
        //    if (string.IsNullOrEmpty(BranchID))
        //    {
        //        Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
        //        Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.UniqueIdentifier, Guid.NewGuid(), Param_Directions.Param_Out));
        //    }
        //    else
        //    {
        //        Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.UniqueIdentifier, new Guid(BranchID), Param_Directions.Param_Out));
        //        Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, new Guid(BranchID)));
        //    }
        //    Params.Add(mdbh.AddParameter("BranchCode", SqlDbType.NVarChar, BranchCode));
        //    Params.Add(mdbh.AddParameter("Bname", SqlDbType.NVarChar, Bname));
        //    Params.Add(mdbh.AddParameter("Sales", SqlDbType.Bit, sales));
        //    Params.Add(mdbh.AddParameter("Export", SqlDbType.Bit, export));
        //    Params.Add(mdbh.AddParameter("WareHousing", SqlDbType.Bit, warehose));
        //    Params.Add(mdbh.AddParameter("Other", SqlDbType.Bit, other));
        //    Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, address));
        //    Params.Add(mdbh.AddParameter("City", SqlDbType.NVarChar, city));
        //    Params.Add(mdbh.AddParameter("Taluk", SqlDbType.NVarChar, taluk));
        //    Params.Add(mdbh.AddParameter("District", SqlDbType.NVarChar, district));
        //    Params.Add(mdbh.AddParameter("State", SqlDbType.NVarChar, state));
        //    Params.Add(mdbh.AddParameter("Country", SqlDbType.NVarChar, country));
        //    Params.Add(mdbh.AddParameter("Designation", SqlDbType.NVarChar, ""));
        //    Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdby));
        //    Params.Add(mdbh.AddParameter("CreatedDate", SqlDbType.DateTime, createdDate));
        //    Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));
        //    Params.Add(mdbh.AddParameter("ModifiedDate", SqlDbType.DateTime, modifiedDate));
        //    Params.Add(mdbh.AddParameter("Delete", SqlDbType.Bit, false));
        //    Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, typeOperation));
        //    if (string.IsNullOrEmpty(parentBranchID))
        //        Params.Add(mdbh.AddParameter("BranchHeadCode", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
        //    else
        //        Params.Add(mdbh.AddParameter("BranchHeadCode", SqlDbType.UniqueIdentifier, new Guid(parentBranchID)));

        //    try
        //    {
        //        Return = mdbh.ExecuteNonQuery(sp.sp_BranchDetails_INSandUPD, Params);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Return.ToString();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="BranchCode"></param>
        /// <param name="modifiedby"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="typeOperation"></param>
        /// <returns></returns>
        public static bool Branch_INT_UPT_DEL(string BranchID, string modifiedby, int typeOperation)
        {
            //bool Return = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, new Guid(BranchID)));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));

            try
            {
                mdbh.ExecuteNonQuery(sp.sp_BranchDetails_DEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static DataTable GetBranchDetails(string BranchID,int Output)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            if(!string.IsNullOrEmpty(BranchID))
                Params.Add(mdbh.AddParameter("BranchID", SqlDbType.UniqueIdentifier, new Guid(BranchID)));
            else
                Params.Add(mdbh.AddParameter("BranchID", SqlDbType.UniqueIdentifier, Guid.NewGuid()));
            Params.Add(mdbh.AddParameter("output", SqlDbType.Int, Output));

            return mdbh.ExecuteDataTable(sp.sp_BranchDetails, Params, "BranchParentList");
        }
        public static DataTable GetDefaulBranchDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblBranchDetails where [Default] = 1 AND [Delete] = 0"); 
        }
        #endregion

        #region Role Opertaion
        public static bool Role_INT_UPT_DEL(string RoleID, string RoleName, string createdby, string modifiedby, int typeOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("RoleId", SqlDbType.UniqueIdentifier, new Guid(RoleID)));
            Params.Add(mdbh.AddParameter("RoleName", SqlDbType.NVarChar, RoleName));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdby));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, modifiedby));
            Params.Add(mdbh.AddParameter("TypeOperation", SqlDbType.Int, typeOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));

            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Roles_INSandUPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }

        public static DataTable GetRoleDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblRoles WHERE [Delete] = 0 ORDER BY RoleName ");
        }

        #endregion

        #region Employee/User Operation

        public static bool Employee_INS_UPT_DEL(string EmployeeId, string EmployeeName, string BranchId, string Phonenumber, string Mnumber, string address, string city, string taluk, string district, string state, string country, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;

            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("EmployeeId", SqlDbType.UniqueIdentifier, new Guid(EmployeeId)));
            Params.Add(mdbh.AddParameter("EmployeeFristName", SqlDbType.NVarChar, EmployeeName));
            Params.Add(mdbh.AddParameter("BranchId", SqlDbType.UniqueIdentifier, new Guid(BranchId)));
            Params.Add(mdbh.AddParameter("Phone", SqlDbType.NVarChar, Phonenumber));
            Params.Add(mdbh.AddParameter("Mphone", SqlDbType.NVarChar, Mnumber));
            Params.Add(mdbh.AddParameter("Address", SqlDbType.NVarChar, address));
            Params.Add(mdbh.AddParameter("City", SqlDbType.NVarChar, city));
            Params.Add(mdbh.AddParameter("Taluk", SqlDbType.NVarChar, taluk));
            Params.Add(mdbh.AddParameter("District", SqlDbType.NVarChar, district));
            Params.Add(mdbh.AddParameter("State", SqlDbType.NVarChar, state));
            Params.Add(mdbh.AddParameter("Country", SqlDbType.NVarChar, country));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));

            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_EmployeeDetails_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static bool UserLogin_INS_UPD_DEL(string UserId, string ULoginID, string UPassword, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;

            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("UserID", SqlDbType.UniqueIdentifier, new Guid(UserId)));
            Params.Add(mdbh.AddParameter("UserLoginID", SqlDbType.NVarChar, ULoginID));
            Params.Add(mdbh.AddParameter("UserPassword", SqlDbType.NVarChar, UPassword ));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy ));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy ));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));

            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_UserLogin_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static bool UserInRoles_INS_UPD_DEL(string RoleId, string UserId, string createdBy, string Modifiedby, int TypeOfOperation)
        {
            bool result = false;

            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("RoleId", SqlDbType.UniqueIdentifier, new Guid(RoleId)));
            Params.Add(mdbh.AddParameter("UserID", SqlDbType.UniqueIdentifier, new Guid(UserId)));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, createdBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, Modifiedby));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));

            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_UserInRoles_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static DataTable GetEmployeeDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,(brn.Bname + ' / ' + brn.BranchCode) AS 'BranchName'  FROM dbo.tblEmployeeDetails emp, dbo.tblBranchDetails brn WHERE emp.[Delete] = 0 AND emp.BranchId = brn.BranchId ");
        }
        public static DataTable GetEmployeeRoles(string EmployeeID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT ur.*, r.RoleName FROM dbo.tblUsersInRoles ur, dbo.tblRoles r WHERE ur.[Delete] = 0 AND ur.UserId = '" + EmployeeID + "' AND r.RoleId = ur.RoleId ");
        }
        public static DataSet GetEmployeeDetails(string EmployeeID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("EmployeeId", SqlDbType.UniqueIdentifier, new Guid(EmployeeID.Trim())));

            return mdbh.ExecuteDataSet(sp.sp_EmployeeDetails, Params);
        }
        public static DataTable GetEmployesListOnICS(string ICSid)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,r.RoleId, r.RoleName FROM tblEmployeeDetails emp INNER JOIN tblUsersInRoles ur ON  ur.UserId = emp.EmployeeId INNER JOIN tblRoles r ON r.RoleId = ur.RoleId WHERE emp.[Delete] = 0 and emp.BranchId='"+ ICSid +"'");
        }
        public static DataTable GetEmployeeWithRoles()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,r.RoleId, r.RoleName FROM tblEmployeeDetails emp INNER JOIN tblUsersInRoles ur ON  ur.UserId = emp.EmployeeId INNER JOIN tblRoles r ON r.RoleId = ur.RoleId WHERE emp.[Delete] = 0");
        }
        public static DataTable GetEmployeeWithRoles(string RoleName)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,r.RoleId, r.RoleName FROM tblEmployeeDetails emp INNER JOIN tblUsersInRoles ur ON  ur.UserId = emp.EmployeeId INNER JOIN tblRoles r ON r.RoleId = ur.RoleId WHERE emp.[Delete] = 0 AND r.RoleName = '" + RoleName + "'");
        }

        public static DataTable GetEmployeeWithRoles(string RoleName, string ICSid)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,r.RoleId, r.RoleName FROM tblEmployeeDetails emp INNER JOIN tblUsersInRoles ur ON  ur.UserId = emp.EmployeeId INNER JOIN tblRoles r ON r.RoleId = ur.RoleId WHERE emp.[Delete] = 0 AND r.RoleId = '" + RoleName + "' and emp.BranchId='" + ICSid + "'");
        }
        public static DataTable GetEmployeBasedonRoleID(string role)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT emp.*,r.RoleId, r.RoleName FROM tblEmployeeDetails emp INNER JOIN tblUsersInRoles ur ON  ur.UserId = emp.EmployeeId INNER JOIN tblRoles r ON r.RoleId = ur.RoleId WHERE emp.[Delete] = 0 AND r.RoleId = '"+role+"' ");
        }
        #endregion
    }
}
