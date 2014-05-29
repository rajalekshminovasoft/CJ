<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserCreation.ascx.cs" Inherits="WebUserCreation" %>
<div  align="center" style="padding-top: 5px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" OnFinishButtonClick="Wizard1_FinishButtonClick" OnSideBarButtonClick="Wizard1_SideBarButtonClick" OnActiveStepChanged="Wizard1_ActiveStepChanged" OnNextButtonClick="Wizard1_NextButtonClick" OnPreviousButtonClick="Wizard1_PreviousButtonClick">
            <NavigationButtonStyle ForeColor="#660033" />
            <SideBarButtonStyle ForeColor="#660033" />

            <WizardSteps>
                <asp:WizardStep ID="Stp1" runat="server" Title="Select Test" >
                    <table>
                        <tr>
                            <td>
                                <asp:DataList ID="dtTestlist" runat="server" RepeatColumns="2" >
                                    <ItemTemplate>
                                        <div class="take_a_test_right_menu">
                                            <table border="0">
                                                <tr>
                                                    <td>
                                                        <div class="right_menu_bg">
                                                            <asp:CheckBox ID="chktest" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                            <asp:LinkButton ID="lnk" runat="server"><%# Eval("TestName") %></asp:LinkButton>
                                                            Passmark:<asp:Label ID="PassMark" runat="server" Text='<%# Eval("PassMark") %>' />
                                                            <asp:Label ID="lbltestid" runat="server" Visible="false"  Text='<%# Eval("TestId") %>' />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                <td>Test Name:</td>
                <td><asp:Label ID="TestName" runat="server"
                    Text='<%# Eval("TestName") %>' /></td></tr><tr>
                <td >PassMark:</td>
                <td><asp:Label ID="PassMark" runat="server"
                    Text='<%# Eval("PassMark") %>' /></td>
                          </tr>--%>
                                                
                                            </table>
                                             
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:LinqDataSource ID="LinqTestLists" runat="server" ContextTypeName="AssesmentDataClassesDataContext" Select="new (TestId, TestName, OrganizationName,PassMark)" TableName="TestLists" Where="Status == @Status &amp;&amp; OrganizationName == @OrganizationName">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                        <asp:Parameter DefaultValue="Career Judge" Name="OrganizationName" Type="String" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="Stp2" runat="server" Title="Sign Up">
                </asp:WizardStep>
                <asp:WizardStep ID="Stp3" runat="server" Title="Payment">
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
                
   </asp:Panel>
</div>


<p>
    &nbsp;</p>