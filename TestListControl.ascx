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
                    <asp:Label ID="lblreptype" runat="server" Text="Summary Report Type" Visible="false" ></asp:Label>
                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged"  >
                    <asp:ListItem>Interpretative Report</asp:ListItem>
                        <asp:ListItem>Indicative Report</asp:ListItem>
                        <asp:ListItem>Certification Report</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <asp:Label ID="lblReportCategory" runat="server" Text="Report Category"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReportCategory" runat="server">
                        <asp:ListItem>Variablewise</asp:ListItem>
                        <asp:ListItem>TestSectionwise</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblsummeryrep" runat="server" Text="Summary Report Type" Visible="false" ></asp:Label>
                    
                    <asp:DropDownList ID="ddlSummaryGraph" runat="server" Visible="false" 
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="Percentage">Percentage (%)</asp:ListItem>
                        <asp:ListItem Value="Percentile">Percentile</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Take test" OnClick="Button1_Click" Visible="false"  />
                    <asp:Button ID="Button2" runat="server" Text="View Report" Visible="false" OnClick="Button2_Click"  />
                    <asp:Button ID="btn_ciareport" runat="server" Text="View CIA Report" OnClick="btn_ciareport_Click" Visible="false"  />
                    <asp:Button ID="btn_citatq" runat="server" Text="View CITAT-Q Report" Visible="false" OnClick="btn_citatq_Click"  />
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                </td>
            </tr>
        
    </table>
                
   </asp:Panel>
</div>  

