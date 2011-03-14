<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="PageList.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.PageList" %>
<EPiServer:PageList id="epiPageList" runat="server">
    <HeaderTemplate>
        <hr runat="server" visible="<%# ShowTopRuler %>" class="clear" />
        <div class="pageList">
            <h2><%=Heading%></h2>
            <ul>
    </HeaderTemplate>
    <ItemTemplate>
            <li>
                <%# GetImage(Container.CurrentPage) %>
                <span class="dateTime" visible="<%#DateProperty!=null%>" runat="server"><%#GetFormattedDate(Container.CurrentPage)%></span>
                <h3>
                    <EPiServer:Property PropertyName="PageLink" ToolTip="<%$ Resources: EPiServer, navigation.readmore %>" runat="server" />
                </h3>
                <div>
                    <%#GetPreviewText(Container.CurrentPage)%>
                </div>
            </li>
    </ItemTemplate>	
    <FooterTemplate>
            </ul>
        </div>
        <span class="seeMore" visible="<%#!String.IsNullOrEmpty(SeeMoreText)%>" runat="server"><%#GetContainerLink()%></span>
    </FooterTemplate>
</EPiServer:PageList>

