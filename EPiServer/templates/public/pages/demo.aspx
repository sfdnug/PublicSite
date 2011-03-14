<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="Demo.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.Demo" %>
<%@ Register TagPrefix="public" TagName="MainBody"  Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="XForm"		Src="~/Templates/Public/Units/Placeable/XForm.ascx" %>
<%@ Register TagPrefix="public" TagName="RssReader" Src="~/Templates/Public/Units/Placeable/RssReader.ascx" %>
<%@ Register TagPrefix="public" TagName="Flash"     Src="~/Templates/Public/Units/Placeable/Flash.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">
	<div id="MainBody">
	    <public:MainBody runat="server" />
	    <public:Flash runat="server" ID="Flash" />
        <div id="RssReaderArea">
            <public:RssReader runat="server" />
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SecondaryBodyRegion" runat="server">
	<div id="SecondaryBody">
		<EPiServer:Property PropertyName="SecondaryBody" EnableViewState="false" runat="server" />	    

		 <public:XForm runat="server" XFormProperty="XForm" ShowStatistics="true" />
	</div>
</asp:Content>
