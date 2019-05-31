using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.EMails;
using System.Data;
using System.Net.Mail;
using System.Web.Configuration;

public partial class Login : System.Web.UI.Page
{
    Login_BL login = new Login_BL();
    Email email = new Email();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSignin.Enabled = true;
            btnRegister.Enabled = true;
        }
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        if (url.Contains("www."))
        {
            url = url.Replace("www.", "");
            Response.Redirect(url);
        }
        //List<string> item = new List<string>();
        //item.Add(Server.MapPath(".") + @"\Attachments\OrderPDF\1019\1019_PO2012910.pdf");

        //Email.SendEMail("bhanu1236@gmail.com;aslamshaik61@gmail.com", "rbhanu@vgsoft.in", null, "test Login", "HI, body", item.ToArray<string>(), EmailFormat.HTML);
    }
    protected void btnSignin_Click(object sender, EventArgs e)
    {
        DataTable dtLoginDetails = new DataTable();
        string check = string.Empty;
        dtLoginDetails = login.ValidateUserGetData(txtUserID.Text, txtPassword.Text);
        if (dtLoginDetails.Rows.Count > 0)
        {
            Session["dtLoginDetails"] = dtLoginDetails;
            check = MudarLogin.RedirectURL(dtLoginDetails, string.Empty);
            Response.Redirect(check);
        }
        else
        {
            lblError.Text = "Please Enter the Valid Login Details";
            divError.Attributes["class"] = "alert alert-danger";
        }
    }
    
    protected void txtPassword_TextChanged(object sender, EventArgs e)
    {
        btnSignin.Enabled = true;
        
    }
    protected void txtUserID_TextChanged(object sender, EventArgs e)
    {
        btnSignin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BuyerReg.aspx");
    }
}
