<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPages/MasterPage.master" AutoEventWireup="true" CodeBehind="EventsList.aspx.cs" Inherits="UserGroup.Site.Templates.EventsList" %>
<%@ Register TagPrefix="public" TagName="PageList"	Src="~/Templates/Public/Units/Placeable/PageList.ascx" %>
<%@ Register TagPrefix="public" TagName="XForm"		Src="~/Templates/Public/Units/Placeable/XForm.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainRegion" runat="server">
<h1>Event Title: <EPiServer:Property runat="server" PropertyName="PageName" /></h1>
Start Time: <EPiServer:Property runat="server" PropertyName="StartTime" />
End Time: <EPiServer:Property runat="server" PropertyName="EndTime" />
Location:

<div class="topic">
    <EPiServer:Property runat="server" PropertyName="MainContent" />
</div>
</asp:Content>
