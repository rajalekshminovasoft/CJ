using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }


    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {

    }
    protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
    {

    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {

    }
    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (Wizard1.ActiveStepIndex == 0)
        {
            Label1.Text=Wizard1.ActiveStepIndex.ToString();
            Session["TestID"] = "";
            foreach (DataListItem dli in dtTestlist.Items)
            {
                CheckBox productID = ((CheckBox)dli.FindControl("chktest"));
                if (productID.Checked==true )
                {
                    Session["TestID"] = ((Label)dli.FindControl("lbltestid")).Text;
                    Session["TestIDList"] = Session["TestIDList"] + "," + Session["TestID"];
                }
            }
            //Label1.Text = Session["TestID"].ToString();
        }
        //if (Wizard1.ActiveStepIndex > 0)
        //{
        //    if (Wizard1.ActiveStepIndex == 0)
        //    { 
        //        Wizard1.WizardSteps(Wizard1.ActiveStep
        //    }
        //}


    }
    protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

    }
}