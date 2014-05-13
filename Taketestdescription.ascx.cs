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
using System.IO;
using System.Drawing;

public partial class Taketestdescription : System.Web.UI.UserControl
{
    DBManagementClass clsClass = new DBManagementClass();
    AssesmentDataClassesDataContext dataclasses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillDetails();
    }
    private void FillDetails()
    {
        var Details = from det in dataclasses.TakeTestDyanamicContents 
                      select det;
        //int intcount = Details.Count();
        if (Details.Count() > 0)
        {
            if (Details.First().Description1 != null && Details.First().Description1 != "")
                lit_righttop.Text  = Details.First().Description1.ToString();

            if (Details.First().Description2 != null && Details.First().Description2 != "")
                lit_innerhead.Text = Details.First().Description2.ToString();

            if (Details.First().Description3 != null && Details.First().Description3 != "")
                lit_innercontent.Text = Details.First().Description3.ToString();

            if (Details.First().Description4 != null && Details.First().Description4 != "")
                lit_inner1.Text = Details.First().Description4.ToString();
        }
        

    }
    protected void lnk_register_Click(object sender, EventArgs e)
    {
        Session["ControlToReDirect"] = "UserCreationFromSite.ascx";
        Response.Redirect("FJAHome.aspx");        
    }
}