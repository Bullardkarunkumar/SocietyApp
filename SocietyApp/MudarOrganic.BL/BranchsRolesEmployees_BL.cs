using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MudarOrganic.DL;
using MudarOrganic.Components;
using MudarOrganic.EMails;

namespace MudarOrganic.BL
{
    public class BranchsRolesEmployees_BL
    {
        #region BranchDetails
        public string BranchDetails(string BranchID, string BranchCode, string Bname, bool sales, bool export, bool warehose, bool other, string ContactPerson, string PhoneorFax, string Mobile, string Email, string website, string BankName, string BankAcctno, string BankADCCode, string IECode, string FDA, string APVAT, string address, string city, string taluk, string district, string state, string country, string parentBranchID, string createdby, DateTime createdDate, string modifiedby, DateTime modifiedDate, int typeOperation, int OrganicPremium, string TIN)
        {
            string Result = string.Empty;

            Result = BranchsRolesEmployees_DL.Branch_INT_UPT_DEL(BranchID, BranchCode, Bname, sales, export, warehose, other, ContactPerson, PhoneorFax, Mobile, Email, website, BankName, BankAcctno, BankADCCode, IECode, FDA, APVAT, address, city, taluk, district, state, country, parentBranchID, createdby, createdDate, modifiedby, modifiedDate, typeOperation, OrganicPremium, TIN);

            return Result;
        }
        //public string BranchDetails(string BranchID, string BranchCode, string Bname, bool sales, bool export, bool warehose, bool other, string address, string city, string taluk, string district, string state, string country, string parentBranchID, string createdby, DateTime createdDate, string modifiedby, DateTime modifiedDate, int typeOperation)
        //{
        //    string Result = string.Empty;
            
        //    Result=BranchsRolesEmployees_DL.Branch_INT_UPT_DEL(BranchID,BranchCode,Bname,sales,export,warehose,other,address,city,taluk,district,state,country,parentBranchID,createdby,createdDate,modifiedby,modifiedDate,typeOperation);

        //    return Result;
        //}
        public bool BranchDetails(string BranchID, int typeOperation,string ModifiedBy)
        {
            return BranchsRolesEmployees_DL.Branch_INT_UPT_DEL(BranchID, ModifiedBy, typeOperation);
        }
        public DataTable GetParentBranchDetails()
        {
            return BranchsRolesEmployees_DL.GetBranchDetails(string.Empty, 1);
        }

        public DataTable BranchDetails(string BranchID)
        {
            return BranchsRolesEmployees_DL.GetBranchDetails(BranchID, 2);
        }

        public DataTable BranchDetails()
        {
            return BranchsRolesEmployees_DL.GetBranchDetails(string.Empty, 3);
        }
        public DataTable GetDefaulBranchDetails()
        {
            return BranchsRolesEmployees_DL.GetDefaulBranchDetails();
        }
        #endregion

        #region Roles
        public bool Role_INT_UPT_DEL(string RoleID, string RoleName, string createdby, string modifiedby, int typeOperation)
        {
            return BranchsRolesEmployees_DL.Role_INT_UPT_DEL(RoleID, RoleName, createdby, modifiedby, typeOperation);
        }
        public DataTable GetRoleDetails()
        {
            return BranchsRolesEmployees_DL.GetRoleDetails();
        }
        #endregion

        #region Employee/User Operation

        public bool Employee_INS_UPT_DEL(string EmployeeId, string EmployeeName, string BranchId, string Phonenumber, string Mnumber, string address, string city, string taluk, string district, string state, string country, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return BranchsRolesEmployees_DL.Employee_INS_UPT_DEL(EmployeeId, EmployeeName, BranchId, Phonenumber, Mnumber, address, city, taluk, district, state, country, CreatedBy, ModifiedBy, TypeOfOperation);
        }

        public bool UserLogin_INS_UPD_DEL(string UserId, string ULoginID, string UPassword, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return BranchsRolesEmployees_DL.UserLogin_INS_UPD_DEL(UserId, ULoginID, UPassword, CreatedBy, ModifiedBy, TypeOfOperation);
        }

