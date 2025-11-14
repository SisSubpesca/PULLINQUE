<%@ Page Language="C#" MasterPageFile="~/Unidades/SitioExperimentalesConcesion.Master" AutoEventWireup="true" CodeBehind="resumenExperimentalesConcesion.aspx.cs" 
Inherits="SubPesca.Unidades.ExperimentalesConcesion.resumenExperimentalesConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="~/Unidades/Concesion/resumenConcesionComponente.ascx" tagname="resumenConcesionComponente" tagprefix="uc0" %>        

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>

    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="FormularioResumenConcesionComponente" ContentPlaceHolderID="rightbody" runat="server">
    
            
    <asp:Panel ID="PanelResumenConcesionComponente"  Visible="true" runat="server">
        <uc0:resumenConcesionComponente ID="resumenConcesionComponente" runat="server" />
    </asp:Panel>

</asp:Content>
