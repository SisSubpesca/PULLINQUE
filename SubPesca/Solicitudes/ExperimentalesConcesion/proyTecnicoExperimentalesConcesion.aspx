<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesExperimentalesConcesion.Master" AutoEventWireup="true" 
CodeBehind="proyTecnicoExperimentalesConcesion.aspx.cs" Inherits="SubPesca.Solicitudes.ExperimentalesConcesion.proyTecnicoExperimentalesConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/proyTecnicoComponente.ascx"                   tagname="proyTecnicoComponente"                 tagprefix="uc01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-1.7.2.min.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery.autoheight.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/jquery-latest.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>

    <style type="text/css">
        .item
        {
            text-align: center;
        }
        .style2
        {
            width: 51px;
            text-align: center;
        }
        .col3
        {
            text-align: center;
        }
        .style4
        {
            text-align: left;
            width: 219px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            text-align: center;
            width: 218px;
        }
        .style7
        {
            line-height: 200%;
        }
    </style>

</asp:Content>


 

<asp:Content ID="FormularioProyectoTecnico" ContentPlaceHolderID="rightbody" runat="server">

    <asp:Panel ID="PanelProyectoTecnico"  Visible="true" runat="server">
        <uc01:proyTecnicoComponente ID="proyTecnicoComponente" runat="server" />
    </asp:Panel>
  
</asp:Content>
