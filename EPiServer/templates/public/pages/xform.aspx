<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="XForm.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.XFormPage" %>
<%@ Register TagPrefix="public" TagName="MainBody"  Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="XForm"		Src="~/Templates/Public/Units/Placeable/XForm.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">
	<div id="MainBody">
	
	    <public:MainBody runat="server" />
	    
        <public:XForm
            runat="server" 
            XFormProperty="XForm"
            HeadingProperty="XFormHeading"
            ShowStatistics="false" />
	    
    </div>
</asp:Content>