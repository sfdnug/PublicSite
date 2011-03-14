<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/MasterPages/MasterPage.master" AutoEventWireup="true" CodeBehind="Speaker.aspx.cs" Inherits="UserGroup.Site.Templates.Speaker" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainRegion" runat="server">
<h1><EPiServer:Property ID="Property1" runat="server" PropertyName="PageName" /></h1>
<EPiServer:Property runat="server" PropertyName="ProfilePicture" />
<div class="twitter"><EPiServer:Property runat="server" PropertyName="TwitterHandle" DisplayMissingMessage="false" /></div>
<div class="blog"><EPiServer:Property runat="server" PropertyName="BlogUrl" DisplayMissingMessage="false" /></div>
<div class="linkedIn"><EPiServer:Property runat="server" PropertyName="LinkedInUrl" /></div>
<div class="facebook"><EPiServer:Property runat="server" PropertyName="FacebookUrl" /></div>
<div class="bio"><EPiServer:Property runat="server" PropertyName="Bio" /></div>
</asp:Content>