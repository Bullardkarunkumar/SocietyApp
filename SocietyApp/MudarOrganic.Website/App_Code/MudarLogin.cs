using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MudarOrganic.Components;
using MudarOrganic.BL;

/// <summary>
/// Summary description for MudarLogin
/// </summary>
public class MudarLogin
{
    public MudarLogin()
    {
        
    }
    public static string RedirectURL(DataTable dt,string url)
    {
        string returnUrl = string.Empty;
        if (string.IsNullOrEmpty(url))
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    return ConfigurationManager.AppSettings["AdminPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Branch.ToLower())
                {
                    BranchsRolesEmployees_BL objbranch = new BranchsRolesEmployees_BL();

                    DataSet dtemployee = objbranch.GetEmployeeDetails(dr["UserId"].ToString());
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    HttpContext.Current.Session["BranchId"] = string.Empty;
                    if (dtemployee.Tables[0].Rows.Count > 0)
                        HttpContext.Current.Session["BranchId"] = dtemployee.Tables[0].Rows[0]["BranchId"].ToString();
                    return ConfigurationManager.AppSettings["BranchPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Buyer.ToLower())
                {
                    Buyer_BL BBL = new Buyer_BL();
                    DataTable dtBuyer = new DataTable();

                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    HttpContext.Current.Session["BuyerId"] = dr["UserId"].ToString();
                    dtBuyer = BBL.BuyerDetails(dr["UserId"].ToString());
                    if (dtBuyer.Rows.Count > 0)
                        if (dtBuyer.Rows[0]["Apporval"].ToString().ToLower() == "true")
                            return ConfigurationManager.AppSettings["BuyerPage"].ToString();
                        else
                            return ConfigurationManager.AppSettings["LoginPage"].ToString();
                    else
                        return ConfigurationManager.AppSettings["LoginPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Farmer.ToLower())
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    return ConfigurationManager.AppSettings["FarmerPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    return ConfigurationManager.AppSettings["SuperAdminPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Supplier.ToLower())
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    HttpContext.Current.Session["SupplierID"] = dr["UserId"].ToString();
                    return ConfigurationManager.AppSettings["SupplierPage"].ToString();
                }
                else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.Society.ToLower())
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    //HttpContext.Current.Session["SupplierID"] = dr["UserId"].ToString();
                    return ConfigurationManager.AppSettings["SocietyPage"].ToString();
                }
                else
                {
                    HttpContext.Current.Session["RoleName_s"] = dr["roleName"].ToString();
                    return ConfigurationManager.AppSettings["HomePage"].ToString();
                }
            }
        }
        else if (!string.IsNullOrEmpty(url))
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] s = path.Split('/');
            foreach (DataRow dr in dt.Rows)
            {
                foreach (string str in s)
                {
                    if (dr["roleName"].ToString().Trim().ToLower() == str.ToString().ToLower())
                        return ConfigurationManager.AppSettings["AdminPage"].ToString();
                    else if (dr["roleName"].ToString().Trim().ToLower() == str.ToString().ToLower())
                        return ConfigurationManager.AppSettings["BranchPage"].ToString();
                    else if (dr["roleName"].ToString().Trim().ToLower() == str.ToString().ToLower())
                        return ConfigurationManager.AppSettings["BuyerPage"].ToString();
                    else if (dr["roleName"].ToString().Trim().ToLower() == str.ToString().ToLower())
                        return ConfigurationManager.AppSettings["FarmerPage"].ToString();
                    else if (dr["roleName"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
                        return ConfigurationManager.AppSettings["SuperAdminPage"].ToString();
                    else
                        return ConfigurationManager.AppSettings["HomePage"].ToString();
                }
            }
        }
        return ConfigurationManager.AppSettings["HomePage"].ToString(); 
    }

    public static Guid GetBranchId()
    {
        DataTable dtlogin = ((DataTable)HttpContext.Current.Session["dtLoginDetails"]);
        Guid result = Guid.Empty;
        if (dtlogin.Rows[0]["RoleName"].ToString().Trim().ToLower() != LoginType.SuperAdmin.ToLower())
        {
            result = new Guid(dtlogin.Rows[0]["BranchId"].ToString().Trim().ToLower());
        }   
        return result;
    }

    public static bool IsIndoreBranch()
    {
        var bid = GetBranchId();
        return bid == new Guid("2F9D0223-6D00-4B58-885D-D494E5B10817");
    }
   
}
