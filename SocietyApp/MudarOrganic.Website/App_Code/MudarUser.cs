using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
/// <summary>
/// Summary description for MudarUser
/// </summary>
public class MudarUser
{
    public static int MudarFamer = 1;
    public static int MudarBuyer = 2;
    public static int MudarBrach = 3;
    public static int OrderPDF = 4;
    public MudarUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool createfolder(string folderName, int Type)
    {
        bool Result = false;
        string path = string.Empty;
        if (Type == MudarFamer)
        {
            path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["farmer"].ToString()) + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Result = true;
            }
            else
                Result = true;
        }
        else if (Type == MudarBuyer)
        {
            string test = WebConfigurationManager.AppSettings["BuyerLogo"].ToString();
            path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["BuyerLogo"].ToString()) + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Result = true;
            }
            else
                Result = true;
        }
        else if (Type == OrderPDF)
        {
            string test = WebConfigurationManager.AppSettings["orderpdf"].ToString();
            path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["orderpdf"].ToString()) + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Result = true;
            }
            else
                Result = true;
        }
        return Result;
    }

    private string MapPath(string p)
    {
        throw new NotImplementedException();
    }
}
