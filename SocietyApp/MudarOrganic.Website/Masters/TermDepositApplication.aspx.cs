using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;


public partial class Masters_TermDepositApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDepositDate.Text = DateTime.Now.ToShortDateString();
        }
    }
}