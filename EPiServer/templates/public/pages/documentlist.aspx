<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" AutoEventWireup="False" CodeBehind="DocumentList.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.DocumentList" %>
<%@ Register TagPrefix="public" TagName="MainBody"  Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>
<%@ Register TagPrefix="public" TagName="DocumentList"  Src="~/Templates/Public/Units/Placeable/DocumentList.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">
    <div id="MainBody" class="documentList">
        <public:MainBody runat="server" />
        <hr />
        <public:DocumentList PageLinkProperty="DocumentListRoot" runat="server" />
    </div>
</asp:Content>