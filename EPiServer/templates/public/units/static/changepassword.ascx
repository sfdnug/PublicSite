<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="ChangePassword.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.ChangePassword" %>
<asp:LinkButton OnClick="ToggleChangePass_Click" Text="<%$ Resources: EPiServer, usersettings.changepassword %>" runat="server" />
<asp:ChangePassword CssClass="changePassArea" ID="ctlChangePass" Visible="false" runat="server">
    <ChangePasswordTemplate>
        <fieldset>
            <asp:Label Text="<%$ Resources: EPiServer, login.password %>" AssociatedControlID="CurrentPassword" runat="server" /> 
            <asp:TextBox ID="CurrentPassword" TextMode="Password" ToolTip="<%$ Resources: EPiServer, login.password %>" TabIndex="3" runat="server" />
            <asp:Label Text="<%$ Resources: EPiServer, usersettings.newpassword %>" AssociatedControlID="NewPassword" runat="server" /> 
            <asp:TextBox ID="NewPassword" TextMode="Password" ToolTip="<%$ Resources: EPiServer, usersettings.newpassword %>" TabIndex="3" runat="server" />
            <asp:Label Text="<%$ Resources: EPiServer, usersettings.confirmpassword %>" AssociatedControlID="ConfirmPassword" runat="server" /> 
            <asp:TextBox ID="ConfirmPassword" TextMode="Password" ToolTip="<%$ Resources: EPiServer, usersettings.confirmpassword %>" TabIndex="3" runat="server" />
            <div id="ButtonArea">
                <asp:Button ID="ChangePassword" CssClass="changePassButton" CommandName="ChangePassword" Text="<%$ Resources: EPiServer, navigation.send %>" runat="server" />
            </div>
        </fieldset>
        <div id="MessageArea">
            <asp:Label ID="FailureText" CssClass="error" runat="server" />
        </div>
    </ChangePasswordTemplate>
    <SuccessTemplate>
        <EPiServer:Property PropertyName="PasswordChangeSuccessBody" runat="server" />
    </SuccessTemplate>
</asp:ChangePassword>