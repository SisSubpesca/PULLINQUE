<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesExperimentalesConcesion.Master" AutoEventWireup="true" CodeBehind="pestanaDifusionBancoNaturalExperimentalesConcesion.aspx.cs" 
Inherits="SubPesca.Solicitudes.ExperimentalesConcesion.pestanaDifusionBancoNaturalExperimentalesConcesion" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/difusionBancoNatural.ascx"            tagname="difusionBancoNatural"                          tagprefix="uc24" %>
<%@ Register src="../Registrar/difusionBancoNaturalObservacion.ascx" tagname="difusionBancoNaturalObservacion"               tagprefix="uc23" %>

<%@ Register src="../Registrar/informacionSolicitud.ascx"            tagname="informacionSolicitud"                          tagprefix="uc0" %>         

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<% Response.Write(ResolveClientUrl("~/js/funciones.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/admin/admin_reportes.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/jscal2.js")); %>" type="text/javascript"></script>
    <script src="<% Response.Write(ResolveClientUrl("~/js/jquery/calendar/lang/es.js")); %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="rightbody" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>
    
        <asp:UpdatePanel ID="UpdatePanelInformacionSolicitud" UpdateMode="Conditional" runat="server">
            <ContentTemplate> 
                <asp:Panel ID="PanelInformacionSolicitud"  Visible="true" runat="server">
                    <uc0:informacionSolicitud ID="informacionSolicitud" runat="server" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


         <!-- Título de la página -->
        <table class="formtop" cellpadding="0px" cellspacing="0px">
        <tr>
            <td align="left" valign="middle">
                <span id="titulo_modulo">Difusión Banco Natural</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />
     
        <asp:Panel ID="PanelDifusionBancoNatural"  Visible="true" runat="server">
            <uc24:difusionBancoNatural   ID="difusionBancoNatural" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelDifusionBancoNaturalObservacion"  Visible="true" runat="server">
            <uc23:difusionBancoNaturalObservacion ID="difusionBancoNaturalObservacion" runat="server" />
        </asp:Panel>

        

    

</asp:Content>