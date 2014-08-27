<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestListControl.ascx.cs" Inherits="TestListControl" %>
<div  align="center" style="padding-top: 5px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <table width="75%" align="center">
        <tr>
            <td>
                <asp:gridview id="grd_usertest" runat="server" AllowPaging="True" AutoGenerateColumns="False"  EnableModelValidation="True" width="85%" align="center" OnSelectedIndexChanged="grd_usertest_SelectedIndexChanged">
                    <Columns>

                        <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
                        <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                        <asp:BoundField DataField="TestName" HeaderText="TestName" SortExpression="TestName" />
                        <asp:BoundField DataField="TestLoginDate" HeaderText="TestLoginDate" SortExpression="TestLoginDate" DataFormatString="{0:g}"  />
                        <asp:BoundField DataField="TestLogoutDate" HeaderText="TestLogoutDate" SortExpression="TestLogoutDate" />
                        <asp:BoundField DataField="TestStatus" HeaderText="TestStatus" SortExpression="TestStatus" />
                        <asp:BoundField DataField="PhoneNum" HeaderText="PhoneNum" SortExpression="PhoneNum" />
                        <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server"  OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="UserTestId" HeaderText="UserTestId" SortExpression="UserTestId" />                       
                    </Columns>
                </asp:gridview>
               
            </td>
        </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Take test" OnClick="Button1_Click" Visible="false"  />
                    <asp:Button ID="Button2" runat="server" Text="View Report" Visible="false" OnClick="Button2_Click"  />
                </td>
            </tr>
    </table>
                
   </asp:Panel>
</div>  

