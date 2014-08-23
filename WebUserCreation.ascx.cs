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
    string Username = "";
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
        if ((Wizard1.ActiveStepIndex == 3) && (Session["TestIDList"] == null))
        {
            lblmsg.Text = "";
            checkdata();
            Wizard1.ActiveStepIndex = 0;
        }
        else
        {
            fillComboValues();
        }

    }
    private void fillComboValues()
    {
        // int orgvalue = 0;
        ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlOrg.Items.Clear();
        ddlOrg.Items.Add(listnew);
        int orgIndex = 0;

        if (Session["OrgIndex"] != null)
        {
            orgIndex = int.Parse(Session["OrgIndex"].ToString());
        }

        ddlOrg.DataSource = OrgLinqDataSource;
        ddlOrg.DataTextField = "Name";
        ddlOrg.DataValueField = "OrganizationID";
        ddlOrg.DataBind();
        if (ddlOrg.SelectedValue == "00")
            ////// txtOrganization.Visible = true;

            listnew = new ListItem("--select--", "0");
        ddlUserGroup.Items.Clear();
        ddlUserGroup.Items.Add(listnew);
        ddlUserGroup.DataSource = GrpUserLinqDataSource;
        ddlUserGroup.DataTextField = "GroupName";
        ddlUserGroup.DataValueField = "GroupUserID";
        ddlUserGroup.DataBind();
        if (orgIndex > 0)
        {
            //////fillprofiles();
        }

        listnew = new ListItem("--select--", "0");
        ddlQualification.Items.Clear();
        ddlQualification.Items.Add(listnew);
        ddlQualification.DataSource = LinqQualifications;
        ddlQualification.DataTextField = "Qualification1";
        ddlQualification.DataValueField = "Qualificationid";
        ddlQualification.DataBind();
        listnew = new ListItem("--other--", "00");
        ddlQualification.Items.Add(listnew);
        if (Session["qualIndex"] != null)
        {
            ddlQualification.SelectedIndex = int.Parse(Session["qualIndex"].ToString());
            if (ddlQualification.SelectedValue == "00")
                txtEduQual.Visible = true;
        }
        int industryindex = 0;
        if (Session["industryindex"] != null)
            industryindex = int.Parse(Session["industryindex"].ToString());
        listnew = new ListItem("--select--", "0");
        ddlIndustry.Items.Clear();
        ddlIndustry.Items.Add(listnew);
        ddlIndustry.DataSource = LinqDataSource3;
        ddlIndustry.DataTextField = "Name";
        ddlIndustry.DataValueField = "IndustryID";
        ddlIndustry.DataBind();
        listnew = new ListItem("--other--", "00");
        ddlIndustry.Items.Add(listnew);
        ddlIndustry.SelectedIndex = industryindex;
        if (ddlIndustry.SelectedValue == "00")
            txtIndustry.Visible = true;
       
    }


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
                Session["UserID"] = userid_new;
                //CheckUserDetails(userid_new, "Yes");
                Session["SubCtrl"] = "TestListControl.ascx";
                Response.Redirect("FJAHome.aspx");
            }
            else lblmsg.Text = "Invalid username/password";
        }
        else lblmsg.Text = "Enter username/password";
    }
    //private void CheckUserDetails(int userid, string dirlog)
    //{
    //    if (dirlog == "No")
    //        Session.Clear();

    //    var LoginDetails1 = from LoginDetails in dataclasses.UserProfiles
    //                        where LoginDetails.UserId == userid && LoginDetails.Status == 1
    //                        select LoginDetails;
    //    if (LoginDetails1.Count() > 0)
    //    {
    //        if (LoginDetails1.First().UserId != null)
    //        {
    //            Session["UserID"] = LoginDetails1.First().UserId.ToString();
    //            userid = int.Parse(Session["UserID"].ToString());
    //        }
    //        if (LoginDetails1.First().ReportAccess != null)
    //            Session["CurUserReportAccess"] = LoginDetails1.First().ReportAccess.ToString();
    //        int testid = 0;
    //        if (LoginDetails1.First().TestId != null && LoginDetails1.First().TestId != 0)
    //        {
    //            Session["UserTestId"] = int.Parse(LoginDetails1.First().TestId.ToString());
    //            testid = int.Parse(LoginDetails1.First().TestId.ToString());
    //        }
    //        else
    //        {
    //            lblmsg.Text = "No Test assigned for you. Please Contact to your admin ";
    //            return;
    //        }

            
    //    }

    //}
    protected void ddlrecruiter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrecruiter.SelectedItem.Text == "Yes")
        {
            txtrecrutr.Enabled = true;
        }
        else
        {
            txtrecrutr.Enabled = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtFsName.Text.Trim()=="" || txtEmailId.Text.Trim()=="")// || ddlOrg.SelectedIndex < 0 || ddlUserGroup.SelectedIndex < 0)
        { lblMessage.Text = "Enter the values"; }
        else
        {
            try
            {
                if (txtPassword.Text != "")
                    ViewState["Password"] = txtPassword.Text;
                DateTime dtFrom = DateTime.Today;
                DateTime dtTo = DateTime.Today.AddDays(1);
                var checkDuplication = from userdetails in dataclasses.UserProfiles
                                       where (userdetails.UserName == txtUserName.Text.Trim() && userdetails.Password == txtPassword.Text.Trim())
                                       select userdetails;
                if (checkDuplication.Count() > 0)
                { lblMessage.Text = "Username/password already exists"; }
                
                string emailid = txtEmailId.Text.Trim();
                int userCode = 0;
                int age = 0;
                if (CheckAge() == false)
                {
                    lblMessage.Text = "Enter valid age";
                    return;
                }
                int industryid = 0, orgid = 0;
                string qualification = "";
                qualification = ddlQualification.SelectedItem.Text;

                if (ddlOrg.SelectedIndex > 0)
                    orgid = int.Parse(ddlOrg.SelectedValue);

                if (ddlIndustry.SelectedIndex > 0)
                    industryid = int.Parse(ddlIndustry.SelectedValue);

                if (txtAge.Text != null)
                    age = int.Parse(txtAge.Text);
                string tests = "";
                CreateUsernamePwd();
                if (Session["TestIDList"] != null)
                {
                    dataclasses.AddUser(Username,Username, "User",int.Parse(ddlOrg.SelectedValue),
                        int.Parse(ddlUserGroup.SelectedValue),dtFrom,dtTo,1,0,txtEmailId.Text,0,1,txtfirstname.Text,txtMidName.Text,txtLstName.Text,ddlGender.SelectedValue,age,
                        industryid,int.Parse(ddlJobCatgy.SelectedValue), txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue),
                        int.Parse(ddlTotExpMonths.SelectedValue), int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue),
                        ddlQualification.SelectedItem.Text, txtProffQual.Text, txtPhoneNumber.Text, txtrecrutr.Text);
                    int uid = 0;
                    var TakeUserid = from userdetails in dataclasses.UserProfiles
                                           where (userdetails.UserName == Username && userdetails.Password == Username)
                                           select userdetails;
                    if (TakeUserid.Count() > 0)
                    {
                        uid = TakeUserid.First().UserId;
                        Session["UserID"] = uid;
                    }
                    tests = Convert.ToString(Session["TestIDList"]);
                    tests = tests.Substring(1, tests.Length - 1);
                    if (String.IsNullOrEmpty(tests))
                    { }
                    else
                    {
                        foreach (var s in tests.Split(','))
                        {
                            int num;
                            if (int.TryParse(s, out num))
                                dataclasses.AddUserTestList(0,num,uid,"", "NOTTAKEN", DateTime.Now, 0, DateTime.Now, DateTime.Now.AddDays(1),0);
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Select  test and proceed";
                }
                //dataclasses.AddUser(txtUserName.Text, txtPassword.Text, "User", int.Parse(ddlOrg.SelectedValue),
                //    int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, 1, 0, txtEmailId.Text, int.Parse(Session["TestID"].ToString()),
                //    1, tests, txtFsName.Text, txtMidName.Text, txtLstName.Text, ddlGender.SelectedValue,
                //    age, industryid, int.Parse(ddlJobCatgy.SelectedValue),txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), 
                //    int.Parse(ddlTotExpMonths.SelectedValue),int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue),
                //    ddlQualification.SelectedItem.Text,txtProffQual.Text, txtPhoneNumber.Text,txtrecrutr.Text );
                Session["UserTestId"] = tests; 
                lblMessage.Text = "Profile Details Saved Successfully";
                Wizard1.ActiveStepIndex = 2;
                //Session["SubCtrl"] = "UserTrainingControl.ascx";
                //Response.Redirect("FJAHome.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                return;
            }
        }
    }
    private string CreateUsernamePwd()
    {

        var lastUserID = from userdetails in dataclasses.UserProfiles
                         orderby userdetails.UserId descending
                         select userdetails;
        if (lastUserID.Count() > 0)
        {
            Username = txtFsName.Text.Trim().Substring(0, 4).ToUpper() + "00" + (lastUserID.First().UserId + 1);
        }
        else
        {
            Username = txtFsName.Text.Trim().Substring(0, 4).ToUpper() + "00" + 1;
        }
        return Username;
    }
    private bool CheckAge()
    {
        try
        {
            int age = int.Parse(txtAge.Text.Trim());
            if (age <= 12) { lblMessage.Text = "age should be greater than 12"; return false; }

            return true;
        }
        catch (Exception ex) { lblMessage.Text = "Enter valid age"; return false; }
    }
    protected void btn_payment_Click(object sender, EventArgs e)
    {
        
    }
    protected void btntaketest_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Session["TestIDList"] != null)
            //{

            //}
           // Session["UserTestId"] = Session["UserTestId"];
            Session["UserID"] = Session["UserID"];
            Session["SubCtrl"] = "TestListControl.ascx";
            Response.Redirect("FJAHome.aspx");
        }
        catch (Exception ex)
        {
        }
    }
}
