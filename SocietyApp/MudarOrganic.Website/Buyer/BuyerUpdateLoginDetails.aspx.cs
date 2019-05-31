using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.IO;

public partial class Buyer_BuyerUpdateLoginDetails : System.Web.UI.Page
{
    UserInfo_BL UI = new UserInfo_BL();
    Buyer_BL BBL = new Buyer_BL();
    MudarUser mu = new MudarUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnUpdateBuyerLoginDetails();
            Session["sDtBuyers"] = BBL.BuyerDetails(Session["BuyerId"].ToString());
            BindLoginDetails();
            divUserEdit.Visible = false;
        }
        DirectoryInfo di = new DirectoryInfo(Server.MapPath(WebConfigurationManager.AppSettings["BuyerLogo"].ToString()));
        FileInfo[] TXTFiles = di.GetFiles(Session["BuyerId"].ToString()+".*");
        if (TXTFiles.Length > 0)
        {
            imgBuyerLogo.ImageUrl = WebConfigurationManager.AppSettings["BuyerLogo"].ToString() + TXTFiles[0].Name;
        }
    }
    protected void gvBuyerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_edit_Buyer")
        {
            divUserEdit.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            lblUserID.Text = gvBuyerDetails.DataKeys[index].Value.ToString();
            txtUsername.Focus();
            txtUsername.Text = gvBuyerDetails.Rows[index].Cells[2].Text.ToString();
            txtPassword.Text = gvBuyerDetails.Rows[index].Cells[3].Text.ToString();
            txtPassword.Enabled = false;
        }
    }
    protected void btnUserSubmit_Click(object sender, EventArgs e)
    {
        bool result = false;

        result = UI.UpdateUserLoginDetails(lblUserID.Text, txtUsername.Text, txtPassword.Text, "Bhanu", MudarApp.Update);
        if (result == true)
        {
            Response.Write("<script>alert('Update userdetails Successfully');</script>");
            txtUsername.Text = "";
            txtPassword.Text = "";
            divUserEdit.Visible = false;
            BindLoginDetails();
        }
        else
        {
            Response.Write("<script>alert('Save failed');</script>");
        }
    }
    protected void btnUserClear_Click(object sender, EventArgs e)
    {
        divUserEdit.Visible = false;
    }
    private void BindLoginDetails()
    {
        DataTable dt = UI.GetBuyerDetailsBasedonBuyerID(Session["BuyerId"].ToString());
        if (dt.Rows.Count > 0)
        {
            gvBuyerDetails.DataSource = dt;
            gvBuyerDetails.DataBind();
        }
    }
    protected void lbtnCheck_Click(object sender, EventArgs e)
    {
        bool result;
        result = UI.CheckUserExist(txtUsername.Text);
        if (result == true)
        {
            lbtnCheck.Visible = true;
            lblCheckuser.Visible = true;
        }
        else
        {
            lbtnCheck.Visible = false;
            lblCheckuser.Visible = false;
            txtPassword.Enabled = true;
        }
    }
    protected void lbtnBuyerlogo_Click(object sender, EventArgs e)
    {
        divUploadlogo.Visible = true;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //DataTable dtBuyer =  UI.GetBuyerDetailsBasedonBuyerID(Session["BuyerId"].ToString());
        //string BuyerName = dtBuyer.Rows[0]["BuyerCompanyName"].ToString();
        if (fuBLogo.FileName.Length > 0)
        {
            string Ext=".JPG";
            string[] FileName = fuBLogo.FileName.Split('.');
            if (fuBLogo.PostedFile.ContentType == "image/pjpeg" ||
                fuBLogo.PostedFile.ContentType == "image/jpeg" ||
                fuBLogo.PostedFile.ContentType == "image/png" ||
                fuBLogo.PostedFile.ContentType == "image/jpg")
            {
                if (FileName.Length >= 2)
                    Ext = "." + FileName[1];
                string path = WebConfigurationManager.AppSettings["BuyerLogo"].ToString() + Session["BuyerId"].ToString() + Ext; //mu.createfolder(BuyerName, MudarUser.MudarBuyer) ? "~/Attachments/BuyerLogo/" + BuyerName + "/" : "~/Attachments/BuyerLogo/";
                fuBLogo.PostedFile.SaveAs(Server.MapPath(path));
                //btnUpload.Visible = false;
            }
            else
            {
                Response.Write("<script>alert('upload the logo format should be !!! pjepg,jpeg,png,jpg !!!')</script>");
            }
        }
    }
}
