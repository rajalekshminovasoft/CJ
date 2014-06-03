using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using PayPal.Api;
//using 

public partial class WebUserCreation : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        //listnew = new ListItem("--select--", "0");
        //dtTestlist .Items.Clear();
        ////dtTestlist.Items.Add(listnew);
        dtTestlist.DataSource = LinqTestLists;        
        dtTestlist.DataBind();
        //if ((IsPostBack == true) && (Session["TestIDList"]!=null))
        //{
        //    Wizard1.ActiveStepIndex = 0;
        //}
    }


    //protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    //{

    //}
    //protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
    //{

    //}

    //protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    //{

    //}
    //protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    //{
    //    if (Wizard1.ActiveStepIndex == 0)
    //    {
    //        Label1.Text = Wizard1.ActiveStepIndex.ToString();
    //        Session["TestID"] = "";
    //        foreach (DataListItem dli in dtTestlist.Items)
    //        {
    //            CheckBox productID = ((CheckBox)dli.FindControl("chktest"));
    //            if (productID.Checked == true)
    //            {
    //                Session["TestID"] = ((Label)dli.FindControl("lbltestid")).Text;
    //                Session["TestIDList"] = Session["TestIDList"] + "," + Session["TestID"];
    //            }
    //        }
    //        //Label1.Text = Session["TestID"].ToString();
    //    }
    //    if((Wizard1.ActiveStepIndex>0) && (Session["TestIDList"] == ""))
    //    {
    //        lblmsg.Text ="Select Atleast One Test";            }
    //    }
       

    //}
    //protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    //{

    //}
    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    //protected void btnstp1_Click(object sender, EventArgs e)
    //{
    //    Wizard1.ActiveStepIndex  = 0;
    //}
    //protected void btnstp2_Click(object sender, EventArgs e)
    //{
    //    Wizard1.ActiveStepIndex = 1;
    //}
    //protected void btnstp3_Click(object sender, EventArgs e)
    //{
    //    Wizard1.ActiveStepIndex = 2;
    //}
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        checkdata();
    }
    private void checkdata()
    {
        if (Wizard1.ActiveStepIndex == 0)
        {
            //Label1.Text = Wizard1.ActiveStepIndex.ToString();
            Session["TestID"] = "";
            foreach (DataListItem dli in dtTestlist.Items)
            {
                CheckBox productID = ((CheckBox)dli.FindControl("chktest"));
                if (productID.Checked == true)
                {
                    Session["TestID"] = ((Label)dli.FindControl("lbltestid")).Text;
                    Session["TestIDList"] = Session["TestIDList"] + "," + Session["TestID"];
                }
            }
            //Label1.Text = Session["TestID"].ToString();
        }
        if( (Session["TestIDList"] == null))
        {
            lblmsg.Text ="Select Atleast One Test";       
        }
       
    }
    protected void btnstp2_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        checkdata();
        Wizard1.ActiveStepIndex = 1;
    }
    protected void btnstp3_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        checkdata();
        Wizard1.ActiveStepIndex = 2;
    }
    protected void btnstp1_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        Wizard1.ActiveStepIndex = 0;
    }
    protected void lnkSignup_Click(object sender, EventArgs e)
    {
        Wizard1.ActiveStepIndex = 3;
    }
    protected void lnklogin_Click(object sender, EventArgs e)
    {
        if (txtuname.Text != "" && txtpwd.Text != "")
        {
            Session.Clear();
            Session["dirLogin"] = "Yes";

            int userid_new = 0;

            var LoginDetails1 = from LoginDetails in dataclasses.UserProfiles
                                where LoginDetails.UserName == txtuname.Text && LoginDetails.Password == txtpwd.Text && LoginDetails.Status == 1
                                select LoginDetails;
            //var LoginDetails1 = from LoginDetails in dataclass.UserProfiles
            //                    where LoginDetails.UserName == "admin" && LoginDetails.Password == "123" && LoginDetails.Status == 1
            //                    select LoginDetails;
            //lblmsg.Text = LoginDetails1.Count().ToString();
            if (LoginDetails1.Count() > 0)
            {
                userid_new = int.Parse(LoginDetails1.First().UserId.ToString());
                CheckUserDetails(userid_new, "Yes");
            }
            else lblmsg.Text = "Invalid username/password";
        }
        else lblmsg.Text = "Enter username/password";
    }
    private void CheckUserDetails(int userid, string dirlog)
    {
        if (dirlog == "No")
            Session.Clear();

        var LoginDetails1 = from LoginDetails in dataclasses.UserProfiles
                            where LoginDetails.UserId == userid && LoginDetails.Status == 1
                            select LoginDetails;
        if (LoginDetails1.Count() > 0)
        {
            if (LoginDetails1.First().UserId != null)
            {
                Session["UserID"] = LoginDetails1.First().UserId.ToString();
                userid = int.Parse(Session["UserID"].ToString());
            }
            if (LoginDetails1.First().ReportAccess != null)
                Session["CurUserReportAccess"] = LoginDetails1.First().ReportAccess.ToString();
            int testid = 0;
            if (LoginDetails1.First().TestId != null && LoginDetails1.First().TestId != 0)
            {
                Session["UserTestId"] = int.Parse(LoginDetails1.First().TestId.ToString());
                testid = int.Parse(LoginDetails1.First().TestId.ToString());
            }
            else
            {
                lblmsg.Text = "No Test assigned for you. Please Contact to your admin ";
                return;
            }

            
        }

    }
}
