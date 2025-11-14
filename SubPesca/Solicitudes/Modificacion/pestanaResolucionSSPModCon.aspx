<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesModificacion.Master"  AutoEventWireup="true" CodeBehind="pestanaResolucionSSPModCon.aspx.cs" 
Inherits="SubPesca.Solicitudes.Modificacion.pestanaResolucionSSPModCon" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>


<%@ Register src="~/Solicitudes/Registrar/resolucionSSP.ascx"                   tagname="resolucionSSP"                 tagprefix="uc5" %>
<%@ Register src="~/Solicitudes/Registrar/resolucionSSPDevolucionSSFFAA.ascx"   tagname="resolucionSSPDevolucionSSFFAA" tagprefix="uc4" %>
<%@ Register src="~/Solicitudes/Registrar/resolucionSSPObservacion.ascx"        tagname="resolucionSSPObservacion"      tagprefix="uc3" %>

<%@ Register src="~/Solicitudes/Registrar/informacionSolicitud.ascx"                   tagname="informacionSolicitud"                 tagprefix="uc0" %>   


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
                <span id="titulo_modulo">Resolución SSP</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />

        <asp:Panel ID="PanelResolucionSSP"  Visible="true" runat="server">
            <uc5:resolucionSSP ID="resolucionSSP" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelResolucionSSPDevolucionSSFFAA"  Visible="true" runat="server">
            <uc4:resolucionSSPDevolucionSSFFAA ID="resolucionSSPDevolucionSSFFAA" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelResolucionSSPObservacion"  Visible="true" runat="server">
            <uc3:resolucionSSPObservacion ID="resolucionSSPObservacion" runat="server" />
        </asp:Panel>
    
        

</asp:Content>