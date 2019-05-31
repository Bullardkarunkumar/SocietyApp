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
using System.Xml.Linq;
using MudarOrganic.BL;

public partial class Admin_HoildayList : System.Web.UI.Page
{
    InspectionPlan_BL ip = new InspectionPlan_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnHolidaysList();
            BindYears();
            gvHolidayList.Visible = false;
            BindHolidaysList();
        }
    }
    private void BindYears()
    {
        ListItemCollection items = MudarApp.BindYear();
        foreach (ListItem item in items)
            ddlyear.Items.Add(item);
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void BindHolidaysList()
    {
        //DataTable dsHolidayslist = ip.GetHolidaysList();
        DataTable dsHolidayslist = ip.GetHolidaysListByYear(Convert.ToInt32(DateTime.Now.Year.ToString()));
        if (dsHolidayslist.Rows.Count > 0)
        {
            lblHCount.Visible = true;
            lblHCount.Text = dsHolidayslist.Rows.Count.ToString();
            gvHolidayList.Visible = true;
            gvHolidayList.DataSource = dsHolidayslist;
            gvHolidayList.DataBind();
        }
        else
        {
            gvHolidayList.Visible = false;
        }
    }
    private void UpdateHolidaysList(int HolidayID)
    {

    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtHolidayDate.Text))
        {
            bool result;
            bool check = ip.HolidayExist(Convert.ToDateTime(txtHolidayDate.Text));
            if (check == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! Enter the Date is Already Inserted !!!')", true);
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(lblHolidayID.Text))
                {
                    result = ip.HolidayList_INSandUPDandDEL(Convert.ToInt32(lblHolidayID.Text), Convert.ToInt32(ddlyear.Text), Convert.ToDateTime(txtHolidayDate.Text), "", "bhanu", 2);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! update successfully !!!')", true);
                    return;
                }
                else
                {
                    result = ip.HolidayList_INSandUPDandDEL(0, Convert.ToInt32(ddlyear.Text), Convert.ToDateTime(txtHolidayDate.Text), "bhanu", "", 1);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! saved successfully !!!')", true);
                    return;
                }
            }
            ////ddlyear.Items.Clear();
            //txtHolidayDate.Text = string.Empty;
            ////BindYears();
            //BindHolidaysList();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "fnShowMessage('!!! plz Enter the Date !!!')", true);
            return;
        }
    }
    protected void gvHolidayList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        bool result;
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "cmd_edit")
        {
            int HolidayID = Convert.ToInt32(gvHolidayList.DataKeys[index].Value.ToString());
            DataTable dsHolidayslist = ip.GetHolidaysListBasedonHolidayID(HolidayID);
            lblHolidayID.Text = HolidayID.ToString();
            if (dsHolidayslist.Rows.Count > 0)
            {
                ddlyear.Text = dsHolidayslist.Rows[0]["HolidayYear"].ToString();
                txtHolidayDate.Text = dsHolidayslist.Rows[0]["HolidayDate"].ToString();
            }
        }
        if (e.CommandName == "cmd_delete")
        {
            int HolidayID = Convert.ToInt32(gvHolidayList.DataKeys[index].Value.ToString());
            result = ip.HolidayList_INSandUPDandDEL(HolidayID, 0, DateTime.Now, string.Empty, "bhanu", 3);
            BindHolidaysList();
        }
    }
    protected void gvHolidayList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Attributes.Add("onClick", "return confirm('Are you sure delete this record?');");
        }
    }
}
