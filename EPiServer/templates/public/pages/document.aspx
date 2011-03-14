<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="Document.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.Document" %>
<%@ Register TagPrefix="public" TagName="MainBody"  Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="Document"  Src="~/Templates/Public/Units/Placeable/Document.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">
    <div id="MainBody">
        <public:MainBody runat="server" />
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="SecondaryBodyRegion" runat="server">
    <div id="SecondaryBody">
        <public:Document runat="server" />
        <EPiServer:Property PropertyName="SecondaryBody" EnableViewState="false" runat="server" />
    </div>
</asp:Content>