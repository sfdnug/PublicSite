<%@ Control EnableViewState="true" Language="C#" AutoEventWireup="False" CodeBehind="QuickSearch.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.QuickSearch" %>

<asp:Panel runat="server" CssClass="QuickSearchArea" DefaultButton="SearchButton">
	<asp:Label ID="SearchLabel" runat="server" AssociatedControlID="SearchText" CssClass="hidden" Text="<%$ Resources: EPiServer, navigation.search %>" />
    <asp:TextBox ID="SearchText" onfocus="this.value='';" TabIndex="1" runat="server" CssClass="quickSearchField" Text="<%$ Resources: EPiServer, search.searchstring %>" />
    <asp:ImageButton ID="SearchButton" runat="server" ImageUrl="~/Templates/Public/Images/MainMenuSearchButton.png" ToolTip="<%$ Resources: EPiServer, navigation.search %>" CausesValidation="false" CssClass="quickSearchButton" OnClick="Search_Click" />
</asp:Panel>