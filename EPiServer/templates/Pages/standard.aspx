<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPages/MasterPage.master" AutoEventWireup="true" CodeBehind="Standard.aspx.cs" Inherits="UserGroup.Site.Templates.Standard" %>
<%@ Register TagPrefix="public" TagName="PageList"	Src="~/Templates/Public/Units/Placeable/PageList.ascx" %>
<%@ Register TagPrefix="public" TagName="XForm"		Src="~/Templates/Public/Units/Placeable/XForm.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainRegion" runat="server">
		<EPiServer:Property ID="Property2" PropertyName="MainBody" EnableViewState="false" runat="server" />
        <public:XForm ID="XForm1"
            runat="server" 
            XFormProperty="Form"
            HeadingProperty="XFormHeading"
            ShowStatistics="false" />
</asp:Content>
