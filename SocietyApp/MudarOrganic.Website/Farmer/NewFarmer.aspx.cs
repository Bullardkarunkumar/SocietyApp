using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Farmer_Farmer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!Page.IsPostBack)
        //    divFarmer.Visible = false;
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnFarmerInfo();
        }
        if (Request.QueryString["NewFarmer"].ToString() == "1")
        {
            //divFarmer.Visible = true;
            //lblinfo.Visible = false;
            //lblinfo.Text = "Farmer Details";
        }
        else if (Request.QueryString["NewFarmer"].ToString() == "0")
        {
            //divFarmer.Visible = false;
            //lblinfo.Visible = false;
            //lblinfo.Text = "Please Enter Farmer Details";
        }
        else if (Request.QueryString["NewFarmer"].ToString() == "2")
        {
            //divFarmer.Visible = false;
            //lblinfo.Visible = false;

            if (!string.IsNullOrEmpty(Request.QueryString["FarmerCode"].ToString()))
                this.CucFarmerDetails.code = Request.QueryString["FarmerCode"].ToString().Trim();
                //this.ucFarmerDetails1.bindFarmerDetails(Request.QueryString["FarmerCode"].ToString().Trim());// = "text";

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //string[] code = txtSFarmerName.Text.Split('-');
        //if (code.Length > 0)
        //    this.ucFarmerDetails1.bindFarmerDetails(code[1].ToString().Trim());// = "text";
    }
}
