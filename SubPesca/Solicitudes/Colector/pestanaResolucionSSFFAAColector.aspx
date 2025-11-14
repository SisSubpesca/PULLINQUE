<%@ Page Language="C#"  MasterPageFile="~/Solicitudes/SitioSolicitudesColector.Master" AutoEventWireup="true" CodeBehind="pestanaResolucionSSFFAAColector.aspx.cs" 
Inherits="SubPesca.Solicitudes.Colector.pestanaResolucionSSFFAAColector" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/resolucionSSFFAA.ascx"                tagname="resolucionSSFFAA"              tagprefix="uc2" %>
<%@ Register src="../Registrar/resolucionSSFFAAObservacion.ascx"     tagname="resolucionSSFFAAObservacion"   tagprefix="uc1" %>


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
                <span id="titulo_modulo">Resolución SSFFAA</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />
        
        <asp:Panel ID="PanelResolucionSSFFAA"  Visible="true"  runat="server">
            <uc2:resolucionSSFFAA ID="resolucionSSFFAA" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelResolucionSSFFAAObservacion"  Visible="true" runat="server">
            <uc1:resolucionSSFFAAObservacion ID="resolucionSSFFAAObservacion" runat="server"/>
        </asp:Panel>

        

</asp:Content>