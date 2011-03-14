<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="FileList.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.FileList" %>

<EPiServer:FileSystemDataSource runat="server" ID="FileDataSource" IncludeRoot="true" />

<asp:TreeView ID="FileTree" runat="server" DataSourceID="FileDataSource" ExpandDepth="1"
    MaxDataBindDepth="6" OnTreeNodeDataBound="FileTree_TreeNodeDataBound"
    ShowExpandCollapse="true" EnableClientScript="true" PopulateNodesFromClient="true" >
    <DataBindings>
        <asp:TreeNodeBinding TextField="Name" ToolTipField="Title" ImageUrlField="ImageUrl" PopulateOnDemand="true" />
    </DataBindings>
</asp:TreeView>