<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="NewsItem.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.NewsItem" Title="Untitled Page" %>

<asp:Content ContentPlaceHolderID="SecondaryBodyRegion" runat="server">
	<div id="SecondaryBody">
		<dl>
			<dt><EPiServer:Translate Text="/news/writername" runat="server" /></dt>
			<dd><EPiServer:Property PropertyName="MetaAuthor" runat="server" /></dd>
			<dt><EPiServer:Translate Text="/news/publishdate" runat="server" /></dt>
			<dd><%=CurrentPage.StartPublish.ToString("d") %></dd>
		</dl>
	</div>
</asp:Content>