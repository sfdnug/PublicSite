<%@ Page Language="C#" AutoEventWireup="False" CodeBehind="Page.aspx.cs" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" Inherits="EPiServer.Templates.Public.Pages.Page" %>
<%@ Register TagPrefix="public" TagName="MainBody"      Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="PageList"		Src="~/Templates/Public/Units/Placeable/PageList.ascx" %>
<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">

	<div id="MainBody">
	    <public:MainBody runat="server" />
        
        <public:PageList
            PreviewTextLength="200"
            PageLinkProperty="MainListRoot"
            MaxCountProperty="MainListCount"
            ShowTopRuler="true" 
            runat="server" />
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SecondaryBodyRegion" runat="server">
    
	<div id="SecondaryBody">
		<EPiServer:Property PropertyName="SecondaryBody" DisplayMissingMessage="false" EnableViewState="false" runat="server" />
		
		<public:PageList
		PageLinkProperty="SecondaryListRoot"
		MaxCountProperty="SecondaryListCount"
		DateProperty="PageSaved"
		runat="server" />
	</div>
	
	
</asp:Content>