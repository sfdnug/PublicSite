<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="False" CodeBehind="MainMenu.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.MainMenu" %>
<EPiServer:MenuList runat="server" id="Menu">
    <HeaderTemplate>
    <nav>
        <ul class="menu menu-main">                 
    </HeaderTemplate>
    <ItemTemplate>
            <li class="unselected"><EPiServer:Property runat="server"  PropertyName="PageLink" /></li>
    </ItemTemplate>
    <SelectedTemplate>
            <li class="current"><EPiServer:Property runat="server" PropertyName="PageLink" /></li>
    </SelectedTemplate>
    <FooterTemplate>
        </ul>
    </nav>
    </FooterTemplate>
</EPiServer:MenuList>