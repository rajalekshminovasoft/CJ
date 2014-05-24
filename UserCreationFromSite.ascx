<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserCreationFromSite.ascx.cs" Inherits="UserCreationFromSite" %>
<div align="left" style="padding-top: 10px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <table>
            <tr>
                <td align="left" valign="top">
                    <%--<table style=" height: 350px">
                        <tr>
                            <td colspan="2"  valign="top">--%>
                                <div class="titlemain">
                                    User Creation</div>
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="label">
                                Name Of Organisation:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrg" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True"  Width="402px" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="OrgLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (Name, OrganizationID)" TableName="Organizations" 
                                    Where="Status == @Status &amp;&amp; Name == @Name">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                        <asp:Parameter DefaultValue="Career Judge" Name="Name" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td class="label">
                                Name of Test :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestLists" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqTestLists" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status &amp;&amp; OrganizationName == @OrganizationName">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                        <asp:Parameter DefaultValue="Career Judge" Name="OrganizationName" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Name of Test1 :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestlIst2" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" >
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status &amp;&amp; OrganizationName == @OrganizationName">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                        <asp:Parameter DefaultValue="Career Judge" Name="OrganizationName" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td class="label" >
                                Group Name:</td>
                            <td>
                                <asp:DropDownList ID="ddlUserGroup" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" >
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="GrpUserLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (GroupName, GroupUserID, OrganizationID)" TableName="GroupUsers" Where="OrganizationID == @OrganizationID">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="OrganizationID" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
            <td class="label">
                First Name:</td>
            <td>
                <asp:TextBox ID="txtFsName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Middle Name:</td>
            <td>
                <asp:TextBox ID="txtMidName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Last Name:</td>
            <td>
                <asp:TextBox ID="txtLstName" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Gender:</td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" Width="80px">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                Age:</td>
            <td>
                <asp:TextBox ID="txtAge" runat="server" Text="0"  
                                        onChange="myJSFunction(this);" 
                    inblur="myJSFunction(this);" MaxLength="3" Width="75px"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Email:</td>
            <td>
                <asp:TextBox ID="txtEmailId" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmailId" ErrorMessage="Invalid email" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                Contact Number:</td>
            <td>
                <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="20" Width="350px"></asp:TextBox>
            </td>
        </tr>

                        
                         <tr>
                            <td class="label" >
                                User Type :</td>
                            <td>
                                <asp:DropDownList ID="ddlUserType" runat="server" Width="128px">
                                    <%--<asp:ListItem Value="0">--select--</asp:ListItem>
                                    <asp:ListItem Value="SuperAdmin">Super Admin</asp:ListItem>
                                    <asp:ListItem Value="SpecialAdmin">Special Admin</asp:ListItem>
                                    <asp:ListItem>OrgAdmin</asp:ListItem>
                                    <asp:ListItem>GrpAdmin</asp:ListItem>--%>
                                    <asp:ListItem>User</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

              <%--             <tr>
            <td class="label">
                <asp:Label ID="lblUserGrp_label" runat="server" Text="User Group:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGrpUser" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True"  Enabled="False" 
                    Width="355px">
                    <asp:ListItem Value="0">--select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (GroupUserID, OrganizationID, JobCatID, GroupName)" 
                    TableName="GroupUsers">
                </asp:LinqDataSource>
            </td>
        </tr>--%>
        <tr>
            <td class="label">
                Industry:</td>
            <td>
                <asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True"                   Width="355px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource3" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Name, IndustryID)" TableName="Industries">
                </asp:LinqDataSource>
                <br /><asp:TextBox ID="txtIndustry" runat="server" Visible="False" Width="350px" 
                    MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Vocation:</td>
            <td>
                <asp:DropDownList ID="ddlJobCatgy" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="LinqDataSource4" DataTextField="Name" DataValueField="JobCatID"
                     Width="355px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqDataSource4" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" Select="new (Name, JobCatID)" 
                    TableName="JobCategories">
                </asp:LinqDataSource>
                <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                Designation:</td>
            <td>
                <asp:TextBox ID="txtJob" runat="server" MaxLength="100" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Total Years of Experience:</td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlTotExpYears" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Years</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTotExpMonths" runat="server">
                                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Months</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                Experience in Present Job:</td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlCurExpYears" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Years</td>
                        <td>
                                            <asp:DropDownList ID="ddlCurExpMonths" runat="server">
                                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </td>
                        <td>
                            Months</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                Educational Qualification:</td>
            <td>
                <asp:DropDownList ID="ddlQualification" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    Width="350px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqQualifications" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Qualification1, QualificationId)" TableName="Qualifications">
                </asp:LinqDataSource>
                <asp:Label ID="Label4" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                <br />
                <asp:TextBox ID="txtEduQual" runat="server" Visible="False" Width="350px" 
                    MaxLength="50"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td class="label">
                Professional Qualification :</td>
            <td>
                <asp:TextBox ID="txtEduQual_professional" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Professional Certification:</td>
            <td>
                <asp:TextBox ID="txtProffQual" runat="server" TextMode="MultiLine" 
                    Height="100px" Width="350px"></asp:TextBox>
            </td>
        </tr>
                      <%--   <tr>
                            <td class="label">
                                Login From Date:</td>
                            <td>
                               <asp:TextBox ID="txtLoginFromDate" runat="server" ></asp:TextBox>
                                <input ID="txtLoginFromDate" runat="server" 
                                    onclick="dispCal('txtLoginFromDate')" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="label" >
                                Login To Date:</td>
                            <td>
                                <asp:TextBox ID="txtLoginToDate" runat="server"  ></asp:TextBox>
                                <input id="txtLoginToDate" runat="server" onclick="dispCal('txtLoginToDate')" 
                    readonly="readonly" /></td>
                        </tr>--%>
                        <tr>
                            <td class="label" >
                                Status:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="128px">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

              <tr>
                            <td class="label" >
                                User Name:</td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" >
                                Password:</td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                       

                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" />
                                <%--<asp:Button ID="btnReset" runat="server" Text="Reset" 
                    onclick="btnReset_Click" />
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
                                <asp:Button ID="btnBulkCreation" runat="server" 
                    Text="Bulk Upload" onclick="btnBulkCreation_Click" />--%>
                            </td>
                        </tr>
                        
                    </table>
                
   </asp:Panel>
</div>


<p>
    &nbsp;</p>