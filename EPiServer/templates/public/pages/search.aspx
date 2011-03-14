<%@ Page Language="C#" MasterPageFile="~/Templates/Public/MasterPages/MasterPage.master" Codebehind="Search.aspx.cs" Inherits="EPiServer.Templates.Public.Pages.SearchPage" %>
<%@ Register TagPrefix="public" TagName="MainBody" Src="~/Templates/Public/Units/Placeable/MainBody.ascx" %>

<asp:Content ContentPlaceHolderID="MainBodyRegion" runat="server">

    <div id="MainBody">
    
        <public:MainBody runat="server" />

        <div id="SearchArea">
            <asp:Label ID="SearchLabel" runat="server" />
            <asp:TextBox ID="SearchText" CssClass="searchText" text="<%$ Resources: EPiServer, search.searchstring %>" onfocus="this.value='';" TabIndex="1" runat="server" />
            <asp:Button ID="SearchButton" CssClass="button" runat="server" TabIndex="2" OnClick="SearchClick" Text="<%$ Resources: EPiServer, navigation.search %>" /><br />
            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="SearchText" Display="Dynamic" runat="server" />

            <div id="AdvancedArea">
                <asp:CheckBox ID="SearchInFiles" runat="server" />
			    <asp:Label runat="server" AssociatedControlID="SearchInFiles" Text="<%$ Resources: EPiServer, search.searchfiles %>"/>
			    <asp:CheckBox ID="SearchOnlyWholeWords" runat="server" />
			    <asp:Label runat="server" AssociatedControlID="SearchOnlyWholeWords" Text="<%$ Resources: EPiServer, search.onlywholewords %>"/>
            </div>                
        </div>
        
        <div id="ResultArea">
        
            <asp:Repeater ID="SearchResult" DataSourceID="SearchDataSource" runat="server" Visible="false">
                <HeaderTemplate>
                    <h2><asp:Literal runat="server" Text="<%$ Resources: EPiServer, search.searchresult %>" /></h2>
                    <ol>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <a href="<%#EPiServer.UriSupport.AddLanguageSelection((string)DataBinder.Eval(Container.DataItem, "LinkURL"), (string)DataBinder.Eval(Container.DataItem, "LanguageBranch")) %>"><%# ((EPiServer.Core.PageData)Container.DataItem).Property["PageName"].ToWebString() %></a>&nbsp;(<span class="dateTime" runat="server"><%# DataBinder.Eval(Container.DataItem, "Changed", "{0:d}")%></span>)
                        <p>
                             <%# GetPreviewText(Container.DataItem) %>
                        </p>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:Repeater>
            
            <h2 id="NoSearchResult" runat="server" visible="false"><asp:Literal runat="server" Text="<%$ Resources: EPiServer, search.nosearchresult %>" /></h2>
        </div>
        
    </div>
    
    <EPiServer:SearchDataSource ID="SearchDataSource" runat="server" EnableVisibleInMenu="false">
        <SelectParameters>
            <EPiServer:PropertyParameter Name="PageLink" PropertyName="SearchRoot" />
            <asp:controlparameter name="SearchQuery" controlid="SearchText" propertyname="Text"/>
            <asp:controlparameter name="SearchFiles" controlid="SearchInFiles" propertyname="Checked"/>
            <asp:controlparameter name="OnlyWholeWords" controlid="SearchOnlyWholeWords" propertyname="Checked"/>
        </SelectParameters>
    </EPiServer:SearchDataSource>
    
</asp:Content>
