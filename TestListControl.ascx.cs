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
            ////if (Session["curtestid"] != "")
            ////{
            ////    Button1.Visible = true;
            ////    Button2.Visible = false;
            ////}
            ////else
            ////{
            ////    Button1.Visible = false;
            ////    Button2.Visible = true ;
            ////}
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
                        if(grd_usertest.Rows[i].Cells[5].Text=="NOTTAKEN")
                        {
                            Button1.Visible = true;
                            Button2.Visible = false;
                        }
                        else
                        {
                            Button1.Visible = false ;
                            Button2.Visible = true ;
                        }
                    }
                }
            }
        }
        catch (Exception es)
        { }
    
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            var usertestdet = from userdetails in dataclass.UserTestLists
                              where (userdetails.UserId == int.Parse(Session["UserID"].ToString()) && userdetails.UserTestId == int.Parse(Session["curtestid"].ToString())) && userdetails.ReportAccess==1)
                          select userdetails;

            if (usertestdet.Count() > 0)
            {
                string reptype = "Interpretative Report";//Indicative Report//Certification Report
            string scortype = "Percentage";//Percentile
            if (Session["curtestid"] != null)// || ddlUserList.SelectedIndex > 0)
            {
                if (Session["UserID"]!=null)
                {
                    Session["UserId_Report"] = Session["UserID"];
                    Session["usertype"] = "User";
                    if (reptype == "Interpretative Report")
                        Session["SubCtrl"] = "ReportPreviewCtrl.ascx";
                    else if (reptype == "Indicative Report")
                        Session["SubCtrl"] = "ReportPreviewCtrl_IdvlRpt.ascx";
                    else
                        Session["SubCtrl"] = "ReportPreviewCtrl_Certify.ascx";

                    int userid = int.Parse(Session["UserID"].ToString());
                    int testid = int.Parse(Session["curtestid"].ToString());
                    dataclass.DeleteSectionMarks(userid, testid);
                }
                //else
                //{
                //    if (ddlUserGroup.SelectedIndex > 0)
                //        Session["UserGroupID_Report"] = ddlUserGroup.SelectedValue;
                //    else { lblMessage.Text = "Please select a Group from the list"; return; }

                //    Session["UserId_Report"] = null;
                //    Session["SubCtrl"] = "ReportPreviewCtrl_GrpRpt.ascx";
                //}

                Session["ReportType"] = reptype;
                Session["ScoringType"] = scortype ;
                Session["UserTestID_Report"] = Session["curtestid"]; //GetUserTestId(ddlUserList.SelectedValue);

                //["TestName"] = ddlTestList.SelectedItem.Text;

                //Session["OrganiationID_Report"] = ddlOrganizationList.SelectedValue;
                Response.Redirect("FJAHome.aspx");

            }
            //else if (ddlTestList.SelectedIndex <= 0 && ddlUserList.SelectedIndex <= 0)
            //{ 
            //    lblMessage.Text = "Please select a Test/User from the list";
            //}
            }
            
        }
        catch (Exception ex)
        { }
    }
}