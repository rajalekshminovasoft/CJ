using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserCreationFromSite : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        fillComboValues();
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
        //////listnew = new ListItem("-- other --", "00");
        //////ddlOrg.Items.Add(listnew);
        //////ddlOrg.SelectedIndex = orgIndex;
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


        //

        //ListItem listnew;
        listnew = new ListItem("--select--", "0");
        ddlTestLists.Items.Clear();
        ddlTestLists.Items.Add(listnew);
        //if (orgIndex > 0)
        //{
        //////var usertestdetailfromdb = from usertestdet in dataclasses.TestLists
        //////                           where usertestdet.OrganizationName == ddlOrg.SelectedItem.Text
        //////                           select new { usertestdet.TestName, usertestdet.TestId };
        //////ddlTestLists.DataSource = usertestdetailfromdb;
        ddlTestLists.DataSource=LinqTestLists;
        ddlTestLists.DataTextField = "TestName";
        ddlTestLists.DataValueField = "TestId";
        ddlTestLists.DataBind();
        listnew = new ListItem("--select--", "0");
        ddlTestlIst2.Items.Clear();
        ddlTestlIst2.Items.Add(listnew);
        ddlTestlIst2.DataSource = LinqDataSource1;
        ddlTestlIst2.DataTextField = "TestName";
        ddlTestlIst2.DataValueField = "TestId";
        ddlTestlIst2.DataBind();
    }
    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ListItem listnew = new ListItem("--select--", "0");
        //ddlGrpUser.Items.Clear();
        //ddlGrpUser.Items.Add(listnew);
        ////if (ddlOrg.SelectedValue != "00" && ddlOrg.SelectedIndex > 0)
        ////{
        //    GrpUserLinqDataSource.Where = "OrganizationID = " + ddlOrg.SelectedValue;
        //    ddlGrpUser.DataSource = GrpUserLinqDataSource;
        //    ddlGrpUser.DataTextField = "GroupName";
        //    ddlGrpUser.DataValueField = "GroupUserID";
        //    ddlGrpUser.DataBind();
        ////}
    }
    private Boolean ValidateMandatory()
    {
        Boolean result = true;
        //txtJob.Text == "" ||
        if (txtFsName.Text == "" || txtLstName.Text.Trim() == "" || txtAge.Text.Trim() == "" || ddlJobCatgy.SelectedIndex <= 0 || (txtEduQual.Text == "" && (ddlQualification.SelectedValue == "0" || ddlQualification.SelectedValue == "00")))
            result = false;
        return result;
    }
    //private Boolean AddUser()
    //{
    //    if (txtUserName.Text.Trim() == "" || ddlUserType.SelectedIndex <= 0 || txtPassword.Text.Trim() == "")// || ddlOrg.SelectedIndex < 0 || ddlUserGroup.SelectedIndex < 0)
    //    { lblMessage.Text = "Enter the values"; }
    //    else
    //    {
    //        if (txtPassword.Text != "")
    //            ViewState["Password"] = txtPassword.Text;            
    //        DateTime dtFrom = DateTime.Today;
    //        DateTime dtTo = DateTime.Today.AddDays(1);
    //        var checkDuplication = from userdetails in dataclasses.UserProfiles
    //                               where (userdetails.UserName == txtUserName.Text.Trim() && userdetails.Password == txtPassword.Text.Trim()) && userdetails.UserId != userCode
    //                               select userdetails;
    //        if (checkDuplication.Count() > 0)
    //        { lblMessage.Text = "Username/password already exists"; }
    //        int testid = 0;
    //        int testid1 = 0;
    //        if (ddlTestLists.SelectedIndex > 0)
    //            testid = int.Parse(ddlTestLists.SelectedValue);
    //        //if (testid == 0)// 19012010 bip
    //        //{ lblMessage.Text = "Please select a Test"; return; }
    //        ////dataclasses = new AssesmentDataClassesDataContext();
    //        if (ddlTestlIst2.SelectedIndex > 0)
    //            testid1 = int.Parse(ddlTestlIst2.SelectedValue);

    //        int status = int.Parse(ddlStatus.SelectedValue);
    //        string emailid = txtEmailId.Text.Trim();
    //        int userCode = 0;
    //        dataclasses.AddUserCreation(userCode, txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue), int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, emailid, testid, 1, testid1);
            
    //        //string mailbody = GenerateMailBody(txtUserName.Text, txtPassword.Text);
    //        //if (emailid != "")
    //        //    SendEmail(emailid, "", "Login Details", mailbody);
    //        //if(emailid!="")
    //        // SendMail(emailid, "", "Login Details", mailbody);
    //        lblMessage.Text = "Values Saved";
    //    }
    //}
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


    //private void test()
    //{
    //    //Session["SubCtrl"] = "UserTrainingControl.ascx";
    //    //Response.Redirect("FJAHome.aspx");
    //    //*************************************************
    //    if (txtUserName.Text.Trim() == "" || ddlUserType.SelectedIndex <= 0 || txtPassword.Text.Trim() == "")// || ddlOrg.SelectedIndex < 0 || ddlUserGroup.SelectedIndex < 0)
    //    { lblMessage.Text = "Enter the values"; }
    //    else
    //    {
    //        if (txtPassword.Text != "")
    //            ViewState["Password"] = txtPassword.Text;
    //        DateTime dtFrom = DateTime.Today;
    //        DateTime dtTo = DateTime.Today.AddDays(1);
    //        var checkDuplication = from userdetails in dataclasses.UserProfiles
    //                               where (userdetails.UserName == txtUserName.Text.Trim() && userdetails.Password == txtPassword.Text.Trim())
    //                               select userdetails;
    //        if (checkDuplication.Count() > 0)
    //        { lblMessage.Text = "Username/password already exists"; }
    //        int testid = 0;
    //        int testid1 = 0;
    //        if (ddlTestLists.SelectedIndex > 0)
    //            testid = int.Parse(ddlTestLists.SelectedValue);
    //        //if (testid == 0)// 19012010 bip
    //        //{ lblMessage.Text = "Please select a Test"; return; }
    //        ////dataclasses = new AssesmentDataClassesDataContext();
    //        if (ddlTestlIst2.SelectedIndex > 0)
    //            testid1 = int.Parse(ddlTestlIst2.SelectedValue);

    //        int status = int.Parse(ddlStatus.SelectedValue);
    //        string emailid = txtEmailId.Text.Trim();
    //        int userCode = 0;
    //        //dataclasses.AddUserCreation(userCode, txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue), int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, emailid, testid, 1, testid1);

    //        //string mailbody = GenerateMailBody(txtUserName.Text, txtPassword.Text);
    //        //if (emailid != "")
    //        //    SendEmail(emailid, "", "Login Details", mailbody);
    //        //if(emailid!="")
    //        // SendMail(emailid, "", "Login Details", mailbody);
    //        //////lblMessage.Text = "Values Saved";
    //        ////// }
    //        //*************************************************
    //        int age = 0;
    //        ////////if (ValidateMandatory() == false)
    //        ////////{
    //        ////////    lblMessage.Text = "Enter the Required Fields";
    //        ////////    return;
    //        ////////}
    //        ////////else
    //        ////////{
    //        if (CheckAge() == false)
    //        {
    //            lblMessage.Text = "Enter valid age";
    //            return;
    //        }
    //        try
    //        {
    //            int industryid = 0, orgid = 0;
    //            string qualification = ""; string organizationname = ""; string industryname = "";
    //            //if (Session["UserID"] != null)
    //            //    userid = int.Parse(Session["UserID"].ToString());
    //            //string qualification = ""; string organizationname = ""; string industryname = "";
    //            //if (ddlQualification.SelectedValue == "00" || ddlQualification.SelectedValue == "0")
    //            //{
    //            //    qualification = txtEduQual.Text;
    //            //    if (qualification != "")
    //            //        dataclass.AddQualification(0, txtEduQual.Text, 1, userid);
    //            //}
    //            //else 
    //            qualification = ddlQualification.SelectedItem.Text;

    //            if (ddlOrg.SelectedValue == "00" || ddlOrg.SelectedValue == "0")
    //            {
    //                //organizationname = txtOrganization.Text;
    //                //if (organizationname != "")
    //                //{
    //                //    dataclass.AddOrganization(0, organizationname, 1, userid, "", 1, "", "");
    //                //    var getorgId = from orgdet in dataclass.Organizations
    //                //                   where orgdet.Name == organizationname
    //                //                   select orgdet;
    //                //    if (getorgId.Count() > 0)
    //                //        if (getorgId.First().OrganizationID != null)
    //                //            orgid = int.Parse(getorgId.First().OrganizationID.ToString());
    //                //}
    //            }
    //            else
    //                if (ddlOrg.SelectedIndex > 0)
    //                    orgid = int.Parse(ddlOrg.SelectedValue);

    //            if (ddlIndustry.SelectedValue == "00" || ddlIndustry.SelectedValue == "0")
    //            {
    //                industryname = ddlIndustry.SelectedItem.Text;
    //                //if (industryname != "")
    //                //{
    //                //    dataclasses.AddIndustry(0, industryname, 1, userid);

    //                //    var getindId = from inddet in dataclass.Industries
    //                //                   where inddet.Name == industryname
    //                //                   select inddet;
    //                //    if (getindId.Count() > 0)
    //                //        if (getindId.First().IndustryID != null)
    //                //            industryid = int.Parse(getindId.First().IndustryID.ToString());

    //                //}
    //            }
    //            else
    //                if (ddlIndustry.SelectedIndex > 0)
    //                    industryid = int.Parse(ddlIndustry.SelectedValue);

    //            if (txtAge.Text != null)
    //                age = int.Parse(txtAge.Text);
    //            //dataclasses.AddUserCreation(userCode, txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue), int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, emailid, testid, 1, testid1);
    //           //// dataclasses.AddUser(0, txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue), int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, emailid, testid, 1, testid1, txtFsName.Text, txtMidName.Text, txtLstName.Text, ddlGender.SelectedValue, age, industryid, int.Parse(ddlJobCatgy.SelectedValue), txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), int.Parse(ddlTotExpMonths.SelectedValue), int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue), qualification, txtProffQual.Text, txtPhoneNumber.Text);
    //            //dataclasses.AddUserProfile(userid, txtFsName.Text, txtMidName.Text, txtLstName.Text,
    //            //    ddlGender.SelectedValue, age, orgid, industryid, int.Parse(ddlJobCatgy.SelectedValue), int.Parse(ddlGrpUser.SelectedValue),
    //            //    txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), int.Parse(ddlTotExpMonths.SelectedValue), int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue), qualification, txtProffQual.Text, 1, userid, txtEmailId.Text, txtPhoneNumber.Text);

    //            //dataclass.AddUserFirstLoginDate(userid);

    //            //ClearControls();
    //            lblMessage.Text = "Profile Details are Saved Successfully";

    //            //string usercode = Session["UserCode"].ToString();
    //            string curcontrol = "TestIntroductionControl.ascx";// "UserTrainingControl.ascx";// "UserTrainingIntroduction.ascx";//"UserTrainingControl.ascx";

    //            //if (Session["evalResult"] == null)
    //            //{
    //            //    int Evalstatid = 0;
    //            //    if (Session["EvalStatId"] != null)
    //            //        Evalstatid = int.Parse(Session["EvalStatId"].ToString());
    //            //    dataclasses.ProcedureEvaluationStatus(Evalstatid, curcontrol, 0, 0, usercode, userid);

    //            //    if (Evalstatid == 0)
    //            //    {
    //            //        var EvaluationDetails = from EvalDet in dataclass.EvaluationStatus
    //            //                                where EvalDet.UserId == userid
    //            //                                select EvalDet;
    //            //        if (EvaluationDetails.Count() > 0)
    //            //        {
    //            //            if (EvaluationDetails.First().EvalStatusId != null)
    //            //                Session["EvalStatId"] = EvaluationDetails.First().EvalStatusId.ToString();
    //            //        }
    //            //    }
    //            //}
    //            //////
    //            //Session["NewIndustry"] = null; Session["NewOrg"] = null; Session["GrpExists"] = null;
    //            //////

    //            Session["SubCtrl"] = curcontrol;
    //            Response.Redirect("FJAHome.aspx");
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMessage.Text = ex.Message;
    //            return;
    //        }
    //        // }
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtUserName.Text.Trim() == "" || ddlUserType.SelectedIndex <= 0 || txtPassword.Text.Trim() == "")// || ddlOrg.SelectedIndex < 0 || ddlUserGroup.SelectedIndex < 0)
        //{ lblMessage.Text = "Enter the values"; }
        //else
        //{
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
            int testid = 0;
            int testid1 = 0;
            if (ddlTestLists.SelectedIndex > 0)
                testid = int.Parse(ddlTestLists.SelectedValue);
            if (ddlTestlIst2.SelectedIndex > 0)
                testid1 = int.Parse(ddlTestlIst2.SelectedValue);
            int status = int.Parse(ddlStatus.SelectedValue);
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
            //dataclasses.AddUser(txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue),
            //    int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, txtEmailId.Text , int.Parse(ddlTestLists.SelectedValue) ,
            //    1, int.Parse(ddlTestlIst2.SelectedValue), txtFsName.Text,
            //    txtMidName.Text, txtLstName.Text, ddlGender.SelectedValue, age, industryid, int.Parse(ddlJobCatgy.SelectedValue),
            //    txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), int.Parse(ddlTotExpMonths.SelectedValue),
            //    int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue), ddlQualification.SelectedItem.Text  ,
            //    txtProffQual.Text, txtPhoneNumber.Text);
            //////dataclasses.AddUser(txtUserName.Text, txtPassword.Text, ddlUserType.SelectedValue, int.Parse(ddlOrg.SelectedValue),
            //////    int.Parse(ddlUserGroup.SelectedValue), dtFrom, dtTo, status, 0, emailid, testid, 1, testid1, txtFsName.Text,
            //////    txtMidName.Text, txtLstName.Text, ddlGender.SelectedValue, age, industryid, int.Parse(ddlJobCatgy.SelectedValue),
            //////    txtJob.Text, int.Parse(ddlTotExpYears.SelectedValue), int.Parse(ddlTotExpMonths.SelectedValue),
            //////    int.Parse(ddlCurExpYears.SelectedValue), int.Parse(ddlCurExpMonths.SelectedValue), qualification,
            //////    txtProffQual.Text, txtPhoneNumber.Text,txtrecrutr.Text );
            Session["UserTestId"] = testid;
            Session["UserTestId1"] = testid1;
            
            //lblMessage.Text = "Profile Details are Saved Successfully";
            //Redirect to take a test page
            Session["SubCtrl"] = "UserTrainingControl.ascx";
            Response.Redirect("FJAHome.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            return;
        }
    }
     
   
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
}