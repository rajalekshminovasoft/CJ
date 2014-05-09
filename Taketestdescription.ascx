﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Taketestdescription.ascx.cs" Inherits="Taketestdescription" %>
<link rel="stylesheet" type="text/css" href="style.css" />
<div id="main_box_middle">
    
    
     <div class="main_box_left">
     		<div class="take_a_test"><img src="images/take_a_test.jpg" width="653" height="210" alt="" /></div>
            
            
            
            
       <div class="take_a_test_text">
   		<div class="left_text_head">
               <asp:Literal ID="lit_innerhead" runat="server"></asp:Literal> </div>
     
        <div class="left_text_inner">
            <asp:Literal ID="lit_innercontent" runat="server"></asp:Literal>
            <%--Welcome to Career Judge’s online assessment engine TalentSCOUT™ . You can take tests of your choice from a long list of test batteries and assessments available through this engine. You can select a test of your choice or contact us for more support in selecting the right test that suits your objective.<br/>
        
        
        
    Following are the major test categories available on TalentSCOUT:
<div class="hd">HR Selection Tests</div>
These are meant for HR selection and recruitment purposes, essentially to screen candidates on the basis of their performance scores on job-specific competencies. You can contact Career Judge, if you want to know the terms and conditions for using these tests.<br/>
<div class="hd">Managerial Leadership Tests</div>
These tests assess your leadership potential, your ability to manage people and processes. They help you identify areas of your strength and development. Interpretative reports tell you what your individual scores on different assessment variables mean and give recommendation wherever applicable.<br/>
<div class="hd">Soft-Skills Tests</div>
They assess your personal skills in managing and mastering situations in the social and work situations. Skills such as assertiveness, communication, negotiation, conflict resolution etc. are some examples which typify this category. Career Judge provides you a set of assessments to identify your soft-skill strengths and weaknesses.<br/>
<div class="hd">Career Profiling Tests</div>
They include tests which provide a career profile report. They allow you to judge which careers are best fit for you based on the analysis of your career interests, abilities and aptitudes, skills and personality disposition. Normally, these tests assess you on these variables and provide your detailed interpretative reports. You can use this information to identify which careers are more suitable for you than others and to make wise decisions on career selection.<br/>
<div class="hd">General Aptitude Tests</div>
If you are interested in knowing your basic aptitudes, you can select appropriate tests available with Career Judge. Such aptitudes include quantitative aptitude, mechanical aptitude, analytical aptitude and several others. Many of them are essential for successful performance on most jobs.<br/>
<div class="hd">Creative Art Tests</div>
Different art works require different abilities in varying proportions. We have specific tests for different art fields. You can evaluate how strong your artistic abilities are for different arts, such as fine arts, plastic arts, performance arts, photography, etc.<br/>
<div class="hd">Social Skills Tests</div>
Social life requires several skills to be effective in our relationships and social interactions. You can learn about yourself, where you are standing on different social skills. <br/>   
        <br/>
                        Select any of the test categories from the left-side menu bar for learning more about tests available with us.
      
      --%>

            <div id="magento-com">
	<div class="btn-outline">
    	<div class="contact-us">
            <asp:LinkButton ID="lnk_register" runat="server" OnClick="lnk_register_Click" >Register</asp:LinkButton>
            <%--<a href="#">Contact Us</a>--%></div>
    </div>
</div>

        </div>

           <div class="left_text_inner">

            <asp:Literal ID="lit_inner1" runat="server"></asp:Literal>
               </div>
   </div> 
            
            
            
            
            
     </div>
    
    
    <div class="main_box_right">
    
    
    
    
    
    
    	<div class="take_a_test_right_banner">
        	<div class="take_a_test_right_banner_inner">,
                <asp:Literal ID="lit_righttop" runat="server"></asp:Literal>



<%--<div id="magento-com">
	<div class="btn-outline">
    	<div class="contact-us"><a href="#">Contact Us</a></div>
    </div>
</div>--%>



</div>
        </div>
            <div class="take_a_test_right_menu">
            	<div class="take_a_test_right_menu_box">Learn more about tests </div>
            	<div class="right_menu_bg"><a href="#">HR Selection Tests</a></div>
                <div class="right_menu_bg"><a href="#">Managerial Leadership Tests</a></div>
                <div class="right_menu_bg"><a href="#">Soft-Skills Tests</a></div>
                <div class="right_menu_bg"><a href="#">Career Profiling Tests</a></div>
                <div class="right_menu_bg"><a href="#">General Aptitude Tests</a></div>
                <div class="right_menu_bg"><a href="#">Creative Art Tests</a></div>
                <div class="right_menu_bg"><a href="#">Social Skills Tests</a></div>
            </div>
    
    
    
    
    </div>
   	  
        
    </div>