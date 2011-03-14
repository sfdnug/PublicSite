<%@ Control Language="C#" AutoEventWireup="False" CodeBehind="Flash.ascx.cs" Inherits="EPiServer.Templates.Public.Units.Placeable.Flash" %>
<object type="application/x-shockwave-flash" data="<%=CurrentPage["EmbeddedURL"]%>" 
    width="<%=CurrentPage["EmbeddedObjectWidth"]%>"
    height="<%=CurrentPage["EmbeddedObjectHeight"]%>">
    <param name="movie" value="<%=CurrentPage["EmbeddedURL"]%>" />
</object>