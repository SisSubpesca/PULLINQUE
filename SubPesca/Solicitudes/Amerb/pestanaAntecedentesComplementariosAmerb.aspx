<%@ Page Language="C#" MasterPageFile="~/Solicitudes/SitioSolicitudesAmerb.Master" AutoEventWireup="true" CodeBehind="pestanaAntecedentesComplementariosAmerb.aspx.cs" 
Inherits="SubPesca.Solicitudes.Amerb.pestanaAntecedentesComplementariosAmerb" Theme="admin_style" %>


<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>

<%@ Register src="../Registrar/antecedentesComplementarios.ascx"                 tagname="antecedentesComplementarios"               tagprefix="uc13" %>
<%@ Register src="../Registrar/certificadoOperacion.ascx"                        tagname="certificadoOperacion"                      tagprefix="uc33" %>
<%@ Register src="../Registrar/antecedentesComplementariosObservacion.ascx"      tagname="antecedentesComplementariosObservacion"    tagprefix="uc12" %>


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
                <span id="titulo_modulo">Antecedentes Complementarios</span>
            </td>
        </tr>
        </table>
        <hr style="width:100%;" />

        <asp:Panel ID="PanelAntecedentesComplementarios"  Visible="true" runat="server">
            <uc13:antecedentesComplementarios ID="antecedentesComplementarios" runat="server" />
        </asp:Panel>

        <asp:Panel ID="PanelCertificadoOperacion"  Visible="false" runat="server">
                
        </asp:Panel>

        <asp:Panel ID="PanelAntecedentesComplementariosObservacion"  Visible="true" runat="server">
            <uc12:antecedentesComplementariosObservacion ID="antecedentesComplementariosObservacion" runat="server" />
        </asp:Panel>

        
    

</asp:Content>