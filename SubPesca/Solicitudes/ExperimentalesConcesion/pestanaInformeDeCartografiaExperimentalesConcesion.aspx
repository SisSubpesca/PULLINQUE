<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesExperimentalesConcesion.Master" AutoEventWireup="true" CodeBehind="pestanaInformeDeCartografiaExperimentalesConcesion.aspx.cs"
Inherits="SubPesca.Solicitudes.ExperimentalesConcesion.pestanaInformeDeCartografiaExperimentalesConcesion" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<%@ Register src="../Registrar/informeDeCartografia.ascx"            tagname="informeDeCartografia"                          tagprefix="uc30" %>
<%@ Register src="../Registrar/informeDeCartografiaObservacion.ascx" tagname="informeDeCartografiaObservacion"               tagprefix="uc29" %>
<%@ Register src="../Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc0" %>          

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
                    <span id="titulo_modulo">IT. U.O.T.</span>
                </td>
            </tr>
            </table>
            <hr style="width:100%;" />

            <asp:Panel ID="PanelInforCart"  Visible="true" runat="server">
                <uc30:informeDeCartografia ID="informeDeCartografia" runat="server" />
            </asp:Panel>
          
            <asp:Panel ID="PanelInforCartObservacion"  Visible="true" runat="server">
                <uc29:informeDeCartografiaObservacion ID="informeDeCartografiaObservacion" runat="server" />
            </asp:Panel>
</asp:Content>