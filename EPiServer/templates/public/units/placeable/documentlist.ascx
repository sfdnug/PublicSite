<%@ Control Language="C#" AutoEventWireup="False" Codebehind="DocumentList.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.DocumentList" %>
<%@ Register TagPrefix="public" TagName="Document"  Src="~/Templates/Public/Units/Placeable/Document.ascx" %>

<EPiServer:PageList ID="DocList" runat="server">
    <HeaderTemplate>
        <h2><%= Heading %></h2>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <h3><%# Container.CurrentPage.Property["PageName"].ToWebString() %></h3>
            <%# GetPreviewText(Container) %>
            
            <div class="download">
                <EPiServer:Translate runat="server" Text="/document/download" /> <public:document documentpage="<%# Container.CurrentPage %>" runat="server" />
           </div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</EPiServer:PageList>
