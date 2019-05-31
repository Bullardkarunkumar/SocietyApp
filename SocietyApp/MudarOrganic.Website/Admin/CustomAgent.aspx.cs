using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class Admin_CustomBroker : System.Web.UI.Page
{
    CustomAgent_BL CBL = new CustomAgent_BL();
    public static string SortExpression_a = "AgentCode";
    bool result = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnCustomAgent();
            btnAgent.Visible = true;
            BindAgentList();
            AgentForm.Visible = false;
        }
    }
    private void BindAgentList()
    {
        DataTable dt = CBL.GetAgentDetails();
        Session["CustomAgent"] = null;
        Session["CustomAgent"] = dt;
        gvAgent.DataSource = (DataTable)Session["CustomAgent"];
        gvAgent.DataBind();
        SortingAgent(SortExpression_a);
        
    }
    protected void btnDeliveryClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/CustomAgent.aspx");
    }
    protected void btnAddAgent_Click(object sender, EventArgs e)
    {
        gvAgent.Visible = false;
        btnAddAgent.Visible = false;
        AgentForm.Visible = true;
    }
    protected void gvAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        string str = e.CommandArgument.GetType().ToString();
        if (e.CommandArgument.ToString() != "AgentCode" && e.CommandArgument.ToString() != "AgentName" && e.CommandArgument.ToString() != "Place" && e.CommandArgument.ToString() != "ModeofTransport" && e.CommandArgument.ToString() != "AddressforDelivery")
            index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Agent")
        {
            string AgentID = gvAgent.DataKeys[index].Value.ToString();
            lblAgentID.Text = AgentID;
            GetAgentDetails(AgentID);
            AgentForm.Visible = true;
            btnAddAgent.Visible = false;
            gvAgent.Visible = false;
        }
    }
    protected void btnAgentSubmit_Click(object sender, EventArgs e)
    {
        gvAgent.Visible = false;
        if (ddlTransportMode.SelectedValue == "--select--")
        {
            Response.Write("<script>alert('select the TransportMode');</script>");
        }
        else
        {
            if (!string.IsNullOrEmpty(lblAgentID.Text))
            {
                string agentaddress = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;
                result = CBL.CustomDetails_INSandUPDandDEL(lblAgentID.Text, txtAgentcode.Text, txtcontactperson.Text,txtAgentname.Text, txtPlace.Text, Convert.ToInt32(ddlTransportMode.SelectedValue), txtDeliveyAddress.Text, agentaddress, txtCity.Text, txtState.Text, txtCountry.Text, txtPhoneNo.Text, txtMphone.Text, txtEmail.Text, txtZipCode.Text, "bhanu", string.Empty, MudarApp.Update);
            }
            else
            {
                lblAgentID.Text = Guid.NewGuid().ToString();
                string agentaddress = txtAddress1.Text + "@" + txtAddress2.Text + "@" + txtAddress3.Text;
                result = CBL.CustomDetails_INSandUPDandDEL(lblAgentID.Text, txtAgentcode.Text, txtcontactperson.Text, txtAgentname.Text, txtPlace.Text, Convert.ToInt32(ddlTransportMode.SelectedValue), txtDeliveyAddress.Text, agentaddress, txtCity.Text, txtState.Text, txtCountry.Text, txtPhoneNo.Text, txtMphone.Text, txtEmail.Text, txtZipCode.Text, "bhanu", string.Empty, MudarApp.Insert);
            }
            BindAgentList();
            AgentForm.Visible = false;
            gvAgent.Visible = true;
            btnAddAgent.Visible = true;
        }
        lblAgentID.Text = string.Empty;
    }
    private void GetAgentDetails(string CustomAgentId)
    {
        DataTable dtAgentDetails = CBL.GetAgentDetails(CustomAgentId);
        if (dtAgentDetails.Rows.Count > 0)
        {
            txtAgentcode.Text = dtAgentDetails.Rows[0]["AgentCode"].ToString();
            txtAgentname.Text = dtAgentDetails.Rows[0]["AgentName"].ToString();
            txtcontactperson.Text = dtAgentDetails.Rows[0]["contactperson"].ToString();
            txtPlace.Text = dtAgentDetails.Rows[0]["place"].ToString();
            string[] Address = dtAgentDetails.Rows[0]["AgentAddress"].ToString().Split('@');
            txtAddress1.Text = Address[0].ToString();
            txtAddress2.Text = Address[1].ToString();
            txtAddress3.Text = Address[2].ToString();
            txtDeliveyAddress.Text = dtAgentDetails.Rows[0]["AddressforDelivery"].ToString();
            ddlTransportMode.ClearSelection();
            ListItem list = ddlTransportMode.Items.FindByValue(dtAgentDetails.Rows[0]["ModeofTransport"].ToString());
            list.Selected = true;
            txtCity.Text = dtAgentDetails.Rows[0]["AgentCity"].ToString();
            txtCountry.Text= dtAgentDetails.Rows[0]["AgentName"].ToString();
            txtState.Text = dtAgentDetails.Rows[0]["AgentState"].ToString();
            txtEmail.Text = dtAgentDetails.Rows[0]["Email"].ToString();
            txtZipCode.Text = dtAgentDetails.Rows[0]["ZipCode"].ToString();
            txtPhoneNo.Text = dtAgentDetails.Rows[0]["Phone"].ToString();
            txtMphone.Text = dtAgentDetails.Rows[0]["Mphone"].ToString();
        }
    }
    protected void gvAgent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text.Trim() == "0" || e.Row.Cells[3].Text.Trim() == "1" || e.Row.Cells[3].Text.Trim() == "2" || e.Row.Cells[3].Text.Trim() == "3")
            {
                e.Row.Cells[3].Text = Transport.TransportMode[Convert.ToInt32(e.Row.Cells[3].Text.Trim())].ToString();
            }
        }
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private void SortingAgent(string SortExpression)
    {
        DataTable dt = (DataTable)Session["CustomAgent"];
        Session["CustomAgent"] = dt;
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        else
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        DataView sortedView = new DataView(dt);
        sortedView.Sort = SortExpression + " " + sortingDirection;
        gvAgent.DataSource = sortedView;
        gvAgent.DataBind();
    }
    protected void gvAgent_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression_a = e.SortExpression.ToString();
        SortingAgent(SortExpression_a);

    }
}
