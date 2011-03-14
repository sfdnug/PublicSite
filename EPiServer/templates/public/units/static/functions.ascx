<%@ Import namespace="EPiServer" %>
<%@ Control Language="C#" AutoEventWireup="False" EnableViewState="true" CodeBehind="Functions.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.Functions" %>

<ul id="Functions">
    <li class="first">
        <asp:LoginView ID="LoginView" runat="server">
            <AnonymousTemplate>
                <a href="<%= GetLoginUrl() %>" class="loginButton"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, login.login %>" /></a>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <asp:LoginStatus runat="server" CssClass="loginButton" ToolTip="<%$ Resources: EPiServer, login.logout %>" LogoutText="<%$ Resources: EPiServer, login.logout %>" />
            </LoggedInTemplate>
        </asp:LoginView>
    </li>
    <li runat="server" visible="false">
        <EPiServer:Property ID="Rss" CssClass="rssButton" PropertyName="PageLink" runat="server" />
    </li>
	<li runat="server" visible="false">
	     <EPiServer:Property ID="Sitemap" CssClass="sitemapButton" PropertyName="PageLink" runat="server" />
	</li>
	<li runat="server" visible="false">
		<asp:HyperLink ID="Language" runat="server" CssClass="languageButton" Visible="false"></asp:HyperLink>
		<asp:Label ID="LanguageListLabel" runat="server" AssociatedControlID="LanguageList" CssClass="hidden" Visible="false" text="Other languages" />
		<asp:DropDownList runat="server" ID="LanguageList" CssClass="languageButton" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ChangeLanguage">
		    <asp:ListItem Text="Other languages" Value="noLangSelected" />
		</asp:DropDownList>
		<noscript>
		    <asp:Button runat="server" ID="LanguageButton" OnClick="ChangeLanguage" Text="OK" Visible="false" />
		</noscript>
	</li>
</ul>