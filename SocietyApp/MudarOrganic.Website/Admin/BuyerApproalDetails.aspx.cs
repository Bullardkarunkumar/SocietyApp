using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.EMails;
using System.Drawing;
using System.Data;

public partial class Admin_BuyerDetails : System.Web.UI.Page
{
    Buyer_BL objBuyer = new Buyer_BL();
    Emailtest email = new Emailtest();
    public static string SortExpression_BA = "BuyerCompanyName";
    public static string SortExpression_BU = "BuyerCompanyName";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["dirState"] = "";
            BindBuyerDetails_Unapproval();
            BindBuyerDetails_Approval();
            UnApproved.Visible = true;
            //btnUnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //btnUnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            Approved.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Email.SendEMail("bhanu1236@gmail.com;aslamshaik61@gmail.com", "rbhanu@vgsoft.in", null, "test Login", "HI, body", item.ToArray<string>(), EmailFormat.HTML);
        foreach (GridViewRow gvr in gvBuyerDetails.Rows)
        {
            bool result = false;
            bool lotsample = false;
            if ((gvr.Cells[0].FindControl("cbBApproval") as CheckBox).Checked)
            {
                int valueType = 0;
                if ((gvr.Cells[0].FindControl("cbLotsample") as CheckBox).Checked)
                    lotsample = true;
                if ((gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled)
                    valueType = (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex + 1;
                result = objBuyer.BuyerApproval(gvBuyerDetails.DataKeys[gvr.RowIndex].Value.ToString(), "Aslam", true, lotsample, (gvr.Cells[0].FindControl("txtBCode") as TextBox).Text, Convert.ToDecimal((gvr.Cells[0].FindControl("txtDiscount") as TextBox).Text), Convert.ToDecimal((gvr.Cells[0].FindControl("txtFairT") as TextBox).Text), Convert.ToDecimal((gvr.Cells[0].FindControl("txtFairTP") as TextBox).Text), valueType);
                //string test1 = (gvr.Cells[0].Controls[0] as LinkButton).Text;
                //string test2 = (gvr.Cells[0].FindControl("hfLoginID") as HiddenField).Value;
                //string test3 = (gvr.Cells[0].FindControl("hfPassword") as HiddenField).Value;
                string ToEmail = (gvr.Cells[0].FindControl("hfEmail") as HiddenField).Value;
                //string test4 = gvr.Cells[2].Text;
                if (result)
                {
                    email.SendEmail(gvr.Cells[0].Text, (gvr.Cells[0].FindControl("hfLoginID") as HiddenField).Value, (gvr.Cells[0].FindControl("hfPassword") as HiddenField).Value, ToEmail, "Mudar Organic Login Credintals");
                    BindBuyerDetails_Approval();
                    BindBuyerDetails_Unapproval();
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('Approved Successfully');</script>");
                }
                //Email.SendLoginCredintials(gvr.Cells[0].Text, (gvr.Cells[0].FindControl("hfLoginID") as HiddenField).Value, (gvr.Cells[0].FindControl("hfPassword") as HiddenField).Value, ToEmail, "Mudar Organic Login Credintals");
            }
        }

    }
    protected void btnApprovalSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvApprovaledBuyer.Rows)
        {
            bool result = false;
            if ((gvr.Cells[0].FindControl("cbBApproval") as CheckBox).Checked == false)
            {
                int valueType = 0;
                if ((gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled)
                    valueType = (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex + 1;
                result = objBuyer.BuyerApproval(gvApprovaledBuyer.DataKeys[gvr.RowIndex].Value.ToString(), "Aslam", false, false, gvr.Cells[5].Text, Convert.ToDecimal(gvr.Cells[6].Text), Convert.ToDecimal(gvr.Cells[7].Text), Convert.ToDecimal(gvr.Cells[8].Text), valueType);
                if (result)
                    ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Unapproved Successfully !!!');</script>");
            }
        }
        BindBuyerDetails_Unapproval();
        BindBuyerDetails_Approval();
    }
    protected void lbtnUnapproved_Click(object sender, EventArgs e)
    {
        UnApproved.Visible = true;
        //lbtnUnapproved.ForeColor = Color.DarkOrange;
        //lbtnapproved.ForeColor = Color.Empty;
        Approved.Visible = false;
    }
    protected void lbtnapproved_Click(object sender, EventArgs e)
    {
        UnApproved.Visible = false;
        Approved.Visible = true;
        //lbtnUnapproved.ForeColor = Color.Empty;
        //lbtnapproved.ForeColor = Color.DarkOrange;
    }
    private void BindBuyerDetails_Unapproval()
    {
        DataTable dtBuyer = objBuyer.BuyerDetails(false);
        Session["BuyerUnApproval"] = null;
        Session["BuyerUnApproval"] = dtBuyer;
        if (dtBuyer.Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            gvBuyerDetails.DataSource = (DataTable)Session["BuyerUnApproval"];
            gvBuyerDetails.DataBind();
            SortingBuyerUnApproval(SortExpression_BA);
        }
        else
            UnApproved.Visible = false;
    }
    private void BindBuyerDetails_Approval()
    {
        DataTable dtBuyer = objBuyer.BuyerDetails(true);
        Session["BuyerApproval"] = null;
        Session["BuyerApproval"] = dtBuyer;
        if (dtBuyer.Rows.Count > 0)
        {
            btnApprovalSubmit.Visible = true;
            gvApprovaledBuyer.DataSource = (DataTable)Session["BuyerApproval"];
            gvApprovaledBuyer.DataBind();
            SortingBuyerApproval(SortExpression_BA);
        }
    }

    protected void gvBuyerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //unapproval 
        switch (e.CommandName)
        {
            case "Select":
                {
                    gvBuyerDetails.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                }
                break;
            case "View":
                {
                    Session["BuyerId"] = gvBuyerDetails.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    Response.Redirect("~/Buyer/BuyerDoc.aspx?BackUrl=~/Admin/BuyerApproalDetails.aspx");
                }
                break;
            case "cmd_delete":
                {
                    objBuyer.DeletetheBuyer(e.CommandArgument.ToString());
                    BindBuyerDetails_Unapproval();
                }
                break;

        }
    }

