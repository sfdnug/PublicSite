<%@ Control Language="C#" AutoEventWireup="False" Codebehind="RssReader.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.RssReader" %>

<asp:Repeater ID="Rss" runat="server">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <h3>
                <a href="<%# HttpUtility.UrlPathEncode(XPath("link").ToString()) %>">
                    <%# XPath("title") %>
                </a>
            </h3>
            <%# XPath("description") %>
       </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<asp:Literal ID="ErrorMessage" runat="server" Visible="false" />
