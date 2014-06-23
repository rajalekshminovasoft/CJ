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
                if (Session["TestIDList"]!=null )
                {
                    tests = Convert.ToString(Session["TestIDList"]);
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
        try
        {
            string PaymentOption = "";
            string creditCardType = "";
            string creditCardNumber = "";
            DateTime expDate;
            string cvv2="";
            string firstName="";
            string lastName = "";
            string street = "";
            string city = "";
            string state = "";
            string zip = "";
            string countryCode = "";
            string currencyCode = "";
            string orderDescription = "";
            string paymentType = "";
            string paymentAmount = "0";
            PaymentOption = ddlpaymenttype.SelectedItem.Text;
            if (PaymentOption == "Visa" || PaymentOption == "MasterCard" || PaymentOption == "Amex" || PaymentOption == "Discover")
            {
                NVPAPICaller test = new NVPAPICaller();

                creditCardType =ddlpaymenttype.SelectedItem.Text; // "<<Visa/MasterCard/Amex/Discover>>"; // Set this to one of the acceptable values (Visa/MasterCard/Amex/Discover) match it to what was selected on your Billing page
                creditCardNumber = txtcreditcardnumber.Text; // "<<CC number>>"; //  Set this to the string entered as the credit card number on the Billing page
                expDate =Convert.ToDateTime(txtexpdate.Text); // "<<Expiry Date>>"; //  Set this to the credit card expiry date entered on the Billing page
                cvv2 = txtcvv2.Text;// "<<cvv2>>"; //  Set this to the CVV2 string entered on the Billing page 
                firstName = txtfirstname.Text;//  "<<firstName>>"; //  Set this to the customer's first name that was entered on the Billing page 
                lastName =txtlastname.Text; // "<<lastName>>"; //  Set this to the customer's last name that was entered on the Billing page 
                street =txtstreet.Text; // "<<street>>"; //  Set this to the customer's street address that was entered on the Billing page 
                city =txtcity.Text;// "<<city>>"; //  Set this to the customer's city that was entered on the Billing page 
                state = txtstate.Text;// "<<state>>"; //  Set this to the customer's state that was entered on the Billing page 
                zip = txtzip.Text;// "<<zip>>"; //  Set this to the zip code of the customer's address that was entered on the Billing page 
                countryCode = txtcountry.Text;// "<<PayPal Country Code>>"; //  Set this to the PayPal code for the Country of the customer's address that was entered on the Billing page 
                currencyCode = txtcurrency.Text;// "<<PayPal Currency Code>>"; //  Set this to the PayPal code for the Currency used by the customer
                orderDescription = txtdescripion.Text;// "<<OrderDescription>>"; //  Set this to the textual description of this order 
                string strexpdate = expDate.ToString();

                paymentType = "Sale";
                string retMsg = "";
                string finalPaymentAmount = "";
                //NVPCodec decoder;
                NVPCodec decoder = new NVPCodec() ;
                //finalPaymentAmount = Session["payment_amt"].ToString();
                finalPaymentAmount = "10";
                bool ret = test.DirectPayment(paymentType, paymentAmount, creditCardType, creditCardNumber, strexpdate, cvv2, firstName, lastName, street, city, state, zip, countryCode, currencyCode, orderDescription,ref decoder, ref retMsg);
                //bool ret = test.DirectPayment(paymentType, paymentAmount, creditCardType, creditCardNumber, sexpdate, cvv2, firstName, lastName, street, city, state, zip, countryCode, currencyCode, orderDescription, ref retMsg);
                if (ret)
                {
                    // success
                    retMsg = "Success";
                    Response.Redirect("APIError.aspx?" + retMsg);
                }
                else
                {
                    Response.Redirect("APIError.aspx?" + retMsg);
                }
            }

        }
        catch (Exception ex)
            {
            }
    }
    protected void btntaketest_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Session["TestIDList"] != null)
            //{

            //}
            Session["UserTestId"] = Session["UserTestId"];
            Session["SubCtrl"] = "UserTrainingControl.ascx";
            Response.Redirect("FJAHome.aspx");
        }
        catch (Exception ex)
        {
        }
    }
}