    protected void gvApprovaledBuyer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //approval 
        switch (e.CommandName)
        {
            case "Select":
                {
                    gvApprovaledBuyer.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                }
                break;
            case "View":
                {
                    Session["BuyerId"] = gvApprovaledBuyer.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    Response.Redirect("~/Buyer/BuyerDoc.aspx?BackUrl=~/Admin/BuyerApproalDetails.aspx");
                }
                break;
            case "cmd_delete":
                {
                    Session["BuyerId"] = gvApprovaledBuyer.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    Response.Redirect("~/Buyer/BuyerDoc.aspx?BackUrl=~/Admin/BuyerApproalDetails.aspx");
                }
                break;
            case "Edit":
                {
                    Session["BuyerId"] = gvApprovaledBuyer.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                    DataTable dtBuyer = objBuyer.BuyerDetails(Session["BuyerId"].ToString());
                    if (dtBuyer.Rows.Count > 0)
                    {
                        divbuyerApprovaledit.Visible = true;
                        txtBcode.Text = dtBuyer.Rows[0]["BuyerCode"].ToString();
                        txtDis.Text = dtBuyer.Rows[0]["Discount"].ToString();
                        txtFT.Text = dtBuyer.Rows[0]["FairTrade"].ToString();
                        txtFTP.Text = dtBuyer.Rows[0]["FairTradPremium"].ToString();
                        lblBuyerName.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString().ToUpper();
                        lblCountry.Text = dtBuyer.Rows[0]["CCountry"].ToString().ToUpper();
                        cbLotsample.Checked = Convert.ToBoolean(dtBuyer.Rows[0]["Lotsample"].ToString());
                        if (lblCountry.Text != "INDIA")
                        {
                            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true)
                            {
                                ddlPrice.Enabled = true;
                                ddlPrice.ClearSelection();
                                ddlPrice.SelectedIndex = 0;
                            }
                            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_AIR_By_WEST_USA"].ToString()) == true)
                            {
                                ddlPrice.Enabled = true;
                                ddlPrice.ClearSelection();
                                ddlPrice.SelectedIndex = 1;
                            }
                            if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Sea_By"].ToString()) == true)
                            {
                                if (Convert.ToBoolean(dtBuyer.Rows[0]["CIF_Seaport"].ToString()) == true)
                                {
                                    ddlPrice.Enabled = true;
                                    ddlPrice.ClearSelection();
                                    ddlPrice.SelectedIndex = 0;
                                }
                                else
                                {
                                    ddlPrice.Enabled = true;
                                    ddlPrice.ClearSelection();
                                    ddlPrice.SelectedIndex = 1;
                                }
                            }
                        }
                    }
                }
                break;
        }
    }

    public string dir
    {
        get
        {
            if (ViewState["dirState"].ToString() == "desc")
            {
                ViewState["dirState"] = "asc";
            }
            else
            {
                ViewState["dirState"] = "desc";
            }
            return ViewState["dirState"].ToString();
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingBuyerApproval(string SortExpression)
    {
        DataTable dt = (DataTable)Session["BuyerApproval"];
        Session["BuyerApproval"] = dt;
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvApprovaledBuyer.DataSource = sortedView;
        gvApprovaledBuyer.DataBind();
        foreach (GridViewRow gvr in gvApprovaledBuyer.Rows)
        {
            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = false;
            for (int k = 0; k < dt.Rows.Count; k++)
                if (gvApprovaledBuyer.DataKeys[gvr.RowIndex].Value.ToString() == dt.Rows[k]["Buyerid"].ToString())
                {
                    if (Convert.ToBoolean(dt.Rows[k]["CIF_Air_By_EuropeandEastUSA"].ToString()) == true)
                    {
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = true;
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).ClearSelection();
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex = 0;
                    }
                    if (Convert.ToBoolean(dt.Rows[k]["CIF_AIR_By_WEST_USA"].ToString()) == true)
                    {
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = true;
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).ClearSelection();
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex = 1;
                    }
                    if (Convert.ToBoolean(dt.Rows[k]["CIF_Sea_By"].ToString()) == true)
                    {
                        if (Convert.ToBoolean(dt.Rows[k]["CIF_Seaport"].ToString()) == true)
                        {
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = true;
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).ClearSelection();
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex = 0;
                        }
                        else
                        {
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = true;
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).ClearSelection();
                            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).SelectedIndex = 1;
                        }
                    }
                }
        }
    }
    private void SortingBuyerUnApproval(string SortExpression)
    {
        DataTable dt = (DataTable)Session["BuyerUnApproval"];
        Session["BuyerUnApproval"] = dt;
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + ViewState["dirState"];
        gvBuyerDetails.DataSource = sortedView;
        gvBuyerDetails.DataBind();
        foreach (GridViewRow gvr in gvBuyerDetails.Rows)
        {
            (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = false;
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                if (gvBuyerDetails.DataKeys[gvr.RowIndex].Value.ToString() == dt.Rows[k]["Buyerid"].ToString())
                    if (dt.Rows[k]["CCountry"].ToString() == "INDIA" || dt.Rows[k]["CCountry"].ToString() == " ")
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = false;
                    else
                        (gvr.Cells[0].FindControl("ddlPrice") as DropDownList).Enabled = true;

            }
        }
    }
    protected void gvBuyerDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_BU = e.SortExpression.ToString();
        SortingBuyerUnApproval(SortExpression_BU);
    }
    protected void gvApprovaledBuyer_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_BA = e.SortExpression.ToString();
        SortingBuyerApproval(SortExpression_BA);
    }
    protected void btnUnapproved_Click(object sender, EventArgs e)
    {
        BindBuyerDetails_Unapproval();
        UnApproved.Visible = true;
        Approved.Visible = false;
        //btnUnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnUnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //btnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
    protected void btnapproved_Click(object sender, EventArgs e)
    {
        Approve();
        //BindBuyerDetails_Approval();
        //UnApproved.Visible = false;
        //Approved.Visible = true;
        //btnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //btnUnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnUnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }

    protected void gvBuyerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[12].Attributes.Add("onClick", "return confirm('Are you sure delete this record?');");
        }
    }
    protected void gvApprovaledBuyer_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        bool result = false;
        int valueType = 0;
        valueType = ddlPrice.SelectedIndex + 1;
        result = objBuyer.BuyerApproval(Session["BuyerId"].ToString(), "Aslam", true, cbLotsample.Checked, txtBcode.Text, Convert.ToDecimal(txtDis.Text), Convert.ToDecimal(txtFT.Text), Convert.ToDecimal(txtFTP.Text), valueType);
        if (result == true)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Update the data successfully !!!');</script>");
            Approve();
            divbuyerApprovaledit.Visible = false;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divbuyerApprovaledit.Visible = false;
        Approve();
    }
    private void Approve()
    {
        BindBuyerDetails_Approval();
        UnApproved.Visible = false;
        Approved.Visible = true;
        //btnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        //btnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        //btnUnapproved.BackColor = System.Drawing.ColorTranslator.FromHtml("#9B336F");
        //btnUnapproved.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    }
}
