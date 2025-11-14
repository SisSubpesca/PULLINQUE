<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacionCentroFaenamiento.Master" AutoEventWireup="true" CodeBehind="pestanaDifrolModificacionCentroFaenamiento.aspx.cs" 
Inherits="SubPesca.Solicitudes.ModificacionCentroFaenamiento.pestanaDifrolModificacionCentroFaenamiento"  Theme="admin_style" %>



<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/difrol.ascx"                          tagname="difrol"                                        tagprefix="uc22" %>
<%@ Register src="../Registrar/difrolObservacion.ascx"               tagname="difrolObservacion"                             tagprefix="uc21" %>

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
                    <span id="titulo_modulo">DIFROL</span>
                </td>
            </tr>
            </table>
            <hr style="width:100%;" />
    
            <asp:Panel ID="PanelDifrol"  Visible="true" runat="server">
                <uc22:difrol ID="difrol" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelDifrolObservacion"  Visible="true" runat="server">
                <uc21:difrolObservacion ID="difrolObservacion" runat="server" />
            </asp:Panel>

            

    

</asp:Content>