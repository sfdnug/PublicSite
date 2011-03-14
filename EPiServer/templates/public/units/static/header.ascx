<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="False" CodeBehind="Header.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.Header" %>
<asp:PlaceHolder ID="plhMetaDataArea" runat="server" />
<link rel="stylesheet" type="text/css" media="screen" href="<%=ResolveUrl("~/Templates/Public/Styles/Glossy/Styles.css")%>"  />
<link rel="stylesheet" type="text/css" media="print" href="<%=ResolveUrl("~/Templates/Public/Styles/print.css")%>"  />
<EPiServer:PageList ID="RssList" PageLinkProperty="RssContainer" runat="server">
    <ItemTemplate>
        <%#GetRss(Container.CurrentPage)%>
    </ItemTemplate>
</EPiServer:PageList>