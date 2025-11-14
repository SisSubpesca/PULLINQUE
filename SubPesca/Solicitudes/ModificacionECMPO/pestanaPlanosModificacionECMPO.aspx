<%@ Page Language="C#"  MasterPageFile="~/Solicitudes/SitioSolicitudesModificacionECMPO.Master" AutoEventWireup="true" CodeBehind="pestanaPlanosModificacionECMPO.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionECMPO.pestanaPlanosModificacionECMPO" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/planos.ascx"                          tagname="planos"                        tagprefix="uc11" %>
<%@ Register src="../Registrar/planosInformeTecnicoUOT.ascx"         tagname="planosInformeTecnicoUOT"       tagprefix="uc10" %>
<%@ Register src="../Registrar/planosObservacion.ascx"               tagname="planosObservacion"             tagprefix="uc9" %>

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
                <span id="titulo_modulo">Planos</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />

        <asp:Panel ID="PanelPlanos"  Visible="true" runat="server">
            <uc11:planos ID="planos" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelPlanosInformeTecnicoUOT"  Visible="true" runat="server">
            <uc10:planosInformeTecnicoUOT ID="planosInformeTecnicoUOT" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelPlanosObservacion"  Visible="true" runat="server">
            <uc9:planosObservacion ID="planosObservacion" runat="server" />
        </asp:Panel>

        

</asp:Content>