        public bool UserInRoles_INS_UPD_DEL(string RoleId, string UserId, string createdBy, string Modifiedby, int TypeOfOperation)
        {
            return BranchsRolesEmployees_DL.UserInRoles_INS_UPD_DEL(RoleId, UserId, createdBy, Modifiedby, TypeOfOperation);
        }

        public bool CreateEmployee(string EmployeeName, string BranchId, string Phonenumber, string Mnumber, string address, string city, string taluk, string district, string state, string country, string CreatedBy, string ModifiedBy, int TypeOfOperation,List<string> RolesList)
        {
            bool Result = false;

            string NewUid = Guid.NewGuid().ToString();
            string NewLoginId = MudarAutoGenerate.GenerateULogin(EmployeeName);
            string NewPassword = MudarAutoGenerate.GeneratePassword(EmployeeName);

            Result = Employee_INS_UPT_DEL(NewUid, EmployeeName, BranchId, Phonenumber, Mnumber, address, city, taluk, district, state, country, CreatedBy, ModifiedBy, TypeOfOperation);
            if (Result)
            {
                Result = UserLogin_INS_UPD_DEL(NewUid, NewLoginId, NewPassword, CreatedBy, ModifiedBy, TypeOfOperation);
                if (Result)
                {
                    Email.SendLoginCredintials(EmployeeName, NewLoginId, NewPassword, "sudheer@mudarindia.com,aslam.shaik@vgsoft.in", "Mudar Organic Login Credintals");
                    foreach (string role in RolesList)
                    {
                        Result = UserInRoles_INS_UPD_DEL(role, NewUid, CreatedBy, ModifiedBy, TypeOfOperation);

                    }
                }
            }

            return Result;
        }
        public bool UpdateEmployee(string EmployeeId, string EmployeeName, string BranchId, string Phonenumber, string Mnumber, string address, string city, string taluk, string district, string state, string country, string CreatedBy, string ModifiedBy, int TypeOfOperation, List<string> RolesList)
        {
            bool Result = false;

            Result = Employee_INS_UPT_DEL(EmployeeId, EmployeeName, BranchId, Phonenumber, Mnumber, address, city, taluk, district, state, country, CreatedBy, ModifiedBy, TypeOfOperation);
            if (Result)
            {
                Result = UserInRoles_INS_UPD_DEL(Guid.NewGuid().ToString(), EmployeeId, CreatedBy, ModifiedBy, 3);
                foreach (string role in RolesList)
                {
                    Result = UserInRoles_INS_UPD_DEL(role, EmployeeId, CreatedBy, ModifiedBy, 1);
                }
            }
            return Result;
        }

        public DataTable GetEmployeeDetails()
        {
            return BranchsRolesEmployees_DL.GetEmployeeDetails();
        }
        public DataSet GetEmployeeDetails(string EmployeeID)
        {
            return BranchsRolesEmployees_DL.GetEmployeeDetails(EmployeeID);
        }
        public DataTable GetEmployeeRoles(string EmployeeID)
        {
            return BranchsRolesEmployees_DL.GetEmployeeRoles(EmployeeID);
        }
        public bool DeleteEmployee(string Employeeid, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            Result = Employee_INS_UPT_DEL(Employeeid, string.Empty, Guid.NewGuid().ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ModifiedBy, TypeOfOperation);
            Result = UserLogin_INS_UPD_DEL(Employeeid, string.Empty, string.Empty, string.Empty, ModifiedBy, TypeOfOperation);
            return Result;
        }
        public DataTable GetEmployeeWithRoles(string RoleName)
        {
            return BranchsRolesEmployees_DL.GetEmployeeWithRoles(RoleName);
        }
        public DataTable GetEmployeeWithRoles()
        {
            return BranchsRolesEmployees_DL.GetEmployeeWithRoles();
        }
        public DataTable GetEmployeBasedonRoleID(string role)
        {
            return BranchsRolesEmployees_DL.GetEmployeBasedonRoleID(role);
        }

        public  DataTable GetEmployeeWithRoles(string RoleName, string ICSid)
        {
            return BranchsRolesEmployees_DL.GetEmployeeWithRoles(RoleName, ICSid);
        }
        #endregion
    }
}
