<%@ Control Language="C#" EnableViewState="true" AutoEventWireup="False" CodeBehind="PageHeader.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Static.PageHeader" %>
<%@ Register TagPrefix="public" TagName="Functions"	Src="~/Templates/Public/Units/Static/Functions.ascx"%>

<div id="Header">
	<div id="Logotype">
		<asp:HyperLink accesskey="1" ID="lnkLogotype" runat="server" />
	</div>
	<public:Functions runat="server" />
</div>
