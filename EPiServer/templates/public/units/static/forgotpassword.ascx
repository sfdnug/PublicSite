<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="ForgotPassword.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.ForgotPassword" %>
<asp:PasswordRecovery ID="PasswordRecovery1" CssClass="forgotPassArea" runat="server">
    <UserNameTemplate>
        <fieldset>
            <asp:Label ID="Label1" Text="<%$ Resources: EPiServer, usersettings.username %>" AssociatedControlID="UserName" runat="server" />
            <asp:TextBox ID="UserName" ToolTip="<%$ Resources: EPiServer, usersettings.username %>" TabIndex="2" runat="server" />
            <asp:Button ID="Submit" Text="<%$ Resources: EPiServer, navigation.send %>" CommandName="Submit" runat="server" />
        </fieldset>
    </UserNameTemplate>
</asp:PasswordRecovery>