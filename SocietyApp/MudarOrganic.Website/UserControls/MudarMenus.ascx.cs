using MudarOrganic.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MudarMenus : System.Web.UI.UserControl
{
    public bool IsIndoreAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleName_s"] == null)
                Response.Redirect("~/Login.aspx");
            switch (Session["RoleName_s"].ToString().ToLower())
            {
                case "admin":
                    pnlAdminMenu.Visible = true;
                    var isvisible= MudarLogin.IsIndoreBranch();
                    liPreorder.Visible = isvisible;
                    liStockOrder.Visible = isvisible;
                    break;
                case "farmer":
                    break;
                case "branch":
                    pnlICS.Visible = true;
                    break;
                case "buyer":
                    pnlBuyer.Visible = true;
                    break;
                case "superadmin":
                    pnlSuperAdmin.Visible = true;
                    break;
                case "supplier":
                    pnlBranch.Visible = true;
                    break;
                case "society":
                    PnlSociety.Visible = true;
                    break;
            }


        }
    }
}