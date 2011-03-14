<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="Login.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.Login"%>
<%@ Register TagPrefix="public" TagName="ForgotPassword"	Src="~/Templates/Public/Units/Static/ForgotPassword.ascx" %>
<%@ Register TagPrefix="public" TagName="ChangePassword"	Src="~/Templates/Public/Units/Static/ChangePassword.ascx" %>

<asp:Content ContentPlaceHolderID="MainContentRegion" runat="server">
    <div id="MainBodyArea">
        <asp:ValidationSummary ID="XFormValidationSummary" runat="server" ValidationGroup="XForm" />
        <div id="MainBody">
            <h1><EPiServer:Property PropertyName="Heading" runat="server" /></h1>
            <asp:Panel ID="LoginPanel" runat="server">
                <asp:LoginView ID="LoginView" runat="server">
                    <AnonymousTemplate>
                        <EPiServer:Property PropertyName="MainBody" runat="server" />
                        
                        <asp:Login ID="LoginControl" VisibleWhenLoggedIn="false"
                                   FailureText="<%$ Resources: EPiServer, login.error.loginfail %>"
                                   CssClass="loginArea"
                                   runat="server">
                            <LayoutTemplate>
                                <fieldset>
                                    <asp:Label Text="<%$ Resources: EPiServer, usersettings.username %>" AssociatedControlID="UserName" runat="server" />
                                    <asp:TextBox ID="UserName" ToolTip="<%$ Resources: EPiServer, usersettings.username %>" TabIndex="2" runat="server" />
                                    <asp:Label Text="<%$ Resources: EPiServer, login.password %>" AssociatedControlID="Password" runat="server" />
                                    <asp:TextBox ID="Password" TextMode="Password" ToolTip="<%$ Resources: EPiServer, login.password %>" TabIndex="3" runat="server" />
                                   
                                    <div id="ButtonArea">
                                        <asp:Button ID="LoginBtn" CommandName="Login" CssClass="button" Text="<%$ Resources: EPiServer, login.login %>" ToolTip="<%$ Resources: EPiServer, login.login %>" TabIndex="4" runat="server" />
                                    </div>
                                    
                                    <div id="MessageArea">
                                        <asp:RequiredFieldValidator ControlToValidate="UserName" ErrorMessage="<%$ Resources: EPiServer, login.error.username %>" Display="None" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="Password" ErrorMessage="<%$ Resources: EPiServer, login.error.password %>" Display="None" runat="server" />
                                        <asp:ValidationSummary DisplayMode="List" CssClass="error" runat="server" />
                                        <asp:Label ID="FailureText" CssClass="error" runat="server" />
                                    </div>
                                </fieldset>
                            </LayoutTemplate>
                        </asp:Login>
                    </AnonymousTemplate>
                    
                    <LoggedInTemplate>
                        <EPiServer:Property PropertyName="SecondaryBody" runat="server" />
                        <div id="CurrentUserStatusArea">
                            <asp:literal runat="server" Text="<%$ Resources: EPiServer, login.currentuser %>" /> <asp:LoginName runat="server" />.
                            <asp:LoginStatus LogoutText="<%$ Resources: EPiServer, login.logout %>" runat="server" />
                        </div>
                        <br />
                    </LoggedInTemplate>
                </asp:LoginView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>