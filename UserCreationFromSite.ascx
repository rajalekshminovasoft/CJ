<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserCreationFromSite.ascx.cs" Inherits="UserCreationFromSite" %>
<div align="left" style="padding-top: 10px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <table>
            <tr>
                <td align="left" valign="top">
                    <table style=" height: 350px">
                        <tr>
                            <td colspan="2" 
                valign="top">
                                <div class="titlemain">
                                    User Creation</div>
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="label">
                                Name Of Organisation:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrg" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrg_SelectedIndexChanged" Width="402px">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="OrgLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (Name, OrganizationID)" TableName="Organizations" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Name of Test :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestLists" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTestLists_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqTestLists" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>

                         <tr>
                            <td class="label">
                                Name of Test1 :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestlIst2" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTestlIst2_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td class="label" >
                                Group Name:</td>
                            <td>
                                <asp:DropDownList ID="ddlUserGroup" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlUserGroup_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="GrpUserLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (GroupName, GroupUserID, OrganizationID)" TableName="GroupUsers">
                                </asp:LinqDataSource>
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
                            <td class="label">
                                EmailId</td>
                            <td>
                                <asp:TextBox ID="txtEmailId" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="label" >
                                User Type :</td>
                            <td>
                                <asp:DropDownList ID="ddlUserType" runat="server" Width="128px">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                    <asp:ListItem Value="SuperAdmin">Super Admin</asp:ListItem>
                                    <asp:ListItem Value="SpecialAdmin">Special Admin</asp:ListItem>
                                    <asp:ListItem>OrgAdmin</asp:ListItem>
                                    <asp:ListItem>GrpAdmin</asp:ListItem>
                                    <asp:ListItem>User</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Login From Date:</td>
                            <td>
                               <asp:TextBox ID="txtLoginFromDate" runat="server" ></asp:TextBox>
                              <%--   <input ID="txtLoginFromDate" runat="server" 
                                    onclick="dispCal('txtLoginFromDate')" readonly="readonly" />--%></td>
                        </tr>
                        <tr>
                            <td class="label" >
                                Login To Date:</td>
                            <td>
                                <asp:TextBox ID="txtLoginToDate" runat="server"  ></asp:TextBox>
                              <%--  <input id="txtLoginToDate" runat="server" onclick="dispCal('txtLoginToDate')" 
                    readonly="readonly" />--%></td>
                        </tr>
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
                            <td colspan="2">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" />
                                <%--<asp:Button ID="btnReset" runat="server" Text="Reset" 
                    onclick="btnReset_Click" />
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
                                <asp:Button ID="btnBulkCreation" runat="server" 
                    Text="Bulk Upload" onclick="btnBulkCreation_Click" />--%>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
   </asp:Panel>
</div>
<%--<asp:Panel ID="pnlBulkUserCreation" runat="server" Visible="False">
    <div align="left">
        <table style="width:100%;">
            <tr>
                <td colspan="2" 
                            valign="top">
                    <div class="titlemain">
                        Import User Details from an Excel File
                    </div>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Organisation</td>
                <td>
                    <asp:DropDownList ID="ddlOrganization_bulkuser" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="OrgLinqDataSource" 
                        DataTextField="Name" DataValueField="Organizationid">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Prefix</td>
                <td>
                    <asp:TextBox ID="txtPrefix" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    User Details File(select excel files only)</td>
                <td>
                    <asp:TextBox ID="txtfilename" runat="server" Width="300px" BackColor="White" 
                    Enabled="False"></asp:TextBox>
                    <input id="btnBrowse" type="button" 
                    value="Browse file ...." onclick="dispBulkUserExcelFile('txtfilename')" /></td>
            </tr>
            <tr>
                <td class="label">
                    Excel Sheet Name</td>
                <td>
                    <asp:TextBox ID="txtSheetName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage0" runat="server" ForeColor="#FF3300"></asp:Label>
                    <asp:Panel ID="pnlProcessingSel" runat="server">
                        <div ID="divProcessSel" align="center">
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnImportUserList" runat="server" OnClientClick="ShowSelProcess();" onclick="btnImportUserList_Click" 
                    Text="Import Data from Excel file" Width="200px" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Reset" />
                    &nbsp;
                    <asp:Button ID="btnGoBack" runat="server" onclick="btnGoBack_Click" 
                        Text="Go Back" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtErrorList" runat="server" Height="200px" 
                        TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btndownload" runat="server" onclick="btndownload_Click" 
                        Text="Download Error list" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>--%>