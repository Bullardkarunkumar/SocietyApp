using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Xml.Linq;
using MudarOrganic.BL;
using Society.Models;

public partial class Masters_Sharecertificate : System.Web.UI.Page
{
    private object mdbh;
    Membership_BL mebmershipDl = new Membership_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument rd = new ReportDocument();
        DataTable dt = new DataTable();
        // MastersSharecertificateViewModel master = new MastersSharecertificateViewModel();
        //master = mebmershipDl.GetSharecertificateDetails(234);
        //rd.Load(Server.MapPath("~/CrystalReports/ShareCertificate.rpt"));
        //rd.SetDataSource(master);
        //CrystalReportViewer1.ReportSource = rd;

        dt = mebmershipDl.GetSharecertificateDetails(234);
        rd.Load(Server.MapPath("~/CrystalReports/ShareCertificate.rpt"));
        dt.TableName = "Crystal Report Example";
        //set dataset to the report viewer.
        rd.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rd;
    }

    protected void CrystalReportViewer1_Init(object sender, EventArgs e)
    {

    }
}
public class test
{
    public string admissionno { get; set; }
    public string noofshares { get; set; }
    public string shareprice { get; set; }
    public string totalshareamt { get; set; }
    public string MemberName { get; set; }
    public string FatherName { get; set; }
}