<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="XForm.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.XFormControl" %>

<asp:Panel runat="server" ID="FormPanel" CssClass="xForm">
	<xforms:xformcontrol ID="FormControl" runat="server" EnableClientScript="false" ValidationGroup="XForm" />
</asp:Panel>
<asp:Panel runat="server" ID="StatisticsPanel" Visible="false">
		<asp:literal id="NumberOfVotes" runat="server" />
		<!-- Set StatisticsType to format output: N=numbers only, P=percentage -->
		<EPiServer:XFormStatistics StatisticsType="P" runat="server" id="Statistics" PropertyName="XForm" />
</asp:Panel>
<br />
<asp:Button runat="server" ID="SwitchButton" CssClass="button" OnClick="SwitchView" CausesValidation="false" Text="<%$ Resources: EPiServer, form.showstat %>" />