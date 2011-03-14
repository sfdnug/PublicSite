<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="PageFooter.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.PageFooter" %>

<div id="Footer">
	<ul>
		<li runat="server" visible="false" class="first">
		    <EPiServer:Property ID="Privacy" PropertyName="PageLink" AccessKey="8" runat="server" />
		</li>
		<li runat="server" visible="false">
		    <EPiServer:Property ID="Contact" PropertyName="PageLink" AccessKey="7" runat="server" />
		</li>
		<li runat="server" visible="false">
		    <EPiServer:Property ID="Sitemap" PropertyName="PageLink" AccessKey="3" runat="server" />
		</li>
		<li>
			Copyright © <%=DateTime.Now.Year%> EPiServer AB
		</li>
	</ul>
</div>
