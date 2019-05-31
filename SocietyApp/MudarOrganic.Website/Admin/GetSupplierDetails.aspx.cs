using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.IO;
using System.Text;
using MudarOrganic.DL;
using MudarOrganic.BL;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text.RegularExpressions;

public partial class Admin_GetSupplierDetails : System.Web.UI.Page
{
    Settings_BL settObj = new Settings_BL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSupplierPlaceorder_Click(object sender, EventArgs e)
    {
        DataTable dt = settObj.GetProductionYear(Convert.ToDateTime(txtPlantationFDate.Text));
        lblID.Text = dt.Rows[0]["ProductionYear"].ToString();
    }
}
