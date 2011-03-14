<%@ Control Language="C#" EnableViewState="false" AutoEventWireup="False" CodeBehind="SubMenu.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.SubMenu" %>
<EPiServer:PageTree ShowRootPage="false" runat="server" id="Menu">
	<IndentTemplate>
		<ul>
	</IndentTemplate>
	
	<ItemHeaderTemplate>
			<li>
	</ItemHeaderTemplate>
		
	<ItemTemplate>
				<EPiServer:Property PropertyName="PageLink" runat="server" />
	</ItemTemplate>
	
	<SelectedItemTemplate>
	            <EPiServer:Property CssClass="selected" PropertyName="PageName" runat="server" />
	</SelectedItemTemplate>
	
	<ItemFooterTemplate>
			</li>
	</ItemFooterTemplate>
	
	<UnindentTemplate>
		</ul>
	</UnindentTemplate>
</EPiServer:PageTree>