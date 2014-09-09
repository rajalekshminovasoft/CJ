﻿using System;
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

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
//using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;

public partial class ReportCITATQ : System.Web.UI.UserControl
{
    DBManagementClass clsClasses = new DBManagementClass();
    AssesmentDataClassesDataContext dataclass = new AssesmentDataClassesDataContext();
    DataTable dt = new DataTable();
    int userid = 0; int testid = 0;
    DataTable dtEmptySessionList = new DataTable();
    string scoringType; string scoretype;
    int totalQuestionMark = 0;

    DataTable dtTestSection;
    Rectangle objRectangle_3 = new Rectangle(250, 200, 50, 25);
    int mark1 = 40, mark2 = 60, mark3 = 80;
    protected void Page_Load(object sender, EventArgs e)
    {
        userid = int.Parse(Session["UserId_Report"].ToString());
        testid = int.Parse(Session["UserTestID_Report"].ToString());
        scoretype = Session["ScoringType"].ToString();
        bool valexists = false;
        if (Session["TestName"] != null)
        {
            lblTestName.Text = Session["TestName"].ToString();
            if (Session["ReportTypeName"] != null)
            { lblReportType.Text = "CIA profile Report"; valexists = true; }
        }
        if (valexists == false)
        {
            var GetTestName = from testnamedet in dataclass.TestLists
                              where testnamedet.TestId == testid
                              select testnamedet;
            if (GetTestName.Count() > 0)
            {
                if (GetTestName.First().TestName != null)
                {
                    lblTestName.Text = GetTestName.First().TestName.ToString();
                    Session["TestName"] = GetTestName.First().TestName.ToString();
                }
                if (GetTestName.First().ReportType != null)
                {
                    lblReportType.Text = "CITAT-Q profile Report";
                    Session["ReportTypeName"] = GetTestName.First().ReportType.ToString();
                }
            }
        }
        if (Session["usertype"].ToString() == "OrgAdmin" || Session["usertype"].ToString() == "GrpAdmin" || Session["usertype"].ToString() == "SuperAdmin" || Session["usertype"].ToString() == "User")
        { lbtnBack.Visible = true; lbtnBack0.Visible = true; }
        GetReportGraphDetailsFromDB();
        goToPrintPage();//bip 17062010
    }
    private void goToPrintPage()
    {
        Session["totalparts"] = txtParts.Text.Trim();
        Session["displayvalues"] = txtValues.Text.Trim();
        Session["testsectionreportvalues"] = dtTestSection;
        DataTable dtvariablevalues = new DataTable();
        DataRow drvariablelist;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows[0].Cells.Count; i++)
            {
                dtvariablevalues.Columns.Add("column" + (i + 1).ToString());
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                drvariablelist = dtvariablevalues.NewRow();
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    if (GridView1.Rows[i].Cells[j].Text != "&nbsp;")
                        drvariablelist["column" + (j + 1).ToString()] = GridView1.Rows[i].Cells[j].Text;
                }
                dtvariablevalues.Rows.Add(drvariablelist);
            }

            Session["variablereportvalues"] = dtvariablevalues;


            /// this part is ment for browser back button
            if (Session["usertype"].ToString() == "OrgAdmin")
                Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx";
            else if (Session["usertype"].ToString() == "GrpAdmin")
                Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx";
            else if (Session["usertype"].ToString() == "SuperAdmin")
                Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx";
            else
                Session["SubCtrl"] = "TestListControl.ascx";
            ///


            //if (Session["usertype"].ToString() == "OrgAdmin")
            //{ Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }

            //else if (Session["usertype"].ToString() == "GrpAdmin")
            //{ Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
            //else if (Session["usertype"].ToString() == "SuperAdmin")
            //{ Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
            //else
            //{ Session["SubCtrl"] = "TestListControl.ascx"; Response.Redirect("FJAHome.aspx"); }
            Session["SubCtrl"] = "TestListControl.ascx";
            Response.Write("<script>");
            Response.Write("window.open('FJAHome.aspx','_blank')");
            Response.Write("</script>");
            //Response.Redirect("FJAHome.aspx");
            Response.Write("<script>");
            Response.Write("window.open('ReportPrintNewCITAT.aspx','_blank')");
            Response.Write("</script>");
            Response.Write("<script language='javascript'>window.open('','_self');window.close();</script>");
        }
        else
        {
            //lblMessage.Text = "No Values for Print";

            if (Session["usertype"].ToString() == "OrgAdmin")
            { Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }

            else if (Session["usertype"].ToString() == "GrpAdmin")
            { Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
            else if (Session["usertype"].ToString() == "SuperAdmin")
            { Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
            else
            { Session["SubCtrl"] = "TestListControl.ascx"; Response.Redirect("FJAHome.aspx"); }
        }
    }


    //

    int Width = 300, Height = 300;
    private void SetScale(Graphics g)
    {
        g.TranslateTransform(Width / 2, Height / 2);

        float inches = Math.Min(Width / g.DpiX, Height / g.DpiX);

        g.ScaleTransform(inches * g.DpiX / 2000, inches * g.DpiY / 2000);
    }
    private void DrawFace(Graphics g)
    {
        Brush brush = new SolidBrush(Color.Black);
        Font font = new Font("Arial", 80);

        float x, y;

        const int numHours = 4;
        const int deg = 360 / numHours;
        const int FaceRadius = 450;

        for (int i = 1; i <= numHours; i++)
        {
            SizeF stringSize =
              g.MeasureString(i.ToString(), font);

            //x = GetCos(1 * deg + 90) * FaceRadius;
            //x += stringSize.Width / 2;
            //y = GetSin(1 * deg + 90) * FaceRadius;
            //y += stringSize.Height / 2;
            //if (i == 1)
            //{
            x = GetCos(i * deg + 150) * FaceRadius;
            x += stringSize.Width / 2;
            y = GetSin(i * deg + 150) * FaceRadius;
            y += stringSize.Height / 2;
            //}
            //else
            //{
            //    x = GetCos(i * deg + 90) * FaceRadius;
            //    x += stringSize.Width / 2;
            //    y = GetSin(i * deg + 90) * FaceRadius;
            //    y += stringSize.Height / 2;
            //}

            g.DrawString(i.ToString() + " SS ", font, brush, -x, -y);

        }
        brush.Dispose();
        font.Dispose();
    }
    private static float GetSin(float degAngle)
    {
        return (float)Math.Sin(Math.PI * degAngle / 180f);
    }
    private static float GetCos(float degAngle)
    {
        return (float)Math.Cos(Math.PI * degAngle / 180f);
    }

    //    

    private void FillUserReportDetails()
    {
        Table tblDisplay = new Table();
        TableCell tblCell = new TableCell();
        TableRow tblRow;
        Label label;
        tblDisplay = new Table();
        //return;

        //string connectionstring = ConfigurationManager.ConnectionStrings["talentscoutConnectionString"].ConnectionString;
        //conn = new SqlConnection(connectionstring);
        //conn.Open();
        string querystring = "SELECT Name, OrganizationName,JobCategory,GroupName,Designation FROM View_UserProfileDetails WHERE UserID =  " + userid;
        //      string querystring = "SELECT Name, Organization,JobCategory,GroupName,Designation FROM UserProfileView WHERE UserID =  " + userid;

        //cmd = new SqlCommand(querystring, conn);
        //da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        //da.Fill(ds);
        ds = clsClasses.GetValuesFromDB(querystring);
        tblDisplay.CellSpacing = 0;
        tblDisplay.CellPadding = 2;
        tblDisplay.BorderWidth = 2;
        //tblDisplay.Width = 400;
        tblDisplay.BorderColor = Color.Orange;
        Color clrTitleRow = ColorTranslator.FromHtml("#4F81BC");
        Color clrRow1 = ColorTranslator.FromHtml("#9FBCE6");
        Color clrRow2 = ColorTranslator.FromHtml("#C9DCFD");
        Color clrTextcolor = ColorTranslator.FromHtml("#56182F");

        if (ds != null)
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    //tblCell.Width = 200;
                    tblCell.ColumnSpan = 2;
                    tblCell.BackColor = clrTitleRow; //Color.DarkBlue; //Color.LightBlue;

                    label = new Label(); label.ForeColor = Color.White;
                    label.Text = "Demographic Profile";
                    label.Font.Size = 14;

                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblCell.Style.Value = "text-align: center; vertical-align: middle";
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    int tableftcellwidth = 100;
                    int tabrightcellwidth = 350;

                    //for (int count = 0; count < 5; count++)
                    //{
                    //    switch (count)
                    //    {
                    //        case 0:
                    //            {

                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2; //tblCell.Style.Add("padding-left:", "20px");
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Name";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Width = tabrightcellwidth;
                    tblCell.BorderWidth = 2;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Name"].ToString();
                    lblName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 1:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow2;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Organization";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["OrganizationName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 2:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; JobCategory";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["JobCategory"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 3:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow2;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; Designation";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["Designation"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //        break;
                    //    }
                    //case 4:
                    //    {
                    tblRow = new TableRow(); tblRow.BackColor = clrRow1;
                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tableftcellwidth;
                    label = new Label();
                    label.Text = "&nbsp;&nbsp; GroupName";
                    label.Font.Bold = true;
                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.BorderWidth = 2;
                    tblCell.Width = tabrightcellwidth;
                    label = new Label(); label.Font.Bold = true; label.ForeColor = clrTextcolor;
                    label.Text = "&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["GroupName"].ToString();

                    tblCell.Controls.Add(label);
                    tblRow.Cells.Add(tblCell);
                    tblDisplay.Rows.Add(tblRow);

                    //            break;
                    //        }
                }

        tcellUserDetails.Controls.Clear();
        tcellUserDetails.Controls.Add(tblDisplay);
        // }
        //}
    }

    private void FillReportDescriptionDetails()
    {
        lblReportDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");//.ToShortDateString();

        var SummaryDetails1 = from SummaryDetails in dataclass.ReportDescriptions
                              where SummaryDetails.TestId == testid
                              select SummaryDetails;
        if (SummaryDetails1.Count() > 0)
        {
            if (SummaryDetails1.First().Summary1 != null)
                lblSummary1.Text = SummaryDetails1.First().Summary1.ToString();
            if (SummaryDetails1.First().Summary2 != null)
                lblSummary2.Text = SummaryDetails1.First().Summary2.ToString();
            if (SummaryDetails1.First().DescriptiveReport != null)
                tcellDescriptionReport.InnerHtml = SummaryDetails1.First().DescriptiveReport.ToString();
            if (SummaryDetails1.First().Conclusion != null)
                lblConclusion.Text = SummaryDetails1.First().Conclusion.ToString();
            if (SummaryDetails1.First().ScoringType != null)
                scoringType = SummaryDetails1.First().ScoringType.ToString();
        }

    }


    private void GetReportGraphDetailsFromDB()
    {
        try
        {
            dtTestSection = new DataTable();
            dtTestSection.Columns.Add("TestSectionID");
            dtTestSection.Columns.Add("TotalMark_sec");
            DataRow drTestSection;

            DataRow dr;
            dt.Columns.Add("SectionID");
            dt.Columns.Add("SectionName");
            dt.Columns.Add("TestSectionId");
            dt.Columns.Add("TestID");
            dt.Columns.Add("TotalMarks");
            dt.Columns.Add("BandDescription");

            DataRow drEmptySession;
            dtEmptySessionList.Columns.Add("SectionName");
            dtEmptySessionList.Columns.Add("BandDescription");
            dtEmptySessionList.Columns.Add("TestSectionId");
            //DataRow dr1;
            //dt1.Columns.Add("BenchMark");
            //dt1.Columns.Add("TotalMark");
            //dt1.Columns.Add("TestID");

            //int userid = int.Parse(Session["UserId_Report"].ToString());
            int MemmoryTestImage = 0;
            int MemmoryTestText = 0;
            int QuesCollection = 0;
            string sectionname = ""; int TestSecionID = 0;
            //SqlConnection conn;
            //SqlCommand cmd;
            //SqlDataAdapter da;
            DataSet ds;
            Table tblDisplay = new Table();
            TableCell tblCell = new TableCell();
            TableRow tblRow;
            Label label;
            //int i = 0;
            int rowid = 0;
            int totalmarks = 0;
            int sectionid = 0;
            int testid = int.Parse(Session["UserTestID_Report"].ToString());


            string quesrystring = "SELECT DISTINCT EvaluationResult.QuestionID, TestBaseQuestionList.TestId, TestBaseQuestionList.TestSectionId,TestBaseQuestionList.Status, EvaluationResult.Question, " +
                      " EvaluationResult.Answer, EvaluationResult.UserId, EvaluationResult.Category, TestBaseQuestionList.SectionId, " +
                      " TestBaseQuestionList.FirstVariableId, TestBaseQuestionList.SecondVariableId, TestBaseQuestionList.ThirdVariableId FROM  EvaluationResult INNER JOIN TestBaseQuestionList ON EvaluationResult.QuestionID = TestBaseQuestionList.QuestionId " +
                      " WHERE     (TestBaseQuestionList.Status = 1) and EvaluationResult.UserId=" + userid + " and TestBaseQuestionList.TestId=" + testid + " order by TestBaseQuestionList.TestSectionId ";

            DataSet dsEvaluationdetails = new DataSet();
            dsEvaluationdetails = clsClasses.GetValuesFromDB(quesrystring);
            if (dsEvaluationdetails != null)
                if (dsEvaluationdetails.Tables.Count > 0)
                    if (dsEvaluationdetails.Tables[0].Rows.Count > 0)
                    {
                        //lblMessage.Text += " after db access..."; return;
                        //var UserAnsws1 = from UserAnsws in dataclass.EvaluationResults
                        //                 where UserAnsws.UserId == userid   // && UserAnsws.TestId == testid
                        //                 select UserAnsws;

                        //if (UserAnsws1.Count() > 0)
                        //{
                        // foreach (var UserAnswers in UserAnsws1)

                        for (int i = 0; i < dsEvaluationdetails.Tables[0].Rows.Count; i++)
                        {
                            int marks = 0;
                            int currentmarks = 0;
                            sectionid = 0;
                            //if (UserAnswers.Category == "MemTestWords")
                            if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestWords")
                            {
                                var MemWords1 = from MemWords in dataclass.MemmoryTestTextQuesCollections
                                                where MemWords.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemWords.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString()) //UserAnswers.Question && MemWords.QuestionID == UserAnswers.QuestionID
                                                select MemWords;
                                if (MemWords1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemWords1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemWords1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemWords1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "MemTestImages")
                            {
                                var MemImages1 = from MemImages in dataclass.MemmoryTestImageQuesCollections
                                                 where MemImages.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && MemImages.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && MemImages.QuestionID == UserAnswers.QuestionID
                                                 select MemImages;
                                if (MemImages1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = MemImages1.First().SectionName.ToString();
                                    sectionid = int.Parse(MemImages1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (MemImages1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "FillBlanks")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());

                                    //if (FillQues1.First().Option1 == UserAnswers.Answer || FillQues1.First().Option2 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option3 == UserAnswers.Answer || FillQues1.First().Option4 == UserAnswers.Answer ||
                                    //    FillQues1.First().Option5 == UserAnswers.Answer)
                                    if (sectionid > 0)// bip 05122009
                                        if (FillQues1.First().Option1 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option2 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option3 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() || FillQues1.First().Option4 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() ||
                                             FillQues1.First().Option5 == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())
                                            marks = 1;
                                }
                            }
                            else if (dsEvaluationdetails.Tables[0].Rows[i]["Category"].ToString() == "RatingType")
                            {
                                var FillQues1 = from FillQues in dataclass.QuestionCollections
                                                where FillQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && FillQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && FillQues.QuestionID == UserAnswers.QuestionID
                                                select FillQues;
                                if (FillQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString());// int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = FillQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(FillQues1.First().SectionId.ToString());
                                    ////if (sectionname == "Test2")
                                    ////    sectionid = 40;
                                    //if (FillQues1.First().Answer == UserAnswers.Answer)
                                    //{

                                    // if (sectionid > 0)// bip 05122009
                                    //     marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());// int.Parse(UserAnswers.Answer.ToString());

                                    if (sectionid > 0)// bip 05062011 "-" is added if the user leave the anwser while taking the test
                                        if (dsEvaluationdetails.Tables[0].Rows[i]["Answer"] != null)
                                            if (dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString() != "-")
                                                marks = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString());

                                    //if (sectionname == "Clarity of Communication")
                                    //    lblMessage.Text += " " + sectionname + " mark " + marks.ToString(); // 021209 bip
                                    //// marks = ratingmark;
                                    // dataclass.ProcSectionMarks(userid, testid, sectionid, sectionname, marks);
                                    ////}
                                    //}
                                }
                            }
                            else
                            {
                                var OtherQues1 = from OtherQues in dataclass.QuestionCollections
                                                 where OtherQues.Question == dsEvaluationdetails.Tables[0].Rows[i]["Question"].ToString() && OtherQues.QuestionID == int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["QuestionID"].ToString())//UserAnswers.Question && OtherQues.QuestionID == UserAnswers.QuestionID
                                                 select OtherQues;
                                if (OtherQues1.Count() > 0)
                                {
                                    if (dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString() != "")
                                        TestSecionID = int.Parse(dsEvaluationdetails.Tables[0].Rows[i]["TestSectionId"].ToString()); //int.Parse(UserAnswers.TestSectionId.ToString());
                                    sectionname = OtherQues1.First().SectionName.ToString();
                                    sectionid = int.Parse(OtherQues1.First().SectionId.ToString());
                                    if (sectionid > 0)// bip 05122009
                                        if (OtherQues1.First().Answer == dsEvaluationdetails.Tables[0].Rows[i]["Answer"].ToString())// UserAnswers.Answer)
                                            marks = 1;
                                }
                            }
                            //if (sectionname == "Clarity of Communication")
                            if (marks >= 0)
                            {
                                bool testsecExists = false; int j = 0;
                                if (dtTestSection.Rows.Count > 0)
                                {
                                    for (int n = 0; n < dtTestSection.Rows.Count; n++)
                                    {
                                        if (dtTestSection.Rows[n]["TestSectionID"].ToString() == TestSecionID.ToString())
                                        { testsecExists = true; j = n; break; }
                                    }
                                }
                                if (testsecExists == true)
                                {
                                    int currentmarks_testsecid = 0;
                                    drTestSection = dtTestSection.Rows[j];
                                    currentmarks_testsecid = int.Parse(drTestSection["TotalMark_sec"].ToString());
                                    currentmarks_testsecid = currentmarks_testsecid + marks;
                                    drTestSection["TotalMark_sec"] = currentmarks_testsecid.ToString();
                                }
                                else
                                {
                                    drTestSection = dtTestSection.NewRow();
                                    drTestSection["TestSectionID"] = TestSecionID.ToString();
                                    drTestSection["TotalMark_sec"] = marks.ToString();
                                    dtTestSection.Rows.Add(drTestSection);
                                }

                                Boolean exists = CheckExistence(sectionname, TestSecionID);
                                if (exists == true)
                                {
                                    rowid = int.Parse(Session["RowID"].ToString());
                                    dr = dt.Rows[rowid];
                                    currentmarks = int.Parse(dr["TotalMarks"].ToString());
                                    currentmarks = currentmarks + marks;
                                    dr["TotalMarks"] = currentmarks;
                                    marks = 0; Session["RowID"] = null;
                                    // lblMessage.Text += " || " + sectionname + " totmarknew= " + currentmarks + " || ";
                                }
                                else
                                {
                                    //if (currentmarks > 0)
                                    //    marks = currentmarks;

                                    dr = dt.NewRow();
                                    //dr["SectionID"] = sectionid;
                                    dr["SectionName"] = sectionname;
                                    dr["TestID"] = testid;
                                    dr["TestSectionId"] = TestSecionID;
                                    if (exists == false)
                                        dr["TotalMarks"] = marks;
                                    else
                                        dr["TotalMarks"] = "0";
                                    if (sectionname == "Investigative-Conventional")
                                    {
                                        dr["BandDescription"] = "C";
                                    }
                                    else if (sectionname == "Numerical-Verbal")
                                    {
                                        dr["BandDescription"] = "N";
                                    }
                                    else if (sectionname == "Serial-Integrative")
                                    {
                                        dr["BandDescription"] = "R";
                                    }
                                    else if (sectionname == "Social-Independent")
                                    {
                                        dr["BandDescription"] = "H";
                                    }
                                    else if (sectionname == "Enterprising-Aesthetic")
                                    {
                                        dr["BandDescription"] = "B";
                                    }
                                    else if (sectionname == "Physical-Mental")
                                    {
                                        dr["BandDescription"] = "M";
                                    }
                                    else
                                    {
                                        dr["BandDescription"] = "";
                                    }

                                    dt.Rows.Add(dr);
                                    marks = 0;
                                    // lblMessage.Text += " sec..." + sectionname + " ";
                                }
                            }
                            else
                            {

                                bool sesExists = false;
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        if (dtEmptySessionList.Rows[e][0].ToString() == sectionname && dtEmptySessionList.Rows[e][2].ToString() == TestSecionID.ToString())
                                            sesExists = true;
                                    }
                                }
                                if (sesExists == false)
                                {
                                    drEmptySession = dtEmptySessionList.NewRow();
                                    drEmptySession["SectionName"] = sectionname;

                                    string remarks = GetBandDescription(0, sectionname);
                                    drEmptySession["BandDescription"] = remarks;
                                    drEmptySession["TestSectionId"] = TestSecionID;
                                    dtEmptySessionList.Rows.Add(drEmptySession);
                                }
                                if (dtEmptySessionList.Rows.Count > 0)
                                {
                                    for (int e = 0; e < dtEmptySessionList.Rows.Count; e++)
                                    {
                                        Boolean exists = CheckExistence(dtEmptySessionList.Rows[e][0].ToString(), TestSecionID);
                                        if (exists == true)
                                        {
                                            dtEmptySessionList.Rows[e].Delete();
                                        }
                                    }
                                }
                            }//
                        }
                    }



            GridView1.DataSource = dt;
            GridView1.DataBind(); //return;// bip 19122009
            // lblMessage.Text = scoretype; return;
            //if (Session["DiagramType"].ToString() == "PieGraph")
            //{
            if (GridView1.Rows.Count > 0)
            {


                if (scoretype == "Percentile")
                {
                    //lblMessage.Text += "hi..."; return;
                    int count = 0;
                    // lblMessage.Text += " rptgridParts= " + GridView1.Rows.Count.ToString() + " marks= ";
                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;
                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            float currentmark = 0;
                            currentmark = GetPercentileScoreUserwise(mark1, GridView1.Rows[k].Cells[1].Text, GridView1.Rows[k].Cells[2].Text);
                            currentmark = float.Parse(currentmark.ToString("0.00"));
                            GridView1.Rows[k].Cells[4].Text = currentmark.ToString();
                            count++;
                        }
                    }

                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = 0;
                            if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                                curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());

                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());
                            if (curmark > 0)
                            {
                                float mark1 = float.Parse(curmark.ToString());
                                float currentmark = 0;
                                currentmark = GetPercentileScoreUserwise_testsec(mark1, testsecid);
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                drTestSection = dtTestSection.Rows[k];
                                drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                            else
                            {
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + "0";
                                txtParts.Text = (k + 1).ToString();
                            }
                        }
                        /* bipson 20-02-2011 commented this code... no preview is need, allow admin for direct print....
                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg");
                        DrawBarGraph();
                         */
                        return;
                    }
                }

                else //(scoretype == "Percentage")
                {

                    for (int k = 0; k < GridView1.Rows.Count; k++)
                    {
                        int curmark = int.Parse(GridView1.Rows[k].Cells[4].Text);
                        string remarks = GetBandDescription(curmark, GridView1.Rows[k].Cells[1].Text);
                        GridView1.Rows[k].Cells[5].Text = remarks;

                        if (GridView1.Rows[k].Cells[4].Text != "0" && GridView1.Rows[k].Cells[4].Text != "&nbsp;")
                        {
                            float mark1 = float.Parse(GridView1.Rows[k].Cells[4].Text);
                            //mark1 = ((mark1 * 100) / 360);
                            float totalMarksectionwise = 0, currentmark = 0;
                            totalMarksectionwise = GetSectionwiseTotalQuestionMarks(GridView1.Rows[k].Cells[1].Text, GridView1.Rows[k].Cells[2].Text, 0);
                            if (totalMarksectionwise > 0)
                            {
                                //mark1 = (mark1 / totalMarksectionwise) * 100;
                                //lblMessage.Text += " tot_F=" + totalMarksectionwise + " &,& " + currentmark.ToString();
                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text +" tot=" + totalMarksectionwise ;//+ " &,& " 
                                currentmark = (mark1 / totalMarksectionwise) * 100;
                                // lblMessage.Text += " mark=" + mark1.ToString() + "curMark &,& " + currentmark.ToString();
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                GridView1.Rows[k].Cells[4].Text = currentmark.ToString();

                                // lblMessage.Text += GridView1.Rows[k].Cells[1].Text + " currentmark &&& " + currentmark.ToString();
                            }
                        }
                    }
                    if (Session["ReportType"].ToString() == "TestSectionwise")
                    {
                        //testsection mark details

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            int curmark = int.Parse(dtTestSection.Rows[k]["TotalMark_sec"].ToString());
                            int testsecid = int.Parse(dtTestSection.Rows[k]["TestSectionID"].ToString());

                            float mark1 = float.Parse(curmark.ToString());
                            //mark1 = ((mark1 * 100) / 360);
                            float totMarktestsecwise = 0, currentmark = 0;
                            totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid.ToString(), 1);

                            if (totMarktestsecwise > 0)
                            {
                                //float mark1 = float.Parse(curmark.ToString());
                                ////mark1 = ((mark1 * 100) / 360);
                                //float totMarktestsecwise = 0, currentmark = 0;
                                //totMarktestsecwise = GetSectionwiseTotalQuestionMarks("", testsecid.ToString(), 1);
                                ////mark1 = (mark1 / totalMarksectionwise) * 100;
                                // if (totMarktestsecwise > 0 && mark1 > 0)
                                currentmark = (mark1 / totMarktestsecwise) * 100;
                                //else currentmark = 0;
                                currentmark = float.Parse(currentmark.ToString("0.00"));
                                //drTestSection = dtTestSection.Rows[k];
                                //drTestSection["TotalMark_sec"] = currentmark.ToString();
                            }
                            else
                            {
                                currentmark = 0;
                            }
                            drTestSection = dtTestSection.Rows[k];
                            drTestSection["TotalMark_sec"] = currentmark.ToString();
                        }

                        for (int k = 0; k < dtTestSection.Rows.Count; k++)
                        {
                            if (dtTestSection.Rows[k][1].ToString() != "" && dtTestSection.Rows[k][1].ToString() != "Infinity")
                            {
                                float curmark = float.Parse(dtTestSection.Rows[k][1].ToString());
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + curmark.ToString();//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                            else
                            {
                                if (txtValues.Text.Trim() != "")
                                    txtValues.Text = txtValues.Text.Trim() + ",";
                                txtValues.Text = txtValues.Text.Trim() + "0";//currentmark;// mark1;//
                                txtParts.Text = (k + 1).ToString();
                            }
                        }
                        /* bipson 20-02-2011 commented this code... no preview is need, allow admin for direct print....
                        DrawPieGraph_TestSecwise("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg");
                        DrawBarGraph();
                        */
                        return;
                    }
                }
                int totvalues = 0;
                for (int j = 0; j < GridView1.Rows.Count; j++)
                {
                    if (GridView1.Rows[j].Cells[1].Text != "&nbsp;")
                        if (GridView1.Rows[j].Cells[4].Text != "&nbsp;")
                        {
                            totvalues++;
                            if (txtValues.Text.Trim() != "")
                                txtValues.Text = txtValues.Text.Trim() + ",";
                            txtValues.Text = txtValues.Text.Trim() + GridView1.Rows[j].Cells[4].Text;//currentmark;// mark1;//
                            txtParts.Text = totvalues.ToString();
                        }
                }
            }
            /* bipson 20-02-2011 commented this code... no preview is need, allow admin for direct print....
            DrawPieGraph("imgReportGraph1_" + userid + "_" + DateTime.Now.Millisecond.ToString() + ".jpg", 1);
            DrawBarGraph();
             */

            //////////Modification for new report
           
             var CheckCIT = from CCIT in dataclass.CITATQValues
                            select CCIT;
             if (CheckCIT.Count() > 0)
             {

             }
            


        }
        catch (Exception ex) { lblMessage.Text += "hi..." + ex.Message; }

    }

    //

    private float GetPercentileScoreUserwise(float totalmark, string sectionname, string testsecId)
    {

        double score = 0;
        int XF = 1, CF = 0, totnum = 1;
        DataSet dsRefScoreDetails = new DataSet();
        string quesrystring = "select * from ScoreTable where TestId = " + testid + " and TestSectionId =" + testsecId + " and SectionName ='" + sectionname + "'";//like '%" + sectionname + "%'"; //
        //lblMessage.Text += " query --- = " + quesrystring;
        dsRefScoreDetails = clsClasses.GetValuesFromDB(quesrystring);
        if (dsRefScoreDetails != null)
            if (dsRefScoreDetails.Tables.Count > 0)
                if (dsRefScoreDetails.Tables[0].Rows.Count > 0)
                {
                    totnum = int.Parse(dsRefScoreDetails.Tables[0].Rows.Count.ToString());
                    //lblMessage.Text += " rowcount= " + totnum;
                    for (int i = 0; i < dsRefScoreDetails.Tables[0].Rows.Count; i++)
                    {
                        float mark = float.Parse(dsRefScoreDetails.Tables[0].Rows[i]["TotalScore"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                    double totsamescore = .5 * XF; totnum = totnum + 1;
                    //score = (CF + (.5 * XF) * 100) / totnum;
                    //score = ((CF + totsamescore) * 100) / totnum;
                    score = ((CF + totsamescore) / totnum) * 100;
                    // lblMessage.Text += " sectionname= " + sectionname + " XF=" + XF.ToString() + " CF=" + CF.ToString() + "cf1:" + totsamescore + " totnum=" + totnum.ToString() + " score=" + score.ToString();
                }
        float scoretotal = 0;
        scoretotal = float.Parse(score.ToString());
        return scoretotal;


        //double score = 0;
        //int XF = 1, CF = 0, totnum = 1;
        //var GetPercentileScore = from perScore in dataclass.ScoreTables
        //                         where perScore.TestId == testid && perScore.TestSectionId == int.Parse(testsecId.ToString()) && perScore.SectionName == sectionname
        //                         select perScore;
        //if (GetPercentileScore.Count() > 0)
        //{
        //    totnum = int.Parse(GetPercentileScore.Count().ToString());
        //    foreach (var totalmarkdet in GetPercentileScore)
        //    {
        //        float mark = float.Parse(totalmarkdet.TotalScore.ToString());
        //        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
        //            XF = XF + 1;
        //        else if (mark < (totalmark - .5))
        //            CF = CF + 1;
        //    }
        //    double totsamescore = .5 * XF; totnum = totnum + 1;
        //    //score = (CF + (.5 * XF) * 100) / totnum;
        //    //score = ((CF + totsamescore) * 100) / totnum;
        //    score = ((CF + totsamescore) / totnum) * 100;
        //    lblMessage.Text += " sectionname= " + sectionname + " XF=" + XF.ToString() + " CF=" + CF.ToString() + "cf1:" + totsamescore + " totnum=" + totnum.ToString() + " score=" + score.ToString();
        //}
        //float scoretotal = 0;
        //scoretotal = float.Parse(score.ToString());
        //return scoretotal;
    }

    private float GetPercentileScoreUserwise_testsec(float totalmark, int testsecionid)
    {
        double score = 0;
        int XF = 1, CF = 0, totnum = 0;
        string quesrystring = "select sum(TotalScore) as SecTotalScore,userid from ScoreTable where TestId=" + testid + " and TestSectionId=" + testsecionid + " group by Userid";
        DataSet dssecdet = new DataSet();
        dssecdet = clsClasses.GetValuesFromDB(quesrystring);
        if (dssecdet != null)
            if (dssecdet.Tables.Count > 0)
                if (dssecdet.Tables[0].Rows.Count > 0)
                {
                    totnum = dssecdet.Tables[0].Rows.Count;
                    for (int i = 0; i < dssecdet.Tables[0].Rows.Count; i++)
                    //foreach (var totalmarkdet in GetPercentileScore)
                    {
                        float mark = float.Parse(dssecdet.Tables[0].Rows[i]["SecTotalScore"].ToString());
                        if (mark >= (totalmark - .5) && mark <= (totalmark + .5))
                            XF = XF + 1;
                        else if (mark < (totalmark - .5))
                            CF = CF + 1;
                    }
                    double totsamescore = .5 * XF; totnum = totnum + 1;
                    //score = (CF + (.5 * XF) * 100) / totnum;
                    score = ((CF + totsamescore) * 100) / totnum;

                }
        float scoretotal = 0;
        scoretotal = float.Parse(score.ToString());
        return scoretotal;
    }

    private int GetSectionId(string sectionname)
    {
        int secID = 0;
        string querystring1 = "select SectionId from SectionDetail where ParentId=0 and SectionName='" + sectionname + "'";

        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["SectionId"] != "")
                        secID = int.Parse(ds1.Tables[0].Rows[0]["SectionId"].ToString());

        return secID;
    }

    private String GetBandDescription(int mark, string secName)
    {
        string remarks = "";
        int secid = GetSectionId(secName);
        string querystring1 = "SELECT TestID, BenchMark,DisplayName FROM TestVariableResultBands WHERE TestID = " + testid;
        querystring1 += " AND (" + mark + " > MarkFrom AND  " + mark + " <= MarkTo)";//MarkFrom <= " + mark + " AND  MarkTo >= " + mark;
        querystring1 += " AND VariableId = " + secid;
        DataSet ds1 = new DataSet();
        ds1 = clsClasses.GetValuesFromDB(querystring1);
        if (ds1 != null)
            if (ds1.Tables.Count > 0)
                if (ds1.Tables[0].Rows.Count > 0)
                    if (ds1.Tables[0].Rows[0]["DisplayName"] != "")
                        remarks = ds1.Tables[0].Rows[0]["DisplayName"].ToString();

        return remarks;
    }


    private Boolean CheckExistence(string section, int testsectinid)
    {
        Boolean result = false;
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SectionName"].ToString() == section && dt.Rows[i]["TestSectionId"].ToString() == testsectinid.ToString())
                {
                    result = true;
                    Session["RowID"] = i;
                }
            }
        }
        return result;
    }


    private int GetSectionwiseTotalQuestionMarks(string sectioname, string testsectionid, int index)
    {
        int totalMemWordQuesCount = 0, totalMemImagesQuesCount = 0, totalRatingQuesCount = 0, totalOtherQuesCount = 0;
        int totalObjQuesCount = 0, totalFillQuesCount = 0, totalImageQuesCount = 0, totalVideoQuesCount = 0, totalAudioQuesCount = 0,
            totalPhotoQuesCount = 0;
        string QuerystringQuestionCount = "select sum(ObjQuestionCount) as ObjQuestions,sum(FillBlanksQuestionCount) as FillQuestions," +
                                            "sum(RatingQuestionCount) as RatingQuestions, sum(ImageQuestionCount) as ImageQuestions," +
                                            "sum(VideoQuestionCount) as VideoQuestions,sum(AudioQuestionCount) as AudioQuestions," +
                                            "sum(PhotoTypeQuestionCount) as PhotoQuestions,sum(WordTypeMemQuestionCount) as WordMemQuestions," +
                                            "sum(ImageTypeMemQuestionCount) as ImageMemQuestions from questioncount where TestId=" + testid + " and TestSectionId=" + testsectionid;
        if (index == 0)
            QuerystringQuestionCount += "and SectionName='" + sectioname + "'";

        DataSet dsQuestionCount = clsClasses.GetValuesFromDB(QuerystringQuestionCount);
        if (dsQuestionCount != null)
            if (dsQuestionCount.Tables.Count > 0)
                if (dsQuestionCount.Tables[0].Rows.Count > 0)
                {
                    for (int c = 0; c < dsQuestionCount.Tables[0].Rows.Count; c++)
                    {
                        if (dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString() != "")
                        {
                            totalObjQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["ObjQuestions"].ToString());
                            totalOtherQuesCount += totalObjQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString() != "")
                        {
                            totalFillQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["FillQuestions"].ToString());
                            totalOtherQuesCount += totalFillQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString() != "")
                        {
                            totalImageQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["ImageQuestions"].ToString());
                            totalOtherQuesCount += totalImageQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString() != "")
                        {
                            totalVideoQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["VideoQuestions"].ToString());
                            totalOtherQuesCount += totalVideoQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString() != "")
                        {
                            totalAudioQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["AudioQuestions"].ToString());
                            totalOtherQuesCount += totalAudioQuesCount;
                        }
                        if (dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString() != "")
                        {
                            totalPhotoQuesCount = int.Parse(dsQuestionCount.Tables[0].Rows[c]["PhotoQuestions"].ToString());
                            totalOtherQuesCount += totalPhotoQuesCount;
                        }

                        if (dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString() != "")
                            totalRatingQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["RatingQuestions"].ToString());
                        if (dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString() != "")
                            totalMemWordQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["WordMemQuestions"].ToString());
                        if (dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString() != null && dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString() != "")
                            totalMemImagesQuesCount += int.Parse(dsQuestionCount.Tables[0].Rows[c]["ImageMemQuestions"].ToString());
                    }
                }

        int totalmark = 0;
        DataSet dsCount = new DataSet();
        int memimageQuesValue = 0, memWordsQuesValue = 0, quesValue = 0, ratingQuesValue = 0;
        string quesryString = "";
        // memImages questions
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memImages where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        memimageQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (memimageQuesValue > totalMemImagesQuesCount)
                            memimageQuesValue = totalMemImagesQuesCount;
                    }

        //memWords questions
        dsCount = new DataSet();
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList_memWords where status=1 and TestId=" + testid + " and TestSectionId=" + testsectionid + "  and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        memWordsQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (memWordsQuesValue > totalMemWordQuesCount)
                            memWordsQuesValue = totalMemWordQuesCount;
                    }

        //Rating questions
        dsCount = new DataSet();
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid + "  and SectionName='" + sectioname + "'";

        //lblMessage.Text += quesryString;

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        int totalrate = 0; int questionid = 0;
                        ratingQuesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (ratingQuesValue > totalRatingQuesCount)
                            ratingQuesValue = totalRatingQuesCount;
                        // questionid = int.Parse(dsCount.Tables[0].Rows[0][1].ToString());
                        // lblMessage.Text += sectioname + " ratingquesCount= " + ratingQuesValue.ToString();// 021209 bip
                        if (index > 0)
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.TestSectionId == int.Parse(testsectionid) && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * (totalrate - 1);// bip 08-06-2011

                                // lblMessage.Text += " totaloptions= " + totalrate + " TotRate= " + ratingQuesValue.ToString() + " ... ";
                            }
                        }
                        else
                        {
                            var optionCount = from optcount in dataclass.View_TestBaseQuestionLists
                                              where optcount.TestSectionId == int.Parse(testsectionid) && optcount.SectionName == sectioname && optcount.TestId == testid && optcount.Category == "RatingType" && optcount.TestBaseQuestionStatus == 1
                                              select optcount;
                            if (optionCount.Count() > 0)
                            {
                                if (optionCount.First().Option1 != null && optionCount.First().Option1.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option2 != null && optionCount.First().Option2.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option3 != null && optionCount.First().Option3.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option4 != null && optionCount.First().Option4.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option5 != null && optionCount.First().Option5.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option6 != null && optionCount.First().Option6.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option7 != null && optionCount.First().Option7.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option8 != null && optionCount.First().Option8.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option9 != null && optionCount.First().Option9.ToString() != "")
                                    totalrate++;
                                if (optionCount.First().Option10 != null && optionCount.First().Option10.ToString() != "")
                                    totalrate++;
                                //if (totalrate > 0)
                                ratingQuesValue = ratingQuesValue * (totalrate - 1);// bip 08-06-2011
                            }
                        }
                    }
        //other type questions
        //if (index > 0)
        //    quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category <> 'RatingType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        //else
        //    quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category <> 'RatingType' and TestId=" + testid + " and SectionName='" + sectioname + "'";
        //dsCount = clsClasses.GetValuesFromDB(quesryString);
        //if (dsCount != null)
        //    if (dsCount.Tables.Count > 0)
        //        if (dsCount.Tables[0].Rows.Count > 0)
        //            if (dsCount.Tables[0].Rows[0][0] != null)
        //            {
        //                quesValue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
        //                if (quesValue > totalOtherQuesCount)
        //                    quesValue = totalOtherQuesCount;
        //            }

        //////Objective,FillBlanks,RatingType,ImageType,VideoType,AudioType,MemTestWords,MemTestImages,PhotoType

        dsCount = new DataSet();
        int curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'Objective' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'Objective' and TestId=" + testid + " and SectionName='" + sectioname + "'";
        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalObjQuesCount)
                            curvalue = totalObjQuesCount;

                        quesValue += curvalue;
                    }

        // filblanks
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'FillBlanks' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'FillBlanks' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalFillQuesCount)
                            curvalue = totalFillQuesCount;

                        quesValue += curvalue;
                    }

        // imagetype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'ImageType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'ImageType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalImageQuesCount)
                            curvalue = totalImageQuesCount;

                        quesValue += curvalue;
                    }
        // videotype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'VideoType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'VideoType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalVideoQuesCount)
                            curvalue = totalVideoQuesCount;

                        quesValue += curvalue;
                    }
        // audiotype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'AudioType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'AudioType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalAudioQuesCount)
                            curvalue = totalAudioQuesCount;

                        quesValue += curvalue;
                    }
        // phototype
        dsCount = new DataSet();
        curvalue = 0;
        if (index > 0)
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and Category = 'PhotoType' and TestId=" + testid + " and TestSectionId=" + testsectionid;
        else
            quesryString = "select Count(*) as TotalCount from View_TestBaseQuestionList where TestBaseQuestionStatus=1 and  Category = 'PhotoType' and TestId=" + testid + " and SectionName='" + sectioname + "'";

        dsCount = clsClasses.GetValuesFromDB(quesryString);
        if (dsCount != null)
            if (dsCount.Tables.Count > 0)
                if (dsCount.Tables[0].Rows.Count > 0)
                    if (dsCount.Tables[0].Rows[0][0] != null)
                    {
                        curvalue = int.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                        if (curvalue > totalPhotoQuesCount)
                            curvalue = totalPhotoQuesCount;

                        quesValue += curvalue;
                    }

        //////

        totalmark = memimageQuesValue + memWordsQuesValue + quesValue + ratingQuesValue;

        return totalmark;
    }


    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "OrgAdmin")
        { Session["SubCtrl"] = "ReportSel_OrgAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }

        else if (Session["usertype"].ToString() == "GrpAdmin")
        { Session["SubCtrl"] = "ReportSel_GroupAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
        else if (Session["usertype"].ToString() == "SuperAdmin")
        { Session["SubCtrl"] = "ReportSel_SuperAdmin.ascx"; Response.Redirect("FJAHome.aspx"); }
        else
        { Session["SubCtrl"] = "TestListControl.ascx"; Response.Redirect("FJAHome.aspx"); }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["totalparts"] = txtParts.Text.Trim();
        Session["displayvalues"] = txtValues.Text.Trim();
        Session["testsectionreportvalues"] = dtTestSection;
        DataTable dtvariablevalues = new DataTable();
        DataRow drvariablelist;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows[0].Cells.Count; i++)
            {
                dtvariablevalues.Columns.Add("column" + (i + 1).ToString());
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                drvariablelist = dtvariablevalues.NewRow();
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    if (GridView1.Rows[i].Cells[j].Text != "&nbsp;")
                        drvariablelist["column" + (j + 1).ToString()] = GridView1.Rows[i].Cells[j].Text;
                }
                dtvariablevalues.Rows.Add(drvariablelist);
            }

            Session["variablereportvalues"] = dtvariablevalues;

            Response.Redirect("ReportPrintNewCITAT.aspx"); return;
        }
        else { lblMessage.Text = "No Values for Print"; }


    }

}