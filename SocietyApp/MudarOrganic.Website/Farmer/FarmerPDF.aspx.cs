using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using MudarOrganic.BL;

public partial class Farmer_FarmerPDF : System.Web.UI.Page
{
    Farmer_BL farmerObj = new Farmer_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFarmerDetails();
        }
    }
    private void BindFarmerDetails()
    {
        try
        {
            divgvFarmerdtforPDF.Visible = true;
            DataTable dt = new DataTable();
            dt = farmerObj.FamerDetails();
            gvFarmerPDf.DataSource = dt;
            gvFarmerPDf.DataBind();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=FarmerDetails.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0.0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception ex)
        {
            Session["ErrorMsg"] = ex.Message;
            Response.Redirect("~/NoAccess.aspx",false);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
