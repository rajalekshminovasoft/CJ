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

public partial class TestListControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    int userid = 0;
    public static int iVal = -1;
    public static int jVal = -2;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
            FillDatagrid();
            if (Session["curtestid"] != "")
            {
                Button1.Visible = true;
                Button2.Visible = false;
            }
            else
            {
                Button1.Visible = false;
                Button2.Visible = false;
            }
        //}
    }
    private void FillDatagrid()
    {
        //if (Session["usertype"].ToString() == "User")
        //{
        var usertestdet = from userdetails in dataclass.View_UserTests
                          where (userdetails.UserId == int.Parse(Session["UserID"].ToString()))
                          select userdetails;

        if (usertestdet.Count() > 0)
        {
            grd_usertest.DataSource = usertestdet;
            grd_usertest.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["curtestid"] != null)
        {
            Session["SubCtrl"] = "TestIntroductionControl.ascx";
            Response.Redirect("FJAHome.aspx");
        }
    }
    protected void grd_usertest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int i;
            CheckBox chk=(CheckBox)grd_usertest.SelectedRow.FindControl("chkSelect");
            iVal = grd_usertest.SelectedIndex;
            if (chk.Checked == true)
            {
                for (i = 0; i >= grd_usertest.Rows.Count - 1; i++)
                {
                    chk = (CheckBox)grd_usertest.Rows[i].FindControl("chkSelect");
                    if (i == iVal)
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //// int i;
            //GridViewRow row = grd_usertest.Rows[grd_usertest.SelectedIndex];
            ////CheckBox chk = row.FindControl(0);
            //////    CheckBox chk = (CheckBox)grd_usertest.SelectedRow.FindControl("chkSelect");
            //////    iVal = grd_usertest.SelectedIndex;
            //////    if (chk.Checked == true)
            //////    {
            //////        for (i = 0; i >= grd_usertest.Rows.Count - 1; i++)
            //////        {
            //////            chk = (CheckBox)grd_usertest.Rows[i].FindControl("chkSelect");
            //////            if (i == iVal)
            //////            {
            //////                chk.Checked = true;
            //////            }
            //////            else
            //////            {
            //////                chk.Checked = false;
            //////            }
            //////        }
            //////    }
            //////}
            //////catch (Exception ex)
            //////{ }
            ////////int i;
            ////////CheckBox chk;
            ////////for (i = 0; i <= grd_usertest.Rows.Count - 1; i++)
            ////////{
            ////////    chk = (CheckBox)grd_usertest.Rows[i].FindControl("chkSelect");
            ////////    if (chk.Checked == true)
            ////////    {
            ////////        if (jVal != i)
            ////////        {
            ////////            iVal = i;
            ////////            jVal = i;
            ////////            if (jVal <= i)
            ////////            {
            ////////                break;
            ////////            }
            ////////        }
            ////////    }
            ////////}
            ////////for (i = 0; i >= grd_usertest.Rows.Count - 1; i++)
            ////////{
            ////////    chk = (CheckBox)grd_usertest.Rows[i].FindControl("chkSelect");
            ////////    if (i == iVal)
            ////////    {
            ////////        chk.Checked = true;
            ////////    }
            ////////    else
            ////////    {
            ////////        chk.Checked = false;
            ////////    }
            ////////}
            CheckBox chk = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chk.NamingContainer;
            int rownumber = gv.RowIndex;

            if (chk.Checked)
            {
                int i;
                for (i = 0; i <= grd_usertest.Rows.Count - 1; i++)
                {
                    if (i != rownumber)
                    {
                        CheckBox chkcheckbox = ((CheckBox)(grd_usertest.Rows[i].FindControl("chkSelect")));
                        chkcheckbox.Checked = false;

                    }
                    else
                    {
                        Session["curtestid"] = int.Parse(grd_usertest.Rows[i].Cells[9].Text);
                    }
                }
            }
        }
        catch (Exception es)
        { }
    
    }
}