<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioCatastroUnidades.Master" AutoEventWireup="true" CodeBehind="pestanaBancoNatural.aspx.cs" 
Inherits="SubPesca.Solicitudes.Registrar.pestanaBancoNatural" Theme="admin_style" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="bancoNatural.ascx"                    tagname="bancoNatural"                                  tagprefix="uc26" %>
<%@ Register src="bancoNaturalObservacion.ascx"         tagname="bancoNaturalObservacion"                       tagprefix="uc25" %> 

<%@ Register src="informacionSolicitud.ascx"            tagname="informacionSolicitud"                          tagprefix="uc0" %>         

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
                <span id="titulo_modulo">Banco Natural</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />

        <asp:Panel ID="PanelBancoNatural"  Visible="true" runat="server">
            <uc26:bancoNatural ID="bancoNatural" runat="server" />
        </asp:Panel>
        <asp:Panel ID="PanelBancoNaturalObservacion"  Visible="true" runat="server">
            <uc25:bancoNaturalObservacion ID="bancoNaturalObservacion" runat="server" />
        </asp:Panel>

        
    

</asp:Content>