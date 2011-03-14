<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="FileExplorer.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.FileExplorer"%>
<%@ Register TagPrefix="public" TagName="MainBody"  Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="FileList"  Src="~/Templates/Public/Units/Placeable/FileList.ascx" %>
<%@ Register TagPrefix="public" TagName="PageList"		Src="~/Templates/Public/Units/Placeable/PageList.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">
    <div id="MainBody">
        <public:MainBody runat="server" />
        <div>
            <asp:Label ID="ErrorMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            <public:FileList runat="server" ID="FileListControl" />
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SecondaryBodyRegion" runat="server">

	<div id="SecondaryBody">
		<EPiServer:Property PropertyName="SecondaryBody" EnableViewState="false" runat="server" />
		
		<public:PageList
		PageLinkProperty="SecondaryListRoot"
		MaxCountProperty="SecondaryListCount"
		DateProperty="PageSaved"
		runat="server" />
	</div>
	
</asp:Content>