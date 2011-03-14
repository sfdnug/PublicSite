<%@ Page Language="C#" AutoEventWireup="False" Codebehind="Default.aspx.cs" MasterPageFile="~/Templates/MasterPages/MasterPage.master" Inherits="UserGroup.Site.Default"%>
<%@ Register TagPrefix="public" TagName="XForm"		Src="~/Templates/Public/Units/Placeable/XForm.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainRegion" runat="server">
		<EPiServer:Property ID="Property2" PropertyName="MainBody" EnableViewState="false" runat="server" />
        <public:XForm ID="XForm1"
            runat="server" 
            XFormProperty="Form"
            HeadingProperty="XFormHeading"
            ShowStatistics="false" />
</asp:Content>