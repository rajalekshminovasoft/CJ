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

public partial class TestIntroductionControl : System.Web.UI.UserControl
{
    AssesmentDataClassesDataContext dataclassses = new AssesmentDataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillText();
    }
    protected void ptnPrevious_Click(object sender, EventArgs e)
    {
        ShowPreviousControl();
    }
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        ShowNextControl();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session["SubCtrl"] = null;
        Response.Redirect("FJAHome.aspx");
    }

    private void FillText()
    {
        Panel1.Visible = true;
        container.InnerHtml = "";
        int testid = 0;
        if (Session["curtestid"] != null)
        {
            //Session["curtestid"] = Session["UserTestId"];
            testid = int.Parse(Session["curtestid"].ToString());
            var Details = from det in dataclassses.TestLists
                          where det.TestId == testid
                          select det;
            if (Details.Count() > 0)
            {
                string trainingdetails = "<div>";
                if (Details.First().Instructions != null && Details.First().Instructions != "")
                    trainingdetails += Details.First().Instructions.ToString();

                if (Details.First().Description != null && Details.First().Description != "")
                    trainingdetails += "<br/>" + Details.First().Description.ToString();
                if (trainingdetails != "<div>")
                {
                    container.InnerHtml = trainingdetails;
                    return;
                }
            }
         //////   ShowNextControl();
        }
        //******************************
        //if (Session["UserTestId1"] != null)
        //{
        //Session["curtestid"] = Session["UserTestId1"];
        //    testid = int.Parse(Session["UserTestId1"].ToString());
        //    var Details = from det in dataclassses.TestLists
        //                  where det.TestId == testid
        //                  select det;
        //    if (Details.Count() > 0)
        //    {
        //        string trainingdetails = "<div>";
        //        if (Details.First().Instructions != null && Details.First().Instructions != "")
        //            trainingdetails += Details.First().Instructions.ToString();

        //        if (Details.First().Description != null && Details.First().Description != "")
        //            trainingdetails += "<br/>" + Details.First().Description.ToString();
        //        if (trainingdetails != "<div>")
        //        {
        //            container.InnerHtml = trainingdetails;
        //            return;
        //        }
        //    }
        //    ShowNextControl();
        //}
        ////**********************
    }

    private void ShowPreviousControl()
    {
        Session["SubCtrl"] = "TestListControl.ascx";
        Response.Redirect("FJAHome.aspx");
    }

    private void ShowNextControl()
    {
        int userid = 0;
        string usercode = "";
        if (Session["UserCode"] != null)
        {
            usercode = Session["UserCode"].ToString();
        }
        if (Session["UserID"] != null)
            userid = int.Parse(Session["UserID"].ToString());
        string curcontrol = "ThankYou.ascx";
        int Evalstatid = 0;
         var EvaluationDetails = from EvalDet in dataclassses.EvaluationStatus1s
                                 where EvalDet.UserId == userid && EvalDet.Testid == int.Parse(Session["curtestid"].ToString()) 
                                            select EvalDet;
         if (EvaluationDetails.Count() > 0)
         {
             Evalstatid = int.Parse(EvaluationDetails.First().EvalStatusId.ToString());
         }
         if (Session["EvalStatId"] != null)
         {
             Evalstatid = int.Parse(Session["EvalStatId"].ToString());
         }
         dataclassses.ProcedureEvaluationStatus(Evalstatid, curcontrol, 1, 0, usercode, userid,int.Parse(Session["curtestid"].ToString()));



        Session["dsTestSectionIds"] = null;
        Session["questionColl_fill"] = null;
        Session["questionColl_Rating"] = null;
        Session["questionColl_img"] = null;
        Session["questionColl_video"] = null;
        Session["questionColl_Audio"] = null;
        Session["CurrentTestSectionId"] = null;
        Session["sectionIdIndexNo"] = null;
        Session["questionColl"] = null;
        Session["evaldirection"] = null;
        Session["timeExpired"] = null;
        //curcontrol = "ObjectiveQuestns.ascx";
        if (Session["curtestid"] != null)
        {
            Session["UserTestId"] = Session["curtestid"];
            Session["SubCtrl"] = "ObjectiveQuestns.ascx";
            Response.Redirect("FJAHome.aspx");
        }
        //Session["SubCtrl"] = "ObjectiveQuestns.ascx";
        //Response.Redirect("FJAHome.aspx");
    }

}
